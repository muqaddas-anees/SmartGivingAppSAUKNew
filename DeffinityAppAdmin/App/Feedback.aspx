<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Feedback.aspx.cs" Inherits="DeffinityAppDev.App.Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
                  <script>
                      function showEditModal(id, name, email, mobile, feedbackType, comments, urgencyLevel, agreeToContact) {
                          // Set hidden field with feedback ID
                          document.getElementById('<%= hdnFeedbackID.ClientID %>').value = id;

    // Populate modal fields with existing feedback data
    document.getElementById('<%= txtName.ClientID %>').value = name;
    document.getElementById('<%= txtEmail.ClientID %>').value = email;
    document.getElementById('<%= txtMobile.ClientID %>').value = mobile;
    document.getElementById('<%= ddlFeedbackType.ClientID %>').value = feedbackType;
    document.getElementById('<%= txtComments.ClientID %>').value = comments;
    document.getElementById('<%= ddlUrgencyLevel.ClientID %>').value = urgencyLevel;
                          document.getElementById('<%= chkContact.ClientID %>').checked = agreeToContact;

                          // Show the modal by adding Bootstrap classes
                          showModal();
                      }

                      function showModal() {
                          var modal = document.getElementById('feedbackModal');
                          modal.classList.add('show');
                          modal.style.display = 'block';

                          // Add class to prevent body from scrolling
                          document.body.classList.add('modal-open');
                          document.body.style.overflow = 'hidden'; // Lock background scrolling
                      }

                      function closeModal() {
                          var modal = document.getElementById('feedbackModal');
                          modal.classList.remove('show');
                          modal.style.display = 'none';

                          // Remove class to allow body scrolling
                          document.body.classList.remove('modal-open');
                          document.body.style.overflow = ''; // Restore body scrolling
                      }

                  </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
	Feedback
	</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #chkContact{
            margin-right:10px;        }
    </style>
    <nav>
  <div class="nav nav-tabs" id="nav-tab" role="tablist">
    <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab" data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home" aria-selected="true">Feedback</button>
    <button class="nav-link" id="nav-profile-tab" data-bs-toggle="tab" data-bs-target="#nav-profile" type="button" role="tab" aria-controls="nav-profile" aria-selected="false">Team Notifications</button>
  </div>
</nav>
<div class="tab-content" id="nav-tabContent">
  <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
      <div class="card mt-4">
    <div class="card-header d-flex justify-content-between align-items-center">
        <!-- Dropdown for Country Selection -->
        <div class="col-md-3 d-flex">
            <asp:TextBox CssClass="form-control" placeholder="Search" ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary mx-5" OnClick="btnSearch_Click" Text="Search" />
        </div>
        <!-- Button to trigger Bootstrap modal -->
        <div class="col-md-3 d-flex justify-content-end text-end">
            <asp:DropDownList CssClass="form-select" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true" style="font-weight:normal!important" Width="150px" runat="server" ID="ddlStatus">

                <asp:ListItem>Select a Status</asp:ListItem>
                <asp:ListItem>Pending</asp:ListItem>
                <asp:ListItem>In Progress</asp:ListItem>
                <asp:ListItem>Resolved</asp:ListItem>

            </asp:DropDownList>
        </div>
    </div>

    <div class="card-body">
        <!-- Levels Table -->
        <div class="table-responsive mt-4">


    <asp:GridView ID="grid_issues" runat="server" OnRowCommand="grid_issues_RowCommand" AutoGenerateColumns="False" CssClass="table table-bordered mt-4">
    <Columns>
        <asp:TemplateField HeaderText="Date">
            <ItemTemplate><%# Eval("CreatedDate", "{0:MM/dd/yyyy}") %></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate><%# Eval("Name") %> <br /><%# Eval("EmailAddress") %><br /><%# Eval("MobileNumber") %></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Request Type">
            <ItemTemplate><%# Eval("FeedbackType") %></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Comments">
            <ItemTemplate><%# Eval("Comments") %></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Urgency Level">
            <ItemTemplate><%# Eval("UrgencyLevel") %></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Consent to Communicate">
            <ItemTemplate><%# Eval("IsAgreetobeContacted") %></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate><%# Eval("Status") %></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
               <button class="btn btn-secondary w-125px" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end">
                                                Actions
                       
                                                <i class="ki-duotone ki-down fs-5 ms-1"></i>
                                            </button>

                <div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-800 menu-state-bg-light-primary fw-semibold w-200px py-3" data-kt-menu="true" style="">

                                                <div class="menu-item px-3">
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit Feedback" CssClass="menu-link px-3" CommandName="EditFeedback" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                                </div>
                                                <div class="menu-item px-3">
                            <asp:LinkButton ID="lnkSendMsg" runat="server" Text="Send Message to Customer" CssClass="menu-link px-3" CommandName="SendMessage" CommandArgument='<%# Eval("ID") %>' ></asp:LinkButton>
                                                </div>
                                                <div class="menu-item px-3">
                            <asp:LinkButton ID="lnkEmailTrail" runat="server" Text="Email Trail" CssClass="menu-link px-3" CommandName="EmailTrail" CommandArgument='<%# Eval("ID") %>' ></asp:LinkButton>
                                                </div>
                                                <div class="menu-item px-3">
                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete Feedback" CssClass="menu-link px-3" CommandName="DeleteFeedback" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure you want to delete this feedback?');"></asp:LinkButton>
                                                </div>
                                                

                                            </div>


            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
            <asp:HiddenField ID="hdnFeedbackID" ClientIDMode="Static" runat="server" />

