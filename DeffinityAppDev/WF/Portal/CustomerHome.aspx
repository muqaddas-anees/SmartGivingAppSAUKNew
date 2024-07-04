<%@ Page Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="CustomerHome" Title="Customer Home" Codebehind="CustomerHome.aspx.cs" %>

<%@ Register src="~/WF/Projects/MailControls/ProjectIssue.ascx" tagname="ProjectIssue" tagprefix="uc4" %>
<%--<%@ Register src="controls/CustomerHomeTabs.ascx" tagname="CustomerHomeTabs" tagprefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
<%: Resources.DeffinityRes.CustomerPortal %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="panel_title" runat="Server">
   Projects
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
<style type="text/css">
   .pgbar1{
background:url(../media/progress_yellow.gif) repeat-x;   

}
</style>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <asp:Label SkinID="Loading" ID="lblLoading" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="form-group">
             <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <asp:Label ID="lblStatus" runat="server" Text="Status" EnableViewState="false" /></label>
                                      <div class="col-sm-8"> <asp:DropDownList  ID="ddlStatus" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" SkinID="ddl_40" />
                                           <asp:Label ID="lblFiler" runat="server" Visible="false" Text="Filter By" EnableViewState="false" />&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                    DataSourceID="objContractors" DataValueField="ID" DataTextField="Name" 
                    onselectedindexchanged="ddlCustomer_SelectedIndexChanged" Visible="false">
                    <asp:ListItem Text="Show All" Value="0"  />
                </asp:DropDownList>
                <asp:ObjectDataSource ID="objContractors" runat="server" TypeName="DataHelperClass"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="LoadAllContractors" />
					</div>
				</div>
                </div>

    <asp:Panel Width="100%" runat="server" ID="Panel2">
          
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div><asp:Label ID="lblMsg" runat="server" ForeColor="Red" Visible="false" EnableViewState="false"></asp:Label>  </div>
    <div><asp:Label ID="lblError" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label></div>
        
                <asp:GridView ID="GridView2" Width="100%" runat="server" OnRowCommand="GridView2_RowCommand"
                     RowStyle-HorizontalAlign="Center" OnSorting="GridView2_Sorting"
                    AllowSorting="True" EmptyDataText="No projects available." 
                    onrowcreated="GridView2_RowCreated" 
                    onrowdatabound="GridView2_RowDataBound1" SkinID="gv_responsive">
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_sendIssue" runat="server" />
                                <asp:HiddenField ID="HID" runat="server" Value='<%#Eval("ProjectReference")%>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="header_bg_l" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Project<br />Ref" ItemStyle-HorizontalAlign="Center"
                            SortExpression="ProjectReferencePrefix">
                            <ItemTemplate>
                            <a href="CustomerProjectOverview.aspx?portal=Yes&project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>" target="_self" ><%#DataBinder.Eval(Container.DataItem, "ProjectReferencePrefix")%> </a>
                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectTitle" HeaderText="Title" ItemStyle-HorizontalAlign="Left" />
                       <%-- <asp:TemplateField HeaderText="View<br/>Details" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Panel ID="pnlHover" runat="server">
                                    <table width="250px" cellpadding="0" cellspacing="1" style="border-color: ButtonFace;
                                        border-width: 3px; border-style: double">
                                        <tr>
                                            <td class="tab_header_Bold" style="width: 50%">
                                                <asp:Literal ID="title" runat="server" Text="Title" />
                                            </td>
                                            <td style="color: #333333; background-color: #F7F6F3;" style="width: 50%">
                                                <b>
                                                    <asp:Literal ID="litProjectTitle" runat="server" Text='<%#Eval("ProjectTitle")%>' />
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tab_header_Bold">
                                                <asp:Literal ID="Description" runat="server" Text="Description" />
                                            </td>
                                            <td style="color: #333333; background-color: #F7F6F3;">
                                                <b>
                                                    <asp:Literal ID="litDescription" runat="server" Text='<%#Eval("ProjectDescription")%>' />
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tab_header_Bold">
                                                <asp:Literal ID="Owner" runat="server" Text="Owner" />
                                            </td>
                                            <td style="color: #333333; background-color: #F7F6F3;">
                                                <b>
                                                    <asp:Literal ID="litOwner" runat="server" Text='<%#Eval("ContractorName")%>' />
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tab_header_Bold">
                                                <asp:Literal ID="Site" runat="server" Text="Site" />
                                            </td>
                                            <td style="color: #333333; background-color: #F7F6F3;">
                                                <b>
                                                    <asp:Literal ID="litCustomer" Text='<%#Eval("Site")%>' runat="server" />
                                                </b>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Label ID="imgPopUp"  runat="server" Text="view"  />
                                <ajaxToolkit:HoverMenuExtender ID="hoverMenu" runat="server" PopDelay="25" PopupControlID="pnlHover"
                                    TargetControlID="imgPopUp" CacheDynamicResults="false" PopupPosition="Right" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" HtmlEncode="False"
                            DataFormatString="{0:d}" SortExpression="StartDate" />
                        <asp:BoundField DataField="ProjectEndDate" HeaderText="End Date" HtmlEncode="False"
                            DataFormatString="{0:d}" SortExpression="ProjectEndDate" />
                             <asp:BoundField DataField="Status" HeaderText="Status"
                             SortExpression="Status" />
                              <asp:BoundField DataField="ProgrammeName" HeaderText="Programme"
                             SortExpression="ProgrammeName" />
