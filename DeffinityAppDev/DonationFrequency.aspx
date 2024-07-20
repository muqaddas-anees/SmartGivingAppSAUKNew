<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="DonationFrequency.aspx.cs" Inherits="DeffinityAppDev.DonationFrequency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
          <style>
      .mycheckBig input {
          width: 18px;
          height: 18px;
      }

      .mycheckBig label {
          padding-left: 8px;
      }

     

      .money_cls {
          width: 95%;
          font-size: xx-large;
      }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <asp:Label ID="lblPageTitle" runat="server" Text="Donors"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            var today = new Date().toISOString().split('T')[0];
            console.log(today)
            document.getElementById('MainContent_startDate').setAttribute('min', today);
            document.getElementById('startDateContainer').style.display = "block";
            toggleDateInput();

        });
        function toggleActiveClass() {
            var labels = document.querySelectorAll('label[data-kt-button="true"]');
            console.log("Labels" + labels)
            labels.forEach(function (label) {
                label.classList.remove('active');
            });

            var selectedRadio = document.querySelector('input[name="ctl00$MainContent$discount_option"]:checked');
            if (selectedRadio) {
                selectedRadio.closest('label').classList.add('active');
            }
        }

        document.addEventListener('DOMContentLoaded', function () {
            toggleActiveClass(); // Set the initial active class
            var radioOneTime = document.getElementById('MainContent_chkRecurring');
            var radioRecurring = document.getElementById('MainContent_chkonetime2');
            console.log(radioOneTime)
            if (radioOneTime) {
                radioOneTime.addEventListener('click', toggleActiveClass, toggleDateInput);
                radioOneTime.addEventListener('click', toggleDateInput);

            }
            if (radioRecurring) {
                radioRecurring.addEventListener('click', toggleActiveClass, toggleDateInput);
                radioRecurring.addEventListener('click', toggleDateInput);

            }
        });
        function toggleDateInput() {
            var isRecurring = document.getElementById('MainContent_chkRecurring').checked;
            var dateInput = document.getElementById('MainContent_startDate');
            var dateLabel = document.getElementById('lblStartDate');
            console.log(dateInput)
            if (isRecurring) {
                dateInput.style.display = 'block';
                dateLabel.style.display = 'block';
            } else {
                dateInput.style.display = 'none';
                dateLabel.style.display = 'none';
            }
        }




    </script>

  

    <div class="row mb-6">
        <asp:Label ID="lblTitleSub" runat="server" Text="How often would you like to give?" Font-Bold="true" Font-Size="22px"></asp:Label>
    </div>

    <div class="row" style="min-height: 300px">
        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-1 row-cols-xl-2 g-9 mb-6">
            <!--begin::Col-->
            <div class="col">
                <!--begin::Option-->
                <label class="btn btn-outline btn-outline-dashed btn-outline-default d-flex text-start p-6 active" data-kt-button="true">
                    <!--begin::Radio-->
                    <span class="form-check form-check-custom form-check-solid form-check-sm align-items-start mt-1">
                        <input class="form-check-input" type="radio" name="discount_option" value="1" runat="server" id="chkonetime2" />
                    </span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <!--end::Radio-->
                    <!--begin::Info-->
                    <span class="ms-5">
                        <span class="fs-4 fw-bolder text-gray-800 d-block">One Time</span>
                    </span>
                    <!--end::Info-->
                </label>
                <!--end::Option-->
            </div>
            <!--end::Col-->
            <!--begin::Col-->
            <div class="col">
                <!--begin::Option-->
                <label class="btn btn-outline btn-outline-dashed btn-outline-default d-flex text-start p-6" data-kt-button="true">
                    <!--begin::Radio-->
                    <span class="form-check form-check-custom form-check-solid form-check-sm align-items-start mt-1">
                        <input class="form-check-input" type="radio" name="discount_option" value="2" runat="server" id="chkRecurring" />
                    </span>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <!--end::Radio-->
                    <!--begin::Info-->
                    <span class="ms-5">
                        <span class="fs-4 fw-bolder text-gray-800 d-block">Recurring</span>
                    </span>
                    <!--end::Info-->
                </label>
                <!--end::Option-->

                <div class="row mb-6" id="startDateContainer" style="margin: 10px">
                    <label id="lblStartDate" class="form-label">Select Start Date:</label>
                    <input type="date" id="startDate" class="form-control" runat="server" />
                </div>
            </div>
            <!--end::Col-->
        </div>
    </div>


    <div class="card-footer mb-10">
        <div class="d-flex justify-content-around">

            <button type="button" class="btn btn-primary" style="width: 120px; height: 60px">Back</button>
            <asp:Button ID="btnPayNow" runat="server" Text="Pay Now" CssClass="btn btn-primary" OnClick="btnPayNow_Click" Style="width: 120px; height: 60px" />


        </div>
    </div>











</asp:Content>
