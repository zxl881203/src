﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendFileList.aspx.cs" Inherits="SendFileList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>发文信息列表</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<script language="javascript" type="text/javascript">
	function doClickRow(fileID,isPigeonhole)
	{
	//	document.getElementById('btnDel').disabled = false;
		document.getElementById('btnViewSign').disabled = false;
		if (isPigeonhole == '1')
		{
		    document.getElementById('btnEdit').disabled = true;
		    document.getElementById('btnGiveOut').disabled = true;
		    document.getElementById('btnSend').disabled = true;
		    document.getElementById('btnOnHole').disabled = true;
		}
		else
		{
		    document.getElementById('btnEdit').disabled = false;
		    document.getElementById('btnGiveOut').disabled = false;
		    document.getElementById('btnSend').disabled = false;
		    document.getElementById('btnOnHole').disabled = false;
		}
		document.getElementById('btnView').disabled = false;
		document.getElementById('hdnFileID').value=fileID;
	}
	function openClassEdit(op,corpCode,userCode)
	{				
		var fileID="";
		if (op==2 || op==3)
		{
			fileID = document.getElementById('hdnFileID').value;
		}	
		var url= "SendFileEdit.aspx?t=" + op + "&code=" + corpCode + "&fid="+ fileID + "&ucd="+userCode;
		return window.showModalDialog(url,window,"dialogHeight:260px;dialogWidth:550px;center:1;help:0;status:0;");
	}
	function openSelect(op)
	{
	    var p = new Array();
		p[0] = "";
		p[1] = "";
	    var fileID = "";
	    fileID = document.getElementById('hdnFileID').value;
	    var url = "";
	    if (op == 1)
	    {
	        url = "../../CommonWindow/consignee.aspx";
	        window.showModalDialog(url,p,"dialogHeight:422px;dialogWidth:659px;center:1;help:0;status:0;");
	        if (p[0]!="")
	        {
			    document.getElementById('hdnUserCode').value = p[0];
			    document.getElementById('hdnUserName').value = p[1];
		    }
		    else
		    {
		        document.getElementById('hdnUserCode').value = "";
			    document.getElementById('hdnUserName').value = "";
		    }
	    }
	    else if(op == 2)
	    {
	        url = "DocDispense.aspx?fid="+fileID;
	        return window.showModalDialog(url,window,"dialogHeight:300px;dialogWidth:550px;center:1;help:0;status:0;");
	    }
	    else
	    {
	        url = "ViewSign.aspx?fid="+fileID+"&types=send";
	        return window.showModalDialog(url,window,"dialogHeight:300px;dialogWidth:550px;center:1;help:0;status:0;");
	    }
	}
	</script>
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
       <table width="100%"  height="100%" border="0" id="table1" class="table-normal">
    <tr>
        <TD class="td-title" align="center" height="28">发文信息列表</TD>
    </tr>
    <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" colspan="3" runat="server">
		    <input type="hidden" id="hdnUserCode" name="hdnUserCode" style="WIDTH: 10px" runat="server" />

		    <input type="hidden" id="hdnUserName" name="hdnUserName" style="WIDTH: 10px" runat="server" />

			<input type="hidden" id="hdnFileID" name="hdnFileID" style="WIDTH: 10px" runat="server" />

			
			<asp:Button ID="btnEdit" Text="发文登记" Enabled="false" OnClick="btnEdit_Click" runat="server" />
			
			<asp:Button ID="btnGiveOut" Text="文件分发" Enabled="false" OnClick="btnGiveOut_Click" runat="server" />
			<asp:Button ID="btnViewSign" Text="查看签收" Enabled="false" runat="server" />
			<asp:Button ID="btnSend" Text="文件发送" Enabled="false" runat="server" />
            <asp:Button ID="btnView" Enabled="false" Text="查  看" runat="server" />
			<asp:Button ID="btnOnHole" Text="归  档" Enabled="false" OnClick="btnOnHole_Click" runat="server" /></td></tr>
    <tr>
        <TD vAlign="top">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
				<asp:DataGrid ID="dgFileList" CssClass="grid" DataKeyField="FileID" AutoGenerateColumns="false" Width="100%" AllowCustomPaging="true" OnItemDataBound="dgFileList_ItemDataBound" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="FileNumber" HeaderText="文件号"></asp:BoundColumn><asp:BoundColumn DataField="Title" HeaderText="公文标题"></asp:BoundColumn><asp:BoundColumn DataField="SignUserName" HeaderText="签发人"></asp:BoundColumn><asp:BoundColumn DataField="DraftDept" HeaderText="拟搞部门"></asp:BoundColumn><asp:BoundColumn DataField="IssueRange" HeaderText="发放范围"></asp:BoundColumn><asp:BoundColumn DataField="IssuePage" HeaderText="发文页码"></asp:BoundColumn><asp:BoundColumn DataField="ShareNumber" HeaderText="印刷份数"></asp:BoundColumn><asp:BoundColumn DataField="IssueDate" HeaderText="发文日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="" HeaderText="归档状态"></asp:BoundColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
								<asp:Label ID="TxtRemark" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid>
			</div>
		</TD>
    </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
