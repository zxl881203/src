﻿using ASP;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public partial class oa_System_zgsTreeView : Page, IRequiresSessionState
{

	public SystemInfoAction sia
	{
		get
		{
			return new SystemInfoAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.SystemInfoClass_Created();
		}
	}
	protected void SystemInfoClass_Created()
	{
		DataTable classID = this.sia.GetClassID("b.ClassTypeCode='003'");
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "子公司制度分类";
		treeNode.NavigateUrl = "";
		treeNode.Target = "rFrame";
		this.tv.Nodes.Add(treeNode);
		foreach (DataRow dataRow in classID.Rows)
		{
			TreeNode treeNode2 = new TreeNode();
			treeNode2.Text = dataRow["ClassName"].ToString();
			treeNode2.NavigateUrl = "zgsgl_right.aspx?cid=" + dataRow["ClassID"].ToString();
			treeNode2.Target = "rFrame";
			treeNode.Nodes.Add(treeNode2);
		}
	}
}

