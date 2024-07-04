using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Deffinity.PortfolioContact;


public partial class controls_PortfolioContactsActivities : System.Web.UI.UserControl
{
    PorfolioContactsCS objPFContactCS = null;
    
    public string ActivityType
    {
        set
        {
            lblacitity.Text = value;
        }
    }
    private int ContactIdTemp;
    public string ContactId1
    {
        set
        {
            hiddencontactid.Value =  value;
        }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          objPFContactCS = new PorfolioContactsCS();
            drpContacts.DataSource = objPFContactCS.GetActiveContractors();
            drpContacts.DataValueField = "ID";
            drpContacts.DataTextField = "ContractorName";
            drpContacts.DataBind();
        }

    }

   

    protected void btnADD_Click(object sender, ImageClickEventArgs e)
    {

        //hiddencontactid.Value = ContactIdTemp.ToString();
        //int ContactId = Convert.ToInt32(hiddencontactid);
        objPFContactCS = new PorfolioContactsCS();
        string Subject = txttasksubject.Text.Trim();
        string ActivityType = lblacitity.Text.Trim();
        string Status = drpstatus.SelectedItem.Text.ToString();

        //DateTime DateofBirth = Convert.ToDateTime(string.IsNullOrEmpty(txtDateofBirth.Text.Trim()) ? "01/01/1900" : txtDateofBirth.Text.Trim());

        DateTime DueDate = Convert.ToDateTime(string.IsNullOrEmpty(txtTaskDate.Text.Trim()) ? "01/01/1900" : txtTaskDate.Text.Trim());
        lblnotesErrMsg.Visible = false;
        if (Subject != "" && Status != "")
        {
            int OwnerID = Convert.ToInt32(drpContacts.SelectedItem.Value.ToString());
            objPFContactCS.ActivityInsert(int.Parse(hiddencontactid.Value), Subject, ActivityType, Status, DueDate, OwnerID);

            ClearNewTaskFields();
            //this line will call the parent page (portfoliocontacts.aspx) public method, and passing value of contact id
            this.Page.GetType().InvokeMember("BindActivityGrid", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { int.Parse(hiddencontactid.Value) });
        }
        else
        {
            lblnotesErrMsg.Visible = true;
            lblnotesErrMsg.Text = "Please enter Subject";
        }

       
    }


    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ClearNewTaskFields();
    }
    private void ClearNewTaskFields()
    {
        txttasksubject.Text = "";
        drpstatus.SelectedIndex = -1;
        txtTaskDate.Text = "";
        drpContacts.SelectedIndex = -1;
    }
  

    protected void Button1_Click(object sender, EventArgs e)
   {
        
    }
   
}
