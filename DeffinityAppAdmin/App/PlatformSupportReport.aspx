<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="PlatformSupportReport.aspx.cs" Inherits="DeffinityAppDev.App.PlatformSupportReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Platform Support Report
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">

    <asp:HyperLink ID="hBack" runat="server" NavigateUrl="~/App/Organizations.aspx" Text="Back to Organizations"></asp:HyperLink>

</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

     <style>
        .grid_header_right{
			text-align:right;
		}
    </style>
	<div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" >
            <!--begin::Card title-->
            <div class="card-title m-5">
                <h3 class="fw-bolder m-0">Start date</h3> <asp:TextBox ID="txtStartDate" runat="server" SkinID="DateNew" style="margin-left:5px;margin-right:5px"></asp:TextBox>
				<h3 class="fw-bolder m-0">End date</h3> <asp:TextBox ID="txtEndDate" runat="server" SkinID="DateNew" style="margin-left:5px;margin-right:5px"></asp:TextBox>
                <h3 class="fw-bolder m-0">Search</h3> <asp:TextBox ID="txtSearch" runat="server" SkinID="txt_175px" style="margin-left:5px;margin-right:5px"></asp:TextBox>
				<asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click" style="margin-left:5px;margin-right:5px"></asp:Button>
            </div>
            <div class="card-toolbar gap-3" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">
                  <asp:Button ID="btnReport" runat="server" CssClass="btn btn-primary" Text="Export Report" OnClick="btnReport_Click"     />
                 <asp:Button ID="btnSend" runat="server" CssClass="btn btn-primary" Text="Send To Client" OnClick="btnSend_Click"    />

               
                <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Add New Category" OnClick="btnAddOrganization_Click"       />--%>
            </div>
            <!--end::Card title-->
        </div>
        <!--begin::Card header-->

    </div>


      <div class="row mb-6">
        <asp:GridView ID="GridDashboard" runat="server" Width="100%" >
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField  HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblPaidDate" runat="server" Text='<%# Bind("PaidDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField  HeaderText="Donor Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'> </asp:Label>
                        <%-- <asp:LinkButton ID="btnNavigate" runat="server"  Text='<%# Bind("Name") %>' CommandArgument='<%# Bind("ID") %>' CommandName="member"  Visible="false" />--%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="Donor Email Address">
                    <ItemTemplate>
                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="Logged By">
                    <ItemTemplate>
                        <asp:Label ID="lblPaidBy" runat="server" Text='<%# Bind("PaidBy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="Amount Donated" HeaderStyle-CssClass="grid_header_right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px" >
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Platform Contribution" HeaderStyle-CssClass="grid_header_right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px" >
                    <ItemTemplate>
                        <asp:Label ID="lblPlatformFee" runat="server" Text='<%# Bind("PlatformFee","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Transaction Ref">
                    <ItemTemplate>
                        <asp:Label ID="lblPayRef" runat="server" Text='<%# Bind("PayRef") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				<%-- <asp:TemplateField HeaderText="Payment Type">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

				<%-- <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lbStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

				<%-- <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                       <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-light" CommandArgument='<%# Bind("ID") %>' CommandName="view"  />
                    </ItemTemplate>
                </asp:TemplateField>--%>
				<%-- <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                       <asp:Button ID="btnSendRecipt" runat="server" Text="Send Receipt" CssClass="btn btn-light" CommandArgument='<%# Bind("ID") %>' CommandName="SendReceipt"  />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>

        </div>


</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
