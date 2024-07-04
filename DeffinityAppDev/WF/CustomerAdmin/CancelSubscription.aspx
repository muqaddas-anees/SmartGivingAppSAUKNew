<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="CancelSubscription.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.CancelSubscription" %>
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
							<h3 class="card-body"> 
                                Cancel subscription
                            </h3>
							<div class="card-toolbar">
								
                              
							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
            
								<asp:Panel ID="pnlInput" runat="server">
                                	<div class="row">
								<div class="col-md-3">
									<label class="control-label" for="name">Reason for cancellation</label> 
								</div>
								<div class="col-md-9">
                                    <asp:TextBox ID="txtReason" runat="server" SkinID="txtMulti_150" MaxLength="4000" TextMode="MultiLine"></asp:TextBox>
                                     
								</div>
							</div>

                                <div class="row">
								<div class="col-md-3">
									
								</div>
								<div class="col-md-9">
									<asp:Button ID="btnsave" runat="server" SkinID="btnSave" OnClick="btnsave_Click" />

									</div>
									</div>
									</asp:Panel>

									<asp:Panel ID="pnlDisplay" runat="server">
										<div class="row">
								<div class="col-md-12">
									<asp:Label ID="lblmsg" runat="server"></asp:Label>

									</div>
											</div>
										</asp:Panel>


                                </div>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
