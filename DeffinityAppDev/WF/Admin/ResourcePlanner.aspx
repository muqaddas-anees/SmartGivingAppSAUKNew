<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Admin_ResourcePlanner" Codebehind="ResourcePlanner.aspx.cs" %>
<%@ Register src="~/WF/CustomerAdmin/controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>

<%@ Register src="controls/ResourcePlannerTabs.ascx" tagname="ResourcePlannerTabs" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ResourcePlannerTabs ID="ResourcePlannerTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Resources%> 
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
   <%= Resources.DeffinityRes.ResourceScheduler%> <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <link href="../../Content/css/ext-all.css" rel="stylesheet" />
    <script src="../../Content/AnyChart.js" type="text/javascript"></script>
 <script type="text/javascript" src="../../Content/ext-base.js"></script>
        <script type="text/javascript" src="../../Content/ext-core.js"></script>
        <script type="text/javascript" src="../../Content/ext-all.js"></script>
         <%-- <style type="text/css">
            .zoomIn { background-image: url(./icons/zoom_in.png) !important; }
            .zoomOut { background-image: url(./icons/zoom_out.png) !important; }
            .fitAll { background-image: url(./icons/fit_all.png) !important; }            
            .print { background-image: url(./icons/print.png) !important; }
            .save { background-image: url(./icons/save_as_image.png) !important; }
        </style>--%>
