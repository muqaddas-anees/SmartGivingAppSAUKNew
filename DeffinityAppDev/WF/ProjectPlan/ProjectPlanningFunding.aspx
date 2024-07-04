<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/WF/MainTab.master" Inherits="ProjectPlanFunding" Title="Project Planning Funding" Codebehind="ProjectPlanningFunding.aspx.cs" %>
<%@ Register src="controls/ProjectProposalTab.ascx" tagname="ProjectProposalTab" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc2:ProjectProposalTab ID="ProjectProposalTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectProposal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
   <%= Resources.DeffinityRes.ProjectPlanningFunding%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink runat="Server" Text="Back to Project Proposal" NavigateUrl="~/WF/ProjectPlan/ProjectPipeline.aspx?Status=8"><i class="fa fa-arrow-left"></i> Return to Project Proposal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="form-group">
             <div class="col-md-12">
                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
                            <asp:Label ID="lblError" runat="server"  ForeColor="Red" ></asp:Label>
                            <asp:Label ID="lblerror1" runat="server" ForeColor=red></asp:Label>
                 </div>
         </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.FundingRequired%></label>
                                      <div class="col-sm-8 form-inline"> <asp:TextBox ID="txtFund" runat="server" SkinID="txt_80"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFund"
                                Display="None" ErrorMessage="Please Enter Project Plan Details" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFund"
                                Display="None" ErrorMessage="Enter valid data" ValidationExpression="^\d*\.?\d*$"
                                ValidationGroup="Group1"></asp:RegularExpressionValidator>
					</div>
				</div>
                </div>
     <div class="form-group text-bold">
         <div class="col-md-12">
         <p class="text-bold">Please describe how your project meets the criteria for funding</p>
             </div>
         </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Criteriaforfunding%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtCriteria" runat="server" TextMode="MultiLine" MaxLength="500" SkinID="txtMulti" Height="50px"></asp:TextBox>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.AvailabilityofExternalFunding%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtAvailable" runat="server" TextMode="MultiLine"  MaxLength="500" SkinID="txtMulti" Height="50px"></asp:TextBox>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8"><asp:Button ID=btnsave runat=server SkinID="btnSubmit" OnClick="btnsave_Click" ValidationGroup="Group1" /></div>
                 </div>
         </div>

	<asp:HiddenField ID="HiddenField2" runat="server" /><asp:HiddenField ID="HiddenField3" runat="server" />
</asp:Content>