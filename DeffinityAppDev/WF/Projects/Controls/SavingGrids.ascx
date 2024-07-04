<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_SavingGrids" Codebehind="SavingGrids.ascx.cs" %>
<%@ Register Src="~/WF/Projects/controls/BudgetSavingGrid.ascx" TagName="Saving" TagPrefix="uc3" %>
<div class="form-group">
      <div class="col-md-2">
          
	</div>
	<div class="col-md-8">
          <div class="form-group well">

     <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.ForecastedProjectCost%></label>
           <div class="col-sm-4">
               <span class="pull-right">
               <asp:Label ID="lblForecastedProjectCost" runat="server" Font-Bold="true" Text="XXXX"></asp:Label>
                   </span>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.MaterialsSaving%></label>
           <div class="col-sm-4">
               <span class="pull-right">
                <asp:Label ID="lblMaterialsSaving" runat="server" Font-Bold="true" Text="XXXX"></asp:Label>
                   </span>
            </div>
	</div>
</div>
     <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.Spenttodate%></label>
           <div class="col-sm-4">
               <span class="pull-right">
                <asp:Label ID="lblSpenttoDate" Font-Bold="true" runat="server" Text="XXXX"></asp:Label>
                   </span>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.LabourSaving%></label>
          <div class="col-sm-4">
              <span class="pull-right">
               <asp:Label ID="lblLabourSaving" runat="server" Font-Bold="true" Text="XXXX"></asp:Label>
                  </span>
            </div>
	</div>
</div>
     <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-12 control-label"></label>
	</div>
	<div class="col-md-6">
           <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.StaffHoursSaving%></label>
          <div class="col-sm-4">
              <span class="pull-right">
                <asp:Label ID="lblStaffHoursSaving" runat="server" Font-Bold="true" Text="XXXX"></asp:Label>
                  </span>
            </div>
	</div>
</div>
     <div class="form-group">
      <div class="col-md-6">
         <label class="col-sm-12 control-label"></label>
	</div>
	<div class="col-md-6">
           <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.PMHoursSaving%></label>
          <div class="col-sm-4">
              <span class="pull-right">
                <asp:Label ID="lblPMHoursSaving" runat="server" Font-Bold="true" Text="XXXX"></asp:Label>
                  </span>
            </div>
	</div>
</div>
     <div class="form-group">
      <div class="col-md-6">
         <label class="col-sm-12 control-label"></label>
	</div>
	<div class="col-md-6">
           <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.MiscellaneousSavings%></label>
            <div class="col-sm-4">
                <span class="pull-right">
                <asp:Label ID="lblMiscSavings" runat="server" Font-Bold="true" Text="XXXX"></asp:Label>
                    </span>
            </div>
	</div>
</div>
     <div class="form-group">
      <div class="col-md-6">
          <label class="col-sm-12 control-label"></label>
	</div>
	<div class="col-md-6">
           <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.ExpenseSavings%></label>
          <div class="col-sm-4">
              <span class="pull-right">
                 <asp:Label ID="lblExpenseSavings" runat="server" Font-Bold="true" Text="XXXX"></asp:Label>
                  </span>
            </div>
	</div>
</div>
    <div class="form-group">
          <div class="col-md-6">
                   <label class="col-sm-12 control-label"></label>
          </div>
           <div class="col-md-6">
                <hr />
           </div>
    </div>
     <div class="form-group">
         <div class="col-md-6">
     <label class="col-sm-12 control-label"></label>
         </div>
 	<div class="col-md-6">
           <label class="col-sm-8 control-label"><%= Resources.DeffinityRes.SavingstoDate%></label>
          <div class="col-sm-4">
              <span class="pull-right">
                  <asp:Label ID="lblSavingstoDate" runat="server" Font-Bold="true" Text="XXXX"></asp:Label>
                  </span>
            </div>
	</div>
</div>
    </div>
	</div>
	<div class="col-md-2">
           
	</div>
</div>


  <div>
           <uc3:Saving ID="savingGrid" runat="server" Visible="true" />
  </div>

  <h5><%= Resources.DeffinityRes.LabourSavings%><%--Labour Savings--%></h5>
