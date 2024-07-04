using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class DefaultSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                BindCountry();
                IProjectRepository<ProjectMgt.Entity.ProjectDefault> pdRep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                var p = pdRep.GetAll().FirstOrDefault();
                if (p != null)
                {
                    txtCountryCode.Text =p.Phonecode;
                    txtSiteName.Text = p.ApplicationName;
                    txtSiteUrl.Text = p.WebURL;
                    txtCity.Text = p.City_Display;
                    txtState.Text = p.State_Display;
                    txtPostcode.Text = p.Postcode;
                    ddlCulture.SelectedValue = p.Culture;
                    ddlCountry.SelectedValue =( p.CountryID.HasValue?p.CountryID.Value.ToString():"0");
                }

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                IProjectRepository<ProjectMgt.Entity.ProjectDefault> pdRep = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                var p = pdRep.GetAll().FirstOrDefault();
                if(p != null)
                {
                    p.Phonecode = txtCountryCode.Text;
                    p.ApplicationName = txtSiteName.Text;
                    p.WebURL = txtSiteUrl.Text ;
                    p.City_Display = txtCity.Text;
                    p.State_Display = txtState.Text;
                    p.Postcode = txtPostcode.Text ;
                    p.Culture = ddlCulture.SelectedValue ;
                    p.CountryID = Convert.ToInt32(  ddlCountry.SelectedValue);
                    p.CountryName = ddlCountry.SelectedItem.Text;
                    
                    pdRep.Edit(p);

                    Deffinity.systemdefaults.ClearCultureName();
                    Deffinity.systemdefaults.ClearPostcode();
                    Deffinity.systemdefaults.ClearStateName();
                    Deffinity.systemdefaults.ClearCityName();
                    Deffinity.systemdefaults.ClearCoutryName();
                    Deffinity.systemdefaults.ClearCoutryID();
                    Deffinity.systemdefaults.ClearCountryCode();
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, "Updated Successfully", "OK");
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}