<%--<script src="js/ext-all.js" type="text/javascript"></script>--%>

     <script type="text/javascript">
     
         Date.prototype.toDDMMYYYYString = function() { return isNaN(this) ? 'NaN' : [this.getDate() > 9 ? this.getDate() : '0' + this.getDate(), this.getMonth() > 8 ? this.getMonth() + 1 : '0' + (this.getMonth() + 1), this.getFullYear()].join('/') }
         Date.prototype.toMMDDYYYYString = function() { return isNaN(this) ? 'NaN' : [this.getMonth() > 8 ? this.getMonth() + 1 : '0' + (this.getMonth() + 1), this.getDate() > 9 ? this.getDate() : '0' + this.getDate(), this.getFullYear()].join('/') }
         //alert(new Date().toDDMMYYYYString())

         var period;         
         var periodID;
         var resourceInfo;
         var hash = new Array();
         hash["Black"] = "Black";
         hash["#e3319d"] = "Maroon";
         hash["#667c26"] = "Olive";
         hash["c"] = "Navy";
         hash["#8e35ef"] = "Purple";
         hash["#4c4646"] = "Gray";
         hash["c1"] = "Silver";
         hash["#e55451"] = "Red";
         hash["#4cc417"] = "Green";
         hash["#41a317"] = "Lime";
         hash["#fff380"] = "Yellow";
         hash["#b6ffff"] = "Aqua";
         hash["#6698ff"] = "Blue";
         hash["#4C7d7e"] = "Teal";
         


         function periodSelectHandler(e) {
            
             ChangeTabView();
             document.getElementById("<%=btnUpdate.ClientID %>").style.visibility = 'visible';
             document.getElementById("<%=btnDelete.ClientID %>").style.visibility = 'visible';
             document.getElementById("<%=btnInsert.ClientID %>").style.visibility = 'hidden';
             document.getElementById("<%=btnInsert.ClientID %>").style.display = 'none';
             document.getElementById("<%=btnUpdate.ClientID %>").style.display = 'block';
             document.getElementById("<%=btnDelete.ClientID %>").style.display = 'block';
             
             periodID = e.id;
             period = chart.getPeriodInfo(e.id);
             if(period.attributes.Shift_type != 'Vacation')
             {
             resourceInfo = chart.getResourceInfo(period.resourceId);
            
             set_TeamName(resourceInfo.attributes.TeamName);
             set_ResourceName(resourceInfo.name);             
             set_MemebershiftID(period.attributes.Member_ShiftID);
             set_MemberID(period.attributes.MemberID);
             set_TeamType(period.attributes.TeamType);
             set_startdate(period.startDate.toDDMMYYYYString());            
             set_enddate(period.endDate);
             setSite(period.attributes.SiteID);
             SetShift(period.attributes.ShiftID);
             set_notes(period.attributes.Notes);
             }
//             chart.updatePeriod(periodID, period.startDate, period.endDate);
//             chart.commitChanges();
             
         }


         function periodEditingProcess(e) {

             period = chart.getPeriodInfo(e.id);
             resourceInfo = chart.getResourceInfo(period.resourceId);

             set_TeamName(resourceInfo.attributes.TeamName);
             set_ResourceName(resourceInfo.name);
             set_MemebershiftID(period.attributes.Member_ShiftID);
             set_MemberID(period.attributes.MemberID);
             set_TeamType(period.attributes.TeamType);
             set_startdate(period.startDate.toDDMMYYYYString());
             set_enddate(period.endDate);
             setSite(period.attributes.SiteID);
             SetShift(period.attributes.ShiftID);
             set_notes(period.attributes.Notes);
         }
         
         function periodEditingEnd(e) {
             periodID = e.id;
             period = chart.getPeriodInfo(e.id);

            var retval =   ResourcePlanner_Service.Update_TeamMemberShift(period.attributes.MemberID, period.attributes.ShiftID, period.startDate.toDDMMYYYYString(), period.attributes.SiteID, period.attributes.TeamType, period.attributes.Notes, period.endDate.toDDMMYYYYString(), period.attributes.Member_ShiftID,'1');

            //alert(retval);     
             chart.updatePeriod(periodID, period.startDate, period.endDate);
             chart.commitChanges();
             chart.updatePeriodCustomAttributeValue(periodID, "Date_display", period.startDate.toDDMMYYYYString() + ' - ' + DateMinusOne(period.endDate).toDDMMYYYYString());
             chart.commitChanges();

         }
         function DateMinusOne(myDate) {           
                
             myDate.setDate(myDate.getDate() -1);
             return myDate;
         }
        
         function DateConvertion(s_date) {

             var b = s_date;
             var temp = new Array();
             temp = b.split('/');
             var s1 = temp[1] + '/' + temp[0] + '/' + temp[2];
             return s1
         }


         function UpdateData() {

             //alert(get_startdate());
             var ret_val = new Boolean(true);

             var sDate;
             var eDate;
             //check the dates are valid or not
             if ((get_startdate().length == 0) || (get_startdate() == null) || (get_enddate().length == 0) || (get_enddate() == null)) {               
             }
             else {
                 sDate = new Date(DateConvertion(get_startdate()));
                 eDate = new Date(DateConvertion(get_enddate()));
             }

             if ((get_startdate().length == 0) || (get_startdate() == null)) {
                 alert("Please enter From date");
                 ret_val = false;
             }
             else if ((get_enddate().length == 0) || (get_enddate() == null)) {
                 alert("Please enter To date");
                 ret_val = false;
             }
             else if (sDate > eDate) {

                 alert("Please ensure to date is after the from date");
                 ret_val = false;
             }
             else if (ret_val) {
                 ResourcePlanner_Service.Update_TeamMemberShift(get_MemberID(), get_ddl_shift(), get_startdate(), get_ddl_site(), get_TeamType(), get_notes(), get_enddate(), get_MemebershiftID(),'2', OnComplete, OnError, OnTimeOut);
                 UpdateStyle();
                 UpdatePeriod();
                 chart.updatePeriodCustomAttributeValue(periodID, "Date_display", get_startdate() + ' - ' + get_enddate());
                 chart.commitChanges();
             }                      
             return false;
         }

         function UpdatePeriod() {
         //add day to show correct prirod on chart
             var update_todate = new Date(DateConvertion(get_enddate()));
             update_todate.setDate(update_todate.getDate() + 1);

             chart.updatePeriod(periodID, new Date(DateConvertion(get_startdate())), update_todate);
             chart.commitChanges();
         }

         function UpdateStyle() {
             chart.switchPeriodStyle(periodID, hash[get_ddl_shiftName()]);
             chart.commitChanges();
             chart.updatePeriodCustomAttributeValue(periodID, "ShiftID", get_ddl_shift());
             chart.commitChanges();
             chart.updatePeriodCustomAttributeValue(periodID, "Notes", get_notes());
             chart.commitChanges();
         
         }

         function InsertData() {
             var ret_val = new Boolean(true);
             var sDate;
             var eDate;
             //check the dates are valid or not
             if ((get_startdate().length == 0) || (get_startdate() == null) || (get_enddate().length == 0) || (get_enddate() == null)) {
             }
             else {
                 sDate = new Date(DateConvertion(get_startdate()));
                 eDate = new Date(DateConvertion(get_enddate()));
             }
             
             if ((get_startdate().length == 0) || (get_startdate() == null)) {
                 alert("Please enter From date");
                 ret_val = false;
             }
             else if ((get_enddate().length == 0) || (get_enddate() == null)) {
                 alert("Please enter To date");
                 ret_val = false;
             }
             else if (sDate > eDate) {
                 alert("Please ensure to date is after the from date");
                 ret_val = false;
             }
             else if (ret_val) {
                 var MemebershiftID = 0;
                 ResourcePlanner_Service.Update_TeamMemberShift(get_MemberID(), get_ddl_shift(), get_startdate(), get_ddl_shift(), get_TeamType(), get_notes(), get_enddate(), MemebershiftID,'2', OnComplete, OnError, OnTimeOut);
                 chart.commitChanges();
             }

             return ret_val;
         }

         function Delete_ShitPeriod() {


             var ret_val = new Boolean(true);

             if (get_MemebershiftID() == null) {
                 alert("Please select a period");
                 ret_val = false;
             }
             else {
                 ret_val = confirm("Do you want to delete shift period?");
                 if (ret_val) {
                     ResourcePlanner_Service.Delete_TeamMemberShift(get_MemebershiftID(), OnComplete, OnError, OnTimeOut);
                     //chart.commitChanges();
                 }                
             }
             return ret_val;
         }
         
         function resourceSelectHandler(e) {

             var resourceInfo = chart.getResourceInfo(e.id);
             //select defalt tab
             ChangeTabView();
             
             set_TeamName(resourceInfo.attributes.TeamName);
             set_ResourceName(resourceInfo.name);
             set_MemberID(resourceInfo.attributes.MemberID);
             set_TeamType(resourceInfo.attributes.TeamType);
             document.getElementById("<%=btnInsert.ClientID %>").style.visibility = 'visible';
             document.getElementById("<%=btnUpdate.ClientID %>").style.visibility = 'hidden';
             document.getElementById("<%=btnUpdate.ClientID %>").style.visibility = 'hidden';
             document.getElementById("<%=btnUpdate.ClientID %>").style.display = 'none';
             document.getElementById("<%=btnInsert.ClientID %>").style.display = 'block';
             document.getElementById("<%=btnDelete.ClientID %>").style.display = 'none';
             
             ClearPeriodData();

         }
         function setSite(strEstadoCarta) {
            
             set_ddl_site(document.getElementById('<%=ddlSite.ClientID %>'), strEstadoCarta);
         }

         function set_ddl_site(elementRef, valueToSetTo) {
             var isFound = false;
             
             for (var i = 0; i < elementRef.options.length; i++) {
                 if (elementRef.options[i].value == valueToSetTo) {
                     elementRef.options[i].selected = true;
                     isFound = true;
                 }
             }

             if (isFound == false)
                 elementRef.options[0].selected = true;
         }
          
          
         function get_ddl_site() {
             var Ret_ddl_site = document.getElementById("<%=ddlSite.ClientID%>")
             var Ret_site;
             for (var i = 0; i < Ret_ddl_site.options.length; i++)
              {
                  if (Ret_ddl_site.options[i].selected == true)
                      Ret_site = Ret_ddl_site.options[i].value;                     
               }
                
             return Ret_site;
         }

         function Clear_setSite() {

             var elementRef = document.getElementById("<%=ddlSite.ClientID%>");
             for (var i = 0; i < elementRef.options.length; i++) {
                 elementRef.options[i].selected = true;
             }
         }

         function SetShift(R_val) {

             var RB1 = document.getElementById("<%=lstShift.ClientID%>");
             var radio = RB1.getElementsByTagName("input");
             var label = RB1.getElementsByTagName("label");

             for (var i = 0; i < radio.length; i++) {

                 if (radio[i].value == R_val) {
                     radio[i].checked = true;
                 }
             }

         }
         function Clear_SetShift() {

             var RB1 = document.getElementById("<%=lstShift.ClientID%>");
             var radio = RB1.getElementsByTagName("input");
             var label = RB1.getElementsByTagName("label");

             for (var i = 0; i < radio.length; i++) {                
                     radio[i].checked = false;                 
             }

         }
         function get_ddl_shift() {
             var ret_siteVal;
             var radioList = document.getElementById("<%=lstShift.ClientID%>")
             var radio = radioList.getElementsByTagName("input");

             for (var j = 0; j < radio.length; j++) {
                 if (radio[j].checked)
                     ret_siteVal = radio[j].value;
             }
             return ret_siteVal;
         }
         function get_ddl_shiftName() {
             var ret_siteVal;
             var radioList = document.getElementById("<%=lstShift.ClientID%>")
             var radio = radioList.getElementsByTagName("input");
             var label = radioList.getElementsByTagName("input");
             
             for (var j = 0; j < radio.length; j++) {
                 if (radio[j].checked) {
                     ret_siteVal = radio[j].parentNode.getElementsByTagName('label')[0].parentNode.getElementsByTagName('span')[0].style.backgroundColor;
                 }
             }
             return ret_siteVal.toString();
         }
         
         
         
            function set_startdate(val_startdate) {
                document.getElementById("<%=txtFromDate.ClientID%>").value = val_startdate;
               
           }

           function get_startdate() {
               var val_startdate = document.getElementById("<%=txtFromDate.ClientID%>").value;
               return val_startdate;
           }

           function set_enddate(val_enddate) {
               document.getElementById("<%=txtToDate.ClientID%>").value = DateMinusOne(val_enddate).toDDMMYYYYString();
           }
           
           function get_enddate() {
               var val_enddate = document.getElementById("<%=txtToDate.ClientID%>").value;
               return val_enddate;
           }
           
           function set_notes(val_notes) {
               document.getElementById("<%=txtNotes.ClientID%>").value = val_notes;
           }

           function get_notes() {
               var val_notes=document.getElementById("<%=txtNotes.ClientID%>").value;
               return val_notes;
           }

           function set_MemberID(val_memberID) {
               document.getElementById("<%=Hid_MemberID.ClientID%>").value = val_memberID;
             
           }
           function get_MemberID() {
               var val_memberID = document.getElementById("<%=Hid_MemberID.ClientID%>").value;
               return val_memberID;

           }
           function set_TeamType(val_teamtype) {
               document.getElementById("<%=Hid_TeamType.ClientID%>").value = val_teamtype;

           }
           function get_TeamType() {
               var val_teamtype = document.getElementById("<%=Hid_TeamType.ClientID%>").value;
               return val_teamtype;

           }
           function set_MemebershiftID(val_MemebershiftID) {
               document.getElementById("<%=Hid_MemberShiftID.ClientID%>").value = val_MemebershiftID;

           }
           function get_MemebershiftID() {
               var val_MemebershiftID = document.getElementById("<%=Hid_MemberShiftID.ClientID%>").value;
               return val_MemebershiftID;

           }
           function set_ResourceName(val_ResourceName) {
               document.getElementById("<%=txtResourceName.ClientID%>").value = val_ResourceName;

           }
           function set_TeamName(val_TeamName) {
               document.getElementById("<%=txtTeamName.ClientID%>").value = val_TeamName;

           }
           //search dates
           function set_sfromdate(val_sfdate) {
               document.getElementById("<%=txt_sFromdate.ClientID%>").value = val_sfdate;
           }

           function get_sfromdate() {
               var val_sfdate = document.getElementById("<%=txt_sFromdate.ClientID%>").value;
               return val_sfdate;
           }
           function set_stodate(val_stodate) {
               document.getElementById("<%=txt_sTodate.ClientID%>").value = val_stodate;
           }

           function get_stodate() {
               var val_stodate = document.getElementById("<%=txt_sTodate.ClientID%>").value;
               return val_stodate;
           }
           
           function get_Teammeber() {
               var ret_teammeber;
               var TeammeberList = document.getElementById("<%=chkTeamMember.ClientID%>")
               var Check_val = TeammeberList.getElementsByTagName("input");
               var label = TeammeberList.getElementsByTagName("label");

               for (var k = 0; k < Check.length; k++) {
                   if (Check[k].checked) {
                       ret_teammeber = Check_val[k].value;
                   }
               }
               return ret_teammeber;
           }

           function get_DisplayView() {

               var Ret_ddl_site = document.getElementById("<%=ddlView.ClientID%>")
               var Ret_site;
               for (var i = 0; i < Ret_ddl_site.options.length; i++) {
                   if (Ret_ddl_site.options[i].selected == true)
                       Ret_site = Ret_ddl_site.options[i].value;
               }

               return Ret_site;
           }
           
           function ChangeTabView() {
               var tabContainer = $get('<%=ResourceTab.ClientID%>');
               //alert(tabContainer);
               if (tabContainer != undefined && tabContainer != null) {
                   tabContainer = tabContainer.control;
                   tabContainer.set_activeTabIndex(0);

               }

           }
           
           function ClearPeriodData() {

               var empty_int = 0;
               set_MemebershiftID(empty_int);
               document.getElementById("<%=txtFromDate.ClientID%>").value = '';
               document.getElementById("<%=txtToDate.ClientID%>").value = '';
               document.getElementById("<%=txtNotes.ClientID%>").value = '';


           }

          

           function OnComplete(arg) {
              
             //alert(arg);
         }
         function OnTimeOut(arg) {
             //alert("timeOut has occured");
         }
         function OnError(arg) {
             //alert("error has occured: " + arg._message);
         }


         
         function ZoomCtr() {
         
         
        
             var zoomButtonsBar = new Ext.Toolbar("Zoom_div");
             zoomButtonsBar.addButton({ text: "Zoom In", iconCls: 'zoomIn', handler: function() { chart.zoomIn(); } });
                 zoomButtonsBar.addSeparator();
                 zoomButtonsBar.addButton({ text: "Zoom Out", iconCls: 'zoomOut', handler: function() { chart.zoomOut(); } });
                 zoomButtonsBar.addSeparator();
                 zoomButtonsBar.addButton({ text: "Fit All", iconCls: 'fitAll', handler: function() { chart.fitAll(); } });
         }
 
    

     </script>
    <div class="row">
           <div class="col-md-12">

               <div class="form-group">
                                  <div class="col-md-4">
                                       <label class="col-sm-4 control-label"> From Date:</label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txt_sFromdate" runat="server" 
            MaxLength="10" SkinID="Date"></asp:TextBox>
                                           <asp:Label ID="img_sFrom" SkinID="Calender" runat="server" />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" 
                    CssClass="MyCalendar"  
                     PopupButtonID="img_sFrom" 
                    TargetControlID="txt_sFromdate">
                </ajaxToolkit:CalendarExtender>
					</div>
				</div>
 <div class="col-md-4">
                                       <label class="col-sm-4 control-label"> To Date:</label>
                                      <div class="col-sm-8 form-inline"> 
                                          <asp:TextBox ID="txt_sTodate" runat="server" MaxLength="10" SkinID="Date"></asp:TextBox> 
               <asp:Label ID="img_sTo" SkinID="Calender" runat="server"  />
                 <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" 
                    CssClass="MyCalendar"  
                     PopupButtonID="img_sTo" 
                    TargetControlID="txt_sTodate">
                </ajaxToolkit:CalendarExtender>
					</div>
				</div>
