﻿using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PMBase.Basic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AjaxGetMsg : System.Web.UI.Page
{
    private static OAJournalService pcSer = new OAJournalService();
    private static OAJournalAppendService pcSer2 = new OAJournalAppendService();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]//标示为web服务方法属性
    public static string getDepts()
    { //{ "alldepts":[{"id":2,"name":"开发部1","fid":1},{"id":3,"name":"测试部1","fid":1},{"id":4,"name":"销售部1","fid":1},{"id":5,"name":"运营部1","fid":1},{"id":6,"name":"维护部1","fid":1},{"id":7,"name":"人事部1","fid":1},{"id":8,"name":"财务部1","fid":1}]}
        string strRes1 = "{\"alldepts\":[";
        string strRes2 = "";
        string strRes3 = "]}";
        DataTable dt = publicDbOpClass.DataTableQuary("select i_bmdm,i_sjdm,V_BMMC from PT_d_bm where c_sfyx='y'");
        foreach (DataRow dr in dt.Rows)
        {
            strRes2 += "{\"id\":\"" + dr["i_bmdm"] + "\",\"name\":\"" + dr["V_BMMC"] + "\",\"fid\":\"" + dr["i_sjdm"] + "\"},";
        }
        string strRes = strRes1 + strRes2.Substring(0, strRes2.Length - 1) + strRes3;
        return strRes;
        //return "{\"alldepts\":[{\"id\":2,\"name\":\"开发部1\",\"fid\":1},{\"id\":3,\"name\":\"测试部1\",\"fid\":1},{\"id\":4,\"name\":\"销售部1\",\"fid\":1},{\"id\":5,\"name\":\"运营部1\",\"fid\":1},{\"id\":6,\"name\":\"维护部1\",\"fid\":1},{\"id\":7,\"name\":\"人事部1\",\"fid\":1},{\"id\":8,\"name\":\"财务部1\",\"fid\":1}]}";
    }

    [WebMethod]//标示为web服务方法属性
    public static string getUsers(string AgentId)
    {
        string strRes1 = "{\"allusers\":[";
        string strRes2 = "";
        string strRes3 = "]}";
        DataTable dt = publicDbOpClass.DataTableQuary("select WXID,i_bmdm,v_yhdm,v_xm,MobilePhoneCode from PT_yhmc where State=1");
        foreach (DataRow dr in dt.Rows)
        {
            string imgUrl = "";
            JObject json1 = WXAPI.getUserInfo(dr["WXID"].ToString(), AgentId);
            if (json1 != null)
            {
                string code = json1["errcode"].ToString();

                if (code == "0")
                {
                    imgUrl = json1["avatar"].ToString();
                }
                else
                {
                    imgUrl = "";
                }
            }
            else
            {
                imgUrl = "";
            }
            //JObject json1 = WXAPI.getUserInfo(dr["WXID"].ToString());
            //string code = json1["errcode"].ToString();
            //string imgUrl = "";
            //if (code == "0")
            //{
            //    imgUrl = json1["avatar"].ToString();
            //}else
            //{
            //    //imgUrl = code;
            //}
            strRes2 += "{\"dept\":\"" + dr["i_bmdm"] + "\",\"first\":\"" + WXAPI.GetCharSpellCode(dr["v_xm"].ToString().Substring(0, 1)) + "\",\"id\":\"" + dr["v_yhdm"] + "\",\"name\":\"" + dr["v_xm"] + "\",\"tel\":\"" + dr["MobilePhoneCode"] + "\",\"url\":\"" + imgUrl + "\"},";
        }
        string strRes = strRes1 + strRes2.Substring(0, strRes2.Length - 1) + strRes3;
        return strRes;
        //{dept:1,first:"Z",id:1,name:"张三",tel:13804093570,url:"http://p.qlogo.cn/bizmail/uqibPEIicNrjEErw2dcgln6868Micl38EWF1VfWlQOcHgQUEJE2D26lmA/100"});
    }
    [WebMethod]//标示为web服务方法属性
    public static string getlogList(string userId, string userType, int rows, int page, string search)
    {
        //string strRes1 = " [{\"total\":\"15\",\"data\":[";
        string strRes2 = "";
        string strRes3 = "]}]";
        string strCreater = userId;//"00000000";//getUserIdByCode(code);//"00000000";//
        string strTemp = "";
        //userType   //cg 草稿 tj 已提交 sy 我审阅的 xg 相关的
        if (userType == "cg") { strTemp = " and OA_Journal.creater='" + strCreater + "' and OA_Journal.status='0'"; }
        if (userType == "tj") { strTemp = " and OA_Journal.creater='" + strCreater + "' and OA_Journal.status='1'"; }
        if (userType == "sy") { strTemp = " and OA_Journal.Id IN(select journal_id from OA_Journal_Append where user_id='" + strCreater + "' and (user_type='0' or user_type='2')) and OA_Journal.status='1'"; }
        if (userType == "xg") { strTemp = " and OA_Journal.Id IN(select journal_id from OA_Journal_Append where user_id='" + strCreater + "' and (user_type='1' or user_type='2')) and OA_Journal.status='1'"; }
        string str1 = ((rows * (page - 1)) + 1).ToString();
        string str2 = (rows * page).ToString();
        string strs = "  and  pageindex between " + str1 + " and " + str2;
        string str3 = "";
        if (!string.IsNullOrEmpty(search))
        {
            str3 = " and OA_Journal.title like '%" + search + "%'";
        }
        //string strSQL = @"select * from(select  row_number() over (order by create_date desc,id desc)as pageindex,* from OA_Journal WHERE 1=1 " + strTemp + ")t where 1=1 " + str3 + strs + "";
       // string strSQLCount = @"select * from(select  row_number() over (order by create_date desc,id desc)as pageindex,* from OA_Journal WHERE 1=1 " + strTemp + ")t where 1=1 " + str3 + " ";
        //DataTable dt = publicDbOpClass.DataTableQuary(strSQL);

        string strWhere = strTemp + str3;
        DataTable dtA = pcSer.GetMsgTable(strWhere, userId);
        DataRow[] rowAs = dtA.Select(" pageindex >=" + str1 + " and  pageindex<=" + str2);
        DataTable dtB = dtA.Clone();
        foreach (DataRow row in rowAs)
        {
            dtB.Rows.Add(row.ItemArray);
        }
       // DataTable dtCount = publicDbOpClass.DataTableQuary(strSQLCount);
        if (dtB.Rows.Count > 0)
        {
            foreach (DataRow dr in dtB.Rows)
            {
                strRes2 += "{\"id\":\"" + dr["id"] + "\",\"status\":\"" + dr["status"] + "\",\"userType\":\"" + userType + "\",\"title\":\"" + dr["title"] + "\",\"content\":\"" + GetFormatStr(dr["content"].ToString()) + "\",\"create_date\":\"" + dr["create_date"] + "\",\"ifck\":\"" + dr["ifck"] + "\",\"plAll\":\"" + dr["plAll"] + "\",\"plNew\":\"" + dr["plNew"] + "\"},";
            }
            string strRes = " [{\"total\":\"" + dtA.Rows.Count + "\",\"data\":[" +strRes2.Substring(0, strRes2.Length - 1) + strRes3;
            return strRes;
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 格式化字符串
    /// </summary>
    /// <param name="AStr"></param>
    /// <returns></returns>
    public static string GetFormatStr(string AStr)
    {

        if ("" == AStr)
            return "";

        else
        {
            AStr = AStr.Replace("\n", "<p>");
            AStr = AStr.Replace("\r", "<br>");
            AStr = AStr.Replace(" ", "&nbsp;&nbsp;");
            //AStr = AStr.Replace("<", "&lt;");
            //AStr = AStr.Replace(">", "&gt;");
            //AStr = AStr.Replace("'", "&apos;");
            //AStr = AStr.Replace("\"", "&quot;");
            //AStr = AStr.Replace("\"", "&quot;");
            //AStr = AStr.Replace("×", "&times;");
            //AStr = AStr.Replace("÷", "&divide;");
            return AStr;
        }
    }
    [WebMethod]//标示为web服务方法属性
    public static string getTemps(string typeId,string mk)
    {
        string strRes3 = ""; DataTable dt = null;
        if (mk == "log")
        {
             dt = publicDbOpClass.DataTableQuary("select * from OA_Journal_Type where Id='" + typeId + "'");
        }
        if (mk == "task")
        {
            dt = publicDbOpClass.DataTableQuary("select * from OA_Task_Type where Id='" + typeId + "'");
        }
        foreach (DataRow dr in dt.Rows)
        {
            strRes3 = dr["title_temp"].ToString() + ";" + dr["content_temp"].ToString();
        }
        return strRes3;
    }

    [WebMethod]//标示为web服务方法属性
    public static string getTypes(string diaryId)
    {
        string strRes0 = "";
        if (!string.IsNullOrEmpty(diaryId))
        {
            strRes0 = "<option value=\"0\" >请选择</option>";
        }
        else
        {
            strRes0 = "<option value=\"0\" selected>请选择</option>";
        }

        //string strRes1 = "<option value=\"1\">日报</option>";
        DataTable dt = publicDbOpClass.DataTableQuary(" select * from OA_Journal_Type where is_use=1 ORDER BY sort desc");
        DataTable dt2 = publicDbOpClass.DataTableQuary(" select * from OA_Journal where id='" + diaryId + "' ");
        foreach (DataRow dr in dt.Rows)
        {
            if (!string.IsNullOrEmpty(diaryId) && dr["Id"].ToString() == dt2.Rows[0]["type_id"].ToString())
            {
                strRes0 += "<option value=\"" + dr["Id"].ToString() + "\" selected>" + dr["name"].ToString() + "</option>";
            }
            else
            {
                strRes0 += "<option value=\"" + dr["Id"].ToString() + "\">" + dr["name"].ToString() + "</option>";///dr["title_temp"].ToString() + ";" + dr["content_temp"].ToString();
            }

        }
        return strRes0;
    }
    [WebMethod]//标示为web服务方法属性
    //加载上次 审阅人 or 相关人  type 0:审阅人；1相关人
    public static string GetBeforeUsers(string type,string userID, string mk)
    {
        int user_type = 0;
        int user_type2 = 0;
        string strUser = userID;// "00000000"; //getUserIdByCode(code);//"00000000";
        if (type == "syr")
        {
            user_type = 0;
            user_type2 = 2;
        }
        if (type == "xgr")
        {
            user_type = 1;
            user_type2 = 2;
        }

        string AgentId = string.Empty;
        //string strRes1 = "{\"checkUsers\":[";
        string strRes1 = "[";
        string strRes2 = "";
        //string strRes3 = "]}";
        string strRes3 = "]";
        DataTable dt = null;
        if (mk=="log")
        {
             dt = publicDbOpClass.DataTableQuary(@"select WXID,v_yhdm,v_xm  from PT_yhmc WHERE v_yhdm in(SELECT user_id from OA_Journal_Append WHERE (user_type = " + user_type + " or  user_type = " + user_type2 + ") and journal_id  in(select top 1 id from OA_Journal WHERE creater = '" + strUser + "' ORDER BY create_date desc))");
            AgentId = "1000008";
        }
        else if(mk == "task")
        {
             dt = publicDbOpClass.DataTableQuary(@"select WXID,v_yhdm,v_xm  from PT_yhmc WHERE v_yhdm in(SELECT user_id from OA_Task_Append WHERE (user_type = " + user_type + " or  user_type = " + user_type2 + ") and journal_id  in(select top 1 id from OA_Task WHERE creater_id = '" + strUser + "' ORDER BY create_time desc))");
            AgentId = "1000010";
        }
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                //JObject json1 = WXAPI.getUserInfo(dr["WXID"].ToString());
                //string code = json1["errcode"].ToString();
                //string imgUrl = "";
                //if (code == "0")
                //{
                //    imgUrl = json1["avatar"].ToString();
                //}
                string imgUrl = "";
                JObject json1 = WXAPI.getUserInfo(dr["WXID"].ToString(), AgentId);
                if (json1 != null)
                {
                    string code = json1["errcode"].ToString();

                    if (code == "0")
                    {
                        imgUrl = json1["avatar"].ToString();
                    }
                    else
                    {
                        imgUrl = "";
                    }
                }
                else
                {
                    imgUrl = "";
                }
                //id name 姓名 imgUrl 头像
                strRes2 += "{\"id\":\"" + dr["v_yhdm"] + "\",\"name\":\"" + dr["v_xm"] + "\",\"url\":\"" + imgUrl + "\"},";
            }
        }
        string strRes = strRes1 + strRes2.Substring(0, strRes2.Length - 1) + strRes3;
        return strRes;
    }
    [WebMethod]//标示为web服务方法属性
    //
    public static string getDataById(string id)
    {
        string strRes1 = "[";
        string strRes2 = "";
        string strRes3 = "]";

        DataTable dt = publicDbOpClass.DataTableQuary(@"select OA_Journal_Type.name typeName,PT_yhmc.v_xm ,PT_PrjInfo.PrjName,OA_Task.title taskName,OA_Journal.* from OA_Journal 
                                                        left join OA_Journal_Type on type_id=OA_Journal_Type.Id
                                                        left join PT_yhmc on creater=PT_yhmc.v_yhdm
                                                        LEFT JOIN OA_Task ON OA_Journal.task_id=OA_Task.id
                                                        left join PT_PrjInfo on project_id=PT_PrjInfo.PrjGuid where OA_Journal.id='"+ id + "'");
        //DataTable dt = pcSer.GetMsgTable("OA_Journal.Id='" + id + "'", UserCode);
        if (dt.Rows.Count > 0)
        {
            string ptName = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["project_id"].ToString() != "undefined" && !string.IsNullOrEmpty(dr["project_id"].ToString()))
                {
                    PTPrjInfo ptp = new PTPrjInfo();
                    PTPrjInfoService ptpService = new PTPrjInfoService();
                    ptp = ptpService.GetById(dr["project_id"].ToString());
                    ptName = ptp.PrjName;
                }
                else
                {
                    ptName = "";
                }
                //{id:"1",type_id:"1",project_id:"1",title:"测试标题1",
                //    creater: "张三",status: "2",start_time: "2017-12-01",times: "1",
                //                content: "测试数据一",intro: "摘要",cover: "封面",attachs: "附件",
                //                vidios: "视频",voices: "音频",tasks: "关联任务"
                strRes2 += "{\"id\":\"" + dr["id"] + "\",\"typeName\":\"" + dr["typeName"] + "\",\"v_xm\":\"" + dr["v_xm"] + "\",\"PrjName\":\"" + ptName + "\",\"taskName\":\"" + dr["taskName"] + "\",\"title\":\"" + dr["title"] + "\",\"type_id\":\"" + dr["type_id"] + "\",\"project_id\":\"" + dr["project_id"] + "\",\"creater\":\"" + dr["creater"] + "\",\"status\":\"" + dr["status"] + "\",\"start_time\":\"" + Convert.ToDateTime(dr["start_time"]).ToString("yyyy-MM-dd HH:mm") + "\",\"end_time\":\"" + Convert.ToDateTime(dr["end_time"]).ToString("yyyy-MM-dd HH:mm") + "\",\"content\":\"" + GetFormatStr(dr["content"].ToString()) + "\"}";
            }
            string strRes = strRes1 + strRes2 + strRes3;
        return strRes;
        }else
        {
            return "";
        }
    }
    [WebMethod]//标示为web服务方法属性
    //加载上次 审阅人 or 相关人  type 0:审阅人；1相关人
    public static string getUsersById(string Id, string userType,string mk)
    {
        int user_type = 0;
        int user_type2 = 0;
        if (userType == "syr")
        {
            user_type = 0;
            user_type2 = 2;
        }
        if (userType == "xgr")
        {
            user_type = 1;
            user_type2 = 2;
        }

        string AgentId = string.Empty;
        //string strRes1 = "{\"checkUsers\":[";
        string strRes1 = "[";
        string strRes2 = "";
        //string strRes3 = "]}";
        string strRes3 = "]";
        string strSQL = "";
        if (mk == "log")
        {
            strSQL = @"select WXID,v_yhdm,v_xm  from PT_yhmc WHERE v_yhdm in(SELECT user_id from OA_Journal_Append WHERE (user_type = " + user_type + " or  user_type = " + user_type2 + ") and journal_id  in(select id from OA_Journal WHERE id = '" + Id + "'))";
            AgentId = "1000008";
        }
        else if (mk == "task")
        {
            strSQL = @"select WXID,v_yhdm,v_xm  from PT_yhmc WHERE v_yhdm in(SELECT user_id from OA_Task_Append WHERE (user_type = " + user_type + " or  user_type = " + user_type2 + ") and task_id  in(select id from OA_Task WHERE id = '" + Id + "'))";
            AgentId = "1000010";
        }

        DataTable dt = publicDbOpClass.DataTableQuary(strSQL);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string imgUrl = "";
                JObject json1 = WXAPI.getUserInfo(dr["WXID"].ToString(), AgentId);
                if (json1 != null)
                {
                    string code = json1["errcode"].ToString();

                    if (code == "0")
                    {
                        imgUrl = json1["avatar"].ToString();
                    }
                    else
                    {
                        imgUrl = "";
                    }
                }
                else
                {
                    imgUrl = "";
                }
                //JObject json1 = WXAPI.getUserInfo(dr["WXID"].ToString());
                //string code = json1["errcode"].ToString();
                //string imgUrl = "";
                //if (code == "0")
                //{
                //       imgUrl = json1["avatar"].ToString();
                //}
                //id name 姓名 imgUrl 头像
                strRes2 += "{\"id\":\"" + dr["v_yhdm"] + "\",\"name\":\"" + dr["v_xm"] + "\",\"url\":\"" + imgUrl + "\"},";
            }
            string strRes = strRes1 + strRes2.Substring(0, strRes2.Length - 1) + strRes3;
            return strRes;
        }
        else
        {
            return "";
        }
       
        
    }
    
    [WebMethod]//标示为web服务方法属性
               //提交保存 type:0 草稿;1 提交;    新增or修改
               //data: "{id: '" + 日志id + "',type: '" +   草稿;1 提交;  + "',action: '" +   add | edit + "',
               //title: '" + 标题 + "'content: '" + 内容 + "'
               //start_time: '" + 开始时间 + "'end_time: '" + 结束时间 + "'type_id: '" +日志类型 + "',
               //syr: '" + 审阅人 + "',xgr: '" + 相关人 + "',project_id: '" + 关联项目 + "',tasks: '" + 关联任务 + "'",
    public static string commitTask(string userId, string id, string imgIds, string voiceIds, string type, string action, string title, string content, string start_time, string end_time, string type_id, string syr, string xgr, string project_id, string task_id)
    {
        //imgIds: '" + imgIds2 + "',voiceIds:
        //voiceIds = "-65SU9sxbsv5n4AXuznCpincdNTiXb8qI7FAd1qcfbkPSBmmPoyic0T7W0AM2il5,B0hBldDNmGhuLzM9RxCu3VDRMn3ORC8Wcm9kwelCGKSKTryrhVPCjqP5RW-cOqc6,";
        //imgIds="iPs_fkMiDb0E9cb7AIlF-bec-lMw4ruLHLCygGmfEamK_F4NJBtG3BhD6xZV61No,enOIMqtPNtyj-ofOCpiY7b0JEkZzn7dHDFIWPCnVd6YxN2NnUiQ2FMEuOG0qrT6E,-icQXBtLRcPboc1Fa1Opn0Q9Wp08ePXY_To8MwPKJlhAFCSjsE-AWxoYV-7Yucc3,";
        OAJournal model = new OAJournal();
        model.Id = id;
        model.title = title;//标题
        model.type_id = type_id;//日志类型
        model.start_time = Convert.ToDateTime(start_time); ;//日志开始时间
        model.end_time = Convert.ToDateTime(end_time);//日志结束时间
        model.content = content;//内容
        model.status = Convert.ToInt32(type);//日志状态(0草稿;1提交)
        model.creater = userId;//"00000000";//创建人ID
        model.create_date = DateTime.Now;//创建时间
        if (!string.IsNullOrEmpty(project_id) && project_id!="undefined" && project_id.Length>10)
        {
           model.project_id = project_id;//关联项目ID
        }
        if (!string.IsNullOrEmpty(task_id) && task_id != "undefined" && task_id.Length > 10)
        {
            model.task_id = task_id;//关联任务ID
        }
        //model.voices = voiceIds;//关联语音
        //model.vidios = imgIds;//关联视频(图片)
        //model.cover = "";//封面
        try
        {
            if (action == "add")
            {
                pcSer.Add(model);
                saveRY(id, syr, xgr);
                if (!string.IsNullOrEmpty(imgIds))
                {
                    saveImgOrVoice(imgIds, id, "img");
                }
                if (!string.IsNullOrEmpty(voiceIds))
                {
                    saveImgOrVoice(voiceIds, id, "voice");
                }
                //saveImgOrVoice("aEc5LvTvcNTCvHkrt2wOyDSY3toM8erri3HpKyf4XEgJNpRPIp8_6TQGW0pWBiSH,", id, "img"); 2017.12.15
            }
            else if (action == "edit")
            {
                pcSer.Update(model);
                publicDbOpClass.ExecSqlString("DELETE from OA_Journal_Append where journal_id='" + model.Id + "'");
                saveRY(id, syr, xgr);
                if (!string.IsNullOrEmpty(imgIds))
                {
                    saveImgOrVoice(imgIds, id, "img");
                }
                if (!string.IsNullOrEmpty(voiceIds))
                {
                    saveImgOrVoice(voiceIds, id, "voice");
                }
            }

            if (Convert.ToInt32(type) == 1)
            {
                string strSYR = syr;//审阅人
                string strXGRS = xgr;//相关人

                string strUsers = "";
                if (!string.IsNullOrEmpty(strSYR))
                {
                    strUsers += "'" + strSYR + "'";
                }
                if (!string.IsNullOrEmpty(strXGRS))
                {
                    string[] strXGR = strXGRS.Split(',');
                    strUsers += "," + "'" + strXGRS + "'";
                    //strUsers += "|"+ strXGRS.Replace(',','|');
                }
                string strSQL1 = "select * from PT_yhmc where v_yhdm ='" + userId + "'";
                DataTable dt1 = publicDbOpClass.DataTableQuary(strSQL1);
                string strSQL = "select * from PT_yhmc where v_yhdm in(" + strUsers + ")";
                DataTable dt = publicDbOpClass.DataTableQuary(strSQL);
                //string wxUsers = "";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //    wxUsers += dr["WXID"] + "|";
                        //}
                        //string str0 = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx251384a873c4d422&redirect_uri=";
                        string str1 = new BasicConfigService().GetValue("domain");
                        string str2 = "/WeChat/log/show.html?id=" + model.Id + "&userType=tj&userID=" + dr["v_yhdm"];
                        //string str3 = "&action=add&response_type=code&scope=snsapi_base#wechat_redirect";

                        //string str4 = System.Web.HttpUtility.UrlEncode(str1 + str2);//System.Web.HttpUtility.UrlDecode("");
                        //string strURL = str0 + str4 + str3;
                        ////WXAPI.sendText(1000008, "您有新的工作日志待查阅\n" + model.title.ToString() + model.start_time.ToString() + "\n", strURL, wxUsers.Substring(0, wxUsers.Length - 1));
                        //WXAPI.sendText(1000008, "您有新的工作日志待查阅\n" + model.title.ToString() + "\n" + model.start_time.ToString() + "\n", strURL, dr["WXID"].ToString());
                        string strURL = str1 + str2;
                        string strTitle = "您有新的工作日志待查阅";
                        string description = "<div class=\\\"highlight\\\">" + model.title.ToString() + "</div><div class=\\\"gray\\\">[填写人:" + dt1.Rows[0]["v_xm"].ToString() + "] " + model.create_date.ToString() + "</div>";
                        string btntxt = "点击查看";
                        WXAPI.sendTextCard(strTitle, 1000008, description, strURL, dr["WXID"].ToString(), btntxt);
                    }
                }
            }





            return "1";
        }
        catch(Exception ex)
        {
            return "0";
        }
    }
    /// <summary>
    /// 保存图片 or 语音
    /// </summary>
    /// <param name="Ids">服务器文件ID</param>
    /// <param name="JournalID">ID</param>
    /// <param name="type">img 图片;voice 语音</param>
    private static void saveImgOrVoice(string Ids, string JournalID, string type)
    {
         //Ids = "BdlTq2kWT4JhM_YsLdJIgg_xBhWa3-3ZyCXMBilIFdqNx2Sen2AdDO5QDY2XDCvW";
       // type = "voice";
        if (!string.IsNullOrEmpty(Ids))
        {
            string[] strImgs = Ids.Substring(0, Ids.Length - 1).Split(',');
            HttpContext context = HttpContext.Current;
            string str1 = HttpContext.Current.Server.MapPath("/");
            string str3 = JournalID;
            string str2 = "";//ConfigHelper.Get("JournalVoices");
            string pathTemp = "";
            string path = "";
            if (type == "img")
            {
                str2 = "/UploadFiles/JournalPhotos/";//ConfigHelper.Get("JournalPhotos");
                pathTemp = str1.Substring(0, str1.Length - 1) + str2 + str3 + "\\";
            }
            else if (type == "voice")
            {
                str2 = "/UploadFiles/JournalVoices/";//ConfigHelper.Get("JournalVoices");
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
                string tagUrl = "https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token=" + WXAPI.getTokenByAgentId(1000008) + "&media_id=" + strID + "";
                HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, null);
                string str=response.Headers.ToString();
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
    [WebMethod]//标示为web服务方法属性
    public static void test2()
    {
       // string type = "voice";
        //if (!string.IsNullOrEmpty(Ids))
        //{
        //    string[] strImgs = Ids.Substring(0, Ids.Length - 1).Split(',');
            HttpContext context = HttpContext.Current;
            string str1 = HttpContext.Current.Server.MapPath("/");
            string str2 = "";//ConfigHelper.Get("JournalVoices");
            //if (type == "img")
            //{
            //    str2 = "/UploadFiles/JournalPhotos/";//ConfigHelper.Get("JournalPhotos");
            //}
            //else if (type == "voice")
            //{
                str2 = "/UploadFiles/JournalVoices/";//ConfigHelper.Get("JournalVoices");
            //}
            //else
            //{
            //    //str2 = "/UploadFiles/";
            //}
            string str3 = "1122";//= JournalID;
            string path = str1.Substring(0, str1.Length - 1) + str2 + str3 + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //foreach (string str in strImgs)
            //{
                string str11 = "BdlTq2kWT4JhM_YsLdJIgg_xBhWa3-3ZyCXMBilIFdqNx2Sen2AdDO5QDY2XDCvW";
                string tagUrl = "https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token=" + WXAPI.getTokenByAgentId(1000008) + "&media_id=" + str11 + "";
                HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, null);
                string strfwqFilename = response.Headers.ToString().Split('"')[1];//文件名及后缀
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                Stream stream = new FileStream(path + strfwqFilename, FileMode.Create);//保存文件到服务器目录
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();

                //if (type == "voice")
                //{
                    string paths = str1.Substring(0, str1.Length - 1).Replace("\\", "/");
                    string pathBefore = path + strfwqFilename;//"D:\\VOICE_001.amr";
                    string pathLater = path + strfwqFilename.Split('.')[0] + ".mp3";//"D:\\VOICE_001.mp3";
                    WavConvertToAmr toamr = new WavConvertToAmr();
                    toamr.ConvertToMp3(paths, pathBefore, pathLater);
               // }
        //    }

        //}
    }
    //保存语音
    private static void saveVoice(string voiceIds, string JournalID)
    {
        
    }



    private static void saveImg(string imgIds)
    {
        if (!string.IsNullOrEmpty(imgIds))
        {
            string[] strImgs = imgIds.Substring(0,imgIds.Length-1).Split(',');
        }
    }

    private static void saveRY(string Id, string syr, string xgr)
    {
        OAJournalAppend model2 = new OAJournalAppend();
        int ii = 0;
        string strSYR = syr;//审阅人
        string strXGRS = xgr;//相关人
        if (!string.IsNullOrEmpty(strXGRS))
        {
            string[] strXGR = strXGRS.Split(',');
            foreach (string str in strXGR)
            {
                model2.Id = Guid.NewGuid().ToString();//主键
                model2.journal_id = Id;//日志ID
                model2.user_id = str;//审阅人ID
                if (str == strSYR)
                {
                    model2.user_type = 2;//0 审阅人、1相关人、2审阅及相关人;
                    ii = 1;//为1时,同时保存审阅人
                }
                else
                {
                    model2.user_type = 1;// l 审阅人、1相关人、2审阅及相关人;
                }
                //model2.score = null;//评价/评分
                //model2.look_time = null;//查阅时间
                pcSer2.Add(model2);
            }
        }
        if (!string.IsNullOrEmpty(strSYR))
        {
            if (ii == 0)
            {
                model2.Id = Guid.NewGuid().ToString();//主键
                model2.journal_id = Id;//日志ID
                model2.user_id = strSYR;//审阅人ID
                model2.user_type = 0;//0 审阅人、1相关人、2审阅及相关人;
                                     //model2.score = null;//评价/评分
                                     //model2.look_time = null;//查阅时间
                pcSer2.Add(model2);
            }
        }
    }

    [WebMethod]//标示为web服务方法属性
    public static string getProject_id(string diaryId,string userID)
    {
        string strRes0 = "";
        if (!string.IsNullOrEmpty(diaryId))
        {
            strRes0 = "<option value=\"\" >请选择</option>";
        }
        else
        {
            strRes0 = "<option value=\"\" selected>请选择</option>";
        }

        //string strRes1 = "<option value=\"1\">日报</option>";
        DataTable dt2 = publicDbOpClass.DataTableQuary(" select project_id from OA_Journal where id='" + diaryId + "' ");
        string strPID = "";
        if (dt2.Rows.Count > 0)
        {
            strPID = dt2.Rows[0]["project_id"].ToString();

        }
        System.Collections.Generic.IList<SelectProject> project = SelectProjectHelper.GetProject(userID, Parameters.PrjAvaildState5, "", "");
        foreach (var item in project)
        {
            if (!string.IsNullOrEmpty(strPID))
            {
                if (item.Id == strPID) {
                    strRes0 += "<option value=\"" + item.Id + "\" selected>" + item.Name + "</option>";
                }
                else
                {
                    strRes0 += "<option value=\"" + item.Id + "\">" + item.Name + "</option>";
                }
            }
            else
            {
                strRes0 += "<option value=\"" + item.Id + "\">" + item.Name + "</option>";///dr["title_temp"].ToString() + ";" + dr["content_temp"].ToString();
            }
        }
        return strRes0;
    }

    [WebMethod]//标示为web服务方法属性
    public static string getTask_id(string Id, string userID)
    {
        string strRes0 = "";
        if (!string.IsNullOrEmpty(Id))
        {
            strRes0 = "<option value=\"\" >请选择</option>";
        }
        else
        {
            strRes0 = "<option value=\"\" selected>请选择</option>";
        }

        DataTable dt2 = publicDbOpClass.DataTableQuary(" select task_id from OA_Journal where id='" + Id + "' ");
        string strPID = "";
        if (dt2.Rows.Count > 0)
        {
            strPID = dt2.Rows[0]["task_id"].ToString();

        }
        DataTable dtA = TaskService.GetTaskListTable(strWhere(userID), userID);
        if (dtA.Rows.Count > 0)
        {
            foreach (DataRow dr in dtA.Rows)
            {
                if (!string.IsNullOrEmpty(strPID))
                {
                    if (dr["id"].ToString() == strPID)
                    {
                        strRes0 += "<option value=\"" + dr["id"].ToString() + "\" selected>" + dr["title"].ToString() + "</option>";
                    }
                    else
                    {
                        strRes0 += "<option value=\"" + dr["id"].ToString() + "\">" + dr["title"].ToString() + "</option>";
                    }
                }
                else
                {
                    strRes0 += "<option value=\"" + dr["id"].ToString() + "\">" + dr["title"].ToString() + "</option>";///dr["title_temp"].ToString() + ";" + dr["content_temp"].ToString();
                }
            }
        }

        return strRes0;
    }
    public static string strWhere(string userID)
    {
        string strWhere = " ";// and ...
        //System.DateTime? startTime = DateTimeHelper.GetDateTime(this.txtStartTime.Text);
        //System.DateTime? endTime = DateTimeHelper.GetDateTime(this.txtEndTime.Text);
        //if (startTime.HasValue)
        //{
        //    strWhere += " and start_time >='" + startTime.Value + "'";
        //}
        //if (endTime.HasValue)
        //{
        //    strWhere += " and start_time <'" + endTime.Value.AddDays(1.0) + "'";
        //}
        //System.DateTime? stime = DateTimeHelper.GetDateTime(this.stime.Text);
        //System.DateTime? etime = DateTimeHelper.GetDateTime(this.etime.Text);
        //if (stime.HasValue)
        //{
        //    strWhere += " and create_time >='" + stime.Value + "'";
        //}
        //if (etime.HasValue)
        //{
        //    strWhere += " and create_time <'" + etime.Value.AddDays(1.0) + "'";
        //}

        //if (!string.IsNullOrEmpty(txtTitle.Text))
        //{
        //    strWhere += " and title like '%" + txtTitle.Text.ToString().Trim() + "%'";
        //}
        //if (type_id.SelectedValue != "")
        //{
        //    strWhere += " and type_id ='" + type_id.SelectedValue + "'";
        //}
        //if (status.SelectedValue != "")
        //{
        //    strWhere += " and status ='" + Convert.ToInt32(status.SelectedValue) + "'";
        //}
        //if (base.UserCode != "00000000")
        //{
        //    strWhere += " and creater_id ='" + UserCode + "'";
        //}
        //任务状态   0草稿、1未开始、2执行中、3已完成、4已关闭、5已删除 
        strWhere += " and status !='5' and status !='0' and status !='3' and status !='4'";
        //string strRYS = hfldTo.Value.ToString();//执行人
        //if (!string.IsNullOrEmpty(strRYS))
        //{
        //    string[] strXGR = strRYS.Split(',');
        //    string strs = "";
        //    foreach (string str in strXGR)
        //    {
        //        strs += "'" + str + "',";
        //    }
        //关联 任务执行人
        strWhere += " and OA_Task.Id in (SELECT task_id FROM OA_Task_Append where (user_type=0 or user_type=2 ) and user_id ='" + userID + "') ";
        //}
        return strWhere;
    }
    [WebMethod]//标示为web服务方法属性
    public static string getPLById(string Id,string mk)
    {
        string AgentId = string.Empty;
        string strRes1 = "[";
        string strRes2 = "";
        string strRes3 = "]";
        string strSql = "";
        if (mk=="log")
        {
            strSql = @" select 'ss' img, PT_yhmc.WXID ,PT_yhmc.v_xm ,OA_Journal_Comment.* from OA_Journal_Comment 
                        left join PT_yhmc on OA_Journal_Comment.user_id=PT_yhmc.v_yhdm WHERE journal_id='" + Id + "' ORDER BY time desc ";
            AgentId = "1000008";
        }
        if (mk == "task")
        {
            strSql = @" select 'ss' img, PT_yhmc.WXID ,PT_yhmc.v_xm ,OA_Comment.* from OA_Comment 
                        left join PT_yhmc on OA_Comment.user_id=PT_yhmc.v_yhdm WHERE head_id='" + Id + "' ORDER BY time desc ";
            AgentId = "1000010";
        }
        if (mk == "doc")
        {
            strSql = @" select 'ss' img, PT_yhmc.WXID ,PT_yhmc.v_xm ,OA_Comment.* from OA_Comment 
                        left join PT_yhmc on OA_Comment.user_id=PT_yhmc.v_yhdm WHERE head_id='" + Id + "' ORDER BY time desc ";
            AgentId = "1000012";
        }
        DataTable dt = publicDbOpClass.DataTableQuary(strSql);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string imgUrl = "";
                JObject json1 = WXAPI.getUserInfo(dr["WXID"].ToString(), AgentId);
                if (json1 != null)
                {
                    string code = json1["errcode"].ToString();

                    if (code == "0")
                    {
                        imgUrl = json1["avatar"].ToString();
                    }
                    else
                    {
                        imgUrl = "";
                    }
                }
                else
                {
                    imgUrl = "";
                }
                //JObject json1 = WXAPI.getUserInfo(dr["WXID"].ToString());
                //string code = json1["errcode"].ToString();
                //string imgUrl = "";
                //if (code == "0")
                //{
                //    imgUrl = json1["avatar"].ToString();
                //}
                strRes2 += "{\"id\":\"" + dr["id"] + "\",\"v_xm\":\"" + dr["v_xm"] + "\",\"img\":\"" + imgUrl + "\",\"time\":\"" + dr["time"] + "\",\"content\":\"" + dr["content"] + "\"},";
            }
            if (!string.IsNullOrEmpty(strRes2))
            {
                string strRes = strRes1 + strRes2.Substring(0, strRes2.Length - 1) + strRes3;
                return strRes;
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }

    }
    [WebMethod]//标示为web服务方法属性
    public static string getTreasuryInfo(string id)
    {
        DataTable dt = publicDbOpClass.DataTableQuary(@"select * from Sm_Treasury WHERE tid='" + id + "'");
        if (dt.Rows.Count > 0)
        {
            string tcore = dt.Rows[0]["tcode"].ToString();
            string tflag = dt.Rows[0]["tflag"].ToString();
            return tcore + "," + tflag;
        }
        else
        {
            return "0";
        }
    }
    [WebMethod]//标示为web服务方法属性
    public static string getJSSDK(string strUrl, string AgentId)
    {
        //appId: '', // 必填，企业微信的cropID
        //timestamp: , // 必填，生成签名的时间戳
        //nonceStr: '', // 必填，生成签名的随机串
        //signature: '',// 必填，签名，见[附录1](#11974)
        string appId = new BasicConfigService().GetValue("corpId");// 必填，签名，见[附录1](#11974)
        string timestamp = Convert.ToString(WXAPI.ConvertDateTimeInt(DateTime.Now)); // 必填，生成签名的时间戳
        string nonceStr = WXAPI.createNonceStr(); // 必填，生成签名的随机串
        string jsapiTicket = WXAPI.getTicket(AgentId);
        string url = strUrl;//HttpContext.Current.Request.Url.ToString();
        string rawstring = "jsapi_ticket=" + jsapiTicket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + url + "";
        //string rawstring = "jsapi_ticket=&noncestr=&timestamp=&url=";
        string signature = WXAPI.SHA1_Hash(rawstring);// 必填，签名
        string strRes = "[{\"appid\":\"" + appId + "\",\"timestamp\":\"" + timestamp + "\",\"jsapiTicket\":\"" + jsapiTicket + "\",\"nonceStr\":\"" + nonceStr + "\",\"signature\":\"" + signature + "\",\"url\":\"" + url + "\"}]";
        return strRes;
    }
    [WebMethod]//标示为web服务方法属性
    public static string getUserIdByCode(string code, string AgentId)
    {
        string str= WXAPI.getUserIdByCode(code, "1000008");
        return str;
    }
    [WebMethod]//标示为web服务方法属性
    public static string getPFById(string diaryId)
    {
        DataTable dt = publicDbOpClass.DataTableQuary(@"select * from OA_Journal_Append WHERE (user_type=0 or user_type=2) and journal_id='" + diaryId + "'");
        if (dt.Rows.Count>0)
        {
            string strPF = dt.Rows[0]["score"].ToString();
            string strUserID = dt.Rows[0]["user_id"].ToString();
            if (!string.IsNullOrEmpty(strPF))
            {
                return strUserID + "," + strPF;
            }
            else
            {
                return strUserID+",无";
            }
        }
        else
        {
            return "null,无";
        }

    }
    
   [WebMethod]//标示为web服务方法属性
    public static string setPFById(string diaryId, string userID, string score)
    {
        //设置评分
       int ii= publicDbOpClass.ExecSqlString("update OA_Journal_Append  set score='" + score + "' where (user_type=0 or user_type=2) and user_id='"+ userID + "' and journal_id='" + diaryId + "'");
        //返回评分人和评分结果
        if (ii>0) {
            return userID + "," + score;
        }else
        {
            return "";
        }
        //DataTable dt = publicDbOpClass.DataTableQuary(@"select * from OA_Journal_Append WHERE (user_type=0 or user_type=2) and user_id='" + userID + "' and  journal_id='" + diaryId + "'");
        //if(dt.Rows.Count>0)
        //{
        //    string strPF = dt.Rows[0]["score"].ToString();
        //    string strUserID = dt.Rows[0]["user_id"].ToString();
        //    if (!string.IsNullOrEmpty(strPF))
        //    {
        //        return strUserID + "," + strPF;
        //    }
        //    else
        //    {
        //        return strUserID + ",无";
        //    }
        //}
        //return userID + ",无";
    }
    //获取附件
    [WebMethod]//标示为web服务方法属性
    public static string getSrcFiles(string diaryId,string type)
    {
        string str1 = HttpContext.Current.Server.MapPath("/");
        string str2 = "";//ConfigHelper.Get("JournalVoices");
                         //if (type == "JournalPhotos")
                         //{
                         //    str2 = "/UploadFiles/JournalPhotos/";
                         //}
                         //if (type == "JournalFiles")
                         //{
                         //    str2 = "/UploadFiles/JournalFiles/";
                         //}
                         //if (type == "JournalVoices")
                         //{
                         //    str2 = "/UploadFiles/JournalVoices/";
                         //}
                         //if (type == "JournalVideos")
                         //{
                         //    str2 = "/UploadFiles/JournalVideos/";
                         //}
        str2 = "/UploadFiles/"+ type + "/";
        string str3 = diaryId; //"9247d6bf-7f22-4149-b09e-6879e7775562";// diaryId;
        string text = str1.Substring(0, str1.Length - 1).Replace("\\","/") + str2 + str3 + "/";
        string text2 = str2 + str3 + "/";
        HttpContext context = HttpContext.Current;
        context.Response.ContentType = "Application/json";
        string s = string.Empty;
        string arg_21_0 = string.Empty;
        if (!string.IsNullOrEmpty(context.Request["Path"]))
        {
            text = context.Request["Path"];
        }
        List<MergerFolder> list = null;
        if (!string.IsNullOrEmpty(context.Request["merger"]))
        {
            list = JsonConvert.DeserializeObject<List<MergerFolder>>(context.Request["merger"]);
        }
        List<Annex> list2 = new List<Annex>();
        if (list != null)
        {
            string str = text.Substring(text.LastIndexOf('/') + 1);
            foreach (MergerFolder current in list)
            {
                try
                {
                    DirectoryUtility directoryUtility = new DirectoryUtility(current.Path + str);
                    //if (type == "JournalVoices")
                    //{
                    //    list2.AddRange(directoryUtility.GetAnnex(true, text2, text));
                    //}else
                    //{
                        list2.AddRange(directoryUtility.GetAnnex(true, text2));
                   // }
                }
                catch
                {
                }
            }
        }
        try
        {
            DirectoryUtility directoryUtility = new DirectoryUtility(text);
            //if (type == "JournalVoices")
            //{
            //    list2.AddRange(directoryUtility.GetAnnex(text2, text));
            //}
            //else
            //{
                list2.AddRange(directoryUtility.GetAnnex(text2));
            //}
        }
        catch
        {
        }
        ISerializable serializable = new cn.justwin.Serialize.JsonSerializer();
        s = serializable.Serialize<List<Annex>>(list2);
        return s;
       // context.Response.Write(s);
    }
    [WebMethod]//标示为web服务方法属性
    public static string deleteFiles(string path) {
        try
        {
            if (File.Exists(path) && (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }
            string str1 = HttpContext.Current.Server.MapPath("/");
            File.Delete(str1.Substring(0, str1.Length - 1).Replace("\\", "/")+path);
        }
        catch(Exception ex)
        {
            return "0";
        }
        return "1";
    }
    private static OAJournalAppendService pcSer3 = new OAJournalAppendService();
    [WebMethod]//标示为web服务方法属性
    public static string setPLById(string id, string pl, string  userID, string title, string atPersonId)
    {
        //保存评论@的人

        OAJournalAppend model2 = new OAJournalAppend();
        string strXGRS = atPersonId.ToString();//评论@相关人
        if (!string.IsNullOrEmpty(strXGRS))
        {
            string strUsers = "";
            string[] strXGR = strXGRS.Split(',');
            foreach (string str in strXGR)
            {
                strUsers += "'" + str + "'" + ",";
                DataTable dt = pcSer3.resulet(str, id);
                if (dt.Rows.Count>0)
                {
                }else
                {
                    model2.Id = Guid.NewGuid().ToString();//主键
                    model2.journal_id = id;//日志ID
                    model2.user_id = str;//审阅人ID
                    model2.user_type = 1;// 3 提交人、0 审阅人、1相关人、2审阅及相关人;
                    pcSer3.Add(model2);
                }
            }
            string strSQL = "select * from PT_yhmc where v_yhdm in(" + strUsers.Substring(0, strUsers.Length - 1) + ")";
            DataTable dt2 = publicDbOpClass.DataTableQuary(strSQL);
            //string wxUsers = "";
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    //    wxUsers += dr["WXID"] + "|";
                    //}
                   // string str0 = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx251384a873c4d422&redirect_uri=";
                    string str1 = new BasicConfigService().GetValue("domain");
                    string str2 = "/WeChat/log/show.html?id=" + id + "&userType=tj&userID=" + dr["v_yhdm"];
                    //string str3 = "&action=add&response_type=code&scope=snsapi_base#wechat_redirect";
                    //string str4 = System.Web.HttpUtility.UrlEncode(str1 + str2);//System.Web.HttpUtility.UrlDecode("");
                    //string strURL = str0 + str4 + str3;
                    //WXAPI.sendText(1000008, "有新的工作日志评论@您\n" + title.Text.ToString() + "\n", strURL, wxUsers.Substring(0, wxUsers.Length - 1));
                    //WXAPI.sendText(1000008, "有新的工作日志评论@您\n" + title + "\n", strURL, dr["WXID"].ToString());
                    string strSQL1 = "select * from PT_yhmc where v_yhdm ='" + dr["v_yhdm"] + "'";
                    DataTable dt1 = publicDbOpClass.DataTableQuary(strSQL1);
                    string strURL = str1 + str2;
                    string strTitle = "有新的工作日志评论@您";
                    string description = "<div class=\\\"highlight\\\">" + title + "</div><div class=\\\"gray\\\">[评论人:" + dt1.Rows[0]["v_xm"].ToString() + "] " + DateTime.Now.ToShortDateString() + "</div>";
                    string btntxt = "点击查看";
                    WXAPI.sendTextCard(strTitle, 1000008, description, strURL, dr["WXID"].ToString(), btntxt);
                }
            }
        }
        int ii= publicDbOpClass.ExecSqlString("INSERT OA_Journal_Comment (journal_id,user_id,time,Id,content) VALUES('"+ id + "', '"+userID+"', '"+DateTime.Now.ToString()+"', '"+Guid.NewGuid()+"', '"+ pl + "')");
       return ii.ToString();
    }

    [WebMethod]//标示为web服务方法属性
    public static string savePLById(string id, string pl, string userID, string title, string atPersonId, string mk)
    {
        string strSql = "";
        string strSql1 = "";
        strSql = string.Format(@"
    INSERT INTO [OA_Comment]
           ([id]
           ,[head_id]
           ,[user_id]
           ,[time]
           ,[content])
     VALUES
           (
		    '{0}'--<id, varchar(50),>
           ,'{1}'--,<head_id, varchar(50),>
           ,'{2}'--,<user_id, varchar(50),>
           ,'{3}'--,<time, datetime,>
           ,'{4}'--,<content, text,>
		   )
        ", Guid.NewGuid().ToString(),  id ,userID, DateTime.Now, pl.ToString());

        using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            try
            {
                SqlHelper.ExecuteNonQuery(sqlTransaction, CommandType.Text, strSql);
                string strs = RYinfo(id, atPersonId.ToString());
                if (!string.IsNullOrEmpty(strs) && mk == "task")
                {
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
                                VALUES " + strs;
                    SqlHelper.ExecuteNonQuery(sqlTransaction, CommandType.Text, strSql1);
                }
                sqlTransaction.Commit();

                if (!string.IsNullOrEmpty(atPersonId.ToString()))
                {
                    string strUsers = atPersonId.ToString();
                    string strSQL = "select * from PT_yhmc where v_yhdm in(" + strUsers.Substring(0, strUsers.Length - 1) + ")";
                    DataTable dt2 = publicDbOpClass.DataTableQuary(strSQL);
                    //string wxUsers = "";
                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt2.Rows)
                        {
                            if (mk == "task")
                            {
                                string str1 = new BasicConfigService().GetValue("domain");
                                string str2 = "/WeChat/task/show.aspx?id=" + id + "&userType=tj&userID=" + dr["v_yhdm"];
                                string strSQL1 = "select * from PT_yhmc where v_yhdm ='" + dr["v_yhdm"] + "'";
                                DataTable dt1 = publicDbOpClass.DataTableQuary(strSQL1);
                                string strURL = str1 + str2;
                                string strTitle = "有新的工作任务评论@您";
                                string description = "<div class=\\\"highlight\\\">" + title + "</div><div class=\\\"gray\\\">[评论人:" + dt1.Rows[0]["v_xm"].ToString() + "] " + DateTime.Now.ToShortDateString() + "</div>";
                                string btntxt = "点击查看";
                                WXAPI.sendTextCard(strTitle, 1000010, description, strURL, dr["WXID"].ToString(), btntxt);
                            }
                            if (mk == "doc")
                            {
                                ////发送微信消息
                                //WXAPI.sendWeChatMsg(UserCode.ToString(), "", hfldCopyto.Value.ToString(), "taskPL", this.KeyId.Value, title.Text.ToString(), DateTime.Now.ToShortDateString());
                                string str1 = new BasicConfigService().GetValue("domain");
                                string str2 = "/WeChat/doc/show.aspx?ic=" + id + "&userID=" + dr["v_yhdm"];
                                string strSQL1 = "select * from PT_yhmc where v_yhdm ='" + dr["v_yhdm"] + "'";
                                DataTable dt1 = publicDbOpClass.DataTableQuary(strSQL1);
                                string strURL = str1 + str2;
                                string strTitle = "有新的工程文档评论@您";
                                string description = "<div class=\\\"highlight\\\">" + title + "</div><div class=\\\"gray\\\">[评论人:" + dt1.Rows[0]["v_xm"].ToString() + "] " + DateTime.Now.ToShortDateString() + "</div>";
                                string btntxt = "点击查看";
                                WXAPI.sendTextCard(strTitle, 1000012, description, strURL, dr["WXID"].ToString(), btntxt);
                            }
                        }
                    }
                }
                return "1";
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                return "";
            }
        }
    }
    //获取负责人及相关人并拼接sql
    private static string RYinfo(string Id, string XGRS)
    {
        string strs = "";
        string strXGRS = XGRS;//评论@相关人
        if (!string.IsNullOrEmpty(strXGRS))
        {
            string[] strXGR = strXGRS.Split(',');
            foreach (string str in strXGR)
            {
                DataTable dt = resulet(str, Id);
                if (dt.Rows.Count > 0)
                {

                }
                else
                {
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
		   ),", Guid.NewGuid().ToString(), Id, str, '1', "null");
                }
            }
        }
        if (!string.IsNullOrEmpty(strs))
        {
            return strs.Substring(0, strs.Length - 1) + ";";
        }
        else
        {
            return null;
        }
    }
    public static DataTable resulet(string user_id, string KeyId)
    {
        string strSql = string.Format(@"select * from OA_Task_Append where user_id='{0}' and task_id='{1}'  ", user_id, KeyId);
        DataTable dt = publicDbOpClass.DataTableQuary(strSql);
        return dt;
    }
    [WebMethod]//标示为web服务方法属性
    //删除方法 
    public static string deleteInfoByIds(string ids, string type)
    {
        string strWhere = " in (\'" + ids.Replace(",","','") + "\')";
        if (type== "log")
        {
            string StrSql = "delete from  OA_Journal where id " + strWhere + ";delete from OA_Journal_Append where journal_id " + strWhere + ";";
            int ii = publicDbOpClass.ExecSqlString(StrSql);
            return ii.ToString();
        }
        if (type == "task")
        {
            string StrSql = "delete from OA_Task where id " + strWhere + ";delete from OA_Task_Append where task_id " + strWhere + ";";
            int ii = publicDbOpClass.ExecSqlString(StrSql);
            return ii.ToString();
        }
        else
        {
            return "";
        }
        //int ii = publicDbOpClass.ExecSqlString("INSERT OA_Journal_Comment (journal_id,user_id,time,Id,content) VALUES('" + id + "', '" + userID + "', '" + DateTime.Now.ToString() + "', '" + Guid.NewGuid() + "', '" + pl + "')");
        //return ii.ToString();
    }
    [WebMethod]//标示为web服务方法属性
    //test 
    public static void transcoding()
    {
        string str1 = HttpContext.Current.Server.MapPath("/");
        string str3 = "\\bin\\ffmpeg.exe"; //"9247d6bf-7f22-4149-b09e-6879e7775562";// diaryId;
        string paths = str1.Substring(0, str1.Length - 1).Replace("\\", "/")  + str3;


        string pathBefore = "D:\\VOICE_001.amr";
        //WXAPI.uploadVoice(fileName);
        string pathLater = "D:\\VOICE_001.mp3";
        WavConvertToAmr toamr = new WavConvertToAmr();
        toamr.ConvertToMp3(paths, pathBefore, pathLater);
        //MessageBox.Show("转换成功");
    }

    [WebMethod]//标示为web服务方法属性
    public static string CheckClashTime(string KeyId, string userCode, string LogType, string sDate, string eDate)
    {
        string strwhere = " and 1=1";
        if (LogType == "edit")
        {
            strwhere = " and Id !='" + KeyId + "'";
        }
        else
        {
            strwhere = " and 1=1";
        }
        string str = @"SELECT [start_time],[end_time],[creater],SUBSTRING(title,0,50) title,SUBSTRING(content,0,50) content 
                           FROM  [OA_Journal] 
                           WHERE [creater]='" + userCode + "' and ( ( [start_time]  BETWEEN '" + sDate + "' and '" + eDate + "' ) or ( [start_time]<= '" + sDate + "' and '" + eDate + "'<=[end_time]) or ( [start_time]<= '" + sDate + "'  and [end_time] BETWEEN '" + sDate + "' and '" + eDate + "') ) " + strwhere + " ";
        DataSet ds = publicDbOpClass.DataSetQuary(str);
        string ss = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string strB = dr["title"].ToString();
                if (strB.Length >= 25)
                {
                    strB += "……";
                }
                else
                {
                    strB += "";
                }
                string strC = dr["content"].ToString();
                if (strC.Length >= 25)
                {
                    strC += "……";
                }
                else
                {
                    strC += "";
                }//日志内容:" + strC + "
                TimeSpan timeSpan = Convert.ToDateTime(dr["end_time"].ToString()) - Convert.ToDateTime(dr["start_time"].ToString());
                ss += "[&nbsp;" + Convert.ToDateTime(dr["start_time"].ToString()).ToString("yyyy-MM-dd HH:mm") + "-" + Convert.ToDateTime(dr["end_time"].ToString()).ToString("HH:mm") + "&nbsp;持续:" + timeSpan.TotalMinutes.ToString() + " 分钟&nbsp;&nbsp;]&nbsp;日志标题:" + strB + "&nbsp;</br>";
            }
        }
        else
        {
            ss = "";
        }

        // string str = ds.Tables[0].Rows.Count;
        return ss;
    }

    [WebMethod]//标示为web服务方法属性
    public static string getResourcesByZZCL(string ids)
    {
        string str = @"<tr class='header'>
                        <th scope='col' style='width: 25px;'>序号
                        </th>
                        <th scope='col'>编码
                        </th>
                        <th scope='col'>名称
                        </th>
                          
                        <th scope='col'>规格
                        </th>
                        <th scope='col'>型号
                        </th>
                        <th scope='col'>计量单位
                        </th>
                        
                        <th scope='col'>品牌
                        </th>
                        <th scope='col'>技术参数
                        </th>
                        <th scope='col'>供应商
                        </th>
                        <th scope='col'>数量
                        </th>
                    </tr>";
        DataTable dt = getResources(ids, "ajax");
        if (dt !=null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                str += string.Format(@"<tr id='{0}' code='{2}'>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td><span class='tooltip' title=''>{3}</span></td>
                                    <td><span class='tooltip ' title=''>{4}</span></td>
                                    <td><span class='tooltip' title=''>{5}</span></td>
                                    <td>{6}</td>
                                    <td><span class='tooltip' title=''>{7}</span></td>
                                    <td><span class='tooltip' title=''>{8}</span></td>
                                    <td><span class='tooltip' title=''>{9}</span></td>
                                    <td>{10}</td>
                                    </tr>", dr["ResourceId"].ToString(), dr["row_number"].ToString(), dr["ResourceCode"].ToString(),
                                        dr["ResourceName"].ToString(), dr["Specification"].ToString(), dr["ModelNumber"].ToString(),
                                        dr["Name"].ToString(), dr["Brand"].ToString(), dr["TechnicalParameter"].ToString(), dr["CorpName"].ToString(), dr["number"].ToString());
            }
        }
        return str;
    }

    public static DataTable getResources(string ids, string type)
    {
        string strIDs = ids;
        DataTable dataTable = null;
        //["4b0ae68c-ff0b-403c-bcd5-39a0d07a7ee0|000400002|99","82eddac8-0546-447f-b96b-eacd75d7d9f1|000400001|66"]
        if (!string.IsNullOrEmpty(strIDs))
        {
            //b89b1b0c - 6d3b - 4b37 - a5f1 - eafdd81f0acb | 000400005 | 1,a7ca9325 - 91dd - 4026 - 9dd6 - fbd4482c81a8 | 000400002 | 1,563c0bc1 - f223 - 4db9 - 928d - 2177e1d1e0c0 | 000400004 | 1
            //ISerializable serializable = new cn.justwin.Serialize.JsonSerializer();
            //string[] array = serializable.Deserialize<string[]>(strIDs);
            string[] array = strIDs.Split(','); ;
            //4b0ae68c - ff0b - 403c - bcd5 - 39a0d07a7ee0 | 000400002 | 99
            //82eddac8 - 0546 - 447f - b96b - eacd75d7d9f1 | 000400001 | 66
            string[] array2 = null;
            if (array != null)
            {
                string strIDS = string.Empty;
                foreach (string str in array)
                {
                    strIDS += "'" + str.Split('|')[0].ToString().Trim() + "',";
                }
                string strSQL = "select FID,CID,NUM from Res_ResourceRelation where FID in (" + strIDS.Substring(0, strIDS.Length - 1) + ")";
                DataTable dt = publicDbOpClass.DataTableQuary(strSQL);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (string str in array)
                        {
                            string strID = str.Split('|')[0].ToString();
                            string strNUM = str.Split('|')[2].ToString();
                            if (dr["FID"].ToString() == strID)
                            {
                                try
                                {
                                    dr["NUM"] = Convert.ToString(Convert.ToDouble(dr["NUM"].ToString().Trim()) * Convert.ToDouble(strNUM));
                                }
                                catch
                                {

                                }

                            }
                        }
                    }

                    array2 = new string[dt.Rows.Count];
                    int ii = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        array2[ii] = dr["CID"].ToString().Trim();
                        ii++;
                    }
                    string str2 = "select CID from Res_ResourceRelation where FID in (" + strIDS.Substring(0, strIDS.Length - 1) + ")";
                    string strSql2 = @"SELECT distinct ROW_NUMBER() OVER (ORDER BY ResourceCode  DESC) AS row_number,0 number,Res_Resource.*,Name--,ISNULL(PriceValue,0) price
                                , ISNULL(CorpName,'') CorpName FROM Res_Resource
                                 LEFT JOIN Res_Unit ON Res_Unit.UnitId = Res_Resource.Unit
                                 --INNER JOIN cte_Resource ON cte_Resource.ResourceTypeId = Res_Resource.ResourceType
                                LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId
                                left join XPM_Basic_ContactCorp on Res_Resource.SupplierId = XPM_Basic_ContactCorp.CorpID
                                WHERE 1 = 1 and Res_Resource.ResourceId in(" + str2 + ")";
                     dataTable = publicDbOpClass.DataTableQuary(strSql2);

                    if (dt != null && dt.Rows.Count > 0 && dataTable != null && dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow drA in dataTable.Rows)
                        {
                            foreach (DataRow drB in dt.Rows)
                            {
                                string number = drA["number"].ToString();
                                string ResourceId = drA["ResourceId"].ToString();

                                string NUM = drB["NUM"].ToString();
                                string CID = drB["CID"].ToString();
                                if (ResourceId == CID)
                                {
                                    try
                                    {
                                        double db = Convert.ToDouble(drA["number"].ToString());
                                        db += Convert.ToDouble(NUM);
                                        drA["number"] = db.ToString();
                                    }
                                    catch
                                    {

                                    }

                                }
                            }
                        }
                    }

                }
            }
          
        }
        return dataTable;
    }
}