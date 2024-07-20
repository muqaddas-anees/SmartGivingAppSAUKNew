using DeffinityAppDev;
using DocumentFormat.OpenXml.Spreadsheet;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using DC.DAL;
using DC.Entity;

using System.Web.UI;
using UserMgt.BAL;

namespace Users
{
    public partial class users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserData();
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
            var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o => o.SID != UserType.Donor).ToList();






            var rData = (from p in iList
                         select new
                         {
                             ID = p.ID,
                             Name = p.ContractorName ?? "No Name",
                             JobTitle = p.Type ?? "No Job Title",


                             Company = p.Company ?? "No Company",
                             Tasks = GetPendingTasksCount(p.ID), // Fetch pending tasks count here
                             SID = p.SID.GetValueOrDefault(0) // Default to 0 if null
                         }).OrderBy(u => u.Name)
                         .Skip((currentPage - 1) * itemsPerPage)
                         .Take(itemsPerPage)
                         .ToList();


            foreach (var user in rData)
            {
                // Fetch payments for the current user
                List<TithingPaymentTracker> payments;
                List<ActivityBooking> Activity;

                using (PortfolioMgt.DAL.PortfolioDataContext db = new PortfolioDataContext())
                {
                    payments = db.TithingPaymentTrackers
                                .Where(p => p.LoggedByID == user.ID && p.IsPaid == true)
                                .ToList();
                    Activity = db.ActivityBookings
                             .Where(p => p.BookedBy == user.ID)
                             .ToList();
                }

                // Calculate total donations raised
                double donationsRaised = payments.Sum(p => p.PaidAmount ?? 0);
                double EventTicketSales = Activity.Sum(p => p.BookedSolts ?? 0);
                string imgUrl = "/ImageHandler.ashx?id=" + user.ID.ToString() + "&s=" + ImageManager.file_section_user;
                int SID = user.SID;
                // Generate initial for name
                var nameInitial = string.IsNullOrEmpty(user.Name) ? "?" : user.Name.Substring(0, 1);

                // Build HTML for each user card
                html.Append($@"
            <div class='col-md-6 col-xxl-4'>
                <!--begin::Card-->
                <div class='card'>
                    <!--begin::Card body-->
                    <div class='card-body d-flex flex-center flex-column pt-12 p-9'>
                        <!--begin::Avatar-->
                        <div class='symbol symbol-65px symbol-circle mb-5'>
                           <img src=""{imgUrl}"" alt=""image"">
                        </div>
                        <!--end::Avatar-->
                        <!--begin::Name-->
                        <a href='member.aspx?mid={user.ID}' class='fs-4 text-gray-800 text-hover-primary fw-bold mb-0'>{user.Name}</a>
                        <!--end::Name-->
                        <!--begin::Position-->
                        <div class='fw-semibold text-gray-500 mb-6'>{GetSIDLabel(SID)} at {user.Company}</div>
                        <!--end::Position-->
                        <!--begin::Info-->
                        <div class='d-flex flex-center flex-wrap'>
                            <!--begin::Stats-->
                            <div class='border border-gray-300 text-center border-dashed rounded min-w-80px py-3 px-4 mx-2 mb-3'>
                                <div class='fs-6 fw-bold text-gray-700'>{donationsRaised}</div>
                                <div class='fw-semibold text-gray-500'>Donations Raised</div>
                            </div>
                            <!--end::Stats-->
                            <!--begin::Stats-->
                            <div class='border border-gray-300 text-center border-dashed rounded min-w-80px py-3 px-4 mx-2 mb-3'>
                                <div class='fs-6 fw-bold text-gray-700'>{user.Tasks}</div>
                                <div class='fw-semibold text-gray-500'>Pending Tasks</div>
                            </div>
                            <!--end::Stats-->
                            <!--begin::Stats-->
                            <div class='border border-gray-300 border-dashed text-center rounded min-w-80px py-3 px-4 mx-2 mb-3'>
                                <div class='fs-6 fw-bold text-gray-700'>{EventTicketSales}</div>
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

            // Output HTML to userCards div
            userCards.InnerHtml = html.ToString();

            // Generate pagination controls
            GeneratePaginationControls(currentPage, itemsPerPage);
        }


        private void GeneratePaginationControls(int currentPage, int itemsPerPage)
        {
            // Replace PlegitDBEntities with your data retrieval logic
            var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o => o.SID != UserType.Donor).ToList();
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
            int tasks = 0;
            using (var dcContext = new DC.DAL.DCDataContext())
            {
                // Count the number of JobTargets associated with CallDetails of the given CompanyID and assigned to the specific user
                var taskCount = (from a in dcContext.CallDetails
                                 join b in dcContext.JobTargets on a.ID equals b.CallID
                                 join c in dcContext.JobTargetAssignedUsers on b.ID equals c.JobTargetID
                                 where a.CompanyID == sessionKeys.PortfolioID && c.UserID == userId
                                 select b).Count();

                tasks = taskCount;
            }

            return tasks;
        }
    }
        public class ContractorData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public int Tasks { get; set; }
        public int SID { get; set; }
    }
}