<div class="col-md-4 form-inline">
               <asp:DropDownList ID="ddlView" 
                    runat="server" AutoPostBack="true" Width="150px" Visible="false">
            <asp:ListItem Enabled="true" Text="Teams" Value="1"></asp:ListItem>
            <asp:ListItem Text="Users" Value="2"></asp:ListItem>
            </asp:DropDownList>
               <asp:Button runat="server" ID="btnView" SkinID="btnDefault" Text="View" onclick="btnView_Click" /> 
				</div>
</div>
           </div>
        </div>
    <div class="form-group">
             <div class="col-md-12">
                 <div id="Zoom_div" class="btn_align_right" ></div>
</div>
</div>

     <div class="row">
                                <div class="col-md-9">
                                    <div id="container" ></div>
                                    </div>
                                  <div class="col-md-3">
                                      <ajaxToolkit:TabContainer runat="server" ID="ResourceTab" ActiveTabIndex ="0">
<ajaxToolkit:TabPanel runat="server" ID="tab1"  headertext="Selected Period/ Resource">
    <HeaderTemplate>
        Selected Period/ Resource
    </HeaderTemplate>
<ContentTemplate>
<asp:Panel ID="PanelSelect" runat="server">

<div style="font-weight:bold">Team :</div>
<asp:TextBox ID="txtTeamName"  runat="server" Enabled="False" SkinID="txt_90"></asp:TextBox>
        <asp:HiddenField ID="hidTeamType" runat="server" Value="1" Visible="False"   />
