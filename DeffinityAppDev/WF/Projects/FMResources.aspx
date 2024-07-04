<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="CustomerResources"  Codebehind="FMResources.aspx.cs" %>

<%@ Register Src="controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Resources%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc2:FMTab ID="tab" runat="server"  />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

   
<script language="JavaScript" type="text/javascript">
    function toggleQuestion(id) {
        var elem = document.getElementById(id);
       
        elem.style.display = elem.style.display == "none" ? "block" : "none";
    }
</script>
   
        <asp:Panel ID="pnlResource" runat="server" >
            <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ResourcesAll%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlResource" runat="server" SkinID="ddl_90"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox  ID="txtFromDate" runat="server" SkinID="Date"></asp:TextBox>
            <asp:Label  ID="imgbtnenddate" runat="server" SkinID="Calender" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender"  runat="server"
                                        PopupButtonID="imgbtnenddate" TargetControlID="txtFromDate" CssClass="MyCalendar">
                                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ToDate%>  </label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtToDate" runat="server" SkinID="Date"></asp:TextBox>
            <asp:Label  ID="imgbtnenddate1" runat="server" SkinID="Calender" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                        PopupButtonID="imgbtnenddate1" TargetControlID="txtToDate" CssClass="MyCalendar">
                                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
</div>
            <div class="form-group">
      <div class="col-md-4">
          
           <div class="col-sm-12">
                <asp:RadioButtonList ID="RadStatus" runat="server" 
                    RepeatDirection="Horizontal" >
                <asp:ListItem Selected="True" Value="0">All&nbsp;Timesheet&nbsp;Entries</asp:ListItem>
                <asp:ListItem Value="4">Approved&nbsp;Only</asp:ListItem>
                </asp:RadioButtonList> <asp:CheckBox ID="chkVacation1" runat="server" Text="Inclue Vacation" Visible="false" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
                  <asp:DropDownList ID="ddlCustomer" runat="server" SkinID="ddl_90">
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Site%> </label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_90">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="casCadSite1" runat="server" Category="Task" 
                    ParentControlID="ddlCustomer" PromptText="Please select..." 
                    ServiceMethod="GetSites" ServicePath="~/WF/DC/webservices/ServiceMgr.asmx" 
                    TargetControlID="ddlSite" />
            </div>
	</div>
</div>
            <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Project%> </label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProject" runat="server" SkinID="ddl_90"></asp:DropDownList> 
            <ajaxToolkit:CascadingDropDown ID="casCadProjectTile" runat="server"
    TargetControlID="ddlProject"
    Category="Title"
    PromptText="Please select..."
    ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
    ServiceMethod="GetAllProjectRef"
    ParentControlID="ddlCustomer" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Task%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlTask" runat="server" SkinID="ddl_90"></asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="casCadTasks" runat="server"
    TargetControlID="ddlTask"
    Category="Task"
    PromptText="Please select..."
    ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
    ServiceMethod="GetProjectsTasks"
    ParentControlID="ddlProject" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.PO%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlPO" runat="server" SkinID="ddl_90"></asp:DropDownList>
            </div>
	</div>
</div>
          
       
              
<div class="form-group">
      <div class="col-md-4">
          
	</div>
	<div class="col-md-6">
          
	</div>
	<div class="col-md-2 form-inline">
          <asp:Button  ID="imgbtnView" runat="server" SkinID="btnDefault" Text="View" 
                    onclick="imgbtnView_Click" /> 
                    <asp:Button ID="imgbtnRest" runat="server"
                    SkinID="btnDefault" Text="Reset" onclick="imgbtnRest_Click" />
          
	</div>
</div>

            </asp:Panel>
         
     
    <div class="form-group">
          <div class="col-md-12">
              <div class="col-sm-3" id="div2"  runat="server" visible="false">
                  <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Summary%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                   <div style="height:400px;overflow:auto" align="left">
             <asp:Repeater ID="rptrAllowence" runat="server" 
                 onitemdatabound="rptrAllowence_ItemDataBound" >
               <HeaderTemplate>
       
        </HeaderTemplate>
             <ItemTemplate>
             
                 <%--<asp:LinkButton ID="lnkBtn" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ContractorName")%>'></asp:LinkButton>--%>
              
             <a href="#" onclick="toggleQuestion('<%#DataBinder.Eval(Container,"DataItem.ID")%>')">+</a>
                 <%--<asp:ImageButton ID="ImageButton1" runat="server" SkinID="ImgSymAdd" CausesValidation="False" OnClientClick="toggleQuestion('<%#DataBinder.Eval(Container,'DataItem.ID')%>')" />--%>
                 <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ContractorName")%>'></asp:Label>
<br />
                 <asp:Label ID="lblID" runat="server" Text=' <%#DataBinder.Eval(Container,"DataItem.ID")%>' Visible="false"></asp:Label>
                 
                 <div style="display:none" id= '<%#DataBinder.Eval(Container,"DataItem.ID")%>'>
                
                 <asp:GridView ID="grdAllowence" runat="server" ShowHeader="false">
                 <Columns>
                 <asp:BoundField DataField="Titles"/>
                 <asp:BoundField DataField="sum_values" ItemStyle-HorizontalAlign="Right"/>
                 </Columns>
                 </asp:GridView>
                
                  </div>
             </ItemTemplate>
             </asp:Repeater>
            </div>
              </div>
 
           <div class="col-sm-9" id="div1"  runat="server" visible="false">
               <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Timesheet%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
               
               <asp:GridView ID="grdTimeSheet" runat="server" AutoGenerateColumns="false"
             Width="100%" EmptyDataText="No Records Found" AllowPaging="True" 
                 onpageindexchanging="grdTimeSheet_PageIndexChanging" PageSize="15">
             <Columns>
            <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Date%>"
 HeaderStyle-CssClass="header_bg_l"
             DataField="DateEntered" DataFormatString="{0:d}" >
<HeaderStyle CssClass="header_bg_l"></HeaderStyle>
                 </asp:BoundField>
             <%--<asp:BoundField HeaderText="Project" DataField="ProjectTitle" />--%>
             <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Project%>">
             <ItemTemplate>
             <a href="./ProjectOverviewV2.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectReference")%>">  <%# DataBinder.Eval(Container.DataItem, "ProjectTitle")%></a>
             </ItemTemplate>
             </asp:TemplateField>
              <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SubmittedDate%>">
                                                <ItemStyle Width="90px" />
                                                <ItemTemplate>
                                                <asp:Label ID="lblSSubmitDate" runat="server" Text='<%#  FormateDate(Eval("SubmittedDate", "{0:dd/MM/yyyy H:mm:ss}")) %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
              <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Task%>" DataField="ItemDescription" />
               <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Type%>" DataField="EntryTypeName" />
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Hours%>" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                 <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Approver%>" DataField="ApproverName" />                  
                 <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Status%>" DataField="ApproveTypeName"  HeaderStyle-CssClass="header_bg_r"/>
                  <%--<asp:BoundField HeaderText="Charged"  />
                   <asp:BoundField HeaderText="Charged By"  />
                    <asp:BoundField HeaderText="Date Changed" 
                     HeaderStyle-CssClass="header_bg_r"  >

                 </asp:BoundField>--%>
             </Columns>
             
             </asp:GridView>
            </div>
	</div>
</div>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
     //grid_responsive();
     grid_responsive_display();
     $(window).load(function () {
         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });
     });
    </script> 
</asp:Content>
