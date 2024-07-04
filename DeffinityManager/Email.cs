using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections;
using System.IO;
using System.Text;
using UserMgt.DAL;
using UserMgt.Entity;

/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{
    MailAddress addressFrom;
    MailAddress addressTo;
    SmtpClient mailClient = new SmtpClient();
    public string url = System.Configuration.ConfigurationManager.AppSettings["Weburl"];
    public string site = System.Configuration.ConfigurationManager.AppSettings["site"];

	public Email()
	{
        
		//
		// TODO: Add constructor logic here
		//
	}
    //type of mail
    //******************************************************************************
    //1. Live Project              2.Mitigation Action     3.Variation Raised
    //4. Variation Approved         5.Variation Denied      6.QA/UAT Issue
    //7. Interim QA Check Approval  8.QA Issue raised       9.QA APPROVED & COMPLETE
    //10.Customer Satisfaction
    //******************************************************************************
    
    
    public void sendMail(int Projectreference,int VariationID,int typeOfMail)
    {
        switch (typeOfMail)
        { 
            case 1:
                getResoourceData(Projectreference, "Live Project");
                break;
            case 2:
                MailData(Projectreference, "Mitigation Action");
                break;
            case 3:
                getVariationData(Projectreference, VariationID,typeOfMail, "Variation Raised");                
                break;
            case 4:
                getVariationData(Projectreference, VariationID, typeOfMail, "Variation Approved");                 
                break;
            case 5:
                getVariationData(Projectreference, VariationID, typeOfMail, "Variation Denied!!!");                 
                break;
            case 6:
                MailData(Projectreference, "QA/UAT Issue");
                break;
            case 7:
                MailData(Projectreference, "Interim QA Check Approval");
                break;
            case 8:
                MailData(Projectreference, "QA Issue raised");
                break;
            case 9:
                MailData(Projectreference, "QA APPROVED & COMPLETE");
                break;      
            case 10:
                //customer satisfaction
                SendRequestorMail(Projectreference);
                break;
            case 11:
                getVariationData(Projectreference, VariationID, typeOfMail, "Variation Raised", "Mail to Manager lists"); // send mail to assigned manager list
                break; 
        }
    
    }
    private void MailData(int Projectreference, string title)
    {
        //add header text
        
       string mailtext =  MailHeader(title)+"";

       //getProjectDetails
    }
    //mail header
    private void SendRequestorMail(int ProjectID)
    {
        string body = "";
        body = MailHeader("Customer Satisfaction");
        
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_PROJREQUESTORDETAILS");
        db.AddInParameter(cmd, "@ProjectID", DbType.Int32, ProjectID);
        using (IDataReader dr = db.ExecuteReader(cmd))
        {
            while (dr.Read())
            {
                body = body + "<br />";
                body = body + "Dear " + dr["RequestorName"].ToString();
                body = body + "<br /><p>";
                body = body + "This email is to inform you that Project " + dr["ProjectID"].ToString() + " ";
                body = body + dr["ProjectTitle"].ToString() +" is now complete. <br />";
                body = body + "We are always looking for ways to improve our service and would love to hear from you. If you have a few spare moments please <a href='" + url + "/Portal/CustomerSatisfaction.aspx?Project=" + ProjectID.ToString() + "'> click here </a> to send us your comments.<br />";
                body = body + "Thank you very much";
                body = body + "</body></html>";
                string subject="We would like to hear from you regarding the work carried out for Project number:"+dr["ProjectID"].ToString();
              
                if (dr["RequestorEmail"].ToString().Length >0)
                {
                    SendingMail(dr["RequestorEmail"].ToString(), subject, body);
                }
            }
        }
        
        

    }
    private string MailHeader(string mailTitleText)
    {
        string header = "";

        header = "<html><head><title>" + mailTitleText + "</title></head><body><div align='left'>" +
                                "<table border='0' cellpadding='0' cellspacing='0' width='100%' ><tr>" +
                                "<td colspan='2' style='font-size:x-large;color:#588526'>" + mailTitleText + "</td>" +
                                "<td colspan='2' align='right'><img src='" + url + "/WF/MailLogo/deffinity_emailer_logo.png'/></strong></td></tr>" +
                                "<tr><td colspan='4' style='height:50px'>&nbsp;</td></tr></table></div>";

        return header;
    }   
    private string mailHeaderText(string name,string Pref,string owner ,int type)
    {
        string strHeader = "";
        Pref = "<strong>" + Pref + "</strong>";
        strHeader = strHeader + "<tr><td colspan='4' style='height:20px;font-family:Verdana;'>Dear " + name + ",<br/><br/></td></tr>";
        string[] stemp = new string[9] {
                                        "This is to let you know that Project "+ Pref +" has been made live by "+owner+". ",
                                        "Within Project "+ Pref +" there is a risk mitigation action assigned to you. ",
                                        "The following variation has been raised against Project "+ Pref +".",
                                        "The following variation has been approved for Project "+ Pref +".",
                                        "The following variation has been Denied for Project "+ Pref +".",
                                        "Within Project "+ Pref +" there is a QA Issue assigned to you. ",
                                        "QA on Project "+ Pref +" has been approved on <Date>. ",
                                        "There has been an issue raised against Project "+ Pref +" during QA on <Date>. ",
                                        "QA/UAT has successfully been completed on Project "+ Pref +". "                                      
                                        };
        strHeader = strHeader + "<tr><td colspan='4' style='height:50px'>" + stemp[type -1] + "</td></tr>";
        return strHeader;
    }
    private string MailFooterText(int type,int variationID,int pref,string AuthorEmail)
    {
        string link = "<a href='" + url + "'>click here</a>";
        string AthourEmailLink = "<a href='mailto:" + AuthorEmail + "'>click here</a>";
        string strFooter = "";
        strFooter = strFooter + "<div align='left'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='font-family:Verdana;'><tr><td><br/><br/>";
        string[] stemp = new string[9] {
                                        "To update the project please "+ link +" to access the system. ",
                                        "To update the project please "+ link +" to access the system.",
                                        "To accept this variation please "+ getvarLink( variationID,true,pref)+".<br/><br/>To deny this variation please "+ getvarLink( variationID,false,pref) +".<br/><br/>If you would like to contact the author please "+ AthourEmailLink +" to email them. "
                                        ,"","",
                                        "To update the project please "+ link +" to access the system. ",
                                        "To update the project please "+ link +" to access the system. ",
                                        "To update the project please "+ link +" to access the system.",
                                        "To access the system please "+ link +". "                                      
                                        };
        strFooter = strFooter + stemp[type -1] + "</td><tr></table></div>";
        strFooter = strFooter + "<tr><td> <br/><br/>Thank you.<br/><br/><a href='" + url + "'> " + site + " </a>" + "<td></tr></table></div>";

        return strFooter;
    }
    //get variation link
    private string getvarLink(int ID,bool Approve,int pref)
    {
        //return "<a href='#' onclick=javascript:window.open('VariationApprove.aspx?ID=" + ID + "&Approve" + Approve + "','variation',toolbar=no,width=400,height=200,scrollbars=no,menubar=no,resizable=no );void('');> click here </a>";
        return "<a href='" + url + "/WF/Projects/VariationApprove.aspx?Project=" + pref + "&ID=" + ID + "&Approve=" + Approve + "' target='_blank' onclick='window.open(" + url + "/WF/Projects/VariationApprove.aspx?Project=" + pref + "&ID=" + ID + "&Approve=" + Approve + ",'variation','height=200,width=400,status=yes,toolbar=no,menubar=no,location=no,resizable=no' )';return false;> click here </a>";
    }
    //get project information
    private string getProjectDetails(int projectReference)
    {
        
        string Projectdetails = "";
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd = db.GetStoredProcCommand("DN_SelectProjectDetails");
        db.AddInParameter(cmd, "@ProjectReference", DbType.Int32,projectReference);
        using (IDataReader dr = db.ExecuteReader(cmd))
        {
            while (dr.Read())
            {
                Projectdetails = "<div align='left'><table border='1' cellpadding='0' cellspacing='0' width='100%' bordercolordark='#FFFFFF' style='font-family:Verdana;'><tr>" +
                                   "<tr><td colspan='4' style='background:#8595a6;font-size:12px;font-weight:bold;color:#fff;padding:7px;margin-bottom:10px;'>Details of the project are as follows:</td></tr>" +
                                   "<tr><td colspan='4' style='height:20px'>&nbsp;</td></tr>" +
                                   "<td width='20%'><strong>Country</strong></td><td width='30%'>" + dr["Country"].ToString() + "</td>" +
                                   "<td><b width='20%'>City</b></td><td width='30%'>" + dr["City"].ToString() + "</td>" + "</tr><tr>" +
                                   "<td><b>Site</b></td><td>" + dr["Site"].ToString() + "</td>" +
                                   "<td><b></b></td><td></td>" + "</tr><tr>" +
                                   "<tr><td colspan='4' style='height:20px'>&nbsp;</td></tr>" +
                                   "<td><b>Details</b></td><td colspan='3'>" + dr["ProjectDescription"].ToString() + "</td>" + "</tr><tr>" +
                                   "<td><b>Start Date</b></td><td>" + string.Format("{0:d}", dr["StartDate"]) + "</td>" +
                                   "<td><b>Expected End Date</b></td><td>" + string.Format("{0:d}", dr["ProjectEndDate"]) + "</td>" + "</tr><tr>" +
                                   "<tr><td colspan='4' style='height:20px'>&nbsp;</td></tr>" +
                                   "<td><b>Project Owner</b></td><td>" + dr["ContractorName"].ToString() + "</td>" +
                                   "<td><b>Email</b></td><td>" + dr["EmailAddress"].ToString() + "</td></tr><tr>" +
                                   "<td><b>Mobile</b></td><td>&nbsp;</td>" +
                                   "<td><b>Program</b></td><td>" + dr["OperationsOwners"].ToString() + "</td>" + "</tr><tr>" +
                                   "<td><b></b></td><td></td>" +
                                   "<td><b>&nbsp;</b></td><td>&nbsp;</td>" + "</tr><tr>" +                                  
                                   "</table></div>";
            }
        
        }       

        return Projectdetails;
       
    }   
    #region sending mail to resources
    //get resources project items
    private void getResoourceData(int projectReference,string mailtext)
    { 
        Hashtable hs = new Hashtable();        
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd = db.GetStoredProcCommand("DN_ProjectResources");
        db.AddInParameter(cmd, "@ProjectReference", DbType.Int32,projectReference);
        using (IDataReader dr = db.ExecuteReader(cmd))
        {
            while (dr.Read())
            {
                hs.Add(dr["ContractorID"].ToString(), dr["ContractorName"].ToString());
            }
        }
        cmd.Dispose();
        //take for each resource
        foreach (string resource in hs.Keys)
        {
            //get resources email id and name
            getResourceId(projectReference, Convert.ToInt32(resource), getResources(projectReference, Convert.ToInt32(resource)), mailtext);
            

        }

    }
    //get mailids
    private void getResourceId(int pref,int cid, string itemtext,string mailtext)
    {
        string name = "";
        string email = "";
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd1 = db.GetStoredProcCommand("DN_SelectResourceID");
        db.AddInParameter(cmd1, "@cid", DbType.Int32, cid);
        using (IDataReader dr = db.ExecuteReader(cmd1))
        {
            while (dr.Read())
            {
                name = dr["ContractorName"].ToString();
                email = dr["EmailAddress"].ToString();
            }
        }
        if (email != "")
        {
            string p = sessionKeys.Prefix + pref.ToString();
            //combile string  
            string Mtext = MailHeader(mailtext) + mailHeaderText(name, p, "", 1);
            Mtext = Mtext + getProjectDetails(pref);
            Mtext = Mtext + itemtext + MailFooterText(1, 0, pref,"");

            SendingMail(email, mailtext, Mtext);
        }

    }
    //get one resource data
    private string getResources(int pref,int cid)
    {
        ArrayList ar = new ArrayList();
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd1 = db.GetStoredProcCommand("DN_SelectResourceItems");        
        db.AddInParameter(cmd1, "@ProjectReference", DbType.Int32, pref);
        db.AddInParameter(cmd1, "@ContractorID", DbType.Int32, cid);
        using (IDataReader dr = db.ExecuteReader(cmd1))
        {
            while (dr.Read())
            {
                ar.Add(new ItemsList(dr["ItemDescription"].ToString(),string.Format(Deffinity.systemdefaults.GetStringDateformat(),dr["ProjectStartDate"]), string.Format(Deffinity.systemdefaults.GetStringDateformat(),dr["ProjectEndDate"])));
            }
        }       
        //display collection
        //no of rows
        int cnt = ar.Count + 1;
        string res = "";
        res = res + "<div><table border='1' cellpadding='0' cellspacing='0' width='100%' style='font-family:Verdana;'>";
        res = res + "<tr><td colspan='3' >&nbsp;</td></tr>";
        res = res + "<tr><td colspan='3' style='background:#FFCF87;font-size:12px;font-weight:bold;padding:7px;margin-bottom:10px;'>The list of activities assigned to you are as follows:</td></tr>";
        res = res + "<tr><td colspan='3' >&nbsp;</td></tr>";
        res = res + "<tr style='background:#FFEEC1;font-size:12px;font-weight:bold;padding:7px;margin-bottom:10px;height:30px'><td width='60%'>Item</td><td width='20%'>Start Date</td><td width='20%'>End Date</td></tr>";
        res = res + "<tr><td><td colspan='3'></td></tr>";
        string temp = "";
        foreach (ItemsList s in ar)
        {
            temp = temp + s.ToString();             
        }
        res = res + temp + "</table></div>";
        ar.Clear();        
        return res;
        
    }
    //get variation data
    #endregion
    private void getVariationData(int pref,int VariationID,int type, string Mailtext)
    {
        string res = "";
        string email = "";
        string name = "";
        int cid = 0;
        int customerId = 0;
        string AuthorEmail = "";
        bool approve = false;
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd1 = db.GetStoredProcCommand("getVariationRaisedDetailsForMail");
        db.AddInParameter(cmd1, "@ProjectReference", DbType.Int32, pref);
        db.AddInParameter(cmd1, "@ID", DbType.Int32, VariationID);       
        
        using (IDataReader dr = db.ExecuteReader(cmd1))
        {
            while (dr.Read())
            {
                res = res + "<div><table border='1' cellpadding='0' cellspacing='0' width='100%' style='font-family:Verdana;'>";
                res = res + "<tr><td colspan='2' >&nbsp;</td></tr>";
                res = res + "<tr><td colspan='2' style='background:#8595a6;font-size:12px;font-weight:bold;color:#fff;padding:7px;margin-bottom:10px;'>Details of the variation are as follows:</td></tr>";
                res = res + "<tr><td colspan='2' >&nbsp;</td></tr>";
                res = res + "<tr style='background:#95A3B3;font-size:12px;font-weight:bold;padding:7px;color:#fff;margin-bottom:10px;height:30px'><td>Details</td><td></td></tr>";
                res = res + "<tr><td height='20px' with='35%'>Variation raised by</td><td with='65%'>" + dr["RequesterName"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Author's contact number</td><td>" + dr["Telephone"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Author's Email</td><td>" + dr["EmailAddress"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Date</td><td>" + dr["DateRaised"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Details</td><td>" + dr["Description"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Justification</td><td >" +dr["Justification"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Value</td><td style='text-align:right;'>" + string.Format("{0:F}", dr["DeviationValue"]) + "</td></tr>";
                res = res + "<tr><td  height='20px'>Authoriser</td><td>" + dr["Approversname"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'></td><td>" + dr["ApproversEmail"].ToString() + "</td></tr>";
                AuthorEmail = dr["EmailAddress"].ToString();
                //approval check if
                if (dr["ApprovalStatus"].ToString() == "")
                {
                    email = dr["ApproversEmail"].ToString();
                    name = dr["Approversname"].ToString();
                } 
                 // get contractor id
                cid = Convert.ToInt32(dr["ContractorID"].ToString());
                customerId=Convert.ToInt32(dr["CustomerID"].ToString());
                }
        }       
        res = res  + "</table></div>";

        if (type == 3)
        {
            cmd1 = db.GetStoredProcCommand("DN_SelectResourceID");
            db.AddInParameter(cmd1, "@cid", DbType.Int32, customerId);
            using (IDataReader dr = db.ExecuteReader(cmd1))
            {
                while (dr.Read())
                {
                    name = dr["ContractorName"].ToString();
                    email = dr["EmailAddress"].ToString();
                }
            }
        }
        else
        {
            //checking approval
            if (email == "")
            {
                cmd1 = db.GetStoredProcCommand("DN_SelectResourceID");
                db.AddInParameter(cmd1, "@cid", DbType.Int32, cid);
                using (IDataReader dr = db.ExecuteReader(cmd1))
                {
                    while (dr.Read())
                    {
                        name = dr["ContractorName"].ToString();
                        email = dr["EmailAddress"].ToString();
                    }
                }
            }
        }
       
        string p = sessionKeys.Prefix + pref.ToString();
        string subject = "Variation for Project " + p + " needs your approval";
        //combile string  
        string Mtext = MailHeader(Mailtext) + mailHeaderText(name, p, "", type);
        Mtext = Mtext + getProjectDetails(pref);
        Mtext = Mtext + res + MailFooterText(type, VariationID, pref, AuthorEmail);
        SendingMail(email, type == 3 ? subject : Mailtext, Mtext);
        //Save to Inbox
        InboxBAL.SaveInboxMessage(type == 3 ? subject : Mailtext, type == 3 ? customerId : cid, email, Mtext);
    }
    private void getVariationData(int pref, int VariationID, int type, string Mailtext, string text)
    {
        string res = "";
        string email = "";
        string name = "";
        DataTable dt = new DataTable();

        int cid = 0;
        string AuthorEmail = "";
        bool approve = false;
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd1 = db.GetStoredProcCommand("getVariationRaisedDetailsForMail");
        db.AddInParameter(cmd1, "@ProjectReference", DbType.Int32, pref);
        db.AddInParameter(cmd1, "@ID", DbType.Int32, VariationID);


        using (IDataReader dr = db.ExecuteReader(cmd1))
        {
            while (dr.Read())
            {
                res = res + "<div><table border='1' cellpadding='0' cellspacing='0' width='100%' style='font-family:Verdana;'>";
                res = res + "<tr><td colspan='2' >&nbsp;</td></tr>";
                res = res + "<tr><td colspan='2' style='background:#8595a6;font-size:12px;font-weight:bold;padding:7px;color:#fff;margin-bottom:10px;'>Details of the variation are as follows:</td></tr>";
                res = res + "<tr><td colspan='2' >&nbsp;</td></tr>";
                res = res + "<tr style='background:#95A3B3;font-size:12px;font-weight:bold;padding:7px;color:#fff;margin-bottom:10px;height:30px'><td>Details</td><td></td></tr>";
                res = res + "<tr><td height='20px' with='35%'>Variation raised by</td><td with='65%'>" + dr["RequesterName"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Author's contact number</td><td>" + dr["Telephone"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Author's Email</td><td>" + dr["EmailAddress"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Date</td><td>" + dr["DateRaised"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Details</td><td>" + dr["Description"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Justification</td><td>" + dr["Justification"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'>Value</td><td style='text-align:right;'>" + string.Format("{0:F}", dr["DeviationValue"]) + "</td></tr>";
                res = res + "<tr><td  height='20px'>Authoriser</td><td>" + dr["Approversname"].ToString() + "</td></tr>";
                res = res + "<tr><td  height='20px'></td><td>" + dr["ApproversEmail"].ToString() + "</td></tr>";
                AuthorEmail = dr["EmailAddress"].ToString();
                //approval check if
                if (dr["ApprovalStatus"].ToString() == "")
                {
                    email = dr["ApproversEmail"].ToString();
                    name = dr["Approversname"].ToString();
                }
                // get contractor id
                cid = Convert.ToInt32(dr["ContractorID"].ToString());

            }
        }
        res = res + "</table></div>";

        

        string p = sessionKeys.Prefix + pref.ToString();
        //combile string  
        string subject = "Variation for Project " + p + " needs your approval";

        using (IDataReader dr = db.ExecuteReader(CommandType.Text, "SELECT ID,ManagerID,(SELECT ContractorName from Contractors WHERE ID = ManagerID) AS ManagerName, (SELECT EmailAddress from Contractors WHERE ID = ManagerID) AS ManagerEmail FROM VariationPermission WHERE UserID=" + cid))
        {
            while (dr.Read())
            {
               

                string managerName = dr["ManagerName"].ToString();
                int managerId = Convert.ToInt32(dr["ManagerID"]);
                string managerEmail = dr["ManagerEmail"].ToString();
                string Mtext = MailHeader(Mailtext) + mailHeaderText(managerName, p, "", 3);
                Mtext = Mtext + getProjectDetails(pref);
                Mtext = Mtext + res + MailFooterText(3, VariationID, pref, AuthorEmail);
                SendingMail(managerEmail, subject, Mtext);
                //Save to Inbox 
                InboxBAL.SaveInboxMessage(subject, managerId, managerEmail, Mtext);
            }
        }

    }
    
    public static void sendingMail(MailMessage msg)
    {
        SmtpClient mailClient = new SmtpClient();
        try 
        {
            mailClient.Send(msg);
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    //add project information to mail text  
    public void SendingMail(string ToEmailID,string Subject,string strHTML,string ccEmail = "",string BCC1 = "", string BCC2 = "")
    {
        try
        {
            addressFrom = new MailAddress(Deffinity.systemdefaults.GetFromEmail());
            addressTo = new MailAddress(ToEmailID);
            MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
            if (!string.IsNullOrEmpty(ccEmail))
                mailMessage.CC.Add(ccEmail);
            if(!string.IsNullOrEmpty( BCC1))
            mailMessage.Bcc.Add(new MailAddress( BCC1));
            if (!string.IsNullOrEmpty(BCC2))
                mailMessage.Bcc.Add(new MailAddress(BCC2));
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = strHTML;
            mailClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "mail.send function");
        }
    }

    public void TimesheetMailIDs(string  ToEmailID, string Subject, string strHTML)
    {
        try
        {
            addressFrom = new MailAddress(Deffinity.systemdefaults.GetFromEmail());
            addressTo = new MailAddress(ToEmailID);
            MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
            //mailMessage.CC.Add("k.indrasenareddy@gmail.com");
            // mailMessage.CC.Add("Nadeem.Mohammed@linemanagement.co.uk");
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = strHTML;
            mailClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "mail.send function");
        }
    }
    public void SendingMail(string ToEmailID, string Subject, string strHTML, Attachment a, string ccEmail = "")
    {
        try
        {
            addressFrom = new MailAddress(Deffinity.systemdefaults.GetFromEmail());
            addressTo = new MailAddress(ToEmailID);
            MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
           
            if (!string.IsNullOrEmpty(ccEmail))
                mailMessage.CC.Add(ccEmail);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = strHTML;
            mailMessage.Attachments.Add(a);
            mailClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "mail.send function");
        }
    }
    public void SendingMail(string ToEmailID, string Subject, string strHTML,string FromEmail, Attachment a, string ccEmail = "")
    {
        try
        {
            addressFrom = new MailAddress(Deffinity.systemdefaults.GetFromEmail());
            addressTo = new MailAddress(ToEmailID);
            MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
            
            if (!string.IsNullOrEmpty(ccEmail))
                mailMessage.CC.Add(ccEmail);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = strHTML;
            mailMessage.Attachments.Add(a);
            mailClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "mail.send function");
        }
    }

    public void SendingMail(string ToEmailID, string Subject, string strHTML, string FromEmail, List<Attachment> alist, string ccEmail = "")
    {
        try
        {
            addressFrom = new MailAddress(Deffinity.systemdefaults.GetFromEmail());
            addressTo = new MailAddress(ToEmailID);
            MailMessage mailMessage = new MailMessage(addressFrom, addressTo);

            if (!string.IsNullOrEmpty(ccEmail))
                mailMessage.CC.Add(ccEmail);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = strHTML;
            foreach(var a in alist )
            mailMessage.Attachments.Add(a);
            mailClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "mail.send function");
        }
    }

    public void SendingMail(string FromEmailID, string Subject, string strHTML, string ToEmailID, string ccEmail = "")
    {
        try
        {

            //mailClient.EnableSsl = true;
            //mailClient.UseDefaultCredentials = false;
            //mailClient.Port = 587;
            //addressFrom = new MailAddress(Deffinity.systemdefaults.GetFromEmail());// (System.Configuration.ConfigurationManager.AppSettings["FromEmailID"]);
            addressFrom = new MailAddress(FromEmailID);
            addressTo = new MailAddress(ToEmailID);
            MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
            if (!string.IsNullOrEmpty(ccEmail))
                mailMessage.CC.Add(ccEmail);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = strHTML;
            mailClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "mail.send function");
        }
    }
    public void SendingMail(string ToEmailID, string Subject, string strHTML,string CC, string Bcc,ArrayList toMailIds)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();
            if(!string.IsNullOrEmpty(CC))mailMessage.CC.Add(CC);
            if(!string.IsNullOrEmpty(Bcc))mailMessage.Bcc.Add(Bcc);
            foreach (string mailId in toMailIds)
            {
                MailAddress toAddress = new MailAddress(mailId);
                //Check for duplicates.
                if (!mailMessage.To.Contains(toAddress))
                    mailMessage.To.Add(mailId);
            }
            //mailMessage.From =new MailAddress(System.Configuration.ConfigurationManager.AppSettings["FromEmailID"]);
            mailMessage.From = new MailAddress(Deffinity.systemdefaults.GetFromEmail());
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = strHTML;
            mailClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public void SendingMail(string FromMailID,string ToEmailID, string Subject, string strHTML, string CC, string Bcc, ArrayList toMailIds)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();
            if (!string.IsNullOrEmpty(CC)) mailMessage.CC.Add(CC);
            if (!string.IsNullOrEmpty(Bcc)) mailMessage.Bcc.Add(Bcc);
            foreach (string mailId in toMailIds)
            {
                MailAddress toAddress = new MailAddress(mailId);
                //Check for duplicates.
                if (!mailMessage.To.Contains(toAddress))
                    mailMessage.To.Add(mailId);
            }
            mailMessage.From = new MailAddress(Deffinity.systemdefaults.GetFromEmail()); //new MailAddress(System.Configuration.ConfigurationManager.AppSettings["FromEmailID"]);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = strHTML;
            mailClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
 public void SendingMail(string ToEmailID, string Subject, string strHTML, string CC, string Bcc, ArrayList toMailIds, System.IO.Stream attachment)//, string attachFileName)
    {
        Attachment attacfilename = new Attachment(attachment, "ProjectPlanReport.pdf");
        try
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.Attachments.Add(attacfilename);
            BuildMailObject(Subject, strHTML, CC, Bcc, toMailIds, mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
	
	

    public void SendingMail(ArrayList ToEmailID, string Subject, string strHTML, string FromEmail, Attachment a)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();
            addressFrom = new MailAddress(Deffinity.systemdefaults.GetFromEmail());
            //addressTo = new MailAddress(ToEmailID);
            foreach (string mailId in ToEmailID)
            {
                MailAddress toAddress = new MailAddress(mailId);
                //Check for duplicates.
                if (!mailMessage.To.Contains(toAddress))
                    mailMessage.To.Add(mailId);
            }
                        //mailMessage.CC.Add("k.indrasenareddy@gmail.com");
            // mailMessage.CC.Add("Nadeem.Mohammed@linemanagement.co.uk");
            mailMessage.From = addressFrom;
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = strHTML;            
            mailMessage.Attachments.Add(a);
            mailClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "mail.send function");
        }
    }

    private void BuildMailObject(string Subject, string strHTML, string CC, string Bcc, ArrayList toMailIds, MailMessage mailMessage)
    {
        if (!string.IsNullOrEmpty(CC)) mailMessage.CC.Add(CC);
        if (!string.IsNullOrEmpty(Bcc)) mailMessage.Bcc.Add(Bcc);
        ArrayList testForDuplicates = new ArrayList();
        foreach (string mailId in toMailIds)
        {
            MailAddress toAddress = new MailAddress(mailId);
            //Check for duplicates.
            if (!mailMessage.To.Contains(toAddress))
                mailMessage.To.Add(mailId);
        }
        mailMessage.From = new MailAddress(Deffinity.systemdefaults.GetFromEmail());
        mailMessage.Subject = Subject;
        mailMessage.IsBodyHtml = true;
        mailMessage.Body = strHTML;
        mailClient.Send(mailMessage);
    }

    #region items collection
    public class ItemsList
    {
        string itemDescription = "";
        string startdate ="";
        string enddate ="";
        public ItemsList(string item,string sDate,string eDate)
        {
            itemDescription = item;
            startdate =  sDate;
            enddate = eDate;        
        }
        
        public override string ToString()
        {
            return
              String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
              itemDescription, startdate, enddate);
        } 

    }
#endregion

    #region Logo
    private string getLogo()
    {
        string s = "";
        Hashtable ht= new Hashtable();
        ht.Add("ems", "deffinity_logo.gif");
        ht.Add("aernet", "deffinity_logo.gif");
       
        
        IDictionaryEnumerator en = ht.GetEnumerator();
        while (en.MoveNext())
        {
            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains(en.Key.ToString()))
            {
                s = en.Value.ToString();
            }
        }


        return s;
    }
    #endregion
}
