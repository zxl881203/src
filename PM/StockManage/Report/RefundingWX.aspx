﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Refunding.aspx.cs" Inherits="StockManage_Report_Refunding" %>

<%@ Import Namespace="cn.justwin.BLL" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>退库报表</title>
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
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.ifshow').hide();
            // 添加验证
            $('#btn_Search')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
            var jwTable = new JustWinTable('gvPurchaseplan');
            showTooltip('tooltip', 10);
        });

        //选择项目
        function openProjPicker() {
            jw.selectOneProject({ nameinput: 'txtProjectName' });

        }

        function pickCorp() {
            jw.selectOneCorp({ idinput: 'hfldCorp', nameinput: 'txtCorp' });
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
            ///StockManage/Refunding/ViewRefunding.aspx?ic=<%# Eval("rid") %>&t=1
            var url = '/StockManage/Refunding/ViewRefundingWX.aspx?ic=' + id + "&t=1";;
            layer.open({
                type: 2,
                title: '查看退库报表',
                shadeClose: true,
                shade: 0.8,
                area: ['100%', '100%'],
                content: url
            });
        }
    </script>
    <style type="text/css">
        .gvw {
            min-width: 800px;
        }

            .gvw tr {
                height: 30px;
            }

        .footerM {
            /*color:red;*/
        }

            .footerM td table tbody tr td span {
                font-size: 12px;
                margin: 5px;
                border: 1px solid #b5b2b2;
                padding: 3px;
                margin-left: 10px;
                background-color: #fbfbfb;
                color: red;
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
                            </td> </tr>
                        <tr>
                            <td class="descTd">结束时间
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">退库编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td> </tr>
                        <tr>
                            <td class="descTd">资源类别
                            </td>
                            <td>
                                <asp:TextBox ID="txtCategory" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">资源名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtResourceNames" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td> </tr>
                        <tr>
                            <td class="descTd">资源编号
                            </td>
                            <td>
                                <asp:TextBox ID="txtResourceCodes" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 25px;">
                            <td class="descTd">项目名称
                            </td>
                            <td>
                                <input type="text" style="width: 120px;" id="txtProjectName" class="easyui-validatebox " data-options="validType:'validQueryChars[50]'" imgclick="openProjPicker" runat="server" />

                            </td> </tr>
                        <tr>
                            <td class="descTd">供应商
                            </td>
                            <td>
                                <asp:TextBox ID="txtCorp" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                                <%-- <asp:TextBox ID="txtCorp" CssClass="txtreadonly easyui-validatebox " Style="width: 120px;" ToolTip="请选择" data-options="validType:'validQueryChars[50]'" imgclick="pickCorp" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hfldCorp" Value="" runat="server" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">规格
                            </td>
                            <td>
                                <asp:TextBox ID="txtSpecification" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td> </tr>
                        <tr>
                            <td class="descTd">品牌
                            </td>
                            <td>
                                <asp:TextBox ID="txtBrand" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd">型号
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtModelNumber" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="descTd"></td>
                            <td >
                                <asp:Button ID="btn_Search" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
                                <input type="button" id="btnUnSelect" value="取消查询" style="width: auto" onclick="ifshow();" />
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

                                <input type="button" id="btnSelect" value="高级查询" style="width: auto" onclick="ifshow();" />
                                <asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnexecl_Click" runat="server" Style="display: none;" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 100%; vertical-align: top;">
                                <div class="">
                                    <asp:GridView ID="gvPurchaseplan" CssClass="gvw" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPurchaseplan_PageIndexChanging" DataKeyNames="rcode" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="退库编号">
                                                <ItemTemplate>
                                                    <span class="al" onclick="rowQuery('<%# Eval("rid") %>')">
                                                        <%# Eval("rcode") %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="仓库名称">
                                                <ItemTemplate>
                                                    <%# Eval("tname") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="资源编号">
                                                <ItemTemplate>
                                                    <%# Eval("ResourceCode") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="资源名称">
                                                <ItemTemplate>
                                                    <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                                        <%# StringUtility.GetStr(System.Convert.ToString(Eval("ResourceName")), 10) %>
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
                                                        <%# StringUtility.GetStr(System.Convert.ToString(Eval("Specification")), 10) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="品牌">
                                                <ItemTemplate>
                                                    <%# Eval("Brand") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="型号">
                                                <ItemTemplate>
                                                    <%# Eval("ModelNumber") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="技术参数">
                                                <ItemTemplate>
                                                    <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                                        <%# StringUtility.GetStr(System.Convert.ToString(Eval("TechnicalParameter")), 10) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单位">
                                                <ItemTemplate>
                                                    <%# Eval("Name") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input">
                                                <ItemTemplate>
                                                    <%# Eval("sprice") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input">
                                                <ItemTemplate>
                                                    <%# Eval("number") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input">
                                                <ItemTemplate>
                                                    <%# System.Convert.ToDecimal(string.IsNullOrEmpty(Eval("xjsprice").ToString()) ? 0 : Eval("xjsprice")).ToString("0.000") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="供应商">
                                                <ItemTemplate>
                                                    <%# StringUtility.GetStr(Eval("corpName").ToString()) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="项目名称">
                                                <ItemTemplate>
                                                    <span class="tooltip" title='<%# Eval("prjName").ToString() %>'>
                                                        <%# StringUtility.GetStr(Eval("prjName").ToString(), 10) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="申请日期">
                                                <ItemTemplate>
                                                    <%# Common2.GetTime(Eval("intime").ToString()) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
        <asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
    </form>
</body>
</html>
