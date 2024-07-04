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

public partial class DC_controls_FLSDocumentVisibilityCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
            BindData();
    }
    private void BindData()
    {
        int customerId = sessionKeys.PortfolioID;
        var flsSectionConfig = FLSSectionConfigBAL.GetFLSSectionConfigList().Where(f => f.CustomerID == customerId).FirstOrDefault();
        if (flsSectionConfig != null)
        {
            chkDocument.Checked = Convert.ToBoolean(flsSectionConfig.Document.HasValue ? flsSectionConfig.Document : true);
        }
        // default set to enabled
        if (flsSectionConfig == null)
        {
            chkDocument.Checked = true;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int customerId = sessionKeys.PortfolioID;
        var currentFLSSectionConfig = FLSSectionConfigBAL.GetFLSSectionConfigList().Where(f => f.CustomerID == customerId).FirstOrDefault();
        if (currentFLSSectionConfig == null)
        {
            FLSSectionConfig flsSectionConfig = new FLSSectionConfig();
            flsSectionConfig.Document = chkDocument.Checked;
            flsSectionConfig.CustomerID = customerId;
            FLSSectionConfigBAL.InsertSectionConfig(flsSectionConfig);
        }
        else
        {
            currentFLSSectionConfig.Document = chkDocument.Checked;
            FLSSectionConfigBAL.UpdateDocumentSection(currentFLSSectionConfig, customerId);
        }
        BindData();
        lblMsg.Text = "Updated successfully";
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
                                flsSectionConfig.Document = notification.Document;
                                fList.Add(flsSectionConfig);
                            }
                            else
                            {
                                fs.Document = notification.Document;
                                dc.SubmitChanges();
                            }
                        }
                        //Bulk insert
                        dc.FLSSectionConfigs.InsertAllOnSubmit(fList);
                        dc.SubmitChanges();
                        lblMsg.Text = "Successfully copied...";
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
                                flsSectionConfig.Document = true;
                                fList.Add(flsSectionConfig);
                            }
                            else
                            {
                                fs.Document = true;
                                dc.SubmitChanges();
                            }
                        }
                        //Bulk insert
                        dc.FLSSectionConfigs.InsertAllOnSubmit(fList);
                        dc.SubmitChanges();
                        lblMsg.Text = "Successfully copied...";
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