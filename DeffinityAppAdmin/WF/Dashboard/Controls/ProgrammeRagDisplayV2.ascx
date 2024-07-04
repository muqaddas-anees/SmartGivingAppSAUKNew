<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProgrammeRagDisplayV2" Codebehind="ProgrammeRagDisplayV2.ascx.cs" %>

    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                TargetControlID="divProgramme" ExpandControlID="btn_show" CollapseControlID="btn_show"
                TextLabelID="btn_show" CollapsedText="<b style='cursor:pointer'>Show Dashboard Configurator</b>" ExpandedText="<b style='cursor:pointer'>Hide Dashboard Configurator</b>"
                 Collapsed="true" SuppressPostBack="true" CollapsedSize="2">
            </ajaxToolkit:CollapsiblePanelExtender>
<div id="btn_show" runat="server" style="float:right;cursor:pointer;padding-top:5px" >Show Dashboard Configurator</div>
<div>


<asp:Panel ID="divProgramme" runat="server" Width="100%">

    
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label">Select Vertical Dimension:</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlVertical" runat="server" Width="200px" 
        onselectedindexchanged="ddlVertical_SelectedIndexChanged">
<asp:ListItem Text="City" Value="City"></asp:ListItem>
<asp:ListItem Text="Class" Value="Class"></asp:ListItem>
<asp:ListItem Text="Country" Value="Country"></asp:ListItem>
<asp:ListItem Text="Owner" Value="Owner" Selected="True"></asp:ListItem>
<asp:ListItem Text="Site" Value="Site"></asp:ListItem>
<asp:ListItem Text="Sub Programme" Value="Sub_Programme" ></asp:ListItem>
    </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label">Select Horizontal Dimension:</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlHorizontal" runat="server" Width="200px" 
                onselectedindexchanged="ddlHorizontal_SelectedIndexChanged">
         <asp:ListItem Text="City" Value="City"></asp:ListItem>
<asp:ListItem Text="Class" Value="Class"></asp:ListItem>
<asp:ListItem Text="Country" Value="Country"></asp:ListItem>
<asp:ListItem Text="Owner" Value="Owner" ></asp:ListItem>
<asp:ListItem Text="Site" Value="Site"></asp:ListItem>
<asp:ListItem Text="Sub Programme" Value="Sub_Programme" Selected="True"></asp:ListItem>
         </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>


<div class="form-group">
      <div class="col-md-12">
          <label style="font-weight:bold">Risk</label><br />
	</div>

</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label">Red</label>
           <div class="col-sm-9"><asp:TextBox ID="txtRedRisk" runat="server" Width="50px"></asp:TextBox>

            </div>
	</div>
	<div class="col-md-8">
           Day(s) &nbsp;&nbsp;&nbsp;<label>One or more high-impact risks in the relavent intersection is overdue for "next review date" value by specified days</label>
	</div>
	
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label">Amber</label>
           <div class="col-sm-9"><asp:TextBox ID="txtAmberRisk" runat="server" Width="50px"></asp:TextBox>

            </div>
	</div>
	<div class="col-md-8">
          Day(s) &nbsp;&nbsp;&nbsp;<label>One or more risks in the relavent intersection is overdue for "next review date" value by specified days</label>
	</div>
	
</div>
    
<div class="form-group">
      <div class="col-md-12">
          <label style="font-weight:bold">Milestones</label><br />
	</div>

</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label">Red</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtRedMilestone" runat="server" Width="50px"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-8">
          Day(s)&nbsp;&nbsp;&nbsp;<label>One of more critical milestones in the relevant intersection is late and is expected to cause delays in orginal planned project delivery</label>
	</div>
	
</div>

     <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label">Amber</label>
           <div class="col-sm-9">
              <asp:TextBox ID="txtAmberMilestone" runat="server" Width="50px"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-8">
        Day(s)&nbsp;&nbsp;&nbsp;<label>One of more critical milestones in the relevant intersection is late, but is not expected to cause delays in orginal planned project delivery</label>
        </div>
         </div>
      <div class="form-group">
      <div class="col-md-4">
          <asp:Button ID="btnApply" runat="server" Text="Show Dashboard" onclick="btnApply_Click" /> 
	</div>
	<div class="col-md-4">
          
	</div>
	<div class="col-md-4">
          
	</div>
</div>
         <div class="clr"></div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label">Benefit Type:</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlBenifitType" runat="server" Width="200px" 
        AutoPostBack="true" 
        onselectedindexchanged="ddlBenifitType_SelectedIndexChanged"></asp:DropDownList><asp:HiddenField ID="hid" runat="server" Value="0" />
            </div>
	</div>
	<div class="col-md-4">
             <asp:DropDownList ID="ddlOwner" runat="server" Width="200px" 
        AutoPostBack="true" onselectedindexchanged="ddlOwner_SelectedIndexChanged" Visible="false" ></asp:DropDownList>
	</div>
	<div class="col-md-4">
          
	</div>
</div>
   
       
<div class="form-group">
      <div class="col-md-12">
          <asp:Label ID="lblMsgDashboard" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
	</div>

</div>
       
    <div class="form-group">
      <div class="col-md-2">
          <asp:DropDownList ID="ddlDashboardconfig" runat="server"></asp:DropDownList>
	</div>
	<div class="col-md-2">
         
 <asp:TextBox ID="txtDashboardConfig" runat="server" Width="210px" Text="" Visible="false"></asp:TextBox>
   
	</div>
	<div class="col-md-5 form-inline">
          
<asp:Button ID="btnSaveconfig" runat="server" onclick="btnSaveconfig_Click" Text="Save Config As" />
<asp:Button ID="btnViewDashboard" runat="server" SkinID="btnView" onclick="btnViewDashboard_Click" />
 <asp:Button ID="btnDashboardConfig" runat="server" Text="OK" SkinID="btnSubmit"
                                        OnClick="btnDashboard_Click" ValidationGroup="Group_DashboardConfig" Visible="false" />
 <asp:Button ID="btnDashboardCancel" runat="server" Text="Close" 
        SkinID="btnCancel" Visible="false" onclick="btnDashboardCancel_Click" />
	</div>
</div>

<div> <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDashboardConfig"
                                        ErrorMessage="Please enter name" ForeColor="Red" ValidationGroup="Group_DashboardConfig"></asp:RequiredFieldValidator></div>
 
</asp:Panel>

</div>
 

<asp:Panel ID="PanelRag" runat="server" ScrollBars="Auto">
<asp:Literal ID="litDisplay" runat="server"></asp:Literal>
</asp:Panel>

