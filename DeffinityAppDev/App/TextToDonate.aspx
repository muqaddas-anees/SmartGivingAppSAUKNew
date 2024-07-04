<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="TextToDonate.aspx.cs" Inherits="DeffinityAppDev.App.TextToDonate" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="row">
        <div class="col-lg-9">
            <div class="card mb-5" style="min-width:340px;margin-left:15px;" >
    <!--begin::Card header-->
    <div class="card-header border-0 cursor-pointer" data-bs-target="#kt_account_profile_details" aria-controls="kt_account_profile_details">
        <!--begin::Card title-->
        <div class="card-title m-0">
            <h3 class="fw-bolder m-0">

               Text To Donate
            </h3>
        </div>
        <div class="card-toolbar">
            
        </div>
        <!--end::Card title-->
    </div>
  
            <div class="card-body border-top p-9">
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/tagify/4.12.0/tagify.min.js" integrity="sha512-uDMk0LmYVhMq6mKY7QfiJAXBchLmLiCZjh5hmZ6UUEJ/iNDk2s8maQDx4lOPCqLJqvhktN/g7oZTesQ6SOIjhw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
	
        <style>
           .mycheckBig input {width:18px; height:18px;}
           .mycheckBig label {padding-left:8px;padding-right:8px}


           .col-lg-8> tags{
			  padding:20px;
			  height:70px;
		}
		.tagify__input {
    flex-grow: 1;
  /*  display: inline-block;*/
    min-width: 110px;
    margin: 5px;
    padding: 0.3em 0.5em;
    padding: var(--tag-pad,.3em .5em);
    line-height: inherit;
    position: relative;
    white-space: pre-wrap;
    color: inherit;
    color: var(--input-color,inherit);
    box-sizing: inherit;
  
}
		.tags-look .tagify__dropdown__item{
  display: inline-block;
  border-radius: 3px;
  padding: .3em .5em;
  border: 1px solid #CCC;
  background: #F3F3F3;
  margin: .2em;
  font-size: .85em;
  color: black;
  transition: 0s;
  height:35px;
}

.tags-look .tagify__dropdown__item--active{
  color: black;
}

