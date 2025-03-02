﻿using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class ContactMaintenanceSchedule : System.Web.UI.Page
    {
        IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> cRepository = null;
        IPortfolioRepository<PortfolioMgt.Entity.v_PortfolioContactAddress> paRepository = null;
        IUserRepository<UserMgt.Entity.Contractor> uRepository = null;
        PortfolioContact pContact = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SetContactDetails();
            }
        }

        private void SetContactDetails()
        {
            try
            {
                if (Request.QueryString["ContactID"] != null)
                {
                    //pnladdress.Visible = true;
                    ////btnuser.Visible = true;
                    ////lbtnUpload.Visible = true;
                    int contactid = Convert.ToInt32(Request.QueryString["ContactID"]);
                    cRepository = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
                    pContact = cRepository.GetAll().Where(o => o.ID == contactid).FirstOrDefault();

                    if (pContact != null)
                    {
                        //    txtAddress.Text = pContact.Address1;
                        //    txtCity.Text = pContact.City;
                        //    txtEmail.Text = pContact.Email;
                        //    txtmobileno.Text = pContact.Mobile;
                        //    txtname.Text = pContact.Name;
                        //    txtPostal.Text = pContact.Postcode;
                        //    txtTelephone.Text = pContact.Telephone;
                        //    txtTown.Text = pContact.Town;
                        lblContact.Text = pContact.Name;
                        //    imguser.ImageUrl = "~/WF/Admin/ImageHandler.ashx?type=contact&id=" + pContact.ID.ToString();
                        //    // ListBox_setValues(pContact.Tags);
                    }
                    // Gridbind(Convert.ToInt32(Request.QueryString["ContactID"]));
                }
                else
                {
                    //btnuser.Visible = false;
                    //lbtnUpload.Visible = false;
                    //pnladdress.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}