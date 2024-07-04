using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using DC.Entity;
using DC.BAL;

public partial class DC_AccessControlDefaults : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!IsPostBack)
            {
                
                samAccessControl.RequestTypeID = 3; //Access Control
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}