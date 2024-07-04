using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using DC.Entity;
using DC.BAL;
using PortfolioMgt.DAL;

public partial class DC_controls_AccessEmailId : System.Web.UI.UserControl
{
    public string TypeofEmail
    {
        set;
        get;
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindAccessEmail();

            }
            lblsuccessemail.Text = string.Empty;
            lblEmail.Text = "From Email";
            //lblHeader.Text = TypeofEmail;

            //CopyToAllCustomer show only for fls or service desk requesters
            if (Request.QueryString["tab"] != null)
            {
                //if (Request.QueryString["tab"].ToString().ToLower() == "fls")
                //    btnCopyToAllCustomers.Visible = true;
                //else
                //    sessionKeys.PortfolioID = 0;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    private void BindAccessEmail()
    {
        try
        {
            AccessControlEmail acsmail = DefaultsOfAccessControl.AccessEmail_select(sessionKeys.PortfolioID);
            if (acsmail != null)
            {
                txtemail.Text = acsmail.EmailAddress;
                imgbtnaddmail.Visible = false;
                imgbtnupdateemail.Visible = true;
                //imgbtndel.Visible = true;
            }
            else
            {
                txtemail.Text = string.Empty;
                imgbtnaddmail.Visible = true;
                imgbtnupdateemail.Visible = false;
                //imgbtndel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void imgbtnaddmail_Click(object sender, EventArgs e)
    {
        try
        {
            int customerId = sessionKeys.PortfolioID;
            AccessControlEmail acsmail = DefaultsOfAccessControl.AccessEmail_select(customerId);

            if (acsmail == null)
            {
                AccessControlEmail mailid = new AccessControlEmail();
                mailid.EmailAddress = txtemail.Text;
                mailid.CustomerID = customerId;
                DefaultsOfAccessControl.AccessMail_Insert(mailid);
                lblsuccessemail.Text = "Email inserted successfully";
                //lblsuccessemail.ForeColor = System.Drawing.Color.Green;
                HttpContext.Current.Application["FromEmail"] = null;
            }

            BindAccessEmail();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgbtnupdateemail_Click(object sender, EventArgs e)
    {
        try
        {
            AccessControlEmail acsmail = DefaultsOfAccessControl.AccessEmail_select(sessionKeys.PortfolioID);
            if (acsmail != null)
            {
                acsmail.EmailAddress = txtemail.Text;
                DefaultsOfAccessControl.AccessEmail_update(acsmail);
                lblsuccessemail.Text = "Email updated successfully";
                //lblsuccessemail.ForeColor = System.Drawing.Color.Green;
                HttpContext.Current.Application["FromEmail"] = null;
            }
            BindAccessEmail();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgbtndel_Click(object sender, EventArgs e)
    {
        try
        {
            AccessControlEmail acsmail = DefaultsOfAccessControl.AccessEmail_select(sessionKeys.PortfolioID);
            if (acsmail != null)
            {
                int id = acsmail.ID;
                DefaultsOfAccessControl.AccessEmail_Delete(id);
                lblsuccessemail.Text = "Email deleted successfully";
                HttpContext.Current.Application["FromEmail"] = null;
                //lblsuccessemail.ForeColor = System.Drawing.Color.Green;
                BindAccessEmail();

            }
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       

    }
   
    protected void btnCopyToAllCustomers_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;
            using (DCDataContext db = new DCDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var emailList = db.AccessControlEmails.Where(m => m.CustomerID == sessionKeys.PortfolioID).ToList();
                    var customerList = pd.ProjectPortfolios.Where(p => p.ID != customerID).ToList();
                    if (emailList.Count() > 0)
                    {
                        List<AccessControlEmail> acsMailList = new List<AccessControlEmail>();
                        List<AccessControlEmail> acsMailListUpdate = new List<AccessControlEmail>();
                        foreach (var c in customerList)
                        {
                            foreach (var el in emailList)
                            {
                                AccessControlEmail acsmail = db.AccessControlEmails.Where(a => a.CustomerID == c.ID).FirstOrDefault();
                                if (acsmail == null)
                                {
                                    AccessControlEmail accessControlEmail = new AccessControlEmail();
                                    accessControlEmail.CustomerID = c.ID;
                                    accessControlEmail.EmailAddress = el.EmailAddress;
                                    acsMailList.Add(accessControlEmail);
                                }
                                else
                                {
                                    acsmail.EmailAddress = el.EmailAddress;
                                    db.SubmitChanges();
                                }
                            }
                        }

                        //Bulk insert
                        db.AccessControlEmails.InsertAllOnSubmit(acsMailList);
                        db.SubmitChanges();
                        lblsuccessemail.Text = "Successfully copied";
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