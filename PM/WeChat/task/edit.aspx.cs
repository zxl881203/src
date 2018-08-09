﻿using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.data;
using Newtonsoft.Json.Linq;
using PMBase.Basic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WeChat_task_edit : Page
{
    public string code = string.Empty;
    private string action = string.Empty;
    private string Id = string.Empty; private string userID = string.Empty;
    protected override void OnInit(System.EventArgs e)
    {
        this.code = base.Request["code"];
        this.action = base.Request["action"];
        this.Id = base.Request["id"];
        this.userID = base.Request["userID"];//用户ID
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            if (!string.IsNullOrEmpty(userID))
            {
                UserID.Value = userID;//WXAPI.getUserIdByCode(code); 
            }
            else
            {
                UserID.Value = WXAPI.getUserIdByCode(code, "1000010"); //"00200002";// WXAPI.getUserIdByCode(code); //"00200002";//
                RegisterScript(UserID.Value);
            }
            this.BindType(); //绑定任务类型
            this.BindPriority();//绑定任务优先级

            if (this.action == "add")
            {
                this.KeyId.Value = System.Guid.NewGuid().ToString();
            }
            else if (this.action == "edit")
            {
                this.KeyId.Value = Id;
                this.Bind(Id, action);
            }
        }
        else
        {
            string strSubType = submitType.Value.ToString();
            if (!string.IsNullOrEmpty(strSubType))
            {
                if (strSubType == "0")
                {
                    saveData(0);
                }
                if (strSubType == "1")
                {
                    if (Convert.ToDateTime(start_time.Value.ToString()) >= DateTime.Now)
                    {
                        saveData(1);
                    }
                    else
                    {
                        saveData(2);
                    }
                }
            }
        }
    }
    public void BindType()
    {
        DataTable aLLProvince = publicDbOpClass.DataTableQuary("select * from OA_Task_Type where is_use = 1");
        this.type_id.DataSource = aLLProvince;
        this.type_id.DataValueField = "id";
        this.type_id.DataTextField = "name";
        this.type_id.DataBind();
        this.type_id.Items.Insert(0, new ListItem("请选择", ""));
    }
    private void BindPriority()
    {
        DataTable dt = publicDbOpClass.DataTableQuary("select * from XPM_Basic_CodeList WHERE SignCode2='Task_Priority' order by I_xh asc");
        this.priority_id.DataSource = dt;
        this.priority_id.DataValueField = "NoteID";
        this.priority_id.DataTextField = "CodeName";
        this.priority_id.DataBind();
        this.priority_id.Items.Insert(0, new ListItem("请选择", ""));

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["IsDefault"].ToString() == "1")
            {
                priority_id.SelectedValue = dr["NoteID"].ToString();
                break;
            }
        }
    }
    private void Bind(string Id, string action)
    {
        if (!string.IsNullOrEmpty(Id))
        {
            string strWhere = " and OA_Task.id='" + Id + "'";
            DataTable dt = TaskService.GetTaskListTable(strWhere, UserID.Value);
            if (dt.Rows.Count > 0)
            {
                this.type_id.SelectedValue = dt.Rows[0]["type_id"].ToString();//任务类型
                this.priority_id.SelectedValue = dt.Rows[0]["priority_id"].ToString();//任务优先级
                this.if_send.SelectedValue = dt.Rows[0]["if_send"].ToString();//是否发送微信通知
                this.KeyId.Value = dt.Rows[0]["id"].ToString();//主键
                this.title.Value = dt.Rows[0]["title"].ToString();//标题
                this.content.Value = dt.Rows[0]["content"].ToString();//内容
                this.start_time.Value = Convert.ToDateTime(dt.Rows[0]["start_time"].ToString()).ToString("yyyy-MM-dd HH:mm");//开始时间
                this.end_time.Value = Convert.ToDateTime(dt.Rows[0]["end_time"].ToString()).ToString("yyyy-MM-dd HH:mm");//结束时间
                TimeSpan timeSpan = Convert.ToDateTime(dt.Rows[0]["end_time"].ToString()) - Convert.ToDateTime(dt.Rows[0]["start_time"].ToString());
                this.cxsj.Value = timeSpan.TotalMinutes.ToString();///持续时间（分钟）
            }
        }
    }
    /// <summary>
    /// 保存页面数据
    /// </summary>
    /// <param name="status">0草稿、1未开始、2执行中、3已完成、4已关闭、5已删除  </param>
    public void saveData(int status)
    {
        string strSql = "";
        string strSql1 = "";
        if (this.action == "add")
        {
            strSql = string.Format(@"
            INSERT INTO [OA_Task]
           ([id]
           ,[type_id]
           ,[title]
           ,[content]
           ,[creater_id]
           ,[create_time]
           ,[status]
           ,[start_time]
           ,[end_time]
           --,[complete_time]
           ,[priority_id]
           ,if_send
)
     VALUES
           ('{0}'--<id, varchar(50),>
           ,'{1}'--<type_id, varchar(50),>
           ,'{2}'--<title, nvarchar(50),>
           ,'{3}'--<content, text,>
           ,'{4}'--<creater_id, varchar(50),>
           ,'{5}'--<create_time, datetime,>
           ,{6}--<status, int,>
           ,'{7}'--<start_time, datetime,>
           ,'{8}'--<end_time, datetime,>
          --,[complete_time] = '{9}'--<complete_time, datetime,>
           ,'{9}'--<priority_id, varchar(50),>
            ,{10}--<if_send, int,>
		   )", this.KeyId.Value, type_id.SelectedValue, title.Value.ToString(), content.Value.ToString(),
           UserID.Value, DateTime.Now, status, start_time.Value.ToString(), end_time.Value.ToString(), priority_id.Text.ToString(), if_send.SelectedValue.ToString());

            strSql1 = @"INSERT INTO [OA_Task_Append]
           ([id]
           ,[task_id]
           ,[user_id]
           --,[look_time]
           --,[progress]
           --,[remark]
           ,[user_type]
           ,[up_time]
		   )
     VALUES" + RYinfo(this.KeyId.Value);
        }
        else if (this.action == "edit")
        {
            strSql = string.Format(@"
       UPDATE [OA_Task]
       SET --[id] = <id, varchar(50),>
      --,
	   [type_id] ='{1}'-- <type_id, varchar(50),>
      ,[title] = '{2}'--<title, nvarchar(50),>
      ,[content] = '{3}'--<content, text,>
      ,[creater_id] ='{4}'-- <creater_id, varchar(50),>
      ,[create_time] ='{5}'-- <create_time, datetime,>
      ,[status] = '{6}'--<status, int,>
      ,[start_time] = '{7}'--<start_time, datetime,>
      ,[end_time] = '{8}'--<end_time, datetime,>
       --,[complete_time] = '{9}'--<complete_time, datetime,>
      ,[priority_id] = '{9}'--<priority_id, varchar(50),>
      ,if_send= '{10}'--<if_send, int,>
 WHERE [id]= '{0}'", this.KeyId.Value, type_id.SelectedValue, title.Value.ToString(), content.Value.ToString(),
           UserID.Value, DateTime.Now, status, start_time.Value.ToString(), end_time.Value.ToString(), priority_id.Text.ToString(), if_send.SelectedValue.ToString());
            strSql1 = "DELETE from OA_Task_Append where task_id='" + this.KeyId.Value + "';" + @"INSERT INTO [OA_Task_Append]
           ([id]
           ,[task_id]
           ,[user_id]
           --,[look_time]
           --,[progress]
           --,[remark]
           ,[user_type]
           ,[up_time]
		   )
     VALUES" + RYinfo(this.KeyId.Value);
        }
        using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            try
            {
                SqlHelper.ExecuteNonQuery(sqlTransaction, CommandType.Text, strSql);
                SqlHelper.ExecuteNonQuery(sqlTransaction, CommandType.Text, strSql1);
               
                #region 保存 图片及音频
                string imgIds = imgId.Value.ToString();
                string voiceIds = voiceId.Value.ToString();
                if (action == "add")
                {
                    //pcSer.Add(model);
                    //saveRY(id, syr, xgr);
                    if (!string.IsNullOrEmpty(imgIds))
                    {
                        saveImgOrVoice(imgIds, this.KeyId.Value, "img");
                    }
                    if (!string.IsNullOrEmpty(voiceIds))
                    {
                        saveImgOrVoice(voiceIds, this.KeyId.Value, "voice");
                    }
                }
                else if (action == "edit")
                {
                    //pcSer.Update(model);
                    //publicDbOpClass.ExecSqlString("DELETE from OA_Journal_Append where journal_id='" + model.Id + "'");
                    //saveRY(id, syr, xgr);
                    if (!string.IsNullOrEmpty(imgIds))
                    {
                        saveImgOrVoice(imgIds, this.KeyId.Value, "img");
                    }
                    if (!string.IsNullOrEmpty(voiceIds))
                    {
                        saveImgOrVoice(voiceIds, this.KeyId.Value, "voice");
                    }
                }
                #endregion 保存 图片及音频

                if (status !=0)
                {
                //给执行人、相关人 发送微信消息
                  WXAPI.sendWeChatMsg(this.UserID.Value.ToString(), hfldTo.Value.ToString(), hfldCopyto.Value.ToString(), "task", this.KeyId.Value, title.Value.ToString(), DateTime.Now.ToString());
                }
                sqlTransaction.Commit();
                if (status == 0)
                {
                    RegisterScript("alert('系统提示：\\n\\操作成功！'); location='list.aspx?status=0&userType=cg&userID=" + UserID.Value.ToString() + "'");
                }
                else
                {
                    RegisterScript("alert('系统提示：\\n\\操作成功！'); location='list.aspx?status="+ status + "&userType=tj&userID=" + UserID.Value.ToString() + "'");
                }
            }
            catch
            {
                sqlTransaction.Rollback();
                RegisterScript("alert('系统提示：\\n\\操作失败！');");
            }
        }
    }
    /// <summary>
    /// 保存图片 or 语音
    /// </summary>
    /// <param name="Ids">服务器文件ID</param>
    /// <param name="KeyId">ID</param>
    /// <param name="type">img 图片;voice 语音</param>
    private static void saveImgOrVoice(string Ids, string KeyId, string type)
    {
        //Ids = "BdlTq2kWT4JhM_YsLdJIgg_xBhWa3-3ZyCXMBilIFdqNx2Sen2AdDO5QDY2XDCvW";
        // type = "voice";
        if (!string.IsNullOrEmpty(Ids))
        {
            string[] strImgs = Ids.Substring(0, Ids.Length - 1).Split(',');
            HttpContext context = HttpContext.Current;
            string str1 = HttpContext.Current.Server.MapPath("/");
            string str3 = KeyId;
            string str2 = "";//ConfigHelper.Get("JournalVoices");
            string pathTemp = "";
            string path = "";
            if (type == "img")
            {
                str2 = "/UploadFiles/TaskPhotos/";//ConfigHelper.Get("JournalPhotos");
                pathTemp = str1.Substring(0, str1.Length - 1) + str2 + str3 + "\\";
            }
            else if (type == "voice")
            {
                str2 = "/UploadFiles/TaskVoices/";//ConfigHelper.Get("JournalVoices");
                pathTemp = str1.Substring(0, str1.Length - 1) + str2 + str3 + "Temp\\";
                path = str1.Substring(0, str1.Length - 1) + str2 + str3 + "\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            if (!Directory.Exists(pathTemp))
            {
                Directory.CreateDirectory(pathTemp);
            }
            foreach (string strID in strImgs)
            {
                //string  str11 = "BdlTq2kWT4JhM_YsLdJIgg_xBhWa3-3ZyCXMBilIFdqNx2Sen2AdDO5QDY2XDCvW";
                string tagUrl = "https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token=" + WXAPI.getTokenByAgentId(1000010) + "&media_id=" + strID + "";
                HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, null);
                string str = response.Headers.ToString();
                string strfwqFilename = str.Split('"')[1];//文件名及后缀
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                Stream stream = new FileStream(pathTemp + strfwqFilename, FileMode.Create);//保存文件到服务器目录
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                if (type == "voice")
                {
                    string paths = str1.Substring(0, str1.Length - 1).Replace("\\", "/");// paths = "C:\\ffmpeg.exe";
                    string pathBefore = pathTemp + strfwqFilename;//"D:\\VOICE_001.amr";
                    string pathLater = path + strfwqFilename.Split('.')[0] + ".mp3";//"D:\\VOICE_001.mp3";
                    WavConvertToAmr toamr = new WavConvertToAmr();
                    toamr.ConvertToMp3(paths, pathBefore, pathLater);
                }
            }

        }
    }
   
    protected void RegisterScript(string message)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<script type='text/javascript'>").Append(Environment.NewLine).Append(message).Append("</script>");
        base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), builder.ToString());
    }
    //获取负责人及相关人并拼接sql
    private string RYinfo(string Id)
    {
        int ii = 0;
        string strs = "";
        string strSYR = hfldTo.Value.ToString();//审阅人
        string strXGRS = hfldCopyto.Value.ToString();//相关人
        if (!string.IsNullOrEmpty(strXGRS))
        {
            string[] strXGR = strXGRS.Split(',');
            foreach (string str in strXGR)
            {
                int user_type = 1;
                if (str == strSYR)
                {
                    user_type = 2;//3 提交人、0 审阅人、1相关人、2审阅及相关人;
                    ii = 1;//为1时,同时保存审阅人
                }
                else
                {
                    user_type = 1;// 3 提交人、0 审阅人、1相关人、2审阅及相关人;
                }
                //
                // --,< progress, int,>
                //  --,< remark, varchar(50),>
                //--,< up_time, datetime2(7),>
                strs += string.Format(@"(
             '{0}'--<id, varchar(50),>
           , '{1}'--<task_id, varchar(50),>
           , '{2}'--<user_id, varchar(50),>
           , {3}--<user_type, int,>
           , {4}--,< look_time, datetime,>
		   ),", Guid.NewGuid().ToString(), Id, str, user_type, "null");
            }
        }
        if (!string.IsNullOrEmpty(strSYR))
        {
            if (ii == 0)
            {
                strs += string.Format(@"(
             '{0}'--<id, varchar(50),>
           , '{1}'--<task_id, varchar(50),>
           , '{2}'--<user_id, varchar(50),>
           , {3}--<user_type, int,>
            , {4}--,< look_time, datetime,>
		   ),", Guid.NewGuid().ToString(), Id, strSYR, 0, "null");
            }
        }
        //保存 提交人(为了评论提醒功能) user_type = 3;//3 提交人、0 审阅人、1相关人、2审阅及相关人;
        strs += string.Format(@"(
             '{0}'--<id, varchar(50),>
           , '{1}'--<task_id, varchar(50),>
           , '{2}'--<user_id, varchar(50),>
           , {3}--<user_type, int,>
           , '{4}'--,< look_time, datetime,>
		   ),", Guid.NewGuid().ToString(), Id, UserID.Value.ToString(), 3, DateTime.Now);

        return strs.Substring(0, strs.Length - 1) + ";";
    }
}