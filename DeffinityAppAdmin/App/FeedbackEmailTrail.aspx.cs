using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;

namespace DeffinityAppDev.App
{
    public partial class FeedbackEmailTrail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTrail();
        }

        private void BindTrail()
        {
            using (var context = new PortfolioDataContext())
            {
                var trail = context.EmailTrails
        .Where(o => o.Feedbackid == QueryStringValues.MID)
        .AsEnumerable() // Switch to LINQ-to-Objects to allow formatting
        .Select(o => new
        {
            CreatedDate = o.Createdate.HasValue ? o.Createdate.Value.ToString("dd/MM/yyyy") : string.Empty,
            Subject = o.Subject,
            Email = o.Email,
            SentBy = o.Sentby,
            FeedbackId = o.Feedbackid,
            EmailAddress = o.EmailAddress,
            CreatedBy = GetContractorName(o.Sentby ?? 0)
        })
        .ToList();

                grid_trail.DataSource = trail;
                grid_trail.DataBind();  // Bind the data to the grid
            }
        }

        private string GetContractorName(int id)
        {
            using (var c = new PortfolioDataContext())
            {
                var u = c.PortfolioTrackerLogins.FirstOrDefault(o => o.ID == id);
                if (u == null)
                    return "";
                return u.DisplayName;
            }
        }

    }
}