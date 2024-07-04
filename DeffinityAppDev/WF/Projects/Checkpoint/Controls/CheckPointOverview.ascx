<%@ Control Language="C#"  AutoEventWireup="true" Inherits="controls_CheckPointOverview" Codebehind="CheckPointOverview.ascx.cs" %>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">Project Title:</label>
           <div class="col-sm-8">
               <label ID="txtProjectTitle" runat="server"></label> 
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"> Status :</label>
           <div class="col-sm-8">
               <label ID="txtStatus" runat="server"></label>
            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">Owner :</label>
           <div class="col-sm-8">
               <label ID="txtOwner" runat="server"></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label">Country :</label>
           <div class="col-sm-8">
               <label ID="txtCountry" runat="server"></label>
            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">Email :</label>
           <div class="col-sm-8">
               <label ID="txtOwneremail" runat="server"></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label">City :</label>
           <div class="col-sm-8">
               <label ID="txtCity" runat="server"></label>
            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">Customer  :</label>
           <div class="col-sm-8">
               <lable ID="txtPortfolio" runat="server"></lable>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label">Site:</label>
           <div class="col-sm-8">
               <label ID="txtSite" runat="server"></label>
            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">Programme  :</label>
           <div class="col-sm-8">
               <lable ID="txtProgramme" runat="server"></lable>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">
                <asp:Button ID="btnProject" runat="server" Text="Click here to access the project" OnClick="btnProject_Click" />
            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">Sub Programme :</label>
           <div class="col-sm-8">
               <lable ID="txtSubprogramme" runat="server"></lable>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">

            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">Description :</label>
           <div class="col-sm-8">
                <textarea id="txtProjectdesc" runat="server" name="" rows="2" cols="7"  class="txt_field" style="height:60px;width:330px;"></textarea>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">

            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">Cost Centre :</label>
           <div class="col-sm-8">
               <label ID="txtCostCentre" runat="server"  ></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">

            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">Start Date:</label>
           <div class="col-sm-8">
               <label ID="txtStartdate" runat="server" ></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">

            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">End Date :</label>
           <div class="col-sm-8">
               <label ID="txtEndDate" runat="server" ></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">

            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label">Scheduled&nbsp;QA&nbsp;date :</label>
           <div class="col-sm-8">
               <label ID="txtScheduledQAdate" runat="server" ></label>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">

            </div>
	</div>
</div>