<div style="font-weight:bold">Resource :</div>
<asp:TextBox ID="txtResourceName"  runat="server" Enabled="False" SkinID="txt_90"></asp:TextBox>
<asp:HiddenField ID="Hid_MemberShiftID" runat="server" /><asp:HiddenField ID="Hid_MemberID" runat="server" />
<div style="font-weight:bold">From Date:</div>
<label id="FromDate"></label>
<div class="form-inline"><asp:TextBox ID="txtFromDate" runat="server" SkinID="Date"></asp:TextBox>  

<asp:Label ID="img1" runat="server" SkinID="Calender" />
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                    CssClass="MyCalendar"  
                     PopupButtonID="img1" 
                    TargetControlID="txtFromDate" BehaviorID="_content_CalendarExtender1">
                </ajaxToolkit:CalendarExtender></div>
<div style="font-weight:bold">To Date:</div>
<label id="ToDate"></label>
<div class="form-inline"><asp:TextBox ID="txtToDate" runat="server" SkinID="Date"></asp:TextBox> 

<asp:Label ID="img2" runat="server" SkinID="Calender" />
 <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                    CssClass="MyCalendar"  
                     PopupButtonID="img2" 
                    TargetControlID="txtToDate" BehaviorID="_content_CalendarExtender2">
                </ajaxToolkit:CalendarExtender>
