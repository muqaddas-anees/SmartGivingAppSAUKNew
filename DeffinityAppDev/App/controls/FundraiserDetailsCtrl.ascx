<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FundraiserDetailsCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.FundraiserDetailsCtrl" %>
<%@ Register Src="~/App/controls/FundraiserPayCtrl.ascx" TagPrefix="Pref" TagName="FundraiserPayCtrl" %>
<%@ Register Src="~/App/controls/FundProgressCtrl.ascx" TagPrefix="Pref" TagName="FundProgressCtrl" %>


<link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
<script src="../assets/plugins/global/plugins.bundle.js"></script>
<!-- Option 1: Include in HTML -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css">
<%--  <link href="assets/main.css?v=5" type="text/css" rel="stylesheet">--%>
<style>
    .view-modal, .popup{
  position: absolute;
  left: 50%;
}
button{
  outline: none;
  cursor: pointer;
  font-weight: 500;
  border-radius: 4px;
  border: 2px solid transparent;
  transition: background 0.1s linear, border-color 0.1s linear, color 0.1s linear;
}
.view-modal{
  top: 10%;
  left: 90%;
  color: #e8e4ee;
  font-weight: bold;
  font-size: 18px;
  padding: 10px 25px;
  background: rgb(113, 5, 156);
  transform: translate(-50%, -50%);
}
.popup{
  background: rgb(255, 254, 254);
  padding: 25px;
  border-radius: 15px;
  top: 10%;
  max-width: 380px;
  width: 100%;
  opacity: 0;
  pointer-events: none;
  box-shadow: 0px 10px 15px rgba(0,0,0,0.3);
  transform: translate(-50%, -50%) scale(1.2);
  transition: top 0s 0.2s ease-in-out,
              opacity 0.2s 0s ease-in-out,
              transform 0.2s 0s ease-in-out;
}
.popup.show {
    top: 50%;
    left: 50%;
    opacity: 7;
    position: fixed;
    pointer-events: auto;
    transform: translate(-50%, -50%) scale(1);
    transition: top 0s 0s ease-in-out, opacity 0.2s 0s ease-in-out, transform 0.2s 0s ease-in-out;
    translate: (-50%, -50%);
    transform: translate(-50%, -50%);
}
.popup :is(header, .icons, .field){
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.popup header{
  padding-bottom: 15px;
  border-bottom: 1px solid #ebedf9;
}
header span{
  font-size: 21px;
  font-weight: 600;
}
header .close, .icons a{
  display: flex;
  align-items: center;
  border-radius: 50%;
  justify-content: center;
  transition: all 0.3s ease-in-out;
} 
header .close{
  color: #878787;
  font-size: 17px;
  background: #f3f3f3;
  height: 33px;
  width: 33px;
  cursor: pointer;
}
header .close:hover{
  background: #ebedf9;
}
.popup .content{
  margin: 20px 0;
}
.popup .icons{
  margin: 15px 0 20px 0;
}
.content p{
  font-size: 16px;
}
.content .icons a{
  height: 50px;
  width: 50px;
  font-size: 20px;
  text-decoration: none;
  border: 1px solid transparent;
}
.icons a i{
  transition: transform 0.3s ease-in-out;
}
.icons a:nth-child(1){
  color: #1877F2;
  border-color: #b7d4fb;
}
.icons a:nth-child(1):hover{
  background: #1877F2;
}
.icons a:nth-child(2){
  color: #46C1F6;
  border-color: #b6e7fc;
}
.icons a:nth-child(2):hover{
  background: #46C1F6;
}
.icons a:nth-child(3){
  color: #e1306c;
  border-color: #f5bccf;
}
.icons a:nth-child(3):hover{
  background: #e1306c;
}
.icons a:nth-child(4){
  color: #25D366;
  border-color: #bef4d2;
}
.icons a:nth-child(4):hover{
  background: #25D366;
}
.icons a:nth-child(5){
  color: #0088cc;
  border-color: #b3e6ff;
}
.icons a:nth-child(5):hover{
  background: #0088cc;
}
.icons a:hover{
  color: #fff;
  border-color: transparent;
}
.icons a:hover i{
  transform: scale(1.2);
}
.content .field{
  margin: 12px 0 -5px 0;
  height: 45px;
  border-radius: 4px;
  padding: 0 5px;
  border: 1px solid #757171;
}
.field.active{
  border-color: #7d2ae8;
}
.field i{
  width: 50px;
  font-size: 18px;
  text-align: center;
}
.field.active i{
  color: #7d2ae8;
}
.field input{
  width: 100%;
  height: 100%;
  border: none;
  outline: none;
  font-size: 15px;
}
.field button{
  color: #fff;
  padding: 5px 18px;
  background: #7d2ae8;
}
.field button:hover{
  background: #8d39fa;
}
/* Default styling for large screens */
.img-resize {
    width: 70vw;
    height: 70vh;
    background-size: cover;
}

/* Medium screens */
@media (max-width: 1024px) {
    .img-resize {
        width: 80vw;
        height: 60vh;
    }
}

/* Small screens */
@media (max-width: 768px) {
    .img-resize {
        width: 90vw;
        height: 50vh;
    }
}

/* Extra small screens */
@media (max-width: 480px) {
    .img-resize {
        width: 100vw;
        height: 40vh;
    }
}

    #shareBtn{
    letter-spacing: 2px;
    font-weight: 600;
    box-shadow: none;
    background-color: #eee;
    color: #7d2ae8;
    border: none;
}

