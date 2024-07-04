<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Grid_View_Questions.aspx.cs" Inherits="DeffinityAppDev.App.Events.PollAndSurvey.Survey.Grid_View_Questions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Survey
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Survey : <asp:Label ID="lblFirst" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <asp:Button ID="btnAddQuestion" runat="server" SkinID="btnDefault"  Text="Add a question" OnClick="btnAddQuestion_Click" style="margin-right:10px" /> 

     <asp:Button ID="btnBakTOlist" runat="server" CssClass="btn btn-light"  Text="Back To Survey List" OnClick="btnBakTOlist_Click" /> 
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row mb-6">
                         <asp:GridView ID="GridPolls" runat="server" Width="100%" OnRowCommand="GridPolls_RowCommand" >
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="9px">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="linkpollEdit" runat="server" NavigateUrl='<%# "Set_Up_a_Survey.aspx?EVENTUNID="+Eval("EventUNUID") +"&UNID=" + Eval("UNID") +"&EID=" + Eval("QuestionID") %>' CssClass="btn btn-light" Text="Edit"> </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="9px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPortfolioID" runat="server" Width="40px" Text='<%# Bind("QuestionID") %>' Visible="false"></asp:Label>
                                        <asp:CheckBox ID="chk" runat="server" OnClick="javascript:SelectSingleCheckbox(this.id)" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Survey" SortExpression="Question">
                                    <HeaderStyle />
                                     <ItemStyle Width="25%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestion" runat="server" Text='<%# Bind("Question") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" SortExpression="QuestionDescription">
                                    <HeaderStyle />
                                    <ItemStyle Width="50%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestionDescription" runat="server" Text='<%# Bind("QuestionDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Users Attented" SortExpression="noOfUsersAttented" Visible="false">
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblnoOfUsersAttented" runat="server" Text='<%# Bind("noOfUsersAttented") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                         <asp:HyperLink ID="linkpollreport" runat="server" NavigateUrl='<%# "Survey_Result.aspx?UNID=" + Eval("UNID") %>' Text="View Report" CssClass="btn btn-primary"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField>
                                         <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="linkpoll" runat="server" Target="_blank" NavigateUrl='<%# "Survey_Question_List.aspx?UNID=" + Eval("UNID") +"&QuestionID=" + Eval("QuestionID") %>' Text="View Survey" CssClass="btn btn-primary"></asp:HyperLink>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField>
                                         <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                      <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("UNID") %>' SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete this record?');"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