.tags-look .tagify__dropdown__item:hover{
  background: lightyellow;
  border-color: gold;
}
.tagify__input {
    flex-grow: 1;
    /*display: inline-block;*/
    min-width: 110px;
    margin: 5px;
    padding: 0.3em 0.5em;
    padding: var(--tag-pad,.3em .5em);
    line-height: inherit;
    position: relative;
    white-space: pre-wrap;
    color: inherit;
    color: var(--input-color,inherit);
    box-sizing: inherit;
}
       </style>
      <script src="../Content/assets/js/ckeditor/ckeditor.js"></script>
    <script src="../Content/assets/js/ckeditor/adapters/jquery.js"></script>
      <script language="javascript" type="text/javascript">
          $(document).ready(function () {
              $("#ddlType").change(function () {
                  var index = this.selectedIndex;
                  var tag = $("#ddlType").val();
                  //alert( $("#ddlType").val());
                  CKEDITOR.instances['txtsms'].insertHtml("{{" + tag + "}} ");

                  return false;
              });
              $("#ddlFund").change(function () {
                  var index = this.selectedIndex;
                  var tag = $("#ddlFund").val();
                  //alert( $("#ddlType").val());
                  CKEDITOR.instances['txtsms'].insertHtml(" " + tag + " ");

                  return false;
              });

          });

      </script>
       <div class="row mb-6">
        <div class="col-lg-12">
            <asp:RadioButtonList ID="rd" runat="server" RepeatDirection="Horizontal" CssClass="mycheckBig" AutoPostBack="true" OnSelectedIndexChanged="rd_SelectedIndexChanged1"> 
                <asp:ListItem Text="By Tags" Value="By Tags" Selected="true"></asp:ListItem>
                 <asp:ListItem Text="By Phone number" Value="By Phone number" ></asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
      <div class="row mb-6" id="pnlTag" runat="server">
        <div class="col-lg-12">
           <input id="txtSkills" runat="server" name="input-custom-dropdown" class="form-control" placeholder="Select tags" value="" style="width:50%;height:75px" />
           <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <%--<script src="../assets/js/custom/documentation/forms/tagify.js"></script>--%>
	<script>
        //      var input1 = document.querySelector("#kt_tagify_1");
        //new Tagify(input1);

        var input = document.querySelector('#MainContent_MainContent_MainContent_txtSkills'),
            // init Tagify script on the above inputs
            tagify = new Tagify(input, {
                whitelist: JSON.parse('<% = JsonWithStringTags()%>'),
                maxTags: 50,
                dropdown: {
                    maxItems: 50,           // <- mixumum allowed rendered suggestions
                    classname: "tags-look", // <- custom classname for this dropdown, so it could be targeted
                    enabled: 0,             // <- show suggestions on focus
                    closeOnSelect: false    // <- do not hide the suggestions dropdown once an item has been selected
                }
            })

        //txtTags


    </script>
        </div>
    </div>
      <div class="row mb-6" id="pnlTextbox" runat="server" visible="false">
        <div class="col-lg-10 d-flex d-inline gap-3">
           
             <asp:TextBox ID="txtcode" runat="server" SkinID="Price_100px" placeholder="Code" MaxLength="6"></asp:TextBox>
            <asp:TextBox ID="txtTo" runat="server" SkinID="txt_80" placeholder="To"></asp:TextBox>
             <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server" TargetControlID="txtTo"
                                ValidChars="0123456789, " />
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtcode"
                                ValidChars="0123456789+" />
        </div>
    </div>
      <div class="col-lg-12">
          <div class="row">
              <div class="col-lg-6">
                   <asp:Label ID="lbl" runat="server" Text="Personalize"></asp:Label>
            <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static">
               
            </asp:DropDownList>
                  </div>
               <div class="col-lg-6">
                   <asp:Label ID="Label1" runat="server" Text="Fundraiser"></asp:Label>
            <asp:DropDownList ID="ddlFund" runat="server" ClientIDMode="Static">
               
            </asp:DropDownList>
                  </div>
          </div>
           

            </div>
    <div class="row mb-12">
        <div class="col-lg-12">
            <label class="col-lg-12">Content</label>
           <%-- <asp:TextBox ID="" runat="server" SkinID="txtMulti_80" TextMode="MultiLine"></asp:TextBox>--%>
             <CKEditor:CKEditorControl ID="txtsms" BasePath="~/Scripts/ckeditor/" runat="server" Height="180px"  ClientIDMode="Static" >
                
             </CKEditor:CKEditorControl>
        </div>
    </div>
     <div class="row mb-12">
        <div class="col-lg-6">
            <asp:Button ID="btnSendSms" runat="server" Text="Send SMS" SkinID="btnDefault" OnClick="btnSendSms_Click" Visible="false" />
             <asp:Button ID="Button1" runat="server" Text="Send SMS" SkinID="btnDefault" OnClick="Button1_Click" />
            <asp:HiddenField ID="hval" runat="server" Value="0" />
        </div>
    </div>

                </div>
         </div>

        </div>


         <div class="col-lg-3">

           
            <div class="card card-flush mb-5 mb-xl-10" style="display:none;visibility:hidden;">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Available Credit</span>

																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    
                                                                    <label id="lbltotal_package_amount" runat="server">0.00</label> 
                                                                   

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>

               <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Number of Messages Sent</span>

																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    
                                                                    <label id="lblsms_sent_count" runat="server">0.00</label> 
                                                                   

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>

               <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Remaining Amount</span>

																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    
                                                                    <label id="lblRemaining_amount" runat="server">0.00</label> 
                                                                   

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>
            

        </div>

         


    </div>

     
    
     <div class="card mb-5" style="min-width:340px;margin-left:15px;" >
    <!--begin::Card header-->
    <div class="card-header border-0 cursor-pointer" data-bs-target="#kt_account_profile_details" aria-controls="kt_account_profile_details">
        <!--begin::Card title-->
        <div class="card-title m-0">
            <h3 class="fw-bolder m-0">

              Packages
            </h3>
        </div>
        <div class="card-toolbar">
            
        </div>
        <!--end::Card title-->
    </div>
  
            <div class="card-body border-top p-9">
      <div class="row p-0 mb-5 px-9">
         <asp:ListView ID="listdata" runat="server" OnItemCommand="listdata_ItemCommand">
            <LayoutTemplate>
               
															  <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
														
            </LayoutTemplate>
           
            <ItemTemplate>
                 <div class="col-lg-3">
                <div class="border border-dashed border-gray-300 text-center min-w-125px rounded pt-4 pb-2 my-3">
																<span class="fs-2hx fw-bold text-primary d-block"> <%# Eval("packagename") %> </span>
                     <br />
																<span class="fs-1 fw-bolder text-gray-900 counted mb-10" data-kt-countup="true"> <%# Eval("packagesubtext") %> </span>
                    <br /><br />
                    <span class="fs-2hx fw-bolder text-gray-900 counted mb-10" data-kt-countup="true"><asp:Button ID="btnBuyNow" runat="server" Text="Buy Now" CommandName="buy" CommandArgument=<%# Eval("packageid") %> />   </span>
															</div>
                     </div>
            </ItemTemplate>
        </asp:ListView>
        </div>
                </div>
         </div>


     <div class="card mb-5" style="min-width:340px;margin-left:15px;" >
    <!--begin::Card header-->
    <div class="card-header border-0 cursor-pointer" data-bs-target="#kt_account_profile_details" aria-controls="kt_account_profile_details">
        <!--begin::Card title-->
        <div class="card-title m-0">
            <h3 class="fw-bolder m-0">

               Package History
            </h3>
        </div>
        <div class="card-toolbar">
            
        </div>
        <!--end::Card title-->
    </div>
  
            <div class="card-body border-top p-9">
                  <asp:GridView ID="GridPackageHistory" runat="server" >
                                                               <Columns>
                                                                   <asp:TemplateField HeaderText="Date & Time">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblDateTime" runat="server" Text='<%# Bind("DateTime") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Organization" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblOrganization" runat="server" Text='<%# Bind("Organization") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																    <asp:TemplateField HeaderText="Bundle" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblPacakge" runat="server" Text='<%# Bind("Pacakge") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																    <asp:TemplateField HeaderText="SMS Volume" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblVolume" runat="server" Text='<%# Bind("Volume") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																     <asp:TemplateField HeaderText="Sell Price" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblSellPrice" runat="server" Text='<%# Bind("SellPrice") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
																    <asp:TemplateField HeaderText="PurchasedBy" >
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblPurchasedBy" runat="server" Text='<%# Bind("PurchasedBy") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                   <%--<asp:TemplateField ItemStyle-Width="5%">
                                                                       <ItemTemplate>
                                                                          <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete record?');" />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>--%>
                                                               </Columns>
                                                           </asp:GridView>


                </div>
         </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
