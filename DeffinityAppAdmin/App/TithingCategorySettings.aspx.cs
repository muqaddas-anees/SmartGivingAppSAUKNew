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
                                var nlist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == 0 && o.IsActive == true).ToList();
                                foreach (var p in nlist)
                                {
                                    p.OrganizationID = sessionKeys.PortfolioID;
                                    PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_Add(p);
                                }
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


                GridInstances.DataSource = plist;
                GridInstances.DataBind();

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
                    chkActive.Visible = plist.IsActive.HasValue ? plist.IsActive.Value : true;
                    chkActive.Checked = plist.IsHidden.HasValue ? plist.IsHidden.Value : false;

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

                        PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_Add(fData);
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