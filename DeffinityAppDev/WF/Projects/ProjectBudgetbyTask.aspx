<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectBudgetbyTask" MaintainScrollPositionOnPostback="true" Codebehind="ProjectBudgetbyTask.aspx.cs" %>
<%@ Register Src="controls/Project_BudgetSubTab.ascx" TagName="BudgetTab" TagPrefix="uc4"%>
<%-- --%>
<%--<%@ Register Assembly="Evyatar.Web.Controls" Namespace="Evyatar.Web.Controls" TagPrefix="evy" %>--%>
<%@ Register src="controls/ProjectTabs.ascx" tagname="ProjectTabs" tagprefix="uc1" %>
<%@ Register Src="controls/ProjectCost.ascx" TagName="ProjectCost" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Budget%> by Task - <Pref:ProjectRef ID="ProjectRef2" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <uc4:BudgetTab ID="BudgetTab1" runat="server" />
    <br />
    <div class="form-group">
         <ajaxToolkit:ModalPopupExtender CancelControlID="ImgCancel" ID="mpopBOM" runat="server"
                 PopupControlID="pnlBOM" TargetControlID="imgItemEdit"  
                 BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
      <asp:HiddenField ID="hd_TaskID" runat="server" />
      <asp:HiddenField ID="Hd_TaskName" runat="server" />
  <div><asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>  
                    
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
          ValidationGroup="project" />
      </div>
    </div>
    <div class="form-group">
     <uc2:ProjectCost ID="ProjectCost1" runat="server" />
        </div>
    <div class="form-group">
   <asp:Panel ID="pnlBOM" runat="server"  BackColor="White"
                 Style="display: none" Width="720px" Height="475px" 
                 BorderStyle="Double" ScrollBars="None" BorderColor="LightSteelBlue">
       <div class="row">
          <div class="col-md-12">
 <strong> <asp:Label ID="lblTaskName" runat="server"></asp:Label> </strong> 
<hr class="no-top-margin" />
	</div>
