<%@ Page Language="C#" AutoEventWireup="true" Inherits="ChangeControl"
    Title="Incident Change Control" MasterPageFile="~/WF/MainTab.master" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Codebehind="ChangeControl.aspx.cs" %>
<%@ Register Src="controls/ChangeControlTab.ascx" TagName="Tab" TagPrefix="Deffinity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ChangeControl%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    <label id="lblPageTitle" runat="server">
                        </label>
</asp:Content>
<asp:Content ContentPlaceHolderID="Tabs" ID="tabControl" runat="server">
    <Deffinity:Tab ID="tabMenu" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:LinkButton ID="btnCancel" SkinID="BtnLinkBackText" Text="Back" runat="server" AlternateText="Back"
                            OnClick="btnCancel_Click" > </asp:LinkButton>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" ID="mainContent" runat="server">
    <div class="form-group">
          <div class="col-md-12">
              <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Red"
                            Width="100%" />

	</div>
</div>
     <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="img_mcat_cancel"
                        BackgroundCssClass="modalBackground" TargetControlID="btn_popup2" PopupControlID="pnlMcategory" />
                    <ajaxToolkit:ModalPopupExtender ID="modleSubcategory" runat="server" CancelControlID="imgSubCancel"
                        BackgroundCssClass="modalBackground" TargetControlID="imgsubcat" PopupControlID="pnlSubcategory" />
    
                <asp:Panel ID="pnlMcategory" runat="server" BackColor="White" Style="display: none"
                    Width="350px" BorderStyle="Double" BorderColor="LightSteelBlue">
                    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Category</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtmastercategory" runat="server" SkinID="txt_90"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtmastercategory"
                                    Text="Please enter Category" ErrorMessage="Please enter Category" ForeColor="Red"
                                    ValidationGroup="Group11"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
                    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               <asp:Button ID="ImageButton1" runat="server" Text="OK" OnClick="lnkOk_Click1"
                                    SkinID="btnSubmit" ValidationGroup="Group11" />
                                <asp:Button ID="img_mcat_cancel" runat="server" Text="Close" SkinID="btnCancel" />
            </div>
	</div>
</div>

                </asp:Panel>
                <asp:Panel ID="pnlSubcategory" runat="server" BackColor="White" Style="display: none"
                    Width="350px" BorderStyle="Double" BorderColor="LightSteelBlue">
                    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><asp:Label ID="lblSubCategory1" Text="Sub Category" runat="server"></asp:Label></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtSubCategory" runat="server" SkinID="txt_90"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubCategory"
                                    Text="Please enter sub Category" ErrorMessage="Please enter sub Category" ForeColor="Red"
                                    ValidationGroup="gp_sub"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
                    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               <asp:Button ID="imgAddSubCategory" runat="server" Text="OK" OnClick="imgAddSubCategory_Click"
                                    SkinID="btnSubmit" ValidationGroup="gp_sub" />
                                <asp:Button ID="imgSubCancel" runat="server" Text="Close" SkinID="btnCancel" />
            </div>
	</div>
</div>

                </asp:Panel>
    <asp:Panel ID="mpnl_area" runat="server" BackColor="White"
                                    Style="display: none" Width="350px" BorderStyle="Double" BorderColor="LightSteelBlue">
        <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><asp:Label ID="lblArea1" Text="Area" runat="server"></asp:Label></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtArea" runat="server" ValidationGroup="Group_area" SkinID="txt_90"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtArea"
                                                    Text="Please enter Area" ErrorMessage="Please enter Area" ForeColor="Red"
                                                    ValidationGroup="Group_area"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
        <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
                <asp:Button ID="btn_insert_area" runat="server" Text="OK" OnClick="btn_insert_area_Click" SkinID="btnSubmit"
                                                    ValidationGroup="Group_area" />

                                                <asp:Button ID="img_area_close" runat="server" Text="Close" SkinID="btnCancel" />
            </div>
	</div>
