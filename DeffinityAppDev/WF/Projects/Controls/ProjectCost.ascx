<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProjectCost" Codebehind="ProjectCost.ascx.cs" %>

    <div class="form-group-no-margin" style="margin-top:10px">
         <div class="col-md-6 no-padding">
               <label class="col-sm-3 control-label no-right-padding"><%= Resources.DeffinityRes.ProjectFee%></label>
         <div class="col-sm-3 no-left-padding"><asp:TextBox ID="txtProjectFee" runat="server" SkinID="Price_100px"></asp:TextBox>
					</div>
              <label class="col-sm-3 control-label no-right-padding"><%= Resources.DeffinityRes.BudgetedCost%></label>
        <div class="col-sm-3 form-inline no-left-padding"><asp:TextBox ID="txtBudgetedCost" runat="server" SkinID="Price_100px"></asp:TextBox> <asp:Label ID="imgBudget" runat="server" Visible="false" ToolTip="Total value of all worksheets exceeds the budget" ></asp:Label>
            </div>
             </div>
         <div class="col-md-6 no-padding">
              <label class="col-sm-3 control-label no-right-padding"><%= Resources.DeffinityRes.BudgetGrossProfit%></label>
        <div class="col-sm-2 no-left-padding">
             <asp:TextBox ID="txtBudGrossProfit" runat="server" SkinID="Price_75px"></asp:TextBox>
            </div>
             <label class="col-sm-2 control-label no-right-padding"><%= Resources.DeffinityRes.BudgetedGP%></label>
            <div class="col-sm-2 no-left-padding">
            <asp:TextBox ID="txtBudgetedGp" runat="server" SkinID="Price_75px"></asp:TextBox>
            </div>
              <div class="col-sm-1 no-left-padding">
                   <asp:Button ID="imgSave" runat="server" SkinID="btnSave"  onclick="imgSave_Click" CausesValidation="false" />
              </div>
             </div>
       </div>


