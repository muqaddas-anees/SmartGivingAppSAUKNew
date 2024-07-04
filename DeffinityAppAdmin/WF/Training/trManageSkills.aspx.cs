using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Training.DAL;
using Training.Entity;
using UserMgt.Entity;
using UserMgt.DAL;
using UserMgt.BAL;
using System.Text;

public partial class Training_trManageSkills : System.Web.UI.Page
{
    string[] userHeaderList = new string[] { "Name", "Email", "Contact No" };
    //string[] userHeaderList = new string[] { "Name" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUsers();
            pnlDisplayHtml.Text = DisplayDashboard();
        }
    }
    private void BindUsers()
    {
        ContractorsBAL cb = new ContractorsBAL();
        var sids = new int[]{1,2,3,4,9};
        var uList = cb.Contractor_Select_Active().Where(p => sids.Contains(p.SID.Value)).OrderBy(o=>o.ContractorName).ToList();
        chkUserlist.DataSource = uList;
        chkUserlist.DataValueField = "ID";
        chkUserlist.DataTextField = "ContractorName";
        chkUserlist.DataBind();

    }
    public List<int> GetSelectedUserid()
    {
         List<int> courseIds = new List<int>();
        foreach (ListItem i in chkUserlist.Items)
        {
            if (i.Selected)
                courseIds.Add(Convert.ToInt32(i.Value));
        }

        return courseIds;

    }
    public string DisplayDashboard()
    {
       
        string retVal = string.Empty;
        string trow1 = string.Empty;
        string trow2 = string.Empty;
        StringBuilder sb = new StringBuilder();
        using (TrainingDataContext tr = new TrainingDataContext())
        {
            using (UserDataContext ud = new UserDataContext())
            {
                //var bookingsList = tr.Bookings.Select(p => p).ToList();
                var bookingsList = ud.UserSkills.Select(p => p).ToList();
                ContractorsBAL cb = new ContractorsBAL();
                var uids = bookingsList.Select(p => p.UserId).ToArray();
                var sids = new int[]{1,2,3,4,9};
                var userlist = cb.Contractor_Select_Active().Where(o => sids.Contains(o.SID.Value)).OrderBy(o=>o.ContractorName).ToList();//.Where(p => uids.Contains(p.ID)).ToList();
                if(GetSelectedUserid().Count >0)
                {
                    userlist = userlist.Where(o => GetSelectedUserid().ToArray().Contains(o.ID)).ToList();
                }
                var tCategories = tr.Categories.ToList();
                var tCourseList = tr.Courses.ToList();

                //create table
                sb.Append("<table width='900px' id='mainTable' class='scrolltable' bordercolor='black' border='1' cellpadding='1' cellspacing='0' style='color:black;font-size:10px;font-weight:bold'>");
                //Header header
                trow1 += "<tr class='tab_header' style='font-weight:bold;height:30px;border-color:silver'>";
                trow2 += "<tr style='font-weight:bold;height:30px;background-color:#F3EFF1;border-color:silver;'>";

                trow1 += string.Format("<th colspan='3'>Resource Details</th>");
                foreach (string s in userHeaderList)
                    trow2 += string.Format("<th>{0}</th>", s);
                //Add Category
                foreach (Category c in tCategories)
                {
                    var csList = tCourseList.Where(o => o.CategoryID == c.ID).ToList();
                    if (csList.Count > 0)
                    {
                        trow1 = trow1 + string.Format("<td colspan='{0}'  style='text-align:left'>{1}</td>", csList.Count, c.Name);
                        foreach (Course cs in csList.OrderBy(o => o.Title))
                            trow2 = trow2 + string.Format("<td>{0}</td>", cs.Title);
                    }
                }
                trow1 = trow1 + "</tr>";
                trow2 = trow2 + "</tr>";
                //first header
                sb.Append(trow1);
                //second header
                sb.Append(trow2);

                foreach (UserMgt.Entity.Contractor ct in userlist.OrderBy(o => o.ContractorName))
                {
                    sb.Append("<tr>");
                    sb.Append(string.Format("<th class='odd_row'>{0}</th><th class='odd_row'><a href='mailto:{1}'>{1}</a></th><th class='odd_row'>{2}</th>", ct.ContractorName, ct.EmailAddress, ct.ContactNumber));
                    //sb.Append(string.Format("<th>{0}</th>", ct.ContractorName));
                    foreach (Category c in tCategories)
                    {
                        var csList = tCourseList.Where(o => o.CategoryID == c.ID).ToList();
                        foreach (Course cs in csList.OrderBy(o => o.Title))
                        {
                            //Complete - 6
                            var dval = bookingsList.Where(o => o.UserId == ct.ID && o.CategoryID == c.ID && o.CourseID == cs.ID ).FirstOrDefault();

                            //var dval = bookingsList.Where(o => o.Employee == ct.ID && o.CategoryID == c.ID && o.CourseID == cs.ID).FirstOrDefault();

                            sb.Append(string.Format("<td class='txt_center'>{0}</td>", (dval != null) ? string.Format("<input type='checkbox' value='{0}' checked='checked' />", c.ID.ToString() + "_" + cs.ID.ToString() + "_" + ct.ID.ToString()) : string.Format("<input type='checkbox' value='{0}' />", c.ID.ToString() + "_" + cs.ID.ToString() + "_" + ct.ID.ToString())));
                        }

                    }
                    sb.Append("</tr>");
                }

                sb.Append("</table>");
                sb.Append("<script type='text/javascript'>if (typeof tableScroll == 'function') { tableScroll('mainTable'); }</script>");
                retVal = sb.ToString();

            }
        }

        return retVal;
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        pnlDisplayHtml.Text = DisplayDashboard();
    }
    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        BindUsers();
        pnlDisplayHtml.Text = DisplayDashboard();
    }
}