﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newedit.aspx.cs" Inherits="WeChat_log_newedit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>工作日志</title>
    <meta name="description" content="" />
    <meta name="HandheldFriendly" content="True" />
    <meta name="MobileOptimized" content="320" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="telephone=no" name="format-detection" />
    <meta content="email=no" name="format-detection" />


    <link rel="stylesheet" href="../resource/css/ku.css" />
    <link rel="stylesheet" href="../resource/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../resource/css/light7.min.css"/>
    <link rel="stylesheet" href="../resource/css/light7-swiper.min.css"/>
    <script src="../resource/js/jquery-12.4.min.js"></script>
    <script src="../resource/js/selectUsers.js"></script>
    <script src="../resource/js/jquery.ui.widget.js"></script>
    <script src="../resource/js/jquery.iframe-transport.js"></script>
    <script src="../resource/js/jquery.fileupload.js"></script>
    <script src="../resource/js/jquery.xdr-transport.js"></script>
    <script src="../resource/js/ajaxfileupload.js"></script>
    <script src="../resource/js/guid.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
</head>
<body>
    <form id="form1" runat="server">
<div id="wrap_main" class="wrap page-group">
        <div id="main" class="wrap_inner page" style="">
            <div id="isUnSubmit" class="topWarn" style="display: none;text-align: center;">
                <div class="topWarn-text">
                    你有日志未提交，点击前往补交
                </div>
            </div>
            <!--<form action="" id="taskform" onsubmit="return false;">-->
            <input type="hidden" name="tbQyDiaryPO.currentDay" id="currentDate" />
            <!--日志类型部分-->
            <div class="f-item f-item-select">
                <div class="inner-f-item item-text flexbox">
                    <span class="f-item-title">日志类型</span>
                    <div class="flexItem">
                        <select name="type_id" id="type_id" onchange="changeVal()" class="flexItem item-select direction_rtl">
                            <!--<option value="">请选择</option>
                            <option value="">日报</option>
                            <option value="">周报</option>
                            <option value="">月报</option>
                            <option value="">工作总结</option>-->
                        </select>
                    </div>
                </div>
            </div>
            <!--<div onclick="transcoding()">测试按钮</div>-->
            <!--日志类型部分-->
            <input type="text" value="" id="KeyID" style="display:none;" />
            <input type="text" value="" id="UserID" style="display:none;" />
            <div class="detaildata">
                <!--日志标题部分-->
                <div class="f-item">
                    <div class="inner-f-item">
                        <div class="flexItem" style="padding-right: 40px;">
                            <input type="text" name="title" value="" id="title" placeholder="请输入日志标题" class="item-select inputStyle item-title" />
                        </div>
                    </div>
                </div>
                <!--日志标题部分-->
                <!--时间选择部分-->
                <div class="f-item">
                    <div class="inner-f-item">
                        <div class="flexItem" style="padding-right: 40px;">
                            <input type="text" name="start_time" value="" id="start_time" placeholder="请选择开始时间"
                                   class="item-select inputStyle item-title" style="width: 50%" />
                            <select id="minSelect" class="flexItem item-select direction_rtl" style="width: 20%;float: right">
                                <option value="0">分钟</option>
                                <option value="30">30分钟(0.5小时)</option>
                                <option value="60">60分钟(1小时)</option>
                                <option value="90">90分钟(1.5小时)</option>
                                <option value="120">120分钟(2小时)</option>
                                <option value="150">150分钟(2.5小时)</option>
                                <option value="180">180分钟(3小时)</option>
                                <option value="210">210分钟(3.5小时)</option>
                                <option value="240">240分钟(4小时)</option>
                                <option value="270">270分钟(4.5小时)</option>
                                <option value="300">300分钟(5小时)</option>
                                <option value="330">330分钟(5.5小时)</option>
                                <option value="360">360分钟(6小时)</option>
                                <option value="390">390分钟(6.5小时)</option>
                                <option value="420">420分钟(7小时)</option>
                                <option value="450">450分钟(7.5小时)</option>
                                <option value="480">480分钟(8小时)</option>
                            </select>

                            <input type="text" value="60" id="cxsj" placeholder="持续"
                                   class="item-select inputStyle item-title" style="width: 30%;float: right;text-align: right" />
                        </div>
                    </div>
                </div>
                <div class="f-item">
                    <div class="inner-f-item">
                        <div class="flexItem" style="padding-right: 40px;">
                            <input type="text" name="end_time" value="" id="end_time" placeholder="请选择结束时间"
                                   class="item-select inputStyle item-title" style="width: 70%" />
                        </div>
                    </div>
                </div>
                <!--时间选择部分-->
                <!--<a onclick="voiceTest()">voice测试</a>
                 <a onclick="imgsTest()">imgs测试</a>-->
                <!--日志内容部分-->
                <div class="f-item">
                    <div class="inner-f-item tapeBox">
                        <div class="text_div">
                            <textarea class="item-select inputStyle item-content" name="content" id="content" cols="30" rows="2" placeholder="请输入日志内容" style="height: 42px;"></textarea>
                        </div>
                        <div class="fr">
                            <!--<div class="icon-circular mt5 mr10" id="showActionSheet">-->
                            <!--<i class="icon-circular-ic icon-circular-book"></i>-->
                            <!--</div>-->
                            <!--<div class="tape" id="voiceIds" style="display: block;"></div>-->
                        </div>
                    </div>
                </div>
                <!--日志内容部分-->
                <!--上传图片部分-->
                <div class="f-item">
                    <div class="loadImg clearfix">
                        <div class="f-add-user-list" style="margin: 0;">
                            <ul id="addImg" class="clearfix1">
                                <li class="f-user-add" onclick="chooseImg()"></li>
                                <li class="f-user-remove" onclick="removeImg()"></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!--上传图片部分-->
                <!--上传音频部分-->
                <div class="f-item">
                    <div class="loadImg clearfix">
                        <div class="f-add-user-list" style="margin: 0;">
                            <ul id="addVoice" class="clearfix1">
                                <li id="voiceLi">
                                    <img src="../resource/images/voice.png"
                                         style="margin-left:10px;width: 42px;height: 42px;" onclick="startVoice()">
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!--浮动窗口-->
                <div class="overlay" id="iosMask" style="display: none;"></div>
                <div class="popBox1 tapePop" style="display: none;">
                    <div class="popBox_title"></div>
                    <div class="popBox1_con tcenter">
                        <img src="../resource/images/tapePlay.gif">
                        <div class="tapeTime">01:00</div>
                    </div>
                    <div class="popBox_error"></div>
                    <div class="popBox_foot flexbox">
                        <a class="popBox_cancel_btn flexItem" onclick="stopVoice(0)">取消</a>
                        <a class="popBox_submit_btn flexItem" onclick="stopVoice(1)">说完了</a>
                    </div>
                </div>
                <!--浮动窗口-->
                <!--上传音频部分-->
                <!--上传视频部分-->
                <div class="f-item">
                    <div class="loadImg clearfix">
                        <div class="f-add-user-list" style="margin: 0;">
                            <ul id="addvideo" class="clearfix1">
                                <li>
                                    <img src="../resource/images/video.png"
                                         style="margin-left:10px;width: 42px;height: 42px;" onclick="addVideo()">
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div style="margin-top:10px;display:none" id="sp">
                    <!--<form action="" name="" enctype="multipart/form-data">-->
                    <!--</form>-->
                </div>
                <!-- 上传进度条及状态： -->
                <div class="progress">
                    <div class="bar" style="width: 0%;display: none;"></div>
                    <div class="upstatus" style="margin-top:10px;"></div>
                </div>
                <!-- 预览框： -->
                <div class="preview"></div>
                <!--上传视频部分-->
                <!--上传附件部分-->
                <div class="form-style" id="medialist">
                    <div class="letter_bar file_top borderBottommNone">
                        <span class="file_top_tit">附件</span>
                        <span class="file_top_btn" onclick="addFile()">
                            <!--<input type="file" name="file" id="files" class="upload_file_input"/>-->
                            <i>+</i>添加
                        </span>
                        <span id="fileInput" style="display: none">
                        </span>
                    </div>
                </div>
                <div class="settings-item" id="fj">
                    <!--<div class="inner-settings-item flexbox fujian">-->
                    <!--<p class="ZIP"></p>-->
                    <!--<div class="fujian_text flexItem">-->
                    <!--<p class="name">工作任务.zip</p>-->
                    <!--<p class="fujian_size">611.39 K</p>-->
                    <!--<p class="arrow">-->
                    <!--<span class="wrap" onclick="$(this).parent().parent().parent().remove()" style="margin-top: 11px">-->
                    <!--<i class="qw-operate-icon qw-operate-icon-del"></i>-->
                    <!--</span>-->
                    <!--</p>-->
                    <!--</div>-->
                    <!--</div>-->
                </div>
                <div class="separate_bar separate_bar_h15"></div>
                <!--上传附件部分-->
                <!--选择审阅人-->
                <!--标题部分-->
                <div class="letter_bar">
                    审阅人  <div style="width: 100px; float: right;font-size: 14px;">
                        加载上次
                        <div class="loadlast_onoff">
                            <!--加载上次-->
                            <div class="onOff" id="onOff2">
                                <span class="onOff_off">
                                    <input type="hidden" name="toSelectId" id="toSelectId" value="0" />
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <!--标题部分-->
                <!--数据部分-->
                <div class="f-item">
                    <div class="f-add-user clearfix">
                        <div class="inner-f-add-user">
                            <div class="f-add-user-list">
                                <ul class="clearfix" id="syrUl">
                                    <!--<li>-->
                                    <!--<a class="remove_icon" style="display: inline;"></a>-->
                                    <!--<p class="img">-->
                                    <!--<img src="http://p.qlogo.cn/bizmail/uqibPEIicNrjEErw2dcgln6868Micl38EWF1VfWlQOcHgQUEJE2D26lmA/100" alt="">-->
                                    <!--</p>-->
                                    <!--<p class="name">潘全全</p>-->
                                    <!--</li>-->
                                    <!--<li><a class="remove_icon" style="display: inline;"></a>-->
                                    <!--<p class="img">-->
                                    <!--<img src="http://p.qlogo.cn/bizmail/vLo4ibTPak84PTBYO5yIkicNL3ftwbV3oibCvYTQqWVfPicicYyiaQLYiakYQ/100" alt="">-->
                                    <!--</p>-->
                                    <!--<p class="name">潘翔伟</p></li>-->
                                    <!--<li>-->
                                    <!--<a class="remove_icon" style="display: inline;"></a>-->
                                    <!--<p class="img">-->
                                    <!--<img src="http://p.qlogo.cn/bizmail/ibvzGOTWvkhKyViabceZGyMC5WoJccbqrxWMZ02Y6RwEBp6DhH2Aejbg/100" alt="">-->
                                    <!--</p>-->
                                    <!--<p class="name">雲潇</p>-->
                                    <!--</li>-->
                                    <li class="ico-add" onclick="selectUsers('syr')"></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!--数据部分-->
                <!--选择审阅人-->
                <!--选择相关人-->
                <!--标题部分-->
                <div class="letter_bar">
                    相关人
                    <div style="width: 100px; float: right;font-size: 14px;">
                        加载上次
                        <div class="loadlast_onoff">
                            <!--加载上次-->
                            <div class="onOff" id="onOff3">
                                <span class="onOff_off"><input type="hidden" name="ccSelectId" id="ccSelectId" value="0" /></span>
                            </div>
                        </div>
                    </div>
                </div>
                <!--标题部分-->
                <!--数据部分-->
                <div class="f-item">
                    <div class="f-add-user clearfix">
                        <div class="inner-f-add-user">
                            <div id="ccPersonList" class="f-add-user-list">
                                <ul class="clearfix" id="xgrUl">
                                    <!--<li>-->
                                    <!--<a class="remove_icon" style="display: inline;"></a>-->
                                    <!--<p class="img">-->
                                    <!--<img src="http://p.qlogo.cn/bizmail/uqibPEIicNrjEErw2dcgln6868Micl38EWF1VfWlQOcHgQUEJE2D26lmA/100" alt="">-->
                                    <!--</p>-->
                                    <!--<p class="name">潘全全</p>-->
                                    <!--</li>-->
                                    <li class="ico-add" onclick="selectUsers('xgr')"></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!--数据部分-->
                <!--选择相关人-->
                <!--关联项目部分-->
                <div class="f-item f-item-select">
                    <div class="inner-f-item item-text flexbox">
                        <span class="f-item-title">关联项目</span>
                        <div class="flexItem">
                            <select name="project_id" id="project_id" class="flexItem item-select direction_rtl">
                                <!--<option value="">请选择</option>
                                    <option value="">日报</option>
                                  <option value="">周报</option>
                                  <option value="">月报</option>
                                  <option value="">工作总结</option>-->
                            </select>
                        </div>
                    </div>
                </div>
                <!--关联项目部分-->
                <!--关联任务部分-->
                <div class="f-item f-item-select">
                    <div class="inner-f-item item-text flexbox">
                        <span class="f-item-title">关联任务</span>
                        <div class="flexItem">
                            <select name="task_id" id="task_id" class="flexItem item-select direction_rtl">
                                <!--<option value="">请选择</option>
                                  <option value="">日报</option>
                                 <option value="">周报</option>
                                 <option value="">月报</option>
                                 <option value="">工作总结</option>-->
                            </select>
                        </div>
                    </div>
                </div>
                <!--关联任务部分-->
                <input type="hidden" id="GUID" value="0" />
                <div class="form_btns mt10">
                    <div class="inner_form_btns">
                        <div class="fbtns flexbox">
                            <input type="hidden" name="tbQyDiaryPO.diaryStatus" id="taskStatus" value="" />
                            <a class="fbtn btn qwui-btn_default flexItem" style="margin-right: 5px;" onclick="commitTask('0')"> <!--href="javascript:commitTask('0')"-->保存为草稿</a>
                            <a class="fbtn btn flexItem" style="margin-left: 5px;color:white" onclick="commitTask('1')"><!--href="javascript:commitTask('1')"-->保存并提交</a>
                        </div>
                        <div class="fbtns_desc"></div>
                    </div>
                </div>
            </div>
            <!--</form>-->


        </div>
        <!--引入选择人员部分-->
        <div id="usersSelect" style="display: none">
            <!--<tc_include file="Monitor:selectUser"/>-->
            <script type="text/javascript">$('#usersSelect').load('selectUsers.html');</script>
        </div>

        <!--引入选择人员部分-->
    </div>
            <script type='text/javascript' src='../resource/js/light7.min.js' charset='utf-8'></script>
    <script type='text/javascript' src='../resource/js/light7-swiper.min.js' charset='utf-8'></script>
            <script>





        //    日期时间控件(timeStr为触发时间控件的标签id)
        $("#start_time").datetimePicker({
            toolbarTemplate: '<header class="bar bar-nav">\
    <button class="button button-link pull-right close-picker" >确定</button>\
    <h1 class="title">选择日期和时间</h1>\
    </header>'
        });
