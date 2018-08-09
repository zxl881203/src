﻿using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_UserControl_MultiSelectTask : NBasePage, IRequiresSessionState
{
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private ISerializable ser = new JsonSerializer();
	private BudTaskService budTaskSer = new BudTaskService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.DataBindResource();
		}
	}
	protected void gvwResource_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			string value2 = this.gvBudget.DataKeys[e.Row.RowIndex]["SubCount"].ToString();
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			string value3 = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskName"].ToString();
			int num = text.Length / 3;
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["orderNumber"] = text;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = value2;
			e.Row.Attributes["taskName"] = value3;
		}
	}
	private void DataBindResource()
	{
		string text = base.Request["prjId"];
		if (!string.IsNullOrEmpty(text))
		{
			BudTask.GetTaskInfo(text, this.hfldIsWBSRelevance.Value, string.Empty, string.Empty, string.Empty);
			DataTable table = this.budTaskSer.GetTable(text);
			this.gvBudget.DataSource = table;
			this.gvBudget.DataBind();
		}
	}
}


