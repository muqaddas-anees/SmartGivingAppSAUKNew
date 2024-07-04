<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_CartSummary" Codebehind="CartSummary.ascx.cs" %>
<div class="shop_cart well">
    <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.CartItems%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.TotalItems%></label>
                                      <div class="col-sm-6 pull-right"><div class="pull-right"> <asp:Label ID="lblQty"  runat="server" style="text-align:right" ></asp:Label></div>
					</div>
				</div>
</div>
    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.SubsTotal%></label>
                                      <div class="col-sm-6 pull-right"><div class="pull-right"><asp:Label ID="lblTotal" runat="server" style="text-align:right"></asp:Label></div> 
					</div>
				</div>
</div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                      <div class="col-sm-12">
                                      <asp:HyperLink ID="lnkViewCart" NavigateUrl="~/WF/Portal/DCNewOrder.aspx" SkinID="ButtonOrange" Text="View Cart" runat="Server" ></asp:HyperLink>
                                          </div>
                                      </div>
         </div>
      
    </div>   
