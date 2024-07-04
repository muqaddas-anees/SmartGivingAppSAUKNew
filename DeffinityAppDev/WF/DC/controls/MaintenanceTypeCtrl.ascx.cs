using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class MaintenanceTypeCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    BindMaintenanceType();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private void BindMaintenanceType()
        {


            var sResult = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceType_Select(sessionKeys.PortfolioID);

            var cd = (from s in sResult
                      select new
                      {
                          s.ID,
                          s.Name
                      }).ToList();

            listAppliances.DataSource = cd.OrderBy(o => o.Name).ToList();
            listAppliances.DataBind();

        }

        protected void listAppliances_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
               
                if (e.CommandName == "Edit")
                {
                   

                }
                else if (e.CommandName == "UpdateItem")
                {
                    int ID = Convert.ToInt32(e.CommandArgument.ToString());
                    var dc = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceType_SelectByID(Convert.ToInt32(e.CommandArgument.ToString()));
                    TextBox txtName = (TextBox)e.Item.FindControl("txtName_e");
                    if (!PortfolioMgt.BAL.MaintenanceBAL.MaintenanceType_IsExists(txtName.Text.Trim(), sessionKeys.PortfolioID, Convert.ToInt32(e.CommandArgument.ToString())))
                    {
                        PortfolioMgt.BAL.MaintenanceBAL.MaintenanceType_Update(txtName.Text.Trim(),sessionKeys.PortfolioID,ID);
                        lblMsg_a.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                        listAppliances.EditIndex = -1;
                        BindMaintenanceType();
                    }
                    else
                    {
                        lblError_a.Text = "Item already exist";
                        //lblMsg1.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else if (e.CommandName == "Add")
                {
                  
                    TextBox txtName = (TextBox)e.Item.FindControl("txtName");
                    
                   

                    if (!PortfolioMgt.BAL.MaintenanceBAL.MaintenanceType_IsExists(txtName.Text,sessionKeys.PortfolioID))
                    {
                        PortfolioMgt.BAL.MaintenanceBAL.MaintenanceType_Add(txtName.Text.Trim(), sessionKeys.PortfolioID);
                         lblMsg_a.Text = "Added sucessfully";
                        BindMaintenanceType();
                    }
                    else
                    {
                        lblError_a.Text = "Item already exist";
                        //lblMsg1.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else if (e.CommandName == "Del")
                {
                    //cb = new CustomFieldsBAL();
                    if (Convert.ToInt32(e.CommandArgument) > 0)
                    {
                        var retval = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceType_DeleteByID(Convert.ToInt32(e.CommandArgument));
                        if (retval)
                        {
                            lblMsg_a.Text = "Deleted sucessfully";
                            BindMaintenanceType();
                        }
                    }
                }
                else if (e.CommandName == "Cancel")
                {

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void listAppliances_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            listAppliances.EditIndex = -1;
            //BindCustomFields();
        }
        protected void listAppliances_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            listAppliances.EditIndex = e.NewEditIndex;
            BindMaintenanceType();
        }
        protected void listAppliances_ItemDataBound(object sender, ListViewItemEventArgs e)
        {




        }
    }
}