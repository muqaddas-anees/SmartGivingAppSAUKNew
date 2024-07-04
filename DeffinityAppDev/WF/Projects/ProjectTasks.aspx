<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true"
     CodeBehind="ProjectTasks.aspx.cs" Inherits="DeffinityAppDev.WF.Projects.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-group">
        <div class="col-md-12">
           <strong><b>Within on time tasks</b></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="form-group">
    <asp:GridView ID="GridP_TaskList_WithInonTime" runat="server" AutoGenerateColumns="False"
                                                                           Width="100%" GridLines="None" ShowFooter="false"
                                                                            EmptyDataText="No Records Exist">
                            <Columns>
                               <asp:TemplateField HeaderText="Assigned Projects">
                                   <ItemTemplate>
                                      <b> <asp:LinkButton ID="linkbtnProjectRef" Text='<%#Bind("AssignedProjects") %>' ForeColor="#006600" Visible="false"
                                                                 CommandArgument='<%#Bind("ProjectReference") %>' CommandName="Url" runat="server"></asp:LinkButton>
                                          <asp:Label ID="l1" runat="server" Text='<%#Bind("AssignedProjects") %>' ForeColor="#006600"></asp:Label>
                                      </b>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Project Title">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname7" runat="server" Text='<%#Bind("ProjectTitle") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname8" runat="server" Text='<%#Bind("Customer") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Site" Visible="false">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname9" runat="server" Text='<%#Bind("Site") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Task Name">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname" runat="server" Text='<%#Bind("TaskName") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
         </div>
    <div class="form-group">
        <div class="col-md-12">
           <strong><b>Delayed tasks</b> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="form-group">
    <asp:GridView ID="GridP_TaskList_DelayedTasks" runat="server" AutoGenerateColumns="False"
                                                                           Width="100%" GridLines="None" ShowFooter="false"
                                                                            EmptyDataText="No Records Exist">
                            <Columns>
                               <asp:TemplateField HeaderText="Assigned Projects">
                                   <ItemTemplate>
                                      <b> <asp:LinkButton ID="linkbtnProjectRef" Text='<%#Bind("AssignedProjects") %>' ForeColor="#006600" Visible="false"
                                                                 CommandArgument='<%#Bind("ProjectReference") %>' CommandName="Url" runat="server"></asp:LinkButton>
                                          <asp:Label ID="l1" runat="server" Text='<%#Bind("AssignedProjects") %>' ForeColor="#006600"></asp:Label>
                                      </b>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Project Title">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname7" runat="server" Text='<%#Bind("ProjectTitle") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname8" runat="server" Text='<%#Bind("Customer") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Site" Visible="false">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname9" runat="server" Text='<%#Bind("Site") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Task Name">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname" runat="server" Text='<%#Bind("TaskName") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
         </div>

    <div class="form-group">
        <div class="col-md-12">
           <strong><b>Task due within 3 days</b></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="form-group">
    <asp:GridView ID="GridP_TaskList_TaskDueWithIn3Days" runat="server" AutoGenerateColumns="False"
                                                                           Width="100%" GridLines="None" ShowFooter="false"
                                                                            EmptyDataText="No Records Exist">
                            <Columns>
                               <asp:TemplateField HeaderText="Assigned Projects">
                                   <ItemTemplate>
                                      <b> <asp:LinkButton ID="linkbtnProjectRef" Text='<%#Bind("AssignedProjects") %>' ForeColor="#006600" Visible="false"
                                                                 CommandArgument='<%#Bind("ProjectReference") %>' CommandName="Url" runat="server"></asp:LinkButton>
                                          <asp:Label ID="l1" runat="server" Text='<%#Bind("AssignedProjects") %>' ForeColor="#006600"></asp:Label>
                                      </b>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Project Title">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname7" runat="server" Text='<%#Bind("ProjectTitle") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname8" runat="server" Text='<%#Bind("Customer") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Site" Visible="false">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname9" runat="server" Text='<%#Bind("Site") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="TaskName">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname" runat="server" Text='<%#Bind("TaskName") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
         </div>

</asp:Content>
