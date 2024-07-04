<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="TithingCategorySettings.aspx.cs" Inherits="DeffinityAppDev.App.TithingCategorySettings" %>

<%@ Register Src="~/App/controls/TithingCategoryTabs.ascx" TagPrefix="Pref" TagName="TithingCategoryTabs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
    Donation Categories
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_title" runat="server">
   Donation Categories
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <a class="btn btn-video" id="btn_video" runat="server" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo"  style="display:none" src="https://player.vimeo.com/video/820570163?h=a329b4663f" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:TithingCategoryTabs runat="server" ID="TithingCategoryTabs" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
      .mycheckBig input {width:25px; height:25px;}
.mycheckSmall input {width:10px; height:10px;}
    </style>

    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>

     

    <div class="row mb-6">
        <div class="col-sm-12 d-flex justify-content-end">
        <asp:Button ID="btnAddCategory" runat="server" CssClass="btn btn-primary" Text="Add New Category"  />
            </div>
    </div>


                            <asp:GridView ID="GridInstances" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridInstances_RowCommand" OnRowDataBound="GridInstances_RowDataBound" OnPageIndexChanging="GridInstances_PageIndexChanging">
                                <Columns>
                                   
                                  
                                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                        <HeaderStyle />
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrganization" runat="server" Text='<%# Bind("Name") %>' Font-Size="20px" Font-Bold="true"></asp:Label> <br />
                                         <asp:Label ID="Label6" runat="server" Text="Category ID"></asp:Label> :  <asp:Label ID="Label5" runat="server" Text='<%# Bind("CategoryID") %>'></asp:Label>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                   
                                      <asp:TemplateField>
                                        <ItemTemplate>


                                            <asp:Label ID="lblPortfolioID" runat="server" Width="40px" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                            <asp:CheckBox ID="chk" runat="server" OnClick="javascript:SelectSingleCheckbox(this.id)" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="" SortExpression="ID">
                                        <HeaderStyle />
                                        <ItemStyle Width="150px" />
                                        <ItemTemplate>
                                             <asp:LinkButton runat="server" ID="EditBannerInList" Text="Delete" SkinID="BtnLinkDelete" CommandArgument='<%# Eval("ID") %>'    CommandName="del" OnClientClick="return confirm('Do you want to delete record?');"   />

                                             <%--<asp:LinkButton SkinID="BtnLinkDelete" ID="linkbtnDelete" runat="server" OnClientClick ="Do you want to delete this record?"   OnClick="btnDeleteReligion_Click"      ></asp:LinkButton>--%>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField ItemStyle-Width="9px">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandArgument='<%# Eval("ID") %>'    CommandName="edit1"   ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   






                                                         
                                                      
                                  

                                </Columns>
                            </asp:GridView>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

    <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="btnAddCategory" PopupControlID="pnlAddReligion" >
    </ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
    <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
    <asp:Panel ID="pnlAddReligion" runat="server" BackColor="White" Style="display: none;"
        Width="550px" Height="630px" ScrollBars="Vertical">

        <div class="card card-bordered">


             <div class="card-header">
                <h3 class="card-title">
                    <asp:Label ID="lblModelHeading" runat="server" Text="Add Category"></asp:Label>
                </h3>
                <div class="card-toolbar">
                  
                </div>
            </div>
            <div class="card-body">
                <div class="row mb-6">
                    <!--begin::Label-->
                    <label class="col-lg-4 col-form-label required fw-bold fs-6">
                        <asp:Label ID="lblpoptitlte" runat="server" Text="Name"></asp:Label>
                    </label>
                    <!--end::Label-->
                    <!--begin::Col-->
                    <div class="col-lg-6 fv-row fv-plugins-icon-container">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hidTxtBox"  runat="server" Value="0"></asp:HiddenField>
                    </div>
                </div>
                 <div class="row mb-6">
                    <!--begin::Label-->
                    <label class="col-lg-4 col-form-label fw-bold fs-6">
                        <asp:Label ID="Label2" runat="server" Text="Category ID"></asp:Label>
                    </label>
                    <!--end::Label-->
                    <!--begin::Col-->
                    <div class="col-lg-6 fv-row fv-plugins-icon-container">
                        <asp:TextBox ID="txtCategoryID" runat="server"></asp:TextBox>
                    </div>
                </div>
                  <div class="row mb-6">
                    <!--begin::Label-->
                    <label class="col-lg-4 col-form-label  fw-bold fs-6">
                        <asp:Label ID="Label1" runat="server" Text="Description"></asp:Label>
                    </label>
                    <!--end::Label-->
                    <!--begin::Col-->
                    <div class="col-lg-6 fv-row fv-plugins-icon-container">
                        <asp:TextBox ID="txtDescriptin" runat="server" TextMode="MultiLine" SkinID="txtMulti_80"></asp:TextBox>
                    </div>
                </div>
                   <div class="row mb-6">
                    <!--begin::Label-->
                    <label class="col-lg-4 col-form-label  fw-bold fs-6">
                        <asp:Label ID="Label4" runat="server" Text="Hidden"></asp:Label>
                    </label>
                    <!--end::Label-->
                    <!--begin::Col-->
                    <div class="col-lg-6 pt-5">
                        
                      <asp:CheckBox ID="chkHidden" runat="server" Checked="false" Font-Size="20px" CssClass="mycheckBig" />
                           
                    </div>
                </div>
                  <div class="row">
                    <!--begin::Label-->
                    <label class="col-lg-4 col-form-label  fw-bold fs-6">
                        <asp:Label ID="Label3" runat="server" Text="Active"></asp:Label>
                    </label>
                    <!--end::Label-->
                    <!--begin::Col-->
                    <div class="col-lg-6 pt-5">
                      <asp:CheckBox ID="chkActive" runat="server" Checked="true" CssClass="mycheckBig"  />

                    </div>
                </div>
            </div>
           
            <div class="card-footer d-flex justify-content-end py-6 px-9">
                <asp:Button  ID="btnClose"  runat="server" CssClass="btn btn-light" Text="Close" OnClick="btnClose_Click" /> &nbsp;&nbsp;&nbsp;

               <%-- ID="btnClose"--%>
               

                 <asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSaveChangesPop_Click" />
            </div>
        </div>

    </asp:Panel>

</asp:Content>
