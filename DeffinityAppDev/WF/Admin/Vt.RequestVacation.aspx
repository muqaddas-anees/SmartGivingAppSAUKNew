<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="RequestVacation" Codebehind="VT.RequestVacation.aspx.cs" %>

<%@ Register src="MailControls/VTRequestMail.ascx" tagname="VT" tagprefix="Mail1" %>
<%@ Register Src="controls/VTsubtabs.ascx" TagName="VTTabs" TagPrefix="VTMainSubTab" %>
<%@ Register src="controls/ResourcePlannerTabs.ascx" tagname="ResourcePlannerTabs" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <%-- <VTMainTab:VTTabs ID="VTTab" runat="server" />--%>
    <uc3:ResourcePlannerTabs ID="ResourcePlannerTabs1" runat="server" />
     <Mail1:VT ID="VTMail1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Resources%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.VacationRequest%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManagerProxy ID="btnSCManager" runat="server">
        <Services>
            <asp:ServiceReference Path="~/WF/Admin/webservices/ResourcePlanner_Service.asmx" />
        </Services>
    </asp:ScriptManagerProxy>
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
 
 <script type="text/javascript" >
     Date.prototype.toDDMMYYYYString = function() { return isNaN(this) ? 'NaN' : [this.getDate() > 9 ? this.getDate() : '0' + this.getDate(), this.getMonth() > 8 ? this.getMonth() + 1 : '0' + (this.getMonth() + 1), this.getFullYear()].join('/') }
     Date.prototype.toMMDDYYYYString = function() { return isNaN(this) ? 'NaN' : [this.getMonth() > 8 ? this.getMonth() + 1 : '0' + (this.getMonth() + 1), this.getDate() > 9 ? this.getDate() : '0' + this.getDate(), this.getFullYear()].join('/') }
         
 
     var periodID;
     var period;
     var resourceInfo;
     var err;

     function ChangeTabView(val) {
         var tabContainer = $get('<%=ResourceTab.ClientID%>');
         //alert(tabContainer);
         if (tabContainer != undefined && tabContainer != null) {
             tabContainer = tabContainer.control;
             tabContainer.set_activeTabIndex(val);
         }
     }

     function DateDiff(fromdate, todate) {
         var retval = 0;
         //var user_date = Date.parse(fromdate);
         //var today_date = Date.parse(todate);
         try {
             todate.setDate(todate.getDate() + 1);
             var diff_date = todate - fromdate;
             var num_days = ((diff_date % 31536000000) % 2628000000) / 86400000;
             retval = Math.floor(num_days);
             //remove no. of saturday and sunday
             retval = retval - retNoSunAndSatdays(fromdate, todate) - checkHalfDays();
             //alert(checkHalfDays());
         } catch (err) { }
         return retval;

     }

     function retNoSunAndSatdays(fromdate, todate) {
         var retval = 0;
         while ( fromdate < todate  ) {
             // check for satruday = 6 and sunday =0
             if (fromdate.getDay() == 6 ) {
                 retval = retval + 1;
             }
             if (fromdate.getDay() == 0) {
                 retval = retval + 1;
             }
             fromdate.setDate(fromdate.getDate() + 1); 
         }
         return retval;
     }

     function checkHalfDays() {
         var retval = 0;

         if (get_ddl_startperiod() == '0.5') {
             retval = 0.5;
         }
         if (get_ddl_EndPeriod() == '0.5') {
             retval = 0.5;
         }
         return retval;
     }
     
     function getQuerystring(key, default_) {

         if (default_ == null) default_ = "";
         key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
         var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
         var qs = regex.exec(window.location.href.toLowerCase());
         if (qs == null)
             return default_;
         else
             return qs[1];
     }
     function DateMinusOne(myDate) {

         myDate.setDate(myDate.getDate() - 1);
         return myDate;
     }
     function resourceSelectHandler(e) {

         var resourceInfo = chart.getResourceInfo(e.id);
         //select defalt tab
         ChangeTabView(1);
         setResource(resourceInfo.attributes.MemberID);


         //LoadResourceType();
         set_iframe(resourceInfo.attributes.MemberID);
         
     }
     
     function periodSelectHandler(e) {
         try {

             periodID = e.id;
             period = chart.getPeriodInfo(e.id);
             resourceInfo = chart.getResourceInfo(period.resourceId);

             set_periodid(periodID);
             //set_team(resourceInfo.attributes.TeamName);
             set_member(resourceInfo.name);
             set_startdate(period.startDate.toDDMMYYYYString());
             set_enddate(period.endDate.toDDMMYYYYString());
             set_notes(period.attributes.Notes);
             set_requestid(period.attributes.RequestID);
             setAbsenttype(period.attributes.AbsenseType);
             set_memberid(period.attributes.MemberID);
             set_startperiod(period.attributes.FromDatePeriod);
             set_EndPeriod(period.attributes.ToDatePeriod);
             
             //Clear_ResourceType();

             //LoadResourceType();
             set_iframe(resourceInfo.attributes.MemberID);
			  
             
         } catch (err)
             { }

         ChangeTabView(0);

     }
     function set_iframe(contractorid) {
         document.getElementById('<%=vtSummary.ClientID%>').src = 'VT.Resourcesummarydisplay.aspx?contractorid=' + contractorid + '&date=' + get_startdate();
     }
     function periodEditingProcess(e) {

         period = chart.getPeriodInfo(e.id);
         resourceInfo = chart.getResourceInfo(period.resourceId);

         set_periodid(periodID);
         //set_team(resourceInfo.attributes.TeamName);
         set_member(resourceInfo.name);
         set_startdate(period.startDate.toDDMMYYYYString());
         set_enddate(period.endDate);
         set_notes(period.attributes.Notes);
         set_requestid(period.attributes.RequestID);
         setAbsenttype(period.attributes.AbsenseType);
         set_startperiod(period.attributes.FromDatePeriod);
         set_EndPeriod(period.attributes.ToDatePeriod);
         ////FromDatePeriod ToDatePeriod
     }
     function delete_request() {
         var ret_val = new Boolean(true);

         if (get_requestid() == null) {
             alert("Please select a request");
             ret_val = false;
         }
         else {
             ret_val = confirm("Do you want to delete request?");
             if (ret_val) {
                 ResourcePlanner_Service.Delete_Request(get_requestid());
             }
         }
         return ret_val;
       
     }
         function periodEditingEnd(e) {
             periodID = e.id;
             period = chart.getPeriodInfo(e.id);
             var retval = ResourcePlanner_Service.Update_Request(get_requestid(), get_ddl_Absenttype(), get_startdate(), get_enddate(), get_notes(),get_ddl_startperiod(),get_ddl_EndPeriod(), update_success, update_fail);
         }

         function update_success() {
             Clear_ResourceType();
             chart.updatePeriod(periodID, period.startDate, period.endDate);
             chart.commitChanges();
             chart.updatePeriodCustomAttributeValue(periodID, "AbsenseType", get_ddl_Absenttype());
             chart.updatePeriodCustomAttributeValue(periodID, "Notes", get_notes());
             chart.updatePeriodCustomAttributeValue(periodID, "FromDatePeriod", get_ddl_startperiod());
             chart.updatePeriodCustomAttributeValue(periodID, "ToDatePeriod", get_ddl_EndPeriod());

             chart.updatePeriodCustomAttributeValue(periodID, "Date_display", get_startdate() + ' - ' + get_enddate());
             //alert(DateDiff(get_startdate(), get_enddate()));
             //chart.updatePeriodCustomAttributeValue(periodID, "Days", DateDiff(get_startdate(), get_enddate()));
             //chart.commitChanges();
             
             chart.commitChanges();
            
             //LoadResourceType();
            
             return false;

         }
         function update_success1() {
             //Clear_ResourceType();
             UpdatePeriod();             
             chart.updatePeriodCustomAttributeValue(get_periodid(), "AbsenseType", get_ddl_Absenttype());
             chart.updatePeriodCustomAttributeValue(get_periodid(), "Notes", get_notes());
             chart.updatePeriodCustomAttributeValue(get_periodid(), "FromDatePeriod", get_ddl_startperiod());
             chart.updatePeriodCustomAttributeValue(get_periodid(), "ToDatePeriod", get_ddl_EndPeriod());
             //alert( DateDiff( new Date(DateConvertion(get_startdate())), new Date(DateConvertion(get_enddate())) ) );
             chart.updatePeriodCustomAttributeValue(get_periodid(), "Days", DateDiff(new Date(DateConvertion(get_startdate())), new Date(DateConvertion(get_enddate()))));
             chart.updatePeriodCustomAttributeValue(get_periodid(), "Date_display", get_startdate() + ' - ' + get_enddate());
             //chart.commitChanges();
             chart.commitChanges();
               
             //LoadResourceType();
             
             return false;

         }
         function UpdatePeriod() {
             //add day to show correct prirod on chart
             var update_todate = new Date(DateConvertion(get_enddate()));
             update_todate.setDate(update_todate.getDate() + 1);

             chart.updatePeriod(get_periodid(), new Date(DateConvertion(get_startdate())), update_todate);
             chart.commitChanges();
         }
         function update_fail() {
             

         }
         
         function update_request() {
             var retval = ResourcePlanner_Service.Update_Request(get_requestid(), get_ddl_Absenttype(), get_startdate(), get_enddate(), get_notes(), get_ddl_startperiod(), get_ddl_EndPeriod(), update_success1, update_fail);

             return false;
         }
         function DateConvertion(s_date) {

             var b = s_date;
             var temp = new Array();
             temp = b.split('/');
             var s1 = temp[1] + '/' + temp[0] + '/' + temp[2];
             return s1
         }
        
        //Members
         function set_member(val_member) {
             document.getElementById("<%=txt_edit_member.ClientID%>").value = val_member;

         }
         function get_member() {
             var val_member = document.getElementById("<%=txt_edit_member.ClientID%>").value;
             return val_member;
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
         //ddl_edit_startperiod
         function set_startperiod(strEstadoCarta) {

             set_ddl_site(document.getElementById('<%=ddlEditFromPeriod.ClientID %>'), strEstadoCarta);
         }
         //ddl_edit_Absenttype
         function set_EndPeriod(strEstadoCarta) {

             set_ddl_site(document.getElementById('<%=ddlEditToPeriod.ClientID %>'), strEstadoCarta);
         }
         //ddl_edit_Absenttype
         function setResource(strEstadoCarta) {

             set_ddl_site(document.getElementById('<%=ddlmembers.ClientID %>'), strEstadoCarta);
         }
         //ddl_edit_Absenttype
         function setAbsenttype(strEstadoCarta) {

             set_ddl_site(document.getElementById('<%=ddl_edit_Absenttype.ClientID %>'), strEstadoCarta);
         }
         function get_ddl_startperiod() {
             return get_ddl_val("<%=ddlEditFromPeriod.ClientID%>");
         }
         function get_ddl_EndPeriod() {
             return get_ddl_val("<%=ddlEditToPeriod.ClientID%>");
         } 
         
         function get_ddl_Absenttype() {
             return get_ddl_val("<%=ddl_edit_Absenttype.ClientID%>");
         } 
         
         function set_ddl_Absenttype(elementRef, valueToSetTo) {
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

           
         
         function get_ddl_val(ddl_val) {
             var Ret_ddl_site = document.getElementById(ddl_val)
             var Ret_site;
             for (var i = 0; i < Ret_ddl_site.options.length; i++) {
                 if (Ret_ddl_site.options[i].selected == true)
                     Ret_site = Ret_ddl_site.options[i].value;
             }

             return Ret_site;
         }

         function Clear_setAbsenttype() {

//             var elementRef = document.getElementById("<%=ddl_edit_Absenttype.ClientID%>");
//             for (var i = 0; i < elementRef.options.length; i++) {
//                 elementRef.options[i].selected = true;
//             }
         }
                  
         //start date
         function set_startdate(val_startdate) {
             document.getElementById("<%=txt_edit_from.ClientID%>").value = val_startdate;

         }

         function get_startdate() {
             var val_startdate = document.getElementById("<%=txt_edit_from.ClientID%>").value;
             return val_startdate;
         }
        //end date
         function set_enddate(val_enddate) {
             document.getElementById("<%=txt_Edit_Todate.ClientID%>").value = val_enddate; 
             //DateMinusOne(new Date(DateConvertion())).toDDMMYYYYString();
         }

         function get_enddate() {
             var val_enddate = document.getElementById("<%=txt_Edit_Todate.ClientID%>").value;
             return val_enddate;
         }
        //notes
         function set_notes(val_notes) {
             document.getElementById("<%=txt_Edit_Notes.ClientID%>").value = val_notes;
         }

         function get_notes() {
             var val_notes = document.getElementById("<%=txt_Edit_Notes.ClientID%>").value;
             return val_notes;
         }
         //h_requestid
         function set_requestid(val_requestid) {
             document.getElementById("<%=h_requestid.ClientID%>").value = val_requestid;
         }

         function get_requestid() {
             var val_notes = document.getElementById("<%=h_requestid.ClientID%>").value;
             return val_notes;
         }
         //h_teamtype
         function set_teamtype(val_teamtype) {
             document.getElementById("<%=h_teamtype.ClientID%>").value = val_teamtype;
         }

         function get_teamtype() {
             var val_notes = document.getElementById("<%=h_teamtype.ClientID%>").value;
             return val_notes;
         }
         //h_memberid
         function set_memberid(val_memberid) {
             document.getElementById("<%=h_memberid.ClientID%>").value = val_memberid;
         }

         function get_memberid() {
             var val_notes = document.getElementById("<%=h_memberid.ClientID%>").value;
             return val_notes;
         }
         //h_periodid

         function set_periodid(val_periodid) {
             document.getElementById("<%=h_periodid.ClientID%>").value = val_periodid;
         }

         function get_periodid() {
             var val_notes = document.getElementById("<%=h_periodid.ClientID%>").value;
             return val_notes;
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

         function get_TeamId() {
             var val_Team = document.getElementById("<%= ddlTeam.ClientID%>").options[document.getElementById('<%= ddlTeam.ClientID %>').selectedIndex].value;
             //var val_TeamId = val_Team.options[val_Team.selectedIndex].value;
             return val_Team;  
         }

         function get_ResourceID() {
             var val_Res = document.getElementById("<%= ddlMResource.ClientID%>").options[document.getElementById('<%= ddlMResource.ClientID %>').selectedIndex].value;
             return val_Res;
         }
         
         
         //get table object

         // Handles exception
         function ExceptionHandler(result) {
         }
        
         
         </script>

       <VTMainSubTab:VTTabs ID="VTTab" runat="server" />
    <br />
    <div class="form-group">
             <div class="col-md-12">
                 <asp:UpdatePanel ID="updatepanel1" runat="server">
 <ContentTemplate>
     <div class="form-group">
                                  <div class="col-md-4">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlCustomer" runat="server" 
         AutoPostBack="True" 
         onselectedindexchanged="ddlCustomer_SelectedIndexChanged" SkinID="ddl_90"></asp:DropDownList>
					</div>
				</div>
 <div class="col-md-4">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Team%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlTeam" runat="server" SkinID="ddl_90" AutoPostBack="True" 
         onselectedindexchanged="ddlTeam_SelectedIndexChanged"></asp:DropDownList>
					</div>
				</div>
<div class="col-md-4">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Resource%></label>
                                      <div class="col-sm-8">
                                          <asp:DropDownList ID="ddlMResource" runat="server" SkinID="ddl_90"></asp:DropDownList>
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-4">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.FromDate%></label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txt_sFromdate" runat="server" 
            MaxLength="10" SkinID="Date"></asp:TextBox><asp:Label ID="img_sFrom" SkinID="Calender" runat="server"  />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" 
                    CssClass="MyCalendar"  
                     PopupButtonID="img_sFrom" 
                    TargetControlID="txt_sFromdate">
                </ajaxToolkit:CalendarExtender>
					</div>
				</div>
 <div class="col-md-4">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ToDate%></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txt_sTodate" runat="server" MaxLength="10" SkinID="Date"></asp:TextBox> <asp:Label ID="img_sTo" SkinID="Calender" runat="server"  />
                 <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" 
                    CssClass="MyCalendar"  
                     PopupButtonID="img_sTo" 
                    TargetControlID="txt_sTodate">
                </ajaxToolkit:CalendarExtender> 
					</div>
				</div>
<div class="col-md-4">
                                       <asp:Button ID="btnViewData" runat="server" SkinID="btnDefault" Text="View" onclick="btnViewData_Click" CausesValidation="false" />
				</div>
</div>
   
 </ContentTemplate>
 <Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlCustomer" EventName="selectedindexchanged" />
<asp:PostBackTrigger ControlID="btnViewData" />
 </Triggers>
 </asp:UpdatePanel>
</div>
</div>
    <div class="form-group">
             <div class="col-md-12">
                 <div id="Zoom_div" class="btn_align_right">
                  </div>
</div>
</div>
    <div class="row">
                                <div class="col-md-9">
                                     <div id="container">

                  </div>

                  <script type="text/javascript" >
                      //<![CDATA[
                      var chart = new AnyChart('../../Content/swf/AnyGantt_4_4.1.0.swf', "../../Content/swf/Preloader.swf");
                      chart.width = '100%';
                      chart.height = '600px';
                      chart.wMode = 'opaque';
                      var xml_path = 'Vt.RequestVacation_xml.aspx?teamid=' + get_TeamId() + '&fromdate=' + get_sfromdate() + '&todate=' + get_stodate() + '&resourceid=' + get_ResourceID();
                      chart.setXMLFile(xml_path);
                      chart.write('container');

                      chart.addEventListener("periodSelect", periodSelectHandler);
                      chart.addEventListener("periodEditingProcess", periodEditingProcess);
                      chart.addEventListener("periodEditingEnd", periodEditingEnd);
                      chart.addEventListener("resourceSelect", resourceSelectHandler);

                      Ext.onReady(function ZoomCtr() {
                          var zoomButtonsBar = new Ext.Toolbar("Zoom_div");
                          zoomButtonsBar.addButton({ text: "<b>Zoom In</b>", iconCls: 'zoomIn', handler: function () { chart.zoomIn(); } });
                          zoomButtonsBar.addSeparator();
                          zoomButtonsBar.addButton({ text: "<b>Zoom Out</b>", iconCls: 'zoomOut', handler: function () { chart.zoomOut(); } });
                          zoomButtonsBar.addSeparator();
                          zoomButtonsBar.addButton({ text: "<b>Fit All</b>", iconCls: 'fitAll', handler: function () { chart.fitAll(); } });
                      });
                  </script>
                                    </div>
                                  <div class="col-md-3">
                                       <ajaxToolkit:TabContainer runat="server" ID="ResourceTab" ActiveTabIndex="0" 
                      Width="100%" Height="930px" >
                      <ajaxToolkit:TabPanel runat="server" ID="tab1">
                          <HeaderTemplate>
                       <%= Resources.DeffinityRes.ExistingBooking%>
                          </HeaderTemplate>
                          <ContentTemplate>
                              <div class="form-group">
             <div class="col-md-12">
                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                              ValidationGroup="update" />
</div>
</div>
                              <div class="form-group">
             <div class="col-md-12">
                 <label> <%= Resources.DeffinityRes.Resource %></label><br />
                 <asp:TextBox ID="txt_edit_member" runat="server" Enabled="False" ReadOnly="True" SkinID="txt_90"></asp:TextBox>
</div>
</div>
                              <div class="form-group">
             <div class="col-md-12">
                 <label><%= Resources.DeffinityRes.AbsenceType%> </label><br />
                 <asp:DropDownList ID="ddl_edit_Absenttype" runat="server" SkinID="ddl_90">
                                          </asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_edit_Absenttype"
                                              Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes, PleaseselectAbsencetype%>" InitialValue="0" />
</div>
</div>
                              <div class="form-group">
             <div class="col-md-12 form-inline">
                 <label><%= Resources.DeffinityRes.Datefrom%></label><br />
                 <asp:TextBox ID="txt_edit_from" runat="server" SkinID="txt_125px" MaxLength="10"></asp:TextBox><asp:Label
                                              ID="img_from" runat="server" SkinID="Calender" />
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_edit_from"
                                              Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PlsselectFromDate%>" Text="*" ValidationGroup="update" />
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="MyCalendar"
                                               PopupButtonID="img_from" TargetControlID="txt_edit_from"
                                              Enabled="True">
                                          </ajaxToolkit:CalendarExtender>
                                        
                
                                         
</div>
                                  <div class="col-md-12 form-inline" style="padding-top:5px">
                                        <asp:DropDownList ID="ddlEditFromPeriod" runat="server" SkinID="ddl_100px">
                                              <asp:ListItem Text="Full Day" Value="0"></asp:ListItem>
                                              <asp:ListItem Text="Half Day" Value="0.5"></asp:ListItem>
                                          </asp:DropDownList>
                                       <asp:DropDownList ID="ddl_edit_meridianfrom" runat="server"  Enabled="False" SkinID="ddl_100px">
                                                  <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                                  <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                                          </asp:DropDownList>
                                      </div>
</div>
                              <div class="form-group">
             <div class="col-md-12 form-inline">
                 <%= Resources.DeffinityRes.Dateto%> <br />
                 <asp:TextBox ID="txt_Edit_Todate" runat="server" SkinID="Price_125px"></asp:TextBox><asp:RequiredFieldValidator
                                              ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Edit_Todate"
                                              Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PlsselectToDate%>" ValidationGroup="update"
                                              Text="*" />
                                          <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_Edit_Todate"
                                              ControlToCompare="txt_edit_from" Display="None" ErrorMessage="<%$ Resources:DeffinityRes,InvalidToDate%>"
                                              Operator="GreaterThan" Type="Date" ValidationGroup="update" Text="*" />
                                          <asp:CompareValidator ID="Cmpdates_edit" runat="server" ControlToValidate="txt_edit_from"
                                              ControlToCompare="txt_Edit_Todate" Display="None" ErrorMessage="<%$ Resources:DeffinityRes,DateComparison%>"
                                              Operator="LessThanEqual" Type="Date" ValidationGroup="update" />
                                          <asp:Label ID="Image2" runat="server" SkinID="Calender" />
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="MyCalendar"
                                               PopupButtonID="Image2" TargetControlID="txt_Edit_Todate"
                                              Enabled="True">
                                          </ajaxToolkit:CalendarExtender>
                                         
                  
                                           
</div>
                                  <div class="col-md-12 form-inline" style="padding-top:5px">
                                       <asp:DropDownList ID="ddlEditToPeriod" runat="server" SkinID="ddl_100px">
                                              <asp:ListItem Text="Full Day" Value="0"></asp:ListItem>
                                              <asp:ListItem Text="Half Day" Value="0.5"></asp:ListItem>
                                          </asp:DropDownList>
                                      <asp:DropDownList ID="ddl_edit_meridianto" runat="server" Enabled="False" SkinID="ddl_100px">
                                                  <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                                  <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                                          </asp:DropDownList>
                                      </div>
</div>
                              <div class="form-group">
             <div class="col-md-12">
                 <label><%= Resources.DeffinityRes.Notes%></label><br />
                  <asp:TextBox ID="txt_Edit_Notes" runat="server" TextMode="MultiLine" 
                                              SkinID="txtMulti"></asp:TextBox>
</div>
</div>
                              <div class="form-group">
             <div class="col-md-12 centered">
                  <asp:Button ID="btn_Update_request" runat="server" OnClientClick="return update_request()"
                                              SkinID="btnUpdate"  />
                                          <asp:LinkButton ID="btn_Request_delete" runat="server" OnClientClick="return delete_request()"
                                              SkinID="BtnLinkDelete"   />
</div>
</div>
                              <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> <%= Resources.DeffinityRes.Summary%></strong>
            <hr class="no-top-margin" />
            </div>
</div>
                              <div class="form-group">
             <div class="col-md-12">
                  <asp:Label ID="lblSummary_edit" runat="server"></asp:Label>
               
                  <div>
                                    <iframe id="vtSummary" name="vtSummary" runat="server" frameborder="0" width="253px" height="350px" scrolling="auto"></iframe>
                                  </div>
</div>
</div>
                          </ContentTemplate>
                      </ajaxToolkit:TabPanel>
                      <ajaxToolkit:TabPanel runat="server" ID="tab2">
                          <HeaderTemplate>
                        <%= Resources.DeffinityRes.NewBooking%></HeaderTemplate>
                          <ContentTemplate>
                              <div class="form-group">
             <div class="col-md-12">
                 <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ValidationGroup="insert" />
                                              <asp:CustomValidator ID="AllowanceReq" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,NoAllowanceforuser%>"
                                                  Display="Dynamic" ></asp:CustomValidator>
                                              <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
</div>
</div>
                              <div class="form-group">
             <div class="col-md-12">
                 <%= Resources.DeffinityRes.Resource%><br />
                 <asp:DropDownList ID="ddlmembers" runat="server" AutoPostBack="True" SkinID="ddl_90"
                                                  OnSelectedIndexChanged="ddlmembers_SelectedIndexChanged">
                                              </asp:DropDownList>
</div>
</div>
                              <div class="form-group">
             <div class="col-md-12">
                  <%= Resources.DeffinityRes.AbsenceType%> <br />
                  <asp:DropDownList ID="ddlAbsenceType" runat="server" SkinID="ddl_90">
                                              </asp:DropDownList>
                                              <asp:RequiredFieldValidator ID="rqdAbsenceType" runat="server" ControlToValidate="ddlAbsenceType"
                                                  Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseselectAbsencetype%>" InitialValue="0"
                                                  ValidationGroup="insert" />
</div>
</div>
                              <div class="form-group">
             <div class="col-md-12 form-inline">
                 <%= Resources.DeffinityRes.Datefrom%><br />
                 <asp:TextBox ID="txtDateFrom" runat="server" SkinID="txt_125px"></asp:TextBox>
                                              <asp:Label ID="Img1" runat="server" SkinID="Calender" />
                                              <asp:RequiredFieldValidator ID="rqdDateFrom" runat="server" ControlToValidate="txtDateFrom" Text="*"
                                                  Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PlsselectFromDate%>" ValidationGroup="insert" />
                                              <asp:CompareValidator ID="cmpDateFrom" runat="server" ControlToValidate="txtDateFrom"  Text="*"
                                                  Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Invalidfromdate%>" Operator="DataTypeCheck" Type="Date"
                                                  ValidationGroup="insert" />
                                              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                                   PopupButtonID="img1" TargetControlID="txtDateFrom" Enabled="True">
                                              </ajaxToolkit:CalendarExtender>
                                              
                               
</div>
                                   <div class="col-md-12 form-inline" style="padding-top:5px">
                                       <asp:DropDownList ID="ddlFromPeriod" runat="server" SkinID="ddl_100px" >
                                                  <asp:ListItem Text="Full Day" Value="0"></asp:ListItem>
                                                  <asp:ListItem Text="Half Day" Value="0.5"></asp:ListItem>
                                              </asp:DropDownList>
                                        <asp:DropDownList ID="ddlmeridianform" runat="server" SkinID="ddl_100px">
                                                  <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                                  <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                                              </asp:DropDownList>
                                       </div>
</div>
                              <div class="form-group">
             <div class="col-md-12 form-inline">
                  <%= Resources.DeffinityRes.Dateto%><br />
                 <asp:TextBox ID="txtDateTo" runat="server" SkinID="txt_125px"></asp:TextBox>
                                              <asp:RequiredFieldValidator ID="rqdDateTo" runat="server" ControlToValidate="txtDateTo" Text="*"
                                                  Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PlsselectToDate%>" ValidationGroup="insert" />
                                              <asp:Label ID="Img2" runat="server" SkinID="Calender" />
                                              <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                                                   PopupButtonID="img2" TargetControlID="txtDateTo" Enabled="True">
                                              </ajaxToolkit:CalendarExtender>
                                              <asp:CompareValidator ID="Cmpdates" runat="server" ControlToValidate="txtDateFrom"
                                                  ControlToCompare="txtDateTo" Display="None" ErrorMessage="<%$ Resources:DeffinityRes,DateComparison%>"
                                                  Operator="LessThanEqual" Type="Date" ValidationGroup="insert" />
</div>
                                  
                                  <div class="col-md-12 form-inline" style="padding-top:5px">
                                       <asp:DropDownList ID="ddlToPeriod" runat="server" SkinID="ddl_100px" >
                                                  <asp:ListItem Text="Full Day" Value="0"></asp:ListItem>
                                                  <asp:ListItem Text="Half Day" Value="0.5"></asp:ListItem>
                                              </asp:DropDownList>
                                      <asp:DropDownList ID="ddlmeridianto" runat="server" SkinID="ddl_100px">
                                                  <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                                  <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                                              </asp:DropDownList>
                                      </div>
</div>
                              <div class="form-group">
             <div class="col-md-12">
                  <%= Resources.DeffinityRes.Notes%><br />
                 <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
</div>
</div>
                              <div class="form-group">
             <div class="col-md-12">
                 <asp:Button ID="btnRequestLeave" runat="server" AlternateText="<%$ Resources:DeffinityRes, RequestforVacation%>"
                                                  SkinID="btnApply" OnClick="btnRequestLeave_Click" OnClientClick="ChkAllowance();"
                                                  ValidationGroup="insert" />
</div>
</div>
                              <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%= Resources.DeffinityRes.Summary%> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
                                                  <asp:UpdatePanel ID="pnlSummary" runat="server">
                                                      <ContentTemplate>
                                                      <asp:Panel ID="Panel1" runat="server" width="253px" height="450px" ScrollBars="Auto">
                                                          <asp:Label ID="lblthisyear" runat="server"></asp:Label>
                                                          <asp:DataList ID="dlist_summary" runat="server" Width="100%" SkinID="ProgrammeList">
                                                              <HeaderTemplate>
                                                              </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <div class="form-group">
             <div class="col-xs-12 form-inline">
                                       <label class="col-sm-8 control-label"> <asp:Label ID="lblTitles" runat="server" Text='<%# Eval("Titles") %>' Font-Size="Smaller" Font-Bold="true" /></label>
                                      <div class="col-sm-4 control-label"><asp:Label ID="lblsum_values" runat="server" Text='<%# Eval("sum_values") %>'  Font-Size="Smaller" />
					</div>
				</div>
                </div>
                                                              </ItemTemplate>
                                                          </asp:DataList>
                                                          </asp:Panel>
                                                      </ContentTemplate>
                                                      <Triggers>
                                                          <asp:AsyncPostBackTrigger ControlID="ddlmembers" EventName="SelectedIndexChanged" />
                                                      </Triggers>
                                                  </asp:UpdatePanel>
                              
                          </ContentTemplate>
                      </ajaxToolkit:TabPanel>
                  </ajaxToolkit:TabContainer>
                 
                  <div>
                      <div id="hidField">
                          <asp:HiddenField ID="HID_Alloance" runat="server" />
                      </div>
                      <asp:ObjectDataSource ID="objTeam" runat="server" TypeName="DataHelperClass" OldValuesParameterFormatString="original_{0}"
                          SelectMethod="LoadAllTeamsByPortfolio" />
                  </div>
                                    </div>
                                 </div>
  
  <div>
          <asp:HiddenField ID="HD_TeamType" runat="server" />
          <asp:HiddenField ID="h_teamtype" runat="server" />
          <asp:HiddenField ID="h_memberid" runat="server" />
          <asp:HiddenField ID="h_requestid" runat="server" />
          <asp:HiddenField ID="h_periodid" runat="server" />
      </div>
  
  
   <script type="text/javascript" >
       function ChkAllowance() {
            
           var hdd = this.document.getElementById("<%=HID_Alloance.ClientID %>");
           if (hdd.value == "0.0") {
               //alert("allowance required!!!!!");
               return;
           }
       }
       
   </script>
   

   </asp:Content>

