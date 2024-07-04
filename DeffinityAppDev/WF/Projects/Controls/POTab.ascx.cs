using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_POTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.Url.ToString().ToLower()).Contains("pojournal.aspx") == true)
        {
            lbtnPoJournal.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("podatabase.aspx") == true)
        {
            lbtnPoJournal.BackColor = System.Drawing.Color.White;
        }
        else if ((Request.Url.ToString().ToLower()).Contains("popurchase.aspx") == true)
        {
            lbtnPoPurchase.BackColor = System.Drawing.Color.White;
        }
        else
        {
            lbtnPoPurchase.BackColor = System.Drawing.Color.White;
        }

    }
}