</div>
                                </asp:Panel>
                <asp:Panel ID="pnlChangeControl" runat="server">
                     <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlCustomerID" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCustomerID_SelectedIndexChanged" SkinID="ddl_90">
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><asp:Label ID="lblArea" Text="Area" runat="server"></asp:Label></label>
           <div class="col-sm-9 form-inline">
               <asp:DropDownList ID="ddlarea" runat="server" SkinID="ddl_90"></asp:DropDownList>
                                <asp:LinkButton ID="btn_popup_area" runat="server" SkinID="BtnLinkAdd"
                                    EnableViewState="false" AlternateText="Add master category"  />
                                <asp:RequiredFieldValidator ID="RFV_area" runat="server" ControlToValidate="ddlarea"
                                    Display="Dynamic" ErrorMessage="Please select area" InitialValue="0" Text="*"
                                    ValidationGroup="Page" /><ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" CancelControlID="img_area_close"
                                        BackgroundCssClass="modalBackground" TargetControlID="btn_popup_area" PopupControlID="mpnl_area" />
            </div>
	</div>
</div>
                     <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlstatus" runat="server" SkinID="ddl_70">
                                    <asp:ListItem Value="New">New</asp:ListItem>
                                    <asp:ListItem Value="In Hand">In Hand</asp:ListItem>
                                    <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><asp:Label ID="lblCategory" Text="Category" runat="server"></asp:Label></label>
           <div class="col-sm-9 form-inline">
                <asp:DropDownList ID="ddlCategory1" runat="server" ValidationGroup="Submit" DataSourceID="objCategory"
                                    DataTextField="CategoryName" DataValueField="ID" SkinID="ddl_90">
                                </asp:DropDownList>
                                <asp:LinkButton ID="btn_popup2" runat="server" SkinID="BtnLinkAdd" AlternateText="Add category"
                                     />
                                <asp:ObjectDataSource ID="objCategory" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetData" TypeName="DeffinityManager.DAL.DBChangeControlTableAdapters.dtCategoryTableAdapter">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlCustomerID" DefaultValue="0" Name="ID" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
            </div>
	</div>
</div>
                     <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><asp:Label ID="lblSite" Text="Site" runat="server"></asp:Label></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="ddlsite_CascadingDropDown" runat="server" TargetControlID="ddlSite"
                                    Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                    ServiceMethod="GetOurSite" LoadingText="[Loading site...]" />
                                <%-- <ajaxToolkit:CascadingDropDown ID="ddlsite_CascadingDropDown" 
                                runat="server" Category="Task1" ParentControlID="ddlCustomerID" 
                                PromptText="Please select..." PromptValue="0" ServiceMethod="GetSitesByPortfolio" 
                                ServicePath="~/ServiceMgr.asmx" TargetControlID="ddlSite" />--%>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><asp:Label ID="lblSubcategory" Text="Sub category" runat="server"></asp:Label></label>
           <div class="col-sm-9 form-inline">
                <asp:DropDownList ID="ddlSubcategory" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
                                <asp:LinkButton ID="imgsubcat" runat="server" SkinID="BtnLinkAdd" AlternateText="Add Sub category"
                                     />
                                <ajaxToolkit:CascadingDropDown ID="casCadSubCategory" runat="server" TargetControlID="ddlSubcategory"
                                    Category="Task1" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                    ServiceMethod="GetProjectSubCategory" ParentControlID="ddlCategory1" />
            </div>
	</div>
</div>
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Requester%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlRequester" runat="server" DataSourceID="ObjectDataSource1"
                                    DataTextField="ContractorName" DataValueField="ID" SkinID="ddl_90"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlRequester_SelectedIndexChanged">
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6" style="display:none;visibility:hidden;">
           <label class="col-sm-3 control-label"><asp:Label ID="lblPriority" Text="Priority" runat="server"></asp:Label></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlPriority" runat="server" DataSourceID="DS_Priority"
                                    DataTextField="Prioritylevel" DataValueField="ID" SkinID="ddl_90">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqdDDLPriority" runat="server"
                                    ControlToValidate="ddlPriority" Display="Dynamic"
                                    ErrorMessage="Please select priority" InitialValue="0" Text="*"
                                    ValidationGroup="Incident" Visible="false" />
                                <asp:SqlDataSource ID="DS_Priority" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                    SelectCommand="DN_GetIncidentpriority" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
            </div>
	</div>
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Requesters Email</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtRequesterEmail" runat="server" SkinID="txt_90"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtRequesterEmail"
                                    ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="ChangeControl" Text="*" Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtRequesterEmail"
                                    ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="Page" Display="None" />
            </div>
	</div>
