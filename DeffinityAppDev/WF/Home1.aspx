<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="Home1.aspx.cs" Inherits="DeffinityAppDev.WF.Home1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <%= Resources.DeffinityRes.Time%>
    <div class="row">
				<div class="col-sm-12">
				
					<div class="card shadow-sm">
						<div class="card-header">
							<h3 class="card-body">Grouped Bars</h3>
							<div class="card-toolbar">
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								<a href="#" data-toggle="remove">
									&times;
								</a>
							</div>
						</div>
						<div class="panel-body">	
							<script type="text/javascript">

							    function GetBarChart() {

							        $.ajax({
							            url: '<%=ResolveUrl("~/WF/chart1.asmx/GetBarChart") %>',
							            type: "POST",
							            data: "{}",
							            dataType: "json",
							            contentType: 'application/json; charset=utf-8',
							            async: true,
							            success: function (data) {

							                if (data.d != '') {
							                    var newData = jQuery.parseJSON(data.d);
							                    debugger;
							                    var ds = [];
							                    var firstCollection = newData.a;
							                    var seriesCollection = newData.b;
							                    debugger;
							                    $("#bar-2").dxChart({
							                        equalBarWidth: false,
							                        dataSource: firstCollection,
							                        commonSeriesSettings: {
							                            argumentField: "state",
							                            type: "bar"
							                        },
							                        series: seriesCollection,
							                        legend: {
							                            verticalAlignment: "bottom",
							                            horizontalAlignment: "center"
							                        },
							                        title: "Percent of Total Energy Production"
							                    });
							                }
							            }
							        });
							    }
							</script>
							<div id="bar-2" style="height: 400px; width: 100%;"></div>
						</div>
					</div>
						
				</div>
			</div>
    <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            GetBarChart();
            GetFormControl();
        });
        function GetFormControl() {
            
            $.ajax({
                url: '<%=ResolveUrl("~/WF/chart1.asmx/GetChart") %>',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);
                        debugger;
                        var ds = [];
                        //first collection
                        var firstCollection = newData;
                        //second collection
                        //var secondCollection = newData.b;
                        debugger;
                        for (var i = 0; i < firstCollection.length; i++) {
                            ds.push({
                                day: firstCollection[i].day,
                                sales: firstCollection[i].sales,
                                color: firstCollection[i].color
                            });
                        }

                        $("#bar-1").dxChart({
                            dataSource: newData,
                            series: {
                                argumentField: "day",
                                valueField: "sales",
                                name: "Sales",
                                type: "bar",
                                color: "color"
                               
                            },
                            palette: ['#8B7355', '#EE9A49', '#808000', '#A2CD5A', '#DEB887', '#87CEFA', '#BDBDBD']
                        });

                    }

                }

            });
           
        }
    </script>
    <div class="row">
				<div class="col-sm-12">
				
					<div class="card shadow-sm">
						<div class="card-header">
							<h3 class="card-body">Standard Bar</h3>
							<div class="card-toolbar">
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								<a href="#" data-toggle="remove">
									&times;
								</a>
							</div>
						</div>
						<div class="panel-body">	
                            <asp:HiddenField ID="hid" runat="server" ClientIDMode="Static" />
							<script type="text/javascript">
								jQuery(document).ready(function($)
								{
									$("#bar-1-randomize").on('click', function(ev)
									{
										ev.preventDefault();
										
										//$('#bar-1').dxChart('instance').option('dataSource', [
										//	{day: "Monday", sales: between(1,25)},
										//	{day: "Tuesday", sales: between(1,25)},
										//	{day: "Wednesday", sales: between(1,25)},
										//	{day: "Thursday", sales: between(1,25)},
										//	{day: "Friday", sales: between(1,25)},
										//	{day: "Saturday", sales: between(1,25)},
										//	{day: "Sunday", sales: between(1,25)} 
									    //]);
										
									});
								});
								
								function between(randNumMin, randNumMax)
								{
									var randInt = Math.floor((Math.random() * ((randNumMax + 1) - randNumMin)) + randNumMin);
									
									return randInt;
								}
							</script>
							<div id="bar-1" style="height: 440px; width: 100%;"></div>
							<br />
							<a href="#" id="bar-1-randomize" class="btn btn-primary btn-small">Randomize</a>
						</div>
					</div>
						
				</div>
			</div>
    
	<!-- Imported scripts on this page -->
    <script src='<%:ResolveClientUrl("~/Content/assets/js/devexpress-web-14.1/js/globalize.min.js")%>'></script>
     <script src='<%:ResolveClientUrl("~/Content/assets/js/devexpress-web-14.1/js/dx.chartjs.js")%>'></script>
	<!-- JavaScripts initializations and stuff -->
	<%--<script src="assets/js/xenon-custom.js"></script>--%>
    <!-- Bottom Scripts -->
	<%--<script src="assets/js/bootstrap.min.js"></script>
	<script src="assets/js/TweenMax.min.js"></script>
	<script src="assets/js/resizeable.js"></script>
	<script src="assets/js/joinable.js"></script>
	<script src="assets/js/xenon-api.js"></script>
	<script src="assets/js/xenon-toggles.js"></script>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
   
</asp:Content>