</div>
<div style="font-weight:bold">Site:</div>
<label id="Site"></label>
<div><asp:DropDownList ID="ddlSite" runat="server" DataSourceID="SiteFiller" DataTextField="Site" DataValueField="ID" SkinID="ddl_90"></asp:DropDownList>
</div>
<div style="width:250px;">
<div style="font-weight:bold;width:70px;float:left;">Shift:</div><div style="width:93px;float:right;">
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/newshift.aspx?type=shift" Text="Shift Patterns" 
        Font-Underline="True"></asp:HyperLink> </div>
</div>
 <asp:Panel ID="pnlShift" runat="server" Height="100px" Width="250px" ScrollBars="Vertical" BorderStyle="None" BackColor="WhiteSmoke" style="padding:0px" >
                        <asp:RadioButtonList ID="lstShift" runat="server" Width="100%" Font-Size="X-Small" />
                    </asp:Panel>
<div style="font-weight:bold">Notes:</div>
<div><asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="50px" SkinID="txt_90" ></asp:TextBox> </div>
    <div class="form-group">
          <div class="col-md-12 form-inline">
              <asp:Button ID="btnUpdate" runat="server" SkinID="btnUpdate" OnClientClick="return UpdateData()"  />
<asp:Button ID="btnInsert" runat="server" SkinID="btnAdd" OnClientClick="return InsertData()" style="display:none;visibility:hidden;"  />
              <asp:LinkButton ID="btnDelete" runat="server" SkinID="BtnLinkDelete" OnClientClick="return Delete_ShitPeriod()"  />
              </div>
        </div>

