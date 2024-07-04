<%@ Control Language="C#"  AutoEventWireup="true" Inherits="controls_CheckPointRecommendation" Codebehind="CheckPointRecommendation.ascx.cs" %>

<%-- <div class="row">
         <div class="col-md-12">
 <strong>Project Report </strong> 
<hr class="no-top-margin" />
	</div>
</div>--%>
<div class="row">
          <div class="col-md-12">
<asp:Label ID="lbl1" runat="server" ForeColor="Red" Font-Size="Small" Visible="False" ></asp:Label>
</div>
    </div>
<div class="form-group">
      <div class="col-md-2">
          Technical
	</div>
	<div class="col-md-7">
           <asp:TextBox ID="txtTechRec" runat="server" Width="700" Height="60" TextMode ="MultiLine" ></asp:TextBox>
	</div>
	<div class="col-md-2">
          
	</div>
</div>
<div class="form-group">
      <div class="col-md-2">
          Financial
	</div>
	<div class="col-md-7">
          <asp:TextBox ID="txtFinancialRec" runat="server" Width="700" Height="60" TextMode ="MultiLine" ></asp:TextBox>
	</div>
	<div class="col-md-2">
          
	</div>
</div>
<div class="form-group">
      <div class="col-md-2">
          Resource
	</div>
	<div class="col-md-7">
          <asp:TextBox ID="txtResourceRec" runat="server" Width="700" Height="60" TextMode ="MultiLine" ></asp:TextBox>
	</div>
	<div class="col-md-2">
          
	</div>


</div>
<div class="form-group">
      <div class="col-md-2">
          Business
	</div>
	<div class="col-md-7">
          <asp:TextBox ID="txtBussinessRec" runat="server" Width="700" Height="60" TextMode ="MultiLine" ></asp:TextBox>
	</div>
	<div class="col-md-2">
          
	</div>


</div>
<div class="form-group">
      <div class="col-md-2">
          Other
	</div>
	<div class="col-md-7">
          <asp:TextBox ID="txtOtherRec" runat="server" Width="700" Height="60" TextMode ="MultiLine" ></asp:TextBox>
	</div>
	<div class="col-md-2">
          
	</div>


</div>
<div class="form-group">
      <div class="col-md-2">
          Project&nbsp;Manager’s&nbsp;Notes 
	</div>
	<div class="col-md-7">
          <asp:TextBox ID="txtPMNotes" runat="server" Width="700" Height="60" TextMode ="MultiLine" ></asp:TextBox>
	</div>
	<div class="col-md-2">
          
	</div>


</div>
<div class="form-group">
      <div class="col-md-2">
         
	</div>
	<div class="col-md-7 form-inline">
        <asp:Button ID="btn_ProceedwithProject" runat="server" SkinID="btnDefault" Text="Proceed with Project" OnClick="btn_ProceedwithProject_Click" />
&nbsp;&nbsp;<asp:Button ID="btn_ProjectonHold" runat="server" SkinID="btnDefault" Text="Place Project on Hold" OnClick="btn_ProjectonHold_Click" />
         <asp:HiddenField ID="HiddenField1" runat="server" />
        </div>
    </div>

<div>

</div>
