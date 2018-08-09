﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Allocation.aspx.cs" Inherits="StockManage_Report_Allocation" %>

<%@ Import Namespace="cn.justwin.BLL" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调拨报表</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="" />
    <meta name="HandheldFriendly" content="True" />
    <meta name="MobileOptimized" content="320" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="telephone=no" name="format-detection" />
    <meta content="email=no" name="format-detection" />

    <link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/default/easyui.css" />
    <link rel="Stylesheet" type="text/css" href="../../Script/jquery.easyui/themes/icon.css" />
    <link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="Stylesheet" type="text/css" />

       <script type="text/javascript" src="/Script/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="/Script/layer_v3.1.1/layer/layer.js"></script>

    <%-- <script type="text/javascript" src="/Script/jquery.js"></script>--%>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.ifshow').hide();
            // 添加验证
            $('#btnSearch')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var jwTable = new JustWinTable('gvwStorage');
            showTooltip('tooltip', 10);
        });

        function pickCorp() {
            jw.selectOneCorp({ idinput: 'hfldCorp', nameinput: 'txtCorp' });
        }
        //打开仓库
        function openPer() {
            var url = "/StockManage/Allocation/SelectDepository.aspx?tshow=PickTreasury2&hid=HdnTreasury&no=-1";
            document.getElementById("divFramPer").title = "选择仓库";
            document.getElementById("ifFramPer").src = url;
            selectnEvent('divFramPer');
        }

        function selectTreaOut() {
            jw.selectTreasury({ codeinput: 'hfldTreaOut', nameinput: 'txtTreaOut' });
        }

        function selectTreaIn() {
            jw.selectTreasury({ codeinput: 'hfldTreaIn', nameinput: 'txtTreaIn' });
        }
        function ifshow() {
            if ($('.ifshow').is(':hidden')) {
                $('.ifshow').show();
                $("#btnSelect").hide();
            }
            else {
                $('.ifshow').hide();
                $("#btnSelect").show();
            }
        }
         //查看
        function rowQuery(id) {
            ///onclick="viewopen('/StockManage/Allocation/AuditPage.aspx?ic=<%# Eval("aid") %>&BusiClass=001&BusiCode=075','','')">
            var url = '/StockManage/Allocation/AuditPageWX.aspx?ic=' + id + "&BusiClass=001&BusiCode=075";;
            layer.open({
                type: 2,
                title: '查看调拨报表',
                shadeClose: true,
                shade: 0.8,
                area: ['100%', '100%'],
                content: url
            });
        }
    </script>
     <style type="text/css">
        .gvw{
            min-width: 800px;
        }
        .gvw tr{
            height: 30px;
        }
        .footerM {
            /*color:red;*/
        }
         .footerM td table tbody tr td span{
            font-size: 12px;
    margin: 5px;
    border: 1px solid #b5b2b2;
    padding: 3px;
    margin-left: 10px;
    background-color: #fbfbfb;
    color:red;
        }
         .footerM td table tbody tr td a {
            font-size: 12px;
    margin: 5px;
    border: 1px solid #b5b2b2;
    padding: 3px;
    margin-left: 10px;
    background-color: #fbfbfb;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr class="ifshow">
                <td>
                    <table class="queryTable" cellpadding="3px" cellspacing="0px">
                        <tr>
                            <td class="descTd">起始时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>    </tr>
                        <tr>
                            <td class="descTd">结束时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">资源名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtResourceName" Width="120px" ToolTip="如果有多个资源 请以逗号隔开" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>    </tr>
                        <tr>
                            <td class="descTd">资源编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtResourceCode" Width="120px" ToolTip="如果有多个资源 请以逗号隔开" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">单据编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPurchaseCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                                </tr>
                        <tr>
                            <td class="descTd">供应商
                            </td>
                            <td>
                                <asp:TextBox ID="txtCorp" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                <%--<asp:TextBox ID="txtCorp" CssClass="txtreadonly easyui-validatebox select_input" Style="width: 120px;" ToolTip="请选择" data-options="validType:'validQueryChars[50]'" imgclick="pickCorp" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hfldCorp" Value="" runat="server" />--%>
                            </td>
                        </tr>
                        <tr style="height: 25px;">
                            <td class="descTd">调出库
                            </td>
                            <td>
                                <asp:TextBox ID="txtTreaOut" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                <%--<asp:TextBox ID="txtTreaOut" CssClass="select_input" Width="120px" imgclick="selectTreaOut" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hfldTreaOut" runat="server" />--%>
                            </td>    </tr>
                        <tr>
                            <td class="descTd">接收库
                            </td>
                            <td>
                                <asp:TextBox ID="txtTreaIn" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                <%-- <asp:TextBox ID="txtTreaIn" CssClass="select_input" Width="120px" imgclick="selectTreaIn" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hfldTreaIn" runat="server" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">资源类别
                            </td>
                            <td>
                                <asp:TextBox ID="txtCategory" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>    </tr>
                        <tr>
                            <td class="descTd">规格
                            </td>
                            <td>
                                <asp:TextBox ID="txtSpecification" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">品牌
                            </td>
                            <td>
                                <asp:TextBox ID="txtBrand" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>    </tr>
                        <tr>
                            <td class="descTd">型号
                            </td>
                            <td >
                                <asp:TextBox ID="txtModelNumber" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="descTd">
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btnSearch_Click" runat="server" />
                                 <input type="button" id="btnUnSelect" value="取消查询" style="width:auto" onclick="ifshow();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" class="tab">
                        <tr>
                            <td class="divFooter" style="text-align: left">
                               <input type="button" id="btnSelect" value="高级查询" style="width:auto" onclick="ifshow();" />
                                <asp:Button ID="btnExcel" Width="80px" Text="导出Excel" CommandName="excel" OnCommand="btnExcel_Command" runat="server" Style="display: none;" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 100%;">
                                <div class="">
                                    <asp:GridView ID="gvwStorage"  CssClass="gvw" Width="100%" AllowPaging="true" AutoGenerateColumns="false" EnableViewState="false" OnPageIndexChanging="gvwStorage_PageIndexChanging" OnRowDataBound="gvwStorage_RowDataBound" DataKeyNames="acode" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单据号">
                                                <ItemTemplate>
                                                    <span class="al" onclick="rowQuery('<%# Eval("aid") %>')">
                                                        <%# Eval("acode") %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ResourceCode" HeaderText="资源编号" />
                                            <asp:TemplateField HeaderText="资源名称">
                                                <ItemTemplate>
                                                    <span class="tooltip" title='<%# Eval("resourceName").ToString() %>'>
                                                        <%# StringUtility.GetStr(Eval("resourceName").ToString(), 10) %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="资源类别">
                                                <ItemTemplate>
                                                    <%# StringUtility.GetStr(System.Convert.ToString(Eval("ResourceTypeName"))) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="规格">
                                                <ItemTemplate>
                                                    <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                                        <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="品牌">
                                                <ItemTemplate>
                                                    <%# Eval("Brand") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ModelNumber" HeaderText="型号" />
                                            <asp:TemplateField HeaderText="技术参数">
                                                <ItemTemplate>
                                                    <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                                        <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="单位" />
                                            <asp:BoundField DataField="sprice" HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" />
                                            <asp:BoundField DataField="number" HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" />
                                            <asp:BoundField DataField="total" HeaderText="总价" HtmlEncode="false" DataFormatString="{0:f3}" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" />
                                            <asp:BoundField DataField="CorpName" HeaderText="供应商" />
                                            <asp:BoundField DataField="intime" HeaderText="调拨日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                            <asp:BoundField HeaderText="调出库" DataField="TAName" />
                                            <asp:BoundField HeaderText="接收库" DataField="TBName" />
                                        </Columns>
                                         <PagerStyle CssClass="footerM" />
                                        <RowStyle CssClass="rowa"></RowStyle>
                                        <AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle>
                                        <HeaderStyle CssClass="header"></HeaderStyle>
                                        <FooterStyle CssClass="footer"></FooterStyle>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
