using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BAL;
using DC.DAL;
using DC.Entity;
using PortfolioMgt.DAL;
public partial class DC_FLSEmailNotificationCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    private void BindData()
    {
        int customerId = sessionKeys.PortfolioID;
        var flsSectionConfig = FLSSectionConfigBAL.GetFLSSectionConfigList().Where(f => f.CustomerID == customerId).FirstOrDefault();
        if (flsSectionConfig != null)
        {
            chkEmailCustomer.Checked = Convert.ToBoolean(flsSectionConfig.EmailCustomer.HasValue ? flsSectionConfig.EmailCustomer : true);
            chkEmailEngineer.Checked = Convert.ToBoolean(flsSectionConfig.EmailEngineer.HasValue ? flsSectionConfig.EmailEngineer : true);

        }
        // default set to enabled
        if (flsSectionConfig == null)
        {
            chkEmailCustomer.Checked = true;
            chkEmailEngineer.Checked = true;
        }
    }
    private void InsertUpdate()
    {
        int customerId = sessionKeys.PortfolioID;
        var currentFLSSectionConfig = FLSSectionConfigBAL.GetFLSSectionConfigList().Where(f => f.CustomerID == customerId).FirstOrDefault();
        if (currentFLSSectionConfig == null)
        {
            FLSSectionConfig flsSectionConfig = new FLSSectionConfig();
            flsSectionConfig.EmailEngineer = chkEmailEngineer.Checked;
            flsSectionConfig.EmailCustomer = chkEmailCustomer.Checked;
            flsSectionConfig.CustomerID = customerId;
            FLSSectionConfigBAL.InsertSectionConfig(flsSectionConfig);
        }
        else
        {
            currentFLSSectionConfig.EmailEngineer = chkEmailEngineer.Checked;
            currentFLSSectionConfig.EmailCustomer = chkEmailCustomer.Checked;
            FLSSectionConfigBAL.UpdateEmailSection(currentFLSSectionConfig,customerId);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdate();
        lblMsg.Text = "Updated successfully";
        BindData();
    }
    protected void btnCopyToAllCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;
            using (DCDataContext dc = new DCDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var customerListA = pd.ProjectPortfolios.Where(p => p.ID != customerID).ToList();
                    var customerListB = pd.ProjectPortfolios.ToList();
                    var notification = dc.FLSSectionConfigs.Where(c => c.CustomerID == customerID).FirstOrDefault();
                    if (notification != null)
                    {
                        List<FLSSectionConfig> fList = new List<FLSSectionConfig>();
                        foreach (var c in customerListA)
                        {
                            FLSSectionConfig fs = dc.FLSSectionConfigs.Where(f => f.CustomerID == c.ID).FirstOrDefault();
                            if (fs == null)
                            {
                                FLSSectionConfig flsSectionConfig = new FLSSectionConfig();
                                flsSectionConfig.CustomerID = c.ID;
                                flsSectionConfig.EmailCustomer = notification.EmailCustomer;
                                flsSectionConfig.EmailEngineer = notification.EmailEngineer;
                                fList.Add(flsSectionConfig);
                            }
                            else
                            {
                                fs.EmailCustomer = notification.EmailCustomer;
                                fs.EmailEngineer = notification.EmailEngineer;
                                dc.SubmitChanges();
                            }
                        }
                        //Bulk insert
                        dc.FLSSectionConfigs.InsertAllOnSubmit(fList);
                        dc.SubmitChanges();
                        lblMsg.Text = "Successfully copied";
                    }
                    else
                    {
                        List<FLSSectionConfig> fList = new List<FLSSectionConfig>();
                        foreach (var c in customerListB)
                        {
                            FLSSectionConfig fs = dc.FLSSectionConfigs.Where(f => f.CustomerID == c.ID).FirstOrDefault();
                            if (fs == null)
                            {
                                FLSSectionConfig flsSectionConfig = new FLSSectionConfig();
                                flsSectionConfig.CustomerID = c.ID;
                                flsSectionConfig.EmailCustomer = true;
                                flsSectionConfig.EmailEngineer = true;
                                fList.Add(flsSectionConfig);
                            }
                            else
                            {
                                fs.EmailCustomer = true;
                                fs.EmailEngineer = true;
                                dc.SubmitChanges();
                            }
                        }
                        //Bulk insert
                        dc.FLSSectionConfigs.InsertAllOnSubmit(fList);
                        dc.SubmitChanges();
                        lblMsg.Text = "Successfully copied";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}