<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Grid_Set_Up_New_Poll.aspx.cs" EnableEventValidation="false" Inherits="DeffinityAppDev.App.PollAndSurvey.Polls.Grid_Set_Up_New_Poll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="page_title">
    Polls
    </asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="panel_title">
    Polls
    </asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="panel_options">
    <asp:Button ID="btnSetup" runat="server" SkinID="btnDefault" Text="Setup New Poll" OnClick="btnNewPoll_Click" />
    </asp:Content>

<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">

    <%--<style >
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }

 .BigCheckBox input {width:30px; height:30px;}




    select {
            width: 390px;
            height: 45px;
            background-color: #F5F8FA;
            /*border-collapse:collapse;*/
            border-radius: 5px;
            border-color: #F5F8FA;
        }



    </style>--%>

    <asp:GridView ID="GridPolls" runat="server" Width="100%"  OnRowCommand="GridPolls_RowCommand" >
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="9px">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="linkpollEdit" runat="server" NavigateUrl='<%# "Set_up_a_Poll.aspx?UNID=" + Eval("UNID") %>' CssClass="btn btn-light" Text="Edit"> </asp:HyperLink>
                                      <%-- <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandArgument='<%# Bind("UNID") %>' CommandName="edit1"></asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="9px">
                                    <ItemTemplate>

                                        <asp:Label ID="lblPortfolioID" runat="server" Width="40px" Text='<%# Bind("QuestionID") %>' Visible="false"></asp:Label>
                                        <asp:CheckBox ID="chk" runat="server" OnClick="javascript:SelectSingleCheckbox(this.id)" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="50px">
                                    <ItemTemplate>
                                       <%-- <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("ID").ToString()) %>' Width="50px" Height="50px" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <%--   <asp:TemplateField HeaderText="Event ID" SortExpression="EventID">
                                    <HeaderStyle />
                                    <ItemStyle Width="150px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrganization" runat="server" Text='<%# Bind("EventID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Poll" SortExpression="Question">
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
                              <%--  <asp:TemplateField HeaderText="Question for Poll Or Survey" SortExpression="QuestionforPollOrSurvey">
                                    <HeaderStyle />
                                    <ItemStyle Width="200px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("QuestionforPollOrSurvey") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
                                         <asp:HyperLink ID="linkpollreport" runat="server" NavigateUrl='<%# "Poll_Result.aspx?UNID=" + Eval("UNID") %>' Text="View Report" CssClass="btn btn-primary"></asp:HyperLink>
                                      <%--  <asp:Button ID="btnViewReport" runat="server" CommandArgument='<%# Eval("UNID") %>' CommandName="ViewReport" SkinID="btnDefault" Text="View Report" CausesValidation="false" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField>
                                         <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="linkpoll" runat="server" Target="_blank" NavigateUrl='<%# "Poll_Display.aspx?UNID=" + Eval("UNID") %>' Text="View Poll" CssClass="btn btn-primary"></asp:HyperLink>
                                       <%-- <asp:Button ID="btnViewPoll" runat="server" CommandArgument='<%# Eval("UNID") %>' CommandName="ViewPoll" SkinID="btnDefault" Text="View Poll" CausesValidation="false"  />--%>
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

</asp:Content>
