<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="DeffinityAppDev.WF.Projects.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
    <div style="color:green;"><i class="fa fa-circle"></i></div>
     <div style="color:red"><i class="fa fa-circle"></i></div>
     <div style="color:yellow"><i class="fa fa-circle"></i></div>
    <asp:Label runat="server" style="color:green;"><i class="fa fa-circle"></i></asp:Label>
    <asp:Label runat="server" style="color:red;"><i class="fa fa-circle"></i></asp:Label>
    <asp:Label runat="server" style="color:yellow;"><i class="fa fa-circle"></i></asp:Label>

    <asp:LinkButton runat="server" style="color:green;"><i class="fa fa-circle"></i></asp:LinkButton>
    <asp:LinkButton runat="server" style="color:red;"><i class="fa fa-circle"></i></asp:LinkButton>
    <asp:LinkButton runat="server" style="color:grey;"><i class="fa fa-circle"></i></asp:LinkButton>
    <asp:HyperLink ID="ss" SkinID="LinkThumbsUpGreen" runat="Server" style="color:green;" Text="" ><i class="fa el-thumbs-up"></i>test</asp:HyperLink>
<%--
    <div class="panel panel-default">
				<div class="panel-heading">
					<h3 class="panel-title">Removing search and results count filter</h3>
					
					<div class="panel-options">
						<a href="#" data-toggle="panel">
							<span class="collapse-icon">&ndash;</span>
							<span class="expand-icon">+</span>
						</a>
						<a href="#" data-toggle="remove">
							&times;
						</a>
					</div>
				</div>
				<div class="panel-body" >
					
					<script type="text/javascript">
					    jQuery(document).ready(function ($) {
					        $("#example-2").dataTable({
					            dom: "t" + "<'row'<'col-xs-6'i><'col-xs-6'p>>",
					            aoColumns: [
                                    { bSortable: false },
                                    null,
                                    null,
                                    null,
                                    null,
                                     null,
                                    null,
                                    null,
                                     null,
                                    null,
                                    null,
                                    null,
                                     null,
                                    null,
                                    null,
                                    null,
                                    null,
                                    null,
                                    null,
                                    null
					            ],
					        });

					        // Replace checkboxes when they appear
					        //var $state = $("#example-2 thead input[type='checkbox']");

					        //$("#example-2").on('draw.dt', function () {
					        //    cbr_replace();

					        //    $state.trigger('change');
					        //});

					        //// Script to select all checkboxes
					        //$state.on('change', function (ev) {
					        //    var $chcks = $("#example-2 tbody input[type='checkbox']");

					        //    if ($state.is(':checked')) {
					        //        $chcks.prop('checked', true).trigger('change');
					        //    }
					        //    else {
					        //        $chcks.prop('checked', false).trigger('change');
					        //    }
					        //});
					  });
					</script>
					<div style="overflow:scroll;">
					<table class="table table-bordered table-striped" id="example-2" style="width:100%">
						<thead>
							<tr>
								<th class="no-sorting">
									<input type="checkbox" class="cbr">
								</th>
								<th>Student Name</th>
								<th>Average Grade</th>
								<th>Curriculum / Occupation</th>
								<th>Actions</th>
                               <th>Student Name</th>
								<th>Average Grade</th>
								<th>Curriculum / Occupation</th>
								 <th>Student Name</th>
								<th>Average Grade</th>
								<th>Curriculum / Occupation</th>
                                 <th>Student Name</th>
								<th>Average Grade</th>
								<th>Curriculum / Occupation</th>
                                <th>Student Name</th>
								<th>Average Grade</th>
								<th>Curriculum / Occupation</th>
                                <th>Student Name</th>
								<th>Average Grade</th>
								<th>Curriculum / Occupation</th>
							</tr>
						</thead>
						
						<tbody class="middle-align">
						
							<tr>
								<td>
									<input type="checkbox" class="cbr">
								</td>
								<td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
								<td>
									<a href="#" class="btn btn-secondary btn-sm btn-icon icon-left">
										Edit
									</a>
									
									<a href="#" class="btn btn-danger btn-sm btn-icon icon-left">
										Delete
									</a>
									
									<a href="#" class="btn btn-info btn-sm btn-icon icon-left">
										Profile
									</a>
								</td>
                              <td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
                                 <td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
                                 <td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
                                 <td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
                                 <td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
							</tr>
							
							<tr>
								<td>
									<input type="checkbox" class="cbr">
								</td>
								<td>Ellen C. Jones</td>
								<td>7.2</td>
								<td>Education and development manager</td>
								<td>
									<a href="#" class="btn btn-secondary btn-sm btn-icon icon-left">
										Edit
									</a>
									
									<a href="#" class="btn btn-danger btn-sm btn-icon icon-left">
										Delete
									</a>
									
									<a href="#" class="btn btn-info btn-sm btn-icon icon-left">
										Profile
									</a>
								</td>
                               <td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
								 <td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
                                 <td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
                                 <td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
                                 <td>Randy S. Smith</td>
								<td>8.7</td>
								<td>Social and human service</td>
							</tr>
							
							
						</tbody>
					</table>
					</div>
				</div>
			</div>

    
	<!-- Imported styles on this page -->
	<link rel="stylesheet" href="../../Content/assets/js/datatables/dataTables.bootstrap.css">

    <script src="../../Content/assets/js/datatables/js/jquery.dataTables.min.js"></script>
	<!-- Imported scripts on this page -->
	<script src="../../Content/assets/js/datatables/dataTables.bootstrap.js"></script>
	<script src="../../Content/assets/js/datatables/yadcf/jquery.dataTables.yadcf.js"></script>
	<script src="../../Content/assets/js/datatables/tabletools/dataTables.tableTools.min.js"></script>
--%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
