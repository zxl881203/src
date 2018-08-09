﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageEdit.aspx.cs" Inherits="StockManage_Storage_StorageEdit" %>

<%@ Import Namespace="cn.justwin.BLL" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>入库单设置</title>
            <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="" />
    <meta name="HandheldFriendly" content="True" />
    <meta name="MobileOptimized" content="320" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="telephone=no" name="format-detection" />
    <meta content="email=no" name="format-detection" />
    <script type="text/javascript" src="/Script/jquery-3.2.1.min.js"></script>
   <script type="text/javascript" src="/Script/layer_v3.1.1/layer/layer.js"></script>

    <link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="/Script/jquery.js"></script>--%>
    <script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
    <script type="text/javascript" src="../script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../script/Validation.js"></script>
    <script type="text/javascript" src="../script/ValidateInput.js"></script>
    <script type="text/javascript" src="../script/Config2.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript">
       
        $(document).ready(function () {
            $('#btnBindResource').hide();
            if (getRequestParam('Action') == 'Query') {
                setAllInputDisabled();
            }
            addEvent(document.getElementById('btnCancel'), 'click', btnCancelEvent);
            addEvent(document.getElementById('btnPickPurchase'), 'click', pickPurchase);
            document.getElementById('btnDelete').onclick = deleteStorageStock;
            document.getElementById('btnSave').onclick = btnSaveEvent;
            replaceEmptyTable('emptyStock', 'gvwStorageStock');
            var stockTable = new JustWinTable('gvwStorageStock');
            showTooltip('tooltip', 10);

            //设置项目和仓库为只读
            document.getElementById('txtProject').setAttribute('readOnly', 'readOnly');
            document.getElementById('txtTrea').setAttribute('readOnly', 'readOnly');

            // 总分模式
            if ($('#hfldStorageMode').val() == '1') {
                window['selectTrea'] = function () { return false; };
                window['selectTreaWX'] = function () { return false; };
            }
        });
        //选择项目
        function openProjPickerWX() {
            var url = '/Common/DivSelectProject2WX.aspx';
            //viewopen(url);
            //jw.selectOneProject({ idinput: 'hfldProject', nameinput: 'txtProject' });
            layer.open({
                type: 2,
                title: '选择项目',
                shadeClose: true,
                shade: 0.8,
                area: ['100%', '100%'],
                content: url
            });
            
        }

        function pickPurchase() {
            var url = '/StockManage/Storage/PurchaseListWX.aspx?prjId=' + $('#hfldProject').val();
            layer.open({
                type: 2,
                title: '从采购单中选择',
                shadeClose: true,
                shade: 0.8,
                area: ['100%', '100%'],
                content: url
            });
        }
       //关闭
        function btnCancelEvent() {
            page_close();
            //window.close();
        }

        //注意：parent 是 JS 自带的全局对象，可用于操作父页面
        var index = parent.layer.getFrameIndex(window.name); //获取窗口索引
        // 关闭
        function page_close() {
            parent.layer.close(index);
        }

        function deleteStorageStock() {
            if (!confirm("确定要删除吗？")) {
                return false;
            }
        }

        function btnSaveEvent() {
            if (!validateInput()) {
                return false;
            }
        }

        // 检查数据是否合法
        function validateInput() {
            if (!document.getElementById('txtTrea').value) {
                top.ui.alert('仓库名称不能为空');
                return false;
            }
            if (!document.getElementById('gvwStorageStock') || $('#gvwStorageStock tr').length == 1) {
                top.ui.alert('请从采购单中选择物资');
                return false;
            }

            // 检查累计入库数量是否大于合同数量
            var nums = $('#gvwStorageStock .number');
            for (var i = 0; i < nums.length; i++) {
                var num = nums[i];
                var number = parseFloat($(num).val().replace(/\,/g, ''));   // 本次入库数量
                var conNumber = parseFloat($(num).attr('conNumber'));       // 合同数量
                var inNumber = parseFloat($(num).attr('inNumber'));         // 已入库数量
                if (number + inNumber > conNumber) {
                    top.ui.alert('您的累计入库数量大于合同数量，请修改后保存。');
                    return false;
                }
            }
            return true;
        }

        function selectTrea() {
            jw.selectTreasury({ codeinput: 'hfldTrea', nameinput: 'txtTrea', module: 'import' });
        }
        function selectTreaWX() {
            var url = "/Common/SelectTreasuryWX.aspx?module=import";
            layer.open({
                type: 2,
                title: '选择仓库',
                shadeClose: true,
                shade: 0.8,
                area: ['100%', '100%'],
                content: url
            });
        }
        function setGvTotalNumber() {
            var totalCount = 0.0;
            var number = 0.0;
            var sprice = 0.0;
            $('#gvwStorageStock tr[id]').each(function () {
                number = $(this).find('input[id$=txtNumber]').val();
                sprice = $(this).find('span[id$=lbSprice]').text();
                var total = number * sprice;
                $(this).find('span[id$=lbTotal]').text(total.toFixed(3));
                totalCount += total;
            });
            $('#gvwStorageStock').find('span[class=_total_]').text(totalCount.toFixed(3));
        }
    </script>
     <style type="text/css">
        .gvw{
            min-width: 700px;
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
<%--        <div class="divHeader2">
            <table class="tableHeader">
                <tr>
                    <td>
                        <asp:Label ID="lblTitle" Font-Bold="true" Text="入库单" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>--%>
        <div class="divContent2">
            <table class="tableContent2" cellpadding="5px" cellspacing="0">
                <tr>
                    <td class="word" style="white-space: nowrap;">入库编号
                    </td>
                    <td class="txt readonly" id="ttt">
                        <asp:TextBox ID="txtScode" ReadOnly="true" Height="15px" Width="100%" runat="server"></asp:TextBox>
                    </td>
                    <td class="word" style="white-space: nowrap;">入库日期
                    </td>
                    <td class="txt readonly">
                        <asp:TextBox ReadOnly="true" ID="txtInputDate" Width="100%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word" style="white-space: nowrap;">仓库名称
                    </td>
                    <%-- <td class="txt mustInput" style="display:none;">
                       <asp:DropDownList ID="dropTrea" runat="server" >
                         </asp:DropDownList>
                    </td>--%>
                    <td class="txt mustInput word" >
                        <asp:TextBox ID="txtTrea" CssClass="select_input" imgclick="selectTreaWX" Width="100%" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hfldTrea" runat="server" />
                    </td> <td class="word" style="white-space: nowrap;">编制人
                    </td>
                    <td class="txt readonly">
                        <asp:TextBox ID="txtPerson" Height="15px" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td  style="white-space: nowrap;">项目
                    </td>
                   <%--  <td class="txt" style="display:none;">
                       <asp:DropDownList ID="dropProject" runat="server" >
                         </asp:DropDownList>
                    </td>--%>
                    <td class="txt" >
                        <asp:TextBox ID="txtProject" CssClass="select_input" imgclick="openProjPickerWX" Width="100%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="word" style="vertical-align: top; padding-top: 7px; white-space: nowrap;">说明
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtExplain" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td class="word" style="white-space: nowrap;">附件
                    </td>
                    <td colspan="3" style="padding-right: 0px;">
                        <MyUserControl:epc_usercontrol1_filelink2_ascx ID="flAnnx" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="tableContent2" style="margin-left: auto;" cellpadding="5px" cellspacing="0">
                <tr>
                    <td style="height: 10px">
                        <hr class="sp" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="headerDiv" style="text-align: left;">
                            <input type="button" id="btnPickPurchase" style="width: 150px" value="从采购单中选择" />
                            <asp:Button ID="btnDelete" Text="删除" OnClick="btnDelete_Click" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr style="vertical-align: top">
                    <td>
                        <div class="pagediv">
                            <asp:GridView ID="gvwStorageStock" CssClass="gvw"  AutoGenerateColumns="false" OnPageIndexChanging="gvwStorageStock_PageIndexChanging" OnRowDataBound="gvwStorageStock_RowDataBound" DataKeyNames="ResourceCode" runat="server">
                                <EmptyDataTemplate>
                                    <table id="emptyStock" class="gvdata">
                                        <tr class="header">
                                            <td style="width: 20px" scope="col">
                                                <input type="checkbox" id="ch1" />
                                            </td>
                                            <td style="width: 30px" align="center" scope="col">序号
                                            </td>
                                            <td scope="col" align="center">物资编号
                                            </td>
                                            <td scope="col" align="center">物资名称
                                            </td>
                                            <td scope="col" align="center">规格
                                            </td>
                                            <td scope="col" align="center">型号
                                            </td>
                                            <td scope="col" align="center">品牌
                                            </td>
                                            <td scope="col" align="center">技术参数
                                            </td>
                                            <td scope="col" align="center">单位
                                            </td>
                                            <td scope="col" align="center">合同数量
                                            </td>
                                            <td scope="col" align="center">累计已入库数量
                                            </td>
                                            <td scope="col" align="center">数量
                                            </td>
                                            <td scope="col" align="center">价格
                                            </td>
                                            <td scope="col" align="center">小计
                                            </td>
                                            <td align="center">供应商
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" />
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectSingle" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序号" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ResourceCode" HeaderText="物资编号" ItemStyle-Width="50px"/>
                                    <asp:TemplateField HeaderText="物资名称" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="型号" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="品牌" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("brand").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="技术参数" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UnitName" HeaderText="单位" ItemStyle-Width="20px"/>
                                    <asp:TemplateField HeaderText="合同数量" ItemStyle-CssClass="decimal_input" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Eval("ContractNumber") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="累计已入库数量" ItemStyle-CssClass="decimal_input" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <%# Eval("AllInNumber") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNumber" Width="50px" Style="text-align: right;" onblur="setGvTotalNumber()" CssClass="decimal_input number" Text='<%# System.Convert.ToString((Eval("number").ToString() == "") ? "0.000" : System.Convert.ToDecimal(Eval("number")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' conNumber='<%# System.Convert.ToString(Eval("ContractNumber"), System.Globalization.CultureInfo.CurrentCulture) %>' inNumber='<%# System.Convert.ToString(Eval("AllInNumber"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="价格" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbSprice" CssClass="decimal_input" Text='<%# System.Convert.ToString(Eval("sprice"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="小计" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbTotal" CssClass="decimal_input" Text='<%# System.Convert.ToString(System.Convert.ToDecimal(Eval("Total")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="供应商" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <span class="tooltip" title='<%# Eval("CorpName").ToString() %>'>
                                                <%# StringUtility.GetStr(Eval("CorpName").ToString(), 10) %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="rowa"></RowStyle>
                                <AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle>
                                <HeaderStyle CssClass="header"></HeaderStyle>
                                <FooterStyle CssClass="footer"></FooterStyle>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="btnSave" Text="保存" OnClick="btnSaveWX_Click" runat="server" />
                        <input type="button" id="btnCancel" value="取消" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divSelectFromPurchase" title="从采购单中选择" style="display: none">
            <iframe id="ifResoueceFromPurchase" frameborder="0" width="100%" height="100%" src="PurchaseList.aspx"></iframe>
        </div>
        <div id="divFram" title="" style="display: none">
            <iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
        </div>
        <!--项目编码-->
        <asp:HiddenField ID="hfldPid" runat="server" />
        <asp:HiddenField ID="hfldProject" runat="server" />
        <asp:HiddenField ID="hfldPurchaseCode" runat="server" />
        <asp:HiddenField ID="hfldResourceCode" runat="server" />
        <asp:HiddenField ID="hfldStorageMode" runat="server" />
        <asp:Button ID="btnBindResource" Text="" OnClick="btnBindResource_Click" runat="server" />
    </form>
</body>
</html>
