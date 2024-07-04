using PortfolioMgt.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Campaign
{
    public partial class CampaignTemplateTags : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    UpdateDonorsTag();
                    BindDefaultTags();
                    BindTags();
                    controlsbind(QueryStringValues.CTID);
                   
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void UpdateDonorsTag()
        {
            try
            {
                var pcTags = new PortfolioContactsTagsBAL();
                var uDonors = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o=>o.SID !=1).Where(o=>!o.Tags.Trim().ToLower().Contains("all donors")).ToList();
                //IUserRepository<UserMgt.Entity.UserSkill> uRep = new UserRepository<UserMgt.Entity.UserSkill>();
                //foreach(var u in uDonors)
                //{
                //    var uDetails = uRep.GetAll().Where(o => o.UserId == u.ID).FirstOrDefault();
                //    if(uDetails != null)
                //    {
                //       if(uDetails.Notes == null)
                //        {
                //            uDetails.Notes = "All, All Donors,";
                //        }
                //        else
                //        {
                //            if(uDetails.Notes.Length ==0)
                //            {
                //                uDetails.Notes = "All, All Donors,";
                //            }
                //            else
                //            uDetails.Notes = uDetails.Notes + "All Donors,";
                //        }

                //        uRep.Edit(uDetails);
                //    }
                //    else
                //    {
                //        var d = new UserMgt.Entity.UserSkill();
                //        d.UserId = u.ID;
                //        d.Notes = "All, All Donors,";
                        
                //        uRep.Add(d);
                //    }
                //}


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

                //update all donore with "All Donors" tag

        private void BindDefaultTags()
        {
            try
            {
                var pcTags = new PortfolioContactsTagsBAL();
                var uTags = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).Select(o => o.Tags).ToList();

                if( uTags.Count() >0)
                {
                    foreach(var t in uTags)
                    {
                        if(t.Trim().Length >0)
                        {
                            foreach (var tag in t.Split(','))
                            {
                                if(tag.Length >0)
                                {
                                    pcTags.PortfolioContactsTags_Add(tag.Trim());
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void controlsbind(int id)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
                var m = cRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
                if (m != null)
                {
                    lblTemplateName.Text = m.TemplateName;


                    try
                    {
                        if (m.Tags == null)
                        {
                            m.Tags = "All Donors,";
                            cRep.Edit(m);
                            ListBox_setValues("All Donors,");
                        }
                        else
                        {
                            ListBox_setValues(m.Tags);
                        }
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #region Bind Contacts
        public void BindTags()
        {
            var pcTags = new PortfolioContactsTagsBAL();

            //var rlist = pcTags.PortfolioContactsTags_SelectAll().ToList();
            //pcTags.a

            listTags.DataSource = pcTags.PortfolioContactsTags_SelectAll().ToList();
            //listTags.DataTextField = "Tag";
            //listTags.DataValueField = "ID";
            listTags.DataBind();
            listTags.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region listview set and get
        private void ListBox_setValues(string values)
        {
            if (values.Length > 0)
            {
                string[] svalues = values.Split(',');

                foreach (ListItem litem in listTags.Items)
                {
                    if (svalues.Contains(litem.Text) && !string.IsNullOrEmpty(litem.Text.Trim()))
                        litem.Selected = true;
                }
            }
        }
        private string ListBox_getValues()
        {
            string retval = string.Empty;
            foreach (ListItem litem in listTags.Items)
            {
                if (litem.Selected)
                    retval = retval + litem.Text + ",";
            }

            return retval;
        }
        #endregion

        protected void BtnAddTag_Click(object sender, EventArgs e)
        {
            var pcTags = new PortfolioContactsTagsBAL();
            if (!string.IsNullOrEmpty(txtTag.Text.Trim()))
            {
                pcTags.PortfolioContactsTags_Add(txtTag.Text.Trim());
                BindTags();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IPortfolioRepository<PortfolioMgt.Entity.CampaignTemplate> cRep = new PortfolioRepository<PortfolioMgt.Entity.CampaignTemplate>();
            var m = cRep.GetAll().Where(o => o.ID == QueryStringValues.CTID).FirstOrDefault();
            if (m != null)
            {
                m.Tags = ListBox_getValues();
                cRep.Edit(m);
            }

            //Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignTemplateDate.aspx?CTID=" + QueryStringValues.CTID, false);
            if (QueryStringValues.CSID > 0)
            {
                Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplateDate.aspx?CTID={0}&CSID={1}", +QueryStringValues.CTID, QueryStringValues.CSID), false);
            }
            else
            {
                Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplateDate.aspx?CTID={0}", +QueryStringValues.CTID), false);
            }
        }

            protected void btnBack_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID=" + QueryStringValues.CTID, false);
            if (QueryStringValues.CSID > 0)
            {
                Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID={0}&CSID={1}", +QueryStringValues.CTID, QueryStringValues.CSID), false);
            }
            else
            {
                Response.Redirect(string.Format("~/WF/CustomerAdmin/Campaign/CampaignTemplate.aspx?CTID={0}", +QueryStringValues.CTID), false);
            }
        }
    }
}