using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.DAL;
using DC.Entity;
using System.IO;
using DC.BAL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using Deffinity.IncidentService;

public partial class DCNewOrder : System.Web.UI.Page
{
    public double TotalSP = 0;
    public double TotalUnits = 0.00;
    public int TotalQuantity = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.ConnectionString = Constants.DBString;
        if (sessionKeys.CartID == Guid.Empty.ToString())
        {
            Guid _guid = Guid.NewGuid();
            sessionKeys.CartID = _guid.ToString();
        }

        if (!IsPostBack)
        {
            
            //ccdType.SelectedValue = "6"; // 6 for FLS
            //ccdStatus.SelectedValue = "22"; //22 for Status "New"
            ccdCompany.SelectedValue = sessionKeys.PortfolioID.ToString();
            ccdName.SelectedValue = CustomerDetailsBAL.GetCustomerUser_ContactID(sessionKeys.UID).ToString();
            
            //txtLoggedName.Text = sessionKeys.UName;
            
            //txtScheduledTime.Text = string.Format("{0:HH:mm}", DateTime.Now);
            FillCart();
           
        }
        //CompareValidator3.ValueToCompare = txtCreatedDate.Text;
    }
    public void FillCart()
    {
        TotalSP = 0;
        TotalUnits = 0.00;
        TotalQuantity = 0;
        //rptCart.DataBind();

        GridView1.DataBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        //FillCart();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update1")
        {

            int id = int.Parse(e.CommandArgument.ToString());
            int i = GridView1.EditIndex;
            GridViewRow Row = GridView1.Rows[i];
            //string qty = ((TextBox)Row.FindControl("txtqty")).Text;
            string Notes = ((TextBox)Row.FindControl("txtNotes")).Text;
            CartManager.UpdateCartNotes(id, Notes);

            GridView1.EditIndex = -1;
            FillCart();
        }
        if (e.CommandName == "Delete1")
        {
            CartManager.DeleteCartItem(Convert.ToInt32(e.CommandArgument));
            FillCart();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblid = e.Row.FindControl("lblID") as Label;
            if (lblid.Text == "-99")
            {
                e.Row.Visible = false;
            }
            else
            {

                TextBox txt = e.Row.FindControl("txtqty") as TextBox;

                Label lbp = e.Row.FindControl("lblitem") as Label;

                TotalQuantity += Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3]);
                TotalSP += Convert.ToDouble(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[4]);
                TotalUnits += Convert.ToDouble(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5]);
            }
        }
        

        if (e.Row.RowType == DataControlRowType.Footer)
        {
           

            Label qty = e.Row.FindControl("lbltqty") as Label;
            Label totalup = e.Row.FindControl("lbltotalSP") as Label;
            Label total_uc = e.Row.FindControl("lblftotalUnits") as Label;
            

            qty.Text = TotalQuantity.ToString();
            totalup.Text = string.Format("{0:f2}", TotalSP);
            total_uc.Text = string.Format("{0:f2}", TotalUnits);

        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        //FillCart();
    }

    private int GetSubjectId()
    {
        string subjectName = "Order";
        int id = 0;
        try
        {
            bool exists = FLSSubject.CheckExists(subjectName,sessionKeys.PortfolioID);
            Subject subject = new Subject();
            if (!exists)
            {
                subject.SubjectName = subjectName;
                subject.CustomerID = sessionKeys.PortfolioID;
                FLSSubject.Add(subject);
                id = subject.ID;
            }
            else
            {
                subject = FLSSubject.Bind().Where(p => p.SubjectName == subjectName).FirstOrDefault();
                id = subject.ID;
            }
            
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return id;
    }

    private int AddRecord(int statusID)
    {
        int id = 0;
        try
        {
            //change status - order type


            DateTime CurrentDateTime = DateTime.Now;
            // DC.SRV.WebService ws = new DC.SRV.WebService();
            /* Add Call Details */
            CallDetail cd = new CallDetail();
            //int rid = ws.GetContactID();
            PortfolioContact pc = CustomerDetailsBAL.GetPortfolioContactDetailsbyID(int.Parse(ddlName.SelectedValue));
            UserMgt.Entity.Contractor c = CustomerDetailsBAL.GetContractorDetailsbyID(sessionKeys.UID);
            string old_email = pc.Email;
            string old_telNo = pc.Telephone;
            //cd.SiteID = int.Parse(ddlSite.SelectedValue);
            cd.RequestTypeID = 6; // 6 for Type "FLS"
            cd.StatusID = statusID; //22 for Status "New"
            cd.CompanyID = int.Parse(ddlCompany.SelectedValue);
            int rid = int.Parse(ddlName.SelectedValue);
            cd.RequesterID = int.Parse(ddlName.SelectedValue);
            cd.LoggedBy = sessionKeys.UID;
            cd.LoggedDate = DateTime.Now;
            CallDetailsBAL.AddCallDetails(cd);
            id = cd.ID;
            AddCallDetailsJournal(cd, CurrentDateTime);

            /*Add FLS Details*/
            FLSDetail fd = new FLSDetail();
            fd.CallID = id;
            fd.SubjectID = GetSubjectId();
            fd.Details = txtDetails.Text.Trim();
            fd.ScheduledDate = DateTime.Now;
            //order should take as Service request
            fd.RequestType = 2;
            //fd.DepartmentID = int.Parse(ddlAssignedtoDept.SelectedValue);
            //fd.UserID = int.Parse(ddlAssignedtoName.SelectedValue);
            FLSDetailsBAL.AddFLSDetails(fd);

            AddFLSDetailsJournal(id, CurrentDateTime);
            /*Add FLS Time Details ie,Time Accumulated */
           
            FLSTimeDetail ft = new FLSTimeDetail();
            ft.CallID = id;
            ft.Status = StatusBAL.SelectbyId(statusID).Name.ToString();
            ft.StatusTime = DateTime.Now;
            FLSTimeDetailsBAL.AddFLSTimeDetail(ft);
           

            if ((old_email != txtReqEmailAddress.Text) || (old_telNo != txtReqTelNo.Text))
            {
                CustomerDetailsBAL.Update_ProfileDetails(int.Parse(ddlName.SelectedValue), sessionKeys.UID, txtReqEmailAddress.Text, txtReqTelNo.Text);
            }
          



        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return id;
    }
    private void AddFLSDetailsJournal(int cid, DateTime ModifiedDate)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            FLSDetailsJournal fj = new FLSDetailsJournal();
            fj.CallID = cid;
            fj.SubjectID = GetSubjectId();
            fj.Details = txtDetails.Text;
            fj.ScheduledDate = DateTime.Now;
            fj.ModifiedBy = sessionKeys.UID;
            fj.ModifiedDate = ModifiedDate;
            fj.VisibleToCustomer = true;
            ws.AddFLSDetailsJournal(fj);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void AddCallDetailsJournal(CallDetail cd, DateTime ModifiedDate)
    {
        try
        {
            DC.SRV.WebService ws = new DC.SRV.WebService();
            CallDetailsJournal cdj = new CallDetailsJournal();
            cdj.CallID = cd.ID;
            //cdj.SiteID = int.Parse(ddlSite.SelectedValue);
            cdj.RequestTypeID = 6; //6 for Type "FLS"
            cdj.StatusID = cd.StatusID; 
            cdj.CompanyID = int.Parse(ddlCompany.SelectedValue);
            cdj.RequesterID = cd.RequesterID;
            cdj.LoggedDate = cd.LoggedDate;
            cdj.LoggedBy = cd.LoggedBy;
            cdj.ModifiedBy = sessionKeys.UID;
            cdj.ModifiedDate = ModifiedDate;
            cdj.VisibleToCustomer = true;
            ws.AddCallDetailsJournal(cdj);
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void btnRequestQuote_Click(object sender, EventArgs e)
    {
       
        if (sessionKeys.IncidentID == 0)
        {
            //FLS status - 38	Quotation Requested
            sessionKeys.IncidentID = AddRecord(38);

            //insert if SD team is selected
            //IncidentAssignTeam IncTeam = new IncidentAssignTeam();
            //IncTeam.IncidentAssignTeam_Insert(sessionKeys.IncidentID, int.Parse(ddlTeam.SelectedValue), 0, "FLS");

            if (sessionKeys.IncidentID > 0)
            {
                ServiceManager.Services_BulkInsert(sessionKeys.IncidentID, sessionKeys.CartID, "FLS");
                SendDistributionMail();
                lblMsg.Visible = true;
                lblMsg.Text =   Message();
                lblMsg.ForeColor = System.Drawing.Color.Gray;
                pnlorder.Visible = false;
                linkCustomerRequest.Visible = true;
                sessionKeys.CartID = Guid.Empty.ToString();
               
            }
        }
    }
    private void SendDistributionMail()
    {
        try
        {
            CustomerOrderToSDteam1.Visible = true;
            List<int> idlist = SecurityAccessMail.GetIds(6,sessionKeys.PortfolioID); // 6 for FLS
            string company = ddlCompany.SelectedItem.Text;
            if (idlist.Count > 0)
            {
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();
                CustomerOrderToSDteam1.BindControls("ALL");
                List<string> strList = SecurityAccessMail.GetEmailAddresses(idlist);
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                CustomerOrderToSDteam1.RenderControl(htmlWrite);
                Email ToEmail =null;
                // mail to customer 
                //ToEmail = new Email();
                //ToEmail.SendingMail(txtReqEmailAddress.Text, "Order", htmlWrite.InnerWriter.ToString());
                //send mail to Distribution list
                if (strList.Count > 0)
                {
                    foreach (string s in strList)
                    {
                        ToEmail = new Email();
                        ToEmail.SendingMail(s, company+ " - Order", htmlWrite.InnerWriter.ToString());
                    }
                }

            }
            CustomerOrderToSDteam1.Visible = false;
        }
        catch (Exception ex)
        {
            CustomerOrderToSDteam1.Visible = false;
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    public string Message()
    {

        string strMsg = string.Empty;
        strMsg = "<table> ";
        strMsg = strMsg + "<tr> <td>";
        strMsg = strMsg + string.Format("Dear <b>" + sessionKeys.UName + "</b> ,</tr> </td>");
        strMsg = strMsg + "<tr> <td><p> Your request has been submitted. Please make a note of the following reference number. </p>";
        strMsg = strMsg + "</tr> </td>";
        strMsg = strMsg + "<tr> <td><p>Ticket Reference Number  <b>TN:" + sessionKeys.IncidentID + "</b></p>";
        strMsg = strMsg + "</tr> </td>";
        strMsg = strMsg + "<tr> <td><p>One of our representatives will be in touch with you soon either via email or by phone.</p>";
        strMsg = strMsg + "</tr> </td>";
        strMsg = strMsg + "<tr> <td>Thank you.";
        strMsg = strMsg + "</tr> </td>";
        strMsg = strMsg + "</table>";

        return strMsg;

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
       

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            int id, quantity;
            GridViewRow Row = GridView1.Rows[i];
            TextBox qty = (TextBox)Row.FindControl("txtqty");
            Label lblid = (Label)Row.FindControl("lblID");

            id = Convert.ToInt32(lblid.Text);
            quantity = Convert.ToInt32(qty.Text);
            CartManager.UpdateCart(id, quantity);
        }
        GridView1.DataBind();
    }
    protected void btnProcessOrder_Click(object sender, EventArgs e)
    {
       
        if (sessionKeys.IncidentID == 0)
        {
            //FLS status - 39	Order Approved
            sessionKeys.IncidentID = AddRecord(39);  //Insert FLS 

            //insert if SD team is selected
            //IncidentAssignTeam IncTeam = new IncidentAssignTeam();
            //IncTeam.IncidentAssignTeam_Insert(sessionKeys.IncidentID, int.Parse(ddlTeam.SelectedValue), 0, "FLS");

            if (sessionKeys.IncidentID > 0)
            {
                ServiceManager.Services_BulkInsert(sessionKeys.IncidentID, sessionKeys.CartID, "FLS");
                lblMsg.Visible = true;
                lblMsg.Text = Message();
                lblMsg.ForeColor = System.Drawing.Color.Gray;
                BuildMail_completeOreder();
                pnlorder.Visible = false;
                linkCustomerRequest.Visible = true;
                sessionKeys.CartID = Guid.Empty.ToString();
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Guid _g = new Guid(sessionKeys.CartID);
        CartManager.ClearCart(_g);
        FillCart();
    }
    private void BuildMail_completeOreder()
    {
        SDTeamToCustomer_Complete1.Visible = true;
        try
        {
            if (sessionKeys.PortfolioID > 0)
            {
                SDTeamToCustomer_Complete1.BindControls(ddlName.SelectedItem.Text.Trim(),txtReqEmailAddress.Text.Trim());

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                SDTeamToCustomer_Complete1.RenderControl(htmlWrite);
                Email ToEmail = new Email();
                ToEmail.SendingMail(txtReqEmailAddress.Text.Trim(), "Order", htmlWrite.InnerWriter.ToString());

            }

        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
        finally { SDTeamToCustomer_Complete1.Visible = false; }
    }
}