<asp:TemplateField HeaderText="Project<br />Files" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            <a href="CustomerProjectDocuments.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>" target="_self" ><asp:Label SkinID="" ID="imgProjectFiles" AlternateText="Project Files"  runat="server" Text="File" /> </a>                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>                             
                             <asp:BoundField DataField="CustomerReference" HeaderText="Customer PO Number"
                             SortExpression="CustomerReference" />
                             <asp:TemplateField HeaderText="PO Days Remaining" ItemStyle-HorizontalAlign="Right" >
                             <ItemStyle Width="100px" />
                             <ItemTemplate >
                                <%-- <asp:Label ID="Label3" runat="server" Text='<%#ReturnDays(DataBinder.Eval(Container.DataItem,"Days").ToString())%>'></asp:Label>--%>
                                <asp:Label ID="Label3" runat="server"  Visible='<%#POVisible(DataBinder.Eval(Container.DataItem,"POCheck").ToString())%>'  ForeColor='<%#foreColor(DataBinder.Eval(Container.DataItem,"Days").ToString())%>' Text='<%#ReturnDays(DataBinder.Eval(Container.DataItem,"Days").ToString(),DataBinder.Eval(Container.DataItem,"CustomerReference").ToString(),DataBinder.Eval(Container.DataItem,"DDays").ToString(),DataBinder.Eval(Container.DataItem,"TotalHrs").ToString())%>'></asp:Label>
                             </ItemTemplate>
                             </asp:TemplateField>
                             
                             
                            
                                                    
                             <asp:TemplateField HeaderText="% PO Remaining" ItemStyle-HorizontalAlign="Right" >
                              <ItemStyle Width="100px" />
                             <ItemTemplate >
                              <asp:Label ID="lblCR" runat="server" Text='<%#Bind("CustomerReference")%>' Visible="false"></asp:Label>
                               <asp:Label ID="lblDDays" runat="server" Text='<%#Bind("DDays")%>' Visible="false"></asp:Label>
                             <asp:Label ID="lblTotalDays" runat="server" Text='<%#Bind("TotalHrs")%>' Visible="false"></asp:Label>
                             
                              <asp:Label ID="lblDays" runat="server" Text='<%#Bind("Days")%>' Visible="false" ></asp:Label>
                                 <asp:Label ID="lblProgress" runat="server" Text=""  Visible='<%#POVisible(DataBinder.Eval(Container.DataItem,"POCheck").ToString())%>' ></asp:Label>
                                 <asp:Label ID="lblPer" runat="server" Text='<%# Bind("per")%>' Visible="false"></asp:Label>
                             </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Check Point" ItemStyle-HorizontalAlign="Center" >
                             
                             <ItemTemplate >
                             <asp:LinkButton ID="Image4" runat="server" Text="Blue"  CommandName="CheckPoint" CommandArgument='<%# Bind("ProjectReference") %>'  Visible='<%#CheckPointVisible(DataBinder.Eval(Container.DataItem,"ProjectReference").ToString())%>'  />
                               <%--<asp:Image ID="imgCheckPoint"  runat="server" ImageUrl="~/media/ico_indcate_blue.gif"   Visible='<%#CheckPointVisible(DataBinder.Eval(Container.DataItem,"ProjectReference").ToString())%>' />--%>
                             </ItemTemplate>
                             </asp:TemplateField>
                             <%--<asp:BoundField DataField="Days" HeaderText="PO Days Remaining"
                             SortExpression="Days"  DataFormatString="{0:c}" />--%>
                        <asp:TemplateField >
         <ItemTemplate>
        <a href="#" onclick="window.open('CustomerTaskItems.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>&Event=Green',null,'height=450 width=750 scrollbars=yes')" ><%#DataBinder.Eval(Container.DataItem, "GREEN")%> </a>
         </ItemTemplate>
         </asp:TemplateField>    
         <asp:TemplateField>
         <ItemTemplate>
        <a href="#" onclick="window.open('CustomerTaskItems.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>&Event=Amber',null,'height=450 width=750 scrollbars=yes')" ><%#DataBinder.Eval(Container.DataItem, "AMBER")%> </a>
         </ItemTemplate>
         </asp:TemplateField>  
         <asp:TemplateField  HeaderStyle-CssClass="header_bg_r">
         <ItemTemplate>
        <a href="#" onclick="window.open('CustomerTaskItems.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>&Event=Red',null,'height=450 width=750 scrollbars=yes')" ><%#DataBinder.Eval(Container.DataItem, "RED")%> </a>
         </ItemTemplate>
             <HeaderStyle CssClass="header_bg_r" />
         </asp:TemplateField>       
                       
                       
                    </Columns>
                </asp:GridView>
                
              <ajaxtoolkit:modalpopupextender id="mdlIssue" cancelcontrolid="imgClose"
                        runat="server" backgroundcssclass="modalBackground" targetcontrolid="imgSubmit"
                        popupcontrolid="pnlIssue">
                    </ajaxtoolkit:modalpopupextender>
            <asp:LinkButton ID="imgSubmit" runat="server" SkinID="BtnLinkBack" Style="display:none;" />
            <asp:LinkButton ID="imgClose" runat="server" SkinID="BtnLinkBack" Style="display:none;" />
            <asp:Button ID="btnRaiseIssue" runat="server" Text="Raise an Issue" OnClick="btnRaiseIssue_Click" />
            <asp:Panel ID="pnlIssue" runat="server" Width="50%" Height="300px" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue">
                 <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                            Project Issue <asp:HiddenField ID="h_project" runat="server" Value="0" /> <asp:HiddenField ID="h_issue" runat="server" Value="0" />
                        </div>
                <table style="width:100%">
                      <tr>
                        <td>Project</td><td colspan="3"><asp:Literal ID="lit_projectTitle" runat="server" ></asp:Literal> </td>
                    </tr>
                    <tr>
                        <td>Date Logged</td><td><asp:TextBox ID="txtDateLogged" runat="server" SkinID="txtCalender"></asp:TextBox> <asp:Label SkinID="Calender" ID="imgCalender" runat="server" /> </td>
                        <td>Status</td><td><asp:DropDownList ID="ddlIssueStatus" runat="server" DataTextField="Status" DataValueField="ID"
                        Width="150px" DataSourceID="objDsStatus" ></asp:DropDownList> 
                       <asp:ObjectDataSource ID="objDsStatus" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaItemstatus"></asp:ObjectDataSource>
                           
                                       </td>
                    </tr>
                    <tr>
                        <td>Issue</td><td colspan="3"><asp:TextBox ID="txtIssueTitle" runat="server" Width="450px" TextMode="MultiLine" Height="70px"></asp:TextBox> </td>
                    </tr>
                    <tr>
                        <td>Location</td><td><asp:DropDownList ID="ddlIssueLocation" runat="server" Width="150px" DataValueField="SiteID" DataSourceID="objSiteByProjectRef" DataTextField="Site"></asp:DropDownList>
                                 <asp:ObjectDataSource ID="objSiteByProjectRef" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataSitesByProjectRef" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaSiteByProjectRef">
                        <SelectParameters>
                            <%--<asp:QueryStringParameter Name="ProjectRefrence" DefaultValue="0" QueryStringField="Project" Type="Int32" />--%>
                            <asp:ControlParameter DefaultValue="0" Name="ProjectReference" ControlID="h_project" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                                         </td>
                        <td>RAG Status</td><td><asp:DropDownList ID="ddlRag" runat="server" Width="150px">  <asp:ListItem Text="Please select..." Value="0"></asp:ListItem>
                        <asp:ListItem Text="GREEN" Value="1"></asp:ListItem>
                        <asp:ListItem Text="AMBER" Value="2"></asp:ListItem>
                        <asp:ListItem Text="RED" Value="3"></asp:ListItem></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td></td><td><asp:Button ID="btnSubmitIssue" runat="server" SkinID="btnSubmit" OnClick="btnSubmitIssue_Click" /> <asp:Button ID="btnIssueCancel" runat="server" SkinID="btnClose" OnClick="btnIssueCancel_Click" />  </td>
                        <td></td><td></td>
                    </tr>
                  
                </table>
            </asp:Panel>
           
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnSubmitIssue" />            
           
          </Triggers>
    </asp:UpdatePanel>
     </asp:Panel>
     <div>
        <uc4:ProjectIssue ID="ProjectIssue1" runat="server" Visible="false" />
    </div>
       <div  id="Div2" runat="server">
           <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Tasks Assigned to me </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
  
