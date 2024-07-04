p<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_CustomerOrder" Codebehind="CustomerOrder.ascx.cs" %>
 <div class="form-group well" >
      <div class="col-md-12 ">

<div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  <asp:Literal ID="Literal1" runat="server" Text="Client Details"></asp:Literal></strong>
            <hr class="no-top-margin" />
            </div>
    </div>

                                 <div class="form-group row">
                                  <div class="col-md-12">
                                      <div class="col-sm-2"> <asp:Literal ID="lblRequesterName" runat="server" Text="Name"></asp:Literal></div>
                                      <div class="col-sm-10"><asp:Label ID="lblName" runat="server" Font-Bold="true"></asp:Label>  </div>
                                    </div>
 </div>
          <div class="form-group row">
          <div class="col-md-12">
           <div class="col-sm-2"><asp:Literal ID="lblRAddress1" runat="server" Text="Client Address"></asp:Literal></div>
           <div class="col-sm-10" >
                <asp:Label ID="lblRaddress" ClientIDMode="Static" Text="" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
              </div>
<div class="form-group row">
                                <div class="col-md-12">
                                    <div class="col-sm-2"> <asp:Literal ID="lblRequesterMail" runat="server" Text="Email"></asp:Literal></div>
                                    <div class="col-sm-10"><asp:Label ID="lblRequesterEmail" runat="server" Font-Bold="true"></asp:Label>  </div>
                                    </div>
                                
                                 </div>
<div class="form-group row">
      <div class="col-md-12">
                                      <div class="col-sm-2"> <asp:Literal ID="lblRequesterContact" runat="server" Text="Contact"></asp:Literal></div>
                                      <div class="col-sm-10"><asp:Label ID="lblRequesterTelepnoneNo" runat="server" Font-Bold="true"></asp:Label>  </div>
                                    </div>
    </div>

<div class="form-group row">
                                <div class="col-md-12">
                                    <div class="col-sm-2"> <asp:Literal ID="lblFlsDetails" runat="server" Text="Details"></asp:Literal></div>
                                    <div class="col-sm-10"><asp:Label ID="lblDetails" runat="server" Font-Bold="true"></asp:Label> </div>
                                    </div>
                                 </div>
<div class="form-group row" id="pnlStatus" runat="server">
                                <div class="col-md-12">
                                    <div class="col-sm-2"> Approval Status</div>
                                    <div class="col-sm-4"><asp:TextBox ID="txtStatus" runat="server" style="background-color:#FC6868;" ></asp:TextBox> </div>
                                    </div>
                                 </div>

</div>
     </div>