</div>
        </div></div>


  </div>
  <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">




            <div class="card mt-4">
    <div class="card-header d-flex align-items-center">
        <!-- Dropdown for Country Selection -->
        <div class="col-md-7 d-flex">
            <asp:TextBox CssClass="form-control mx-3" style="margin-right:10px!important;" placeholder="Name" ID="txtNameofTeam" runat="server"></asp:TextBox>
                        <asp:TextBox CssClass="form-control mx-3" style="margin-right:10px!important;" placeholder="Email" ID="txtEmailofTeam" runat="server"></asp:TextBox>

            <asp:Button ID="btnaddteam" runat="server" CssClass="btn btn-primary mx-3" Text="Add to Team Notification List" OnClick="btnaddteam_Click"  />
        </div>
        <!-- Button to trigger Bootstrap modal -->
    </div>

    <div class="card-body">
        <!-- Levels Table -->
        <div class="table-responsive mt-4">


    <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False" CssClass="table table-bordered mt-4">
    <Columns>
     
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate><%# Eval("name") %> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Email">
            <ItemTemplate><%# Eval("email") %></ItemTemplate>
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>

                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="menu-link px-3" CommandName="del" CommandArgument='<%# Eval("id") %>'><i class="fas fa-trash-alt"></i></asp:LinkButton>
                                    



            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
            <asp:HiddenField ID="HiddenField1" ClientIDMode="Static" runat="server" />

</div>
        </div></div>










  </div>
