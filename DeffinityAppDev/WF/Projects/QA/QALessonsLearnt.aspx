<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="QALessonsLearnt" Title="Untitled Page" Codebehind="QALessonsLearnt.aspx.cs" %>

<%@ Register Src="controls/QAtabs.ascx" TagName="QATab" TagPrefix="uc1" %>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.QA%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.LessonsLearnt%> -  <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_description" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:QATab ID="QATab1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    
<div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIdentified"
                        Display="None" ErrorMessage="Please select valid data in Identified by" ValidationGroup="ValGroup"
                        InitialValue="0"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAssignedTo"
                        Display="None" ErrorMessage="Please select valid data in Assign to" ValidationGroup="ValGroup"
                        InitialValue="0"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStatus"
                        Display="None" ErrorMessage="Please select valid data in Status" ValidationGroup="ValGroup"
                        InitialValue="0"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLessons"
                        Display="None" ErrorMessage="Please enter description" ValidationGroup="ValGroup"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ValGroup" />
	</div>
</div>
    
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Identifiedby%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlIdentified" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Assignto%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlAssignedTo" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>

            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
            </div>
	</div>
</div>
    
<div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Description%></label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtLessons" Width="750px" Height="70px" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
	</div>
</div>
    
<div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RemediationActs%></label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtRemedationActions" runat="server" Width="750px" Height="70px"
                                    TextMode="MultiLine"></asp:TextBox>
            </div>
	</div>
</div>
    
<div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Impacttothebusiness%></label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtBusinessImapact" runat="server" Width="750px" Height="70px" TextMode="MultiLine"></asp:TextBox>
            </div>
	</div>
</div>
    
<div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-8 form-inline">
               <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click"
                                    ValidationGroup="ValGroup" />&nbsp;<asp:Button ID="btnCancel" runat="server"
                                        SkinID="btnCancel" OnClick="btnCancel_Click" CausesValidation="False" />
            </div>
	</div>
</div>
    

    <asp:UpdatePanel ID="UpdatePanelLessonLearnt" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                        <asp:GridView ID="GVLessonLearnt" runat="server" Width="100%" OnRowCancelingEdit="GVLessonLearnt_RowCancelingEdit"
                                            OnRowCommand="GVLessonLearnt_RowCommand" OnRowDataBound="GVLessonLearnt_RowDataBound"
                                            OnRowEditing="GVLessonLearnt_RowEditing" OnRowUpdating="GVLessonLearnt_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderStyle Width="60px"  />
                                                    <ItemStyle Width="60px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ToolTip="Update">
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<% #Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Identifiedby%>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdentifiedBy" runat="server" Text='<% #Bind("IdentifiedByName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlEIdentifiedBy" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assigned To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssignedTo" runat="server" Text='<% #Bind("AssignedToName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlEAssignedTo" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Status%>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<% #Bind("StatusName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlEStatus" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Description%>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" Text='<% #Bind("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEDesciption" runat="server" Text='<% #Bind("Description") %>' TextMode="MultiLine" Height="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,RemediationActs%>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemediationActions" runat="server" Text='<% #Bind("RemediationActions") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtERemediationActions" runat="server" Text='<% #Bind("RemediationActions") %>' TextMode="MultiLine" Height="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Impacttothebusiness%>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBusinessImpact" runat="server" Text='<% #Bind("BusinessImpact") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEBusinessImpact" runat="server" Text='<% #Bind("BusinessImpact") %>' TextMode="MultiLine" Height="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton SkinID="ImgSymDel" runat="server" ID="grid_delete" CommandArgument='<%# Bind("ID")%>'
                                                            CommandName="delete1" OnClientClick="return confirm('Do you want to delete the record?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
            $("button:contains('Display all')").click(function (e) {
                e.preventDefault();
                $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            });
        });
    </script>

</asp:Content>
