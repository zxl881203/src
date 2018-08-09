﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskView.aspx.cs" Inherits="OA3_WorkTask_TaskView" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <link href="/StyleExt/CSS/StyleSheet.css" rel="stylesheet" type="text/css" media="all" />
    <link href="/StockManage/skins/sm4.css" rel="stylesheet" type="text/css" />
    <link href="/StyleCss/Common.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../Script/DecimalInput.js"></script>
        <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$("#up1").hide();
            //$("#up2").hide();
            $("#upShow").hide();
            replaceEmptyTable('emptyTable', 'gvDiary_MX');
            getImgPath();
        });
        var imgArr = new Array();
        var imgLength = 0;
        function showImg(imgUrl, imgIndex) {
            var img3 = parseInt($('#img3').attr('alt'));
            if (imgIndex == 1) {
                imgIndex = img3 - 1;
            }
            else {

                if (imgIndex == 2) {
                    imgIndex = img3;
                }
                else {
                    if (imgIndex == 3) {
                        imgIndex = 4;
                    }
                }
            }
            var url = '/EPC/BuildDiary/ShowImg.aspx?imgUrl=' + imgUrl + '&imgIndex=' + imgIndex + '&imgPath=' + $('#hfldImgPath').val();
            top.ui.openWin({ title: '相关图片', url: url, width: 450, height: 450 });
        }
        function getImgPath() {
            var str = $('#hfldImgPath').val();
            if (str.length <= 2) {
                $('#photo').hide();
            }
            JSON.parse(str, function (k, v) {
                if (v != ',') {
                    imgArr.push(v);
                }
            });
            if (imgArr.length >= 3) {
                imgLength = imgArr.length - 1;
            }
            else {
                imgLength = imgArr.length;
            }
            if (imgLength > 0) {
                if (imgLength == 1) {
                    $('#img1').attr('src', imgArr[0]);
                    $('#img2').hide();
                    $('#img3').hide();
                    $('#img3').attr('alt', 1);
                }
                if (imgLength == 2) {
                    $('#img1').attr('src', imgArr[0]);
                    $('#img2').attr('src', imgArr[1]);
                    $('#img3').hide();
                    $('#img3').attr('alt', 2);
                }
                if (imgLength >= 3) {
                    $('#img1').attr('src', imgArr[0]);
                    $('#img2').attr('src', imgArr[1]);
                    $('#img3').attr('src', imgArr[2]);
                    $('#img3').attr('alt', 3);
                }
            }
        }
        //向后翻
        function next() {
            var imgIndex = parseInt($('#img3').attr('alt'));
            if (imgLength <= 3 || imgIndex < 3) {
                return;
            }
            else {
                if (imgLength == imgIndex) {
                    $('#img1').attr('src', imgArr[0]);
                    $('#img2').attr('src', imgArr[1]);
                    $('#img3').attr('src', imgArr[2]);
                    $('#img3').attr('alt', 3);
                    $('#img2').show();
                    $('#img3').show();
                    return false;
                }
                if (imgLength - imgIndex == 1) {
                    $('#img1').attr('src', imgArr[imgIndex]);
                    $('#img2').hide();
                    $('#img3').hide();
                    $('#img3').attr('alt', ++imgIndex);
                    return false;
                }
                if (imgLength - imgIndex == 2) {
                    $('#img1').attr('src', imgArr[imgIndex]);
                    $('#img2').attr('src', imgArr[++imgIndex]);
                    $('#img3').hide();
                    $('#img3').attr('alt', ++imgIndex);
                    $('#img2').show();
                    return false;
                }
                if (imgLength - imgIndex >= 3) {
                    $('#img1').attr('src', imgArr[imgIndex]);
                    $('#img2').attr('src', imgArr[++imgIndex]);
                    $('#img3').attr('src', imgArr[++imgIndex]);
                    $('#img3').attr('alt', ++imgIndex);
                    $('#img2').show();
                    $('#img3').show();
                    return false;
                }
            }
        }
        //向前翻
        function pre() {
            var img1Index;
            if (imgLength <= 3) {
                return;
            }
            else {
                var imgIndex = parseInt($('#img3').attr('alt'));
                if (imgIndex == 3) {
                    img1Index = imgLength - 1;
                    $('#img1').attr('src', imgArr[img1Index]);
                    $('#img3').attr('alt', ++img1Index);
                    $('#img2').hide();
                    $('#img3').hide();
                    return;
                }
                if (imgLength == imgIndex) {
                    img1Index = imgIndex - 5;
                    $('#img1').attr('src', imgArr[img1Index]);
                    $('#img2').attr('src', imgArr[++img1Index]);
                    $('#img3').attr('src', imgArr[++img1Index]);
                    $('#img3').attr('alt', ++img1Index);
                    $('#img2').show();
                    $('#img3').show();
                    return false;
                }
                if (imgLength - imgIndex == 1) {
                    img1Index = imgIndex;
                    $('#img1').attr('src', imgArr[img1Index]);
                    $('#img3').attr('alt', ++img1Index);
                    $('#img2').hide();
                    $('#img3').hide();
                    return;
                }
                if (imgLength - imgIndex == 2) {
                    img1Index = imgIndex;
                    $('#img1').attr('src', imgArr[img1Index]);
                    $('#img2').attr('src', imgArr[++img1Index]);
                    $('#img3').attr('alt', ++img1Index);
                    $('#img2').show();
                    $('#img3').hide();
                    return false;
                }
                if (imgLength - imgIndex >= 3) {
                    img1Index = imgLength - 4;
                    $('#img1').attr('src', imgArr[img1Index]);
                    $('#img2').attr('src', imgArr[++img1Index]);
                    $('#img3').attr('src', imgArr[++img1Index]);
                    $('#img3').attr('alt', ++img1Index);
                    $('#img2').show();
                    $('#img3').show();
                    return false;
                }
            }
        }
        //function upProgress() {
        //    if ($("#up1").is(":hidden")) {
        //        $("#up1").show();
        //        $("#up2").show();//如果元素为隐藏,则将它显现
        //    } else {
        //        $("#up1").hide();
        //        $("#up2").hide();//如果元素为显现,则将其隐藏
        //    }
        //}
        function upProgressShow() {
            if ($("#upShow").is(":hidden")) {
                $("#upShow").show();    //如果元素为隐藏,则将它显现
            } else {
                $("#upShow").hide();     //如果元素为显现,则将其隐藏
            }
        }
        function viewLog() {
            top.ui._Task = window;
            var url = "/OA3/WorkLog/MyLogList.aspx?action=view&time=&taskId=" + document.getElementById('KeyId').value;
            title = '查看关联日志';
            toolbox_oncommand(url, title);
        }
    </script>
