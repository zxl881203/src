﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTypeManageFrame.aspx.cs" Inherits="oa_FileManage_FileTypeManageFrame" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title></title><meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
    <script language="javascript" type="text/javascript">
    
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="formx" runat="server">
        <table width="100%"  height="100%" cellpadding="0" cellspacing="0" border="0" id="tablex" class="table-normal">
            <tr>
                <td width="25%" valign="top">
                    <iewc:TreeView ID="TVLibrary" ExpandLevel="1" SelectedStyle="color:red;background-color:#ffffff;" runat="server"></iewc:TreeView>
                </td>
                <td width="75%">
                    <iframe id="frmLibrary" name="frmLibrary" src="FileTypeManage.aspx?isFirst=true&uc=&lc=&ln=" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
