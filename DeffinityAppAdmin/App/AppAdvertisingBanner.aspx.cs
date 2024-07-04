using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
   

    public partial class AppAdvertisingBanner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FirstFunction();", true);
                if (!IsPostBack)
                {
                    


                    

                    

                    if (Request.QueryString["orgid"] != null)
                    {
                       
                    }


                  //  var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == 124 ).FirstOrDefault();
                   


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

       

     
     
        //btnSaveChangesPop_Click
     
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {

            var cRep = new PortfolioRepository<PortfolioMgt.Entity.AppBannerDetail>();

            var value = new PortfolioMgt.Entity.AppBannerDetail();

            var text = CKEditorTextArea.Text;

            try
            {
                


                var religion = HiddenFieldReligion.Value;

                var Group = HiddenFieldGroup.Value;

                var Denomination = HiddenFieldDenomination.Value;




                value.PageTitle = txtPageTitle.Text;
                value.BannerImage = txtLinkUrl.Text;
                value.BannerClickUrl = txtLinkUrl.Text;
                value.Button1Text = txtBtnText1.Text;
                value.Button1Url = txtBtnUrl1.Text;
                value.Button2Text = txtBtnText2.Text;
                value.Button2Url = txtBtnUrl2.Text;
                value.PageBackGroupImage = "";
                value.ReligionID = Convert.ToInt32(religion);
                value.DenominationID = Convert.ToInt32(Group);
                value.GroupID = Convert.ToInt32(Denomination);






                cRep.Add(value);


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
              //  BindDenomination(Convert.ToInt32(ddlReligion.SelectedValue));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}