<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainTab.master" Inherits="ProjectPlan" Title="Project Plan" Codebehind="ProjectPlan.aspx.cs" %>

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
   <%= Resources.DeffinityRes.ProjectProposalDetails%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink runat="Server" Text="Back to Project Proposal" NavigateUrl="~/WF/ProjectPlan/ProjectPipeline.aspx?Status=8"><i class="fa fa-arrow-left"></i> Return to Project Proposal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="form-group">
 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
    <asp:Label ID="lblError" runat="server"  ForeColor="Red" Font-Size="Small"></asp:Label>
    <asp:Label ID="lblExistError" runat="server" ForeColor="Red" Font-Size="Small"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProjectTitle"
            Display="None" ErrorMessage="Please enter Project title" ValidationGroup="Group1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDesc"
            Display="None" ErrorMessage="Please enter Description" ValidationGroup="Group1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Please enter valid date in Start date" ControlToValidate="txtStartDate" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
        
    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStartDate"
                            Display="None" ErrorMessage="Please enter Start date" ValidationGroup="Group1"></asp:RequiredFieldValidator>
            
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFinishDate"
                            Display="None" ErrorMessage="Please enter End date" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid date in End date" ControlToValidate="txtFinishDate" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator14" 
        runat="server" ControlToValidate="ddlContry"
            ErrorMessage="Please select Country" InitialValue="0" 
        Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator15" 
        runat="server" ControlToValidate="ddlPortfolio"
            ErrorMessage="Please select Portfolio" InitialValue="0" 
        Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator16" 
        runat="server" ControlToValidate="ddlProgramme"
            ErrorMessage="Please select Programme" InitialValue="0" 
        Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>   
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCity"
        Display="None" ErrorMessage="Please select City" InitialValue="0"
        ValidationGroup="Group1"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSiti"
        Display="None" ErrorMessage="Please select Site" InitialValue="0"
        ValidationGroup="Group1"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlFstatus"
        Display="None" ErrorMessage="Please select Financial status" InitialValue="0"
        ValidationGroup="Group1"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlPlanStatus"
        Display="None" ErrorMessage="Please select Planning phase" InitialValue="0"
        ValidationGroup="Group1"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlContribut"
        Display="None" ErrorMessage="Please select Contribution attribute" InitialValue="0"
        ValidationGroup="Group1"></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlOwner"
        Display="None" ErrorMessage="Please select Owner" InitialValue="0"
        ValidationGroup="Group1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" 
        runat="server" ControlToValidate="ddlRagstatus"
            ErrorMessage="Please select RAG Status" 
        InitialValue="0" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" 
        runat="server" ControlToValidate="ddlPriority"
            ErrorMessage="Please select Priority" InitialValue="0" 
        Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
        
            
        </div>
    
 <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.ProjectTitle%></label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtProjectTitle" runat="server" SkinID="txt_80" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
                                    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Description%></label>
                                      <div class="col-sm-9">
                                          <asp:TextBox ID="txtDesc" runat="server" Height="50px" SkinID="txtMulti" TextMode="MultiLine" Width="425px" MaxLength="500"></asp:TextBox>
					</div>
				</div>
                </div>
                                    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.StrategicObjective%></label>
                                      <div class="col-sm-9">
                                          <asp:TextBox ID="txtStrater" runat="server" Height="50px" TextMode="MultiLine" Width="425px" SkinID="txtMulti"></asp:TextBox>
					</div>
				</div>
                </div>
                                    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Justification%></label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtJustifi" runat="server" SkinID="txtMulti" Height="50px" TextMode="MultiLine" Width="425px" MaxLength="2500"></asp:TextBox>
					</div>
				</div>
                </div>
                                    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Benefit%></label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtBnf" runat="server" SkinID="txtMulti" Height="50px" TextMode="MultiLine" Width="425px" MaxLength="2500"></asp:TextBox>
					</div>
				</div>
                </div>
                                    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Risktothebusiness%></label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtRisk" runat="server" SkinID="txtMulti" Height="50px" TextMode="MultiLine" Width="425px" MaxLength="2500"></asp:TextBox>
					</div>
				</div>
                </div>
                                    </div>
                                  <div class="col-md-4">
                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label><b>Rating</b><span style="color:Red">*</span></label>
                                      </div>
                                          </div>
                                      <div class="form-group">
                                  <div class="col-md-12">
                                      <label>Contribution Attribute</label>
                                      <br />
                                      <asp:DropDownList runat="server" ID="ddlContribut"></asp:DropDownList>
                                      </div>
                                          </div>
                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.RiskScore%></label>
                                      <div class="col-sm-6"><asp:RadioButtonList ID="rdbtnlstRiskScore" runat="server" RepeatDirection="Horizontal">
      
    </asp:RadioButtonList>
					</div>
				</div>
</div>
                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.FinancialImpact%></label>
                                      <div class="col-sm-6"><asp:RadioButtonList ID="rdbtnlstFinancialImpact" runat="server" RepeatDirection="Horizontal">
   
    </asp:RadioButtonList>
					</div>
				</div>