<hr />
  <asp:GridView ID="LabourSavingsGrid" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found" Width="100%">
             <Columns>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Name%>"  >
                     <ItemTemplate>
                         <asp:Label ID="lblname" runat="server" Text='<%#Bind("UserName") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,BOMName%>">
                     <ItemTemplate>
                         <asp:Label ID="lblBOM" runat="server" Text='<%#Bind("BOMName") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,WCDate%>" Visible="false">
                     <ItemTemplate>
                         <asp:Label ID="lblWCDate" runat="server" Text='<%#Bind("Datemodified") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Budget%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblBudgetHrs" runat="server" Text='<%#Bind("BudgetQty","{0:F2}") %>'>></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Actuals%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblActualsHrs" runat="server" Text='<%#Bind("ActualQty","{0:F2}") %>'>></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Saving%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingHours" runat="server" Text='<%#Bind("Differ","{0:F2}") %>'>></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SavingCost%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingCost" runat="server" Text='<%#Bind("SavingCost","{0:F2}") %>'>></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
      </asp:GridView>
 
 <h5><%= Resources.DeffinityRes.MaterialsSaving%></h5>
<hr />
  <asp:GridView ID="MaterialsSavingGrid" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found" Width="100%">
             <Columns>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ItemDescription%>"  >
                     <ItemTemplate>
                         <asp:Label ID="lblname" runat="server" Text='<%#Bind("BOMName") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,QtyBudgeted%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblWCDate" runat="server" Text='<%#Bind("BudgetQty","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,BudgetedCost%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblBudgetHrs" runat="server" Text='<%#Bind("BudgetedCost","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ActualUsed%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblActualsHrs" runat="server" Text='<%#Bind("ActualQty","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ActualCost%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingHours" runat="server" Text='<%#Bind("ActualCost","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Saving%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingCost" runat="server" Text='<%#Bind("SavingCost","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
 
 <h5><%=Resources.DeffinityRes.MiscellaneousSavings%></h5>
<hr />
  <asp:GridView ID="MiscellaneousSavingsGrid" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found" Width="100%">
            <Columns>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ItemDescription%>"  >
                     <ItemTemplate>
                         <asp:Label ID="lblname" runat="server" Text='<%#Bind("BOMName") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,QtyBudgeted%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblWCDate" runat="server" Text='<%#Bind("BudgetQty","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,BudgetedCost%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblBudgetHrs" runat="server" Text='<%#Bind("BudgetedCost","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ActualUsed%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblActualsHrs" runat="server" Text='<%#Bind("ActualQty","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ActualCost%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingHours" runat="server" Text='<%#Bind("ActualCost","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Saving%>"   ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingCost" runat="server" Text='<%#Bind("SavingCost","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
    </asp:GridView>
  
 <h5><%= Resources.DeffinityRes.PMHours%></h5>
<hr />
   <asp:GridView ID="PMHoursGrid" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found" Width="100%">
            <Columns>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Name%>"  >
                     <ItemTemplate>
                         <asp:Label ID="lblname" runat="server" Text='<%#Bind("project") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,BudgetHrs%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblWCDate" runat="server" Text='<%#Bind("BudgetValue") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ActualsHrs%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblBudgetHrs" runat="server" Text='<%#Bind("ActualValue") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SavingsinHours%>"   ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblActualsHrs" runat="server" Text='<%#Bind("Differ") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SavingHrs%>" Visible="false">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingHours" runat="server"></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SavingCost%>" Visible="false">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingCost" runat="server"></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
      </asp:GridView>

 <h5><%= Resources.DeffinityRes.StaffHours%></h5>
<hr />
 <asp:GridView ID="GridStaffHours" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found" Width="100%">
            <Columns>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Name%>"  >
                     <ItemTemplate>
                         <asp:Label ID="lblname" runat="server" Text='<%#Bind("project") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,BudgetHrs%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblWCDate" runat="server" Text='<%#Bind("BudgetValue") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ActualsHrs%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblBudgetHrs" runat="server" Text='<%#Bind("ActualValue") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SavingsinHours%>"   ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblActualsHrs" runat="server" Text='<%#Bind("Differ") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SavingHrs%>" Visible="false">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingHours" runat="server"></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SavingCost%>" Visible="false">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingCost" runat="server"></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
      </asp:GridView>
  
 <h5><%=Resources.DeffinityRes.Variations%></h5>
<hr />
   <asp:GridView ID="VariationsGrid" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found" Width="100%">
           <Columns>
               
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ValueofUnapprovedVariations%>"   ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblWCDate" runat="server" Text='<%#Bind("UnApprovedValue","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,CostofUnapprovedVariations%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblname" runat="server" Text='<%#Bind("UnApprovedCost","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ValueofApprovedVariations%>" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblActualsHrs" runat="server" Text='<%#Bind("ApprovedValue","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,CostofApprovedVariations%>"   ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="lblBudgetHrs" runat="server" Text='<%#Bind("ApprovedCost","{0:F2}") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SavingHrs%>" Visible="false">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingHours" runat="server"></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SavingCost%>" Visible="false">
                     <ItemTemplate>
                         <asp:Label ID="lblSavingCost" runat="server"></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
      </asp:GridView>
  