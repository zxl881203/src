﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanRatifyDetail.aspx.cs" Inherits="ProgressManagement_Plan_PlanRatifyDetail" %>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>计划审核查看</title>

    <script type="text/javascript" src="../../Script/jquery.js"></script>

    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            setIfFrameSrc();
        });

        function setIfFrameSrc() {
            //计划版本Id
            var progressVerId = '';
            var id = getRequestParam('progressId');
            $.ajax({
                type: "GET",
                dataType: "text",
                async: false,
                url: "../../ProgressManagement/Handler/GetProgressVerId.ashx?" + new Date().getTime() + '&id=' + id ,
                success: function(data) {
                progressVerId = data;
                    loadGantt(progressVerId);
                },
                error: function() {
                    alert("error");
                }
            });
        }
        
        function loadGantt(progressVerId) {
            var url = "/ProgressManagement/Gantt/PlanView.htm?id=" + progressVerId;
            $('#ifPlusGantt').attr("src", url);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="divBudget" class="pagediv" style="overflow: hidden; width: 100%; height: 100%;">
        <iframe id="ifPlusGantt" style="width: 100%; height: 100%;" frameborder="0" scrolling="auto">
        </iframe>
    </div>
    </form>
</body>
</html>
