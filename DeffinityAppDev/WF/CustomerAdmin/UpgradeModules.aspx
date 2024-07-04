<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="UpgradeModules.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.UpgradeModules" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Upgrade
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server" >
     <script language="javascript" type="text/javascript">
         function SelectSingleCheckbox(Chkid) {
             var chkbid = document.getElementById(Chkid);
             var chkList = document.getElementsByTagName("input");
             for (i = 0; i < chkList.length; i++) {
                 if (chkList[i].type == "checkbox" && chkList[i].id != chkbid.id) {
                     chkList[i].checked = false;
                 }
             }
         }
</script>
     <div class="row" id="pnlDescription" runat="server">
        <div class="col-md-12">
                <div class="card shadow-sm">
						
						<div class="panel-body">
                            <div class="row">
                                    
                                   <div class="form-group row">
      <div class="col-md-12">

          <asp:Literal ID="lblDescription" runat="server"></asp:Literal>
         
          </div>
                                        
              </div>
                                </div>
                </div>
            </div>
            </div>
         </div>

    <div class="row">
        <div class="col-md-9">
                        
            <asp:ListView ID="list_modules" runat="server" InsertItemPosition="None" OnItemCommand="list_modules_ItemCommand" >
           <LayoutTemplate>
              <div class="form-group ">
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>
          <ItemTemplate>
              <div  class="col-sm-4" runat="server">
                                     <div class="xe-widget xe-todo-list xe-counter-blue">
                                         <div class="xe-header">
                                             <div class="xe-icon">
                                                 <%--<i class="fa-desktop"></i>--%>
                                                 <asp:Literal ID="Literal1" runat="server" Text='<%# GetCssImage(Eval("ModuleImage")) %>'></asp:Literal>
                                             </div>
                                             <div class="xe-label">
                                                 <strong style="font-size:medium;"><asp:Literal ID="lbltitle" runat="server" Text='<%# Eval("ModuleName") %>'></asp:Literal></strong>
                                             </div>
                                         </div>
                                         <div class="xe-body" style="font-size:medium;">
                                                 <div style="height:120px;">
                                                    <asp:Literal ID="lblMdata" runat="server" Text='<%# Eval("ModuleDescription") %>'></asp:Literal>
                                             </div>
                                            
                                         </div>
                                        
                                         <div class="xe-footer" style="padding-top:8px;">
                                           
                                              <br /><br />
                                             <asp:Button ID="BtnView" runat="server" CommandArgument='<%# Eval("ModuleName") %>' CommandName="view" Text="Watch Video" SkinID="btnDefault" Font-Size="Large" style="width:100%"  />

                                         </div>
                                     </div>
                                 </div>
              </ItemTemplate>
               </asp:ListView>

                    </div>
				<div class="col-md-3">

                    <div class="panel panel-color panel-info">
						<div class="card-header">
							<h3 class="panel-title form-inline">Become SMARTER with PREMIUM<%-- Required User Licences--%>
                               </h3>
							<div class="card-toolbar">

                              


							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
                                    
                                   <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-12 control-label">You currently have the following number of users:</label>
          <div class="col-sm-12 form-inline">
              <asp:TextBox ID="txtNoofUsers" runat="server" SkinID="txt_50px" MaxLength="10" Text=""></asp:TextBox>
              <ajaxToolkit:FilteredTextBoxExtender ID="txtFilterPrice" runat="server" ValidChars="0123456789" TargetControlID="txtNoofUsers"></ajaxToolkit:FilteredTextBoxExtender>
           <asp:Label ID="lblPriceperuser" runat="server"></asp:Label>    
          </div>
          </div>
                                        
              </div>
                                 <div class="form-group row">
                                 <div class="col-md-12 form-inline">
                                      <label class="col-sm-6 control-label" style="font-weight:bold;width:150px;padding-right:0px"><asp:CheckBox ID="chkmonth" runat="server" Checked="true" OnClick="javascript:SelectSingleCheckbox(this.id)" /> Monthly Cost:</label>
                                      <label ID="lblMonthly" runat="server" class="col-sm-4 control-label" style="font-weight:bold;text-align:right;padding-left:0px;"></label>
         <asp:HiddenField ID="hmonthprice" runat="server" />
                                     </div>
                                </div>
                                 <div class="form-group row">
                                 <div class="col-md-12">
                                      <label class="col-sm-6 control-label" style="font-weight:bold;width:150px;padding-right:0px"><asp:CheckBox ID="chkyear" runat="server" OnClick="javascript:SelectSingleCheckbox(this.id)"/> Yearly Cost:</label>
                                      <label ID="lblYearly" runat="server" class="col-sm-4 control-label" style="font-weight:bold;text-align:right;padding-left:0px;"></label>
          <asp:HiddenField ID="hyearprice" runat="server" />
                                     </div>
                                </div>
 <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-12 control-label"> <p>Users relate to Administrators and Field Service Contractors</p> </label>
          </div>
     </div>
                                <div class="form-group row">
      <div class="col-md-12" style="text-align:center;">
          <asp:Button ID="btnPay" runat="server" Text="Upgrade and Pay Now" OnClick="btnPay_Click" />
          </div>
     </div>

                                </div>
                </div>
            </div>

                    </div>
        </div>


     <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="480px" CssClass="panel panel-color panel-info" ScrollBars="None">
         

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text=""></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">

                       <iframe id="viframe" runat="server" height="340" width="600" style="border:none;"></iframe>
                       
                       </div>
            </div>
 
      
        
           
        </div>
                  
           </asp:Panel>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script type="text/javascript">
        
        hidetabs();
    </script>
</asp:Content>