//        onclick="startChange()"
        $("#end_time").datetimePicker({
            toolbarTemplate: '<header class="bar bar-nav">\
    <button class="button button-link pull-right close-picker">确定</button>\
    <h1 class="title">选择日期和时间</h1>\
    </header>'
        });
        //日期时间控件
        $("#minSelect").change(function () {
            var min = $(this).val();
            $("#cxsj").val(min);
            $("#minSelect option[value='0']").prop("selected", true);
            var nowTime = $("#start_time").val();
            if (nowTime) {
                $("#end_time").val(shijianzhuanhuan(nowTime, min));
            }
        });
        function startChange() {
            var nowTime = $("#start_time").val();
            var min = $("#cxsj").val();
            if (!min) { min = 0; }
            $("#end_time").val(shijianzhuanhuan(nowTime, min));
        }
        $('#cxsj').bind('input propertychange', function () {
            var nowTime = $("#start_time").val();
            var min = $("#cxsj").val();
            if (!min) { min = 0; }
            $("#end_time").val(shijianzhuanhuan(nowTime, min));
        });
        var now = getNowFormatDate();
        $("#start_time").val(now);
        //时间加减运算
        // 获取当前时间戳(以s为单位)
        var timestamp = Date.parse(new Date());
        timestamp = timestamp / 1000;
        timestamp += 60 * 60;
        var newDate = new Date();
        newDate.setTime(timestamp * 1000);
        Date.prototype.format = function (format) {
            var date = {
                "M+": this.getMonth() + 1,
                "d+": this.getDate(),
                "h+": pad(this.getHours(), 2),
                "m+": pad(this.getMinutes(), 2),
                "s+": this.getSeconds(),
                "q+": Math.floor((this.getMonth() + 3) / 3),
                "S+": this.getMilliseconds()
            };
            if (/(y+)/i.test(format)) {
                format = format.replace(RegExp.$1, (this.getFullYear() + '').substr(4 - RegExp.$1.length));
            }
            for (var k in date) {
                if (new RegExp("(" + k + ")").test(format)) {
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1
                        ? date[k] : ("00" + date[k]).substr(("" + date[k]).length));
                }
            }
            return format;
        };
        var s = (newDate.format("yyyy-MM-dd h:m"));
        $("#end_time").val(s);
        //时间加减运算
        //获取当前系统时间
        function getNowFormatDate() {
            var date = new Date();
            var seperator1 = "-";
            var seperator2 = ":";
            var month = date.getMonth() + 1;
            var strDate = date.getDate();
            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }
            var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
                + " " + pad(date.getHours(), 2) + seperator2 + pad(date.getMinutes(), 2);
            return currentdate;
        }
        function pad(num, n) {
            return Array(n > num ? (n - ('' + num).length + 1) : 0).join(0) + num;
        }
        function shijianzhuanhuan(nowTime, min) {
            var timestamp2 = Date.parse(new Date(nowTime)) / 1000;
            timestamp2 += min * 60;
            var newDate = new Date();
            newDate.setTime(timestamp2 * 1000);
            Date.prototype.format = function (format) {
                var date = {
                    "M+": this.getMonth() + 1,
                    "d+": this.getDate(),
                    "h+": pad(this.getHours(), 2),
                    "m+": pad(this.getMinutes(), 2),
                    "s+": this.getSeconds(),
                    "q+": Math.floor((this.getMonth() + 3) / 3),
                    "S+": this.getMilliseconds()
                };
                if (/(y+)/i.test(format)) {
                    format = format.replace(RegExp.$1, (this.getFullYear() + '').substr(4 - RegExp.$1.length));
                }
                for (var k in date) {
                    if (new RegExp("(" + k + ")").test(format)) {
                        format = format.replace(RegExp.$1, RegExp.$1.length == 1
                            ? date[k] : ("00" + date[k]).substr(("" + date[k]).length));
                    }
                }
                return format;
            };
            var s1 = (newDate.format("yyyy-MM-dd hh:mm"));
            return s1;
        }
    </script>


    <script>
        //$.showPreloader('加载中...');
        var UserID = "";
        $(document).ready(function () {
            $.showPreloader('加载中...');
            //获取url中的id参数
            var diaryId = GetQueryString("id");
            if (diaryId) {
                getTypes(diaryId);//获取日志类型信息
            }
            else//新增
            {
                getTypes("");//获取日志类型信息
            }
            UserID = getUserIdBycode(); //"00300005";//
            if (typeof (UserID) == "undefined") {
                alert("无法获取用户信息,请重新加载!");
                return;
                //UserID = getUserIdBycode();
                //if (typeof (UserID) == "undefined") {
                //    location.reload();
                //} else {
                //    if (UserID == 'no_user') {
                //        alert("无法获取用户信息,请联系管理员进行人员同步!");
                //        return
                //    }
                //}
            } else {
                if (UserID == 'no_user') {
                    alert("无法获取用户信息,请联系管理员进行人员同步!");
                    return
                }
            }
            getDepts();//获取部门信息
            getUsers();//获取人员信息
            getJSSDK()//获取jssdk

            getTask_id();//获取关联任务信息
          
            
            //此页面为update页面
            if (diaryId) {
                getDataById(GetQueryString("id"));//根据ID获取基本数据
                getUsersById(GetQueryString("id"), 'syr');//根据日志ID获取审阅人
                getUsersById(GetQueryString("id"), 'xgr');//根据日志ID获取相关人
                //getTypes(diaryId);//获取日志类型信息
                getSrcFiles(diaryId, "JournalPhotos");//获取 工作日志图片
                getSrcFiles(diaryId, "JournalFiles");//获取 工作日志附件
                getSrcFiles(diaryId, "JournalVoices");//获取 工作日志语音
                getSrcFiles(diaryId, "JournalVideos");//获取 工作日志视频
                document.getElementById("KeyID").value = diaryId;
            }
            else//新增
            {
                //getTypes("");//获取日志类型信息
                var guid = new GUID();
                document.getElementById("KeyID").value = guid.newGUID();
            }
            getProject_id();//获取关联项目信息
        });

        var audio = null;

        function editPlay(e) {
            if (audio) {
                audio.currentTime = 0;
                audio.pause();
                $("img[name='editVoice']").each(function () {
                    $(this).attr("src", "../resource/images/voiceFile.png");
                });
                audio = null;
            } else {
                audio = document.createElement("audio");
                audio.src = e.attr("imgSrc");
                e.attr("src", "../resource/images/playVoice.gif");
                audio.play();
            }
        }

        function getSrcFiles(diaryId, type) {
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/getSrcFiles",
                data: "{diaryId: '" + diaryId + "',type: '" + type + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {

                    if (data.d != "") {
                        var res = eval("(" + data.d + ")");

                        if (type == 'JournalPhotos') {
                            var addHtml = "";
                            for (var i = 0; i < res.length; i++) {
                                addHtml += "<li>" +
                                "<a class=\"remove_icon\" name='removeImgA' onclick=\"deleteAllFiles('" + res[i].Path + res[i].Name + "','JournalPhotos',$(this))\"  bdId=\"\" style=\"display: none;\"></a>" +
                                "<p class=\"img\">" +
                                "<img name=\"toUpLoadImg\" src=\"" + res[i].Path + res[i].Name + "\" alt=\"\">" +
                                "</p>" +
                                "</li>";

                            }
                            $("#addImg").prepend(addHtml);
                        }
                        if (type == 'JournalFiles') {
                            var addHtml = "";
                            for (var i = 0; i < res.length; i++) {
                                addHtml += "<div class=\"inner-settings-item flexbox fujian\">" +
                                               // "<p class=\""+cl+"\"></p>" +
                                                "<div class=\"fujian_text flexItem\">" +
                                                "<p class=\"name\">" + res[i].Name + "</p>" +
                                                "<p class=\"fujian_size\">" + res[i].Length + " K</p>" +
                                                "<p class=\"arrow\">" +
                                                "<span class=\"wrap\"  onclick=\"deleteAllFiles('" + res[i].Path + res[i].Name + "','JournalFiles',$(this))\" style=\"margin-top: 11px\">" +
                                                "<i class=\"qw-operate-icon qw-operate-icon-del\"></i>" +
                                                "</span>" +
                                                "</p>" +
                                                "</div>" +
                                                "</div>";
                            }

                            $("#fj").prepend(addHtml);
                        }
                        if (type == 'JournalVoices') {
                            var addHtml = "";
                            for (var i = 0; i < res.length; i++) {
                                addHtml = "<li>" +
                                               "<a class=\"remove_icon\" onclick=\"deleteAllFiles('" + res[i].Path + res[i].Name + "','JournalVoices,$(this)')\" bdvId=\"\"   style=\"display: block;\"></a>" +
                                               "<p class=\"img\">" +
                                               "<img name='editVoice' src=\"../resource/images/voiceFile.png\" imgSrc='" + res[i].Path + res[i].Name + "'  style=\"margin-left:10px;width: 42px;height: 42px;\" onclick='editPlay($(this))'>" +
                                               "</p>" +
                                           "</li>";
                                $("#addVoice").prepend(addHtml);
                            }
                        }
                        if (type == 'JournalVideos') {
                            var addHtml = "";
                            for (var i = 0; i < res.length; i++) {
                                $(".preview").append("<div style='margin-top:10px;'>" +
                                    "<video src='" + res[i].Path + res[i].Name + "' controls='controls' width='70' height='50'>" +
                             "</video></div>");
                            }
                        }
                    }
                    else {
                        //allusers = [];
                    }
                }
            });
        }
        function getUserIdBycode() {
            var type = GetQueryString("action");
            if (type == "edit") {
                return GetQueryString("userID");
            }
            else if (type == "add") {
                var code = GetQueryString("code");
                var UserId = getCookieValue("UserId");
                //alert(GetQueryString("userID"));
                if (UserId == "" || UserId == null || typeof (UserId) == "undefined") {
                    $.ajax({
                        type: "POST",
                        url: "../Ajax/AjaxGetMsg.aspx/getUserIdBycode",
                        data: "{code: '" + code + "',AgentId: '1000008'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        success: function Success(data) {
                            if (data.d != "") {
                                return data.d;
                                //$("textarea[name='content']").html(data.d);//内容
                            }
                            else {
                                //alldepts=[];
                            }
                        }
                    });
                } else {
                    return UserId;
                }
            }
        }
        //获取cookie
        function getCookieValue(cookieName) {
            var cookieValue = document.cookie;
            var cookieStartAt = cookieValue.indexOf("" + cookieName + "=");
            if (cookieStartAt == -1) {
                cookieStartAt = cookieValue.indexOf(cookieName + "=");
            }
            if (cookieStartAt == -1) {
                cookieValue = null;
            }
            else {
                cookieStartAt = cookieValue.indexOf("=", cookieStartAt) + 1;
                cookieEndAt = cookieValue.indexOf(";", cookieStartAt);
                if (cookieEndAt == -1) {
                    cookieEndAt = cookieValue.length;
                }
                cookieValue = unescape(cookieValue.substring(cookieStartAt, cookieEndAt));//解码latin-1
            }
            return cookieValue;
        }
        function getProject_id() {
            var temp = document.getElementById("KeyID").value;
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/getProject_id",
                data: "{diaryId: '" + temp + "',userID: '" + UserID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        document.getElementById("project_id").innerHTML = data.d;
                    }
                    else {
                        //alldepts=[];
                    }
                }
            });
        }
        function transcoding() {
            var temp = document.getElementById("KeyID").value;
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/transcoding",
                //data: "{diaryId: '" + temp + "',userID: '" + UserID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        //document.getElementById("project_id").innerHTML = data.d;
                    }
                    else {
                        //alldepts=[];
                    }
                }
            });
        }
        function getTask_id() {
            var temp = document.getElementById("KeyID").value;
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/getTask_id",
                data: "{Id: '" + temp + "',userID: '" + UserID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        document.getElementById("task_id").innerHTML = data.d;
                    }
                    else {
                        //alldepts=[];
                    }
                }
            });
        }

        function getUsersById(Id, userType) {
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/getUsersById",
                data: "{Id: '" + Id + "',userType: '" + userType + "',mk: 'log'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        checkedAllUsers = eval("(" + data.d + ")");
                        selectType = userType;
                        okChecked();
                    }
                    else {

                    }
                }
            });
        }
        //加载 上次 审阅人or相关人  type syr:审阅人；xgr相关人
        function GetBeforeUsers(userType) {
            checkedAllUsers = [];
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/GetBeforeUsers",
                data: "{type: '" + userType + "',userID: '" + UserID + "',mk: 'log'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        checkedAllUsers = eval("(" + data.d + ")");
                        selectType = userType;
                        okChecked();
                    }
                    else {
                        //alldepts=[];
                    }
                }
            });
        }
        function getTypes(diaryId) {
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/getTypes",
                data: "{diaryId: '" + diaryId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function Success(data) {
                    if (data.d != "") {
                        document.getElementById("type_id").innerHTML = data.d;
                        $.hidePreloader();
                    }
                    else {
                        //alldepts=[];
                        $.hidePreloader();
                    }
                }
            });
        }
        function changeVal() {
            //var temp = $('#taskType option:selected').text();//选中的文本
            var type_id = $('#type_id option:selected').val();//选中的值
            //var temp = $("#taskType ").get(0).selectedIndex;//索引
            if (type_id == "0") {
                return false;
            }
            if (document.getElementById('title').value == "" && document.getElementById('content').value == "") {
                getTemps(type_id);//获取日志类型模版内容
            }
            else {
                if (confirm('是否用\'日志模版\'替换当前的\'标题\'和\'内容\'?')) {
                    getTemps(type_id);//获取日志类型模版内容
                }
            }
            //if (confirm('是否用日志模版替换已填写的标题和内容?')) {

            //}
            return false;
        }
        function getTemps(type_id) {
            $.showPreloader("加载中");
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/getTemps",
                data: "{typeId: '" + type_id + "',mk: 'log'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        document.getElementById("title").value = data.d.split(';')[0];
                        document.getElementById("content").innerHTML = data.d.split(';')[1];
                        $.hidePreloader();
                    } else {
                        //alldepts=[];
                        $.hidePreloader();
                    }
                }
            });
        }
        function getDepts() {
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/getDepts",
                // data: "{helpId: '" + helpId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        alldepts = eval("(" + data.d + ")").alldepts;
                    }
                    else {
                        //alldepts=[];
                    }
                }
            });
        }
        function getUsers() {
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/getUsers",
                data: "{AgentId: '1000008'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        allusers = eval("(" + data.d + ")").allusers;
                        changeType(this, 'ry');
                        allUsers();
                    }
                    else {
                        //allusers = [];
                    }
                }
            });
        }
        var diaryId;
        //获取url参数
        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        //获取update页面数据start
        function getDataById(id) {
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/getDataById",
                data: "{id: '" + id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        var das = [];
                        das = eval("(" + data.d + ")");
                        $("input[name='title']").val(das[0].title);//标题
                        $("textarea[name='content']").html(das[0].content);//内容
                        $('#type_id option:selected').val(das[0].type_id)
                        $("input[name='start_time']").val(das[0].start_time);//开始时间
                        $("input[name='end_time']").val(das[0].end_time);//结束时间
                        // 计算时间差, 单位是毫秒
                        var minus = Math.abs(new Date(das[0].end_time) - new Date(das[0].start_time)) / 60000;
                        $("input[id='cxsj']").val(minus);//结束时间
                        //$("#cxsj").val();//持续时间
                        cxsj
                    }
                    else {
                        //allusers = [];
                    }
                }
            });
        }
        //提交保存 type:0 草稿;1 提交;    新增or修改
        function commitTask(type) {
            $.showPreloader('提交中');
            //图片
            var imgIds2 = "";
            for (var i = 0; i < imgIds.length; i++) {
                imgIds2 += imgIds[i].fwqId + ',';
            }
            //语音
            var voiceIds2 = "";
            for (var i = 0; i < voiceIds.length; i++) {
                voiceIds2 += voiceIds[i].fwqId + ',';
            }
            //$("#content").val(imgIds2);
            //alert(imgIds2);
            //return;
            var diaryId = document.getElementById("KeyID").value;//日志ID
            var action = GetQueryString("action");//add 添加 |  edit修改
            var code = GetQueryString("code");
            //task_id
            var project_id = $('#project_id option:selected').val();//选中的值
            if (typeof (project_id) == "undefined") {
                project_id = "";
            }
            var task_id = $('#task_id option:selected').val();//选中的值
            if (typeof (task_id) == "undefined") {
                task_id = "";
            }
            var strSYR = $("input[name='syr']").val();
            var strXGR = $("input[name='xgr']").val();
            if (typeof (strSYR) == "undefined") {
                strSYR = "";
            }
            if (typeof (strXGR) == "undefined") {
                strXGR = "";
            }
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/commitTask",
                data: "{userId: '" + UserID + "',id: '" + diaryId + "',imgIds: '" + imgIds2 + "',voiceIds: '" + voiceIds2 + "',type: '" + type + "',action: '" + action + "',title: '" + $("input[name='title']").val() + "',content: '" + document.getElementById('content').value + "',start_time: '" + $("input[name='start_time']").val() + "',end_time: '" + $("input[name='end_time']").val() + "',type_id: '" + $('#type_id option:selected').val() + "',syr: '" + strSYR + "',xgr: '" + strXGR + "',project_id: '" + project_id + "',task_id: '" + task_id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        $.hidePreloader();
                        alert("操作成功");
                        $.showPreloader('页面跳转中');
                        if (type == "0") {
                            window.location.href = '../log/list.html?status=0&userType=cg&userId=' + UserID;
                        }
                        if (type == "1") {
                            window.location.href = '../log/list.html?status=1&userType=tj&userId=' + UserID;
                        }
                        $.hidePreloader();
                    }
                    else {
                        $.hidePreloader();
                    }
                }
            });


        }
        //获取update页面数据end
        //点击加载上一次审阅人(样式)
        $('#onOff2').click(function () {
            if ($(this).attr('class') == 'onOff') {
                $(this).attr('class', 'onOff_on');
                $(this).children('.onOff_off').addClass('active');
                $("#toSelectId").val("1");
                GetBeforeUsers("syr");//0表示审阅人
            } else {
                $(this).attr('class', 'onOff');
                $(this).children('.onOff_off').removeClass('active');
                $("#toSelectId").val("0");
            }
        });

        //点击加载上一次相关人(样式)
        $('#onOff3').click(function () {
            if ($(this).attr('class') == 'onOff') {
                $(this).attr('class', 'onOff_on');
                $(this).children('.onOff_off').addClass('active');
                $("#ccSelectId").val("1");
                GetBeforeUsers("xgr");//1表示相关人
            } else {
                $(this).attr('class', 'onOff');
                $(this).children('.onOff_off').removeClass('active');
                $("#ccSelectId").val("0");
            }
        });

        //公共删除方法
        function deleteAllFiles(path, type, e) {
            $.showPreloader('删除中');
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/deleteFiles",
                data: "{path: '" + path + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        $.hidePreloader();
                    }
                    else {
                        $.hidePreloader();
                    }
                }
            });

            //成功后回调函数执行内容
            if (type == 'JournalPhotos') {
                e.parent().remove();
            }
            if (type == 'JournalFiles') {
                e.parent().parent().parent().remove();
            }
            if (type == 'JournalVoices') {
                e.parent().remove();
            }
            if (type == 'JournalVideos') {

            }
            //成功后回调函数执行内容

        }

        //<!--图片部分js-->
        function getJSSDK() {
            var strUrl = window.location.href;
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/getJSSDK",
                data: "{strUrl: '" + strUrl + "',AgentId: '1000008'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    if (data.d != "") {
                        var das = eval("(" + data.d + ")");
                        initWxApi(das[0].appid, das[0].timestamp, das[0].nonceStr, das[0].signature)
                    }
                    else {
                    }
                }
            });
        }
        //初始化微信api
        function initWxApi(appid, timestamp, nonceStr, signature) {
            wx.config({
                beta: true,// 必须这么写，否则在微信插件有些jsapi会有问题
                debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                appId: appid, // 必填，企业微信的cropID
                timestamp: timestamp, // 必填，生成签名的时间戳
                nonceStr: nonceStr, // 必填，生成签名的随机串
                signature: signature,// 必填，签名，见[附录1](#11974)
                jsApiList: [ // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
                    "openEnterpriseChat",
                    "openEnterpriseContact",
                    "onMenuShareTimeline",
                    "onMenuShareAppMessage",
                    "onMenuShareQQ",
                    "onMenuShareWeibo",
                    "onMenuShareQZone",
                    "startRecord",
                    "stopRecord",
                    "onVoiceRecordEnd",
                    "playVoice",
                    "pauseVoice",
                    "stopVoice",
                    "onVoicePlayEnd",
                    "uploadVoice",
                    "downloadVoice",
                    "chooseImage",
                    "previewImage",
                    "uploadImage",
                    "downloadImage",
                    "translateVoice",
                    "getNetworkType",
                    "openLocation",
                    "getLocation",
                    "hideOptionMenu",
                    "showOptionMenu",
                    "hideMenuItems",
                    "showMenuItems",
                    "hideAllNonBaseMenuItem",
                    "showAllNonBaseMenuItem",
                    "closeWindow",
                    "scanQRCode"
                ]
            });
            wx.error(function (res) {
                alert('js授权出错,请检查域名授权设置和参数是否正确');
            });
        }

        //wx.ready(function(res){
        var imgIds = [];

        function chooseImg() {
            wx.chooseImage({
                count: 9, // 默认9
                sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
                sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
                success: function (res) {
                    var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片
                    if (localIds.length > 0) {
                        var addImgHtml = "";
                        imgUpLoad(localIds);
                        for (var i = 0; i < localIds.length; i++) {
                                var src = localIds[i];
                                addImgHtml += "<li>" +
                                    "<a class=\"remove_icon\" name='removeImgA' onclick=\"deleteImg(this)\"  bdId=\"" + src + "\" style=\"display: none;\"></a>" +
                                    "<p class=\"img\">" +
                                    "<img name=\"toUpLoadImg\" src=\"" + src + "\" alt=\"\">" +
                                    "</p>" +
                                    "</li>";

                        }
                        //addImgHtml += "<li class=\"f-user-add\" onclick=\"chooseImg()\"></li>" +
                        //    "<li class=\"f-user-remove\" onclick=\"removeImg()\"></li>";
                        $("#addImg").prepend(addImgHtml);
                    } else {
                        alert("未选择图片");
                    }
                }
            });
        }
        var idx = 0;
        function imgUpLoad(bendiIds) {
            if(idx<bendiIds.length){
                wx.uploadImage({
                    localId: bendiIds[idx], // 需要上传的图片的本地ID，由chooseImage接口获得
                    isShowProgressTips: 1,// 默认为1，显示进度提示
                    success: function (res) {
                        var serverId = res.serverId; // 返回图片的服务器端ID.
                        if (serverId) {
                            imgIds.push({ bdId: bendiIds[idx], fwqId: serverId });
                        }
                        idx++;
                        imgUpLoad(bendiIds);
                    }
                });
            }else{
                idx=0;

            }

        }

        //        function downLoadImg() {
        //            wx.downloadImage({
        //                serverId: '', // 需要下载的图片的服务器端ID，由uploadImage接口获得
        //                isShowProgressTips: 1,// 默认为1，显示进度提示
        //                success: function (res){
        //                    var localId = res.localId; // 返回图片下载后的本地ID
        //                }
        //            });
        //        }


        var removeImgStatus = 0;

        function removeImg() {
            if (removeImgStatus == 0) {
                $("a[name='removeImgA']").show();
                removeImgStatus = 1;
            } else {
                $("a[name='removeImgA']").hide();
                removeImgStatus = 0;
            }
        }

        function deleteImg(e) {
            var thisId = $(e).attr("bdId");
            for (var i = 0; i < imgIds.length; i++) {
                if (imgIds[i].bdId == thisId || imgIds[i].bdId.toString() == thisId.toString()) {
                    imgIds.splice(i, 1);
                }
            }
            $(e).parent().remove();
        }
    </script>
    <!--图片部分js-->
    <!--音频部分js-->
    <script>
        var beginVoice;

        function startVoice() {
            $("#iosMask").show();
            $(".popBox1").show();
            beginVoice = setInterval(timeLess, 1000);
            wx.startRecord();
            wx.onVoiceRecordEnd({
                // 录音时间超过一分钟没有停止的时候会执行 complete 回调
                complete: function (res) {
                    upLoadVoice(res.localId);
                }
            });
        }

        var voiceIds = [];

        function stopVoice(type) {
            $("#iosMask").hide();
            $(".popBox1").hide();
            clearInterval(beginVoice);
            $(".tapeTime").html("01:00");
            time = 59;
            wx.stopRecord({
                success: function (res) {
                    if (type == 1) {
                        var html = "<li>" +
                            "<a class=\"remove_icon\" onclick=\"deleteVoice(this)\"  bdvId=\"" + res.localId + "\" style=\"display: block;\"></a>" +
                            "<p class=\"img\">" +
                            "<img  src=\"../resource/images/voiceFile.png\" style=\"margin-left:10px;width: 42px;height: 42px;\" onclick='playVoice(" + res.localId + ")'>" +
                            "</p>" +
                            "</li>";
                        $("#addVoice").prepend(html);
                        upLoadVoice(res.localId);
                    }
                }
            });
        }

        function playVoice(id) {
            wx.playVoice({
                localId: id // 需要播放的音频的本地ID，由stopRecord接口获得
            });
        }

        function upLoadVoice(bdvId) {
            wx.uploadVoice({
                localId: bdvId, // 需要上传的音频的本地ID，由stopRecord接口获得
                isShowProgressTips: 1,// 默认为1，显示进度提示
                success: function (res) {
                    var serverId = res.serverId; // 返回音频的服务器端ID
                    voiceIds.push({ bdId: bdvId, fwqId: serverId });
                }
            });
        }

        function deleteVoice(e) {
            var thisId = $(e).attr("bdvId");
            for (var i = 0; i < imgIds.length; i++) {
                if (voiceIds[i].bdvId == thisId) {
                    voiceIds.splice(i, 1);
                }
            }
            $(e).parent().remove();
        }

        var time = 59;

        function timeLess() {
            $(".tapeTime").html("00:" + time);
            if (time <= 0) {
                stopVoice();
            }
            time--;
        }
    </script>
    <!--音频部分js-->
    <!--视频部分js-->
    <script>

        //$('#fileupload').fileupload({
        //    //url: 'index.php?g=monitor&m=index&a=uploadFile',
        //    url: "../Ajax/upload.aspx?id=" + document.getElementById("KeyID").value + "&type=JournalVideos", //用于文件上传的服务器端请求地址
        //    dataType: "json",
        //    multipart: true,
        //    done: function (e, data) {
        //        //done方法就是上传完毕的回调函数，其他回调函数可以自行查看api
        //        //注意data要和jquery的ajax的data参数区分，这个对象包含了整个请求信息
        //        //返回的数据在data.result中，这里dataType中设置的返回的数据类型为json
        //        if (data.result.sta) {
        //            // 上传成功：
        //            $(".preview").append("<div style='margin-top:10px;'><embed src=" +
        //                data.result.previewSrc + " allowscriptaccess='always' " +
        //                "allowfullscreen='true' wmode='opaque' width='70' height='50'>" +
        //                "</embed></div>");
        //            //                    $(".preview").append("<div>"+data.result.msg+"</div>");
        //        } else {
        //            // 上传失败：

        //            $(".upstatus").append("<div style='color:red;'>" + data.result.msg + "</div>");
        //        }

        //    },
        //    //上传进度
        //    progressall: function (e, data) {
        //        var progress = parseInt(data.loaded / data.total * 100, 10);
        //        $(".progress .bar").css("width", progress + "%");
        //        $(".proportion").html("上传总进度：" + progress + "%");
        //    }
        //});
        var nowVideoCount = 1;
        function addVideo() {
            var nowVideoId = "video" + nowVideoCount;
            var inputVideo = "<input type='file' id='" + nowVideoId + "' name='" + nowVideoId + "' capture='camera' accept='video/*'>";

            $("#sp").append(inputVideo);
            $("#" + nowVideoId).click();
            $("#" + nowVideoId).change(function () {

                ajaxVideoUpload(nowVideoId);
            });
            nowVideoCount++;
        }
        function ajaxVideoUpload(videoId) {
            $.showPreloader('上传中');
            var diaryId = document.getElementById("KeyID").value;
            $.ajaxFileUpload({
                url: "../ajax/upload.aspx?id=" + diaryId + "&type=JournalVideos", //用于文件上传的服务器端请求地址
                type: 'post',
                secureuri: false, //一般设置为false
                fileElementId: videoId, //文件上传空间的id属性  <input type="file" id="file" name="file" />
                dataType: 'json', //返回值类型 一般设置为json
                success: function (data, status) {  //服务器成功响应处理函数
                    var res = eval("(" + data + ")")[0];
                    //alert(res.path + res.name)//data.result.previewSrc
                    // 上传成功：
                    $(".preview").append("<div style='margin-top:10px;'><embed src=" + res.path + res.name
                        + " allowscriptaccess='always' " +
                        "allowfullscreen='true' wmode='opaque' width='180' height='180'>" +
                        "</embed></div>");
                    alert("上传成功");
                    $.hidePreloader();
                    var file = $("#fileupload")
                    file.after(file.clone().val(""));
                    file.remove();

                },
                error: function (data, status, e)//服务器响应失败处理函数
                {  // 上传失败：
                    alert("上传失败,请重试!");
                    $.hidePreloader();
                    var file = $("#fileupload")
                    file.after(file.clone().val(""));
                    file.remove();

                }
            });
        };

    </script>
    <!--视频部分js-->
    <!--附件部分js-->
    <script>
        var nowFileCount = 1;
        function addFile() {
            var nowFileId = "file" + nowFileCount;
            var inputFile = "<input type=\"file\" id=\"" + nowFileId + "\" name=\"" + nowFileId + "\">";
            $("#fileInput").append(inputFile);
            $("#" + nowFileId).click();
            $("#" + nowFileId).change(function () {

                ajaxFileUpload(nowFileId);
            });
            nowFileCount++;
        }
        function ajaxFileUpload(fileId) {
            $.showPreloader('上传中');
            var diaryId = document.getElementById("KeyID").value;
            $.ajaxFileUpload({
                url: "../Ajax/upload.aspx?id=" + diaryId + "&type=JournalFiles", //用于文件上传的服务器端请求地址
                type: 'post',
                secureuri: false, //一般设置为false
                fileElementId: fileId, //文件上传空间的id属性  <input type="file" id="file" name="file" />
                dataType: 'json', //返回值类型 一般设置为json
                success: function (data, status) {  //服务器成功响应处理函数
                    var res = eval("(" + data + ")")[0];
                    var html = "<div class=\"inner-settings-item flexbox fujian\">" +
                        //"<p class=\""+cl+"\"></p>" +
                        "<div class=\"fujian_text flexItem\">" +
                        "<p class=\"name\">" + res.name + "</p>" +
                        "<p class=\"fujian_size\">" + res.size + " K</p>" +
                        "<p class=\"arrow\">" +
                        "<span class=\"wrap\" onclick=\"deleteAllFiles('" + res.path + res.name + "','JournalFiles',$(this))\" style=\"margin-top: 11px\">" +
                        "<i class=\"qw-operate-icon qw-operate-icon-del\"></i>" +
                        "</span>" +
                        "</p>" +
                        "</div>" +
                        "</div>";
                    $("#fj").append(html);
                    alert("上传成功");
                    $.hidePreloader();
                },
                error: function (data, status, e)//服务器响应失败处理函数
                {
                    alert("上传失败,请重试!");
                    $.hidePreloader();
                }
            }
                )
            return false;
        }
        function voiceTest() {
            //语音
            var voiceIds2 = "";
            for (var i = 0; i < voiceIds.length; i++) {
                voiceIds2 += voiceIds[i].fwqId + ',';
            }
            $("textarea[name='content']").html(voiceIds2);//内容
        }
        function imgsTest() {
            var imgIds2 = "";
            for (var i = 0; i < imgIds.length; i++) {
                imgIds2 += imgIds[i].fwqId + ',';
            }
            $("textarea[name='content']").html(imgIds2);//内容
        }
        function ajaxTest() {
            $.ajax({
                type: "POST",
                url: "../Ajax/AjaxGetMsg.aspx/test2",
                //data: "{strUrl: '" + strUrl + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function Success(data) {
                    //if (data.d != "") {
                    //    var das = eval("(" + data.d + ")");
                    //    initWxApi(das[0].appid, das[0].timestamp, das[0].nonceStr, das[0].signature)
                    //}
                    //else {
                    //}
                }
            });
        }
    </script>
    <!--附件部分js-->
    </form>
</body>

</html>