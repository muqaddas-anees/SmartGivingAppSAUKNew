<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FundraiserDetailsCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.FundraiserDetailsCtrl" %>
<%@ Register Src="~/App/controls/FundraiserPayCtrl.ascx" TagPrefix="Pref" TagName="FundraiserPayCtrl" %>
<%@ Register Src="~/App/controls/FundProgressCtrl.ascx" TagPrefix="Pref" TagName="FundProgressCtrl" %>


<link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
<script src="../assets/plugins/global/plugins.bundle.js"></script>
<%--  <link href="assets/main.css?v=5" type="text/css" rel="stylesheet">--%>

<div class="row">
    <div class="col-lg-9 col-md-6 col-sm-12">
        <div class="card mb-5 mb-xl-10" style="margin-left: 15px">
            <!--begin::Card body-->
            <div class="card-body border-top p-9">
                <!--begin::Input group-->
                <div class="row pt-5">
                    <div class="row mb-6"></div>
                    <div class="row p-5">
                        <h3 class="fw-bolder m-0 text-center">
                            <asp:Label ID="lblTitle" runat="server" Font-Size="32px"></asp:Label>
                        </h3>
                    </div>
                    <div class="col-lg-12">
                        <div class="row mt-5">
                            <asp:Image ID="imgcenterimage" runat="server" CssClass="img-fluid" />
                            <asp:HiddenField ID="hmoney" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <!--end::Card body-->
            <input type="hidden">
            <div></div>
        </div>

        <div class="card mb-5 mb-xl-10" id="pnl_mystory" runat="server" style="margin-left: 15px">
            <div class="card-body border-top p-9">
                <div id="pnl_img" runat="server" class="text-center"></div>
                <div class="row fs-2 mt-10">
                    <asp:Label ID="lblMyStory" Text="" runat="server" /><br />
                    <br />
                </div>
            </div>
        </div>

        <div class="card mb-5 mb-xl-10" style="margin-left: 15px">
            <div class="card-body border-top p-9">
                <div class="row">
                    <h4 class="fs-1">Campaign Description </h4>
                </div>
                <div class="row fs-2">
                    <asp:Label ID="lblDescription" Text="" runat="server" /><br />
                    <br />
                </div>
            </div>
        </div>

        <div class="row" style="margin-left: 15px">
            <Pref:FundraiserPayCtrl runat="server" ID="FundraiserPayCtrl" />
        </div>
    </div>

    <div class="col-lg-3 col-md-6 col-sm-12">
        <div class="card mb-5" style="min-width: 340px; margin-left: 15px;">
            <!--begin::Card header-->
            <div class="card-header border-0 cursor-pointer" data-bs-target="#kt_account_profile_details" aria-controls="kt_account_profile_details">
                <!--begin::Card title-->
                <div class="card-title m-0">
                    <h3 class="fw-bolder m-0">Fundraising Target</h3>
                </div>
                <div class="card-toolbar">
                    <asp:HyperLink ID="btnBack" runat="server" NavigateUrl="~/App/FundraiserListView.aspx" CssClass="btn btn-light" Text="Back to Fundraisers"></asp:HyperLink>
                </div>
                <!--end::Card title-->
            </div>

            <div class="card-body border-top p-9 h-400px text-center">
                <div style="">
                <span id="amountraised" class="raised fw-bolder">
                    
                </span>
                   <p class="fs-2"> Raised of    <asp:Label ID="lblTarget" runat="server" Text=""></asp:Label>  </p>
                    </div>
                <div class="d-flex flex-wrap align-center flex-center" style="min-width: 350px;align-content:center">

                    <div class="progress-wrapper position-relative d-flex flex-center  me-15 mb-7" style="min-width: 300px">

                        <div class="position-absolute translate-middle start-50 top-75 d-flex flex-column flex-center">

                            <p class="fs-1 fw-bold text-gray-400 mb-10"></p>
                        </div>
                        <div id="progress-bar" class="progress-bar"></div>
                    </div>
                   <p> Days left <span id="days"></span>, Total Supporters <span id="supporters"></span></p>
                </div>
            </div>
        </div>

        <div class="card mb-5 mb-xl-10" id="pnl_QRcode" runat="server">
            <div class="card-header border-0 cursor-pointer" data-bs-target="#kt_account_profile_details" aria-controls="kt_account_profile_details">
                <div class="card-title m-0">
                    <h3 class="fw-bolder m-0 d-flex justify-content-center">QR Code</h3>
                </div>
            </div>
            <div class="card-body border-top p-9">
                <asp:Image ID="imgQR" runat="server" CssClass="img-fluid" />
            </div>
        </div>

        <div class="card mb-0 mb-xl-10" id="pnl_topdonors" runat="server" visible="false">
            <div class="card-header border-0 cursor-pointer" data-bs-target="#kt_account_profile_details" aria-controls="kt_account_profile_details">
                <div class="card-title m-0">
                    <h3 class="fw-bolder m-0 d-flex justify-content-center">Top Donors</h3>
                </div>
            </div>
            <div class="card-body border-top p-9">
                <asp:GridView ID="gridtopdonors" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <div class="d-flex d-inline gap-3">
                                    <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetDonorImageUrl("0") %>' Width="50px" Height="50px" />
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' Style="margin-top: 15px; font-weight: bold"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="header_right" ItemStyle-CssClass="header_right">
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AmountDis")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</div>

