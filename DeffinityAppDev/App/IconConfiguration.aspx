<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="IconConfiguration.aspx.cs" Inherits="DeffinityAppDev.App.IconConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Donor CRM
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        .right-align {
    text-align: right; /* Align text to the right */
    padding-right:  11% !important; /* Adjust padding as needed */
}


    </style>
   <div class="card">
    <div class="card-header d-flex justify-content-between align-items-center">
        <div class="col-md-3">
            <!-- Dropdown for Country Selection -->
        </div>
        <div class="col-md-9 text-end">
            <!-- Button to trigger Bootstrap modal -->
            <asp:Button ID="btnAddLevel" runat="server" CssClass="btn btn-primary" Text="Add Level" OnClientClick="$('#addLevelModal').modal('show'); return false;" />
        </div>
    </div>

    <div class="card-body">
        <!-- Levels Table -->
        <div class="table-responsive mt-4">
            <asp:GridView ID="gvLevels" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" OnRowCommand="gvLevels_RowCommand" DataKeyNames="Id">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <button type="button" style="color:black" class="btn btn-sm btn-outline-secondary" 
onclick='editLevel(<%# Eval("Id") %>, "<%# string.Format("{0:N0}", Eval("FromRange")) %>", "<%# string.Format("{0:N0}", Eval("ToRange")) %>", "<%# Eval("Country") %>")'>
                                Edit
                            </button>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="imgProfilePic" runat="server" 
                                       ImageUrl='<%# GetImageUrl(Eval("Id")) %>' 
                                       Width="50px" Height="50px" CssClass="rounded-circle" />
                        </ItemTemplate>
                    </asp:TemplateField>

      <asp:BoundField HeaderText="Lower Level" DataField="FromRange" 
                DataFormatString="{0:N0}">
    <ItemStyle CssClass="right-align" />
</asp:BoundField>

<asp:BoundField HeaderText="Upper Level" DataField="ToRange" 
                DataFormatString="{0:N0}">
    <ItemStyle CssClass="right-align" />
</asp:BoundField>


                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-danger" CommandName="DeleteLevel" CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>


    <!-- Bootstrap Modal for Adding New Level -->
    <div class="modal fade" id="addLevelModal" tabindex="-1" aria-labelledby="addLevelModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="addLevelModalLabel">Add New Level</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                     <asp:HiddenField ID="hfLevelId" runat="server" />
               <div class="form-group">
    <label for="txtFromRange">Lower Level</label>
    <asp:TextBox ID="txtFromRange" runat="server" CssClass="form-control" 
                 oninput="formatNumberWithCommas(this)"></asp:TextBox>
</div>
<div class="form-group">
    <label for="txtToRange">Upper Level</label>
    <asp:TextBox ID="txtToRange" runat="server" CssClass="form-control" 
                 oninput="formatNumberWithCommas(this)"></asp:TextBox>
</div>


                    <div class="form-group">
                        <label for="txtImageUrl">Image</label>

                        <asp:FileUpload runat="server" ID="Image" CssClass="form-control" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveLevel" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSaveLevel_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/jquery-1.9.0.min.js"></script>
    <script type="text/javascript">
        function editLevel(id, fromRange, toRange, country) {
            // Assign values to the modal fields for editing
            document.getElementById('<%= txtFromRange.ClientID %>').value = fromRange;
        document.getElementById('<%= txtToRange.ClientID %>').value = toRange;

        // Optionally set the hidden field with the level id
        document.getElementById('<%= hfLevelId.ClientID %>').value = id;

            // Show the modal
            $('#addLevelModal').modal('show');
        }
    </script>

    <script type="text/javascript">
    function showEditModal() {
        // Show the modal
        $('#addLevelModal').modal('show');
        }
        function formatNumberWithCommas(input) {
            // Remove non-numeric characters except for the decimal point
            let value = input.value.replace(/[^0-9]/g, '');

            // Add commas every three digits
            input.value = value.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
        }

    </script>
    

    </asp:Content>