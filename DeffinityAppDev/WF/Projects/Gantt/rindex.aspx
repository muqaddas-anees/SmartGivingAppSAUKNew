<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rindex.aspx.cs" Inherits="DeffinityAppDev.WF.Projects.Gantt.rindex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    
    <!--Ext and ux styles -->
     <link href="https://cdnjs.cloudflare.com/ajax/libs/extjs/6.0.1/classic/theme-triton/resources/theme-triton-all.css" rel="stylesheet" type="text/css"/>
    <%--<link href="http://www.bryntum.com/examples/extjs-6.0.1/build/classic/theme-triton/resources/theme-triton-all.css" rel="stylesheet" type="text/css"/>--%>
    <!-- Gantt styles -->
    <link href="resources/css/sch-gantt-triton-all.css?ver=" rel="stylesheet" type="text/css" />
    <!-- Application styles -->
    <link href="resources/advanced/resources/app.css" rel="stylesheet" type="text/css" />
    
    <!--Ext lib -->
    <script src="<%=ResolveClientUrl("~/WF/Projects/Gantt/jsReadonly/ext-all.js") %>" crossorigin="anonymous" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/WF/Projects/Gantt/jsReadonly/theme-triton.js") %>" type="text/javascript"></script>
    <!--<script src="http://www.bryntum.com/examples/extjs-6.0.1/build/ext-all.js" crossorigin="anonymous" type="text/javascript"></script>
    <script src="http://www.bryntum.com/examples/extjs-6.0.1/build/classic/theme-triton/theme-triton.js" type="text/javascript"></script>-->
    <!-- Gantt components -->
    <script src="<%=ResolveClientUrl("~/WF/Projects/Gantt/jsReadonly/gnt-all-debug.js?ver=") %>" type="text/javascript"></script>
    <!-- Application -->
    <script src="<%=ResolveClientUrl("~/WF/Projects/Gantt/jsReadonly/app.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/WF/Projects/Gantt/jsReadonly/appExtent.js") %>" type="text/javascript"></script>

    <style>
        .x-panel-default-outer-border-rbl {
            border-width:0px;
        }
        .x-combo-checker { vertical-align:central; background-repeat: no-repeat; height: 14px; width: 14px; display: inline-block; background-image: url("../../../Content/ext/Resources/themes/images/gray/grid/unchecked.gif");} 
.x-boundlist-selected .x-combo-checker { background-image: url("../../../Content/ext/Resources/themes/images/gray/grid/checked.gif"); }
    </style>
    <title>Gantt</title>
</head>
<body id="gantt_boday">
    <form id="form1" runat="server">
    <asp:Panel ID="gantt" runat="server">
     <div id="north">
    
    </div>
    </asp:Panel>
    </form>
</body>
</html>
