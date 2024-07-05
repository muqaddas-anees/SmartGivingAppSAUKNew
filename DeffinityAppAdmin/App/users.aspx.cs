using DeffinityAppDev;
using System;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Users
{
    public partial class users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserData();
                LoadTableUserData();
            }
        }

        private void LoadUserData()
        {
            StringBuilder html = new StringBuilder();

            // Define pagination variables
            int currentPage = 1;
            int itemsPerPage = 9;

            // Retrieve page number from query string if available
            if (!string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                currentPage = Convert.ToInt32(Request.QueryString["page"]);
            }

            // Replace PlegitDBEntities with your data retrieval logic
            var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany();
            var rData = (from p in iList
                         select new
                         {
                             ID = p.ID,
                             Name = p.ContractorName ?? "No Name",
                             JobTitle = p.Type ?? "No Job Title",
                             Company = p.Company ?? "No Company",
                             Earnings = p.NormalBuyingRate.GetValueOrDefault(0), // Default to 0 if null
                             Tasks = GetPendingTasksCount(p.ID), // Fetch pending tasks count here
                             Sales = p.OvertimeBuyingRate.GetValueOrDefault(0), // Default to 0 if null
                             SID = p.SID.GetValueOrDefault(0) // Default to 0 if null
                         }).OrderBy(u => u.Name)
                         .Skip((currentPage - 1) * itemsPerPage)
                         .Take(itemsPerPage)
                         .ToList();

            foreach (var user in rData)
            {
                   

                html.Append($@"
                    <div class='col-md-6 col-xxl-4'>
                        <!--begin::Card-->
                        <div class='card'>
                            <!--begin::Card body-->
                            <div class='card-body d-flex flex-center flex-column pt-12 p-9'>
                                <!--begin::Avatar-->
                                <div class='symbol symbol-65px symbol-circle mb-5'>
                               <span class='symbol-label fs-2x fw-semibold text-info bg-light-info'>{user.Name.Substring(0, 1)}</span>
                                    <div class='bg-success position-absolute border border-4 border-body h-15px w-15px rounded-circle translate-middle start-100 top-100 ms-n3 mt-n3'></div>
                                </div>
                                <!--end::Avatar-->
                                <!--begin::Name-->
                                <a href='#' class='fs-4 text-gray-800 text-hover-primary fw-bold mb-0'>{user.Name}</a>
                                <!--end::Name-->
                                <!--begin::Position-->
                                <div class='fw-semibold text-gray-500 mb-6'>{GetSIDLabel(user.SID)} at {user.Company}</div>
                                <!--end::Position-->
                                <!--begin::Info-->
                                <div class='d-flex flex-center flex-wrap'>
                                    <!--begin::Stats-->
                                    <div class='border border-gray-300 border-dashed rounded min-w-80px py-3 px-4 mx-2 mb-3'>
                                        <div class='fs-6 fw-bold text-gray-700'>{user.Earnings}</div>
                                        <div class='fw-semibold text-gray-500'>Donations Raised</div>
                                    </div>
                                    <!--end::Stats-->
                                    <!--begin::Stats-->
                                    <div class='border border-gray-300 border-dashed rounded min-w-80px py-3 px-4 mx-2 mb-3'>
                                        <div class='fs-6 fw-bold text-gray-700'>{user.Tasks}</div>
                                        <div class='fw-semibold text-gray-500'>Pending Tasks</div>
                                    </div>
                                    <!--end::Stats-->
                                    <!--begin::Stats-->
                                    <div class='border border-gray-300 border-dashed rounded min-w-80px py-3 px-4 mx-2 mb-3'>
                                        <div class='fs-6 fw-bold text-gray-700'>{user.Sales}</div>
                                        <div class='fw-semibold text-gray-500'>Event Ticket Sales</div>
                                    </div>
                                    <!--end::Stats-->
                                </div>
                                <!--end::Info-->
                            </div>
                            <!--end::Card body-->
                        </div>
                        <!--end::Card-->
                    </div>");
            }

            userCards.InnerHtml = html.ToString();

            GeneratePaginationControls(currentPage, itemsPerPage);
        }

        private void LoadTableUserData()
        {
            StringBuilder html = new StringBuilder();

            // Replace PlegitDBEntities with your data retrieval logic
            var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany();

            var usersQuery = (from p in iList
                              select new
                              {
                                  Name = p.ContractorName,
                                  JobTitle = p.Type,
                                  Company = p.Company,
                                  Earnings = p.NormalBuyingRate,
                                  Tasks = GetPendingTasksCount(p.ID), // Fetch pending tasks count here
                                  Sales = p.OvertimeBuyingRate,
                                  // Assuming ImageData holds the profile image URL or path
                              }).ToList();

            foreach (var user in usersQuery)
            {
                html.Append($@"
                    <tr class='odd'>
                        <td>
                            <div class='d-flex align-items-center'>
                                <div class='me-5 position-relative'>
                                    <div class='symbol symbol-35px symbol-circle'>
                                        <img alt='Pic' src='{"user.ProfileImg"}'>
                                    </div>
                                </div>
                                <div class='d-flex flex-column justify-content-center'>
                                    <a href='#' class='mb-1 text-gray-800 text-hover-primary'>{user.Name}</a>
                                    <div class='fw-semibold fs-6 text-gray-500'>{user.JobTitle} at {user.Company}</div>
                                </div>
                            </div>
                        </td>
                        <td data-order='{user.Tasks}'>{user.Tasks}</td>
                        <td>${user.Sales}</td>
                        <td>
                            <span class='badge badge-light-info fw-bold px-4 py-3'>{user.Earnings}</span>
                        </td>
                        <td class='text-end'>
                            <a href='#' class='btn btn-light btn-sm'>View</a>
                        </td>
                    </tr>");
            }

            userTableBody.InnerHtml = html.ToString();
        }

        private void GeneratePaginationControls(int currentPage, int itemsPerPage)
        {
            // Replace PlegitDBEntities with your data retrieval logic
            var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany();
            int totalUsers = iList.Count();
            int totalPages = (int)Math.Ceiling((double)totalUsers / itemsPerPage);

            StringBuilder paginationHtml = new StringBuilder();

            paginationHtml.Append($@"
                <div class='d-flex flex-stack flex-wrap pt-10'>
                    <div class='fs-6 fw-semibold text-gray-700'>Showing {((currentPage - 1) * itemsPerPage) + 1} to {Math.Min(currentPage * itemsPerPage, totalUsers)} of {totalUsers} entries</div>
                    <ul class='pagination'>
                        <li class='page-item previous'>
                            <a href='users.aspx?page={Math.Max(currentPage - 1, 1)}' class='page-link'>
                                <i class='previous'></i>
                            </a>
                        </li>");

            for (int page = 1; page <= totalPages; page++)
            {
                paginationHtml.Append($@"
                    <li class='page-item {(page == currentPage ? "active" : "")}'>
                        <a href='users.aspx?page={page}' class='page-link'>{page}</a>
                    </li>");
            }

            paginationHtml.Append($@"
                        <li class='page-item next'>
                            <a href='users.aspx?page={Math.Min(currentPage + 1, totalPages)}' class='page-link'>
                                <i class='next'></i>
                            </a>
                        </li>
                    </ul>
                </div>");

            paginationControlsContainer.Controls.Add(new LiteralControl(paginationHtml.ToString()));
        }

        private string GetSIDLabel(int sid)
        {
            // Replace with your logic to get SID labels
            switch (sid)
            {
                case 1:
                    return "Manager/Administrator";
                case 2:
                    return "Donor";
                case 3:
                    return "Fundraiser";
                case 4:
                    return "Volunteer";
                default:
                    return "";
            }
        }

        private int GetPendingTasksCount(int userId)
        {
            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];

            using (var tasksContext = new TasksDataContext(ConnectionString))
            {
                var tasks = tasksContext.Taskks.Where(t => t.currID == userId && t.StatusID == 1).ToList();
                return tasks.Count;
            }
        }
    }
}
