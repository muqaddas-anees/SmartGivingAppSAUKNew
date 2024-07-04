using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCFeedbackDashborad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<feeddisply> flist = new List<feeddisply>();
            flist.Add(new feeddisply() { col1 = "<img src='img/img1.png' />", col2 = "256", col3 = "233 <img src='img/img4.png' />", col4 = "943", col5 = "964 <img src='img/img4.png' />" });
            flist.Add(new feeddisply() { col1 = "<img src='img/img2.png' />", col2 = "322", col3 = "300 <img src='img/img5.png' />", col4 = "876", col5 = "890 <img src='img/img4.png' />" });
            flist.Add(new feeddisply() { col1 = "<img src='img/img3.png' />", col2 = "455", col3 = "345 <img src='img/img5.png' />", col4 = "1233", col5 = "1187 <img src='img/img5.png' />" });
            gridDashboard.DataSource = flist;
            gridDashboard.DataBind();
        }

        public class feeddisply
        {
            public string col1 { set; get; }
            public string col2 { set; get; }
            public string col3 { set; get; }
            public string col4 { set; get; }
            public string col5 { set; get; }
        }
    }
}