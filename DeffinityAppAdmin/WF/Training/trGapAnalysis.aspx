<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trGapAnalysis" Codebehind="trGapAnalysis.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register Src="controls/dropdownView.ascx" TagName="DropDownList" TagPrefix="uc2" %>


<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs2" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Training%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
    Dashboard -
  <%= Resources.DeffinityRes.TrainingGapAnalysis%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="panel_options" Runat="Server">
  <uc2:DropDownList ID="dropDownList1" runat="server" />  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
div#tipDiv {
    padding:4px;
   
    color:#000; font-size:13px; line-height:1.2;
    background-color:#F5F5F5; border:1px solid #667295; 
    width:200px; 
}
</style>
    <style type="text/css">

	.tableBoxOuter {
		width:900px;height:36em; 
		background: #FFFFFF;
		
	}

	.scrolltable  th {
		font-size: 10px;
		font-family: verdana, arial, sans-serif;
		margin:0em;
		padding-top: 4px;
		padding-bottom: 4px;
		padding-right: 0px;
		padding-left: 4px;
		table-layout:fixed;
		white-space:nowrap;
	}

</style>
<script type="text/javascript" src="../js/LockScroll.js"> 
</script>
<script src="../js/dw_event.js" type="text/javascript"></script>
<script src="../js/dw_viewport.js" type="text/javascript"></script>
<script src="../js/dw_tooltip.js" type="text/javascript"></script>
<script src="../js/dw_tooltip_aux.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    function employeeDetails(s) {

        // alert(s)E1E5F1;
        var name,title,date,status
        var splitResult = s.split("$");

        name = splitResult[0].replace("_", " ");
        title = splitResult[1].replace("_", " ");
        date = splitResult[2].replace("_", " ");
        status = splitResult[3].replace("_", " ");
        status = status.replace("_", " ");
       // alert(status[1]);
        dw_Tooltip.defaultProps = {
            hoverable: true
        }

        dw_Tooltip.content_vars = {

        L2: '<b>Name:  </b>' + name + '</br><b>Course Title:  </b>' + title + '<br><b>Booking Date:  </b>' + date + '</br><b>Status:  </b>' + status
        }
    }
    function emp() {
        dw_Tooltip.defaultProps = {
            hoverable: true
        }

        dw_Tooltip.content_vars = {

            L2: 'cgjdskdskjfdskfjbd dfjdsfkjfb jdfkjdfdjf jfjdfdjfdf fddfds'
        }
    }

</script>

    <div class="form-group">
        <div class="col-xs-4">
            <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Department%> </label>
             <div class="col-sm-8">
                   <asp:DropDownList ID="ddlDepartment" runat="server"
                               onselectedindexchanged="ddlDepartment_SelectedIndexChanged" 
                               AutoPostBack="True"></asp:DropDownList>
             </div>
        </div>
          <div class="col-xs-4">
                 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Area%> </label>
                <div class="col-sm-8">
                        <asp:DropDownList ID="ddlArea" runat="server"></asp:DropDownList>
             </div>
          </div>
          <div class="col-xs-2">
               <asp:Button ID="btnView" runat="server" SkinID="btnDefault" Text="View" onclick="btnView_Click" />
          </div>
    </div>
    <div class="form-group">
           <asp:Literal ID="ltlGapAnalysis" runat="server"></asp:Literal>
    </div>
</asp:Content>