</div>

 <div class="form-group">
     
	<div class="col-md-12">
           <label class="col-sm-1 control-label"><%= Resources.DeffinityRes.Title%></label>
           <div class="col-sm-9" style="margin-left:45px">
                <asp:TextBox ID="txtChangeControlTitle" runat="server" SkinID="txt_80"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtChangeControlTitle"
                                    ErrorMessage="Please enter Title" ValidationGroup="Page" Display="None"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>

                     <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.DateRaised%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtDateRaised" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="imgDateRaised" runat="server" SkinID="Calender" /><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDateRaised"
                                    ErrorMessage="Invalid Date" ValidationExpression="(0[1-9]|[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|[1-9]|1[012])[-/.](19\d\d|20\d\d|0[0-9])"
                                    ValidationGroup="ChangeControl">*</asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDateRaised"
                                    ErrorMessage="Please enter Date Raised" ValidationGroup="Page" Display="None"></asp:RequiredFieldValidator>
                                <ajaxToolkit:CalendarExtender ID="calDateRaised" runat="server" CssClass="MyCalendar"
                                     PopupButtonID="imgDateRaised" TargetControlID="txtDateRaised">
                                </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Coordinator%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlcoordinator" runat="server"
                                    DataSourceID="ObjectDataSource1" DataTextField="ContractorName"
                                    DataValueField="ID" SkinID="ddl_90">
                                </asp:DropDownList>
               <ajaxToolkit:CalendarExtender ID="calTargetReleaseDate" runat="server" CssClass="MyCalendar"
                                     PopupButtonID="imgTargetRDate" TargetControlID="txtTargetReleaseDate">
                                </ajaxToolkit:CalendarExtender>
                                
            </div>
	</div>
</div>
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Target Start Date</label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtTargetStartDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="imgTargetStartDate" runat="server" SkinID="Calender"  />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTargetStartDate"
                                    ErrorMessage="Invalid Date" ValidationExpression="(0[1-9]|[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|[1-9]|1[012])[-/.](19\d\d|20\d\d|0[0-9])"
                                    ValidationGroup="ChangeControl">*</asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTargetStartDate"
                                    ErrorMessage="Please enter Target Start Date" ValidationGroup="Page" Display="None"></asp:RequiredFieldValidator>
                                <ajaxToolkit:CalendarExtender ID="calTargetStartDate" runat="server" CssClass="MyCalendar"
                                     PopupButtonID="imgTargetStartDate" TargetControlID="txtTargetStartDate">
                                </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Target Release Date</label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtTargetReleaseDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="imgTargetRDate" runat="server" 
                                    SkinID="Calender" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ControlToValidate="txtTargetReleaseDate" ErrorMessage="Invalid Date"
                                    ValidationExpression="(0[1-9]|[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|[1-9]|1[012])[-/.](19\d\d|20\d\d|0[0-9])"
                                    ValidationGroup="ChangeControl">*</asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                    ControlToValidate="txtTargetReleaseDate" Display="None"
                                    ErrorMessage="Please enter Target Release Date" ValidationGroup="Page"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>

 <div class="form-group" style="display:none;visibility:hidden">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Relates To Project Ref</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlProjectRef" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
                                <%--<asp:ObjectDataSource ID="objProjectRef" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetData" TypeName="DBChangeControlTableAdapters.adaProjects">
                                    <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlCustomerID" Name="portfolio" Type="Int32" DefaultValue="0" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>--%>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Relates To service request</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlRelatesToServiceRequest" runat="server"
                                    SkinID="ddl_90">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rFRelatesToServiceRequest" runat="server" ControlToValidate="ddlRelatesToServiceRequest"
                                    ErrorMessage="*" ValidationGroup="ChangeControl" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                                <asp:ObjectDataSource ID="objServiceRequest" runat="server" OldValuesParameterFormatString="{0}"
                                    SelectMethod="GetData" TypeName="DeffinityManager.DAL.DBChangeControlTableAdapters.dtIncidentServiceTableAdapter">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlCustomerID" DefaultValue="0" Name="portfolio" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
            </div>
	</div>