</head>
<body id="print1" style="overflow-y: auto; height: auto;">
    <form id="form1" runat="server">
        <div class="VPage">
            <%-- <div align="center" id="mTable" style="margin-top: 5px; vertical-align: top;">
                <div style="vertical-align: top;">--%>

            <table class="tabCss" style="vertical-align: top; border-collapse: collapse;">
           <tr>
             <td class="title-divHeader">
                工作任务信息详情
                
                <%--<input type="button" style="float: right;" class="noprint" onclick="winPrint()" value=" 打 印 " />
               <input type="button" id="btnDy" style="float: right;" class="noprint" value=" 打 印 "/>--%>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;width: 100%;">
                    <tr>
                        <td style="border-style: none;">
                            填写人:&nbsp;&nbsp;<asp:Label ID="creater" runat="server"></asp:Label>
                        </td>
                        <td style="border-style: none; text-align: right">
                            填写时间:&nbsp;&nbsp;<asp:Label ID="create_date" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
                <tr>
                    <td style="vertical-align: top">
                        <div class="cell_title_blue_line" >
                            <div class="cell_title_blue_left">
                            </div>
                            <div class="cell_title_blue_bg">
                                <span class="titlespan">工作任务详情</span>
                            </div>
                            <div class="cell_title_blue_right">
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <table class="tabCss" style="vertical-align: top; border-collapse: collapse;">
                <tr>
                    <td class="descTd">任务类型</td>
                    <td class="elemTd">
                        <asp:Label ID="type_id" runat="server"></asp:Label>
                    </td>
                </tr>
              
                <tr>
                    <td class="descTd">任务优先级</td>
                    <td class="elemTd">
                        <asp:Label ID="CodeName" runat="server"></asp:Label>
                    </td>
                </tr>
              
                <tr>
                    <td class="descTd">任务名称
                    </td>
                    <td class="elemTd">
                        <asp:Label ID="title" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="descTd">开始时间
                    </td>
                    <td class="elemTd">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="start_time" runat="server"></asp:Label>
                                </td>
                                <td style="width: 5px;"></td>
                                <td style="background-color: #fafafa;">持续 <asp:Label ID="txtValue" runat="server"></asp:Label>
                                            分钟
                                </td>
                                <td style="width: 5px;"></td>

                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="descTd">结束时间
                    </td>
                    <td class="elemTd">
                        <asp:Label ID="end_time" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="descTd">任务内容
                    </td>
                    <td class="elemTd">
                        <asp:Label ID="content" Style="background-color: #fafafa;" Height="120px" Width="100%" Wrap="true" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="descTd">任务状态
                    </td>
                    <td class="elemTd">
                        <asp:Label ID="status_name" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="descTd">任务进度(%)
                    </td>
                    <td class="elemTd">
                        <asp:Label ID="progress" runat="server"></asp:Label>
                         <%-- <a href="javascript:void(0)" onclick="upProgress()" style="margin-left: 10px;">[更新进度]</a> --%> 
                         <a href="javascript:void(0)" onclick="upProgressShow()" style="margin-left: 10px;">[更新记录]</a> 
                         <a href="javascript:void(0)" onclick="viewLog()" style="margin-left: 10px;">[关联日志]</a> 
                        
                    </td>
                </tr>
                
                  <tr id="upShow">
                        <td class="descTd">
                        </td>
                        <td class="descTd">
                             <%=JD %>  
                        </td>
                    </tr>
                <tr>
                    <td class="descTd">负责人
                    </td>
                    <td class="elemTd">
                        <asp:Label ID="txtTo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="descTd">相关人
                    </td>
                    <td class="elemTd">
                        <asp:Label ID="txtCopyto" runat="server"></asp:Label>
                    </td>
                </tr>
                                <tr id="photo" style="height: 150px;">
                    <td class="descTd" valign="middle" align="center">相关图片
                    </td>
                    <td style="vertical-align: middle">
                        <div style="width: 35px; float: left; padding-top: 60px;">
                            <input class="noprint" type="button" value="<<" onclick="pre();" style="width: 35px" />
                        </div>
                        <div style="width: 600px; height: 140px; float: left; padding-left: 13px; padding-top: 5px; padding-bottom: 5px; overflow: hidden;">
                            <table style="border: none; border-width: 0px; text-align: right;">
                                <tr>
                                    <td style="border: none; border-width: 0px; width: 200px;">
                                        <img alt="" id="img1" style="height: 140px; width: 200px;" src="" onclick="showImg(this.src,1)" />
                                    </td>
                                    <td style="border: none; border-width: 0px; width: 200px;">
                                        <img alt="" id="img2" style="height: 140px; width: 200px;" src="" onclick="showImg(this.src,2)" />
                                    </td>
                                    <td style="border: none; border-width: 0px; width: 200px;">
                                        <img alt="" id="img3" style="height: 140px; width: 200px;" src="" onclick="showImg(this.src,3)" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        &nbsp
                            <div style="width: 35px; float: right; padding-top: 60px;">
                                <input class="noprint" type="button" value=">>" onclick="next();" style="width: 35px" />
                            </div>
                    </td>
                </tr>
                <tr id="videos" style="height: 150px;">
                    <td class="descTd" valign="middle" align="center">相关音频
                    </td>
                    <td class="elemTd">
                        <%=voices %>
                    </td>
                </tr>
                <tr id="voices" style="height: 150px;">
                    <td class="descTd" valign="middle" align="center">相关视频
                    </td>
                    <td class="elemTd">
                        <%=videos %>
                    </td>
                </tr>
                <tr>
                    <td class="descTd">相关附件
                    </td>
                    <td class="elemTd" id="FileUpload2" runat="server"></td>
                </tr>
                <%--<tr>
                    <td class="descTd">相关图片
                    </td>
                    <td class="elemTd" id="FileUpload1" runat="server" colspan="3">
                    </td>
                </tr>
                 <tr>
                    <td class="descTd">相关音频
                    </td>
                    <td class="elemTd" id="FileUpload3" runat="server" colspan="3">
                    </td>
                </tr>
                 <tr>
                    <td class="descTd">相关视频
                    </td>
                    <td class="elemTd" id="FileUpload4" runat="server" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="descTd">相关附件
                    </td>
                    <td class="elemTd" id="FileUpload2" runat="server" colspan="3">
                    </td>
                </tr>--%>
                <tr id="PLtitle" runat="server">
                   <td  class="descTd" colspan="2"> 
                        <div class="cell_title_blue_line">
                            <div class="cell_title_blue_left">
                            </div>
                            <div class="cell_title_blue_bg">
                                <span class="titlespan">工作任务回复</span>
                            </div>
                            <div class="cell_title_blue_right">
                            </div>
                        </div>
                    </td>
                </tr>
                <%=PL %>
                <tr id="btnWCorGB" runat="server">
                    <td class="descTd"></td>
                    <td class="descTd">
                        <table class="tableFooter2">
                            <tr>
                                <td>
                                  <asp:Button ID="btnSaveWC" Text="完成任务" OnClick="btnSaveWC_Click"  runat="server" style="width: auto;"/>
                                  <asp:Button ID="btnSaveGB" Text="关闭任务" OnClick="btnSaveGB_Click"  runat="server" style="width: auto;"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <%-- <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="btnSaveWC" Text="完成任务" OnClientClick="return valForm()" OnClick="btnSaveWC_Click"  runat="server" style="width: auto;"/>
                        <asp:Button ID="btnSaveGB" Text="关闭任务" OnClientClick="return valForm()" OnClick="btnSaveGB_Click"  runat="server" style="width: auto;"/>
                    </td>
                </tr>
            </table>
        </div>--%>

        <div id="divFram" title="" style="display: none">
            <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
        </div>
        <input type="hidden" id="KeyId" runat="server" />
        <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="图片" FileType="*.jpg;*.gif;*.png" Class="TaskPhotos" Visible="false" runat="server" />
        <asp:HiddenField ID="hfldImgPath" Value="" runat="server" />
        <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload3" Name="音频" Class="TaskVoices" Visible="false" runat="server" />
        <MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload4" Name="视频" Class="TaskVideos" Visible="false" runat="server" />
    </form>
</body>
</html>
