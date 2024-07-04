<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProgrammeKeyMilestones" Codebehind="ProgrammeKeyMilestones.aspx.cs" %>
<%@ Register src="controls/ProgrammeManagement.ascx" tagname="ProgrammeManagement" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc1:ProgrammeManagement ID="ProgrammeManagement1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.KeyMilestones%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <%-- <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> <%= Resources.DeffinityRes.AddKeyMilestones%></strong>
            <hr class="no-top-margin" />
            </div>
</div>--%>
    <div class="form-group">
             <div class="col-md-12">
                 <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtRagName" ErrorMessage="<%$ Resources:DeffinityRes,PlsEnterKeyMilestone%>" 
                ValidationGroup="Group1"></asp:RequiredFieldValidator>
</div>
</div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Programme%></label>
                                      <div class="col-sm-6"> <asp:DropDownList ID="ddlProgramme" runat="server" SkinID="ddl_90" 
                                            AutoPostBack="True" onselectedindexchanged="ddlProgramme_SelectedIndexChanged">
                                        </asp:DropDownList>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.SubProgramme%> </label>
                                      <div class="col-sm-6"><asp:DropDownList ID="ddlSubProgramme" runat="server" SkinID="ddl_90" 
                                            onselectedindexchanged="ddlSubProgramme_SelectedIndexChanged">
                                        </asp:DropDownList>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label">  <%= Resources.DeffinityRes.KeyMilestone%></label>
                                      <div class="col-sm-6"><asp:TextBox ID="txtRagName" runat="server" SkinID="txt_90" MaxLength="200"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label">  <%= Resources.DeffinityRes.Description%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtRAGDescription" runat="server" SkinID="txtMulti" TextMode="MultiLine" Height="50px"></asp:TextBox> 
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> </label>
                                      <div class="col-sm-9"><asp:Button ID="btnApply" runat="server" SkinID="btnSubmit" 
                onclick="btnApply_Click" ValidationGroup="Group1"/>
            <asp:Button ID="btnCancel" runat="server" SkinID="btnCancel" 
                CausesValidation="false" onclick="btnCancel_Click" />
					</div>
				</div>
                </div>
  
    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  <%= Resources.DeffinityRes.LstofKeyMilestones%></strong>
            <hr class="no-top-margin" />
            </div>
</div>
 <asp:GridView ID="GridViewProjectRag" runat="server" DataKeyNames="ID" Width="100%" 
                onrowcancelingedit="GridViewProjectRag_RowCancelingEdit" 
                onrowcommand="GridViewProjectRag_RowCommand" 
                onrowediting="GridViewProjectRag_RowEditing" 
                onrowdeleting="GridViewProjectRag_RowDeleting" 
                onrowupdated="GridViewProjectRag_RowUpdated" 
                onrowupdating="GridViewProjectRag_RowUpdating" 
            onrowdatabound="GridViewProjectRag_RowDataBound">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" SkinID="BtnLinkUpdate"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" SkinID="BtnLinkCancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" SkinID="BtnLinkEdit"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="75px" />
            </asp:TemplateField>
        <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes,KeyMilestone%>">
        <ItemStyle Width="200px" />
        <ItemTemplate>
        <asp:Literal ID="lblRAGSectionName" runat="server" Text='<%#Eval("RAGSectionName") %>'></asp:Literal>
        </ItemTemplate>
        <EditItemTemplate>
        <asp:TextBox ID="txtRAGSectionName" runat="server" Text='<%#Eval("RAGSectionName") %>' Width="200px"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtRAGSectionName" ErrorMessage="<%$ Resources:DeffinityRes,EnterRAGNameArea%>" 
                ValidationGroup="Group2"></asp:RequiredFieldValidator>
        </EditItemTemplate>
        </asp:TemplateField>     
        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Programme%>">
        <ItemTemplate>
            <asp:Literal ID="lblProgramme" runat="server" Text='<%#Eval("ProgrammeName") %>'></asp:Literal>
        </ItemTemplate>
        <EditItemTemplate>
        <asp:DropDownList ID="ddlGridProgramme" runat="server" Width="150px"></asp:DropDownList>
        </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SubProgramme%>">
        <ItemTemplate>
            <asp:Literal ID="lblSubProgramme" runat="server" Text='<%#Eval("SubProgrammeName") %>'></asp:Literal>
        </ItemTemplate>
        <EditItemTemplate>
        <asp:DropDownList ID="ddlGridSubProgramme" runat="server" Width="150px"></asp:DropDownList>
        </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Description%>">
        <ItemStyle Width="300px" />
        <ItemTemplate>
        <asp:Literal ID="lblDescription" runat="server" Text='<%#Eval("RAGDescription") %>'></asp:Literal>
        </ItemTemplate>
        <EditItemTemplate>
        <asp:TextBox ID="txtDescription" runat="server" Text='<%#Eval("RAGDescription") %>' Width="300px"></asp:TextBox>
        </EditItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Delete" SkinID="BtnLinkDelete"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="75px" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
    <%= Resources.DeffinityRes.NoKeyMilestnesavl%>
      
        </EmptyDataTemplate>
        </asp:GridView>
     
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


