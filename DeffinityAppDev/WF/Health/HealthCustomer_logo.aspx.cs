using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthCheckMgt.Entity;
using HealthCheckMgt.DAL;

public partial class Customer_logo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (sessionKeys.PortfolioID > 0)
                {
                    InitialChecking();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    public void InitialChecking()
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                DN_Customerlogo record = Hdc.DN_Customerlogos.Where(a => a.CustomerId == sessionKeys.PortfolioID).FirstOrDefault();
                if (record != null)
                {
                    if (record.EmailTypeId.HasValue)
                    {
                        foreach (ListItem li in rblist.Items)
                        {
                            if (li.Value == record.EmailTypeId.Value.ToString())
                            {
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                    Btnsave.Text = "Update";
                }
                else
                {
                    foreach (ListItem li in rblist.Items)
                    {
                        if (li.Value == "1")
                        {
                            li.Selected = true;
                            break;
                        }
                    }
                    Btnsave.Text = "Submit";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void Btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                DN_Customerlogo record = Hdc.DN_Customerlogos.Where(a => a.CustomerId == sessionKeys.PortfolioID).FirstOrDefault();
                if (record == null)
                {
                    DN_Customerlogo record_new = new DN_Customerlogo();
                    record_new.CustomerId = sessionKeys.PortfolioID;
                    int CheckId = 0;
                    foreach (ListItem li in rblist.Items)
                    {
                        if (li.Selected == true)
                        {
                            CheckId = int.Parse(li.Value);
                            break;
                        }
                    }
                    record_new.EmailTypeId = CheckId;
                    Hdc.DN_Customerlogos.InsertOnSubmit(record_new);
                    Hdc.SubmitChanges();
                    lblMsg.Text = "Updated successfully";
                }
                else
                {
                    int CheckId = 0;
                    foreach (ListItem li in rblist.Items)
                    {
                        if (li.Selected == true)
                        {
                            CheckId = int.Parse(li.Value);
                            break;
                        }
                    }
                    record.EmailTypeId = CheckId;
                    Hdc.SubmitChanges();
                    lblMsg.Text = "Updated successfully";
                }
                InitialChecking();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}