<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Budget_General" EnableEventValidation="false" Codebehind="Budget_General.aspx.cs" %>

<%@ Register Src="controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/Project_BudgetSubTab.ascx" TagName="BudgetTab" TagPrefix="uc4"%>
<%@ Register Src="controls/ProjectCost.ascx" TagName="ProjectCost" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
       <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.General%>
    </asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <uc4:BudgetTab ID="BudgetTab1" runat="server" />
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <style>
      .roundViolet
    {
        border: 0px solid Silver;
        padding: 5px 5px;
        background: #DBE3F7;
        border-radius: 8px;
    }
   </style>
     <script type="text/javascript">
          google.load("visualization", "1.1", { packages: ["bar"] });
          google.load('visualization', '1.1', { packages: ['corechart'] });

          google.setOnLoadCallback(DrawPieChart);
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
          function DrawPieChart() {
              $(function () {
                  $.ajax({
                      type: 'POST',
                      dataType: 'json',
                      contentType: 'application/json',
                      url: 'Budget_General.aspx/GetPieChartData',
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
          title: "",
          legend: { position: "bottom" }
      };
      new google.visualization.PieChart(document.getElementById('PieChat')).draw(data2, options2);
  },
                      error: function () {
                          alert("Error loading data.Please try again.");
                      }
                  });
              })
          }
        </script>
          <div class="form-group">
               <div class="col-md-12">
                <uc2:ProjectCost ID="ProjectCost1" runat="server" />
                   </div>
          </div>
           
         <div class="form-group">
              <div class="col-md-5 well">
                      <div class="form-group">
          <div class="col-md-12">
          <p><strong><%= Resources.DeffinityRes.OverallprojectCostSummary%></strong> </p>
     	</div>
                         
     </div>
                   <hr />
                      <div class="form-group">
          <div class="col-md-12">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.BillofMaterials%></label>
           <div class="col-sm-3" style="text-align:right;">
                <asp:Label ID="lblBillofMaterialValue" runat="server"  Font-Bold="true"></asp:Label>
            </div>
	  </div>
</div>
                      <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.BillofLabour%></label>
            <div class="col-sm-3" style="text-align:right;">
                 <asp:Label ID="LblBillOfLabourValue" runat="server" Font-Bold="true"></asp:Label>
            </div>
     	</div>
     </div>
                      <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.Miscellaneous%></label>
           <div class="col-sm-3" style="text-align:right;">
               <asp:Label ID="lblMisCellValue" runat="server" Font-Bold="true"></asp:Label>
            </div>
     	</div>
     </div>
                      <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.InHouseHours%></label>
            <div class="col-sm-3" style="text-align:right;">
                <asp:Label ID="lblHouseHoursValue" runat="server" Font-Bold="true"></asp:Label>
            </div>
     	</div>
     </div>
                      <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.InHouseCost%></label>
              <div class="col-sm-3" style="text-align:right;">
                   <asp:Label ID="lblHouseCostValue" runat="server" Font-Bold="true"></asp:Label>
            </div>
     	</div>
     </div>
                      <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.Expenses%></label>
           <div class="col-sm-3" style="text-align:right;">
                <asp:Label ID="lblExpensesValue" runat="server" Font-Bold="true"></asp:Label>
            </div>
     	</div>
     </div>
                      <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.TotalProjectBudgetCost%></label>
           <div class="col-sm-3" style="text-align:right;">
                <asp:Label ID="LblTotalbudgetCost" runat="server" Font-Bold="true"></asp:Label>
            </div>
     	</div>
     </div>
               </div>
             <div class="col-md-7">
                  <div id="PieChat" style="width: 600px; height: 400px;vertical-align:top"></div>
             </div>
       </div>
</asp:Content>