</div>
                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.BusinesImpact%></label>
                                      <div class="col-sm-6"><asp:RadioButtonList ID="rdbtnlstBusinessImpact" runat="server" RepeatDirection="Horizontal">
        
    </asp:RadioButtonList>
					</div>
				</div>
</div>
                                       <div class="form-group">
                                  <div class="col-md-12 text-bold">
                                      Rate it, as '1' is lowest and '5' is highest
                                      </div>
                                          </div>
                                       <div class="form-group">
                                  <div class="col-md-12">

                                      </div>
                                          </div>
                                    </div>
                                 </div>


     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Owner%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlOwner" runat="server" SkinID="ddl_80">
        
        </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%></label>
                                      <div class="col-sm-8 form-inline"> <asp:DropDownList ID="ddlPortfolio" runat="server" SkinID="ddl_80" AutoPostBack="true" 
                                                        onselectedindexchanged="ddlPortfolio_SelectedIndexChanged">
        </asp:DropDownList><asp:LinkButton ID="btnPortfolio" runat="server" SkinID="BtnLinkAdd"  OnClick="btnPortfolio_Click" />
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ExpectedStartdate%></label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtStartDate" runat="server" SkinID="Date"></asp:TextBox>
        <asp:Label ID="Image1" runat="server" SkinID="Calender"  />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Programme%></label>
                                      <div class="col-sm-8 form-inline"><asp:DropDownList ID="ddlProgramme" runat="server" SkinID="ddl_80" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlProgramme_SelectedIndexChanged">
        </asp:DropDownList>
    
    <asp:LinkButton ID="btnProgramme" runat="server" SkinID="BtnLinkAdd"  OnClick="btnProgramme_Click" />
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ExpectedFinishDate%></label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtFinishDate" runat="server" SkinID="Date"></asp:TextBox>
         <asp:Label ID="Image2" runat="server" SkinID="Calender"  />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.SubProgramme%></label>
                                      <div class="col-sm-8 form-inline"> <asp:DropDownList ID="ddlSubProgramme" runat="server" SkinID="ddl_80">
        </asp:DropDownList>
    
    <asp:LinkButton ID="btnSubprogramme" runat="server" SkinID="BtnLinkAdd"  OnClick="btnSubprogramme_Click" />
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Projectvalue%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtBudget" runat="server" SkinID="Price_125px"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Country%></label>
                                      <div class="col-sm-8 form-inline"><asp:DropDownList ID="ddlContry" runat="server" SkinID="ddl_80" AutoPostBack="True" OnSelectedIndexChanged="ddlContry_SelectedIndexChanged"></asp:DropDownList>
    
    <asp:LinkButton ID="btnCountry" runat="server" SkinID="BtnLinkAdd"  OnClick="btnCountry_Click" />
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Costcenter%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtCost" runat="server" SkinID="txt_80"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.City%></label>
                                      <div class="col-sm-8 form-inline"><asp:DropDownList ID="ddlCity" runat="server"  SkinID="ddl_80" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
       <asp:LinkButton ID="btnCity" runat="server" SkinID="BtnLinkAdd"  OnClick="btnCity_Click" />
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Priority%></label>
                                      <div class="col-sm-8">  <asp:DropDownList ID="ddlPriority" runat="server" Width="200px">
            <asp:ListItem Text="Please select..." Value="Please select..."></asp:ListItem>
             <asp:ListItem Text="High" Value="High"></asp:ListItem>
            <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
            <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
            </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Site%></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:DropDownList ID="ddlSiti" runat="server" SkinID="ddl_80"></asp:DropDownList>
        <asp:LinkButton ID="btnSite" runat="server" SkinID="BtnLinkAdd"  OnClick="btnSite_Click" />
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RAGStatus%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlRagstatus" runat="server" SkinID="ddl_80">
    <asp:ListItem Text="Please select..." Value="Please select..."></asp:ListItem>
    <asp:ListItem Text="GREEN" Value="GREEN"></asp:ListItem>
            <asp:ListItem Text="RED" Value="RED"></asp:ListItem>
            <asp:ListItem Text="AMBER" Value="AMBER"></asp:ListItem>
           
    </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Department%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtDepartment" runat="server" SkinID="txt_80" MaxLength="50"></asp:TextBox>
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.FinancialStatus%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlFstatus" runat="server" SkinID="ddl_80"></asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Building%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtBuild" runat="server" SkinID="txt_80" MaxLength="50"></asp:TextBox>
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.StatusPlanningPhase%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlPlanStatus" runat="server" SkinID="ddl_80"></asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ProjectTitle%></label>
                                      <div class="col-sm-8">
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8"> 
                                           <asp:Button ID="btnsubmit" runat="server" 
            SkinID="btnSubmit" OnClick="btnsubmit_Click" 
            ValidationGroup="Group1" /> 
                                          <asp:HiddenField ID="HiddenField2" runat="server" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4"   runat="server" PopupButtonID="Image1" TargetControlID="txtStartDate" CssClass="MyCalendar"></ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1"   runat="server" PopupButtonID="Image2" TargetControlID="txtFinishDate" CssClass="MyCalendar"></ajaxToolkit:CalendarExtender>
                                          </div>
                                      </div>
        </div>

</asp:Content>
