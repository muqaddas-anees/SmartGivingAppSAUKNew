<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App/Beneficiaries/Beneficiaries.Master" CodeBehind="GetBeneficiaries.aspx.cs" Inherits="DeffinityAppDev.App.Beneficiaries.GetBeneficiaries" %>

<asp:Content ID="BeneficiaryReport" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <!-- Flexbox container for table selector and search bar -->
        <div class="d-flex justify-content-between align-items-center mb-3">
            <div class="me-auto mx-auto my-12">
                <select id="tableSelector" class="form-select" onchange="toggleTables();">
                    <option value="beneficiaries">Beneficiaries</option>
                    <option value="secondaryBeneficiaries">Secondary Beneficiaries</option>
                </select>
            </div>

            <!-- Search bar and button (aligned to the right) -->
            <div class="d-flex align-items-center">
                <div class="position-relative w-md-400px me-2">
                    <i class="ki-duotone ki-magnifier fs-3 text-gray-500 position-absolute top-50 translate-middle ms-6"></i>
                    <input type="text" id="searchInput" class="form-control form-control-solid ps-10" name="search" placeholder="Search" />

                </div>
            </div>
        </div>

        <!-- Secondary Beneficiaries Table -->
        <div id="secondaryBeneficiariesTableContainer" class="table-responsive" style="display: block;">
            <table class="table align-middle table-row-dashed fs-6 gy-5" id="secondaryBeneficiariesTable" style="width: 100%; text-align: left;">
                <thead>
                    <tr class="text-start text-muted fw-bold fs-7 text-uppercase gs-0" role="row">
                        <th>Image</th>
                        <th>Name</th>
                        <th>Enrollment Date</th>
                        <th>City</th>
                        <th>Country</th>
                        <th>Email</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody class="text-gray-600 fw-semibold">
                    <asp:Repeater ID="SecondaryBeneficiariesRepeater" runat="server" OnItemCommand="RepeaterSecondaryBeneficiaries_ItemCommand">
                        <ItemTemplate>
                            <tr>
           <td>
    <img src='<%# Eval("ProfileImage") != DBNull.Value && Eval("ProfileImage") != null ? "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("ProfileImage")) : "/metronic/8/default.jpeg" %>' 
         style="width: 50px; height: 50px; border-radius: 50%; object-fit: cover;" alt="Image" />
</td>

                                <td><%# Eval("Name") %></td>
                                <td><%# Convert.ToDateTime(Eval("CreatedAt")).ToString("MM/dd/yyyy") %></td>
                                <td><%# Eval("City") %></td>
                                <td><%# Eval("Country") %></td>
                                <td><%# Eval("Email") %></td>
                                <td>
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("BeneficiaryID") %>' CssClass="btnEdit">
                                        <i class="fas fa-edit"></i>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <!-- Beneficiaries Table -->
        <div id="beneficiariesTableContainer" class="table-responsive" style="display: none;">
            <table class="table align-middle table-row-dashed fs-6 gy-5" id="beneficiariesTable" style="width: 100%; text-align: left;">
                <thead>
                    <tr class="text-start text-muted fw-bold fs-7 text-uppercase gs-0" role="row">
                        <th>Image</th>
                        <th>Name</th>
                        <th>Enrollment Date</th>
                       
                        <th>City</th>
                        <th>Country</th>
                        <th>Email</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody class="text-gray-600 fw-semibold">
                    <asp:Repeater ID="BeneficiariesRepeater" runat="server" OnItemCommand="RepeaterBeneficiaries_ItemCommand">
                        <ItemTemplate>
                            <tr>
  <td>
    <img src='<%# Eval("ProfileImage") != DBNull.Value && Eval("ProfileImage") != null ? "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("ProfileImage")) : "/metronic/8/default.jpeg" %>' 
         style="width: 50px; height: 50px; border-radius: 50%; object-fit: cover;" alt="Image" />
</td>


                                <td><%# Eval("Name") %></td>
                                <td><%# Convert.ToDateTime(Eval("CreatedAt")).ToString("MM/dd/yyyy") %></td>
                                
                                <td><%# Eval("City") %></td>
                                <td><%# Eval("Country") %></td>
                                <td><%# Eval("Email") %></td>
                                <td>
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("PersonID") %>' CssClass="btnEdit">
                                        <i class="fas fa-edit"></i>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <script src="/metronic8/demo1/assets/plugins/global/plugins.bundle.js"></script>
        <script src="/metronic8/demo1/assets/js/scripts.bundle.js"></script>
        <script>
            function toggleTables() {
                const secondaryBeneficiariesTableContainer = document.getElementById('secondaryBeneficiariesTableContainer');
                const beneficiariesTableContainer = document.getElementById('beneficiariesTableContainer');
                const selector = document.getElementById('tableSelector');

                if (selector.value === 'beneficiaries') {
                    beneficiariesTableContainer.style.display = 'block';
                    secondaryBeneficiariesTableContainer.style.display = 'none';
                } else {
                    secondaryBeneficiariesTableContainer.style.display = 'block';
                    beneficiariesTableContainer.style.display = 'none';
                }
            }

            // Run on page load to hide one table initially
            window.onload = function () {
                toggleTables();
            };
            function searchFunction() {
                const input = document.getElementById('searchInput');
                const filter = input.value.toLowerCase();

                // Get the currently visible table
                const selector = document.getElementById('tableSelector');
                const currentTable = selector.value === 'beneficiaries'
                    ? document.getElementById('beneficiariesTable')
                    : document.getElementById('secondaryBeneficiariesTable');

                const rows = currentTable.getElementsByTagName('tr');

                // Loop through all table rows and hide those that don't match the search query
                for (let i = 1; i < rows.length; i++) { // Start from 1 to skip the header row
                    const cells = rows[i].getElementsByTagName('td');
                    let found = false;

                    // Check each cell in the row
                    for (let j = 0; j < cells.length; j++) {
                        if (cells[j]) {
                            const txtValue = cells[j].textContent || cells[j].innerText;
                            if (txtValue.toLowerCase().indexOf(filter) > -1) {
                                found = true;
                                break;
                            }
                        }
                    }

                    // Show or hide the row based on whether it matches the search
                    rows[i].style.display = found ? '' : 'none';
                }
            }

            // Event listener for dynamic search
            document.getElementById('searchInput').addEventListener('input', searchFunction);
            if (window.history.replaceState) {
                window.history.replaceState(null, null, window.location.href);
            }
        </script>


 </asp:Content>