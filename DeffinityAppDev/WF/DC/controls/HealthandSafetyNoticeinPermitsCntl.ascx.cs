using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using DC.Entity;
using ProjectMgt.DAL;
using PortfolioMgt.DAL;


public partial class DC_controls_HealthandSafetyNoticeinPermitsCntl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
          
        }
    }
    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            using (DCDataContext Dc = new DCDataContext())
            {
                SafetyNotice sn = Dc.SafetyNotices.Where(a => a.CustomerId ==int.Parse(ddlcustomer.SelectedValue)).FirstOrDefault();
                if (sn == null)
                {
                    SafetyNotice sn_New = new SafetyNotice();
                    sn_New.SafetyNotice1 = HttpUtility.HtmlEncode(editfooter.Text);
                    sn_New.UserId = sessionKeys.UID;
                    sn_New.DisplayHealthandSafetyNotice = CheckPermit.Checked;
                    sn_New.DateTimeCreated = DateTime.Now;
                    sn_New.CustomerId = int.Parse(ddlcustomer.SelectedValue);
                    Dc.SafetyNotices.InsertOnSubmit(sn_New);
                    Dc.SubmitChanges();
                    lblmsg.Text = "Added successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    sn.SafetyNotice1 = HttpUtility.HtmlEncode(editfooter.Text);
                    sn.UserId = sessionKeys.UID;
                    sn.DisplayHealthandSafetyNotice = CheckPermit.Checked;
                    sn.DateTimeCreated = DateTime.Now;
                    sn.CustomerId = int.Parse(ddlcustomer.SelectedValue);
                    Dc.SubmitChanges();
                    lblmsg.Text = "Updated successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                }
                ClearAll();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void ClearAll()
    {
        ddlcustomer.SelectedValue = "0";
        editfooter.Text = string.Empty;
        CheckPermit.Checked = false;
    }
    protected void btnCopyToAllCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            using (DCDataContext db = new DCDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var customerList = pd.ProjectPortfolios.Where(p => p.ID != int.Parse(ddlcustomer.SelectedValue)).ToList();
                    SafetyNotice snotice = db.SafetyNotices.Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue)).FirstOrDefault();
                    if (snotice != null)
                    {
                        List<SafetyNotice> snoticeList = new List<SafetyNotice>();
                        foreach (var c in customerList)
                        {
                            SafetyNotice snoticeNew = db.SafetyNotices.Where(f => f.CustomerId == c.ID).FirstOrDefault();
                            if (snoticeNew == null)
                            {
                                SafetyNotice ef = new SafetyNotice();
                                ef.CustomerId = c.ID;
                                ef.DateTimeCreated = DateTime.Now;
                                ef.DisplayHealthandSafetyNotice = snotice.DisplayHealthandSafetyNotice;
                                ef.UserId = snotice.UserId;
                                ef.SafetyNotice1 = snotice.SafetyNotice1;
                                snoticeList.Add(ef);
                            }
                            else
                            {
                                snoticeNew.SafetyNotice1 = snotice.SafetyNotice1;
                                snoticeNew.UserId = snotice.UserId;
                                snoticeNew.DisplayHealthandSafetyNotice = snotice.DisplayHealthandSafetyNotice;
                                snoticeNew.DateTimeCreated = DateTime.Now;
                                snoticeNew.SafetyNotice1 = snotice.SafetyNotice1;
                                db.SubmitChanges();
                            }
                        }

                        //Bulk insert
                        db.SafetyNotices.InsertAllOnSubmit(snoticeList);
                        db.SubmitChanges();
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Successfully copied";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        ClearAll();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(ddlcustomer.SelectedValue) > 0)
            {
                using (DCDataContext Dc = new DCDataContext())
                {
                    SafetyNotice sn = Dc.SafetyNotices.Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue)).FirstOrDefault();
                    if (sn != null)
                    {
                        editfooter.Text = HttpUtility.HtmlDecode(sn.SafetyNotice1);
                        CheckPermit.Checked = sn.DisplayHealthandSafetyNotice.HasValue ? sn.DisplayHealthandSafetyNotice.Value : false;
                    }
                    else
                    {
                        editfooter.Text = string.Empty;
                        CheckPermit.Checked = false;
                    }
                }
            }
            else
            {
                editfooter.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}