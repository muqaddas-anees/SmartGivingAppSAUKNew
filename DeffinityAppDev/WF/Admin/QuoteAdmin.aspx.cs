using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.OleDb;
using Deffinity.ServiceCatalogManager;
using Quote.BLL;

public partial class Admin_QuoteAdmin : System.Web.UI.Page
{
   
    QuoteAdminManager QAdmin = new QuoteAdminManager();
    
    int ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {           
            //Master.PageHead="Quote Admin";
            BindData();
        }
        lblmsg.Text = string.Empty;
    }
    private void BindData()
    {
        try
        {
            ddlcustomer.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
            ddlcustomer.DataTextField = "PortFolio";
            ddlcustomer.DataValueField = "ID";
            ddlcustomer.DataBind();
        }
        catch (Exception ex)
        {
           LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            object obj;
            QuoteAdminEntity QAEntity = new QuoteAdminEntity();
            QAEntity.ID = Convert.ToInt32(HD_ID.Value);
            QAEntity.Portfolio = Convert.ToInt32(ddlcustomer.SelectedValue);
            QAEntity.Prefix = txtprefix.Text.Trim();
            QAEntity.QuoteStart = Convert.ToInt32(txtstartpoint.Text);
            QAEntity.VAT = float.Parse(txtvat.Text);
            QAEntity.Header = txtheader.Text.Trim();
            QAEntity.Footer = txtfooter.Text.Trim();
            QAEntity.FolderName = txtfolder.Text.Trim();
            QAEntity.ContactName = txtcontactname.Text.Trim();
            QAEntity.Email = txtemail.Text.Trim();
            QAEntity.Contactnumber = txtcontactno.Text.Trim();
            obj = QAdmin.QuoteAdminInsertUpdate(QAEntity);
            if (Convert.ToInt32(obj) == 1)
            {
                lblmsg.Text = "Details inserted successfully";
                ClearFields();
            }
            else if (Convert.ToInt32(obj) == 2)
            {
                lblmsg.Text = "Details updated successfully";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        

    }
    protected void ddlcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindID(Convert.ToInt32(ddlcustomer.SelectedValue));
        BindControls(Convert.ToInt32(ddlcustomer.SelectedValue));
    }
    private void BindID(int Portfolio)
    {
       HD_ID.Value= QAdmin.SelectID(Portfolio).ToString();
    }
    private void BindControls(int Portfolio)
    {
        try
        {
            QuoteAdminEntity Qa = new QuoteAdminEntity();
            Qa = QAdmin.SelectQuoteAdmin(0,Portfolio);
            HD_ID.Value = Qa.ID.ToString();
            txtprefix.Text = Qa.Prefix;
            txtstartpoint.Text = Qa.QuoteStart.ToString();
            txtvat.Text = Qa.VAT.ToString();
            txtheader.Text = Qa.Header;
            txtfooter.Text = Qa.Footer;
            txtfolder.Text = Qa.FolderName;
            txtcontactname.Text = Qa.ContactName;
            txtemail.Text = Qa.Email;
            txtcontactno.Text = Qa.Contactnumber;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            ClearFields();
        }

    }
    private void ClearFields()
    {
        txtprefix.Text = string.Empty;
        txtstartpoint.Text = string.Empty;
        txtvat.Text = string.Empty;
        txtheader.Text = string.Empty;
        txtfooter.Text = string.Empty;
        txtfolder.Text = string.Empty;
        txtcontactname.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtcontactno.Text = string.Empty;
    }
}
