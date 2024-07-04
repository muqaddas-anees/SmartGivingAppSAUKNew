<%@ Page Language="C#" AutoEventWireup="true" Inherits="ProjectNewGanttChart" Codebehind="ProjectNewGanttChart.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <!--Ext and ux styles -->
    <link href="../../Content/ext/Resources/Css/ext-all.css" rel="stylesheet" type="text/css" />

	<!--Scheduler styles-->
    <link href="../../Content/ext/Resources/Css/sch-gantt-all.css" rel="stylesheet" type="text/css" />
 	<!--Application CSS files-->
    
	<link href="../../Content/ext/Resources/Css/style.css" rel="stylesheet" type="text/css" />

	<!--Implementation specific styles-->
    <link href="../../Content/ext/examples/advanced/advanced.css" rel="stylesheet" type="text/css" />
   
    <link href="../../Content/ext/examples/css/examples.css" rel="stylesheet" type="text/css" />
    
      <link href="../../Content/ext/examples/assigningresources/resources.css" rel="stylesheet" type="text/css" />
      <link href="../../Content/ext/examples/styles/css/common.css" rel="stylesheet" type="text/css" />
        <link href="../../Content/ext/examples/styles/css/style1.css" rel="stylesheet" type="text/css" />
	<!--Ext lib and UX components-->
    <script src="../../Scripts/ext/ext-all-gantt.js?3" type="text/javascript"></script>
   
	<!--Gantt components-->
    <script src="../../Scripts/ext/gnt-all-debug.js?3" type="text/javascript"></script>

    <!--Application files-->
    <script src="../../Scripts/ext/DemoGanttPanel.js?1" type="text/javascript"></script>
    <script src="../../Scripts/ext/OverrideVer2.js?3" type="text/javascript"></script> 
     <script src="../../Scripts/ext/advanced.js?1" type="text/javascript"></script>
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