</div>
                
                    
                     <div><asp:Label ID="lblErr" runat="server" Text=""></asp:Label></div>
                    
                 <div style="text-align:right"><asp:Button ID="imgUpdate"
                             runat="server" SkinID="btnUpdate" OnClick="imgUpdate_Click" />
                             <asp:Button ID="ImgCancel" runat="server" SkinID="btnCancel" /> 
                             <asp:Button runat="server" ID="imgItemEdit"  style="display:none"/> </div>
               
            <asp:Panel ID="panel_grid" runat="server" Width="100%" Height="355px" ScrollBars="Auto">     
            <asp:GridView ID="GridView2" runat="server" Width="99%" AutoGenerateColumns="False"
            OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowEditing="GridView2_RowEditing"
            OnRowUpdating="GridView2_RowUpdating"  OnRowDeleting="GridView2_RowDeleting"  OnRowCommand="GridView2_RowCommand"
            DataKeyNames="ID" EmptyDataText="<%$ Resources:DeffinityRes,Nodataavailable%>">
            <Columns>
                <asp:TemplateField Visible='false'>
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        <%--<asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle Width="50px" CssClass="header_bg_l" />
                    <ItemStyle Width="50px" />
                     
                    <ItemTemplate>
                     
                        <asp:CheckBox ID="chkbox" runat="server" Enabled="<%#CommandField()%>"  />
                    </ItemTemplate>
                   
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Description%>" DataField="Description"/>
                    <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,QuantityBudgeted%>" DataField="QTY" ItemStyle-HorizontalAlign="Right"/>
                      <%--<asp:BoundField HeaderText="Quantity Available" DataField="QTY_aviable" ItemStyle-HorizontalAlign="Right"/>--%>
                      <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,QuantityAvailable%>"  ItemStyle-HorizontalAlign="Right">
                      <ItemTemplate>
                          <asp:Label ID="lblAvil" runat="server" Text='<%# Bind("QTY_aviable") %>'></asp:Label>
                      </ItemTemplate>
                     <%-- <EditItemTemplate>
                          <asp:TextBox ID="txtQtyReq" runat="server" Text='<%# Bind("QTY_Required") %>'></asp:TextBox>
                      </EditItemTemplate>--%>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,QuantityRequired%>" HeaderStyle-CssClass="header_bg_r" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                         <asp:TextBox ID="txtQtyReq" Width="50px" runat="server" Text='<%# Bind("QTY_Required") %>'></asp:TextBox>
                      </ItemTemplate>
                     <%-- <EditItemTemplate>
                          <asp:TextBox ID="txtQtyReq" runat="server" Text='<%# Bind("QTY_Required") %>'></asp:TextBox>
                      </EditItemTemplate>--%>
                      </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                     </asp:Panel>
                 </asp:Panel>
        </div>
    <div class="form-group">
   <asp:Panel ID="pnlProjectBudget" runat="server" >
     
    <asp:GridView ID="grdProjectBudget" runat="server" Width="100%" OnRowCommand="grdProjectBudget_RowCommand"
            EmptyDataText="<%$ Resources:DeffinityRes,Noservdataexists%>" 
            onrowcancelingedit="grdProjectBudget_RowCancelingEdit" 
            onrowediting="grdProjectBudget_RowEditing" 
            onrowupdating="grdProjectBudget_RowUpdating" 
            onrowdatabound="grdProjectBudget_RowDataBound" 
          onselectedindexchanged="grdProjectBudget_SelectedIndexChanged"  
           AllowPaging="true" onpageindexchanging="grdProjectBudget_PageIndexChanging" 
           PageSize="30"  >
         
            <Columns>
            
                <asp:TemplateField  HeaderStyle-CssClass="header_bg_l">
                  <%--  <HeaderStyle CssClass="header_bg_l" />--%>
                    
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                            CommandArgument='<%# Bind("ID")%>' Enabled="<%#CommandField()%>" SkinID="BtnLinkEdit"
                            ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                        </asp:LinkButton>
                        
                        <%--<asp:CheckBox ID="chkbox" runat="server" EnableViewState="true" />--%>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" CommandArgument='<%# Bind("ID")%>'
                            SkinID="BtnLinkUpdate" ToolTip="<%$ Resources:DeffinityRes,Update%>" 
                            ValidationGroup="Group9">
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>"></asp:LinkButton>
                    </EditItemTemplate>
                   
                    <FooterStyle Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectTask%>">
                   
                    <ItemStyle Width="200px" />
                    <ItemTemplate>
                        <asp:Label ID="lblProjectTask" runat="server" 
                            
                            Text='<%#getItemDes(DataBinder.Eval(Container.DataItem, "IndentLevel").ToString(),DataBinder.Eval(Container.DataItem, "ItemDescription").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                 <%--   <EditItemTemplate>
                        <asp:TextBox ID="txtProjectTask" runat="server" ></asp:TextBox>
                    </EditItemTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EstimatedHrs%>">
                    <ItemTemplate><asp:Label ID="lblEstimatedHrs" runat="server" 
                            
                            Text='<%# Changehours(ConvertMinutesToHours(DataBinder.Eval(Container.DataItem,"EstimatedHrs").ToString()).ToString())%>'></asp:Label></ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtEstimatedHrs" runat="server"  Width="70px" 
                             Text='<%#Changehours(ConvertMinutesToHours(DataBinder.Eval(Container.DataItem,"EstimatedHrs").ToString()).ToString())%>' 
                             SkinID="Price"></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Fee%>" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblFee1" runat="server" 
                            Text='<%# Bind("Fee" , "{0:N2}") %>'></asp:Label></ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtFee" runat="server"  Width="75px" 
                             Text='<%# Bind("Fee" , "{0:N2}") %>' SkinID="Price"></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,PercentageofProject%>" 
                    ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblProjectFee2" runat="server" 
                            Text='<%# Bind("PrProject", "{0:N2}") %>'></asp:Label></ItemTemplate>
                     
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Cost%>" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblCost" runat="server" 
                            Text='<%# Bind("Cost", "{0:N2}") %>' ></asp:Label></ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtCost" runat="server" Width="75px" 
                             Text='<%# Bind("Cost", "{0:N2}") %>' SkinID="Price"></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,PercentageofCost%>" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblProjectFee1" runat="server" 
                            Text='<%# Bind("PrCost", "{0:N2}") %>'></asp:Label></ItemTemplate>
                     
                    </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ResourceFee%>" 
                    ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblResourceFee" runat="server" 
                            Text='<%# Bind("ResourceFee", "{0:N2}") %>'></asp:Label></ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtResourceFee" runat="server" Width="75px" 
                             Text='<%# Bind("ResourceFee", "{0:N2}") %>' SkinID="Price"></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>
                    
                     
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ResourceFeePercentage%>" 
                    ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblProjectFee" runat="server" 
                            Text='<%# Bind("prResourceFee", "{0:N2}") %>'></asp:Label></ItemTemplate>
                     
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ResourceCost%>" 
                    ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblResourceCost1" 
                            runat="server" Text='<%# Bind("ResourceCost", "{0:N2}") %>'></asp:Label></ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="txtResourceCost" runat="server" Width="75px" 
                             Text='<%# Bind("ResourceCost", "{0:N2}") %>' SkinID="Price"></asp:TextBox></EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ResourceCostPercentage%>" 
                    ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblResourceCost2" 
                            runat="server" Text='<%# Bind("prResourceCost", "{0:N2}") %>'></asp:Label></ItemTemplate>
                                         </asp:TemplateField>
                     
                      <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ResourceProfit%>" 
                    ItemStyle-HorizontalAlign="Right" >
                    <ItemTemplate>
                        <asp:Label ID="lblResourceProfit" 
                            runat="server" Text='<%# Bind("ResourceProfit", "{0:N2}") %>'></asp:Label></ItemTemplate>
                     <%--<EditItemTemplate>
                         <asp:TextBox ID="txtResourceProfit" runat="server" Width="75px"></asp:TextBox></EditItemTemplate>--%>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Actual Hours">
                    <ItemTemplate>
                    <asp:Label ID="lblhours" runat="server" Text='<%# ChangeHoues(Eval("ActualHours").ToString())%>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Labour Cost to Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLabourCost" runat="server" Text='<%# Bind("lbrCost", "{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="header_bg_r" HeaderText="<%$ Resources:DeffinityRes,BOM%>">
                     <ItemTemplate> 
                         <asp:LinkButton ID="imgBOM" OnClick="imgItemEdit1_Click" SkinID="BtnLinkList" CommandArgument='<%# Bind("ID")%>' runat="server" /></ItemTemplate>
                     </asp:TemplateField>  
                     
                                   
                             <asp:TemplateField Visible='false'>
                    <ItemTemplate>
                        <asp:Label ID="lblID1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        
                    </ItemTemplate>
                </asp:TemplateField>             
                    </Columns>
                   <%-- <HeaderStyle CssClass="fixHeader" Height="20px" />--%>
                    </asp:GridView>
                 
                    </asp:Panel>
        </div>
<style type="text/css">
  /*.fixHeader
  {
    
 font-weight:bold;  position:relative; 
                 top:expression(this.parentNode.parentNode.parentNode.scrollTop-1);
   
  }*/
</style>
    <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();
        activeTab('budget');
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

