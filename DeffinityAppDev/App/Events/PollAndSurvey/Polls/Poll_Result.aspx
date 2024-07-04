<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Poll_Result.aspx.cs" Inherits="DeffinityAppDev.App.PollAndSurvey.Polls.Poll_Result" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <style>
table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

td, th {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}

tr:nth-child(even) {


  background-color: #dddddd;
}
</style>








     <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" role="button"  data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
           <%-- <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Poll Result    </h3>
            </div>--%>
            <!--end::Card title-->
        </div>
        <!--begin::Card header-->
        <!--begin::Content-->
        <div id="kt_account_profile_details" class="collapse show" style="">
            <!--begin::Form-->
            <form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
                <!--begin::Card body-->
               
                    <!--begin::Input group-->









                    <div class="row g-6">


                      

                      
                        
                        <center>

                        <div class="col-lg-8">



                            <div class="card card-stretch card-bordered mb-5">

                                <div class="card-header">



                                    <div class="d-flex flex-column flex-row-fluid">
                                        <div class="d-flex flex-column-auto h-70px  flex-center">
                                            <span class="text-white">
                                                <h1><asp:Label ID="lblTitlte" runat="server"></asp:Label> - Poll Result  </h1>
                                            </span>
                                        </div>


                                    </div>




                                </div>

                                <center>

                                <div class="card-body">

                                    <div class="row justify-content-center">

                                     <div id="barchart" class="d-flex justify-content-center d-inline" style="height: 500px; width: 100%;"></div>
                       <asp:HiddenField ID="hunid" runat="server" ClientIDMode="Static" />
                                   


                                        <asp:Repeater runat="server" ID="rptPollCountGrid" Visible="false">
                                            <HeaderTemplate>
                                                <table width="400">
                                                    <thead>
                                                        <th>Options
                                                        </th>
                                                        <th> Count
                                                        </th>
                                                    </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td    >
                                                        <asp:Label ID="lblOptions" Text='<%# Eval("value") %>' runat="server" Width="100" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCount" Text='<%# Eval("count") %>' runat="server" Width="100" /> (  <asp:Label ID="Label1" Text='<%# Eval("percent","{0:F2}") %>' runat="server" /> %)
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>

                                        </asp:Repeater>


                                        </div>
                                    

                                 


                                </div>

                                </center>
                       

                    </div>





                            </div>




                </div>
                <!--end::Card body-->
                <!--begin::Actions-->
                <div class=" d-flex  py-6 px-9">
                
                </div>
                <!--end::Actions-->

            </form>
            <!--end::Form-->
        </div>
        <!--end::Content-->
    </div>




      <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
                  <script type="text/javascript">
        //google.load("visualization", "1", { packages: ["corechart"] });
        //google.setOnLoadCallback(drawChart);
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChartBar);
        function drawChartBar() {
            var options = {
                title: '',
                width: 500,
                height: 500,
                bar: { groupWidth: "95%" },
                legend: { position: "none" },
                isStacked: true,
                colors: ['#7239ea']
            };
            debugger;
            var d1 = {
                'unid': $('#hunid').val()
               
            };

            
            $.ajax({
                type: "POST",
                url: "../../../../app/chartdata.asmx/GetPollData",
                data: JSON.stringify(d1),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.ColumnChart($("#barchart")[0]);
                    chart.draw(data, options);
                },
                failure: function (r) {
                    console.log(r.d);
                },
                error: function (r) {
                    console.log(r.d);
                }
            });
        }
                  </script>

    

</asp:Content>
