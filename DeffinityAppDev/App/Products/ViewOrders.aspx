<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="ViewOrders.aspx.cs" Inherits="DeffinityAppDev.App.Products.ViewOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	Orders
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row mb-5">
        <div class="col-lg-5">
            <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Orders This Month</span>

																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    
                                                                    <label id="lbltotal_thismonth" runat="server">0.00</label> 
                                                                   

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>
            

        </div>
        <div class="col-lg-5">
              <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Year To Date Orders</span>
																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    <label id="lbltotal_thisyear" runat="server">0.00</label> 
                                                                    

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>

        </div>


        </div>
   

    <div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> View Orders</h3>
									</div>

				</div>
		 <div class="card-body">
              <div class="row mb-10 ">

										<div class="col-lg-2 ">
                                             <label class="pt-5">Search</label><br />
                                            <asp:TextBox ID="txtSearch" runat="server" placeholder=""></asp:TextBox>

                                           
                                            </div>
				  <div class="col-lg-2 ">
                       <label class="pt-5">Start Date</label><br />
					  <asp:TextBox SkinID="DateNew" ID="txtStart" runat="server"></asp:TextBox>
					  </div>
				    <div class="col-lg-2 ">
                        <label class="pt-5">End Date</label><br />
						 <asp:TextBox SkinID="DateNew" ID="txtEnd" runat="server"></asp:TextBox>
					  </div>
				    <div class="col-lg-2">
                         <label class="pt-5">Category</label><br />
						 <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
					  </div>
                   <div class="col-lg-2">
                         <label class="pt-5">Status</label><br />
						  <asp:DropDownList ID="ddlStatus" runat="server">
                                                           <asp:ListItem Text="Pending Dispatch" Value="Pending Dispatch"></asp:ListItem>
                                                            <asp:ListItem Text="Dispatched" Value="Dispatched"></asp:ListItem>
                                                       </asp:DropDownList>
					  </div>
				    <div class="col-lg-2">
                         <label class="pt-5">&nbsp;</label><br />
						 <asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click" />
                        
					  </div>
                  </div>


			  <div class="row mb-10 d-flex d-inline">

										<div class="col-lg-12">


												<asp:GridView ID="GridProducts" runat="server" Width="60%" AutoGenerateColumns="false" OnRowCommand="GridProducts_RowCommand" OnRowDataBound="GridProducts_RowDataBound">
        <Columns>
             
           
              <asp:TemplateField HeaderText="" Visible="false" >
                                                    <HeaderStyle />
                                                    <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton SkinID="BtnLinkEdit" runat="server" ID="grid_edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>'
                            CommandName="edit1" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Date Sold" >
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDateSold" runat="server" Text='<%# Bind("DateSold","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Time" >
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDateSoldtime" runat="server" Text='<%# Bind("DateSold","{0:hh:mm tt}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("ProductGuid").ToString()) %>' Height="100px" />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Category" >
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
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
            
             <asp:TemplateField HeaderText="QTY" >
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Price" >
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductPrice" runat="server" Text='<%# Bind("ProductPriceDisplay") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Customer Details" >
                                                    
                                                    <ItemTemplate>
                                                    <label style="font-weight:bold;">Name:</label>     <asp:Label ID="lblUnitsSold" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label><br />
                                                        <label style="font-weight:bold;">Address:</label>  <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label><br />
                                                        <label style="font-weight:bold;">Town</label><asp:Label ID="lblTown" runat="server" Text='<%# Bind("Town") %>'></asp:Label><br />
                                                        <label style="font-weight:bold;">State</label>   <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label><br />
                                                        <label style="font-weight:bold;">Zipcode</label> <asp:Label ID="lblZip" runat="server" Text='<%# Bind("Zipcode") %>'></asp:Label><br />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             
               <asp:TemplateField HeaderText="Status" >
                                                    
                                                    <ItemTemplate>
                                                       <asp:DropDownList ID="ddlStatus" runat="server">
                                                           <asp:ListItem Text="Pending Dispatch" Value="Pending Dispatch"></asp:ListItem>
                                                            <asp:ListItem Text="Dispatched" Value="Dispatched"></asp:ListItem>
                                                       </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


             

             <asp:TemplateField HeaderText="" >
                                                    <HeaderStyle />
                                                    <ItemStyle Width="5%" />
                                                    <ItemTemplate>
                                                         <asp:Button SkinID="btnDefault" Text="Save" runat="server" ID="LinkButton1" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>'
                            CommandName="save"  Visible="true" CssClass="btn btn-light" />
                                                        <asp:LinkButton SkinID="BtnLinkDelete" runat="server" ID="grid_delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>'
                            CommandName="del" OnClientClick="return confirm('Do you want to delete the record?');" Visible="false" />
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
