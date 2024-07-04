<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="ViewProducts.aspx.cs" Inherits="DeffinityAppDev.App.Products.ViewProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Products
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">


    
    <script>
function copyUrlToClipboard(url) {
    // Create a temporary input element
    var tempInput = document.createElement("input");
    
    // Set its value to the URL
    tempInput.setAttribute("value", url);
    
    // Append the input element to the document body
    document.body.appendChild(tempInput);
    
    // Select the input element's contents
    tempInput.select();
    
    // Copy the selected text to the clipboard
    document.execCommand("copy");
    
    // Remove the temporary input element
    document.body.removeChild(tempInput);

    showswal("URL Copied successfully", "Ok");
    // Prevent postback
    return false;
}
    </script>
    <div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> Products</h3>
									</div>

                 <div class="card-toolbar gap-3" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">
                      <a class="btn btn-video" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" style="display:none" src="https://player.vimeo.com/video/836626081?h=d2d3d21798" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
                     </div>

				</div>
		 <div class="card-body">
              <div class="row mb-10 ">

										<div class="col-lg-6 d-flex d-inline gap-3">
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="txt_70"></asp:TextBox>

                                            <asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click" />
                                            </div>
                  </div>

     <div class="row mb-10 d-flex d-inline">

										<div class="col-lg-12">


												<asp:GridView ID="GridProducts" runat="server" Width="60%" AutoGenerateColumns="false" OnRowCommand="GridProducts_RowCommand" OnRowDataBound="GridProducts_RowDataBound">
        <Columns>
             
           
              <asp:TemplateField HeaderText="" >
                                                    <HeaderStyle />
                                                    <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton SkinID="BtnLinkEdit" runat="server" ID="grid_edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>'
                            CommandName="edit1" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("ProductGuid").ToString()) %>' Height="100px" />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Name" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="txtLable" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Description" >
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("ProductDetails") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Price" >
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("ProductPriceDisplay") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Units Available" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"  ItemStyle-Width="8%">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalUnits" runat="server" Text='<%# Bind("TotalUnits") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Units Sold" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitsSold" runat="server" Text='<%# Bind("UnitsSold") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Status" >
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

              <asp:TemplateField HeaderText="Status" >
                                                    
                                                    <ItemTemplate>
                                                        <asp:Button ID="CopyButton" runat="server" Text="Copy URL" OnClientClick='<%# Eval("Url", "return copyUrlToClipboard(\"{0}\");") %>' />
            
                                                       <%-- <asp:Button ID="btnCopyUrl" runat="server" Text="Copy URL" CommandName="copy"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>' />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

             <asp:TemplateField HeaderText="" >
                                                    <HeaderStyle />
                                                    <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton SkinID="BtnLinkDelete" runat="server" ID="grid_delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>'
                            CommandName="del" OnClientClick="return confirm('Do you want to delete the record?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
               
    
        </Columns>
    </asp:GridView>
											</div>


										</div>

             </div>
        </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
