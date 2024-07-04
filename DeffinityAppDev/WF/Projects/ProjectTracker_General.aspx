<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectTracker_General" Codebehind="ProjectTracker_General.aspx.cs" %>
<%@ Register Src="controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/Project_FinancialSubtab.ascx" TagName="Project_FinalcialSubtab"
    TagPrefix="uc2" %>
 <%@ Register Src="controls/PTGeneral.ascx" TagName="General" TagPrefix="uc3" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" Runat="Server">

 </asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc1:BuildProjectTabs ID="BuildProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      Project Tracker - <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="row"><div class="col-md-12"> 
    <div class="pull-right form-inline">
     <a href="pandldisplay.aspx?Project=<%=getProject%>" target="_blank" style="font-weight: bold;">
                        <%= Resources.DeffinityRes.ViewPandLaccount%>
                    </a>
      <asp:LinkButton ID="btnPandL" runat="server" Text="<%$ Resources:DeffinityRes,ViewPandLaccount%>"
                        OnClick="btnPandL_Click" Font-Bold="True" Visible="false"></asp:LinkButton>
    </div> </div>  </div>

   <uc2:Project_FinalcialSubtab ID="Project_FinalcialSubtab1" runat="server" />
    <br />
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
          <div id="DivCostOfMiscitemsInPT"></div>
	</div>
	<div class="col-md-4">
          <div id="DivCostOfMaterialsInPT"></div>
	</div>
	<div class="col-md-4">
           <div id="DivVariationSummary"></div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
          <div id="DivExpenseTrackerInPt"></div>
	</div>
	<div class="col-md-4">
           <div id="DivLabourInPT"></div>
	</div>
	<div class="col-md-4">
          
	</div>
</div>
     <div>
                  
                </div>
                <br />
     <uc3:General ID="General1" runat="server"/>
    
</asp:Content>

<asp:Content ID="Content9" ContentPlaceHolderID="Scripts_Section" runat="Server">
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
                          s = 'error';  //alert("Error loading data.Please try again.");
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
                          //alert("Error loading data.Please try again.");
                          s = "error";
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
                          //alert("Error loading data.Please try again.");
                          s = "error";
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
                      url: 'ProjectTracker_General.aspx/CostOfMiscitemsData',
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
      new google.visualization.BarChart(document.getElementById('DivCostOfMiscitemsInPT')).draw(data2, options2);
  },
                      error: function () {
                          //alert("Error loading data.Please try again.");
                          s = "error";
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
                      url: 'ProjectTracker_General.aspx/CostOfMaterialsData',
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
      new google.visualization.BarChart(document.getElementById('DivCostOfMaterialsInPT')).draw(data2, options2);
  },
                      error: function () {
                          //alert("Error loading data.Please try again.");
                          s = "error";
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
                      url: 'ProjectTracker_General.aspx/CostOfLabourData',
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
      new google.visualization.BarChart(document.getElementById('DivLabourInPT')).draw(data2, options2);
  },
                      error: function () {
                          //alert("Error loading data.Please try again.");
                          s = "error";
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
                          //alert("Error loading data.Please try again.");
                          s = "error";
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
                      url: 'ProjectTracker_General.aspx/ExpenseTrackerData',
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
      new google.visualization.BarChart(document.getElementById('DivExpenseTrackerInPt')).draw(data2, options2);
  },
                      error: function () {
                          //alert("Error loading data.Please try again.");
                          s = "error";
                      }
                  });
              })
          }

      </script>
</asp:Content>



