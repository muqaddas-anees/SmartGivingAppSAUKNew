using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class controls_TrainingSubTabs : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.Url.ToString().ToLower()).Contains("category.aspx") == true)
        {
            lbtnTrCategory.BackColor =  System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("deptreq.aspx") == true)
        {
            lbtnTrDepartment.BackColor = System.Drawing.Color.White;
        }
        else
        {
            lbtnTrNotification.BackColor = System.Drawing.Color.White;
        }
     
    }

  
}
