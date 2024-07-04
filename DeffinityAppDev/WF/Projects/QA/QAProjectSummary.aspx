<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true"   Inherits="QASummary1" Title="QAProject Summary" Codebehind="QAProjectSummary.aspx.cs" %>
<%@ Register Src="controls/QAtabs.ascx" TagName="QATab1" TagPrefix="QA1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.QA%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.ProjectOverview%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<QA1:QATab1 ID ="QATab1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectTitle%> :</label>
           <div class="col-sm-8">
               <label id="txtProjectTitle" runat="server"/> 
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Owner%> :</label>
           <div class="col-sm-8">
               <label id="txtowner" runat="server"></label>
            </div>
	</div>
</div>
        
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Email%> :</label>
           <div class="col-sm-8">
               <label id="txtemail" runat="server"></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%> :</label>
           <div class="col-sm-8">
               <label ID="txtPortfolio" runat="server"></label>
            </div>
	</div>
</div>

    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Programme%> :</label>
           <div class="col-sm-8">
               <label ID="txtProgramme" runat="server"></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.SubProgramme%> :</label>
           <div class="col-sm-8">
               <label ID="txtSubprogramme" runat="server"></label>
            </div>
	</div>
</div>
    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Description%> :</label>
           <div class="col-sm-8">
               <textarea id="txtdescription" runat="server" name="" rows="2" cols="7"  class="txt_field" style="height:60px;width:330px;"></textarea>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.CostCentre%> :</label>
           <div class="col-sm-8">
               <label ID="txtCostCentre" runat="server"  ></label>
            </div>
	</div>
</div>
    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.StartDate%> :</label>
           <div class="col-sm-8">
               <label id="txtstartdate" runat="server"></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.EndDate%> :</label>
           <div class="col-sm-8">
               <label id="txtcompleteddate" runat="server"></label>
            </div>
	</div>
</div>
    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ScheduledQAdate%> :</label>
           <div class="col-sm-8">
               <label ID="txtScheduledQAdate" runat="server" ></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Country%> :</label>
           <div class="col-sm-8">
               <label id="txt_counrty" runat="server"  />
            </div>
	</div>
</div>
    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.City%> :</label>
           <div class="col-sm-8">
               <label id="txt_city" runat="server"  />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Site%> :</label>
           <div class="col-sm-8">
               <label id="txtsite" runat="server"  />
            </div>
	</div>
</div>
    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Projectvalue%> :</label>
           <div class="col-sm-8">
               <label id="txtbudgetlevel0" runat="server" ></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Revisedprojectvalue%> :</label>
           <div class="col-sm-8">
               <label id="txtbudgetlevel3" runat="server" ></label>
            </div>
	</div>
</div>
    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Actualcoststodate%> :</label>
           <div class="col-sm-8">
               <label id="txtactualdate" runat="server" ></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectTitle%></label>
           <div class="col-sm-8">

            </div>
	</div>
</div>

</asp:Content>