<asp:GridView ID="grdAssignedTask" runat="server" Width="100%" 
            EmptyDataText="No Records Found." AllowPaging="True" 
        onpageindexchanging="grdAssignedTask_PageIndexChanging" PageSize="10" 
        onrowcommand="grdAssignedTask_RowCommand" 
        onrowdatabound="grdAssignedTask_RowDataBound" 
        onrowcancelingedit="grdAssignedTask_RowCancelingEdit" 
        onrowediting="grdAssignedTask_RowEditing" onrowupdating="grdAssignedTask_RowUpdating" SkinID="gv_responsive" 
             >
         
            <Columns>
             <asp:TemplateField  HeaderStyle-CssClass="header_bg_l" >
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit" />
                                            
                                            
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                CommandArgument="<%# Bind('ID')%>"  SkinID="BtnLinkUpdate"
                                                ToolTip="Update"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
              
                    <asp:TemplateField HeaderText="Project">
                   
                    <ItemStyle />
                   <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server"  Text='<%# Bind("ProjectReferenceWithPrefix")%>' 
                        CommandName="View" CommandArgument='<%# Bind("ProjectReference")%>' >
                       </asp:LinkButton>
                        <asp:Label ID="lblProject" runat="server"  Visible="false"
                            
                            Text='<%# Bind("ProjectReference")%>'></asp:Label>
                    </ItemTemplate>
               

               
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Task">
                    <ItemTemplate><asp:Label ID="lblTask" runat="server" 
                            
                             Text='<%# Bind("TaskTitle")%>'></asp:Label></ItemTemplate>
                    
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Start Date" >
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" 
                            Text='<%# Bind("ProjectStartDate", "{0:d}") %>'></asp:Label></ItemTemplate>
                    
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="End Date" 
                    >
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" 
                            Text='<%# Bind("ProjectEndDate", "{0:d}")%>'></asp:Label></ItemTemplate>
                     
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Status" >
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" 
                            Text='<%# Bind("ItemStatus") %>' ></asp:Label>
                         
                            
        </ItemTemplate>
                    <EditItemTemplate>
                       <asp:Label ID="lblStatusID" runat="server" 
                            Text='<%# Bind("StatusID") %>' Visible="false" ></asp:Label>
                                        <asp:DropDownList 
          ID="ddlStatus" runat="server" Width="90px" 
         
         >
      </asp:DropDownList>
                    </EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Notes"   HeaderStyle-CssClass="header_bg_r">
                    <ItemTemplate>
                        <asp:Label ID="lblNotes" runat="server" 
                            Text='<%# Bind("Notes") %>'></asp:Label></ItemTemplate>
                     
