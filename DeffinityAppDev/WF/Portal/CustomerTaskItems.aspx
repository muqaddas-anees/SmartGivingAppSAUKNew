<%@ Page Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="CustomerTaskItems" Codebehind="CustomerTaskItems.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<div class="row">
          <div class="col-md-12">
 <strong>Tasks in  <%=Request.QueryString["Event"] %> Status</strong> 
<hr class="no-top-margin" />
	</div>
</div>
    <div class="row">
          <div class="col-md-12">
       
        <asp:GridView ID="GridView1" runat="server" Width="100%">
            <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                <asp:BoundField DataField="ItemDescription" HeaderText="Task">
                <ItemStyle Width="150px" />
                 </asp:BoundField >
                <asp:BoundField DataField="PercentComplete" HeaderText="Percent Complete">
                <ItemStyle Width="75px"  HorizontalAlign="Center" />
                </asp:BoundField >
                <asp:BoundField DataField="ProjectStartDate" HeaderText="Start Date"  HtmlEncode="False" DataFormatString="{0:d}" >
                <ItemStyle Width="100px"  HorizontalAlign="Center" />
                </asp:BoundField >
                <asp:BoundField DataField="ProjectEndDate" HeaderText="End Date"  HtmlEncode="False" DataFormatString="{0:d}" >
                <ItemStyle Width="100px"  HorizontalAlign="Center" />
                </asp:BoundField >
                <asp:BoundField DataField="ItemStatus1" HeaderText="Status" >
                <ItemStyle Width="100px"  HorizontalAlign="Center" />
                </asp:BoundField >
                <asp:TemplateField HeaderText="Resources">
                <ItemTemplate>          
                        
                        <asp:Label ID="lblResources" Width="110px" runat="server" Text='<%# lblResource(DataBinder.Eval(Container.DataItem, "ID").ToString()) %>' ></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="110px" />
                </asp:TemplateField> 
                
            </Columns>
        </asp:GridView>
      
        </div>
        </div>
</asp:Content>
