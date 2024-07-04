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

public partial class DC_controls_EmailFooter : System.Web.UI.UserControl
{
    public int RequestTypeID
    { set; get; }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            if (!IsPostBack)
            {
                BindRequestTypes();
                BindFooter();


            }
          
            HideControls();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        //CopyToAllCustomer show only for fls or service desk requesters
        if (Request.QueryString["tab"] != null)
        {
            //if(Request.QueryString["tab"].ToString().ToLower() == "fls")
            //btnCopyToAllCustomer.Visible = false;
            //else
            //    sessionKeys.PortfolioID = 0;
        }
    }
    #region Hide controls
    private void HideControls()
    {
        try
        {
            if (ddlRtype.SelectedValue == "0")
            {
                pnlfooter.Visible = false;

            }
            else
            {
                pnlfooter.Visible = true;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    #endregion

    #region Bind request types
    private void BindRequestTypes()
    {
        try
        {
            ddlRtype.DataSource = FooterEmail.RequestTypes_selectAll();
            ddlRtype.DataTextField = "Name";
            ddlRtype.DataValueField = "ID";
            ddlRtype.DataBind();
            ddlRtype.Items.Insert(0, new ListItem("Please select...", "0"));
            if (RequestTypeID > 0)
            {
                ddlRtype.SelectedValue = RequestTypeID.ToString();
                pnlLable.Visible = false;
            }
            else { pnlLable.Visible = true; }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Bind footer
    private void BindFooter()
    {
        try
        {
            int id = int.Parse((ddlRtype.SelectedValue).ToString());
            EmailFooter ef = new EmailFooter();
            //request is not FLS
            if(id != 6)
             ef = FooterEmail.EmailFooter_selectByID(id);
            else
                ef = FooterEmail.EmailFooter_selectByID(id, sessionKeys.PortfolioID);
            if (ef != null)
            {
               // editfooter.Text = HttpUtility.HtmlDecode(ef.EmailFooter1);
                editfooter.Text = ef.EmailFooter1;
            }
            pnlfooter.Visible = true;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    protected void imgbtnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            EmailFooter er = new EmailFooter();

            int id = int.Parse((ddlRtype.SelectedValue).ToString());
            //if it not FLS request
            if(id != 6)
            er = FooterEmail.EmailFooter_selectByID(id);
            else
                er = FooterEmail.EmailFooter_selectByID(id, sessionKeys.PortfolioID);

            if (er != null)
            {
                if (er.ID > 0)
                {
                    er.RequestTypeID = int.Parse(ddlRtype.SelectedValue);
                    //er.EmailFooter1 = HttpUtility.HtmlEncode(editfooter.Text);
                    er.EmailFooter1 = editfooter.Text;

                    FooterEmail.EmailFooter_update(er);
                    lblmsg.Text = "Updated successfully";
                    //lblmsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    AddFooter();
                }

            }
            else
            {
                AddFooter();

            }

            BindFooter();
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
           
        
    }

    private void AddFooter()
    {
        EmailFooter ef = new EmailFooter();
        ef.RequestTypeID = int.Parse(ddlRtype.SelectedValue);
        //ef.EmailFooter1 = HttpUtility.HtmlEncode(editfooter.Text);
        ef.EmailFooter1 = editfooter.Text;
        ef.customerID = sessionKeys.PortfolioID;
        FooterEmail.EmailFooter_Insert(ef);
        lblmsg.Text = "Added successfully";
        //lblmsg.ForeColor = System.Drawing.Color.Green;
    }
    protected void ddlRtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRtype.SelectedValue != "0")
            {
                EmailFooter ef = FooterEmail.EmailFooter_selectByID(int.Parse(ddlRtype.SelectedValue),sessionKeys.PortfolioID);
                if (ef != null)
                {

                    if (!string.IsNullOrEmpty(ef.EmailFooter1))
                    {
                        editfooter.Text = HttpUtility.HtmlDecode(ef.EmailFooter1);
                    }
                    else
                    {
                        editfooter.Text = string.Empty;
                    }
                    pnlfooter.Visible = true;

                }
                else
                {
                    editfooter.Text = string.Empty;
                }
            }

            else
            {
                pnlfooter.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
    protected void btnCopyToAllCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            int customerID = sessionKeys.PortfolioID;
            using (DCDataContext db = new DCDataContext())
            {
                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var footer = FooterEmail.EmailFooter_selectByID(RequestTypeID, sessionKeys.PortfolioID);
                    var customerList = pd.ProjectPortfolios.Where(p => p.ID != customerID).ToList();
                    if (footer != null)
                    {
                        List<EmailFooter> footerList = new List<EmailFooter>();
                        foreach (var c in customerList)
                        {
                            EmailFooter emailFooter = db.EmailFooters.Where(f => f.customerID == c.ID && f.RequestTypeID == RequestTypeID).FirstOrDefault();
                            if (emailFooter == null)
                            {
                                EmailFooter ef = new EmailFooter();
                                ef.customerID = c.ID;
                                ef.RequestTypeID = RequestTypeID;
                                ef.EmailFooter1 = footer.EmailFooter1;
                                footerList.Add(ef);
                            }
                            else
                            {
                                emailFooter.EmailFooter1 = footer.EmailFooter1;
                                db.SubmitChanges();
                            }
                        }

                        //Bulk insert
                        db.EmailFooters.InsertAllOnSubmit(footerList);
                        db.SubmitChanges();
                        lblmsg.Text = "Successfully copied";
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