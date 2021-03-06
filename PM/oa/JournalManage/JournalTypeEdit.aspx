﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JournalTypeEdit.aspx.cs" Inherits="oa_JournalManage_JournalTypeEdit" %>

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
        
    </script>
    <form id="form1" runat="server">
        <div style="height: 95%; overflow: auto; width: 100%">
            <div align="center" id="mTable" style="margin-top: 5px; vertical-align: top;">
                <div style="vertical-align: top;">
                    <table class="tableContent2" style="vertical-align: middle; width: 80%">
                        <tr>
                            <td style="text-align: right;" class="auto-style1">类型名称
                            </td>
                            <td style="text-align: left" class="auto-style2">
                                <asp:TextBox ID="name" Height="100%" Width="99%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align: right;" class="auto-style1">序号
                            </td>
                            <td style="text-align: left" class="auto-style2">
                               <asp:TextBox ID="sort" Height="100%" Width="99%" runat="server" class="mustInput" onkeyup="this.value=this.value.replace(/[^\d]/g,'') " onblur="this.value=this.value.replace(/[^\d]/g,'') "></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style1">是否启用
                            </td>
                            <td style="text-align: left" class="auto-style2">
                                <asp:RadioButtonList ID="is_use" runat="server" RepeatDirection="Horizontal">
                                   <asp:ListItem Selected="True" Value="1">启用</asp:ListItem>
                                    <asp:ListItem Value="0">停用</asp:ListItem>
                                </asp:RadioButtonList> <%--<asp:CheckBoxList ID="is_use" runat="server" Height="16px" RepeatDirection="Horizontal">
                                    
                                </asp:CheckBoxList>--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style1">日志标题缺省值
                            </td>
                            <td style="text-align: left" class="auto-style2">
                                <asp:TextBox ID="title_temp" Height="100%" Width="99%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; text-align: right">日志内容缺省值
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="content_temp" Style="background-image: url(img/Txt_bg.jpg); overflow: auto; line-height: 1.25" Height="120px" Width="100%" BorderStyle="None" Wrap="true" Rows="15" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style1">备注
                            </td>
                            <td style="text-align: left" class="auto-style2">
                                <asp:TextBox ID="remark" Height="100%" Width="99%" runat="server"></asp:TextBox>
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
                        <input id="btnSave" name="submit" type="submit" value="保存" style="width: auto; height: 21px;" onserverclick="btnSave_Click" runat="server" />
                        <input type="button" id="btnCancel" value="取消" onclick="top.ui.closeTab();" />
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" id="KeyId" runat="server" />
    </form>
</body>
</html>
