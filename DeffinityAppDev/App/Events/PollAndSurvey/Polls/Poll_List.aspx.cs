using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.PollAndSurvey.Polls
{
    public partial class Poll_List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable dt;

            dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("College", typeof(string));
            dt.Rows.Add(1, "Rahul", "8505012345", "MITRC");
            dt.Rows.Add(2, "Pankaj", "8505012346", "MITRC");
            dt.Rows.Add(3, "Sandeep", "8505012347", "MITRC");
            dt.Rows.Add(4, "Sanjeev", "8505012348", "MITRC");
            dt.Rows.Add(5, "Neeraj", "8505012349", "MITRC");
            dt.AcceptChanges();



        }
    }
}