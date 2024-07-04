using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class Denomination : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindReligion();
                    BindGroup(0);
                    BindDenomination(0);
                    if (Request.QueryString["orgid"] != null)
                    {
                        btnSaveChanges.Visible = true;
                        // btnClose.Visible = true;
                        var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["orgid"].ToString())).FirstOrDefault();
                        if (pEntity != null)
                        {
                            ddlReligion.SelectedValue = (pEntity.DenominationDetailsID.HasValue ? pEntity.DenominationDetailsID.Value : 0).ToString();
                            BindGroup(Convert.ToInt32(ddlReligion.SelectedValue));
                            ddlGroup.SelectedValue = (pEntity.GroupDetailsID.HasValue ? pEntity.GroupDetailsID.Value : 0).ToString();
                            BindDenomination(Convert.ToInt32(ddlGroup.SelectedValue));
                            ddlDenimination.SelectedValue = (pEntity.SubDenominationDetailsID.HasValue ? pEntity.SubDenominationDetailsID.Value : 0).ToString();
                        }
                    }
                    else
                    {
                        btnSaveChanges.Visible = false;
                        //btnClose.Visible = false;
                        OrgTabs.Visible = false;
                    }

                }
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




        public static IQueryable<PortfolioMgt.Entity.DenominationGroupDetail> DenominationGroupDetailsBAL_Select()
        {
            IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();
            return pRep.GetAll();
        }

        private void BindGroup(int religionID)
        {
            try
            {
                ddlGroup.Items.Clear();
                if (religionID > 0)
                {
                  //  var rlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.DenominationDetailsID == religionID).OrderBy(o => o.Name).ToList();

                    var rlist = DenominationGroupDetailsBAL_Select().Where(o => o.DenominationDetailsID == religionID).OrderBy(o => o.Name).ToList();

                    

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

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               

                BindGroup(Convert.ToInt32(ddlReligion.SelectedValue));

                BindDenomination(0);
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


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelHeading.Text = "Add Religion";
                lblpoptitlte.Text = "Religion";
                ddlReligion.SelectedValue = "0";
                mdlManageOptions.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void btnAddGroup_Click(object sender, EventArgs e)
        {

            try
            {
               


                if (Convert.ToInt32(ddlReligion.SelectedValue) > 0)
                {
                    lblModelHeading.Text = "Add Group";

                    lblpoptitlte.Text = "Group";
                    ddlDenimination.SelectedValue = "0";
                    mdlManageOptions.Show();
                }
                else
                {

                }

                
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnAddDenimination_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlGroup.SelectedValue) > 0)
                {
                    lblModelHeading.Text = "Add Denomination";
                    lblpoptitlte.Text = "Denomination";
                    ddlDenimination.SelectedValue = "0";
                    mdlManageOptions.Show();
                }
                else
                {

                }

                   
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }





        protected void btnEditReligion_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelHeading.Text = "Edit Religion";
                lblpoptitlte.Text = "Religion";
                // hdid.Value = ddlDenimination.SelectedValue;
                if (Convert.ToInt32(ddlReligion.SelectedValue) > 0)
                {
                    hdid.Value = ddlReligion.SelectedValue;
                    txtAddReligion.Text = ddlReligion.SelectedItem.Text;
                    // ddlDenimination.SelectedValue = "0";
                    mdlManageOptions.Show();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnDeleteReligion_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (Convert.ToInt32(ddlReligion.SelectedValue) > 0)
                {

                    IPortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail> pRepSub = new PortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail>();
                    var pSub = pRepSub.GetAll().Where(o => o.DenominationDetailsID == Convert.ToInt32(ddlReligion.SelectedValue)).FirstOrDefault();
                    if (pSub != null)
                    {
                        pRepSub.Delete(pSub);

                    }


                    IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();
                    var p = pRep.GetAll().Where(o => o.DenominationDetailsID == Convert.ToInt32(ddlReligion.SelectedValue)).FirstOrDefault();
                    if (p != null)
                    {
                        pRep.Delete(p);

                    }

                    PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Delete(Convert.ToInt32(ddlReligion.SelectedValue));
                 //   DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Deletedsuccessfully, "");
                    BindReligion();
                    BindGroup(0);
                    BindDenomination(0);


                   
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }







        protected void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (Convert.ToInt32(ddlGroup.SelectedValue) > 0)
                {

                    IPortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail> pRepSub = new PortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail>();
                    var pSub = pRepSub.GetAll().Where(o => o.DenominationGroupDetailsID == Convert.ToInt32(ddlGroup.SelectedValue)).FirstOrDefault();
                    if (pSub != null)
                    {
                        pRepSub.Delete(pSub);

                    }


                    IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();
                    var p = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(ddlGroup.SelectedValue)).FirstOrDefault();
                    if (p != null)
                    {
                        pRep.Delete(p);
                        
                    }

                    

                    BindGroup(Convert.ToInt32(ddlReligion.SelectedValue));
                    BindDenomination(0);


                    
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }






        protected void btnDeleteDenomination_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (Convert.ToInt32(ddlDenimination.SelectedValue) > 0)
                {

                    IPortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail>();
                    var p = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(ddlDenimination.SelectedValue)).FirstOrDefault();
                    if (p != null)
                    {
                        pRep.Delete(p);
                       
                    }


                    PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Delete(Convert.ToInt32(ddlDenimination.SelectedValue));
                   // DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Deletedsuccessfully, "");
                    //BindReligion();
                    BindDenomination(Convert.ToInt32(ddlGroup.SelectedValue));


                   
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }






        protected void btnEditGroup_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelHeading.Text = "Edit RGroup";
                lblpoptitlte.Text = "Group";
                // hdid.Value = ddlDenimination.SelectedValue;
                if (Convert.ToInt32(ddlGroup.SelectedValue) > 0)
                {
                    hdid.Value = ddlGroup.SelectedValue;
                    txtAddReligion.Text = ddlGroup.SelectedItem.Text;
                    // ddlDenimination.SelectedValue = "0";
                    mdlManageOptions.Show();



                    //BindGroup(Convert.ToInt32(ddlReligion.SelectedValue));

                    //BindDenomination(0);



                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }






        protected void btnEditDenimination_Click(object sender, EventArgs e)
        {
            try
            {
                lblModelHeading.Text = "Edit Denomination";
                lblpoptitlte.Text = "Denomination";
                // hdid.Value = ddlDenimination.SelectedValue;
                if (Convert.ToInt32(ddlDenimination.SelectedValue) > 0)
                {
                    hdid.Value = ddlDenimination.SelectedValue;
                    txtAddReligion.Text = ddlDenimination.SelectedItem.Text;
                    // ddlDenimination.SelectedValue = "0";
                    mdlManageOptions.Show();






                  //  BindDenomination(Convert.ToInt32(ddlGroup.SelectedValue));





                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //btnSaveChangesPop_Click
        protected void btnSaveChangesPop_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(hdid.Value) > 0)
                {
                    if (lblModelHeading.Text.Contains("Religion"))
                    {
                        var rEntity = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(hdid.Value)).FirstOrDefault();
                        if (rEntity != null)
                        {
                            rEntity.Name = txtAddReligion.Text.Trim();
                            PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Update(rEntity);
                            hdid.Value = "0";
                            BindReligion();

                            mdlManageOptions.Hide();
                            //  DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "");
                        }
                    }
                    else if (lblModelHeading.Text.Contains("Group"))
                    {
                       // var rEntity = DenominationGroupDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(hdid.Value)).FirstOrDefault();

                        IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();

                        var rEntity = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(hdid.Value)).FirstOrDefault();

                        if (rEntity != null)
                        {
                            //  rEntity.Name = txtAddReligion.Text.Trim();

                            var data = Convert.ToInt32(hdid.Value);



                            var p = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(hdid.Value)).FirstOrDefault();
                            if (p != null)
                            {
                             //   p.DenominationDetailsID = Convert.ToInt32(ddlDenimination.SelectedValue);
                                p.Name= txtAddReligion.Text.Trim();

                                
                                pRep.Edit(p);

                                
                            }


                          
                            hdid.Value = "0";
                           // BindDenomination(Convert.ToInt32(ddlReligion.SelectedValue));

                            BindGroup(Convert.ToInt32(ddlReligion.SelectedValue));
                            //  DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "");

                            mdlManageOptions.Hide();
                        }

                    }
                    else if (lblModelHeading.Text.Contains("Denomination"))
                    {
                        var rEntity = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.ID == Convert.ToInt32(hdid.Value)).FirstOrDefault();

                        var data = Convert.ToInt32(hdid.Value);

                        if (rEntity != null)
                        {
                            rEntity.Name = txtAddReligion.Text.Trim();
                            PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Update(rEntity);
                            hdid.Value = "0";
                           // BindDenomination(Convert.ToInt32(ddlReligion.SelectedValue));

                            BindDenomination(Convert.ToInt32(ddlGroup.SelectedValue));

                            //  DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "");

                            mdlManageOptions.Hide();
                        }
                    }

                }
                else
                {
                    if (lblModelHeading.Text.Contains("Religion"))
                    {

                        if (txtAddReligion.Text.Trim().Length > 0)
                        {
                            PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Add(new PortfolioMgt.Entity.DenominationDetail() { IsActive = true, Name = txtAddReligion.Text.Trim() });
                            txtAddReligion.Text = string.Empty;
                            BindReligion();
                            mdlManageOptions.Hide();
                         //   DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Addedsuccessfully, "");
                        }
                    }
                    else if (lblModelHeading.Text.Contains("Group"))
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.DenominationGroupDetail>();


                        var rid = Convert.ToInt32(ddlReligion.SelectedValue);
                        if (rid > 0)
                        {
                            if (txtAddReligion.Text.Trim().Length > 0)
                            {

                                var p = new PortfolioMgt.Entity.DenominationGroupDetail();

                                var Hdid = Convert.ToInt32(hdid.Value);

                                    p.DenominationDetailsID = rid;
                                    p.Name = txtAddReligion.Text.Trim();


                                    pRep.Add(p);

                                

                                txtAddReligion.Text = string.Empty;

                                BindGroup(Convert.ToInt32(ddlReligion.SelectedValue));

                                mdlManageOptions.Hide();
                                //  DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Addedsuccessfully, "");
                            }

                        }

                    }
                    else if (lblModelHeading.Text.Contains("Denomination"))
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.SubDenominationDetail>();

                        var Gid = Convert.ToInt32(ddlGroup.SelectedValue);
                        if (Gid > 0)
                        {
                            if (txtAddReligion.Text.Trim().Length > 0)
                            {
                                var p = new PortfolioMgt.Entity.SubDenominationDetail();

                                var dId= Convert.ToInt32(ddlReligion.SelectedValue);

                                p.IsActive = true;
                                p.Name = txtAddReligion.Text.Trim();
                                p.DenominationDetailsID = Convert.ToInt32(ddlReligion.SelectedValue);
                                p.DenominationGroupDetailsID = Gid;

                                pRep.Add(p);

                                //PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Add(new PortfolioMgt.Entity.SubDenominationDetail()
                                //{
                                //    IsActive = true,
                                //    Name = txtAddReligion.Text.Trim(),
                                //    DenominationDetailsID = Convert.ToInt32(ddlDenimination.SelectedValue),
                                //    DenominationGroupDetailsID=Gid,
                                //});
                                txtAddReligion.Text = string.Empty;

                                BindDenomination(Convert.ToInt32(ddlGroup.SelectedValue));

                                mdlManageOptions.Hide();
                                //  DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, Resources.DeffinityRes.Addedsuccessfully, "");
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                var r = Convert.ToInt32(ddlReligion.SelectedValue);
                var d = Convert.ToInt32(ddlDenimination.SelectedValue);
                var g = Convert.ToInt32(ddlGroup.SelectedValue);

                if (Request.QueryString["orgid"] == null)
                {
                    var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(Request.QueryString["orgid"].ToString())).FirstOrDefault();
                    if (pEntity != null)
                    {
                        pEntity.DenominationDetailsID = r;
                        pEntity.SubDenominationDetailsID = d;
                        pEntity.GroupDetailsID = g;
                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Update(pEntity);
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        

      


        



        protected void btnSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/ContributionSettings.aspx", false);
        }
    }






   








}