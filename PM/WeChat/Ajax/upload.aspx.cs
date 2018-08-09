﻿using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WeChat_Ajax_upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         string type = System.Web.HttpContext.Current.Request.QueryString["type"].ToString();//PathWithQueryString;
        string strId2 = System.Web.HttpContext.Current.Request.QueryString["id"].ToString();//PathWithQueryString;
        //string strName = System.Web.HttpContext.Current.Request.Form.Get("name");
        HttpFileCollection files = Request.Files;//这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型
        HttpContext context = HttpContext.Current;
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";
        HttpPostedFile httpPostedFile = files[0];//context.Request.Files["Filedata"];
        string msg = string.Empty;
        string status = "true";
        string imgurl;
        //ConfigHelper.Get("JournalPhotos");
        //ConfigHelper.Get("JournalFiles"); 
        //string str = context.Request["folder"];
        //E:\001.WORK\001.projects\001.工程项目管理系统(DX0316)\pm.src\PM\/UploadFiles/JournalFiles/\
       // string text = HttpContext.Current.Server.MapPath("/") +  + "\\";
        string str1 = HttpContext.Current.Server.MapPath("/");
        string str2 = "";//ConfigHelper.Get("JournalVoices");

        //if (type == "JournalFiles")
        //{
        //    str2 = "/UploadFiles/JournalFiles/";//ConfigHelper.Get("JournalVoices");
        //}
        //if (type == "JournalVideos")
        //{
        //    str2 = "/UploadFiles/JournalVideos/";//ConfigHelper.Get("JournalVoices");
        //}

        //if (type == "TaskFiles")
        //{
        //    str2 = "/UploadFiles/TaskFiles/";
        //}
        //if (type == "TaskVoices")
        //{
        //    str2 = "/UploadFiles/TaskVoices/";
        //}
        // string str2 = "/UploadFiles/JournalFiles/"; //ConfigHelper.Get("JournalFiles");
        str2 = "/UploadFiles/"+ type + "/";
        string str3 = strId2;
        string text = str1.Substring(0,str1.Length-1).Replace("\\", "/") + str2 + str3 + "/";
        string text2 = str2 + str3 + "/";
        if (httpPostedFile != null || files.Count > 0)
        {
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            httpPostedFile.SaveAs(text + files[0].FileName);
            msg = " 成功! 文件大小为:" + files[0].ContentLength;
            imgurl = "/" + files[0].FileName;
            //string res = "{ "error":'" + error + "', msg:'" + msg + "',imgurl:'" + imgurl + "'}";
            string res= "[{\"status\":\"" + status + "\",\"name\":\"" + files[0].FileName + "\",\"path\":\"" + text2 + "\",\"size\":\"" + files[0].ContentLength + "\"}]";
            Response.Write(res);
            Response.End();
        }
    }
}