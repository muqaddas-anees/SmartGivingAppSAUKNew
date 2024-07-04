using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryMgt.DAL;
using InventoryMgt.Entity;

public partial class InventoryAdminDropdown : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SelectRblValues();
        }
    }
 
    public void SelectRblValues()
    {
        try
        {
            using (InventoryDataContext Idc = new InventoryDataContext())
            {
                var singleRecord = Idc.InventoryBatchOptions.Where(a => a.CustomerId == sessionKeys.PortfolioID).FirstOrDefault();
                if (singleRecord != null)
                {
                    CheckControlOption.Checked = singleRecord.BatchControl.Value;
                }
                else
                {
                    CheckControlOption.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void InsertOrUpdate()
    {
        try
        {
            using (InventoryDataContext Idc = new InventoryDataContext())
            {
                InventoryBatchOption singleRecord = Idc.InventoryBatchOptions.Where(a => a.CustomerId == sessionKeys.PortfolioID).FirstOrDefault();
                if (singleRecord != null)
                {
                    singleRecord.BatchControl = CheckControlOption.Checked;
                    singleRecord.DateTimeCreated = DateTime.Now;
                    singleRecord.UserId = sessionKeys.UID;
                    Idc.SubmitChanges();
                    lblMsg.Text = "Updated successfully";
                }
                else
                {
                    InventoryBatchOption singleRecordNew = new InventoryBatchOption();
                    singleRecordNew.CustomerId = sessionKeys.PortfolioID;
                    singleRecordNew.BatchControl = CheckControlOption.Checked;
                    singleRecordNew.UserId = sessionKeys.UID;
                    singleRecordNew.DateTimeCreated = DateTime.Now;
                    Idc.InventoryBatchOptions.InsertOnSubmit(singleRecordNew);
                    Idc.SubmitChanges();
                    lblMsg.Text = "Added successfully";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            InsertOrUpdate();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}