<HeaderStyle CssClass="header_bg_r"></HeaderStyle>
                     
                    </asp:TemplateField>
                                          
                    </Columns>
                   <%-- <HeaderStyle CssClass="fixHeader" Height="20px" />--%>
                    </asp:GridView>
</div>
    <div  id="Div4" runat="server" >
        <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.Issues%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
  
<asp:GridView ID="grdIssues" runat="server" Width="100%" 
            EmptyDataText="No Records Found." AllowPaging="True" 
       PageSize="10" onpageindexchanging="grdIssues_PageIndexChanging" onrowcommand="grdIssues_RowCommand"  SkinID="gv_responsive"
             >
         
            <Columns>
            
              
                    <asp:TemplateField HeaderText="Project"  HeaderStyle-CssClass="header_bg_l">
                   
                    <ItemStyle />
                  <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server"  Text='<%# Bind("ProjectReferenceWithPrefix")%>' 
                        CommandName="View" CommandArgument='<%# Bind("IssueID")%>' >
                       </asp:LinkButton>
                        <asp:Label ID="lblProject" runat="server"  Visible="false"
                            
                            Text='<%# Bind("ProjectReference")%>'></asp:Label>
                      <asp:HiddenField ID="hpid" runat="server" Value='<%# Bind("ProjectReference") %>' />
                    </ItemTemplate>
               
<HeaderStyle CssClass="header_bg_l"></HeaderStyle>
               
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Title">
                    <ItemTemplate><asp:Label ID="lblTitle" runat="server" 
                            
                             Text='<%# Bind("ProjectTitle")%>'></asp:Label></ItemTemplate>
                    
                    </asp:TemplateField>
                    
                 
                    
                  
                    
                    <asp:TemplateField HeaderText="Status" >
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" 
                            Text='<%# Bind("ProjectStatusName") %>' ></asp:Label></ItemTemplate>
                    
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Issue"   HeaderStyle-CssClass="header_bg_r">
                    <ItemTemplate>
                        <asp:Label ID="lblIssue" runat="server" 
                            Text='<%# Bind("Issue") %>'></asp:Label></ItemTemplate>
                     
<HeaderStyle CssClass="header_bg_r"></HeaderStyle>
                     
                    </asp:TemplateField>
                          
                    </Columns>
                   <%-- <HeaderStyle CssClass="fixHeader" Height="20px" />--%>
                    </asp:GridView>

</div>
 

    
  <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
              $(".dropdown-menu li")
            .find("input[type='checkbox']")
            .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>
</asp:Content>