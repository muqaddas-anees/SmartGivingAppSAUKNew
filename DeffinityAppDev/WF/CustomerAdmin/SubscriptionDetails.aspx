<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="SubscriptionDetails.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.SubscriptionDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Subscription
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

      <div class="card shadow-sm">
						<div class="card-header">
							<h3 class="panel-title form-inline"> 
                            Subscription
                             </h3>
							<div class="card-toolbar">
								
                             


							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
             
                                 <div class="form-group row">
          <div class="col-md-12">
           <div class="col-sm-12 form-inline">
                Plan : <asp:Literal ID="lblplan" runat="server" Text="Standrad"></asp:Literal> <asp:Button SkinID="btnDefault" Text="Upgrade" runat="server" OnClick="Unnamed1_Click" />
               </div>
              </div>
                                     </div>

                                
                                 <div class="form-group row">
          <div class="col-md-4">
              <div class="col-sm-12">
									<label class="control-label" for="name">Plan</label> 
								</div>
								<div class="col-sm-12">
                                    	<asp:Label ID="lblPlanName" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </div>
              </div>
                                      <div class="col-md-4">
                                            <div class="col-sm-12">
									<label class="control-label" for="name">No of Users</label> 
								</div>
								<div class="col-sm-9">
                                    <asp:Label ID="lblNoofUsers" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </div>

              </div>
                                      <div class="col-md-4">
                                             <div class="col-sm-12">
									<label class="control-label" for="name">Term</label> 
								</div>
								<div class="col-sm-9">
                                    <asp:Label ID="lblTerms" runat="server" Font-Size="XX-Large"></asp:Label>
                                    </div>
              </div>
                                     </div>
                                

                                </div>
                </div>
            </div>

    

     <div class="card shadow-sm">
						<div class="card-header">
							<h3 class="panel-title form-inline"> 
                             Invoices
                            </h3>
							<div class="card-toolbar">
								
                             
							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
            
                                </div>
                </div>
            </div>

     <div class="form-group row">
          <div class="col-md-12">
               <p> To downgrade or cancel your account, <a href="CancelSubscription.aspx">contact us</a>.</p>
              </div>

         </div>



</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
