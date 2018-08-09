﻿using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using cn.justwin.stockModel;
using com.jwsoft.pm.data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
public partial class OA3_FileMsg_MenuPowerList : NBasePage, IRequiresSessionState
{
    public PrivRoleService roleSer = new PrivRoleService();
    private PtYhmcBll ptYhmcBll = new PtYhmcBll();
    private PtdutyBll ptdutyBll = new PtdutyBll();
    protected string SubPrjGuid = "";
    private PTbdmBll ptdbmBll = new PTbdmBll();
    private PTDRoleBll pTDRoleBll = new PTDRoleBll();
    private string hid = string.Empty;
    protected override void OnInit(System.EventArgs e)
    {
        this.hid = base.Request["hid"];
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Cache.SetNoStore();
        if (!this.Page.IsPostBack)
        {
            this.BindTree();
            if(ddlType.SelectedValue == SmEnum.PermitType.Project.ToString())
            {
                this.ViewState["where"] = "";

            }
            else
            {
                this.ViewState["where"] = " where  i_bmdm=" + this.TvBranch.SelectedValue;
            }
            //this.ViewState["type"] = "0";
            //this.BindFile();
            this.BindMsg();
            this.BindGv();
        }
    }
    public void BindMsg()
    {
        this.lblUserList.Text = "";
        this.lblBranchList.Text = "";
        this.lblPostList.Text = "";
        this.lblRoleList.Text = "";
        this.lblProjectList.Text = "";

        this.hdUserList.Value = "";
        this.hdBranchList.Value = "";
        this.hdPostList.Value = "";
        this.hdBranchName.Value = "";
        this.hdRoleList.Value = "";
        this.hdProjectList.Value = "";
        string str = (hid == string.Empty) ? "0" : hid;
        IList<FilePermit> allFilePermitByWhere = FilePermitService.GetAllFilePermitByWhere(" where tcode = '" + str + "' ");
        foreach (FilePermit current in allFilePermitByWhere)
        {
            string sr = "";
            string sw = "";
            if (current.pread == "1")
            {
                sr = "checked";
            }
            else
            {
                sr = "";
            }
            if (current.pwrite == "1")
            {
                sw = "checked";
            }
            else
            {
                sw = "";
            }
            if (current.ptype == SmEnum.PermitType.Person.ToString() && current.tcode == hid)
            {
                PtYhmc modelById = this.ptYhmcBll.GetModelById(current.pcode);
                if (modelById != null && !this.hdUserList.Value.Contains(modelById.v_yhdm))
                {
                    HiddenField expr_137 = this.hdUserList;
                    expr_137.Value = expr_137.Value + modelById.v_yhdm + ",";
                    Label expr_158 = this.lblUserList;
                    string text = expr_158.Text;
                    expr_158.Text = string.Concat(new string[]
                    {// &nbsp;&nbsp;读取权限:<asp:CheckBox  name='read' runat='server' /> &nbsp;&nbsp;写入权限:<asp:CheckBox name='write' runat='server' />
                        text,
                        modelById.v_xm,
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+modelById.v_yhdm+"_UserRead' "+sr+">&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+modelById.v_yhdm+"_UserWrite' "+sw+"><span style=' cursor:pointer;' onclick=\"delUser('",modelById.v_yhdm,"','hdUserList','lblUserList')\">×</span>；</br>"
                    });
                }
            }
            if (current.ptype == SmEnum.PermitType.Department.ToString() && current.tcode == hid)
            {
                PTbdm pTbdmById = this.ptdbmBll.GetPTbdmById(Convert.ToInt32(current.pcode));
                if (pTbdmById != null && !this.hdBranchList.Value.Contains(Convert.ToString(pTbdmById.i_bmdm)))
                {
                    HiddenField expr_21C = this.hdBranchList;
                    expr_21C.Value = expr_21C.Value + pTbdmById.i_bmdm + ",";
                    Label expr_243 = this.lblBranchList;
                    object text2 = expr_243.Text;
                    expr_243.Text = string.Concat(new object[]
                    {
                        text2,
                        pTbdmById.V_BMMC,
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+pTbdmById.i_bmdm+"_BranchRead' "+sr+">&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+pTbdmById.i_bmdm+"_BranchWrite "+sw+"'><span style=' cursor:pointer;' onclick=\"delUser('",
                        pTbdmById.i_bmdm,
                        "','hdBranchList','lblBranchList')\">×</span>；</br>"
                    });
                    HiddenField expr_298 = this.hdBranchName;
                    object value = expr_298.Value;
                    expr_298.Value = string.Concat(new object[]
                    {
                        value,
                        pTbdmById.V_BMMC,
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+pTbdmById.i_bmdm+"_BranchRead' "+sr+">&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+pTbdmById.i_bmdm+"_BranchWrite' "+sw+"><span style=' cursor:pointer;' onclick=\"delUser('",
                        pTbdmById.i_bmdm,
                        "','hdBranchList','lblBranchList')\">×</span>；</br>"
                    });
                }
            }
            if (current.ptype == SmEnum.PermitType.Post.ToString() && current.tcode == hid)
            {
                Ptduty ptDutyById = this.ptdutyBll.GetPtDutyById(Convert.ToInt32(current.pcode));
                if (!this.hdPostList.Value.Contains(Convert.ToString(ptDutyById.I_DUTYID)))
                {
                    HiddenField expr_359 = this.hdPostList;
                    expr_359.Value = expr_359.Value + ptDutyById.I_DUTYID + ",";
                    Label expr_380 = this.lblPostList;
                    object text3 = expr_380.Text;
                    expr_380.Text = string.Concat(new object[]
                    {
                        text3,
                        ptDutyById.DutyName,
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+ptDutyById.I_DUTYID+"_PostRead' "+sr+">&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+ptDutyById.I_DUTYID+"_PostWrite' "+sw+"><span style=' cursor:pointer;' onclick=\"delUser('",
                        ptDutyById.I_DUTYID,
                        "','hdPostList','lblPostList')\">×</span>&nbsp;&nbsp;&nbsp;"
                    });
                }
            }
            if (current.ptype == SmEnum.PermitType.Role.ToString())// && current.tcode == hid
            {
                string strSql = "select Id,Name,No from Priv_Role where Id='" + current.pcode + "' order by No asc";
                DataTable dt = publicDbOpClass.DataTableQuary(strSql);
                if (dt.Rows.Count>0 && !this.hdRoleList.Value.Contains(dt.Rows[0]["Id"].ToString()))
                {
                    HiddenField expr_137 = this.hdRoleList;
                    expr_137.Value = expr_137.Value + dt.Rows[0]["Id"].ToString() + ",";
                    Label expr_158 = this.lblRoleList;
                    string text = expr_158.Text;
                    expr_158.Text = string.Concat(new string[]
                    {
                        text,
                       dt.Rows[0]["Name"].ToString(),
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+dt.Rows[0]["Id"].ToString()+"_RoleRead' "+sr+">&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+dt.Rows[0]["Id"].ToString()+"_RoleWrite' "+sw+"><span style=' cursor:pointer;' onclick=\"delUser('",
                        dt.Rows[0]["Id"].ToString(),
                        "','hdRoleList','lblRoleList')\">×</span>；</br>"
                    });
                }
            }
            if (current.ptype == SmEnum.PermitType.Project.ToString())//&& current.tcode == hid
            {
                PtYhmc modelById = this.ptYhmcBll.GetModelById(current.pcode);
                if (modelById != null && !this.hdProjectList.Value.Contains(modelById.v_yhdm))
                {
                    HiddenField expr_137 = this.hdProjectList;
                    expr_137.Value = expr_137.Value + modelById.v_yhdm + ",";
                    Label expr_158 = this.lblProjectList;
                    string text = expr_158.Text;
                    expr_158.Text = string.Concat(new string[]
                    {//
                        text,
                        modelById.v_xm,
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+modelById.v_yhdm+"_ProjectRead' "+sr+">&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+modelById.v_yhdm+"_ProjectWrite' "+sw+"><span style=' cursor:pointer;' onclick=\"delUser('",
                        modelById.v_yhdm,
                        "','hdProjectList','lblProjectList')\">×</span>；</br>"
                    });
                }
            }
        }
    }
    protected void SelectBranch(TreeNode Node)
    {
        string text = " where ptype='" + this.ddlType.SelectedValue + "'";
        if (hid != "")
        {
            text = text + " and tcode='" + hid + "'";
        }
        //if (this.ViewState["type"].ToString() == "0")
        //{
            IList<FilePermit> allFilePermitByWhere = FilePermitService.GetAllFilePermitByWhere(text);
            using (IEnumerator<FilePermit> enumerator = allFilePermitByWhere.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    FilePermit current = enumerator.Current;
                    if (current.pcode == Node.Value)
                    {
                        Node.Checked = true;
                    }
                }
                //return;
            }
       // }
        if (this.hdBranchList.Value.Contains(Node.Value))
        {
            Node.Checked = true;
        }
    }
    protected void BindGv()
    {
        this.lblBranchList.Text = this.hdBranchName.Value;
        //人员
        if (this.ddlType.SelectedValue == SmEnum.PermitType.Person.ToString())
        {
            this.tdTree.Visible = true;
            this.tdGv.Visible = true;
            this.gvPost.Visible = false;
            this.gvBranch.Visible = true;
            this.gvRole.Visible = false;
            this.gvProject.Visible = false;
            this.TvBranch.ShowCheckBoxes = TreeNodeTypes.None;
            this.gvBranch.DataSource = this.ptYhmcBll.GetAllModelByWhere(this.ViewState["where"].ToString() + " and state='1'");
            this.gvBranch.DataBind();
            IEnumerator enumerator = this.gvBranch.Rows.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
                    CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
                    if (this.hdUserList.Value.Contains(checkBox.ToolTip))
                    {
                        checkBox.Checked = true;
                    }
                }
                return;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
        //岗位
        if (this.ddlType.SelectedValue == SmEnum.PermitType.Post.ToString())
        {
            this.tdTree.Visible = true;
            this.tdGv.Visible = true;
            this.gvPost.Visible = true;
            this.gvBranch.Visible = false;
            this.gvRole.Visible = false;
            this.gvProject.Visible = false;
            this.TvBranch.ShowCheckBoxes = TreeNodeTypes.None;
            IList<Ptduty> allPtdutyByWhere = this.ptdutyBll.GetAllPtdutyByWhere(this.ViewState["where"].ToString());
            this.gvPost.DataSource = allPtdutyByWhere;
            this.gvPost.DataBind();
            IEnumerator enumerator2 = this.gvPost.Rows.GetEnumerator();
            try
            {
                while (enumerator2.MoveNext())
                {
                    GridViewRow gridViewRow2 = (GridViewRow)enumerator2.Current;
                    CheckBox checkBox2 = gridViewRow2.FindControl("cbPost") as CheckBox;
                    if (this.hdPostList.Value.Contains(checkBox2.ToolTip))
                    {
                        checkBox2.Checked = true;
                    }
                }
                return;
            }
            finally
            {
                IDisposable disposable2 = enumerator2 as IDisposable;
                if (disposable2 != null)
                {
                    disposable2.Dispose();
                }
            }
        }
        //部门
        if (this.ddlType.SelectedValue == SmEnum.PermitType.Department.ToString())
        {
            this.tdTree.Visible = true;
            this.TvBranch.ShowCheckBoxes = TreeNodeTypes.All;
            this.TvBranch.ExpandDepth = 1;
            this.gvPost.Visible = false;
            this.gvBranch.Visible = false;
            this.tdGv.Visible = false;
            this.gvRole.Visible = false;
            this.gvProject.Visible = false;
            return;
        }
        //角色
        if (this.ddlType.SelectedValue == SmEnum.PermitType.Role.ToString())
        {
            this.tdTree.Visible = false;
            this.tdGv.Visible = true;
            this.gvPost.Visible = false;
            this.gvBranch.Visible = false;
            this.gvRole.Visible = true;
            this.gvProject.Visible = false;
            string strSql = "select Id,Name,No from Priv_Role order by No asc";
            DataTable dt=publicDbOpClass.DataTableQuary(strSql);
            this.gvRole.DataSource = dt;
            this.gvRole.DataBind();
            IEnumerator enumerator = this.gvRole.Rows.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
                    CheckBox checkBox = gridViewRow.FindControl("cbRole") as CheckBox;
                    if (this.hdRoleList.Value.Contains(checkBox.ToolTip))
                    {
                        checkBox.Checked = true;
                    }
                }
                return;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
        //项目成员
        if (this.ddlType.SelectedValue == SmEnum.PermitType.Project.ToString())
        {
            this.tdTree.Visible = true;
            this.tdGv.Visible = true;
            this.gvPost.Visible = false;
            this.gvBranch.Visible = false;
            this.gvRole.Visible = false;
            this.gvProject.Visible = true;
            this.TvBranch.ShowCheckBoxes = TreeNodeTypes.None;
            string strID=this.ViewState["where"].ToString();
            string strSql = "";
            if (string.IsNullOrEmpty(strID) || strID == " where  i_bmdm=1")
            {
                strSql = "SELECT v_yhdm,v_xm from PT_yhmc where v_yhdm in(SELECT UserCode from PT_PrjInfo_ZTB_User) order by i_xh asc ";
            }
            else
            {
                strSql = "SELECT v_yhdm,v_xm from PT_yhmc where v_yhdm in(SELECT UserCode from PT_PrjInfo_ZTB_User where PrjGuid='" + strID + "') order by i_xh asc ";
            }
            DataTable dt = publicDbOpClass.DataTableQuary(strSql);
            this.gvProject.DataSource = dt; //this.ptYhmcBll.GetAllModelByWhere(this.ViewState["where"].ToString() + " and state='1'");
            this.gvProject.DataBind();
            IEnumerator enumerator = this.gvProject.Rows.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
                    CheckBox checkBox = gridViewRow.FindControl("cbProject") as CheckBox;
                    if (this.hdProjectList.Value.Contains(checkBox.ToolTip))
                    {
                        checkBox.Checked = true;
                    }
                }
                return;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
        this.gvPost.Visible = false;
        this.gvBranch.Visible = false;
        this.gvRole.Visible = false;
        this.gvProject.Visible = false;
        this.TvBranch.ShowCheckBoxes = TreeNodeTypes.None;
    }
    protected void BindTree()
    {
        this.TvBranch.Nodes.Clear();
        if (ddlType.SelectedValue == "Project")
        {
            string strSql = "select PrjGuid,PrjName from PT_PrjInfo where PrjState IN ( 5, 17, 7, 8, 9, 10, 11, 12, 13)";
            DataTable dt = publicDbOpClass.DataTableQuary(strSql);
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode treeNode = new TreeNode(dr["PrjName"].ToString(), dr["PrjGuid"].ToString());
                treeNode.ToolTip = string.Concat(dr["PrjGuid"].ToString());
                this.SelectBranch(treeNode);
                treeNode.Selected = false;
                //this.AddChildNodes(treeNode);
                this.TvBranch.Nodes.Add(treeNode);
            }
        }
        else
        {
            IList<PTbdm> pTbdmByWhere = this.ptdbmBll.GetPTbdmByWhere(" where i_sjdm=0 ");
            foreach (PTbdm current in pTbdmByWhere)
            {
                TreeNode treeNode = new TreeNode(current.V_BMMC, current.i_bmdm.ToString());
                treeNode.ToolTip = string.Concat(current.i_bmdm);
                this.SelectBranch(treeNode);
                treeNode.Selected = true;
                this.AddChildNodes(treeNode);
                this.TvBranch.Nodes.Add(treeNode);
            }
            TvBranch.SelectedNode.Expand();
        }
    }
    protected void AddChildNodes(TreeNode node)
    {
        IList<PTbdm> pTbdmByWhere = this.ptdbmBll.GetPTbdmByWhere("where i_sjdm='" + node.Value + "'");
        foreach (PTbdm current in pTbdmByWhere)
        {
            TreeNode treeNode = new TreeNode(current.V_BMMC, current.i_bmdm.ToString());
            treeNode.ToolTip = string.Concat(current.i_bmdm);
            this.SelectBranch(treeNode);
            node.ChildNodes.Add(treeNode);
            this.AddChildNodes(treeNode);
        }
    }
    protected void cbBox_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in this.gvBranch.Rows)
        {
            CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
            PtYhmc modelById = this.ptYhmcBll.GetModelById(checkBox.ToolTip);
            if (checkBox.Checked)
            {
                if (!this.hdUserList.Value.Contains(modelById.v_yhdm))
                {
                    HiddenField expr_71 = this.hdUserList;
                    expr_71.Value = expr_71.Value + modelById.v_yhdm + ",";
                    Label expr_92 = this.lblUserList;
                    string text = expr_92.Text;
                    expr_92.Text = string.Concat(new string[]
                    {
                        text,
                        modelById.v_xm,
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+modelById.v_yhdm+"_UserRead' checked>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+modelById.v_yhdm+"_UserWrite' checked><span style=' cursor:pointer;' onclick=\"delUser('",
                        modelById.v_yhdm,
                        "','hdUserList','lblUserList')\">×</span>；</br>"
                    });
                }
            }
            else
            {
                if (this.hdUserList.Value.Contains(modelById.v_yhdm))
                {
                    this.RemoveMsg(this.hdUserList, this.lblUserList, modelById.v_yhdm);
                }
            }
        }
    }
    protected void cbAllBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBox = this.gvBranch.HeaderRow.FindControl("cbAllBox") as CheckBox;
        if (checkBox.Checked)
        {
            IEnumerator enumerator = this.gvBranch.Rows.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
                    CheckBox checkBox2 = gridViewRow.FindControl("cbBox") as CheckBox;
                    PtYhmc modelById = this.ptYhmcBll.GetModelById(checkBox2.ToolTip);
                    checkBox2.Checked = true;
                    if (!this.hdUserList.Value.Contains(modelById.v_yhdm))
                    {
                        HiddenField expr_92 = this.hdUserList;
                        expr_92.Value = expr_92.Value + modelById.v_yhdm + ",";
                        Label expr_B3 = this.lblUserList;
                        string text = expr_B3.Text;
                        expr_B3.Text = string.Concat(new string[]
                        {
                            text,
                            modelById.v_xm,
                            "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+modelById.v_yhdm+"_UserRead' checked>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+modelById.v_yhdm+"_UserWrite' checked><span style=' cursor:pointer;' onclick=\"delUser('",
                            modelById.v_yhdm,
                            "','hdUserList','lblUserList')\">×</span>；</br>"
                        });
                    }
                }
                return;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
        foreach (GridViewRow gridViewRow2 in this.gvBranch.Rows)
        {
            CheckBox checkBox3 = gridViewRow2.FindControl("cbBox") as CheckBox;
            PtYhmc modelById2 = this.ptYhmcBll.GetModelById(checkBox3.ToolTip);
            checkBox3.Checked = false;
            if (this.hdUserList.Value.Contains(modelById2.v_yhdm))
            {
                this.RemoveMsg(this.hdUserList, this.lblUserList, modelById2.v_yhdm);
            }
        }
    }
    protected void cbAllProject_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBox = this.gvProject.HeaderRow.FindControl("cbAllBox") as CheckBox;
        if (checkBox.Checked)
        {
            IEnumerator enumerator = this.gvProject.Rows.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {

                    //this.tdTree.Visible = true;
                    //this.tdGv.Visible = true;
                    //this.gvPost.Visible = false;
                    //this.gvBranch.Visible = false;
                    //this.gvRole.Visible = false;
                    //this.gvProject.Visible = true;
                    //this.TvBranch.ShowCheckBoxes = TreeNodeTypes.None;
                    //string strID = this.ViewState["where"].ToString();
                    //string strSql = "";
                    //if (string.IsNullOrEmpty(strID) || strID == " where  i_bmdm=1")
                    //{
                    //    strSql = "SELECT v_yhdm,v_xm from PT_yhmc where v_yhdm in(SELECT UserCode from PT_PrjInfo_ZTB_User) order by i_xh asc ";
                    //}
                    //else
                    //{
                    //    strSql = "SELECT v_yhdm,v_xm from PT_yhmc where v_yhdm in(SELECT UserCode from PT_PrjInfo_ZTB_User where PrjGuid='" + strID + "') order by i_xh asc ";
                    //}
                    //DataTable dt = publicDbOpClass.DataTableQuary(strSql);
                    //this.gvProject.DataSource = dt; //this.ptYhmcBll.GetAllModelByWhere(this.ViewState["where"].ToString() + " and state='1'");
                    //this.gvProject.DataBind();
                    //IEnumerator enumerator = this.gvProject.Rows.GetEnumerator();
                    //try
                    //{
                    //    while (enumerator.MoveNext())
                    //    {
                    //        GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
                    //        CheckBox checkBox = gridViewRow.FindControl("cbProject") as CheckBox;
                    //        if (this.hdProjectList.Value.Contains(checkBox.ToolTip))
                    //        {
                    //            checkBox.Checked = true;
                    //        }
                    //    }
                    //    return;
                    //}
                    //finally
                    //{
                    //    IDisposable disposable = enumerator as IDisposable;
                    //    if (disposable != null)
                    //    {
                    //        disposable.Dispose();
                    //    }
                    //}

                    GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
                    CheckBox checkBox2 = gridViewRow.FindControl("cbProject") as CheckBox;
                    PtYhmc modelById = this.ptYhmcBll.GetModelById(checkBox2.ToolTip);
                    //checkBox2.Checked = true;
                    //if (!this.hdProjectList.Value.Contains(modelById.v_yhdm))
                    //{
                    //    HiddenField expr_92 = this.hdProjectList;
                    //    expr_92.Value = expr_92.Value + modelById.v_yhdm + ",";
                    //    Label expr_B3 = this.lblUserList;
                    //    string text = expr_B3.Text;
                    //    expr_B3.Text = string.Concat(new string[]
                    //    {
                    //        text,
                    //        modelById.v_xm,
                    //        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+modelById.v_yhdm+"_UserRead' checked>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+modelById.v_yhdm+"_UserWrite' checked><span style=' cursor:pointer;' onclick=\"delUser('",
                    //        modelById.v_yhdm,
                    //        "','hdUserList','lblUserList')\">×</span>；</br>"
                    //    });
                    //}

                    //PtYhmc modelById = this.ptYhmcBll.GetModelById(checkBox2.ToolTip);
                    if (modelById != null && !this.hdProjectList.Value.Contains(modelById.v_yhdm))
                    {
                        HiddenField expr_137 = this.hdProjectList;
                        expr_137.Value = expr_137.Value + modelById.v_yhdm + ",";
                        Label expr_158 = this.lblProjectList;
                        string text = expr_158.Text;
                        expr_158.Text = string.Concat(new string[]
                        {//
                        text,
                        modelById.v_xm,
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+modelById.v_yhdm+"_ProjectRead' checked>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+modelById.v_yhdm+"_ProjectWrite' checked><span style=' cursor:pointer;' onclick=\"delUser('",
                        modelById.v_yhdm,
                        "','hdProjectList','lblProjectList')\">×</span>；</br>"
                        });
                    }

                }
                return;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
        foreach (GridViewRow gridViewRow2 in this.gvBranch.Rows)
        {
            CheckBox checkBox3 = gridViewRow2.FindControl("cbBox") as CheckBox;
            PtYhmc modelById2 = this.ptYhmcBll.GetModelById(checkBox3.ToolTip);
            checkBox3.Checked = false;
            if (this.hdUserList.Value.Contains(modelById2.v_yhdm))
            {
                this.RemoveMsg(this.hdUserList, this.lblUserList, modelById2.v_yhdm);
            }
        }
    }
    public void RemoveMsg(HiddenField hdList, Label lbList, string yhdm)
    {
        string[] array = hdList.Value.Split(new char[]
        {
            ','
        });
        //string[] array2 = lbList.Text.Replace(">,", "|").Split(new char[]
        //{
        //    '|'
        //});
        //string[] array2 = lbList.Text.Split('；</br>');
        string[] array2 = lbList.Text.Split(new char[]
        {
            '；'
        });
        string[] array3 = array;
        for (int i = 0; i < array3.Length; i++)
        {
            string text = array3[i];
            if (text == yhdm)
            {
                hdList.Value = hdList.Value.Replace(yhdm + ",", "");
                string[] array4 = array2;
                for (int j = 0; j < array4.Length; j++)
                {
                    string text2 = array4[j];
                    if (text2.Contains(text))
                    {
                        //lbList.Text = lbList.Text.Replace(text2 + ">,", "");
                        lbList.Text = lbList.Text.Replace(text2 + "；</br>", "");
                    }
                }
            }
        }
        this.BindGv();
    }
    public void RemoveBranchMsg(HiddenField hdList, string lbList, string yhdm)
    {
        string[] array = hdList.Value.Split(new char[]
        {
            ','
        });
        //string[] array2 = this.hdBranchName.Value.Replace(">,", "|").Split(new char[]
        //{
        //    '|'
        //});
        string[] array2 = lbList.Split(new char[]
        {
            '；'
        });
        string[] array3 = array;
        for (int i = 0; i < array3.Length; i++)
        {
            string text = array3[i];
            if (text == yhdm)
            {
                hdList.Value = hdList.Value.Replace(yhdm + ",", "");
                string[] array4 = array2;
                for (int j = 0; j < array4.Length; j++)
                {
                    string text2 = array4[j];
                    if (text2.Contains(text))
                    {
                        lblBranchList.Text = hdBranchName.Value.Replace(text2 + "；</br>", "");
                        this.hdBranchName.Value = hdBranchName.Value.Replace(text2 + "；</br>", "");
                        //lbList.Text = this.hdBranchName.Value.Replace(text2 + ">,", "");
                        //this.hdBranchName.Value = this.hdBranchName.Value.Replace(text2 + ">,", "");
                    }
                }
            }
        }
        //this.ViewState["type"] = "1";
        this.BindTree();
    }
    protected void cbBox_CheckedChanged2(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in this.gvPost.Rows)
        {
            CheckBox checkBox = gridViewRow.FindControl("cbPost") as CheckBox;
            Ptduty ptDutyById = this.ptdutyBll.GetPtDutyById(Convert.ToInt32(checkBox.ToolTip));
            if (checkBox.Checked)
            {
                if (!this.hdPostList.Value.Contains(Convert.ToString(ptDutyById.I_DUTYID)))
                {
                    HiddenField expr_7B = this.hdPostList;
                    expr_7B.Value = expr_7B.Value + ptDutyById.I_DUTYID + ",";
                    Label expr_A1 = this.lblPostList;
                    object text = expr_A1.Text;
                    expr_A1.Text = string.Concat(new object[]
                    {
                        text,
                        ptDutyById.DutyName,
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+ptDutyById.I_DUTYID+"_PostRead' checked>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+ptDutyById.I_DUTYID+"_PostWrite' checked><span style=' cursor:pointer;' onclick=\"delUser('",
                        ptDutyById.I_DUTYID,
                        "','hdPostList','lblPostList')\">×</span>；</br>"
                    });
                }
            }
            else
            {
                if (this.hdPostList.Value.Contains(Convert.ToString(ptDutyById.I_DUTYID)))
                {
                    this.RemoveMsg(this.hdPostList, this.lblPostList, ptDutyById.I_DUTYID.ToString());
                }
            }
        }
    }
    protected void cbBox_CheckedChanged3(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in this.gvRole.Rows)
        {
            CheckBox checkBox = gridViewRow.FindControl("cbRole") as CheckBox;
            string strSql = "select Id,Name,No from Priv_Role where Id='"+ checkBox.ToolTip + "' order by No asc";
            DataTable dt = publicDbOpClass.DataTableQuary(strSql);
            if (checkBox.Checked)
            {
                if (!this.hdRoleList.Value.Contains(Convert.ToString(dt.Rows[0]["Id"].ToString())))
                {
                    HiddenField expr_7B = this.hdRoleList;
                    expr_7B.Value = expr_7B.Value + dt.Rows[0]["Id"].ToString() + ",";
                    Label expr_A1 = this.lblRoleList;
                    object text = expr_A1.Text;
                    expr_A1.Text = string.Concat(new object[]
                    {
                        text,
                        dt.Rows[0]["Name"].ToString(),
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+dt.Rows[0]["Id"].ToString()+"_RoleRead' checked>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+dt.Rows[0]["Id"].ToString()+"_RoleWrite' checked><span style=' cursor:pointer;' onclick=\"delUser('",
                       dt.Rows[0]["Id"].ToString(),
                        "','hdRoleList','lblRoleList')\">×</span>；</br>"
                    });
                }
            }
            else
            {
                if (this.hdRoleList.Value.Contains(Convert.ToString(dt.Rows[0]["Id"].ToString())))
                {
                    this.RemoveMsg(this.hdRoleList, this.lblRoleList, dt.Rows[0]["Id"].ToString());
                }
            }
        }
    }
    protected void cbBox_CheckedChanged4(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in this.gvProject.Rows)
        {
            CheckBox checkBox = gridViewRow.FindControl("cbProject") as CheckBox;
            PtYhmc modelById = this.ptYhmcBll.GetModelById(checkBox.ToolTip);
            if (checkBox.Checked)
            {
                if (!this.hdProjectList.Value.Contains(modelById.v_yhdm))
                {
                    HiddenField expr_71 = this.hdProjectList;
                    expr_71.Value = expr_71.Value + modelById.v_yhdm + ",";
                    Label expr_92 = this.lblProjectList;
                    string text = expr_92.Text;
                    expr_92.Text = string.Concat(new string[]
                    {
                        text,
                        modelById.v_xm,
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+modelById.v_yhdm+"_ProjectRead' checked>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+modelById.v_yhdm+"_ProjectWrite' checked><span style=' cursor:pointer;' onclick=\"delUser('",
                        modelById.v_yhdm,
                        "','hdProjectList','lblProjectList')\">×</span>；</br>"
                    });
                }
            }
            else
            {
                if (this.hdProjectList.Value.Contains(modelById.v_yhdm))
                {
                    this.RemoveMsg(this.hdProjectList, this.lblProjectList, modelById.v_yhdm);
                }
            }
        }
    }
    protected void cbAllBoxPost_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBox = this.gvPost.HeaderRow.FindControl("cbAllBoxPost") as CheckBox;
        if (checkBox.Checked)
        {
            IEnumerator enumerator = this.gvPost.Rows.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
                    CheckBox checkBox2 = gridViewRow.FindControl("cbPost") as CheckBox;
                    Ptduty ptDutyById = this.ptdutyBll.GetPtDutyById(Convert.ToInt32(checkBox2.ToolTip));
                    checkBox2.Checked = true;
                    if (!this.hdPostList.Value.Contains(Convert.ToString(ptDutyById.I_DUTYID)))
                    {
                        HiddenField expr_9C = this.hdPostList;
                        expr_9C.Value = expr_9C.Value + ptDutyById.I_DUTYID + ",";
                        Label expr_C2 = this.lblPostList;
                        object text = expr_C2.Text;
                        expr_C2.Text = string.Concat(new object[]
                        {
                            text,
                            ptDutyById.DutyName,
                            "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+ ptDutyById.I_DUTYID+"_PostRead' checked>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+ ptDutyById.I_DUTYID+"_PostWrite' checked><span style=' cursor:pointer;' onclick=\"delUser('",
                            ptDutyById.I_DUTYID,
                            "','hdPostList','lblPostList')\">×</span>；</br>"
                        });
                    }
                }
                return;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
        foreach (GridViewRow gridViewRow2 in this.gvPost.Rows)
        {
            CheckBox checkBox3 = gridViewRow2.FindControl("cbPost") as CheckBox;
            Ptduty ptDutyById2 = this.ptdutyBll.GetPtDutyById(Convert.ToInt32(checkBox3.ToolTip));
            checkBox3.Checked = false;
            if (this.hdPostList.Value.Contains(Convert.ToString(ptDutyById2.I_DUTYID)))
            {
                this.RemoveMsg(this.hdPostList, this.lblPostList, ptDutyById2.I_DUTYID.ToString());
            }
        }
    }
    protected void cbAllBoxRole_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in this.gvRole.Rows)
        {
            CheckBox checkBox = gridViewRow.FindControl("cbRole") as CheckBox;
            string strSql = "select Id,Name,No from Priv_Role where Id='" + checkBox.ToolTip + "' order by No asc";
            DataTable dt = publicDbOpClass.DataTableQuary(strSql);
            if (checkBox.Checked)
            {
                if (!this.hdRoleList.Value.Contains(Convert.ToString(dt.Rows[0]["Id"].ToString())))
                {
                    HiddenField expr_7B = this.hdRoleList;
                    expr_7B.Value = expr_7B.Value + dt.Rows[0]["Id"].ToString() + ",";
                    Label expr_A1 = this.lblRoleList;
                    object text = expr_A1.Text;
                    expr_A1.Text = string.Concat(new object[]
                    {
                        text,
                        dt.Rows[0]["Name"].ToString(),
                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+dt.Rows[0]["Id"].ToString()+"_RoleRead' checked>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+dt.Rows[0]["Id"].ToString()+"_RoleWrite' checked><span style=' cursor:pointer;' onclick=\"delUser('",
                       dt.Rows[0]["Id"].ToString(),
                        "','hdRoleList','lblRoleList')\">×</span>；</br>"
                    });
                }
            }
            else
            {
                if (this.hdRoleList.Value.Contains(Convert.ToString(dt.Rows[0]["Id"].ToString())))
                {
                    this.RemoveMsg(this.hdRoleList, this.lblRoleList, dt.Rows[0]["Id"].ToString());
                }
            }
        }
        //foreach (GridViewRow gridViewRow in this.gvRole.Rows)
        //{
        //    CheckBox checkBox = gridViewRow.FindControl("cbRole") as CheckBox;
        //    //PtYhmc modelById = this.ptYhmcBll.GetModelById(checkBox.ToolTip);
        //    if (checkBox.Checked)
        //    {
        //        string strSql = "select Id,Name,No from Priv_Role where Id='" + checkBox.ToolTip + "' order by No asc";
        //        DataTable dt = publicDbOpClass.DataTableQuary(strSql);
        //        if (dt.Rows.Count > 0 && !this.hdRoleList.Value.Contains(dt.Rows[0]["Id"].ToString()))
        //        {
        //            HiddenField expr_137 = this.hdRoleList;
        //            expr_137.Value = expr_137.Value + dt.Rows[0]["Id"].ToString() + ",";
        //            Label expr_158 = this.lblRoleList;
        //            string text = expr_158.Text;
        //            expr_158.Text = string.Concat(new string[]
        //                            {
        //                        text,
        //                       dt.Rows[0]["Name"].ToString(),
        //                        "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+dt.Rows[0]["Id"].ToString()+"_RoleRead' checkd>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+dt.Rows[0]["Id"].ToString()+"_RoleWrite' checkd><span style=' cursor:pointer;' onclick=\"delUser('",
        //                        dt.Rows[0]["Id"].ToString(),
        //                        "','hdRoleList','lblRoleList')\">×</span>；</br>"
        //                            });
        //        }
        //        else if (this.hdRoleList.Value.Contains(dt.Rows[0]["Id"].ToString()))
        //            {
        //                this.RemoveMsg(this.hdRoleList, this.lblRoleList, dt.Rows[0]["Id"].ToString());
        //            }

        //    else
        //    {

        //    }
   // }
        //CheckBox checkBox = this.gvPost.HeaderRow.FindControl("cbAllBoxRole") as CheckBox;
        //if (checkBox.Checked)
        //{
        //    IEnumerator enumerator = this.gvPost.Rows.GetEnumerator();
        //    try
        //    {
        //        while (enumerator.MoveNext())
        //        {
        //            GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
        //            CheckBox checkBox2 = gridViewRow.FindControl("cbRole") as CheckBox;
        //            Ptduty ptDutyById = this.ptdutyBll.GetPtDutyById(Convert.ToInt32(checkBox2.ToolTip));
        //            checkBox2.Checked = true;
        //            if (!this.hdPostList.Value.Contains(Convert.ToString(ptDutyById.I_DUTYID)))
        //            {
        //                HiddenField expr_9C = this.hdPostList;
        //                expr_9C.Value = expr_9C.Value + ptDutyById.I_DUTYID + ",";
        //                Label expr_C2 = this.lblPostList;
        //                object text = expr_C2.Text;
        //                expr_C2.Text = string.Concat(new object[]
        //                {
        //                    text,
        //                    ptDutyById.DutyName,
        //                    "&nbsp;&nbsp;读取权限:<input type='checkbox' name='"+ ptDutyById.I_DUTYID+"_PostRead' checked>&nbsp;&nbsp;写入权限:<input type='checkbox' name='"+ ptDutyById.I_DUTYID+"_PostWrite' checked><span style=' cursor:pointer;' onclick=\"delUser('",
        //                    ptDutyById.I_DUTYID,
        //                    "','hdRoleList','lblRoleList')\">×</span>；</br>"
        //                });
        //            }
        //        }
        //        return;
        //    }
        //    finally
        //    {
        //        IDisposable disposable = enumerator as IDisposable;
        //        if (disposable != null)
        //        {
        //            disposable.Dispose();
        //        }
        //    }
        //}
        //foreach (GridViewRow gridViewRow2 in this.gvPost.Rows)
        //{
        //    CheckBox checkBox3 = gridViewRow2.FindControl("cbPost") as CheckBox;
        //    Ptduty ptDutyById2 = this.ptdutyBll.GetPtDutyById(Convert.ToInt32(checkBox3.ToolTip));
        //    checkBox3.Checked = false;
        //    if (this.hdPostList.Value.Contains(Convert.ToString(ptDutyById2.I_DUTYID)))
        //    {
        //        this.RemoveMsg(this.hdPostList, this.lblPostList, ptDutyById2.I_DUTYID.ToString());
        //    }
        //}
    }
    protected void TvBranch_SelectedNodeChanged(object sender, EventArgs e)
    {
        
        if (this.ddlType.SelectedValue == Convert.ToString(SmEnum.PermitType.Project))
        {
            this.ViewState["where"] = this.TvBranch.SelectedValue;
            this.BindGv();
        }else
        {
            if (this.ddlType.SelectedValue != Convert.ToString(SmEnum.PermitType.Department))
            {
                this.ViewState["where"] = " where i_bmdm=" + this.TvBranch.SelectedValue;
                this.BindGv();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strIDs = tbRW.Text.ToString();
       // 
        if (hid == "")
        {
            base.RegisterScript("top.ui.alert('请选择目录!');");
            return;
        }
        FilePermitService.DelFilePermitByTcode(hid);
        string[] uList = this.hdBranchList.Value.Split(new char[]
        {
            ','
        });
        bool flag = this.AddPermit(uList, SmEnum.PermitType.Department.ToString(), strIDs);

        string[] uList2 = this.hdUserList.Value.Split(new char[]
        {
            ','
        });
        bool flag2 = this.AddPermit(uList2, SmEnum.PermitType.Person.ToString(), strIDs);

        string[] uList3 = this.hdPostList.Value.Split(new char[]
        {
            ','
        });
        bool flag3 = this.AddPermit(uList3, SmEnum.PermitType.Post.ToString(), strIDs);

        string[] uList4 = this.hdRoleList.Value.Split(new char[]
        {
            ','
        });
        bool flag4 = this.AddPermit(uList4, SmEnum.PermitType.Role.ToString(), strIDs);

        string[] uList5 = this.hdProjectList.Value.Split(new char[]
        {
            ','
        });
        bool flag5 = this.AddPermit(uList5, SmEnum.PermitType.Project.ToString(), strIDs);

        if (flag && flag2 && flag3 && flag4 && flag5)
        {
            base.RegisterScript("top.ui.alert('设置成功！');location='MenuPowerList.aspx?hid=" + hid + "'");
            //base.RegisterScript("top.ui.alert('设置成功！');");//
            //string str = (hid == string.Empty) ? "0" : hid;
            //this.BindMsg();
        }
    }
    public bool AddPermit(string[] uList, string type, string strIDs)
    {
       

        bool result = true;
        for (int i = 0; i < uList.Length; i++)
        {
            string strR = "0";
            string strW = "0";
            string text = uList[i];
            if (strIDs.Length > 0)
            {
                string[] strs = strIDs.Substring(0, strIDs.Length-1).Split('|');
                foreach (string str in strs)
                {
                    string[] ss = str.Split('_');
                    if (type == SmEnum.PermitType.Department.ToString())
                    {
                        if (ss[0].ToString() == text && ss[1].ToString() == "BranchRead")
                        {
                            strR = "1";
                        }
                        else if (ss[0].ToString() == text && ss[1].ToString() == "BranchWrite")
                        {
                            strW = "1";
                        }
                    }
                    if (type == SmEnum.PermitType.Person.ToString())
                    {
                        if (ss[0].ToString() == text && ss[1].ToString() == "UserRead")
                        {
                            strR = "1";
                        }
                        else if (ss[0].ToString() == text && ss[1].ToString() == "UserWrite")
                        {
                            strW = "1";
                        }
                    }
                    if (type == SmEnum.PermitType.Post.ToString())
                    {
                        if (ss[0].ToString() == text && ss[1].ToString() == "PostRead")
                        {
                            strR = "1";
                        }
                        else if (ss[0].ToString() == text && ss[1].ToString() == "PostWrite")
                        {
                            strW = "1";
                        }
                    }
                    if (type == SmEnum.PermitType.Project.ToString())
                    {
                        if (ss[0].ToString() == text && ss[1].ToString() == "ProjectRead")
                        {
                            strR = "1";
                        }
                        else if (ss[0].ToString() == text && ss[1].ToString() == "ProjectWrite")
                        {
                            strW = "1";
                        }
                    }
                    if (type == SmEnum.PermitType.Role.ToString())
                    {
                        if (ss[0].ToString() == text && ss[1].ToString() == "RoleRead")
                        {
                            strR = "1";
                        }
                        else if (ss[0].ToString() == text && ss[1].ToString() == "RoleWrite")
                        {
                            strW = "1";
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(text))
            {
                FilePermit filePermit = new FilePermit();
                filePermit.tpid = Guid.NewGuid().ToString();
                filePermit.tcode = hid;
                filePermit.ptype = type;
                filePermit.pcode = text;
                filePermit.pread = strR;
                filePermit.pwrite = strW;
                if (FilePermitService.AddFilePermit(filePermit) == 0)
                {
                    result = false;
                }
            }
        }
        return result;
    }
    //public void BindFile()
    //{
    //    System.Collections.Generic.List<FileModel> listArray = FileService.GetListArray(" where ParentId = Id and FileType!=0  and IsValid=0  order by CreateTime desc");
    //    foreach (FileModel current in listArray)
    //    {
    //        TreeNode treeNode = new TreeNode();
    //        treeNode.Value = current.Id;
    //        treeNode.Text = current.FileName;
    //        if (listArray[0] == current)
    //        {
    //            treeNode.Select();
    //        }
    //        if (base.Request.QueryString["id"] != null && base.Request.QueryString["id"] == current.Id)
    //        {
    //            treeNode.Select();
    //        }
    //        treeNode.ImageUrl = "/images/tree/FileInfo/folder.gif";
    //        this.AddNode(treeNode);
    //        this.tvFile.Nodes.Add(treeNode);
    //    }
    //    tvFile.SelectedNode.Expand();
    //}
    //public TreeNode AddNode(TreeNode node)
    //{
    //    System.Collections.Generic.List<FileModel> listArray = FileService.GetListArray(" where ParentId='" + node.Value + "' and id != parentId and FileType=2  and IsValid='false'  order by CreateTime desc");
    //    foreach (FileModel current in listArray)
    //    {
    //        TreeNode treeNode = new TreeNode();
    //        treeNode.ImageUrl = this.imageUrl(current.Id);
    //        treeNode.Value = current.Id;
    //        treeNode.Text = current.FileName;
    //        node.ChildNodes.Add(treeNode);
    //        if (base.Request.QueryString["id"] != null && base.Request.QueryString["id"] == current.Id)
    //        {
    //            treeNode.Select();
    //            this.ExpandSelectNode(treeNode);
    //        }
    //        this.AddNode(treeNode);
    //    }
    //    return node;
    //}
    //protected void ExpandSelectNode(TreeNode node)
    //{
    //    if (node != null)
    //    {
    //        TreeNode parent = node.Parent;
    //        if (node.Depth == 1 || parent == null)
    //        {
    //            node.ExpandAll();
    //            return;
    //        }
    //        if (parent.Depth == 1)
    //        {
    //            parent.ExpandAll();
    //            return;
    //        }
    //        this.ExpandSelectNode(parent);
    //    }
    //}
    //public string imageUrl(string id)
    //{
    //    string result = "/images/tree/FileInfo/folderSubtract.png";
    //    if (FileService.GetListArray(" where parentId='" + id + "' AND FileType=2 AND IsValid=0").Count > 0)
    //    {
    //        result = "/images/tree/FileInfo/folderAdd.png";
    //    }
    //    return result;
    //}
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindGv();
        //this.ViewState["type"] = "1";
        this.BindTree();
    }
    public string GetDutyName(string id)
    {
        PTDRole model = this.pTDRoleBll.GetModel(id);
        return model.RoleTypeName;
    }
    protected void tvFile_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.BindTree();
        this.BindMsg();
        this.BindGv();
    }
    protected void btnRe_Click(object sender, EventArgs e)
    {
        if (this.hdType.Value == "hdUserList")
        {
            this.RemoveMsg(this.hdUserList, this.lblUserList, this.hdDelId.Value);
        }
        if (this.hdType.Value == "hdPostList")
        {
            this.RemoveMsg(this.hdPostList, this.lblPostList, this.hdDelId.Value);
        }
        if (this.hdType.Value == "hdBranchList")
        {//hdBranchList
            this.RemoveBranchMsg(this.hdBranchList, this.lblBranchList2.Text, this.hdDelId.Value);
        }
        if (this.hdType.Value == "hdRoleList")
        {
            this.RemoveMsg(this.hdRoleList, this.lblRoleList, this.hdDelId.Value);
        }
        if (this.hdType.Value == "hdProjectList")
        {
            this.RemoveMsg(this.hdProjectList, this.lblProjectList, this.hdDelId.Value);
        }
    }
}


