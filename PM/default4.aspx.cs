﻿using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class _default : Page, IRequiresSessionState
{
	private static int _invalidTime = Convert.ToInt32(ConfigurationSettings.AppSettings["RefreshTime"]);

	protected void Page_Load(object sender, EventArgs e)
	{
		this.labYear.Text = "2003-" + DateTime.Now.Year.ToString();
		base.Response.Cache.SetNoStore();
		this.ImageButton1.Attributes["onclick"] = "if( !validatorSpace() ) {return false;}";
		if (!this.Page.IsPostBack)
		{
			this.InitCookName();
			this.clearSession();
		}
	}
	protected override void OnInit(EventArgs e)
	{
		this.InitializeComponent();
		base.OnInit(e);
	}
	private void InitializeComponent()
	{
	}
	private void AddUserInfo(string userCode, bool isReturn)
	{
		DataTable dataTable = this.GetUserTable();
		base.Application.Lock();
		if (isReturn)
		{
			if (base.Application["TempUserCodeCollection"] != null)
			{
				dataTable = (DataTable)base.Application["TempUserCodeCollection"];
			}
		}
		else
		{
			if (base.Application["UserCodeCollection"] != null)
			{
				dataTable = (DataTable)base.Application["UserCodeCollection"];
			}
		}
		DataRow dataRow = dataTable.NewRow();
		dataRow["userCode"] = userCode;
		dataRow["loginID"] = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, userCode);
		dataRow["loginTime"] = DateTime.Now;
		dataRow["endTime"] = DateTime.Now;
		dataRow["ip"] = this.getIP();
		dataTable.Rows.Add(dataRow);
		if (isReturn)
		{
			base.Application["TempUserCodeCollection"] = dataTable;
		}
		else
		{
			base.Application["UserCodeCollection"] = dataTable;
		}
		base.Application.UnLock();
	}
	private int IsReturnLogin(string userCode)
	{
		string iP = this.getIP();
		if (base.Application["UserCodeCollection"] != null)
		{
			DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
			int i = 0;
			while (i < dataTable.Rows.Count)
			{
				if (dataTable.Rows[i]["userCode"].ToString() == userCode)
				{
					if (dataTable.Rows[i]["ip"].ToString() != iP)
					{
						return 2;
					}
					return 1;
				}
				else
				{
					i++;
				}
			}
		}
		return 0;
	}
	private bool IsRuleByText(string strText)
	{
		return strText.IndexOf("'", 0) == -1 && strText.IndexOf("-", 0) == -1 && strText.IndexOf("/", 0) == -1;
	}
	private DataTable GetUserTable()
	{
		DataTable dataTable = new DataTable();
		DataColumn column = new DataColumn("userCode", Type.GetType("System.String"));
		dataTable.Columns.Add(column);
		column = new DataColumn("loginID", Type.GetType("System.String"));
		dataTable.Columns.Add(column);
		column = new DataColumn("loginTime", Type.GetType("System.DateTime"));
		dataTable.Columns.Add(column);
		column = new DataColumn("endTime", Type.GetType("System.DateTime"));
		dataTable.Columns.Add(column);
		column = new DataColumn("ip", Type.GetType("System.String"));
		dataTable.Columns.Add(column);
		return dataTable;
	}
	public void Useronline()
	{
		string iP = this.getIP();
		if (!this.IsRuleByText(this.tb_yhdm.Value) || !this.IsRuleByText(this.tb_pwd.Value))
		{
			string script = "<script language='javascript'>alert('对不起！用户名或者密码输入错误，请重新输入!');</script>";
			base.ClientScript.RegisterStartupScript(base.GetType(), "IsRule", script);
			return;
		}
		HttpCookie httpCookie = base.Request.Cookies["cbPass"];
		if (httpCookie != null && this.tb_pwd.Value == "justwin")
		{
			this.tb_pwd.Value = EncryptionUtility.Decryption(httpCookie.Value);
		}
		object obj = userManageDb.ValidatorLoginAccess(this.tb_yhdm.Value, this.tb_pwd.Value);
		int num = this.IsReturnLogin((obj == null) ? "" : ((string)obj));
		bool flag = true;
		if (obj != null && flag)
		{
			SysManageDb sysManageDb = new SysManageDb();
			this.Session["SysID"] = sysManageDb.GetDefault().ToString();
			this.Session["yhdm"] = Convert.ToString(obj);
			this.Session["yhdma"] = Convert.ToString(obj);
			this.Session["SkinID"] = userManageDb.getUserSkin(obj.ToString());
			this.Session["SkinID"] = "4";
			this.Session["PmSet"] = PmTypeAction.GetPmType(obj.ToString());
			this.Session["ptcs"] = myxml.Getcs(base.Server.MapPath("/ptcs.txt"));
			this.Session["pttest"] = "notest";
			if (this.Session["pttest"].ToString() != "notest" && (this.Session["ptcs"] == null || Convert.ToInt32(this.Session["ptcs"]) >= 200))
			{
				string str = "请找甲乙软件联系。";
				string script2 = string.Format("<script language='javascript'>alert('试用已到期；如需要继续使用,\\r\\n'+'" + str + "');</script>", new object[0]);
				base.ClientScript.RegisterStartupScript(base.GetType(), "Startup", script2);
				return;
			}
			this.Session["OpModules"] = userManageDb.GetUserOpModules(this.Session["yhdm"].ToString());
			this.Session["CorpCode"] = userManageDb.GetCorpCode(this.Session["yhdm"].ToString());
			HttpCookie httpCookie2 = new HttpCookie("LogName");
			httpCookie2.Values["key1"] = "zyg";
			httpCookie2.Expires = DateTime.Now.AddHours(2.0);
			base.Response.Cookies.Add(httpCookie2);
			loginLogDb loginLogDb = new loginLogDb();
			loginLogDb.UserlLoginInfo(iP, this.Session["yhdm"].ToString());
			if (num == 0)
			{
				this.AddUserInfo(this.Session["yhdm"].ToString(), false);
			}
			else
			{
				base.Application.Lock();
				DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
				foreach (DataRow dataRow in dataTable.Rows)
				{
					if (dataRow["userCode"].ToString() == obj.ToString())
					{
						dataRow["loginTime"] = DateTime.Now;
						dataRow["endTime"] = DateTime.Now;
						dataRow["ip"] = iP;
						break;
					}
				}
				base.Application["UserCodeCollection"] = dataTable;
				base.Application.UnLock();
			}
			FormsAuthentication.RedirectFromLoginPage(this.Session["yhdm"].ToString(), false);
			AduitAction.AddToLog(this.Session["yhdm"].ToString(), "登录系统", iP, this.tb_yhdm.Value);
			this.setCookUName();
			base.Response.Redirect("./SysFrame/frameset.aspx");
			return;
		}
		else
		{
			if (obj == null)
			{
				string script3 = "<script language='javascript'>alert('对不起！用户名或者密码错误，请重新输入!');</script>";
				base.ClientScript.RegisterStartupScript(base.GetType(), "JavaScript", script3);
				return;
			}
			if (!flag)
			{
				string script4 = "<script language='javascript'>alert('对不起！验证码输入错误，请重新输入!');</script>";
				base.ClientScript.RegisterStartupScript(base.GetType(), "JavaScript", script4);
			}
			return;
		}
	}
	protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
	{
	}
	public void clearSession()
	{
		HttpCookie httpCookie = new HttpCookie("LogName");
		httpCookie.Values["key1"] = "greatWall";
		httpCookie.Expires = DateTime.MinValue;
		base.Response.Cookies.Add(httpCookie);
	}
	protected void ImageButton1_Click2(object sender, ImageClickEventArgs e)
	{
		int num = int.Parse(base.Application["num"].ToString());
		if (num == 0)
		{
			this.Useronline();
			return;
		}
		if (base.Application["UserCodeCollection"] == null)
		{
			this.Useronline();
			return;
		}
		int count = ((DataTable)base.Application["UserCodeCollection"]).Rows.Count;
		if (count > num)
		{
			base.Response.Write("<script>alert('对不起，只允许登陆" + num + "个用户');</script>");
			return;
		}
		this.Useronline();
	}
	public void setCookUName()
	{
		HttpCookie httpCookie = base.Request.Cookies["cbName"];
		if (httpCookie == null)
		{
			httpCookie = new HttpCookie("cbName");
		}
		httpCookie.Expires = DateTime.MaxValue;
		base.Response.Cookies.Set(httpCookie);
		HttpCookie httpCookie2 = base.Request.Cookies["cbPass"];
		if (httpCookie2 == null)
		{
			httpCookie2 = new HttpCookie("cbPass");
		}
		httpCookie2.Expires = DateTime.MaxValue;
		base.Response.Cookies.Set(httpCookie2);
		if (this.cbName.Checked && this.cbPass.Checked)
		{
			httpCookie.Value = HttpUtility.UrlEncode(this.tb_yhdm.Value);
			httpCookie2.Value = EncryptionUtility.Encryption(this.tb_pwd.Value);
			base.Response.Cookies.Add(httpCookie);
			base.Response.Cookies.Add(httpCookie2);
			return;
		}
		if (this.cbName.Checked && !this.cbPass.Checked)
		{
			httpCookie.Value = HttpUtility.UrlEncode(this.tb_yhdm.Value);
			base.Response.Cookies.Add(httpCookie);
			httpCookie2.Expires = DateTime.Now;
			base.Response.Cookies.Set(httpCookie2);
			return;
		}
		httpCookie2.Expires = DateTime.Now;
		httpCookie.Expires = DateTime.Now;
		base.Response.Cookies.Set(httpCookie2);
		base.Response.Cookies.Set(httpCookie);
	}
	public void InitCookName()
	{
		HttpCookie httpCookie = base.Request.Cookies["cbName"];
		HttpCookie httpCookie2 = base.Request.Cookies["cbPass"];
		if (httpCookie != null)
		{
			this.cbName.Checked = true;
			this.tb_yhdm.Value = HttpUtility.UrlDecode(httpCookie.Value);
		}
		else
		{
			this.tb_yhdm.Value = "";
		}
		if (httpCookie2 != null)
		{
			this.cbPass.Checked = true;
			base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), "<script>$('#tb_pwd').val('justwin')</script>");
			return;
		}
		this.tb_pwd.Value = "";
	}
	public string getIP()
	{
		string text = string.Empty;
		text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
		if (text == null || text == string.Empty)
		{
			text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
		}
		if (text == null || text == string.Empty)
		{
			text = HttpContext.Current.Request.UserHostAddress;
		}
		if (text == null || text == string.Empty)
		{
			return "0.0.0.0";
		}
		if (text == "::1")
		{
			text = "127.0.0.1";
		}
		return text;
	}
}


