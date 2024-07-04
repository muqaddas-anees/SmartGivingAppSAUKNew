<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="PurchaseTraining.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.PurchaseTraining" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Purchase Training
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
        <div class="col-md-12">
                        
            <asp:ListView ID="list_training" runat="server" InsertItemPosition="None" OnItemCommand="list_training_ItemCommand" >
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
                                                 <asp:Literal ID="Literal1" runat="server" Text='<%# GetCssImage(Eval("TrainingImage")) %>'></asp:Literal>
                                             </div>
                                             <div class="xe-label">
                                                 <strong style="font-size:medium;"><asp:Literal ID="lbltitle" runat="server" Text='<%# Eval("TrainingName") %>'></asp:Literal></strong>
                                             </div>
                                         </div>
                                         <div class="xe-body" style="font-size:medium;">
                                                 <div style="height:120px;">
                                                    <asp:Literal ID="lblMdata" runat="server" Text='<%# Eval("TrainingDescription") %>'></asp:Literal>
                                             </div>
                                            
                                         </div>
                                        
                                         <div class="xe-footer" style="padding-top:8px;text-align:center;">
                                            <asp:Label ID="Literal2" runat="server" Text='<%# Eval("Amount","{0:C}") %>' style="text-align:center;font-size:22px;font-weight:bold;"></asp:Label>
                                              <br /><br />
                                             <asp:Button ID="BtnView" runat="server" CommandArgument='<%# Eval("TrainingID") %>' CommandName="buy" Text="BUY" SkinID="btnDefault" Font-Size="Large" style="width:100%"  />

                                         </div>
                                     </div>
                                 </div>
              </ItemTemplate>
               </asp:ListView>

                    </div>
				
        </div>


   
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script type="text/javascript">

        hidetabs();
    </script>
</asp:Content>
