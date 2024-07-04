<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainTab.master" Inherits="ProjectPlanningBizReq" Codebehind="ProjectPlanningBizReq.aspx.cs" %>

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
   <%= Resources.DeffinityRes.ProjectPlanBusinessRequirments%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink runat="Server" Text="Back to Project Proposal" NavigateUrl="~/WF/ProjectPlan/ProjectPipeline.aspx?Status=8"><i class="fa fa-arrow-left"></i> Return to Project Proposal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
             <div class="col-md-12"><asp:Label ID="lblError" runat="server"  ForeColor="Red" Font-Size="Small"></asp:Label>
	            <asp:Label ID="lblerror1" runat="server" ForeColor="red"></asp:Label>
</div>
</div>

    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.GeneralBusinessRequirements%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtBizReq" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ReportingRequirements%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtRptReq" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.OperatingRegulationsRequirements%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtOprReq" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.DocumentationandTrainingRequirements%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtDocReq" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.BusinessContinuityPlanning%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtBizPlan" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.TestingRequirementsCertification%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtTestReq" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8"><asp:Button  ID=btnSave runat=server SkinID="btnSubmit" OnClick="btnSave_Click" ValidationGroup="Group1" /> 
					</div>
				</div>
                </div>


          <asp:HiddenField ID="HiddenField2" runat="server" /><asp:HiddenField ID="HiddenField3" runat="server" />


</asp:Content>