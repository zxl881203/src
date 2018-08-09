﻿using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class MonthIndirectCostTab : PageBase, IRequiresSessionState
	{

		public AccountReportAction arAction
		{
			get
			{
				return new AccountReportAction();
			}
		}
		public Guid ProjectCode
		{
			get
			{
				object obj = this.ViewState["ProjectCode"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["ProjectCode"] = value;
			}
		}
		public DateTime dtDateTime
		{
			get
			{
				object obj = this.ViewState["dtDateTime"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return DateTime.Now;
			}
			set
			{
				this.ViewState["dtDateTime"] = value;
			}
		}
		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				CryReport cryReport = new CryReport();
				this.ddlPrjname.DataSource = cryReport.GetWorkUsepcList("", base.UserCode);
				this.ddlPrjname.DataTextField = "pn";
				this.ddlPrjname.DataValueField = "pc";
				this.ddlPrjname.DataBind();
				this.Bind_Date();
				this.DGrd_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.BtnSearch.Click += new EventHandler(this.BtnSearch_Click);
			this.DGrdMaterialExpend.ItemCreated += new DataGridItemEventHandler(this.DGrdMaterialExpend_ItemCreated);
			this.DGrdMaterialExpend.ItemDataBound += new DataGridItemEventHandler(this.DGrdMaterialExpend_ItemDataBound);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void Bind_Date()
		{
			try
			{
				this.DtbStartDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
			}
			catch
			{
			}
			this.DtbEndDate.Text = DateTime.Now.ToShortDateString();
		}
		private void DGrd_Bind()
		{
			DateTime dtStartDate = Convert.ToDateTime(this.DtbStartDate.Text.Trim());
			DateTime dtEndDate = Convert.ToDateTime(this.DtbEndDate.Text.Trim());
			if (this.ddlPrjname.Items.Count > 0)
			{
				this.ProjectCode = new Guid(this.ddlPrjname.SelectedValue);
			}
			DataTable monthInDirectCostAccount = this.arAction.GetMonthInDirectCostAccount(this.ProjectCode, dtStartDate, dtEndDate);
			this.DGrdMaterialExpend.DataSource = monthInDirectCostAccount;
			this.DGrdMaterialExpend.DataBind();
		}
		private void DGrdMaterialExpend_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
			{
				TableCell tableCell = new TableCell();
				tableCell.RowSpan = 2;
				tableCell.BackColor = Color.FromName("#ece9d8");
				TableCell expr_2E = tableCell;
				expr_2E.Text += " <font style=\"FONT-WEIGHT: bold\">间接成本编号</font></td> ";
				TableCell expr_44 = tableCell;
				expr_44.Text += "   <td rowspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">间接成本项目</font></td> ";
				TableCell expr_5A = tableCell;
				expr_5A.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">预算成本</font></td> ";
				TableCell expr_70 = tableCell;
				expr_70.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际成本</font></td> ";
				TableCell expr_86 = tableCell;
				expr_86.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">实际偏差</font></td> ";
				TableCell expr_9C = tableCell;
				expr_9C.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">目标偏差</font></td> ";
				TableCell expr_B2 = tableCell;
				expr_B2.Text += "   <td colspan=2 height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">占产值的百分数(%)</font></td> ";
				TableCell expr_C8 = tableCell;
				expr_C8.Text += "   <td colspan=2></td> ";
				TableCell expr_DE = tableCell;
				expr_DE.Text += "  </tr> ";
				TableCell expr_F4 = tableCell;
				expr_F4.Text += "  <tr> ";
				TableCell expr_10A = tableCell;
				expr_10A.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">本月</font></td> ";
				TableCell expr_120 = tableCell;
				expr_120.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">累计</font></td> ";
				TableCell expr_136 = tableCell;
				expr_136.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">本月</font></td> ";
				TableCell expr_14C = tableCell;
				expr_14C.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">累计</font></td> ";
				TableCell expr_162 = tableCell;
				expr_162.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">本月</font></td> ";
				TableCell expr_178 = tableCell;
				expr_178.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">累计</font></td> ";
				TableCell expr_18E = tableCell;
				expr_18E.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">本月</font></td> ";
				TableCell expr_1A4 = tableCell;
				expr_1A4.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">累计</font></td> ";
				TableCell expr_1BA = tableCell;
				expr_1BA.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">本月</font></td> ";
				TableCell expr_1D0 = tableCell;
				expr_1D0.Text += "   <td height=20 align=center style='border-left:#ffffff 1px solid;border-top:#ffffff 1px solid;border-right:#aca899 1px solid;border-bottom:#aca899 1px solid;text-align:center;' bgcolor='#ece9d8'><font style=\"FONT-WEIGHT: bold\">累计</font> ";
				e.Item.Cells.Clear();
				e.Item.Cells.Add(tableCell);
			}
		}
		private void DGrdMaterialExpend_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Cells[14].Text = Convert.ToDouble(dataRowView["CurrMonthPercent"].ToString()).ToString("p");
				e.Item.Cells[15].Text = Convert.ToDouble(dataRowView["JournalPercent"].ToString()).ToString("p");
			}
		}
		private void BtnSearch_Click(object sender, EventArgs e)
		{
			this.DGrd_Bind();
		}
		protected void btnexecl_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "月度间接成本分析表.xls");
		}
		protected void btnWord_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "月度间接成本分析表.doc");
		}
		public void ExportToExcel(string FileType, string FileName)
		{
			base.Response.Charset = "GB2312";
			base.Response.ContentEncoding = Encoding.UTF8;
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
			base.Response.ContentType = FileType;
			this.EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			this.TableOUT.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
	}


