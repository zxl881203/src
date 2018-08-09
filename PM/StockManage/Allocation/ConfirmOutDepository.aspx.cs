﻿using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.Warn;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Allocation_ConfirmOutDepository : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		base.Response.Clear();
		if (!base.IsPostBack)
		{
			this.GVAllocationList.PageSize = NBasePage.pagesize;
			this.Bind_GVAllocationList();
		}
	}
	protected void Bind_GVAllocationList()
	{
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(" flowstate!=-3 and flowstate=1 ");
		stringBuilder.Append(" and OutAllocationPerson='" + base.UserCode + "' ");
		stringBuilder.Append(" order by intime desc");
		DataTable allocationList = allocationBllAction.GetAllocationList(stringBuilder.ToString());
		GridViewUtility.DataBind(this.GVAllocationList, allocationList);
	}
	protected void GVAllocationList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				AllocationBllAction allocationBllAction = new AllocationBllAction();
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				e.Row.Cells[1].Text = (this.GVAllocationList.PageSize * this.GVAllocationList.PageIndex + e.Row.RowIndex + 1).ToString();
				HyperLink hyperLink = (HyperLink)e.Row.Cells[2].FindControl("HyperLink1");
				hyperLink.Text = string.Concat(new string[]
				{
					"<a class='al' href=javascript:viewopen('AuditPage.aspx?ic=",
					dataRowView["aid"].ToString(),
					"&BusiClass=001&BusiCode=075',820,500) >",
					dataRowView["acode"].ToString(),
					"</a>"
				});
				if (e.Row.Cells[8].Text == "已审核")
				{
					e.Row.Cells[8].ForeColor = Color.Green;
				}
				else
				{
					e.Row.Cells[6].ForeColor = Color.Red;
				}
				e.Row.Cells[3].Text = allocationBllAction.GetTreasuryNameByCode(dataRowView["tcodea"].ToString()).Rows[0][0].ToString();
				e.Row.Cells[4].Text = allocationBllAction.GetTreasuryNameByCode(dataRowView["tcodeb"].ToString()).Rows[0][0].ToString();
				((CheckBox)e.Row.Cells[0].FindControl("chkSelectIt")).Attributes["onclick"] = string.Concat(new object[]
				{
					"chkClick('",
					Convert.ToBoolean(dataRowView["isouta"]),
					"',this.checked,'",
					dataRowView["acode"].ToString(),
					"');"
				});
				e.Row.Attributes["onclick"] = string.Concat(new object[]
				{
					"rowClick('",
					Convert.ToBoolean(dataRowView["isouta"]),
					"','",
					dataRowView["acode"].ToString(),
					"');"
				});
				e.Row.Attributes["id"] = dataRowView["acode"].ToString();
				if (Convert.ToBoolean(dataRowView["isouta"]))
				{
					e.Row.Cells[6].Text = "已调出";
					e.Row.Cells[6].ForeColor = Color.Green;
				}
				else
				{
					e.Row.Cells[6].Text = "未调出";
					e.Row.Cells[6].ForeColor = Color.Red;
				}
				if (Convert.ToBoolean(dataRowView["isinb"]))
				{
					e.Row.Cells[7].Text = "已接收";
					e.Row.Cells[7].ForeColor = Color.Green;
				}
				else
				{
					e.Row.Cells[7].Text = "未接收";
					e.Row.Cells[7].ForeColor = Color.Red;
				}
			}
			catch
			{
			}
		}
	}
	protected void btnConfirm_Click(object sender, EventArgs e)
	{
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		string text = (this.HdnAcode.Value == "") ? this.HdnAcodeList.Value : this.HdnAcode.Value;
		int num = allocationBllAction.OutDepositoryConfirm(text, this.Session["yhdm"].ToString());
		if (num != 1)
		{
			this.RegisterStartupScript("alert", "<script>alert('\\n系统提示：\\n\\n库存不足 或者 没有权限！');</script>");
			return;
		}
		DataTable allocationList = allocationBllAction.GetAllocationList("acode='" + text + "'");
		string title = "调出提醒";
		string content = text + "调拨单已被" + WebUtil.GetUserNames(base.UserCode) + "调出";
		DataRow dataRow = allocationList.Rows[0];
		string userCode = dataRow["InAllocationPerson"].ToString();
		string dbTable = "Sm_Allocation";
		string dbColumn = "acode";
		string key = text;
		string uri = string.Concat(new string[]
		{
			"/StockManage/Allocation/AuditPage.aspx?ic=",
			dataRow["aid"].ToString(),
			"&acode=",
			text,
			"&allocationType=out"
		});
		Warning.AddWarning(title, content, userCode, dbTable, dbColumn, key, uri);
		this.RegisterStartupScript("alert1", "<script>alert('\\n系统提示：\\n\\n 调拨成功！');</script>");
		base.RegisterScript("location='ConfirmOutDepository.aspx'");
	}
    protected void btnConfirmWX_Click(object sender, EventArgs e)
    {
        AllocationBllAction allocationBllAction = new AllocationBllAction();
        string text = (this.HdnAcode.Value == "") ? this.HdnAcodeList.Value : this.HdnAcode.Value;
        int num = allocationBllAction.OutDepositoryConfirm(text, this.Session["yhdm"].ToString());
        if (num != 1)
        {
            this.RegisterStartupScript("alert", "<script>alert('\\n系统提示：\\n\\n库存不足 或者 没有权限！');</script>");
            return;
        }
        DataTable allocationList = allocationBllAction.GetAllocationList("acode='" + text + "'");
        string title = "调出提醒";
        string content = text + "调拨单已被" + WebUtil.GetUserNames(base.UserCode) + "调出";
        DataRow dataRow = allocationList.Rows[0];
        string userCode = dataRow["InAllocationPerson"].ToString();
        string dbTable = "Sm_Allocation";
        string dbColumn = "acode";
        string key = text;
        string uri = string.Concat(new string[]
        {
            "/StockManage/Allocation/AuditPage.aspx?ic=",
            dataRow["aid"].ToString(),
            "&acode=",
            text,
            "&allocationType=out"
        });
        Warning.AddWarning(title, content, userCode, dbTable, dbColumn, key, uri);
        RegisterStartupScript("alert1", "<script>alert('\\n系统提示：\\n\\n 调拨成功！');</script>");
        base.RegisterScript("location='ConfirmOutDepositoryWX.aspx'");
    }

    protected void GVAllocationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVAllocationList.PageIndex = e.NewPageIndex;
		this.Bind_GVAllocationList();
	}
}

