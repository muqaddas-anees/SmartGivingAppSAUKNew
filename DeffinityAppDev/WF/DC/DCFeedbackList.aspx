<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="DCFeedbackList.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCFeedbackList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <label id="lblTitle" runat="server">Feedback</label> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <%--<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>\
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
        
        <li><a id="link_dashboard" href="~/WF/DC/DCFeedbackDashboard.aspx" runat="server" target="_self"><%= Resources.DeffinityRes.Dashboard%></a></li>
       
    </ul>
   </div>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Feedback
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group row">
      <div class="col-md-4">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-7 form-inline">
               <asp:TextBox ID="txtfrom" runat="server" SkinID="Date"></asp:TextBox>
               <asp:Label ID="lblsc" runat="server" SkinID="Calender"></asp:Label>
            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-7 form-inline">
                <asp:TextBox ID="txtTodate" runat="server" SkinID="Date"></asp:TextBox>
               <asp:Label ID="Label10" runat="server" SkinID="Calender"></asp:Label>
            </div>
	</div>
	<div class="col-md-2">
           <asp:Button SkinID="btnView" runat="server" />
	</div>
</div>

     <asp:GridView ID="gridFeedback" runat="server">
        <Columns>
              <asp:TemplateField HeaderText="Job ref">
                <EditItemTemplate>
                    <asp:TextBox ID="txtTicket1" runat="server" Text='<%# Bind("CallRef") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblTicket" runat="server" Text='<%# Bind("CallRef") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Customer">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CustomerName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date and Time" ItemStyle-HorizontalAlign="Right">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CreatedDate") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("CreatedDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="How satisfied are you with the Service Tech Technician?">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("likelyService") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("likelyService") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="How satisfied are you with customer service you spoke with?">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Worksatisifaction") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Worksatisifaction") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="How satisfied are you with the customer portal to submit your claim?">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Rating") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Rating") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Improvement">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Didwell") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Didwell") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="How satisfied are you with your overall claim experience?">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("recommend") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("recommend") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Additional Comments">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("servicefeedback") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("servicefeedback") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
           
           
            <asp:TemplateField HeaderText="Status">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("StatusName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("StatusName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField Visible="false">
                <EditItemTemplate>
                    
                </EditItemTemplate>
                <ItemTemplate>
                   <asp:Button ID="btnc" runat="server" SkinID="btnDefault" Text="Email Customer" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField Visible="false">
                <EditItemTemplate>
                </EditItemTemplate>
                <ItemTemplate>
                   <asp:Button ID="btnc" runat="server" SkinID="btnDefault" Text="Submit to Sites" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
    hidetabs();
 </script> 
</asp:Content>
