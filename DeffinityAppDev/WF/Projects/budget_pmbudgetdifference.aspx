<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="budget_pmbudgetdifference" EnableEventValidation="false" Codebehind="budget_pmbudgetdifference.aspx.cs" %>

<%@ Register Src="controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/Project_BudgetSubTab.ascx" TagName="BudgetTab" TagPrefix="uc4"%>
<%@ Register Src="controls/ProjectCost.ascx" TagName="ProjectCost" TagPrefix="uc2" %>
<%--<%@ Register Src="~/WF/Projects/Controls/BudgetSavingGrid.ascx" TagName="Saving" TagPrefix="uc3" %>--%>
<%@ Register Src="controls/SavingGrids.ascx" TagName="Saving" TagPrefix="uc5" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
         <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
 </asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectSavings%>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <script type="text/javascript" src="https://www.google.com/jsapi"></script>
      <style type="text/css">
          .roundViolet {
              border: 0px solid Silver;
              padding: 5px 5px;
              background: #DBE3F7;
              border-radius: 8px;
          }
   </style>

      <script type="text/javascript">
          google.load("visualization", "1.1", { packages: ["bar"] });
          google.load('visualization', '1.1', { packages: ['corechart'] });
          google.setOnLoadCallback(DrawOverallProjectFinancialHealth);

          google.setOnLoadCallback(DrawPMHours);
          google.setOnLoadCallback(DrawStaffhours);
          google.setOnLoadCallback(DrawCostOfMiscitems);
          google.setOnLoadCallback(DrawCostOfMaterials);
          google.setOnLoadCallback(DrawCostOfLabour);
          google.setOnLoadCallback(DrawVariationSummary);
          google.setOnLoadCallback(DrawDExpenseTracker);



          function GetParameterValues(param) {  
              var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');  
              for (var i = 0; i < url.length; i++) {  
                  var urlparam = url[i].split('=');  
                  if (urlparam[0] == param) {  
                      return urlparam[1];  
                  }  
              }  
          }
          var pid = GetParameterValues('project');
          function DrawOverallProjectFinancialHealth() {
              $(function () {
                  $.ajax({
                      type: 'POST',
                      dataType: 'json',
                      contentType: 'application/json',
                      url: 'budget_pmbudgetdifference.aspx/OverallProjectFinancialHealthData',
                      data: "{pid:'" + pid + "'}",
                      success:
  function (response) {
      var data1 = response.d;
      var data2 = new google.visualization.DataTable();
      data2.addColumn('string', 'Column Name');
      data2.addColumn('number', 'Value');
      data2.addColumn({ type: 'string', role: 'style' });
      for (var i = 0; i < data1.length; i++) {
          data2.addRow([data1[i].Name, data1[i].Value, data1[i].ColorName]);
      }
   
      var options2 = {
          title: "Overall Project Financial Health",
          legend: { position: "none" },
          chartArea: { width: '50%' },
          hAxis: {
              minValue: 0
          },
          vAxis: {
          }
      };
      new google.visualization.BarChart(document.getElementById('DivOverallProjectFinancialHealth')).draw(data2, options2);
  },
                      error: function () {
                          var s = "error";
                          //alert("Error loading data.Please try again.");
                      }
                  });
              })
          }
          function DrawPMHours() {
              $(function () {
                  $.ajax({
                      type: 'POST',
                      dataType: 'json',
                      contentType: 'application/json',
                      url: 'budget_pmbudgetdifference.aspx/PMHoursData',
                      data: "{pid:'" + pid + "'}",
                      success:
  function (response) {
      var data1 = response.d;
      var data2 = new google.visualization.DataTable();
      data2.addColumn('string', 'Column Name');
      data2.addColumn('number', 'Value');
      data2.addColumn({ type: 'string', role: 'style' });
      for (var i = 0; i < data1.length; i++) {
          data2.addRow([data1[i].Name, data1[i].Value, data1[i].ColorName]);
      }
      var options2 = {
          title: "PM Hours",
          chartArea: { width: '80%' },
          legend: { position: "none" },
          hAxis: {
              minValue: 0
          },
          vAxis: {
          }
         // colors: ['red','green']
      };
      new google.visualization.BarChart(document.getElementById('DivPMHours')).draw(data2, options2);
      var formatter = new google.visualization.ColorFormat();
      formatter.addRange(-20000, 0, 'white', 'orange');
  },
                      error: function () {
                          var s = "error";
                         // alert("Error loading data.Please try again.");
                      }
                  });
              })
          }
          function DrawStaffhours() {
              $(function () {
                  $.ajax({
                      type: 'POST',
                      dataType: 'json',
                      contentType: 'application/json',
                      url: 'budget_pmbudgetdifference.aspx/StaffhoursData',
                      data: "{pid:'" + pid + "'}",
                      success:
  function (response) {
      var data1 = response.d;
      var data2 = new google.visualization.DataTable();
      data2.addColumn('string', 'Column Name');
      data2.addColumn('number', 'Value');
      data2.addColumn({ type: 'string', role: 'style' });
      for (var i = 0; i < data1.length; i++) {
          data2.addRow([data1[i].Name, data1[i].Value, data1[i].ColorName]);
      }
      var options2 = {
          title: "Staff Hours",
          chartArea: { width: '80%' },
          legend: { position: "none" },
          hAxis: {
              minValue: 0
          },
          vAxis: {
          }
      };
      new google.visualization.BarChart(document.getElementById('DivStaffhours')).draw(data2, options2);
  },
                      error: function () {
                          var s = "error";
                          //alert("Error loading data.Please try again.");
                      }
                  });
              })
          }
          function DrawCostOfMiscitems() {
              $(function () {
                  $.ajax({
                      type: 'POST',
                      dataType: 'json',
                      contentType: 'application/json',
                      url: 'budget_pmbudgetdifference.aspx/CostOfMiscitemsData',
                      data: "{pid:'" + pid + "'}",
                      success:
  function (response) {
      var data1 = response.d;
      var data2 = new google.visualization.DataTable();
      data2.addColumn('string', 'Column Name');
      data2.addColumn('number', 'Value');
      data2.addColumn({ type: 'string', role: 'style' });
      for (var i = 0; i < data1.length; i++) {
          data2.addRow([data1[i].Name, data1[i].Value, data1[i].ColorName]);
      }

      var options2 = {
          title: "Cost of Miscellaneous items",
          chartArea: { width: '80%' },
          legend: { position: "none" },
          hAxis: {
              minValue: 0
          },
          vAxis: {
          }
      };
      new google.visualization.BarChart(document.getElementById('DivCostOfMiscitems')).draw(data2, options2);
  },
                      error: function () {
                          var s = "error";
                          //alert("Error loading data.Please try again.");
                      }
                  });
              })
          }
          function DrawCostOfMaterials() {
              $(function () {
                  $.ajax({
                      type: 'POST',
                      dataType: 'json',
                      contentType: 'application/json',
                      url: 'budget_pmbudgetdifference.aspx/CostOfMaterialsData',
                      data: "{pid:'" + pid + "'}",
                      success:
  function (response) {
      var data1 = response.d;
      var data2 = new google.visualization.DataTable();
      data2.addColumn('string', 'Column Name');
      data2.addColumn('number', 'Value');
      data2.addColumn({ type: 'string', role: 'style' });
      for (var i = 0; i < data1.length; i++) {
          data2.addRow([data1[i].Name, data1[i].Value, data1[i].ColorName]);
      }
      var options2 = {
          title: "Cost of Materials",
          chartArea: { width: '80%' },
          legend: { position: "none" },
          hAxis: {
              minValue: 0
          },
          vAxis: {
          }
      };
      new google.visualization.BarChart(document.getElementById('DivCostOfMaterials')).draw(data2, options2);
  },
                      error: function () {
                          var s = "error";
                          //alert("Error loading data.Please try again.");
                      }
                  });
              })
          }
          function DrawCostOfLabour() {
              $(function () {
                  $.ajax({
                      type: 'POST',
                      dataType: 'json',
                      contentType: 'application/json',
                      url: 'budget_pmbudgetdifference.aspx/CostOfLabourData',
                      data: "{pid:'" + pid + "'}",
                      success:
  function (response) {
      var data1 = response.d;
      var data2 = new google.visualization.DataTable();
      data2.addColumn('string', 'Column Name');
      data2.addColumn('number', 'Value');
      data2.addColumn({ type: 'string', role: 'style' });
      for (var i = 0; i < data1.length; i++) {
          data2.addRow([data1[i].Name, data1[i].Value, data1[i].ColorName]);
      }
      var options2 = {
          title: "Cost of Labour",
          chartArea: { width: '80%' },
          legend: { position: "none" },
          hAxis: {
              minValue: 0
          },
          vAxis: {
          }
      };
      new google.visualization.BarChart(document.getElementById('DivLabour')).draw(data2, options2);
  },
                      error: function () {
                          var s = "error";
                          //alert("Error loading data.Please try again.");
                      }
                  });
              })
          }
          function DrawVariationSummary() {
              $(function () {
                  $.ajax({
                      type: 'POST',
                      dataType: 'json',
                      contentType: 'application/json',
                      url: 'budget_pmbudgetdifference.aspx/VariationSummaryData',
                      data: "{pid:'" + pid + "'}",
                      success:
  function (response) {
      var data1 = response.d;
      var data2 = new google.visualization.DataTable();
      data2.addColumn('string', 'Column Name');
      data2.addColumn('number', 'Value');
      data2.addColumn({ type: 'string', role: 'style' });
      for (var i = 0; i < data1.length; i++) {
          data2.addRow([data1[i].Name, data1[i].Value, data1[i].ColorName]);
      }

      var options2 = {
          title: "Variation Summary",
          chartArea: { width: '50%' },
          legend: { position: "none" },
          hAxis: {
              minValue: 0
          },
          vAxis: {
          }
      };
      new google.visualization.BarChart(document.getElementById('DivVariationSummary')).draw(data2, options2);
  },
                      error: function () {
                          var s = "error";
                          //alert("Error loading data.Please try again.");
                      }
                  });
              })
          }
          function DrawDExpenseTracker() {
              $(function () {
                  $.ajax({
                      type: 'POST',
                      dataType: 'json',
                      contentType: 'application/json',
                      url: 'budget_pmbudgetdifference.aspx/ExpenseTrackerData',
                      data: "{pid:'" + pid + "'}",
                      success:
  function (response) {
      var data1 = response.d;
      var data2 = new google.visualization.DataTable();
      data2.addColumn('string', 'Column Name');
      data2.addColumn('number', 'Value');
      data2.addColumn({ type: 'string', role: 'style' });
      for (var i = 0; i < data1.length; i++) {
          data2.addRow([data1[i].Name, data1[i].Value, data1[i].ColorName]);
      }

      var options2 = {
          title: "Expense Tracker",
          chartArea: { width: '80%' },
          legend: { position: "none" },
          hAxis: {
              minValue: 0
          },
          vAxis: {
          }
      };
      new google.visualization.BarChart(document.getElementById('DivExpenseTracker')).draw(data2, options2);
  },
                      error: function () {
                          var s = "error";
                          //alert("Error loading data.Please try again.");
                      }
                  });
              })
          }

      </script>
     

        <div>
                        <uc4:BudgetTab ID="BudgetTab1" runat="server" />
              <div class="form-group">
                        <div class="col-md-4">
                                <div id="DivOverallProjectFinancialHealth"></div>
                        </div>
	                    <div class="col-md-4">
                                <div id="DivPMHours"></div>
	                    </div>
	                    <div class="col-md-4">
                                <div id="DivStaffhours"></div>
                        </div>         
             </div>
            <div class="form-group">
                       <div class="col-md-4">
                             <div id="DivCostOfMiscitems"></div>
                       </div>
           	            <div class="col-md-4">
                              <div id="DivCostOfMaterials"></div>
	                    </div>
	                    <div class="col-md-4">
                             <div id="DivVariationSummary"></div>
                       	</div>
            </div>
            <div class="form-group">
                      <div class="col-md-4">
                          <div id="DivExpenseTracker"></div>
                      </div>
                	<div class="col-md-4">
                          <div id="DivLabour"></div>
                	</div>
            </div>
                 <br />
                  <div class="clr"></div>
                 <div>
                     <uc5:Saving ID="sgrid" runat="server"></uc5:Saving>
                 </div>
              <div>
           </div>
      </div>
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
     //grid_responsive();
     grid_responsive_display();
     $(window).load(function () {
         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });
     });
    </script> 
</asp:Content>

