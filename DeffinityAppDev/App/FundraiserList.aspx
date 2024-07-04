<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="FundraiserList.aspx.cs" Inherits="DeffinityAppDev.App.FaithGivingListManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Fundraiser
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        /* .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }*/
    </style>

    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>

    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" >
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Fundraiser</h3>
            </div>
            <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">
                <%--  <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-bg-light" Text="Upload Members" OnClick="btnUpload_Click" style="margin-right:20px;"   /> --%>
                <asp:Button ID="btnAddOrganization" runat="server" CssClass="btn btn-primary" Text="Add New Fundraiser" OnClick="btnAddOrganization_Click" />

            </div>
            <!--end::Card title-->
        </div>
        <!--begin::Card header-->
        <!--begin::Content-->
        <div id="kt_account_profile_details" class="collapse show" style="">
            <!--begin::Form-->
            <form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
                <!--begin::Card body-->
                <div class="card-body border-top p-9">
                    <!--begin::Input group-->

                    <!--end::Input group-->
                    <!--begin::Input group-->
                    <div class="row mb-6">
                        <!--end::Col-->
                    </div>

                    <div class="row">
                        <div class="col-lg-5 fv-row fv-plugins-icon-container form-inline" style="display: inline;">
                            <br />
                        </div>
                    </div>


                    <!--end::Input group-->
                    <!--begin::Input group-->
                    <div class="row mb-6">
                        <asp:GridView ID="GridInstances" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridInstances_RowCommand" OnRowDataBound="GridInstances_RowDataBound" OnPageIndexChanging="GridInstances_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="9px">
                                    <ItemTemplate>
                                        <a href='AddFundraiser.aspx?unid=<%# Eval("unid") %>' class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
                                            <!--begin::Svg Icon | path: icons/duotune/art/art005.svg-->
                                            <span class="svg-icon svg-icon-3">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                                                    <path opacity="0.3" d="M21.4 8.35303L19.241 10.511L13.485 4.755L15.643 2.59595C16.0248 2.21423 16.5426 1.99988 17.0825 1.99988C17.6224 1.99988 18.1402 2.21423 18.522 2.59595L21.4 5.474C21.7817 5.85581 21.9962 6.37355 21.9962 6.91345C21.9962 7.45335 21.7817 7.97122 21.4 8.35303ZM3.68699 21.932L9.88699 19.865L4.13099 14.109L2.06399 20.309C1.98815 20.5354 1.97703 20.7787 2.03189 21.0111C2.08674 21.2436 2.2054 21.4561 2.37449 21.6248C2.54359 21.7934 2.75641 21.9115 2.989 21.9658C3.22158 22.0201 3.4647 22.0084 3.69099 21.932H3.68699Z" fill="black"></path>
                                                    <path d="M5.574 21.3L3.692 21.928C3.46591 22.0032 3.22334 22.0141 2.99144 21.9594C2.75954 21.9046 2.54744 21.7864 2.3789 21.6179C2.21036 21.4495 2.09202 21.2375 2.03711 21.0056C1.9822 20.7737 1.99289 20.5312 2.06799 20.3051L2.696 18.422L5.574 21.3ZM4.13499 14.105L9.891 19.861L19.245 10.507L13.489 4.75098L4.13499 14.105Z" fill="black"></path>
                                                </svg>
                                            </span>
                                            <!--end::Svg Icon-->
                                        </a>
                                    </ItemTemplate>
                                     <ItemStyle Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="9px" Visible="false">
                                    <ItemTemplate>

                                        <asp:Label ID="lblPortfolioID" runat="server" Width="40px" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                        <asp:CheckBox ID="chk" runat="server" OnClick="javascript:SelectSingleCheckbox(this.id)" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("ID").ToString()) %>' Width="50px" Height="50px" />
                                    </ItemTemplate>
                                     <ItemStyle Width="7%" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                    <HeaderStyle />
                                    <ItemStyle Width="15%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle1" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                    <HeaderStyle />
                                    <ItemStyle Width="25%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Start Date" SortExpression="StartDate">
                                    <HeaderStyle />
                                    <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate","{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="End Date" SortExpression="EndDate">
                                    <HeaderStyle />
                                    <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate","{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                 <asp:TemplateField HeaderText="Default Values" SortExpression="DefaultValues">
                                    <HeaderStyle />
                                    <ItemStyle Width="15%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefaultValues" runat="server" Text='<%# Bind("DefaultValues") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target" SortExpression="DefaultTarget" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle />
                                    <ItemStyle Width="5%" />
                                    <ItemTemplate>
                                    <asp:Label ID="lblTargetAmount" runat="server" Text='<%# Bind("DefaultTarget","{0:F2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Raised Amount" SortExpression="DefaultTarget" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle />
                                    <ItemStyle Width="5%" />
                                    <ItemTemplate>
                                    <asp:Label ID="lblRaisedAmount" runat="server" Text='<%# Bind("RaisedAmount","{0:F2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send Mail After Donation" SortExpression="SendMailAfterDonation" Visible="false">
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSendMailAfterDonation" runat="server" Text='<%# Bind("SendMailAfterDonation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Organization ID" SortExpression="OrganizationID" Visible="false">
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrganizationID" runat="server" Text='<%# Bind("OrganizationID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                         <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="linkpoll" runat="server" Target="_blank" NavigateUrl='<%# "FundraiserView.aspx?UNID=" + Eval("UNID") +"&eventunid="+ Eval("Event_unid") %>' Text="View Details" CssClass="btn btn-primary"></asp:HyperLink>
                                       <%-- <asp:Button ID="btnViewPoll" runat="server" CommandArgument='<%# Eval("UNID") %>' CommandName="ViewPoll" SkinID="btnDefault" Text="View Poll" CausesValidation="false"  />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField ItemStyle-Width="5%">
                                                                       <ItemTemplate>
                                                                          <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete record?');" />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>


                </div>
                <!--end::Card body-->
                <!--begin::Actions-->
                <div class="card-footer d-flex justify-content-end py-6 px-9">
                    <%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>
                </div>
                <!--end::Actions-->
                <input type="hidden"><div></div>
            </form>
            <!--end::Form-->
        </div>
        <!--end::Content-->
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>


    <script>
        // set the dropzone container id
        var id = "#kt_dropzonejs_example_3";

        // set the preview element template
        var previewNode = $(id + " .dropzone-item");

        previewNode.id = "";
        var previewTemplate = previewNode.parent(".dropzone-items").html();
        previewNode.remove();

        var myDropzone = new Dropzone(id, { // Make the whole body a dropzone
            url: "MemberHandler.ashx", // Set the url for your upload script location
            parallelUploads: 20,
            maxFilesize: 1, // Max filesize in MB
            previewTemplate: previewTemplate,
            previewsContainer: id + " .dropzone-items", // Define the container to display the previews
            clickable: id + " .dropzone-select" // Define the element that should be used as click trigger to select files.
        });


        myDropzone.on("addedfile", function (file) {
            // Hookup the start button
            $(document).find(id + " .dropzone-item").css("display", "");
        });

        // Update the total progress bar
        myDropzone.on("totaluploadprogress", function (progress) {
            $(id + " .progress-bar").css("width", progress + "%");
        });

        myDropzone.on("sending", function (file) {
            // Show the total progress bar when upload starts
            $(id + " .progress-bar").css("opacity", "1");
        });

        // Hide the total progress bar when nothing"s uploading anymore
        myDropzone.on("complete", function (progress) {
            var thisProgressBar = id + " .dz-complete";

            setTimeout(function () {
                $(thisProgressBar + " .progress-bar, " + thisProgressBar + " .progress").css("opacity", "0");
            }, 300)
        });
    </script>


</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
