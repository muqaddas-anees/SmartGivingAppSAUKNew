        <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="GetBeneficiaries.aspx.cs" Inherits="DeffinityAppDev.App.Beneficiaries.GetBeneficiaries" %>



    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>


    <asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
   
        <div class="row justify-content-end">
        </div>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <div class="me-auto mx-auto my-12 d-flex">
                <select id="tableSelector" class="form-select" onchange="toggleTables();">
                    <option value="beneficiaries">Beneficiaries</option>
                    <option value="secondaryBeneficiaries">Secondary Beneficiaries</option>
                </select>
                        <a class="btn btn-primary mx-2" href="BasicInfo.aspx" ID="btnBeneficiaries">Beneficiaries</a>
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
                                <td class="text-start">
                        <a href="#" class="btn btn-light btn-active-light-primary btn-flex btn-center btn-sm" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end">
                            Actions
                            <i class="ki-duotone ki-down fs-5 ms-1"></i>                    </a>
                        <!--begin::Menu-->
    <div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-semibold fs-7 w-125px py-4" data-kt-menu="true">
        <!--begin::Menu item-->
        <div class="menu-item px-3">
            <a href="/App/Beneficiaries/SecondaryBeneficiaries.aspx" class="menu-link px-3">
                Edit
            </a>
        </div>
        <!--end::Menu item-->
    
        <!--begin::Menu item-->
        <div class="menu-item px-3">
        <asp:LinkButton 
            ID="DeleteButton" 
            runat="server" 
            CommandArgument='<%# Eval("SecondaryBeneficiaryID") %>' 
            OnClick="DeleteButton_Click"
            CssClass="menu-link px-3"
            data-kt-users-table-filter="delete_row">
            Delete
        </asp:LinkButton>
    </div>

        <!--end::Menu item-->
    </div>
    <!--end::Menu-->
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
                                <td class="text-start">
                        <a href="#" class="btn btn-light btn-active-light-primary btn-flex btn-center btn-sm" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end">
                            Actions
                            <i class="ki-duotone ki-down fs-5 ms-1"></i>                    </a>
                        <!--begin::Menu-->
    <div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-semibold fs-7 w-125px py-4" data-kt-menu="true">
        <!--begin::Menu item-->
        <div class="menu-item px-3">
        <a href='/App/Beneficiaries/BasicInfo.aspx?PersonID=<%# Eval("PersonID") %>' class="menu-link px-3">
    Edit
</a>

        </div>
        <!--end::Menu item-->
    
        <!--begin::Menu item-->
        <div class="menu-item px-3">
        <asp:LinkButton 
            ID="DeleteButton" 
            runat="server" 
            CommandArgument='<%# Eval("PersonID") %>' 
            OnClick="DeleteButtonForBeneficiaries_Click"
            CssClass="menu-link px-3"
            data-kt-users-table-filter="delete_row">
            Delete
        </asp:LinkButton>
    </div>

        <!--end::Menu item-->
    </div>
    <!--end::Menu-->
                    </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
                         <script type="text/javascript">
                             var hostUrl = "/assets/";
                         </script>
    <script type="text/javascript" src="/assets/plugins/global/plugins.bundle.js"></script>
    <script type="text/javascript" src="/assets/js/scripts.bundle.js"></script>

        <script type="text/javascript">
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