</asp:Panel>
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel  runat="server" ID="tab2" headertext="Add Schedule">
    <HeaderTemplate>
        Add Schedule
    </HeaderTemplate>
<ContentTemplate>
<asp:Panel ID="Panle_Insert" runat="server">
<div> <asp:ValidationSummary ID="valsum_tab1" runat="server" ValidationGroup="tab2" />

 </div>
<div style="font-weight:bold">Team :</div>
<asp:DropDownList ID="ddlTeams" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
            DataSourceID="objTeam" DataTextField="TeamName" DataValueField="SDID" 
        onselectedindexchanged="ddlTeams_SelectedIndexChanged" >
            <asp:ListItem Text="Select Team" Value="0" />
        </asp:DropDownList>
<div style="font-weight:bold">Resources</div>
<div style="height:100px">
     <asp:UpdatePanel ID="pnlTeamMember" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server" Height="100px" ScrollBars="Auto" BackColor="WhiteSmoke">
                        <asp:CheckBoxList ID="chkTeamMember" runat="server"
                            RepeatColumns="1" RepeatLayout="Flow" />
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlTeams" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>   
            </div>
 <div style="font-weight:bold">From Date:</div>
<label id="Label1"></label>
<div class="form-inline"><asp:TextBox ID="txt_insert_fromdate" runat="server" SkinID="Date"></asp:TextBox> 
<asp:RequiredFieldValidator id="RequiredFieldValidator19" runat="server" ValidationGroup="tab2" Display="None"
 ErrorMessage="Please enter From Date" ControlToValidate="txt_insert_fromdate"></asp:RequiredFieldValidator>