</div>
	
    <div class="modal fade" id="feedbackModal" tabindex="-1" aria-labelledby="feedbackModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="feedbackModalLabel">Got Ideas? Tell us what's on your mind!</h5>
        <button type="button" class="btn-close" onclick="closeModal();" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <h5>Your feedback helps us improve! Please share your thoughts, and we'll get back to you if needed.</h5>
        
        <!-- ASP.NET Input Fields -->
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Name"></asp:TextBox><br />
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email Address"></asp:TextBox><br />
        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" Placeholder="Mobile Number"></asp:TextBox><br />
        
        <asp:DropDownList ID="ddlFeedbackType" runat="server" style="font-weight:normal!important" CssClass="form-control text-muted">
            <asp:ListItem Value="" Text="Feedback Type"></asp:ListItem>
            <asp:ListItem Value="Feature request" Text="Feature request"></asp:ListItem>
            <asp:ListItem Value="Bug Report" Text="Bug Report"></asp:ListItem>
            <asp:ListItem Value="General Feedback" Text="General Feedback"></asp:ListItem>
        </asp:DropDownList><br />
        
        <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" Placeholder="Comments"></asp:TextBox><br />
        
        <asp:DropDownList ID="ddlUrgencyLevel" style="font-weight:normal!important" runat="server" CssClass="form-control">
            <asp:ListItem Value="" Text="Urgency Level"></asp:ListItem>
            <asp:ListItem Value="Low" Text="Low"></asp:ListItem>
            <asp:ListItem Value="Medium" Text="Medium"></asp:ListItem>
            <asp:ListItem Value="High" Text="High"></asp:ListItem>
        </asp:DropDownList><br />


           <asp:DropDownList ID="ddlStatusinModal" style="font-weight:normal!important" runat="server" CssClass="form-control">
     <asp:ListItem Value="" Text="Status"></asp:ListItem>
     <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
     <asp:ListItem Value="In Hand" Text="In Hand"></asp:ListItem>
     <asp:ListItem Value="Resolved" Text="Resolved"></asp:ListItem>
                    <asp:ListItem Value="Canceled" Text="Canceled"></asp:ListItem>

 </asp:DropDownList><br />
                  <asp:TextBox ID="txtimprovemnts" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" Placeholder="Improvements Made To The Platform"></asp:TextBox><br />

        
        <asp:CheckBox ID="chkContact" CssClass="text-muted" runat="server" Text="I agree to be contacted regarding this feedback" />
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnSubmitFeedback" ClientIDMode="Static" runat="server" style="background-color:#50CD89;" Text="Submit" OnClick="btnSubmitFeedback_Click" />
        <button onclick="closeModal();" type="button" aria-label="Close" class="btn btn-light">Close</button>
      </div>
    </div>
  </div>
</div>


    <!-- Modal Trigger Button (Optional) -->

<!-- Modal Structure -->
<div class="modal fade" id="emailModal" tabindex="-1" aria-labelledby="emailModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="emailModalLabel">Email Customer</h5>
                <button type="button" class="btn-close" onclick="hideSendEmailModal()" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <p>Use the following form to send a response to the following feedback:</p>
         
           <table class="table table-borderless">
                    <tbody>
                        <tr>
                            <td><strong>Instance:</strong></td>
                            <td><asp:Label ID="lblInstance" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><strong>Logged By:</strong></td>
                            <td><asp:Label ID="lblloggedby" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><strong>Feedback:</strong></td>
                            <td><asp:Label ID="lblfeedback" runat="server"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
                <hr />

                <h6>Your Details:</h6>

                <!-- ASP.NET Controls for User Details -->
                <div class="mb-3">
                    <asp:Label ID="lblName" runat="server" Text="Name" AssociatedControlID="txtName" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtNameofSender" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblEmailAddress" runat="server" Text="Email Address" AssociatedControlID="txtEmailAddress" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control" placeholder="Email Address"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblSubject" runat="server" Text="Subject Line" AssociatedControlID="txtSubject" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" placeholder="Subject Line"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtEmailofSender" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Email" Rows="5"></asp:TextBox>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSendEmail" runat="server" ClientIDMode="Static" Text="Send Email" OnClick="btnSendEmail_Click" />
                <button type="button" class="btn btn-secondary" onclick=" hideSendEmailModal()">Close</button>
                <asp:HiddenField ID="hfIDtosendemail" runat="server" />
            </div>
        </div>
    </div>
</div>

    

<!-- JavaScript to Show the Modal -->

<script>
    function showSendEmailModal() {
        var emailModal = document.getElementById('emailModal');

        // Add the 'show' class to make the modal visible
        emailModal.classList.add('show');

        // Remove the 'display: none;' style (if any) to ensure the modal is shown
        emailModal.style.display = 'block';

        // Optionally, add the 'modal-backdrop' class to show the backdrop
        var backdrop = document.createElement('div');
        backdrop.classList.add('modal-backdrop', 'fade', 'show');
        document.body.appendChild(backdrop);

        // Add the 'fade' class to make it fade in (optional)
        setTimeout(function () {
            emailModal.classList.add('fade');
        }, 200);
    }
    function hideSendEmailModal() {
        var emailModal = document.getElementById('emailModal');

        // Remove the 'show' class to hide the modal
        emailModal.classList.remove('show');

        // Set the modal's display to 'none' to hide it
        emailModal.style.display = 'none';

        // Also remove the backdrop
        var backdrop = document.querySelector('.modal-backdrop');
        if (backdrop) {
            backdrop.remove();
        }
    }

</script>

      
	</asp:Content>
