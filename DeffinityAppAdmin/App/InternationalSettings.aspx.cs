using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{


    public partial class InternationalSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindCountry();
                    //var rlist = 
                    
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
       
        private void BindCountry()
        {
            LocationRepository<Location.Entity.CountryClass> lcRep = new LocationRepository<Location.Entity.CountryClass>();
            var lc = lcRep.GetAll().Where(o => o.Visible == 'Y').OrderBy(o => o.Country1).ToList();
            if (lc.Count > 0)
            {
                ddlCountry.DataSource = lc;
                ddlCountry.DataTextField = "Country1";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                // ProjectPortFolioBAL
               

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

      
    }




   
}