.modal{
    top: 20%;
}

.btn-close{
    box-shadow: none;
    border: none;
    outline: none;
}

.modal-body .icons{
    margin: 15px 0px 20px 0px;
}

.modal-body .icons a{
    text-decoration: none;
    border: 1px solid transparent;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    margin-right: 20px;
    transition: all 0.3s ease-in-out;
}

.modal-body .icons a:nth-child(1){
    color: #1877F2;
    border-color: #B7D4FB;
}

.modal-body .icons a:nth-child(1):hover{
    background-color: #1877F2;
    color: #fff;
}

.modal-body .icons a:nth-child(2){
    color: #46C1F6;
    border-color: #b6e7fc;
}

.modal-body .icons a:nth-child(2):hover{
    background-color: #46C1F6;
    color: #fff;
}

.modal-body .icons a:nth-child(3){
    color: #e1306c;
    border-color: #f5bccf;
}

.modal-body .icons a:nth-child(3):hover{
    background-color: #e1306c;
    color: #fff;
}

.modal-body .icons a:nth-child(4){
    color: #25d366;
    border-color: #bef4d2;
}

.modal-body .icons a:nth-child(4):hover{
    background-color: #25d366;
    color: #fff;
}


.modal-body .icons a:nth-child(5){
    color: #0088cc;
    border-color: #b3e6ff;
}

.modal-body .icons a:nth-child(5):hover{
    background-color: #0088cc;
    color: #fff;
}

.modal-body .icons a:hover{
    border-color: transparent;
}

.modal-body .icons a span{
    transition: all 0.09s ease-in-out;
}

.modal-body .icons a:hover span{
    transform: scaleX(1.1);
}

.modal-body .field{
    margin: 15px 0px -5px 0px;
    height: 45px;
    border: 1px solid #dfdfdf;
    border-radius: 5px;
    padding: 0 5px;
}

.modal-body .field.active{
    border-color: #7d2ae8;
}

.field span{
    width: 50px;
    font-size: 1.1rem;
}

.field.active span{
    color: #7d2ae8;
}

.field input{
    border: none;
    outline: none;
    font-size: 0.89rem;
    width: 100%;
    height: 100%;
}

.field button{
    padding: 5px 16px;
    color: #fff;
    background: #7d2ae8;
    border: 2px solid transparent;
    border-radius: 5px;
    font-weight: 500;
}

@media (max-width: 330px) {
    .modal-body .icons a{
        margin-right: 15px;
        width: 35px;
        height: 35px;
    }
}
</style>
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
                        





<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">

                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                         

<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>






                            <asp:HiddenField ID="hmoney" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <!--end::Card body-->
            <input type="hidden">
            <div></div>
        </div>

        <div class="card mb-5 mb-xl-10" id="pnl_mystory" runat="server" >
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
        <div class="card mb-5" style="min-width: 340px; ">
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
                </div>
                                   <p class="text-center"> Days left: <span id="days"></span> , Total Supporters: <span id="supporters"></span></p>

           <button type="button" class="btn view-modal1" id="openModalBtn" style="
    border-radius: 65px;
    padding: 10px 15px;
    /* background-color: #c1c1c1; */
    color: black;
    font-weight: 300;
    border: 1px solid #c1c1c1;
    margin-right: 5px;
    ">Share</button>   <button type="button" class="btn btn-success" style="border-radius: 65px; padding: 10px 35px;margin-left:5px" onclick="scrollToDonation()">Donate</button>

            </div>
        </div>


        <div id="sharemodal1" runat="server">

        </div>


        <!-- Button trigger modal -->


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
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/js/bootstrap.bundle.min.js"></script>
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>


<script>
    dchart();
    // Function to open the modal
    const viewBtn = document.querySelector(".view-modal1"),
        popup = document.querySelector(".popup"),
        close = popup.querySelector(".close"),
        field = popup.querySelector(".field"),
        input = field.querySelector("input"),
        copy = field.querySelector("button");

    viewBtn.onclick = () => {
        popup.classList.toggle("show");
    }
    close.onclick = () => {
        viewBtn.click();
    }

    copy.onclick = () => {
        input.select(); //select input value
        if (document.execCommand("copy")) { //if the selected text is copied
            field.classList.add("active");
            copy.innerText = "Copied";
            setTimeout(() => {
                window.getSelection().removeAllRanges(); //remove selection from page
                field.classList.remove("active");
                copy.innerText = "Copy";
            }, 3000);
        }
    }
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
    function scrollToDonation() {
        const section = document.getElementById("donationsraised");
        section.scrollIntoView({ behavior: 'smooth' });
    }
</script>
