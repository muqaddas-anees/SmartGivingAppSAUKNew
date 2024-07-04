<%@ Page Language="C#" AutoEventWireup="true" Inherits="ProjectGanntNewReadOnly" Codebehind="ProjectGanntNewReadOnly.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <!--Ext and ux styles -->
    <link href="resources/css/ext-all.css?4" rel="stylesheet" type="text/css" />

	<!--Scheduler styles-->
    <link href="Resources/Css/sch-gantt-all.css?4" rel="stylesheet" type="text/css" />
 	<!--Application CSS files-->
    
	<link href="Resources/Css/style.css?4" rel="stylesheet" type="text/css" />

	<!--Implementation specific styles-->
    <link href="examples/advanced/advanced.css?4" rel="stylesheet" type="text/css" />

    <link href="examples/css/examples.css?4" rel="stylesheet" type="text/css" />
    
      <link href="examples/assigningresources/resources.css?4" rel="stylesheet" type="text/css" />
      <link href="examples/styles/css/common.css?4" rel="stylesheet" type="text/css" />
        <link href="examples/styles/css/style1.css?4" rel="stylesheet" type="text/css" />
	<!--Ext lib and UX components-->
    <script src="js/ext-all-gantt.js?4" type="text/javascript"></script>

	<!--Gantt components-->
    <script src="js/gnt-all-debug.js?4" type="text/javascript"></script>

    <!--Application files-->
    <script src="js/DemoGanttPanel.js?4" type="text/javascript"></script>
    <script src="js/OverrideVer2.js?4" type="text/javascript"></script> 
     <script src="js/advancedReadOnly.js?4" type="text/javascript"></script>
    <title>Gantt demo</title>
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