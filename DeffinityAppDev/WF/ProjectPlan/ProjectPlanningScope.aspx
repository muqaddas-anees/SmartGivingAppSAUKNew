<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainTab.master" Inherits="ProjectPlanningScope" Title="Project Planning Scope" Codebehind="ProjectPlanningScope.aspx.cs" %>
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
   <%= Resources.DeffinityRes.Scope%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink runat="Server" Text="Back to Project Proposal" NavigateUrl="~/WF/ProjectPlan/ProjectPipeline.aspx?Status=8"><i class="fa fa-arrow-left"></i> Return to Project Proposal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
             <div class="col-md-12">
                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
              <asp:Label ID="lblError" runat="server" ForeColor="Red" ></asp:Label>
	     <asp:Label ID="lblerror1" runat="server"  ForeColor="red" ></asp:Label>
                 </div>
        </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Assumptions%></label>
                                      <div class="col-sm-9 form-inline"><asp:TextBox ID="txtAssum" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAssum"
              Display="None" ErrorMessage="Please Enter data in assumptions" ValidationGroup="Group1"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Constraints%></label>
                                      <div class="col-sm-9 form-inline"><asp:TextBox ID="txtConstraint" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtConstraint"
              Display="None" ErrorMessage="Please Enter data in constraints" ValidationGroup="Group1"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Approvals%></label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtApprove" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Dependencies%></label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtDepend" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.RelatedDocumentLinks%></label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtRelat" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9"><asp:Button ID="btnsave" runat="server" SkinID="btnSubmit" OnClick="btnsave_Click" ValidationGroup="Group1" /> 
					</div>
				</div>
                </div>


    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:HiddenField ID="HiddenField3" runat="server" />

</asp:Content>