<asp:Label ID="img3" runat="server" SkinID="Calender" />
 <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                    CssClass="MyCalendar"  
                     PopupButtonID="img3" 
                    TargetControlID="txt_insert_fromdate" Enabled="True">
                </ajaxToolkit:CalendarExtender>
</div>
<div style="font-weight:bold">To Date:</div>
<label id="Label2"></label>
<div class="form-inline"><asp:TextBox ID="txt_insert_todate" runat="server"  SkinID="Date"></asp:TextBox> 
<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ValidationGroup="tab2" Display="None"
 ErrorMessage="Please enter To Date" ControlToValidate="txt_insert_todate"></asp:RequiredFieldValidator>
 <asp:CompareValidator id="CompareValidator8" runat="server" ValidationGroup="tab2" Display="None"
  ErrorMessage="Please ensure to date is after the from date" ControlToValidate="txt_insert_todate"
  Type="Date" Operator="GreaterThanEqual" ControlToCompare="txt_insert_fromdate"></asp:CompareValidator>
<asp:Label ID="img4" runat="server" SkinID="Calender" />
 <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                    CssClass="MyCalendar"  
                     PopupButtonID="img4" 
                    TargetControlID="txt_insert_todate" Enabled="True">
                </ajaxToolkit:CalendarExtender>
