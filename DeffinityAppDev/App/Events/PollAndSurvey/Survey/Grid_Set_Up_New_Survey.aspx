<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Grid_Set_Up_New_Survey.aspx.cs" Inherits="DeffinityAppDev.App.PollAndSurvey.Survey.Grid_Set_Up_New_Survey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Survey
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="server">
    Survey
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_options" runat="server">
    <asp:Button ID="btnNewSurvey" runat="server" SkinID="btnDefault"  Text="Set Up New Survey"   OnClick="btnNewSurvey_Click" style="margin-right:10px;margin-left:10px;" />   
     <asp:Button ID="btnBack" runat="server" CssClass="btn btn-light"  Text="Back to Event interaction"   OnClick="btnBack_Click" />   
</asp:Content>
<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">
       <div class="row mb-6">
                         <asp:GridView ID="GridPolls" runat="server" Width="100%" OnRowCommand="GridPolls_RowCommand" >
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="9px">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="linkpollEdit" runat="server" NavigateUrl='<%# "Grid_View_Questions.aspx?EVENTUNID="+Eval("EventUNUID") +"&UNID=" + Eval("UNID") %>' CssClass="btn btn-light" Text="Edit"> </asp:HyperLink>
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
                                     <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestion" runat="server" Text='<%# Bind("Question") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" SortExpression="QuestionDescription">
                                    <HeaderStyle />
                                    <ItemStyle Width="40%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestionDescription" runat="server" Text='<%# Bind("QuestionDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="No of Questions">
                                   <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblnoofquestions" runat="server" Text='<%# Bind("TotalQustions") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Users Responded" SortExpression="noOfUsersAttented" >
                                      <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblnoOfUsersAttented" runat="server" Text='<%# Bind("noOfUsersAttented") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                         <asp:HyperLink ID="linkpollreport"  Target="_blank" runat="server" NavigateUrl='<%# "Survey_Result.aspx?UNID=" + Eval("UNID") %>' Text="View Report" CssClass="btn btn-primary"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField>
                                         <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="linkpoll" runat="server" Target="_blank" NavigateUrl='<%# "Survey_Question_List.aspx?UNID=" + Eval("UNID") %>' Text="View Survey" CssClass="btn btn-primary"></asp:HyperLink>
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

