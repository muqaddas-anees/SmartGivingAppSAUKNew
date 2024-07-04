<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Survey_Result.aspx.cs" Inherits="DeffinityAppDev.App.PollAndSurvey.Survey.Survey_Result" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">

    <style>
        .modalBackground {
            overflow : scroll;
           
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }



          select {
            width: 390px;
            height: 45px;
            background-color: #F5F8FA;
            /*border-collapse:collapse;*/
            border-radius: 5px;
            border-color: #F5F8FA;
        }

    </style>

    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
      <div class="card-header">



                                    <div class="d-flex flex-column flex-row-fluid">
                                        <div class="d-flex flex-column-auto h-70px  flex-center">
                                            <span class="text-white">
                                                <h1>Survey  Result  </h1>
                                            </span>
                                        </div>


                                    </div>




                                </div>
       <div class="card-body">
                                    <asp:HiddenField ID="hid" runat="server" ClientIDMode="Static" />
                                      <asp:ListView ID="listview" runat="server">
                                        <LayoutTemplate>
                                             <div class="row justify-content-center">
                                                  <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                                                 </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <div class="col-lg-12">

                                                 <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>' Font-Size="22px"></asp:Label> 
                                            </div>
                                            <div class="row justify-content-center">

                                     <div  id='<%# "barchart" + Eval("QuestionID").ToString() %>' class="d-flex justify-content-center d-inline" style="height: 300px; width: 100%;"></div>

                                                </div>
                                        </ItemTemplate>
                                    </asp:ListView>



                                    <div>


                                        <table style="width: 90%; display:none;" >

                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblQuestion" Font-Size="Large" runat="server" /><br />
                                                    <br />
                                                </th>
                                            </tr>

                                            <tr>
                                                <td>
                                               <h3> Total Questions :   <asp:Label runat="server" ID="lblTotalQuestions"></asp:Label></h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                  <h3>  Correct Answers : <asp:Label runat="server" ID="lblCorrectAnswers"></asp:Label></h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   
                                               <h3>      <asp:Label ID="Label1" Font-Size="Large" runat="server" />  of   <asp:Label ID="Label2" Font-Size="Large" runat="server" />   </h3>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                <h3>    Result Persentage :   <asp:Label ID="lblResult" Font-Size="Large" runat="server" />     </h3> 
                                                </td>
                                            </tr>

                                         

                                            <tr>
                                                <td>

                                                    <div class=" d-flex justify-content-end py-6 px-9">


                                                       <asp:Button runat="server" ID="btnBack"  Text="Go Back" OnClick="btnBack_Click" />
                                                        
                                                    </div>

                                                </td>
                                            </tr>



                                        </table>


                                    </div>
















                                </div>
        <!--end::Content-->
    </div>

















    









     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
                  <script type="text/javascript">
                      //google.load("visualization", "1", { packages: ["corechart"] });
                      //google.setOnLoadCallback(drawChart);
                     
                      var plist = $('#hid').val();

                      var parray = plist.split(",");

                      for (const element of parray) { // You can use `let` instead of `const` if you like
                          console.log(element);

                          if (element != "") {
                              google.charts.load('current', { 'packages': ['corechart'] });
                              google.charts.setOnLoadCallback(function () {
                                  drawChartBar(element);

                              });
                          }
                      }

                      function drawChartBar(questionid) {
                          var options = {
                              title: '',
                              width: 500,
                              height: 300,
                              bar: { groupWidth: "95%" },
                              legend: { position: "none" },
                              isStacked: true,
                              colors: ['#7239ea']
                          };
                          debugger;
                          var d1 = {
                              'questionid': questionid

                          };


                          $.ajax({
                              type: "POST",
                              url: "../../../../app/chartdata.asmx/GetSurveyData",
                              data: JSON.stringify(d1),
                              contentType: "application/json; charset=utf-8",
                              dataType: "json",
                              success: function (r) {
                                  var data = google.visualization.arrayToDataTable(r.d);
                                  var chart = new google.visualization.ColumnChart($("#barchart" + questionid)[0]);
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