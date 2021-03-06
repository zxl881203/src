﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JournalEdit.aspx.cs" Inherits="oa_JournalManage_JournalEdit" %>
<%--TagPrefix  是前缀  如asp:   自带控件的前缀都是asp:
TagName 是控件的名称--%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx1" Src="~/UserControl/FileUpload/FileUpload.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx2" Src="~/UserControl/FileUpload/FileUpload.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/StockManage/script/Validation.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtTo').attr('readOnly', true);
            $('#txtCopyto').attr('readOnly', true);
        });
        //选择项目
        function openProjPicker() {
            var action = getRequestParam('action');
            if (action != 'view') {
                jw.selectOneProject({ idinput: 'hdnProjectCode', nameinput: 'txtProject' });
            }
        }


        function getfocus() {
            document.getElementById('hdnid').value = this.name;
        }
        function CheckEmpty() {
            var list = document.getElementsByTagName("input");
            for (var i = 0; i < list.length && list[i]; i++) {
                if (list[i].type == "text") {
                    if (list[i].value == "") {
                        top.ui.alert("请不要留空!")
                        return false;
                    }
                }
            }
        }
        function saveDate() {
            var val = document.getElementById('DateInTime').value;
            document.getElementById('hdnReportDate').value = val;
        }
        function winclose(tobj, url, rebool) {
            if (rebool) {
                parent.desktop[tobj].location = url + "?planType=" + getRequestParam("planType");
            };
            parent.desktop[tobj] = null;

            top.frameWorkArea.window.desktop.getActive().close();

        }
        function typeChange() {
            var tempId = $('#type_id option:selected').val();//选中的值
            if (tempId == "0") {
                return false;
            }
            var ajax = null;
           
            if (tempId != 0) {
                if (document.getElementById('title').value == "" && document.getElementById('content').value == "") {
                    ajax = 1;
                }
                else {
                    if (confirm('是否用\'日志模版\'替换当前的\'标题\'和\'内容\'?')) {
                        ajax = 1;
                    } else { ajax = 0; }
                }

                if (ajax == 1) {
                    $.ajax({
                        type: "POST",
                        url: "../../WeChat/Ajax/AjaxGetMsg.aspx/getTemps",
                        data: "{typeId: '" + tempId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function Success(data) {
                            if (data.d != "") {
                                document.getElementById('title').value = data.d.split(';')[0];
                                document.getElementById('content').value = data.d.split(';')[1];
                            }
                            else {
                                //allusers = [];
                            }
                        }
                    });
                }
            }
        }
        function AddOption() {
            var res = checkStartTimeIfNull();
            if (res == 0) return;
            var DateTime = document.getElementById("start_time").value;
            var times = document.getElementById("txtValue").value; //持续的时间段
            var oldTime = (new Date(DateTime)).getTime(); //得到毫秒数
            var num = times * 60 * 1000
            var b = oldTime + num;
            var newTime = new Date(b);
           document.getElementById("end_time").value = formatDateTime(newTime);
        }
        var formatDateTime = function (date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            m = m < 10 ? ('0' + m) : m;
            var d = date.getDate();
            d = d < 10 ? ('0' + d) : d;
            var h = Number(date.getHours())< 10 ? ('0' + Number(date.getHours())) : Number(date.getHours());
            var minute = Number(date.getMinutes()) < 10 ? ('0' + Number(date.getMinutes())) : Number(date.getMinutes());
            //minute = minute < 10 ? ('0' + minute) : minute;
            return y + '-' + m + '-' + d + ' ' + h + ':' + minute;
        };
        function checkTime() {
            //var res = checkStartTimeIfNull();
            //if (res == 0) return;
            document.getElementById('txtValue').value = $('#ddl option:selected').val();//选中的值
            //document.getElementById('txtValue').focus();
            AddOption();
        }
        function checkStartTimeIfNull(){
            if (document.getElementById("start_time").value == "") {
                $.messager.alert('系统提示', '请先填写工作日志的开始时间！');
                document.getElementById('txtValue').value = "";
                document.getElementById('start_time').focus();
                return 0;
            } else {
                return 1;
            }
        }
        //管理日期
        function controlDate() {
            var startDates = document.getElementById('start_time').value;
            var endDates = document.getElementById('end_time').value;
            if (startDates != '' && endDates != '') {
                var sTime = (new Date(startDates)).getTime();
                var eTime = (new Date(endDates)).getTime();
                if (sTime - eTime > 0) {
                    $.messager.alert('系统提示', '工作日志 开始时间[' + startDates + ']</br>不能小于 结束时间[' + endDates + ']</br>请重新填写！');
                    document.getElementById('endDates').focus();
                }
                else {

                }
            }
        }
        //表单验证
        function valForm() {
            if ($('#type_id option:selected').val() == "") {
                alert("系统提示：\n日志类型必须输入！");
                //document.getElementById("type_id").focus();
                return false;
            }
            else if (document.getElementById("title").value == "") {
                alert("系统提示：\n日志标题必须输入！");
                document.getElementById("title").focus();
                return false;
            }
            return true;
        }
    </script>
    <form id="form1" runat="server">
        <div style="height: 95%; overflow: auto; width: 100%">
            <div align="center" id="mTable" style="margin-top: 5px; vertical-align: top;">
                <div style="vertical-align: top;">
                    <table class="tableContent2" cellpadding="5px"cellspacing="0"  style="vertical-align: middle; width: 80%">
                        <tr>
                            <td style="width: 10%; text-align: right;">日志类型</td>
                            <td style="width: 90%; text-align: left" >
                                <asp:DropDownList ID="type_id"  runat="server" Width="99%" onchange="typeChange()"></asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td class="word" align="right">
                                日志标题
                            </td>
                             <td class="txt mustInput">
                                <asp:TextBox ID="title" Height="100%" Width="99%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style1">开始时间
                            </td>
                            <td style="text-align: left" class="auto-style2">
                                <table>
                                    <tr>
                                        <td >
                                            <asp:TextBox ID="start_time" onchange="controlDate()" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" CssClass="easyui-validatebox" style="width: 120px;" data-options="required:true" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 5px;">
                                            
                                        </td>
                                        <td style="background-color: #fafafa;">持续<asp:TextBox ID="txtValue" runat="server" onkeyup="AddOption();" sucmsg="填写正确" Style="text-align: right; width: 40px;" Width="50px" />
                                            <asp:DropDownList ID="ddl" onchange="checkTime()" runat="server" CssClass="ddcss" Width="20px">
                                                <asp:ListItem Value="30">  30分钟(0.5小时)</asp:ListItem>
                                                <asp:ListItem Value="60">  60分钟(1.0小时)</asp:ListItem>
                                                <asp:ListItem Value="90">  90分钟(1.5小时)</asp:ListItem>
                                                <asp:ListItem Value="120">120分钟(2.5小时)</asp:ListItem>
                                                <asp:ListItem Value="150">150分钟(2.5小时)</asp:ListItem>
                                                <asp:ListItem Value="180">180分钟(3.0小时)</asp:ListItem>
                                                <asp:ListItem Value="210">210分钟(3.5小时)</asp:ListItem>
                                                <asp:ListItem Value="240">240分钟(4.0小时)</asp:ListItem>
                                                <asp:ListItem Value="270">270分钟(4.5小时)</asp:ListItem>
                                                <asp:ListItem Value="300">300分钟(5.0小时)</asp:ListItem>
                                                <asp:ListItem Value="330">330分钟(5.5小时)</asp:ListItem>
                                                <asp:ListItem Value="360">360分钟(6.0小时)</asp:ListItem>
                                                <asp:ListItem Value="390">390分钟(6.5小时)</asp:ListItem>
                                                <asp:ListItem Value="420">420分钟(7.0小时)</asp:ListItem>
                                                <asp:ListItem Value="450">450分钟(7.5小时)</asp:ListItem>
                                                <asp:ListItem Value="480">480分钟(8.0小时)</asp:ListItem>
                                            </asp:DropDownList>
                                            分钟
                                        </td>
                                         <td style="width: 5px;">
                                            
                                        </td>
                                        
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style1">结束时间
                                        </td>
                                        <td style="text-align: left" class="auto-style2">
                                            <asp:TextBox ID="end_time" onchange="controlDate()" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" CssClass="easyui-validatebox" style="width: 120px;" data-options="required:true" runat="server"></asp:TextBox>
                                        </td>
                        </tr>
                        
                        <tr>
                            <td style="width: 10%; text-align: right">内容
                            </td>
                            <td style="text-align: left;">
                                <%--<asp:TextBox ID="content" TextMode="MultiLine" Height="100px" Width="100%" runat="server"></asp:TextBox>--%>
                                <asp:TextBox ID="content" Style="background-image: url(img/Txt_bg.jpg); overflow: auto; line-height: 1.25" Height="120px" Width="100%" BorderStyle="None" Wrap="true" Rows="15" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <%--                        <tr>
                            <td style="text-align: right;" class="auto-style1">封面图片
                            </td>
                            <td style="text-align: left" class="auto-style2">
                               <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload0" Name="图片" FileType="*.jpg;*.gif;*.png" Class="BuildDiaryPhoto" runat="server" />
                            </td>
                        </tr>--%>
                       
                        <tr>
                            <td style="width: 10%; text-align: right;">审阅人
                            </td>
                            <td style="text-align: left">
                                <span class="spanSelect" style="width: 90%">
                                    <asp:TextBox ID="txtTo" Style="width: 90%; height: 15px; border: none; line-height: 16px; margin: 1px 2px"
                                        runat="server"></asp:TextBox>
                                    <img src="../../images1/email/add.jpg" style="float: right; border: none;" alt="选择" id="imgName" onclick="jw.selectOneUser({ nameinput: 'txtTo', codeinput: 'hfldTo' });" runat="server" />

                                </span>
                                <input id="hfldTo" type="hidden" style="width: 1px" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%; text-align: right;">相关人
                            </td>
                            <td style="text-align: left">
                                <span class="spanSelect" style="width: 90%;">
                                    <asp:TextBox ID="txtCopyto" Style="width: 90%; height: 15px; border: none; line-height: 16px; margin: 1px 2px"
                                        runat="server"></asp:TextBox>
                                    <img src="../../images1/email/add.jpg" style="float: right;" alt="选择" id="img1" onclick="jw.selectMultiUser({ nameinput: 'txtCopyto', codeinput: 'hfldCopyto' });" runat="server" />

                                </span>
                                <input id="hfldCopyto" type="hidden" style="width: 1px" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style1">相关图片
                            </td>
                            <td style="text-align: left" class="auto-style2">
                                <MyUserControl:usercontrol_fileupload_fileupload_ascx1 ID="FileUpload1" Name="图片" FileType="*.jpg;*.gif;*.png" Class="JournalPhotos" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style1">相关附件
                            </td>
                            <td style="text-align: left" class="auto-style2">
                                <MyUserControl:usercontrol_fileupload_fileupload_ascx2 ID="FileUpload2" Name="附件" Class="JournalFiles" runat="server" />
                            </td>
                        </tr>
                         <tr>
                            <td style="width: 10%; text-align: right;">关联项目</td>
                            <td style="text-align: left">
                                <input type="text" id="txtProject" readonly="readonly" class="select_input" imgclick="openProjPicker" runat="server" />

                                <asp:HiddenField ID="hdnProjectCode" runat="server" />
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align: right;" class="auto-style1">关联任务
                            </td>
                            <td style="text-align: left" class="auto-style2">
                                  <input type="text" id="tasks" readonly="readonly" class="select_input" imgclick="openProjPicker" runat="server" />
					<asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="btnSaveCG" Text="保存为草稿" OnClientClick="return valForm()" OnClick="btnSaveCG_Click"  runat="server" style="width: auto;"/>
                        <asp:Button ID="btnSaveTJ" Text="保存并提交" OnClientClick="return valForm()" OnClick="btnSaveTJ_Click"  runat="server" style="width: auto;"/>
                        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeTab();" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divFram" title="" style="display: none"><iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe></div>
          <input type="hidden" id="KeyId" runat="server" />
        <input type="hidden" id="hdnDelPlanId" runat="server" />
    </form>
</body>
</html>
