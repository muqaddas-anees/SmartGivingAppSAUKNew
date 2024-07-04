<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="UpgradePlan.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.UpgradePlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Upgrade Plan
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

       <div class="form-group row">
      <div class="col-md-12">
          <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
          </div>
              </div>

    <asp:UpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <asp:ListView ID="list_Customfields" runat="server" InsertItemPosition="None" OnItemCanceling="list_Customfields_ItemCanceling" OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
           <LayoutTemplate>
              <div class="form-group ">
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>
          <ItemTemplate>
              <div class="col-md-4">
              <div class="well" style="min-height:760px;">
                   <div class="form-group row" style="text-align:right;margin-bottom:-5px;">
       
            <p style="font-size:15px;text-align:right;">
           
                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                </p>
           
                        </div>

                     <div class="form-group row">
        <div class="col-md-12" style="padding-left:0px;padding-right:0px;padding-top:5px">
                   <asp:Button ID="btnItemView" runat="server" Text='<%# Eval("PlanName") %>' SkinID="btnDefault" CommandName="Item" CommandArgument='<%# Eval("ID") %>' CausesValidation="false" style="float:left;width:100%;font-size:25px" ></asp:Button>
                 
            </div>
                         </div>
                    <div class="form-group row">
                         <div class="col-md-5" style="background-color: antiquewhite;padding:15px;margin:15px">
                               <div class="form-group row">
      <div class="col-md-12"  style="text-align:center">
           <label class="col-sm-12 control-label" style="font-size:18px;"> Monthly</label>
          </div>
                       </div>

                             
                   <div class="form-group row">
      <div class="col-md-12" style="text-align:center;">
         
              <asp:Label ID="txtMonth" runat="server" Font-Size="XX-Large" Text='<%# Eval("MonthlyPrice","{0:C2}") %>'></asp:Label>
          <asp:HiddenField ID="hMonth" runat="server" Value='<%# Eval("MonthlyPrice","{0:F2}") %>'></asp:HiddenField>
            
              </div>
         
              </div>

                         </div>
                          <div class="col-md-5" style="background-color: paleturquoise;padding:15px;margin:15px">
                                 <div class="form-group row">
      <div class="col-md-12"  style="text-align:center">
           <label class="col-sm-12 control-label" style="font-size:18px;"> Yearly</label>
          </div>
                       </div>

                                  <div class="form-group row">
      <div class="col-md-12" style="text-align:center">
         
              <asp:Label ID="txtYear" runat="server" Font-Size="XX-Large" Text='<%# Eval("YearlyPrice","{0:C2}") %>'></asp:Label>

           <asp:HiddenField ID="hYear" runat="server" Value='<%# Eval("YearlyPrice","{0:F2}") %>'></asp:HiddenField>
            
              </div>
         
              </div>

                              </div>
                        </div>

                 
          



                    <div class="form-group row" style="padding-bottom:150px">
      <div class="col-md-12"  style="text-align:center">
           <label class="col-sm-12 control-label" style="font-size:16px;"> Billed Annually per user -per month</label>
          </div>
                       </div>

                 
                 
                  <div class="form-group row" style="display:none;visibility:hidden;">
                       <div class="col-md-6">
                          
                           </div>
                       <div class="col-md-6" style="text-align:right;">
                          
                           </div>
                      </div>
                   
                    <div class="form-group row" style="margin-bottom:0px">
        <div class="col-md-12 text-center alert alert-info" style="padding:5px;margin-bottom:10px">
        <strong> <asp:HyperLink ID="hLinkFinnace" runat="server"  Text="Modules" ForeColor="White" Target="_blank" Font-Size="Large"></asp:HyperLink> </strong>
            
            </div>
    </div>
                  
           
                   <div class="form-group row">
                       <div class="col-md-12 form-inline" style="text-align:center;height:300px">
                           
                          <asp:GridView ID="gv" runat="server" Width="100%" ShowHeader="false" SkinID="NewGrid" CellSpacing="15">
                              <Columns>
                                  <asp:TemplateField Visible="false">
                                      <ItemTemplate>
                                          <asp:CheckBox runat="server" ID="chk" />
                                          <asp:Label ID="lblModuleID" runat="server"  Text='<%# Eval("ModuleID") %>' Visible="false"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Height="35">
                                      <ItemTemplate>
                                          <asp:Label ID="lblModuleName" runat="server" Text='<%# Eval("ModuleName") %>' Font-Size="Medium" ></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                       </div>
                       </div>
                   <div class="form-group row">
        <div class="col-md-12" style="text-align:center">
          
          <div class="col-sm-6 form-inline">
             
              <label class="control-label"> No. of Users</label> 
             <asp:DropDownList ID="ddlUser" runat="server" SkinID="ddl_75px" OnSelectedIndexChanged="ddlterm_SelectedIndexChanged" AutoPostBack="true">
                 <asp:ListItem Selected="True" Value="1" Text="1"></asp:ListItem>
                 <asp:ListItem Value="2" Text="2"></asp:ListItem>
                 <asp:ListItem Value="3" Text="3"></asp:ListItem>
                 <asp:ListItem Value="4" Text="4"></asp:ListItem>
                 <asp:ListItem Value="5" Text="5"></asp:ListItem>
                 <asp:ListItem Value="6" Text="6"></asp:ListItem>
                 <asp:ListItem Value="7" Text="7"></asp:ListItem>
                 <asp:ListItem Value="8" Text="8"></asp:ListItem>
                 <asp:ListItem Value="9" Text="9"></asp:ListItem>
                 <asp:ListItem Value="10" Text="10"></asp:ListItem>
                 <asp:ListItem Value="11" Text="11"></asp:ListItem>
                 <asp:ListItem Value="12" Text="12"></asp:ListItem>
                 <asp:ListItem Value="13" Text="13"></asp:ListItem>
                 <asp:ListItem Value="14" Text="14"></asp:ListItem>
                 <asp:ListItem Value="15" Text="15"></asp:ListItem>
                 <asp:ListItem Value="16" Text="16"></asp:ListItem>
                 <asp:ListItem Value="17" Text="17"></asp:ListItem>
                 <asp:ListItem Value="18" Text="18"></asp:ListItem>
                 <asp:ListItem Value="19" Text="19"></asp:ListItem>
                 <asp:ListItem Value="20" Text="20"></asp:ListItem>
                  <asp:ListItem Value="21" Text="21"></asp:ListItem>
                 <asp:ListItem Value="22" Text="22"></asp:ListItem>
                 <asp:ListItem Value="23" Text="23"></asp:ListItem>
                 <asp:ListItem Value="24" Text="24"></asp:ListItem>
                 <asp:ListItem Value="25" Text="25"></asp:ListItem>
                 <asp:ListItem Value="26" Text="26"></asp:ListItem>
                 <asp:ListItem Value="27" Text="27"></asp:ListItem>
                 <asp:ListItem Value="28" Text="28"></asp:ListItem>
                 <asp:ListItem Value="29" Text="29"></asp:ListItem>
                 <asp:ListItem Value="30" Text="30"></asp:ListItem>

             </asp:DropDownList>
              </div>
             <div class="col-sm-6 form-inline" >
                 <label class="control-label"> Term</label> 
                 <asp:DropDownList ID="ddlTerm" runat="server" SkinID="ddl_100px" OnSelectedIndexChanged="ddlterm_SelectedIndexChanged" AutoPostBack="true">
                     <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                      <asp:ListItem Text="Yearly" Value="Yearly"></asp:ListItem>
                 </asp:DropDownList>
                 </div>

            </div>
                       </div>

                   <div class="form-group row">
        <div class="col-md-12" style="text-align:center;margin-top:15px" >
            <p style="font-size:x-large"> Total : <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label> </p>

            </div>
                       </div>

                    <div class="form-group row">
        <div class="col-md-12" style="padding-left:0px;padding-right:0px;padding-top:5px">
                   <asp:Button ID="Button1" runat="server"  SkinID="btnDefault" CommandName="upgrade" Text="Buy Now" CommandArgument='<%# Eval("PlanName") %>' CausesValidation="false" style="float:left;width:100%;font-size:25px" ></asp:Button>
                 
            </div>
                         </div>

                
    </div>
                  </div>


                
               
              </ItemTemplate>
               </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>

   


    <div class="form-group row">
      <div class="col-md-12">
          <asp:Label ID="Label1" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
          </div>
              </div>
    <script>

        //$(document).ready(
        //    function () {
        //        var t = $('[id^=ddlTerm]').val();
        //        var u = $('[id^=ddlUser]').val();
        //        var m = $('[id^=hMonth]').val();
        //        var y = $('[id^=hYear]').val();
        //        $('[id^=lbltotal]').html(getamount(t, u, m, y).toFixed(2));

        //        $('[id^=ddlTerm]').change(function () {

        //            alert($(this).closest('input').find('[id^=hMonth]').val());
        //            t = $('[id^=ddlTerm]').val();
        //            u = $('[id^=ddlUser]').val();
        //            m = $('[id^=hMonth]').val();
        //            y = $('[id^=hYear]').val();
        //            //alert(m);
        //            //alert(getamount(t, u, m, y));

        //            $('[id^=lbltotal]').html(getamount(t, u, m, y).toFixed(2));

        //        });

        //        $('[id^=ddlUser]').change(function () {

                   
        //             t = $('[id^=ddlTerm]').val();
        //             u = $('[id^=ddlUser]').val();
        //             m = $('[id^=hMonth]').val();
        //             y = $('[id^=hYear]').val();

        //            $('[id^=lbltotal]').html(getamount(t, u, m, y).toFixed(2));
                    
                   
        //           // alert(getamount(t,u,m,y));
        //        });

        //        function getamount(t, u, m, y) {

        //            if (t == "Monthly") {
        //                return m * u;
        //            }
        //            else if (t == "Yearly") {
        //                return y * u;
        //            }

        //        }

        //    });
    </script>


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
