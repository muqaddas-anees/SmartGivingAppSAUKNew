<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMain.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DeffinityAppDev.WF.Portal.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="display:none;visibility:hidden;">
        <div class="col-sm-3" style="display:none;">			
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class="linecons-calendar"></i>
						</div>
						<div class="xe-label">
							<a style="font-size:large;" href="CustomerHome.aspx?customerid=0">Request Service</a>
						</div>
					</div>
					
				</div>	
        <div class="col-sm-12">
					<div class="xe-widget xe-counter xe-counter-blue" data-count=".num" data-from="1" data-to="117" data-suffix="k" data-duration="3" data-easing="false">
						<div class="xe-icon">
							<a style="cursor:pointer;"  href="../DC/FLSCustomer.aspx"> <i class="fa-wrench"></i> </a>
						</div>
						<div class="xe-label">
								<a style="font-size:large;" href="../DC/FLSCustomer.aspx">Request Service Repair</a>
						</div>
					</div>
				
				</div>
        <div class="col-sm-6" style="display:none;visibility:hidden">
					
					<div class="xe-widget xe-counter xe-counter-blue" data-count=".num" data-from="1" data-to="117" data-suffix="k" data-duration="3" data-easing="false">
						<div class="xe-icon">
							<i class="linecons-desktop"></i>
						</div>
						<div class="xe-label">
								<a style="font-size:large;" href="../DC/DCCustomerJlist.aspx?type=FLS">View Service Status</a>
						</div>
					</div>
				
				</div>
	    <div class="col-sm-3" style="display:none;">
					
					<div class="xe-widget xe-counter xe-counter-red"
                          data-count=".num" data-from="0" data-to="57" data-prefix="-," data-suffix="%" data-duration="5" data-easing="true" data-delay="1">
						<div class="xe-icon">
							<i class="linecons-paper-plane"></i>
						</div>
						<div class="xe-label">
								<a style="font-size:large;" href="CustomerDocs.aspx?customerid=0">Access Documents</a>
						</div>
					</div>
				
				</div>
   </div>
   <div class="row">
       <%-- <div class="col-md-6">
       <div class="card shadow-sm">
                <div class="card-header">
                     Home Warranty Coverage
                </div>
        <div class="panel-body">
                      <iframe src=" http://player.vimeo.com/video/182129576" height="330" width="500" > </iframe>
                       <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImage1" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                           <asp:GridView ID="grdIssues" runat="server" Width="100%" AutoGenerateColumns="false" PageSize="4"
                                                               EmptyDataText="No Records Found" AllowPaging="True" OnPageIndexChanging="grdIssues_PageIndexChanging" OnRowCommand="grdIssues_RowCommand">
                          <Columns>
                              <asp:TemplateField HeaderText="Project" Visible="false">
                                  <ItemTemplate>
                                       <asp:LinkButton ID="LinkButton1" runat="server"  Text='<%# Bind("ProjectReferenceWithPrefix")%>' Visible="false"
                                                                                  CommandName="View" CommandArgument='<%# Bind("IssueID")%>' ></asp:LinkButton>
                                      <asp:Label ID="LblId" runat="server" Text='<%# Bind("ProjectReferenceWithPrefix")%>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Reference">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname10" runat="server" Text='<%# Eval("ProjectReferenceWithPrefix")+"-"+Eval("IssueID") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Issue">
                                  <ItemTemplate>
                                      <asp:Label ID="lblIssue" runat="server" Text='<%# Bind("Issue") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Project Title" Visible="false">
                                  <ItemTemplate>
                                      <asp:Label ID="lblTitle" runat="server"
                                                         Text='<%# Bind("ProjectTitle")%>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>                                
                              <asp:TemplateField HeaderText="Status">
                                  <ItemTemplate>
                                      <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("ProjectStatusName") %>' ></asp:Label>
                                  </ItemTemplate>      
                              </asp:TemplateField>
                          
                          </Columns>
                      </asp:GridView>
                                    </ContentTemplate>
                                   </asp:UpdatePanel>
                  </div>
           </div>
       </div>
       <div class="col-md-6">
               <div class="card shadow-sm">
                    <div class="card-header">
                       Home Owner
                    </div>
                  <div class="panel-body">
                          <iframe src=" http://player.vimeo.com/video/167769544"  height="330" width="500"> </iframe>
                           <%-- <asp:UpdateProgress ID="up2" runat="server" AssociatedUpdatePanelID="Update2">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImage" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                               <asp:UpdatePanel ID="Update2" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                          <asp:GridView ID="grdAssignedTask" runat="server" Width="100%" AllowPaging="true" PageSize="4"
                                                     AutoGenerateColumns="false" EmptyDataText="No Records Found"
                               OnPageIndexChanging="grdAssignedTask_PageIndexChanging" OnRowCommand="grdAssignedTask_RowCommand">
                              <Columns>
                                  <asp:TemplateField HeaderText="Assigned Projects">
                                      <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server"  Text='<%# Bind("ProjectReferenceWithPrefix")%>' Visible="false"
                                                                        CommandName="View" CommandArgument='<%# Bind("ProjectReference")%>'></asp:LinkButton>
                                            <asp:Label ID="lblProject" runat="server"  Visible="true" Text='<%# Bind("ProjectReferenceWithPrefix")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Task">
                                      <ItemTemplate>
                                         <asp:Label ID="lblTask" runat="server" Text='<%# Bind("TaskTitle")%>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Status">
                                      <ItemTemplate>
                                          <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("ItemStatus") %>' Visible="false" ></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Project EndDate">
                                      <ItemTemplate>
                                          <asp:Label ID="lblStatus1" runat="server" Text='<%# Bind("ProjectEndDate","{0:d}") %>' ></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Notes" Visible="false">
                                      <ItemTemplate>
                                            <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                                       </ContentTemplate>
                                       </asp:UpdatePanel>
                      </div>
               </div>
       </div>--%>
       
      
       <div class="col-md-4" style="display:none;visibility:hidden;">
           <div class="card shadow-sm">
               <div class="card-header">
                   Latest documents
               </div>
              <div class="scrollable ps-container ps-active-y" data-max-height="350" style="max-height:450px;">
                  <div class="panel-body">
                     <%--  <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImage2" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                               <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>--%>
                      <asp:GridView ID="GridDocuments" runat="server" AutoGenerateColumns="false"  DataSourceID="sqlFileList" EmptyDataText="No documents found"
                                                                         ShowFooter="false" Width="100%" OnRowCommand="GridDocuments_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Document">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlDocumentDownload" runat="server" Text='<%#Bind("Dname") %>' ForeColor="#388787"
                                                                                          CommandName="Download" CommandArgument='<%#Bind("ID") %>'></asp:LinkButton>
                                                     <asp:Label ID="lblDname" runat="server" Text='<%#Bind("Dname") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Author">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAuthor" runat="server" Text='<%#Bind("U_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uploaded Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUploaded" runat="server" Text='<%#Bind("U_date","{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                      <%--  </ContentTemplate>
                                   </asp:UpdatePanel>--%>

                                    <asp:SqlDataSource ID="sqlFileList" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                                                 SelectCommandType="StoredProcedure" SelectCommand="Select_AC2P_DocumentsInCustomerPortal">
                                        <SelectParameters>
                                              <asp:SessionParameter DefaultValue="0" Name="CurrentUserID" Type="Int32" SessionField="UID" /> 
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                  </div>
               </div>
           </div>
       </div>
   </div>
    <div class="row">
       <div class="col-md-12">
           <div class="card shadow-sm">
                   <div class="card-header">
                      <%= Resources.DeffinityRes.ServiceDesk %>
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
                       <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                                  <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanelServices">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImageServices" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
                                  <asp:UpdatePanel ID="UpdatePanelServices" runat="server" UpdateMode="Conditional">
                                      <ContentTemplate>--%>
                             <asp:GridView ID="GridServices" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10"
                            Width="100%" 
                            EmptyDataText="<%$ Resources:DeffinityRes,NoRecordsExists%>" OnPageIndexChanging="GridServices_PageIndexChanging"
                                  OnRowCommand="GridServices_RowCommand" OnRowDataBound="GridServices_RowDataBound">
                               <Columns>
                                <asp:TemplateField HeaderText="Job Ref"  ItemStyle-Width="10%">
                                   <ItemTemplate>
                                         <asp:LinkButton ID="linkbtnTicketRef" Text='<%#Bind("CCID") %>' 
                                                                               CommandArgument='<%#Bind("CallID1") %>' CommandName="Url" runat="server"></asp:LinkButton>
                                       <asp:Label ID="lblPname1" runat="server" Text='<%#Bind("CallID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblCCID" runat="server" Text='<%#Bind("CCID") %>' Visible="false"></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Company" Visible="false">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname2" runat="server" Text='<%#Bind("Company") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Details">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname3" runat="server" Text='<%#Bind("Notes") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Category" Visible="false">
                                   <ItemTemplate>
                                       <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("Category") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Tech" ItemStyle-Width="17%">
                                   <ItemTemplate>
                                       <asp:Label ID="lblServiceProvider" runat="server" Text='<%#Bind("ServiceProvider") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type of Request" Visible="false">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname4" runat="server" Text='<%#Bind("TypeofRequest") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status"  ItemStyle-Width="10%">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname5" runat="server" Text='<%#Bind("Status") %>' CssClass="statuscls"  ForeColor="White" Font-Bold="true"></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Logged Date/Time" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="10%">
                                   <ItemTemplate>
                                       <asp:Label ID="lblPname6E" runat="server" Text='<%#Bind("LoggedDateTime") %>' style="float:right;"></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="10%">
                                   <ItemTemplate>
                                      <asp:Button ID="btnCancel" runat="server" Text="Cancel Request" SkinID="btnDefault" CommandArgument='<%#Bind("CallID1") %>' CommandName="CancelRequest" OnClientClick="return confirm('Do you want to cancel the request?');" />
                                   </ItemTemplate>
                               </asp:TemplateField>
                                     <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                   <ItemTemplate>
                                      <asp:Button ID="btnReschedule" runat="server" Text="Reschedule" CommandArgument='<%#Bind("CallID1") %>' CommandName="Reschedule" CssClass="btn btn-info" OnClientClick="return confirm('Do you want to reschedule the request?');"/>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               </Columns>
                           </asp:GridView>


                                         <%-- </ContentTemplate>
                                           <Triggers>                                       
                                           </Triggers>
                                      </asp:UpdatePanel>--%>
                             <script type="text/javascript">
                                 Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
                                 setStatusBackColor();
                                 function setStatusBackColor() {
                                    
                                         $('.statuscls').each(function () {
                                             
                                             var s = $(this).html();
                                             if (s == 'New')
                                                 $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Cancelled')
                                                 $(this).closest("td").css({ "background-color": "#44546a", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Resolved')
                                                 $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Job Complete')
                                                 $(this).closest("td").css({ "background-color": "#0070C0", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Scheduled')
                                                 $(this).closest("td").css({ "background-color": "#92D050", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Awaiting Schedule')
                                                 $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Arrived')
                                                 $(this).closest("td").css({ "background-color": "#57579D", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Customer Not Responding')
                                                 $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == ' Feedback Submitted')
                                                 $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Feedback Received')
                                                 $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Quote Rejected')
                                                 $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Quote Accepted')
                                                 $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Awaiting Information')
                                                 $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Waiting On Parts')
                                                 $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Authorised')
                                                 $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Quote Sent')
                                                 $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
                                         });
                                    
                                 }
</script>
                    </div>
               </div>
       </div>
   </div>

    <div class="row">
       <div class="col-md-12">
           <div class="card shadow-sm">
                   <div class="card-header">
                      My Profile
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


                       <asp:HiddenField ID="hcid" runat="server" Value="" ClientIDMode="Static" />
                         <asp:HiddenField ID="pstatus" runat="server" Value="" ClientIDMode="Static" />
             <asp:HiddenField ID="sid" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="selectedids" runat="server" Value="" ClientIDMode="Static" />
                            <asp:HiddenField ID="haid" runat="server" Value="0" ClientIDMode="Static" />
    <%--<button id="btn">btn</button>--%>
            <script type="text/javascript">
                function ShowModalPopup() {
                    $find("mpe").show();
                    return false;
                }
                function HideModalPopup() {
                    $find("mpe").hide();
                    return false;
                }
</script>
         
 <script type="text/javascript" language="javascript" class="init">
     function gethcid() {
         return $('#hcid').val();
     }
     function getUrlParameter(name) {
         name = name.toLowerCase().replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
         var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
         var results = regex.exec(location.search.toLowerCase());

         return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
     };

     var editor; // use a global for the submit and return data rendering in the examples

     $(document).ready(function () {

         if ($('#haid').val() == "0") {
             $('.pnl').hide();
         }

         $.fn.dataTable.moment('MM/DD/YYYY');
         editor = new $.fn.dataTable.Editor({
             ajax: "/api/ContactAdress",
             table: "#example",
             fields: [
                  {
                      label: "ContactID:",
                      name: "PortfolioContactAddress.ContactID",
                      type: "hidden",
                      def: gethcid()//getUrlParameter('ContactID')
                  },

                 {
                     label: "Address:",
                     name: "PortfolioContactAddress.Address"
                 },
                 {
                     label: "City:",
                     name: "PortfolioContactAddress.City"
                 },
                  {
                      label: "State:",
                      name: "PortfolioContactAddress.State"
                  },
                   {
                       label: "Post code:",
                       name: "PortfolioContactAddress.PostCode"
                   },
             //{
             //    label: "Policy type:",
             //    name: "PortfolioContactAddress.PolicyTypeID",
             //    type: "select",
             //    placeholder: "Please select..."
             //},
             //{
             //    label: "Policy number:",
             //    name: "PortfolioContactAddress.PolicyNumber"
             //},
             // {
             //     label: "Start date:",
             //     name: "PortfolioContactAddress.StartDate",
             //     type: 'datetime',
             //     def: function () {
             //         //return new Date();
             //         return moment(new Date()).format("MM/DD/YYYY")
             //     },
             //     dateFormat: "MM/DD/YYYY"

             // },
             //   {
             //       label: "Expiry date:",
             //       name: "PortfolioContactAddress.ExpiryDate",
             //       type: 'datetime',
             //       def: function () {
             //           //return new Date();
             //           return moment(new Date(), "MM/DD/YYYY").add('year', 1).format("MM/DD/YYYY");
             //       },
             //       dateFormat: "MM/DD/YYYY"
             //   },
             //    {
             //        label: "Days Remaining:",
             //        name: "PortfolioContactAddress.DaysRemaining",
             //        type: "hidden"
             //    },
             ]
         }
             );

         debugger;
         $(window).load(function () {
             $('.dt-buttons').hide();

             $('.statuscls1').each(function () {
                 debugger;
                 var s = $(this).html();
                 if (s == 'Expired')
                     debugger;
                 $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle", "color": "white", "font-weight": "bold" });

             });
         });
         //editor.on('preSubmit', validation);  //execute presubmit
         // Activate an inline edit on click of a table cell
         $('#example').on('click', 'tbody td:not(:first-child)', function (e) {

             editor.inline(this, {
                 onBlur: 'submit',
                 submit: 'all'

             });
         });

         editor.on('preSubmit', function (e, d, action) {
             if (action === 'create' || action === 'edit') {

             }


             if (action == 'edit') {
                 //alert(data.ProjectBOM.Material);
             }

         });


         $('#btn').click(function (e) {
             e.preventDefault();
             // editor.inline($('#example tbody tr:first-child td:first-child'));
             var dataArr = [];
             $.each($("#example tbody tr.selected"), function () {
                 //alert($(this).attr('id').replace('row_', ''));
                 dataArr.push($(this).attr('id').replace('row_', ''));
             });
             //alert(dataArr);
             //alert(rowCount);
             return false;
         });
         var selected = [];
         var table;


         table = $('#example').DataTable({
             "iDisplayLength": "400",
             "scrollX": true,
             responsive: true,
             destroy: true,
             paging: true,
             searching: false,
             dom: "Bfrtip",
             ajax: {
                 url: "/api/ContactAdress",
                 type: 'POST',
                 data: function (d) {
                     d.ContactID =gethcid()// getUrlParameter('ContactID')
                 }
             },
             columns: [

                 //{
                 //    data: null,
                 //    className: "center",
                 //    defaultContent: '<a href="" class="editor_edit"><i class="fa fa-pencil"></i></a>',
                 //    orderable: false
                 //},
                   {
                       data: null,
                       className: "center",
                       defaultContent: '<a href="" class="show_edit"><i class="fa fa-pencil"></i></a>',
                       orderable: false
                   },
                 { data: "PortfolioContactAddress.Address" },
                 { data: "PortfolioContactAddress.City" },
                  { data: "PortfolioContactAddress.State" },
                 { data: "PortfolioContactAddress.PostCode" },
                 {
                     data: "ProductPolicyType.Title", editField: "PortfolioContactAddress.PolicyTypeID",
                     "visible": false
                 },
                 {
                     data: "PortfolioContactAddress.PolicyNumber",
                     "visible": false
                 },
                 {
                     data: "PortfolioContactAddress.StartDate",
                     className: "dt-right",
                     "visible": false,
                     render: function (data, type, row) {
                         return (moment(row.PortfolioContactAddress.StartDate).format("MM/DD/YYYY"));
                     }
                 },
                 {
                     data: "PortfolioContactAddress.ExpiryDate",
                     className: "dt-right",
                     "visible": false,
                     render: function (data, type, row) {
                         return (moment(row.PortfolioContactAddress.ExpiryDate).format("MM/DD/YYYY"));
                     }
                 },
                 {
                     data: "PortfolioContactAddress.DaysRemaining",
                     className: " dt-right",
                      "visible": false,
                     render: function (data, type, row) {
                         var a = moment(row.PortfolioContactAddress.ExpiryDate);
                         // var b = moment(row.PortfolioContactAddress.StartDate);
                         var b = moment(new Date());
                         var d = a.diff(b, 'days');
                         
                         if (d > 0)
                             return (d);
                         else
                             return ("<span style='color:white;font-weight:bold;' class='statuscls1'> Expired</span>");
                     }
                 },
                  {
                      orderable: false,
                      data: null,
                      className: "center",
                      defaultContent: '<a href="" class="show_list btn btn-secondary" style="margin-bottom:0px">List Appliances</a>',
                      "visible": false
                  },
                  {
                      orderable: false,
                      data: null,
                      className: "center",
                      "visible": false,
                      defaultContent: '<a href="" class="editor_remove"><i class="fa fa-trash"></i></a>'
                  }
             ],
             order: [1, 'asc'],
             select: {
                 style: 'single'
                 //selector: 'td:last-child'
             },
             buttons: [
                 { extend: "create", editor: editor, text: "Add New Address" }
                 //{ extend: "edit", editor: editor },
                 //{ extend: "remove", editor: editor }
             ]
         });



         $('#example').on('click', 'tbody td:first-child', function (e) {
             //editor.inline(this);


             if ($(this).parents('tr').hasClass('selected')) {
                 $(this).parents('tr').removeClass('selected');
             }
             else {
                 //table.$('tr.selected').removeClass('selected');
                 $(this).parents('tr').addClass('selected');
             }


             var id = table.row(this).id();
             //alert(id);
             var dataArr = [];
             $.each($("#example tbody tr.selected"), function () {
                 var sid = $(this).attr('id');

                 dataArr.push(sid.replace('row_', ''));
             });
             $("#selectedids").val(dataArr);
         });


         // Edit record
         $('#example').on('click', 'a.save_saving', function (e) {
             e.preventDefault();
             ShowModalPopup();
             //alert($(this).closest('tr').attr('id').replace('row_', ''));
             ShowCurrentTime($(this).closest('tr').attr('id').replace('row_', ''));
         });
         // Edit record
         $('#example').on('click', 'a.editor_edit', function (e) {
             e.preventDefault();

             editor.edit($(this).closest('tr'), {
                 title: 'Edit record',
                 buttons: 'Update'
             });
         });

         // Delete a record
         $('#example').on('click', 'a.editor_remove', function (e) {
             e.preventDefault();

             editor.remove($(this).closest('tr'), {
                 title: 'Delete record',
                 message: 'Are you sure you wish to remove this record?',
                 buttons: 'Delete'
             });
         });

         //
         $('#example').on('click', 'a.show_list', function (e) {
             e.preventDefault();
             var tr = $(this).closest('tr');
             var row = table.row(tr);

             var data1 = row.data();
             var d = data1['PortfolioContactAddress']["ID"];
             
             $('#haid').val(data1['PortfolioContactAddress']["ID"]);
             $('.pnl').show();
             assets_table_load();
             //$('#plist').DataTable().ajax.reload();
             //editor.remove($(this).closest('tr'), {
             //    title: 'Delete record',
             //    message: 'Are you sure you wish to remove this record?',
             //    buttons: 'Delete'
             //});
         });
         $('#example').on('click', 'a.show_edit', function (e) {
             e.preventDefault();
             var tr = $(this).closest('tr');
             var row = table.row(tr);

             var data1 = row.data();
             var d = data1['PortfolioContactAddress']["ID"];
             window.location.href = "ContactAddressDetails.aspx?ContactID=" + gethcid() + "&addid=" + d;
             //debugger;
             //$('#haid').val(data1['PortfolioContactAddress']["ID"]);
             //$('.pnl').show();
             //assets_table_load();
             //$('#plist').DataTable().ajax.reload();
             //editor.remove($(this).closest('tr'), {
             //    title: 'Delete record',
             //    message: 'Are you sure you wish to remove this record?',
             //    buttons: 'Delete'
             //});
         });
         //selectall
         $('#selectall').on('click', 'input.selectall', function (e) {
             e.preventDefault();
             //alert('t');
             table.rows().select();
         });


         $('#selectall').click(function (e) {
             //alert('d');
             if ($(this).is(':checked'))
                 table.rows().select();
             else
                 table.rows().deselect();


             var dataArr = [];
             $.each($("#example tbody tr.selected"), function () {
                 var sid = $(this).attr('id');

                 dataArr.push(sid.replace('row_', ''));
             });

             //alert(dataArr);
             //var sid = id.replace('row_', '');
             $("#selectedids").val(dataArr);
         });

     });


     function GetTrackUrl(l, m, mi, p, imurl, t) {

         if (l != 0)
             return '<a href="' + 'ProjectLabourTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '"  border="0"></a>';
         else if (m != 0)
             return '<a href="' + 'ProjectMaterialTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '" border="0"></a>';
         else if (mi != 0)
             return '<a href="' + 'ProjectMiscTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '" border="0"></a>';

     }


    </script>
             <div class="row">
                  <asp:HyperLink ID="btnAddNewAddress" runat="server" Text="Add New Address" SkinID="Button" NavigateUrl="~/WF/Portal/ContactAddressDetails.aspx" style="float:right" Visible="false" />
            <table id="example" class="col-md-12 display nowrap"  cellspacing="0" width="100%">
        <thead>
            <tr>
                <th></th>
                <th>Address</th>
                <th>City</th>
                <th>State</th>
                <th>Zip Code / Post Code</th>
                <th>Policy Type</th>
                <th>Policy Number</th>
                <th>Start Date</th>
                <th>Expiry Date</th>
                <th>Days Remaining </th>
                <th>List Appliances</th>
                <th>&nbsp;</th>
               
            </tr>
        </thead>
       
    </table>
                     
            </div>
                            <script type="text/javascript" language="javascript"  class="init">

                                function gethaid() {
                                    return $('#haid').val();
                                }

                                var editor1;
                                var selected1 = [];
                                var table1;
                                function assets_table_load() {
                                    editor1 = new $.fn.dataTable.Editor({
                                        ajax: "/api/ContactAdressAssets",
                                        table: "#plist",
                                        fields: [
                                             {
                                                 label: ":",
                                                 name: "Assets.ContactID",
                                                 type: "hidden",
                                                 def: gethcid() //getUrlParameter('ContactID')
                                             },
                                              {
                                                  label: ":",
                                                  name: "Assets.ContactAddressID",
                                                  type: "hidden",
                                                  def: gethaid()
                                              },

                                            {
                                                label: "Type:",
                                                name: "Assets.Type",
                                                type: "select",
                                                placeholder: "Please select..."
                                            },
                                           {
                                               label: "Make:",
                                               name: "Assets.Make",
                                               type: "select",
                                               placeholder: "Please select..."
                                           },
                                             {
                                                 label: "Model:",
                                                 name: "Assets.Model",
                                                 type: "select",
                                                 placeholder: "Please select..."
                                             },
                                              {
                                                  label: "Serial number:",
                                                  name: "Assets.SerialNo"

                                              },
                                         {
                                             label: "Purchase Date:",
                                             name: "Assets.PurchasedDate",
                                             type: 'datetime',
                                             def: function () { return new Date(); },
                                             dateFormat: 'MM/DD/YYYY'

                                         },
                                          {
                                              label: "Notes:",
                                              name: "Assets.FromNotes"
                                          },
                                          //{
                                          //    label: 'Image:',
                                          //    name: 'Assets.image',
                                          //    type: 'upload',
                                          //    noFileText: 'No image'
                                          //},

                                        ]
                                    }
                                        );


                                    table1 = $('#plist').DataTable({
                                        "iDisplayLength": "400",
                                        "scrollX": true,
                                        responsive: true,
                                        destroy: true,
                                        paging: true,
                                        searching: false,
                                        dom: "Bfrtip",
                                        ajax: {
                                            url: "/api/ContactAdressAssets",
                                            type: 'POST',
                                            data: function (d) {
                                                d.ContactID = gethcid(),//getUrlParameter('ContactID'),
                                                d.ContactAddressID = gethaid()
                                            }
                                        },
                                        columns: [

                                            {
                                                data: null,
                                                className: "center",
                                                defaultContent: '<a href="" class="editor_edit_asset"><i class="fa fa-pencil"></i></a>',
                                                orderable: false
                                            },


                                            { data: "Category.Name", editField: "Assets.Type" },
                                            { data: "SubCategory.Name", editField: "Assets.Make" },
                                            { data: "ProductModel.ModelName", editField: "Assets.Model" },
                                            { data: "Assets.SerialNo" },
                                            {
                                                data: "Assets.PurchasedDate",
                                                render: function (data, type, row) {
                                                    return (moment(row.Assets.PurchasedDate).format("MM/DD/YYYY"));
                                                }
                                            },
                                            { data: "Assets.FromNotes" },

                                            // {
                                            //     orderable: false,
                                            //     data: null,
                                            //     className: "center",
                                            //     defaultContent: '<a href="" class="show_list">List Appliances</a>'
                                            // },
                                             //{
                                             //    data: 'Assets.image',
                                             //    render: function (data, type, row) {
                                             //        return (moment(row.Assets.PurchasedDate).format("MM/DD/YYYY"));
                                             //    },
                                             //    defaultContent: 'No image'
                                             //},
                                        ],
                                        order: [1, 'asc'],
                                        select: {
                                            style: 'multi',
                                            selector: 'td:first-child'
                                        },
                                        buttons: [
                                            { extend: "create", editor: editor1, text: "Add New Appliance" }

                                        ]
                                    });


                                    $('#plist').on('click', 'tbody td:not(:first-child)', function (e) {

                                        editor1.inline(this, {
                                            onBlur: 'submit',
                                            submit: 'all'

                                        });
                                    });

                                    $('#plist').on('click', 'a.editor_edit_asset', function (e) {
                                        e.preventDefault();

                                        editor1.edit($(this).closest('tr'), {
                                            title: 'Edit record',
                                            buttons: 'Update'
                                        });
                                    });
                                    editor1.dependent('Assets.Type', function (val, data, callback) {
                                        
                                        $.ajax({
                                            url: '/api/GetMakeData',
                                            data: {
                                                typeid: val
                                            },
                                            type: 'post',
                                            dataType: 'json',
                                            success: function (json) {
                                                
                                                //callback(json.options);
                                                editor1.field('Assets.Make').update(json.options.Assets_Make);
                                            }
                                        });
                                    });
                                    editor1.dependent('Assets.Make', function (val, data, callback) {
                                        
                                        $.ajax({
                                            url: '/api/getmodeldata',
                                            data: {
                                                makeid: val
                                            },
                                            type: 'post',
                                            dataType: 'json',
                                            success: function (json) {
                                                
                                                //callback(json.options);
                                                editor1.field('Assets.Model').update(json.options.Assets_Model);
                                            }
                                        });
                                    });
                                    //editor1.dependent('Assets.Type', '/api/countries');
                                    // editor1.dependent("department", "work_cell");
                                }
                                $(document).ready(function () {
                                    //$('#plist').on('postCreate', function (e, json) {
                                    //    alert('New row added');
                                    //})

                                    //postCreate
                                    $('#plist').on('create', function (e, json, data) {
                                        alert('New row added');
                                    });
                                    //assets_table_load();

                                    //editor.on('preSubmit', validation);  //execute presubmit
                                    // Activate an inline edit on click of a table cell
                                    //$('#plist').on('click', 'tbody td:not(:first-child)', function (e) {

                                    //    editor1.inline(this, {
                                    //        onBlur: 'submit',
                                    //        submit: 'all'

                                    //    });
                                    //});

                                });
                            </script>
                       <br />
                              <div class="row pnl">
                                  <table id="plist" class="col-md-12 display nowrap"  cellspacing="0" width="100%">
        <thead>
            <tr>
                <th></th>
                <th>Type</th>
               <th>Make</th>
                <th>Model</th>
                <th>Serial Number</th>
                <th>Purchase Date</th>
                <th>Notes</th>
                <%--<th>&nbsp;</th>--%>
               
            </tr>
        </thead>
                                      </table>
                                  </div>


                       </div>
               </div>
       </div>
   </div>

       <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImage111" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
                               </asp:UpdateProgress>
       <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                     <ContentTemplate>
                                               <div class="row" style="display:none;visibility:hidden;">
                            <div class="col-md-12">
    <section class="profile-env">
				<div class="form-group row" style="padding-bottom:0px">
                     <div class="card shadow-sm" style="width:100%">
                          <div class="card-header">
                                 Chit chat
                          </div>
                          <div class="panel-body" style="padding-bottom:0px">
                                      <div class="col-sm-12" style="padding-bottom:0px;padding-left:0px">
                                                   <div id="DivChitchatPost" style="width:74%;"></div>
                                      </div>
                              </div>
                         </div>

                    <div class="scrollable ps-container ps-active-y" data-max-height="650" style="max-height:650px;">
                            <div class="col-sm-12" style="padding-left:0px;">
                        <section class="user-timeline-stories">
                                <div id="DivChitchat"></div>
                         </section>
                        </div>
                    </div>

				</div>
        </section>
           <%--<Pref:ChitChatCtrlHomeNew runat="server" id="ChitChatCtrlHomeNew" />--%>
       </div>
                    </div>
                                     </ContentTemplate>
                                   <Triggers>
                                      <%-- <asp:AsyncPostBackTrigger ControlID="ddlPortfolio" />--%>
                                   </Triggers>
                               </asp:UpdatePanel>
        

    
    
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css">
    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.0/css/responsive.dataTables.min.css">--%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.4.1/css/buttons.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/select/1.2.2/css/select.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="/Web/css/editor.dataTables.min.css">
    <%--<link rel="stylesheet" type="text/css" href="/Web/examples/resources/syntax/shCore.css">--%>
   <%-- <link rel="stylesheet" type="text/css" href="/Web/examples/resources/demo.css">--%>
     
    <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
    </script>
   
     
    <script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">

    </script>
     <script type="text/javascript"  src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js">
    </script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.15/sorting/datetime-moment.js"></script>
    
     <script type="text/javascript" src="https://cdn.datatables.net/plug-ins/1.10.16/dataRender/datetime.js">
    </script>
    <%--<script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js"></script>--%>
    <script type="text/javascript"  src="https://cdn.datatables.net/buttons/1.4.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="https://cdn.datatables.net/select/1.2.2/js/dataTables.select.min.js">
    </script>
    <script type="text/javascript" src="/web/js/dataTables.editor.min.js">
    </script>
  
    
    <style type="text/css" class="init">
        div.dataTables_wrapper {
        /*width: 800px;*/
        margin: 0 auto;
    }
        div.dt-buttons{
            float:right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
       <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 

       <%--  <%: System.Web.Optimization.Scripts.Render("~/bundles/chitchat") %>--%>


     <script type="text/javascript">
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);

         setStatusBackColor();

         $(window).load(function () {
             $('.statuscls1').each(function () {
                 debugger;
                 var s = $(this).html();
                 if (s == 'Expired')
                     debugger;
                 $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle", "color": "white", "font-weight": "bold" });

             });
         });
         function setStatusBackColor() {

             $('.statuscls1').each(function () {
                 debugger;
                 var s = $(this).html();
                 if (s == 'Expired')
                     debugger;
                     $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle", "color": "white", "font-weight": "bold" });

             });

         }
</script>
</asp:Content>
