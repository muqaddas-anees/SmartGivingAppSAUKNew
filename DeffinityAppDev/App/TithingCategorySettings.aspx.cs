using DC.BLL;
using DC.Entity;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{



    public partial class TithingCategorySettings : System.Web.UI.Page
    {
        public const string users = "users";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblModelHeading.Text = "Add Category";

                    try
                    {
                        if (sessionKeys.PortfolioID > 0)
                        {
                            var plist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID).ToList();
                            if (plist.Count == 0)
                            {
                                // var nlist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == 0 && o.IsActive == true).ToList();
                                //foreach (var p in nlist)
                                //{
                                var p = new TithingCategorySetting();
                                p.CategoryID = "1";
                                p.Description = "";
                                p.Name = "Donate to " + sessionKeys.PortfolioName;
                                p.IsActive = true;
                                p.IsHidden = false;
                                p.unid = Guid.NewGuid().ToString();
                                    p.OrganizationID = sessionKeys.PortfolioID;
                                    PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_Add(p);
                               // }
                                //copy new 
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                    BindGrid();



                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


      
        public void BindGrid()
        {
            try
            {
                List<PortfolioMgt.Entity.TithingCategorySetting> plist = new List<TithingCategorySetting>();

                if(QueryStringValues.Type == "hidden")
                {
                    plist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID && o.IsHidden == true).ToList();
                }
                else if(QueryStringValues.Type == "inactive")
                {
                    plist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID && o.IsActive == false).ToList();
                }
                else
                 plist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID && o.IsActive == true && o.IsHidden == false).ToList();


                if(plist.Where(o=>o.unid ==  null).Count() >0)
                {
                    foreach(var u in plist)
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting> pE = new PortfolioRepository<PortfolioMgt.Entity.TithingCategorySetting>();
                        var p = pE.GetAll().Where(o => o.ID == u.ID).FirstOrDefault();
                        if(p != null)
                        {
                            p.unid = Guid.NewGuid().ToString();
                            pE.Edit(p);
                            CreateAProject(p.unid, "category", p.Name, DateTime.Now, DateTime.Now.AddDays(1));
                        }
                    }
                }

                GridInstances.DataSource = plist;
                GridInstances.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void CreateAProject(string unid, string section, string title, DateTime startdate, DateTime enddate)
        {
            try
            {


                var fE = FLSDetailsBAL.FLSDetailsBAL_SelectAll().Where(o => o.UNID == unid).FirstOrDefault();

                if (fE == null)
                {

                    var cudate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddHours(9);

                    int incBy = 0;

                    var c = new CallDetail();
                    c.CompanyID = sessionKeys.PortfolioID;
                    c.LoggedBy = sessionKeys.UID;
                    c.LoggedDate = cudate;
                    c.RequesterID = sessionKeys.UID;
                    //6 default
                    c.RequestTypeID = 6;
                    c.SiteID = 0;
                    c.StatusID = JobStatus.Active;
                    CallDetailsBAL.AddCallDetails(c);
                    var CallID = c.ID;
                    //Journal entiry
                    CallDetailsJournalBAL.AddCallDetailsJournal(c);


                    var f = new FLSDetail();
                    f.CallID = c.ID;
                    f.CategoryID = 0;
                    f.ContactAddressID = sessionKeys.UID;
                    f.DateTimeStarted = cudate.AddHours(incBy);
                    f.DepartmentID = 0;
                    f.Details = title;
                    f.PriorityId = 0;
                    f.ScheduledDate = startdate;
                    //increment by hours
                    incBy = incBy + 2;
                    f.ScheduledEndDateTime = enddate;
                    f.DateTimeClosed = enddate;

                    f.SourceOfRequestID = 0;
                    f.SubCategoryID = 0;
                    f.SubjectID = 0;
                    f.UNID = unid;
                    f.Section = section;
                    var tReq = TypeOfRequestBAL.GetTypeOfRequestList().Where(o => o.Name == "Charity").FirstOrDefault();
                    if (tReq != null)
                    {

                        f.RequestType = tReq.ID;
                    }


                    //f.UserID = value.ID;
                    FLSDetailsBAL.AddFLSDetails(f);


                    //add to journal
                    FLSDetailsJournalBAL.AddFLSDetailsJournal(f);

                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();

        }
        protected void GridInstances_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //GridView gv = new GridView();
                //GridViewRow row = e.Row;

                //// Make sure we aren't in header/footer rows
                //if (row.DataItem == null)
                //{
                //    return;
                //}
                //else
                //{

                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }



        }
        protected void GridInstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridInstances.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void GridInstances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
             
                if(e.CommandName == "edit1")
                {
                    var plist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();
                    lblModelHeading.Text = "Edit Category";
                    hidTxtBox.Value = plist.ID.ToString();
                    txtName.Text = plist.Name;
                    txtCategoryID.Text = plist.CategoryID;
                    txtDescriptin.Text = plist.Description;
                    chkActive.Checked = plist.IsActive.HasValue ? plist.IsActive.Value : true;
                    chkHidden.Checked = plist.IsHidden.HasValue ? plist.IsHidden.Value : false;

                    mdlManageOptions.Show();


                }
                else
                    if(e.CommandName == "del")
                {
                    PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    BindGrid();
                    DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, Resources.DeffinityRes.Deletedsuccessfully);


                }




            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //btnAddOrganization
     
      
        //btnClose_Click
        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {

                var url = Request.RawUrl;

                url = url.Split('?')[0];



                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void clearFields()
        {
            txtCategoryID.Text = string.Empty;
            txtDescriptin.Text = string.Empty;
            txtName.Text = string.Empty;
            chkActive.Checked = true;
            chkHidden.Checked = false;

        }
        protected void btnSaveChangesPop_Click(object sender, EventArgs e)
        {
            try
            {
                var id = hidTxtBox.Value;

                if(Convert.ToInt32(hidTxtBox.Value) >0)
                {
                    var checkIsExists = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.ID != Convert.ToInt32(hidTxtBox.Value) && o.OrganizationID == sessionKeys.PortfolioID && o.Name.ToLower() == txtName.Text.ToLower().Trim()).FirstOrDefault();
                    if (checkIsExists == null)
                    {
                        var fData = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(hidTxtBox.Value)).FirstOrDefault();

                        if (fData != null)
                        {

                            fData.CategoryID = txtCategoryID.Text.Trim();
                            fData.Description = txtDescriptin.Text.Trim();
                            fData.IsActive = chkActive.Checked;
                            fData.IsHidden = chkHidden.Checked;
                            fData.Name = txtName.Text.Trim();

                            PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_Update(fData);
                            lblModelHeading.Text = "Add Category";
                            BindGrid();
                            hidTxtBox.Value = "0";
                            mdlManageOptions.Hide();
                            clearFields();
                            DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, Resources.DeffinityRes.UpdatedSuccessfully);

                        }
                    }
                    else
                    {
                        mdlManageOptions.Hide();
                        DeffinityManager.ShowMessages.ShowSuccessError(this.Page, sessionKeys.Message, "Category already exists");
                    }
                    //update the data

                }
                else
                {
                    var checkIsExists = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID && o.Name.ToLower() == txtName.Text.ToLower().Trim()).FirstOrDefault();
                    if (checkIsExists == null)
                    {
                        //Insert the data
                        var fData = new PortfolioMgt.Entity.TithingCategorySetting();
                        fData.CategoryID = txtCategoryID.Text.Trim();
                        fData.Description = txtDescriptin.Text.Trim();
                        fData.IsActive = chkActive.Checked;
                        fData.IsHidden = chkHidden.Checked;
                        fData.Name = txtName.Text.Trim();
                        fData.OrganizationID = sessionKeys.PortfolioID;
                        fData.unid = Guid.NewGuid().ToString();
                        PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_Add(fData);

                        CreateAProject(fData.unid, "category", fData.Name, DateTime.Now, DateTime.Now.AddDays(1));
                        mdlManageOptions.Hide();
                        clearFields();
                        DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, sessionKeys.Message, Resources.DeffinityRes.Addedsuccessfully);
                        BindGrid();
                    }
                    else
                    {
                        mdlManageOptions.Hide();
                        DeffinityManager.ShowMessages.ShowSuccessError(this.Page, sessionKeys.Message, "Category already exists");
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //var cdRep = new UserRepository<PortfolioMgt.Entity.AdvertisingBannerDetails>();
            //var cdEntity = new PortfolioMgt.Entity.AdvertisingBannerDetails();

           

          




        }







        protected static string EditBanner(string contactsId)
        {
            return "" + contactsId;
        }



        //protected void deleteInListView(object sender, EventArgs e)
        //{

        //    Button btn = (Button)sender;

        //    int ID = Int32.Parse(btn.CommandName);

        //    var cRep = new PortfolioRepository<PortfolioMgt.Entity.AdvertisingBannerDetails>();

        //    var list = cRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();


        //    cRep.Delete(list);


        //}

        //protected void btnDeleteReligion_Click(object sender, EventArgs e)
        //{
        //    Button btn = (Button)sender;

        //    var ID = btn.CommandName;
        //}



        //protected static string DeleteBanner(string contactsId)
        //{
        //    return "" + contactsId;
        //}






        protected void EditBannerInList(object sender, EventArgs e)
        {
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {

        }
    }


}