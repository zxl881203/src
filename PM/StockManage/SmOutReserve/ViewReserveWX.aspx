﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewReserve.aspx.cs" Inherits="StockManage_SmOutReserve_ViewReserve" %>

<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看出库单</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="" />
    <meta name="HandheldFriendly" content="True" />
    <meta name="MobileOptimized" content="320" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="telephone=no" name="format-detection" />
    <meta content="email=no" name="format-detection" />

    <script type="text/javascript" src="/Script/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="/Script/layer_v3.1.1/layer/layer.js"></script>

    <%-- <script type="text/javascript" src="/Script/jquery.js"></script>--%>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <style type="text/css" media="print">
        .noprint {
            display: none;
        }
    </style>
    <script type="text/javascript">
        addEvent(window, 'load', function () {
            setAnnxReadOnly('FileLink1');
        });
        function showName() {
            var orid = $("#hdGuid").val();
            //var img = $("#hdImg").val();
            // alert(img)
            var url = "/WeChat/writeName/showName.aspx?orid=" + orid;// + "&img=" + img;
            layer.open({
                type: 2,
                title: '查看签名',
                shadeClose: true,
                shade: 0.8,
                area: ['100%', '100%'],//['380px', '90%'],
                content: url//'mobile/' //iframe的url
            });
        }
    </script>
    <style type="text/css">
        #FileLink1_But_UpFile {
            width: auto;
        }

        #FileLink1_Btn_Upload {
            width: auto;
        }
    </style>
</head>
<body id="print1">
    <form id="form1" runat="server">
        <table class="tab" >
            <tr style="height: 20px;display:none;">
                <td class="divHeader">查看出库
                <input type="button" id="btnDy" style="float: right;" class="noprint" onclick="winPrint()"
                    value=" 打 印 " />
                </td>
            </tr>
            <tr style="height: 1px;">
                <td>
                    <table id="bllProducer" cellpadding="0" cellspacing="0" style="border-style: none;"
                        class="viewTable">
                        <tr>
                            <td style="border-style: none;">制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
                            </td>
                            <td style="border-style: none; text-align: right">打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 406px; width: 100%;" valign="top">
                    <table cellpadding="0" cellspacing="2" style="width: 100%;" class="viewTable" border="0">
                        <tr>
                            <td class="descTd">单据号
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblPPCode" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">项目
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                                <input id="hdnProjectCode" style="width: 10px" type="hidden" name="hdnProjectCode" runat="server" />

                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">领料人
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblPickingPeople" runat="server"></asp:Label>
                                <span class="al tooltip"  onclick="showName()">
                                                                    查看签名
                                                                </span>
                            </td>
                            <td class="descTd">领料部门
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblPickingSector" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">选择仓库
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblTerasuryName" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">设备
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblEquCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">录入人
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblPeople" runat="server"></asp:Label>
                            </td>
                            <td class="descTd">录入时间
                            </td>
                            <td class="elemTd">
                                <asp:Label ID="lblInTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">附件
                            </td>
                            <td colspan="3">
                                <MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">说明
                            </td>
                            <td colspan="3">
                                <div style="width: 100%; word-break: break-all;">
                                    <asp:Label ID="lblExplain" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table style="vertical-align: top; width: 100%;">
                        <tr>
                            <td style="padding-top: 10px;">
                                <asp:GridView ID="gvNeedNote" Width="100%" AutoGenerateColumns="false" CssClass="viewTable" OnRowDataBound="gvNeedNote_RowDataBound" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="物资编号">
                                            <ItemTemplate>
                                                <%# Eval("scode") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="物资名称">
                                            <ItemTemplate>
                                                <%# Eval("ResourceName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="规格" ItemStyle-Width="70px">
                                            <ItemTemplate>
                                                <%# Eval("Specification") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单位" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <%# Eval("Name") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="库存数量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# Eval("snumber") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="出库数量" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# Eval("number") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input">
                                            <ItemTemplate>
                                                <%# Eval("sprice") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="出库小计" ItemStyle-CssClass="decimal_input" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# (Eval("Total").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="供应商" ItemStyle-Width="85px">
                                            <ItemTemplate>
                                                <div style="word-break: break-all;">
                                                    <%# Eval("Corp") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="型号" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <div style="word-break: break-all;">
                                                    <%# Eval("ModelNumber") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="品牌" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <div style="word-break: break-all;">
                                                    <%# Eval("brand") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="技术参数" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <div style="word-break: break-all;">
                                                    <%# Eval("TechnicalParameter") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="分部分项" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <div style="word-break: break-all;">
                                                    <%# Eval("TaskName") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="rowa"></RowStyle>
                                    <AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle>
                                    <HeaderStyle CssClass="header"></HeaderStyle>
                                    <FooterStyle CssClass="footer"></FooterStyle>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="trAudit" style="vertical-align: top;">
                            <td>
                                <MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="076" BusiClass="001" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdwzId" runat="server" />
        <asp:HiddenField ID="hdGuid" runat="server" />
        <asp:HiddenField ID="hdTsid" runat="server" />
        <asp:HiddenField ID="hdImg" runat="server" />
        <asp:HiddenField ID="hdflowstate" Value="-1" runat="server" />
    </form>
</body>
</html>