</div>
<div style="font-weight:bold">Site:</div>
<label id="Label3"></label>
<div><asp:DropDownList ID="ddl_insert_site" runat="server" DataSourceID="SiteFiller" DataTextField="Site" DataValueField="ID"></asp:DropDownList>
</div>
<div style="width:250px;">
<div style="font-weight:bold;width:70px;float:left;">Shift:</div><div style="width:93px;float:right;">
    <asp:HyperLink ID="btnlink_ShiftPattern" runat="server" 
        NavigateUrl="~/WF/CustomerAdmin/newshift.aspx?type=shift" Text="Shift Patterns" 
        Font-Underline="True"></asp:HyperLink> </div>

</div>
 <asp:Panel ID="Panel2" runat="server" Height="100px" Width="250px" ScrollBars="Vertical" BorderStyle="None" BackColor="WhiteSmoke" style="padding:0px" >
                        <asp:RadioButtonList ID="listShift_insert" runat="server" Width="100%" 
                            Font-Size="Smaller" />
                    </asp:Panel>

        
<div style="font-weight:bold">Notes:</div>
<div><asp:TextBox ID="txt_insert_Notes" runat="server" TextMode="MultiLine" Height="50px" SkinID="txt_90"></asp:TextBox> </div>
    <br />
<div class="form-group">
          <div class="col-md-12">
	
<asp:Button  ID="btnInsertAll" runat="server" SkinID="btnAdd" onclick="btnInsertAll_Click" ValidationGroup="tab2" />
</div>
</div>


</asp:Panel>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
                                    </div>
                                 </div>
    
<asp:ObjectDataSource ID="objTeam" runat="server" TypeName="DataHelperClass" OldValuesParameterFormatString="original_{0}"
            SelectMethod="LoadAllTeamsByPortfolio" />

<asp:ObjectDataSource ID="SiteFiller" runat="server" SelectMethod="LoadSiteByPortfolio"
                TypeName="DataHelperClass" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource> 

<asp:HiddenField ID="Hid_TeamType" runat="server" />
<script type="text/javascript" language="javascript">
    //<![CDATA[
    var chart = new AnyChart('../../Content/swf/AnyGantt_4_4.1.0.swf', "../../Content/swf/Preloader.swf");
    chart.width = '100%';
    chart.height = '500px';
    chart.wMode = 'transparent';
    chart.setXMLFile('ResourcePlanner_xml.aspx' + '?fromdate=' + get_sfromdate() + '&todate=' + get_stodate() );
    chart.write('container'); 

    chart.addEventListener("periodSelect", periodSelectHandler);
    chart.addEventListener("periodEditingProcess", periodEditingProcess);
    chart.addEventListener("periodEditingEnd", periodEditingEnd);
    chart.addEventListener("resourceSelect", resourceSelectHandler);


    Ext.onReady(function ZoomCtr() {

        var zoomButtonsBar = new Ext.Toolbar("Zoom_div");
        zoomButtonsBar.addButton({ text: "<b>Zoom In</b>", iconCls: 'zoomIn', handler: function() { chart.zoomIn(); } });
        zoomButtonsBar.addSeparator();
        zoomButtonsBar.addButton({ text: "<b>Zoom Out</b>", iconCls: 'zoomOut', handler: function() { chart.zoomOut(); } });
        zoomButtonsBar.addSeparator();
        zoomButtonsBar.addButton({ text: "<b>Fit All</b>", iconCls: 'fitAll', handler: function() { chart.fitAll(); } });
    });
    //]]>
</script>
</asp:Content>