</div>
                     <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.EstimatedCost%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtestimatedCost" runat="server" SkinID="txt_90"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server"
                                    ControlToValidate="txtestimatedCost" Display="None"
                                    ErrorMessage="Please enter valid data in Estimated Cost"
                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="ChangeControl"></asp:CompareValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RaisedBy%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlRaisedBy" runat="server"
                                    DataSourceID="ObjectDataSource1" DataTextField="ContractorName"
                                    DataValueField="ID" SkinID="ddl_90">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
                                    OldValuesParameterFormatString="{0}" SelectMethod="GetData"
                                    TypeName="DeffinityManager.DAL.DBChangeControlTableAdapters.dtRaisedByTableAdapter"></asp:ObjectDataSource>
            </div>
	</div>
</div>
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.EstimatedDaysReq%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtEstimatedDays" runat="server" SkinID="txt_150px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2" runat="server"
                                    ControlToValidate="txtEstimatedDays" Display="None"
                                    ErrorMessage="Please enter valid data in Estimated Days"
                                    Operator="DataTypeCheck" Type="Integer" ValidationGroup="ChangeControl"></asp:CompareValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>

 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.DateLogged%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtDatelogged" runat="server" SkinID="Date" ReadOnly="True"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.TimeLogged%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtTimelogged" runat="server" SkinID="Time" ReadOnly="True"></asp:TextBox>
            </div>
	</div>
</div>
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">In Hand Date</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtDateInhand" runat="server" SkinID="Date" ReadOnly="True"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">In Hand Time</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtTimeInhand" runat="server" SkinID="Time" ReadOnly="True"></asp:TextBox>
            </div>
	</div>
</div>
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Closed Date</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtDateclosed" runat="server" SkinID="Date" ReadOnly="True"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Closed Time</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtTimeclosed" runat="server" SkinID="Time" ReadOnly="True"></asp:TextBox>
            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-12">
           <label class="col-sm-1 control-label">Description of Change</label>
           <div class="col-sm-9" style="margin-left:45px">
                <asp:TextBox ID="txtDescriptionOfChange" runat="server" TextMode="MultiLine" SkinID="txtMulti_150" Rows="6">
                                </asp:TextBox>
            </div>
	</div>
	
</div>
                    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-1 control-label">Justification/Reason for Change</label>
           <div class="col-sm-9" style="margin-left:45px">
               <asp:TextBox ID="txtJustification" runat="server" TextMode="MultiLine" SkinID="txtMulti_150"
                                    Rows="6">
                                </asp:TextBox>
            </div>
	</div>
</div>
                    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-1 control-label">Resource Impact</label>
           <div class="col-sm-9" style="margin-left:45px">
               <asp:TextBox ID="txtResourceImpact" runat="server" SkinID="txtMulti_150" TextMode="MultiLine"
                                    Rows="6"></asp:TextBox>
            </div>
	</div>
</div>
                    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-1 control-label"></label>
           <div class="col-sm-9" style="margin-left:45px">
               <asp:Button ID="btnAddChangeControl" SkinID="btnAdd" runat="server" OnClick="btnAddChangeControl_Click"
                            ValidationGroup="Page" />&nbsp;
                        <asp:Button ID="btnUpdateChangeControl" runat="server" SkinID="btnUpdate" Visible="false"
                            OnClick="btnSubmitChangeControl_Click" />
            </div>
	</div>
</div>
                </asp:Panel>
</asp:Content>