<div class="row" id="pnlProgress" runat="server">
    <div class="col-lg-11 col-md-12 col-sm-12" style="margin-left: 15px; margin-right: 25px">
        <Pref:FundProgressCtrl runat="server" ID="FundProgressCtrl" />
    </div>
</div>

<style>
    .header_right {
        text-align: right;
    }

    .progress-wrapper {
        border-radius: 20px;
        overflow: hidden;
    }

    .raised {
        font-size: xxx-large;
        color: #4caf50;
    }

    .progress-bar {
        width: 100%;
        height: 10px;
        background-color: #eee;
        border-radius: 15px;
    }
</style>

<asp:HiddenField ID="hraised" runat="server" Value="30.00" ClientIDMode="Static" />
<asp:HiddenField ID="remainingTime" runat="server" Value="30.00" ClientIDMode="Static" />
<asp:HiddenField ID="totalsupporters" runat="server" Value="30.00" ClientIDMode="Static" />
<asp:HiddenField ID="hremaing" runat="server" Value="40.00" ClientIDMode="Static" />

<script src="https://cdn.jsdelivr.net/npm/progressbar.js"></script>
<script>
    dchart();

    function dchart() {
        var raised = parseFloat(document.getElementById("hraised").value);
        var remaining = parseFloat(document.getElementById("hremaing").value);
        var total = raised + remaining;
        
        var percentage = raised===0||remaining==0?0:raised / total;
        var amountraised = document.getElementById("amountraised");
        var DaysLeft = document.getElementById("remainingTime").value;
        var days = document.getElementById("days");
        var totalsupporters = document.getElementById("totalsupporters").value;
        var supporters = document.getElementById("supporters");
        days.innerHTML = " "+DaysLeft;
        supporters.innerHTML = " " +totalsupporters;

        amountraised.innerHTML = raised;

        var bar = new ProgressBar.Line('#progress-bar', {
            strokeWidth: 4,
            easing: 'easeInOut',
            duration: 1400,
            color: '#4caf50',
            trailColor: '#eee',
            trailWidth: 1,
            svgStyle: { width: '100%', height: '100%' },
            from: { color: '#4caf50' },
            to: { color: '#4caf50' },
            step: (state, bar) => {
                bar.path.setAttribute('stroke', state.color);
            }
        });

        bar.animate(percentage); // Number from 0.0 to 1.0
    }
</script>
