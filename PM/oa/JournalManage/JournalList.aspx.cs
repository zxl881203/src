﻿using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.SessionState;
using System.Web.UI.WebControls;

public partial class oa_JournalManage_JournalList : NBasePage, IRequiresSessionState
{
    private OAJournalService pcSer = new OAJournalService();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.btnDel.Attributes.Add("onclick", "if(!confirm('确定删除该工作日志吗?')) return false;");
            dropBind();//绑定下拉
            this.DataBinds();
        }
    }
    protected void GvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string value = this.GvList.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes["guid"] = (e.Row.Attributes["id"] = value);
        }
    }
    protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
    {
        dropBind();
        this.DataBinds();
    }

    private void dropBind()
    {
        DataTable aLLProvince = publicDbOpClass.DataTableQuary("select * from OA_Journal_Type where is_use = 1");
        this.type_id.DataSource = aLLProvince;
        this.type_id.DataValueField = "id";
        this.type_id.DataTextField = "name";
        this.type_id.DataBind();
        this.type_id.Items.Insert(0, new ListItem("请选择", ""));
    }

    protected void DataBinds()
    {
        IQueryable<OAJournal> source = this.Queryable();
        this.AspNetPager1.RecordCount = source.Count<OAJournal>();
        source =
            from p in source
            orderby p.start_time descending
            orderby p.create_date descending
            select p;
        IQueryable<OAJournal> dataSource = source.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
        this.GvList.DataSource = dataSource;
        this.GvList.DataBind();
    }
    protected string BackType(string type_id)
    {
        string sqlString = "select name from OA_Journal_Type where Id='" + type_id + "'";
        DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
        if (dataTable.Rows.Count > 0)
        {
            return dataTable.Rows[0]["name"].ToString();
        }
        return "";
    }
    protected string BackSYR(string ID)
    {
        DataTable dt = publicDbOpClass.DataTableQuary(@"select OA_Journal_Append.user_id,OA_Journal_Append.user_type,PT_yhmc.v_xm from OA_Journal_Append 
                                  left join PT_yhmc on OA_Journal_Append.user_id=PT_yhmc.v_yhdm where journal_id='" + ID + "'");
        string strSYRXM = "";
        foreach (DataRow dr in dt.Rows)
        {
            //0 审阅人、1相关人、2审阅及相关人;
            if (dr["user_type"].ToString() == "0")
            {
                strSYRXM = dr["v_xm"].ToString();
            }
            if (dr["user_type"].ToString() == "2")
            {
                strSYRXM = dr["v_xm"].ToString();
            }
        }
        //审阅人
        return strSYRXM;
      
    }
    protected string BackXGR(string ID)
    {
        DataTable dt = publicDbOpClass.DataTableQuary(@"select OA_Journal_Append.user_id,OA_Journal_Append.user_type,PT_yhmc.v_xm from OA_Journal_Append 
                                  left join PT_yhmc on OA_Journal_Append.user_id=PT_yhmc.v_yhdm where journal_id='" + ID + "'");
        string strXGRXM = "";
        foreach (DataRow dr in dt.Rows)
        {
            //0 审阅人、1相关人、2审阅及相关人;
           
            if (dr["user_type"].ToString() == "1")
            {
                strXGRXM += dr["v_xm"].ToString() + ",";
            }
            if (dr["user_type"].ToString() == "2")
            {
                strXGRXM += dr["v_xm"].ToString() + ",";
            }
        }
        //相关人
        if (strXGRXM.Length > 0)
        {
            return strXGRXM.Substring(0, strXGRXM.Length - 1);
        }else
        {
            return "";
        }

    }
    private IQueryable<OAJournal> Queryable()
    {
        System.DateTime? startTime = DateTimeHelper.GetDateTime(this.txtStartTime.Text);
        System.DateTime? endTime = DateTimeHelper.GetDateTime(this.txtEndTime.Text);
        //decimal? startCash = DecimalUtility.ConvertDecimal(this.txtStartCash.Text);
        //decimal? endCash = DecimalUtility.ConvertDecimal(this.txtEndCash.Text);
        //string matter = this.txtMatter.Text.Trim();
        IQueryable<OAJournal> queryable =
            from p in this.pcSer
                //where p.FlowState==-1
            select p;
        if (startTime.HasValue)
        {
            queryable =
                from p in queryable
                where p.start_time >= startTime.Value
                select p;
        }
        if (endTime.HasValue)
        {
            queryable =
                from p in queryable
                where p.start_time < endTime.Value.AddDays(1.0)
                select p;
        }
        //if (startCash.HasValue)
        //{
        //    queryable =
        //        from p in queryable
        //        where p.Cash >= startCash.Value
        //        select p;
        //}
        //if (endCash.HasValue)
        //{
        //    queryable =
        //        from p in queryable
        //        where p.Cash <= endCash.Value
        //        select p;
        //}
        //if (!string.IsNullOrWhiteSpace(matter))
        //{
        //    queryable =
        //        from p in queryable
        //        where p.Matter.Contains(matter)
        //        select p;
        //}
        if (!string.IsNullOrEmpty(txtTitle.Text))
        {
            queryable = 
                from p in queryable
                where p.title == txtTitle.Text
                select p;
        }
        if (type_id.SelectedValue!="")
        {
            queryable =
                from p in queryable
                where p.type_id == type_id.SelectedValue
                select p;
        }
        if (status.SelectedValue != "")
        {
            queryable =
                from p in queryable
                where p.status == Convert.ToInt32(status.SelectedValue)
                select p;
        }
        if (base.UserCode != "00000000")
        {
            queryable =
                from p in queryable
                where p.creater == UserCode
                select p;
        }
        return queryable;
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
        string value = this.KeyId.Value;
        string str = value.Replace('[', '(').Replace(']', ')').Replace('"', '\'');
        string strSqlA = "select * FROM OA_Journal where status !=0 and Id in " + str;
        DataTable dt = publicDbOpClass.DataTableQuary(strSqlA);
        if (dt.Rows.Count > 0)
        {
            base.RegisterShow("系统提示", "只能删除日志状态为草稿中的,请重新选择！");
            return;
        }
        string strSql = "DELETE FROM OA_Journal where Id in " + str;
        using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            try
            {
                SqlHelper.ExecuteNonQuery(sqlTransaction, CommandType.Text, strSql);
                sqlTransaction.Commit();
                base.RegisterShow("系统提示", "删除成功！");
                this.DataBinds();
            }
            catch
            {
                sqlTransaction.Rollback();
                base.RegisterScript("alert('系统提示：\\n\\删除失败！');");
            }
        }
    }

    protected void GvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GvList.PageIndex = e.NewPageIndex;
        this.DataBinds();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DataBinds();
    }
}