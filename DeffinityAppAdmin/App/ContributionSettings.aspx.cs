using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class ContributionSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtPortfolioContribution.Text = "0.00";
                txtPortfolioDate.Text = "";
                txtDenominationContribution.Text = "0.00";
                txtDenominationDate.Text = "";
                txtGroupContribution.Text = "0.00";
                txtGroupDate.Text = "";

                BindReligion();
                BindGroup(0);
                BindDenomination(0);
                BindPortfolio(0);
            }
        }


        private void SetContributionSettings()
        {
            try
            {
               
                var g = Convert.ToInt32(ddlGroup.SelectedValue);
                var s = Convert.ToInt32(ddlDenimination.SelectedValue);
                var p = Convert.ToInt32(ddlProtfolio.SelectedValue);


                if (p > 0)
                {
                    var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == p).FirstOrDefault();
                    if (pEntity != null)
                    {
                        txtPortfolioContribution.Text = string.Format("{0:F2}", (pEntity.Contribution.HasValue ? pEntity.Contribution.Value : 0));
                        txtPortfolioDate.Text = pEntity.DateStamp.HasValue ? pEntity.DateStamp.Value.ToShortDateString() : "";
                    }
                }
                if (s > 0)
                {
                    var pEntity = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.ID == s).FirstOrDefault();
                    if (pEntity != null)
                    {
                        txtDenominationContribution.Text = string.Format("{0:F2}", (pEntity.Contribution.HasValue ? pEntity.Contribution.Value : 0));
                        txtDenominationDate.Text = pEntity.DateStamp.HasValue ? pEntity.DateStamp.Value.ToShortDateString() : "";
                    }
                }
                if (g > 0)
                {
                    var pEntity = PortfolioMgt.BAL.DenominationGroupDetailsBAL.DenominationGroupDetailsBAL_Select().Where(o => o.ID == g).FirstOrDefault();
                    if (pEntity != null)
                    {
                        txtGroupContribution.Text = string.Format("{0:F2}", (pEntity.Contribution.HasValue ? pEntity.Contribution.Value : 0));
                        txtGroupDate.Text = pEntity.DateStamp.HasValue ? pEntity.DateStamp.Value.ToShortDateString() : "";
                    }
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                //var r = Convert.ToInt32(ddlReligion.SelectedValue);
                var d = Convert.ToInt32(ddlDenimination.SelectedValue);
                var g = Convert.ToInt32(ddlGroup.SelectedValue);
                var p = Convert.ToInt32(ddlProtfolio.SelectedValue);
                //if (Request.QueryString["orgid"] == null)
                //{
                //update portfolio id
                if (p > 0)
                {
                    var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == p).FirstOrDefault();
                    if (pEntity != null)
                    {
                        pEntity.Contribution = Convert.ToDouble(txtPortfolioContribution.Text.Trim());
                        pEntity.DateStamp = Convert.ToDateTime(!string.IsNullOrEmpty( txtPortfolioDate.Text)? txtPortfolioDate.Text:DateTime.Now.ToShortDateString());

                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(pEntity);
                    }
                  
                }

                if (g > 0)
                {
                    //update group contribution
                    var dEntity = PortfolioMgt.BAL.DenominationGroupDetailsBAL.DenominationGroupDetailsBAL_Select().Where(o => o.ID == d).FirstOrDefault();
                    if (dEntity != null)
                    {
                        dEntity.Contribution = Convert.ToDouble(txtGroupContribution.Text.Trim());
                        dEntity.DateStamp = Convert.ToDateTime(!string.IsNullOrEmpty(txtPortfolioDate.Text) ? txtPortfolioDate.Text : DateTime.Now.ToShortDateString());

                        PortfolioMgt.BAL.DenominationGroupDetailsBAL.DenominationGroupDetailsBAL_Update(dEntity);
                    }
                }

                if (d > 0)
                {
                    var sEntity = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.ID == d).FirstOrDefault();
                    if (sEntity != null)
                    {
                        sEntity.Contribution = Convert.ToDouble(txtDenominationContribution.Text.Trim());
                        sEntity.DateStamp = Convert.ToDateTime(!string.IsNullOrEmpty(txtPortfolioDate.Text) ? txtPortfolioDate.Text : DateTime.Now.ToShortDateString());

                        PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Update(sEntity);
                    }
                }

                DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "");
                //  }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindReligion()
        {
            try
            {
                ddlReligion.Items.Clear();
                var rlist = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().OrderBy(o => o.Name).ToList();

                ddlReligion.DataSource = rlist;
                ddlReligion.DataTextField = "Name";
                ddlReligion.DataValueField = "ID";
                ddlReligion.DataBind();

                ddlReligion.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }




  

        private void BindGroup(int religionID)
        {
            try
            {
                ddlGroup.Items.Clear();
                if (religionID > 0)
                {
                    //  var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationDetailsID == religionID).OrderBy(o => o.Name).ToList();

                    var rlist = PortfolioMgt.BAL.DenominationGroupDetailsBAL.DenominationGroupDetailsBAL_Select().Where(o => o.DenominationDetailsID == religionID).OrderBy(o => o.Name).ToList();



                    ddlGroup.DataSource = rlist;
                    ddlGroup.DataTextField = "Name";
                    ddlGroup.DataValueField = "ID";
                    ddlGroup.DataBind();

                    ddlGroup.Items.Insert(0, new ListItem("Please select...", "0"));
                }
                else
                {
                    ddlGroup.Items.Insert(0, new ListItem("Please select...", "0"));
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindDenomination(int GroupId)
        {
            try
            {
                ddlDenimination.Items.Clear();
                if (GroupId > 0)
                {
                    var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationGroupDetailsID == GroupId).OrderBy(o => o.Name).ToList();

                    ddlDenimination.DataSource = rlist;
                    ddlDenimination.DataTextField = "Name";
                    ddlDenimination.DataValueField = "ID";
                    ddlDenimination.DataBind();

                    ddlDenimination.Items.Insert(0, new ListItem("Please select...", "0"));
                }
                else
                {
                    ddlDenimination.Items.Insert(0, new ListItem("Please select...", "0"));
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindPortfolio(int Denominationid)
        {
            try
            {
                ddlProtfolio.Items.Clear();
                if (Denominationid > 0)
                {
                    var rlist = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.SubDenominationDetailsID == Denominationid).OrderBy(o => o.PortFolio).ToList();

                    ddlProtfolio.DataSource = rlist;
                    ddlProtfolio.DataTextField = "PortFolio";
                    ddlProtfolio.DataValueField = "ID";
                    ddlProtfolio.DataBind();

                    ddlProtfolio.Items.Insert(0, new ListItem("Please select...", "0"));
                }
                else
                {
                    ddlProtfolio.Items.Insert(0, new ListItem("Please select...", "0"));
                }



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


                BindGroup(Convert.ToInt32(ddlReligion.SelectedValue));

                BindDenomination(0);
                SetContributionSettings();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindDenomination(Convert.ToInt32(ddlGroup.SelectedValue));
                SetContributionSettings();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void ddlDenimination_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindPortfolio(Convert.ToInt32(ddlDenimination.SelectedValue));
                SetContributionSettings();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlProtfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetContributionSettings();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}