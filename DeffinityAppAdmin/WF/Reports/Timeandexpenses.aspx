<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Reports_Timeandexpenses" Codebehind="Timeandexpenses.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://www.deffinity.com/dlite/media//favicon1.ico" rel="shortcut icon" />
    <%--<link rel="stylesheet" type="text/css" href="../stylcss/deffinity_frame.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_color_scheme.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_custom.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/ajaxtabs.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/customer_admin.css" />--%>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
        <%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
        
<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>


<div class="row">
          <div class="col-xs-12">
 <strong>Time and Expenses Report </strong> 
<hr class="no-top-margin" />
	</div>
</div>        

<div class="row">
          <div class="col-xs-12">
               <asp:ValidationSummary ID="V1" runat="server" ValidationGroup="one" />
                  
 <asp:CompareValidator ID="c1" runat="server" ControlToCompare="txt_StartDate" ControlToValidate="txt_EndDate"
                        Display="none" Type="Date" Operator="GreaterThanEqual" ErrorMessage="start date can not greater then end date" ValidationGroup="one" ></asp:CompareValidator>      
                        
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_StartDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one" >*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_EndDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one">*</asp:RegularExpressionValidator>
	</div>
</div>
        <div>
            
<div class="form-group">
      <div class="col-xs-3">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Resource%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlcustomer_Timesheet" runat="server" Width="160px"  DataSourceID="DDLCustomer"
                                                DataTextField="ContractorName" DataValueField="ContractorID" >
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DDLCustomer" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                        SelectCommand="DN_ContractorTimeSheet" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="Project" QueryStringField="project" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
            </div>
	</div>
	<div class="col-xs-3">
           <label class="col-sm-3 control-label">Start&nbsp;date:</label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txt_StartDate" runat="server" SkinID="Date" MaxLength="10">
                        </asp:TextBox>
                        <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender" />
            </div>
	</div>
	<div class="col-xs-3">
           <label class="col-sm-3 control-label">End&nbsp;date:</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txt_EndDate" runat="server" SkinID="Date" MaxLength="10" />
                        <asp:Label ID="Image1" runat="server" SkinID="Calender"  />
            </div>
	</div>
    <div class="col-xs-3">
         <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="horizontal"   >
<asp:ListItem Value="A" Selected="true">All</asp:ListItem>
<asp:ListItem  Value ="B" >Buying Price</asp:ListItem>
<asp:ListItem  Value ="C" >Selling Price</asp:ListItem>
        </asp:RadioButtonList>  
        </div>
</div>
            <div class="form-group">
      <div class="col-xs-4">
         
	</div>
	<div class="col-xs-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
	<div class="col-xs-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
                <asp:Button ID="btn_Submitt" runat="server" SkinID="btnView" OnClick="btn_Submitt_Click"
                            ValidationGroup="one"  />
            </div>
	</div>
</div>

          
        </div>
       <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                            PopupButtonID="imgbtnenddate6" TargetControlID="txt_StartDate" CssClass="MyCalendar">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                            PopupButtonID="Image1" TargetControlID="txt_EndDate" CssClass="MyCalendar">
                        </ajaxToolkit:CalendarExtender>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>
    </form>
</body>
</html>
