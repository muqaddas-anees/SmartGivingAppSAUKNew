using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Net.Mail;

/// <summary>
/// Summary description for BusinessClass
/// </summary>
public static class BusinessRule
{
	static BusinessRule()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    /// <summary>
    /// Implements the Pascal Casing on each word.
    /// For eg., if you pass the string "hello world".  
    /// This method returns "Hello World".
    /// </summary>
    /// <param name="originalString"></param>
    /// <returns>Pascal Casing of the passed string. For eg., if you pass the string "hello world".  This method returns "Hello World".</returns>
    
    public static string getInitCapital(string originalString)
    {
        try
        {
            string s = originalString;
            s = s.ToLower();
            string e = @"\w+|\W+";
            string sProper = "";
            foreach (Match m in Regex.Matches(s, e))
            {
                sProper += char.ToUpper(m.Value[0]) + m.Value.Substring(1, m.Length - 1);
            }
            return sProper;
        }
        catch
        {
            return originalString;
        }
    }
}

//Implementation is under process..



public class genericMailClass
{
    #region member declaration for project details in the mail..
    //static string projectReference = "";
    string receipientName;
    string country;
    string city;
    string site;
    string building;
    string jobDescription;
    string ownersEmail;
    string ownersMobile;
    string projectGroup;
    string projectOwner;
    string startDate;
    string endDate;
    string date;
    string qacomments;

    MailAddress addressFrom;
    MailAddress addressTo;
    SmtpClient mailClient;//=new SmtpClient();
    #endregion

    public genericMailClass()
    {
        receipientName = "";
        country = "";
        city = "";
        site = "";
        building = "";
        jobDescription = "";
        ownersEmail = "";
        ownersMobile = "";
        projectGroup = "";
        projectOwner = "";
        startDate = "";
        endDate = "";
        date = "";
        qacomments = "";
       
       
    }

    /// <summary>
    /// This method sends a mail.
    /// Call this method if you need to send any id value to be compared in the database.
    /// </summary>
    /// <param name="projectReference"></param>

    private void initializeProjectFields(int projectReference)
    {

        // The select statement retrieves the following fields
        //Country.Country,  City.City,  Site.Site,  ProjectDescription,  StartDate,  
        //CompletedDate,  Owner,  OwnerEmail,  OwnerContact, OperationsOwners 
        //The table is projects,city,site,country and OperationsOwners

        string[] projectData;
        string st_proc = "getProjectDetailsForMail";
        projectDetails objProjectDetails = new projectDetails();
        projectData = objProjectDetails.getData(projectReference,st_proc);
        country = projectData[0];
        city = BusinessRule.getInitCapital(projectData[1]);
        site = BusinessRule.getInitCapital(projectData[2]);
        jobDescription = projectData[3];
        startDate = projectData[4];
        endDate = projectData[5];
        projectOwner = BusinessRule.getInitCapital(projectData[6]);
        ownersMobile = projectData[8];
        projectGroup = BusinessRule.getInitCapital(projectData[9]);
        date = projectData[10];         //For QA
        qacomments = projectData[11];   //For QA
        
    }

    //public void mailFormat(string mailTitle, string mailTitleText,string subject, string systemName, int projectReference, int id,string[] toMailAddress)
    //{
    //    ID = 0;
    //    ID = id;
    //    mailFormat(mailTitle, mailTitleText,subject, systemName, projectReference,toMailAddress);    
    //}

    /// <summary>
    /// This method sends a mail.
    /// Call this method if u dont need to send any id value to be compared in the database.
    /// </summary>
    /// <param name="mailTitle"></param>
    /// <param name="mailTitleText"></param>
    /// <param name="systemName"></param>
    /// <param name="projectReference"></param>
    /// <param name="toMailAddress"></param>
    
    public void mailFormat(string mailTitle,string mailTitleText,string subject,string systemName,int projectReference,string[] toMailAddress)
    {
        initializeProjectFields(projectReference);
        StringBuilder mailTextFormat = new StringBuilder();
        mailTextFormat.Append("<html><head><title>" + mailTitleText + "</title></head><body><div align='left'>" +
                                "<table border='0' cellpadding='0' cellspacing='0' width='100%' ><tr>" +
                                "<td colspan='2'><strong>" + mailTitleText + "</strong></td>" +
                                "<td colspan='2'><img src='images/deffinity_logo.gif' /></strong></td></tr>" +
                                "<tr><td colspan='4' style='height:50px'>&nbsp;</td></tr>" +
                                "<tr><td colspan='4'>Dear" + "Resource name" + "</td></tr>" +
                                "<tr><td colspan='4' style='height:30px'>&nbsp;</td></tr>" +
                                "<tr><td colspan='4'>This is to let you know that Project <b>" + sessionKeys.Prefix + projectReference.ToString() + " </b>has been made live by " + projectOwner + "</td></tr>" +
                                "<tr><td colspan='4' style='height:30px'>&nbsp;</td></tr>" +
                                "<tr><td colspan='4' style='height:30px'>Details of the project are as follows:</td></tr>" +
                                "</table></div>" +
                                "<div align='left'><table border='1' cellpadding='1' cellspacing='1' width='100%' bordercolordark='#FFFFFF'><tr>" +
                                "<td><strong>Country</strong></td><td>" + country + "</td>" +
                                "<td><b>City</b></td><td>" + city + "</td>" + "</tr><tr>" +
                                "<td><b>Site</b></td><td>" + site + "</td>" +
                                "<td><b></b></td><td></td>" + "</tr><tr>" +
                                "<tr><td colspan='4' style='height:20px'>&nbsp;</td></tr>" +
                                "<td><b>Details</b></td><td colspan='3'>" + jobDescription + "</td>" + "</tr><tr>" +
                                "<td><b>Start Date</b></td><td>" + startDate + "</td>" +
                                "<td><b>Expected End Date</b></td><td>" + endDate + "</td>" + "</tr><tr>" +
                                "<tr><td colspan='4' style='height:20px'>&nbsp;</td></tr>" +
                                "<td><b>Project Owner</b></td><td>" + projectOwner + "</td>" +
                                "<td><b>Email</b></td><td></td>" + ownersEmail + "</tr><tr>" +
                                "<td><b>Mobile</b></td><td>" + ownersMobile + "</td>" +
                                "<td><b></b></td><td></td>" + "</tr><tr>" +
                                "<td><b>Programme</b></td><td>" + projectGroup + "</td>" +
                                "<td><b></b></td><td></td>" + "</tr><tr>" +
                                "<td><b></b></td><td></td>" +
                                "<td><b></b></td><td></td>" + "</tr><tr>" +
                                "</table></div>" + "<div><table border='0' cellpadding='0' cellspacing='0' width='100%'>");


        mailTextFormat.Append(markupPlaceHolder(mailTitle,projectReference));

        mailTextFormat.Append("</p><p style=\"margin-bottom: .0pt; font-size: 10.0pt; font-family: Verdana; margin-left: 0pt;" +
                        "margin-right: 0pt; margin-top: 0pt;\"><span>Thank you.</span></p><p class=\"MsoNormal\"><span>&nbsp; </span></p>"+
                        "<p style=\"margin-bottom: .0pt; font-size: 10.0pt; font-family: Verdana; margin-left: 0pt;"+
                        "margin-right: 0pt; margin-top: 0pt;\">");
        mailTextFormat.Append(systemName);
        mailTextFormat.Append("</p><p><span>&nbsp; </span></p></td></tr></table></body></html>");


        foreach (string address in toMailAddress)
        {
            addressFrom = new MailAddress("info@deffinity.com");
            addressTo = new MailAddress(address);
            MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
            mailMessage.Body = mailTextFormat.ToString();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            SmtpClient mailClient = new SmtpClient("localhost", 25);
            mailClient.Host = "localhost";
            mailClient.Send(mailMessage);
        }
    }

    public void mailFormat(string mailTitle, string mailTitleText, string subject, string systemName, int projectReference)
    {
        string prefix = sessionKeys.Prefix;
        string[] toMailAddress;
        initializeProjectFields(projectReference);
        StringBuilder mailTextFormat = new StringBuilder();
        mailTextFormat.Append("<html><head><title>" + mailTitle + "</title></head><body><div align='left'>" +
                                 "<table border='0' cellpadding='0' cellspacing='0' width='100%' ><tr>" +
                                 "<td colspan='2'><strong>" + mailTitleText + "</strong></td>" +
                                 "<td colspan='2' align='right'><img src='msa.deffinity.com/images/deffinity_logo.gif' /></strong></td></tr>" +
                                 "<tr><td colspan='4' style='height:50px'>&nbsp;</td></tr>" +
                                 "<tr><td colspan='4'>Dear" + "Resource name" + "</td></tr>" +
                                 "<tr><td colspan='4' style='height:30px'>&nbsp;</td></tr>" +
                                 "<tr><td colspan='4'>This is to let you know that Project <b>" + prefix + projectReference.ToString() + " </b>has been made live by " + projectOwner + "</td></tr>" +
                                 "<tr><td colspan='4' style='height:30px'>&nbsp;</td></tr>" +
                                 "<tr><td colspan='4' style='background:#FFCF87;font-size:12px;font-weight:bold;padding:7px;margin-bottom:10px;'>Details of the project are as follows:</td></tr>" +
                                 "</table></div>" +
                                 "<div align='left'><table border='1' cellpadding='0' cellspacing='0' width='100%' bordercolordark='#FFFFFF'><tr>" +
                                 "<tr><td colspan='4' style='height:20px'>&nbsp;</td></tr>" +
                                 "<td width='22%'><strong>Country</strong></td><td width='28%'>" + country + "</td>" +
                                 "<td  width='22%'><b>City</b></td><td width='28%'>" + city + "</td>" + "</tr><tr>" +
                                 "<td><b>Site</b></td><td>" + site + "</td>" +
                                 "<td><b></b></td><td></td>" + "</tr><tr>" +
                                 "<tr><td colspan='4' style='height:20px'>&nbsp;</td></tr>" +
                                 "<td><b>Details</b></td><td colspan='3'>" + jobDescription + "</td>" + "</tr><tr>" +
                                 "<td><b>Start Date</b></td><td>" + startDate + "</td>" +
                                 "<td><b>Expected End Date</b></td><td>" + endDate + "</td>" + "</tr><tr>" +
                                 "<tr><td colspan='4' style='height:20px'>&nbsp;</td></tr>" +
                                 "<td><b>Project Owner</b></td><td>" + projectOwner + "</td>" +
                                 "<td><b>Email</b></td><td></td>" + ownersEmail + "</tr><tr>" +
                                 "<td><b>Mobile</b></td><td>" + ownersMobile + "</td>" +
                                 "<td><b></b></td><td></td>" + "</tr><tr>" +
                                 "<td><b>Programme</b></td><td>" + projectGroup + "</td>" +
                                 "<td><b></b></td><td></td>" + "</tr><tr>" +
                                 "<td><b></b></td><td></td>" +
                                 "<td><b></b></td><td></td>" + "</tr><tr>" +
                                 "</table></div>" + "<div><table border='0' cellpadding='0' cellspacing='0' width='100%'>");
        sendmails(mailTextFormat, projectReference, systemName);
    }
    private void sendmails(StringBuilder mailTextFormat, int projectReference,String systemName)
    {
        //string st_proc = "getLiveProjectDetailsForMail";
        
        projectDetails objProjectDetails = new projectDetails();
        DataSet _ds= objProjectDetails.getLiveProjectData(projectReference);
        foreach (DataRow _drContractor in _ds.Tables[1].Rows)
        {
            StringBuilder mailText = new StringBuilder(mailTextFormat.ToString());
            mailText.Append(liveProject(Convert.ToInt32(_drContractor["ContractorID"]), _ds));
            //DataSet _ds=GetLiveProject
            mailText.Append("</p><p style=\"margin-bottom: .0pt; font-size: 10.0pt; font-family: Verdana; margin-left: 0pt;" +
                            "margin-right: 0pt; margin-top: 0pt;\"><span>Thank you.</span></p><p class=\"MsoNormal\"><span>&nbsp; </span></p>" +
                            "<p style=\"margin-bottom: .0pt; font-size: 10.0pt; font-family: Verdana; margin-left: 0pt;" +
                            "margin-right: 0pt; margin-top: 0pt;\">");
            mailText.Append(systemName);
            mailText.Append("</p><p><span>&nbsp; </span></p></td></tr></table></body></html>");
            addressFrom = new MailAddress("info@deffinity.com");
            addressTo = new MailAddress(_drContractor["EMailAddress"].ToString());
            MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
            mailMessage.Body = mailText.ToString();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "Live Project";
            SmtpClient mailClient = new SmtpClient("localhost", 25);
            mailClient.Host = "localhost";
            mailClient.Send(mailMessage);
        }
    }


    private int id=0;

    private int ID
    {
        get { return id; }
        set{id=value;}
    }

    //public static string markupPlaceHolder(string mailTitle, int projectReference, int id)
    //{
    //    ID = 0;
    //    ID = id;
    //    return markupPlaceHolder(mailTitle, projectReference);
    //}

    public string markupPlaceHolder(string mailTitle,int projectReference)
    {
        string returnMarkupString = "";

        switch (mailTitle)
        { 
            case "Variation Raised":
                //for testing
                returnMarkupString = variationRaised(ID,projectReference);
                break;
            case "Live Project":
                returnMarkupString = liveProject(projectReference);
                break;
            case "Mitigation Action":
                returnMarkupString = mitigationAction(projectReference);
                break;
            case "Variation Approved":
                returnMarkupString = variationApproved(projectReference,ID);
                break;
            case "Variation Denied!!!":
                returnMarkupString = variationDenied(projectReference,ID);
                break;
            case "QA/UAT Issue":
                returnMarkupString = QAandUAT(projectReference,ID);
                break;
            case "Interim QA Check Approval":
                returnMarkupString = interimQACheckApproval();
                break;
            case "QA Issue raised!":
                //returnMarkupString = QAIssueRaised();
                returnMarkupString = interimQACheckApproval();
                break;
            case "QA APPROVED & COMPLETE":
                //returnMarkupString = qaApprovedAndComplete();
                returnMarkupString = interimQACheckApproval();
                break;
            case "QA APPROVED & Project Issues Raised":
                //returnMarkupString = qaApprovedAndProjectIssuesRaised();
                returnMarkupString = interimQACheckApproval();
                break;
        }
        return returnMarkupString;
    }

    private string qaApprovedAndProjectIssuesRaised()
    {
        throw new NotImplementedException();
    }

    private string qaApprovedAndProjectIssuesRaised(string date,string comments)
    {
        StringBuilder mailText = new StringBuilder();
        mailText.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"MsoNormalTable\"><tr style=\"mso-yfti-irow: 0; mso-yfti-firstrow: yes\">"+
                        "<td style=\"width: 103.6pt; mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">Date QA carried out<o:p></o:p></span></p></td>"+
                        "<td style=\"width: 310.95pt; mso-border-left-alt: solid black .5pt; mso-border-alt: solid black .5pt;"+
                        "padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"415\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(date);
        mailText.Append("<o:p></o:p></span></p></td></tr><tr style=\"mso-yfti-irow: 1; mso-yfti-lastrow: yes\">"+
                        "<td style=\"width: 103.6pt; mso-border-top-alt: solid black .5pt; mso-border-alt: solid black .5pt;"+
                        "padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">Comments<o:p></o:p></span></p>"+
                        "</td><td style=\"width: 310.95pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"415\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(comments);
        mailText.Append("<o:p></o:p></span></p></td></tr></table><p class=\"MsoNormal\"><span lang=\"EN-GB\">"+
                        "<o:p>&nbsp;</o:p></span></p><p class=\"MsoNormal\"><span lang=\"EN-GB\">To access the system please <u>click here</u>. </span>"+
                        "</p><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p>");
        return mailText.ToString();
    }

    private string qaApprovedAndComplete(string date,string comments)
    {
        StringBuilder mailText = new StringBuilder();
        mailText.Append("<br/><br/><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"MsoNormalTable\">"+
                        "<tr style=\"mso-yfti-irow: 0; mso-yfti-firstrow: yes\"><td style=\"width: 103.6pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">Date QA carried out<o:p></o:p></span></p></td>"+
                        "<td style=\"width: 310.95pt; mso-border-left-alt: solid black .5pt; mso-border-alt: solid black .5pt;"+
                        "padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"415\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(date);
        mailText.Append("<o:p></o:p></span></p></td></tr><tr style=\"mso-yfti-irow: 1; mso-yfti-lastrow: yes\">"+
                        "<td style=\"width: 103.6pt; mso-border-top-alt: solid black .5pt; mso-border-alt: solid black .5pt;"+
                        "padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\"><p class=\"MsoNormal\">"+
                        "<span lang=\"EN-GB\">Comments<o:p></o:p></span></p></td>"+
                        "<td style=\"width: 310.95pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"415\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(comments);
        mailText.Append("<o:p></o:p></span></p></td></tr></table><p class=\"MsoNormal\"><span lang=\"EN-GB\">"+
                        "<o:p>&nbsp;</o:p></span></p><p class=\"MsoNormal\"><span lang=\"EN-GB\">To access the system please <u>click here</u>. </span>"+
                        "</p><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p>");
        return mailText.ToString();
    }
    private string QAIssueRaised(string date,string comments)
    {
        StringBuilder mailText = new StringBuilder();
        mailText.Append("<br/><br/><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"MsoNormalTable\"><tr style=\"mso-yfti-irow: 0; mso-yfti-firstrow: yes\">"+
                        "<td style=\"width: 103.6pt; mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">Date QA carried out<o:p></o:p></span></p></td>"+
                        "<td style=\"width: 310.95pt; mso-border-left-alt: solid black .5pt; mso-border-alt: solid black .5pt;"+
                        "padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"415\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(date);
        mailText.Append("<o:p></o:p></span></p></td></tr><tr style=\"mso-yfti-irow: 1; mso-yfti-lastrow: yes\">"+
                        "<td style=\"width: 103.6pt; mso-border-top-alt: solid black .5pt; mso-border-alt: solid black .5pt;"+
                        "padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\"><p class=\"MsoNormal\">"+
                        "<span lang=\"EN-GB\">Comments<o:p></o:p></span></p></td>"+
                        "<td style=\"width: 310.95pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"415\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(comments);
        mailText.Append("<o:p></o:p></span></p></td></tr></table><p class=\"MsoNormal\"><span lang=\"EN-GB\">"+
                        "<o:p>&nbsp;</o:p></span></p><p class=\"MsoNormal\"><span lang=\"EN-GB\">To update the project please <u>click here</u> to access the system."+
                        "</span></p><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p>");
        return mailText.ToString();
    }

    private string interimQACheckApproval()
    {

        StringBuilder mailText = new StringBuilder();
        mailText.Append("<br/><br/><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"MsoNormalTable\">"+
                        "<tr style=\"mso-yfti-irow:0;mso-yfti-firstrow:yes\"><td style=\"width:103.6pt;mso-border-alt:solid black .5pt;padding:0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">Date QA approval carried out <o:p></o:p></span>"+
                        "</p></td><td style=\"width:310.95pt;mso-border-left-alt:solid black .5pt;mso-border-alt:solid black .5pt;"+
                        "padding:0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"415\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(date);
        mailText.Append("<o:p></o:p></span></p></td></tr><tr style=\"mso-yfti-irow:1;mso-yfti-lastrow:yes\">"+
                        "<td style=\"width:103.6pt;mso-border-top-alt:solid black .5pt;mso-border-alt:solid black .5pt;"+
                        "padding:0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        
        mailText.Append("Comments<o:p></o:p></span></p></td><td style=\"width:310.95pt;"+
                        "mso-border-top-alt:solid black .5pt;mso-border-left-alt:solid black .5pt;"+
                        "mso-border-alt:solid black .5pt;padding:0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"415\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(qacomments);
        mailText.Append("<o:p></o:p></span></p></td></tr></table><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p>"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">To update the project please <u>click here</u> to access the "+
                        "system. </span></p><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p>");
        return mailText.ToString();
    }

    private string QAandUAT(int projectReference,int id)
    {
        
        string st_proc = "getQAandUATIssueDetailsForMail";
        projectDetails objProjectDetails = new projectDetails();
        string[] variationRaisedDetails = objProjectDetails.getData(projectReference, st_proc, id);

        string qaIssue = variationRaisedDetails[0];
        string RAG = variationRaisedDetails[1];
        string outCome = "";
        string completionDate = "";

        StringBuilder mailText = new StringBuilder();
        mailText.Append("<br/><br/><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"MsoNormalTable\"><tr style=\"mso-yfti-irow: 0; mso-yfti-firstrow: yes\">"+
                        "<td style=\"width: 103.6pt; mso-border-alt: solid black .5pt;padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">QA Issue<o:p></o:p></span></p></td>"+
                        "<td colspan=\"3\" style=\"width: 310.95pt;mso-border-left-alt: solid black .5pt; mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"415\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(qaIssue);
        mailText.Append("<o:p></o:p></span></p></td></tr><tr style=\"mso-yfti-irow: 1\">"+
                        "<td style=\"width: 103.6pt; mso-border-top-alt: solid black .5pt;mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\""+
                        "width=\"138\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">RAG Status<o:p></o:p></span></p></td>"+
                        "<td style=\"width: 103.65pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(RAG);
        mailText.Append("<o:p></o:p></span></p></td><td style=\"width: 103.65pt;border-left: none; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p></td>"+
                        "<td style=\"width: 103.65pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p></td></tr><tr style=\"mso-yfti-irow: 2; mso-yfti-lastrow: yes\">"+
                        "<td style=\"width: 103.6pt;mso-border-top-alt: solid black .5pt;mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\""+
                        "width=\"138\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">Expected Outcome<o:p></o:p></span></p></td>"+
                        "<td style=\"width: 103.65pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(outCome);
        mailText.Append("<o:p></o:p></span></p></td><td style=\"width: 103.65pt;mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\">"+
                        "<p class=\"MsoNormal\" style=\"width: 153px\"><span lang=\"EN-GB\">Exp. Completion Date<o:p></o:p></span></p>"+
                        "</td><td style=\"width: 103.65pt;mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(completionDate);
        mailText.Append("<o:p></o:p></span></p></td></tr></table><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p>"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">To update the project please <u>click here</u> to access the "+
                        "system. </span></p><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p>");
        return mailText.ToString();
    }

    private string variationDenied(int projectReference,int id)
    {
        string variationRaisedBy;
        string authorsContact;
        string authorsEmail;
        string date;
        string details;
        string justification;
        string value;
        string authoriser;

        StringBuilder mailText = variationValues(out variationRaisedBy, out authorsContact, out authorsEmail, out date, out details, out justification, out value, id, projectReference);
        mailText.Append("</p></td><td style=\"width: 103.65pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"138\"><p class=\"MsoNormal\"><span>&nbsp; </span></p></td>" +
                        "<td style=\"width: 103.65pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\">" +
                        "<p class=\"MsoNormal\"><span>&nbsp; </span></p></td></tr></table><p class=\"MsoNormal\"><span>&nbsp; </span></p>" +
                        "<p class=\"MsoNormal\"><span>Details of the variation are as follows:</span></p><p class=\"MsoNormal\">" +
                        "<span>&nbsp; </span></p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"margin-bottom: .0pt; font-size: 10.0pt;" +
                        "font-family: Verdana; margin-left: 0pt; margin-right: 0pt; margin-top: 0pt; border-collapse: collapse;border: none;\">" +
                        "<tr style=\"mso-yfti-firstrow: yes\"><td style=\"width: 233.65pt; border: solid black 0pt; background: #C6D9F1; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><b><span>Details</span></b></p></td>" +
                        "<td style=\"width: 177.2pt; border: solid black 0pt; border-left: none; background: #C6D9F1;padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"236\">" +
                        "<p class=\"MsoNormal\"><b><span>&nbsp; </span></b></p></td></tr><tr>" +
                        "<td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"312\"><p class=\"MsoNormal\">" +
                        "<span>Variation raised by:</span></p></td><td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(variationRaisedBy);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Author’s contact number</span></p></td>" +
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");

        mailText.Append(authorsContact);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Author’s Email</span></p></td>" +
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\">" +
                        "<p class=\"MsoNormal\">");
        mailText.Append(authorsEmail);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Date</span></p></td>" +
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(date);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Details</span></p></td>" +
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(details);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Justification</span></p></td>" +
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(justification);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Value</span></p></td><td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(value);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Authoriser</span></p></td>" +
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0pt\" valign=\"top\"width=\"236\"><p class=\"MsoNormal\">");
        authoriser = "";
        mailText.Append("&nbsp;&nbsp;" + authoriser);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>&nbsp; </span></p></td>" +
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">" +
                        "</p></td></tr></table>");
        return mailText.ToString();
    }


    private string variationApproved(int projectReference,int id)
    {
        string variationRaisedBy;
        string authorsContact;
        string authorsEmail;
        string date;
        string details;
        string justification;
        string value;
        string authoriser;
        StringBuilder mailText = variationValues(out variationRaisedBy, out authorsContact, out authorsEmail, out date, out details, out justification, out value,id, projectReference);
        mailText.Append("</p></td><td style=\"width: 103.65pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"138\"><p class=\"MsoNormal\"><span>&nbsp; </span></p></td>"+
                        "<td style=\"width: 103.65pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\">"+
                        "<p class=\"MsoNormal\"><span>&nbsp; </span></p></td></tr></table><p class=\"MsoNormal\"><span>&nbsp; </span></p>"+
                        "<p class=\"MsoNormal\"><span>Details of the variation are as follows:</span></p><p class=\"MsoNormal\">"+
                        "<span>&nbsp; </span></p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"margin-bottom: .0pt; font-size: 10.0pt;"+
                        "font-family: Verdana; margin-left: 0pt; margin-right: 0pt; margin-top: 0pt; border-collapse: collapse;border: none;\">"+
                        "<tr style=\"mso-yfti-firstrow: yes\"><td style=\"width: 233.65pt; border: solid black 0pt; background: #C6D9F1; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><b><span>Details</span></b></p></td>"+
                        "<td style=\"width: 177.2pt; border: solid black 0pt; border-left: none; background: #C6D9F1;padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"236\">"+
                        "<p class=\"MsoNormal\"><b><span>&nbsp; </span></b></p></td></tr><tr>"+
                        "<td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"312\"><p class=\"MsoNormal\">"+
                        "<span>Variation raised by:</span></p></td><td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(variationRaisedBy);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Author’s contact number</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        
        mailText.Append(authorsContact);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Author’s Email</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\">"+
                        "<p class=\"MsoNormal\">");
        mailText.Append(authorsEmail);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Date</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(date);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Details</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(details);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Justification</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(justification);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Value</span></p></td><td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(value);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Authoriser</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0pt\" valign=\"top\"width=\"236\"><p class=\"MsoNormal\">");
        authoriser = "";
        mailText.Append("&nbsp;&nbsp;"+authoriser);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"" +
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>&nbsp; </span></p></td>" +
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">" +
                        "</p></td></tr></table>");
        return mailText.ToString();
    }

    private string mitigationAction(int projectReference)
    {
        string riskMitigationAction;
        string type;
        string RAG;
        string outCome;
        string completionDate;
        string st_proc = "getMitigationActionDetailsForMail";
        projectDetails objProjectDetails = new projectDetails();
        string[] mitigationActionDetails = objProjectDetails.getData(projectReference, st_proc);
        riskMitigationAction = BusinessRule.getInitCapital(mitigationActionDetails[0]);
        type = BusinessRule.getInitCapital(mitigationActionDetails[1]);
        RAG = BusinessRule.getInitCapital(mitigationActionDetails[2]);
        completionDate  = mitigationActionDetails[3];

        StringBuilder mailText = new StringBuilder();
        mailText.Append("<br/><br/><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"MsoNormalTable\"><tr style=\"mso-yfti-irow: 0; mso-yfti-firstrow: yes\">"+
                        "<td style=\"mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\" style=\"width: 103.6pt;height: 16px;\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">Risk<o:p></o:p></span></p></td>"+
                        "<td colspan=\"3\" style=\"border-left: none; mso-border-left-alt: solid black .5pt;mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\""+
                        "width=\"415\" style=\"width: 310.95pt;height: 16px;\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(riskMitigationAction);
        mailText.Append("<o:p></o:p></span></p></td></tr><tr style=\"mso-yfti-irow: 1\"><td style=\"width: 103.6pt; border-top: none; mso-border-top-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\"><p class=\"MsoNormal\">"+
                        "<span lang=\"EN-GB\">Risk Type<o:p></o:p></span></p></td><td style=\"width: 103.65pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(type);
        mailText.Append("<o:p></o:p></span></p></td><td style=\"width: 103.65pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\"><p class=\"MsoNormal\">"+
                        "<span lang=\"EN-GB\">RAG Status<o:p></o:p></span></p></td><td style=\"width: 103.65pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(RAG);
        mailText.Append("<o:p></o:p></span></p></td></tr><tr style=\"mso-yfti-irow: 2; mso-yfti-lastrow: yes\">"+
                        "<td style=\"width: 103.6pt; mso-border-top-alt: solid black .5pt; mso-border-alt: solid black .5pt;"+
                        "padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">Expected Outcome<o:p></o:p></span></p>"+
                        "</td><td style=\"width: 103.65pt; border-top: none; border-left: none; mso-border-top-alt: solid black .5pt;"+
                        "mso-border-left-alt: solid black .5pt; mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        outCome = "";
        mailText.Append(outCome);
        mailText.Append("<o:p></o:p></span></p></td><td style=\"width: 103.65pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\"><p class=\"MsoNormal\">"+
                        "<span lang=\"EN-GB\">Exp. Completion Date<o:p></o:p></span></p></td>"+
                        "<td style=\"width: 103.65pt; mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;"+
                        "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"138\">"+
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">");
        mailText.Append(completionDate);
        mailText.Append("<o:p></o:p></span></p></td></tr></table><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p>"+
                        "</span></p><p class=\"MsoNormal\"><span lang=\"EN-GB\">To update the project please <u>click here</u> to access the system."+
                        "</span></p><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p>");
        return mailText.ToString();
    }

    private string liveProject(int projectReference)
    {
        string projectItem;
        string startDate; 
        string endDate;
        StringBuilder mailText = new StringBuilder();
        mailText.Append("<p class=\"MsoNormal\"><span lang=\"EN-GB\"><span style=\"mso-spacerun: yes\">&nbsp;</span></span></p>"+
                        "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"MsoNormalTable\" style=\"border-collapse: collapse;border: none; mso-border-alt: solid black .5pt;>"+
                        "<tr style=\"mso-yfti-irow: 1; mso-yfti-lastrow: yes\"><td colspan=\"2\" style=\"width: 426.1pt;  border-top: none;"+
                        "mso-border-top-alt: solid black .5pt; mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"568\">"+
                        "<span lang=\"EN-GB\">The list of activities assigned to you are as follows:</span></p><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span>"+
                        "</p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"MsoNormalTable\" style=\"border-collapse: collapse;border: none; mso-border-alt: solid black .5pt; mso-yfti-tbllook: 160; mso-padding-alt: 0in 5.4pt 0in 5.4pt;"+
                        "mso-border-insideh: .5pt solid black; mso-border-insidev: .5pt solid black\"><tr style=\"mso-yfti-irow: 0; mso-yfti-firstrow: yes\"><td style=\"width: 138.15pt;  mso-border-alt: solid black .5pt;"+
                        "background: #C6D9F1; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"184\"><p class=\"MsoNormal\"><b style=\"mso-bidi-font-weight: normal\"><span lang=\"EN-GB\">Item<o:p></o:p></span></b></p>"+
                        "</td><td style=\"width: 138.2pt;  border-left: none; mso-border-left-alt: solid black .5pt;mso-border-alt: solid black .5pt; background: #C6D9F1; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"184\"><p class=\"MsoNormal\"><b style=\"mso-bidi-font-weight: normal\"><span lang=\"EN-GB\">Start Date<o:p></o:p></span></b></p></td>"+
                        "<td style=\"width: 138.2pt;  border-left: none; mso-border-left-alt: solid black .5pt;mso-border-alt: solid black .5pt; background: #C6D9F1; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"184\"><p class=\"MsoNormal\"><b style=\"mso-bidi-font-weight: normal\"><span lang=\"EN-GB\">End Date<o:p></o:p></span></b></p>"+
                        "</td></tr>");

        //This block should be placed in the loop.
        //It loops n times, supposed that n is the no. of records retrieved from the database.

        string[,] liveProjectDetails=initializeLiveProjectFields(projectReference);
        
        #region HTML Table Generator

        for (int i = 0; i < liveProjectDetails.GetLength(0); i++)
        {
            projectItem = liveProjectDetails[i,0];
            startDate = liveProjectDetails[i,1];
            endDate = liveProjectDetails[i,2];
            mailText.Append("<tr style=\"mso-yfti-irow: 1\"><td style=\"width: 138.15pt;  border-top: none;mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"" +
                            "width=\"184\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
            mailText.Append(projectItem);
            mailText.Append("</span></p></td><td style=\"width: 138.2pt; border-top: none; border-left: none;mso-border-top-alt: solid black .5pt;" +
                            "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"184\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
            mailText.Append(startDate);
            mailText.Append("</span></p></td><td style=\"width: 138.2pt; border-top: none; border-left: none;" +
                            "mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"" +
                            "width=\"184\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
            mailText.Append(endDate);
            mailText.Append("</span></p></td></tr>");
        }

        #endregion

        mailText.Append("</table><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p>" +
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">For an up to date list of activities and to update the project please"+
                        "&nbsp;&nbsp;<u>click here</u> to access the system. </span></p>");
        return mailText.ToString();
    }
    private string liveProject(int ContractorID,DataSet _dsContractors)
    {
        string projectItem;
        string startDate;
        string endDate;
        StringBuilder mailText = new StringBuilder();
        //mailText.Append("<p class=\"MsoNormal\"><span lang=\"EN-GB\"><span style=\"mso-spacerun: yes\">&nbsp;</span></span></p>" +
        //                "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"MsoNormalTable\" style=\"border-collapse: collapse;border: none; mso-border-alt: solid black .5pt;>" +
        //                "<tr style=\"mso-yfti-irow: 1; mso-yfti-lastrow: yes\"><td colspan=\"2\" style=\"width: 426.1pt;  border-top: none;" +
        //                "mso-border-top-alt: solid black .5pt; mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"568\">" +
        //                "<span lang=\"EN-GB\">The list of activities assigned to you are as follows:</span></p><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span>" +
        //                "</p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"MsoNormalTable\" style=\"border-collapse: collapse;border: none; mso-border-alt: solid black .5pt; mso-yfti-tbllook: 160; mso-padding-alt: 0in 5.4pt 0in 5.4pt;" +
        //                "mso-border-insideh: .5pt solid black; mso-border-insidev: .5pt solid black\"><tr style=\"mso-yfti-irow: 0; mso-yfti-firstrow: yes\"><td style=\"width: 138.15pt;  mso-border-alt: solid black .5pt;" +
        //                "background: #C6D9F1; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"184\"><p class=\"MsoNormal\"><b style=\"mso-bidi-font-weight: normal\"><span lang=\"EN-GB\">Item<o:p></o:p></span></b></p>" +
        //                "</td><td style=\"width: 138.2pt;  border-left: none; mso-border-left-alt: solid black .5pt;mso-border-alt: solid black .5pt; background: #C6D9F1; padding: 0in 5.4pt 0in 5.4pt\"" +
        //                "valign=\"top\" width=\"184\"><p class=\"MsoNormal\"><b style=\"mso-bidi-font-weight: normal\"><span lang=\"EN-GB\">Start Date<o:p></o:p></span></b></p></td>" +
        //                "<td style=\"width: 138.2pt;  border-left: none; mso-border-left-alt: solid black .5pt;mso-border-alt: solid black .5pt; background: #C6D9F1; padding: 0in 5.4pt 0in 5.4pt\"" +
        //                "valign=\"top\" width=\"184\"><p class=\"MsoNormal\"><b style=\"mso-bidi-font-weight: normal\"><span lang=\"EN-GB\">End Date<o:p></o:p></span></b></p>" +
        //                "</td></tr>");

        //This block should be placed in the loop.
        //It loops n times, supposed that n is the no. of records retrieved from the database.
        projectDetails _projDet = new projectDetails();
        string[,] liveProjectDetails = _projDet.getLiveProjectDatabyContracotor(_dsContractors, ContractorID);

        #region HTML Table Generator

        for (int i = 0; i < liveProjectDetails.GetLength(0); i++)
        {
            projectItem = liveProjectDetails[i, 0];
            startDate = liveProjectDetails[i, 1];
            endDate = liveProjectDetails[i, 2];
            mailText.Append("<tr style=\"mso-yfti-irow: 1\"><td style=\"width: 138.15pt;  border-top: none;mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"" +
                            "width=\"184\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
            mailText.Append(projectItem);
            mailText.Append("</span></p></td><td style=\"width: 138.2pt; border-top: none; border-left: none;mso-border-top-alt: solid black .5pt;" +
                            "mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"width=\"184\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
            mailText.Append(startDate);
            mailText.Append("</span></p></td><td style=\"width: 138.2pt; border-top: none; border-left: none;" +
                            "mso-border-top-alt: solid black .5pt; mso-border-left-alt: solid black .5pt;mso-border-alt: solid black .5pt; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\"" +
                            "width=\"184\"><p class=\"MsoNormal\"><span lang=\"EN-GB\">");
            mailText.Append(endDate);
            mailText.Append("</span></p></td></tr>");
        }

        #endregion

        mailText.Append("</table><p class=\"MsoNormal\"><span lang=\"EN-GB\"><o:p>&nbsp;</o:p></span></p>" +
                        "<p class=\"MsoNormal\"><span lang=\"EN-GB\">For an up to date list of activities and to update the project please" +
                        "&nbsp;&nbsp;<u>click here</u> to access the system. </span></p>");
        return mailText.ToString();
    }


    private string[,] initializeLiveProjectFields(int projectReference)
    {
        string st_proc = "getLiveProjectDetailsForMail";
        projectDetails objProjectDetails = new projectDetails();
        string[,] liveProjectArray=objProjectDetails.getLiveProjectData(projectReference, st_proc);
        return liveProjectArray;
        
        
    }
    //private string[,] initializeLiveProjectFields(int projectReference, int contractorID)
    //{
    //    string st_proc = "getLiveProjectDetailsForMail";
    //}

    private string variationRaised(int id,int projectReference)
    {
        string variationRaisedBy;
        string authorsContact;
        string authorsEmail;
        string date;
        string details;
        string justification;
        string value;
        string authoriser;
        StringBuilder mailText = variationValues(out variationRaisedBy, out authorsContact, out authorsEmail, out date, out details, out justification, out value, id, projectReference);

        mailText.Append("</p></td><td style=\"width: 103.65pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"138\"><p class=\"MsoNormal\"><span>&nbsp; </span></p></td>"+
                        "<td style=\"width: 103.65pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"138\">"+
                        "<p class=\"MsoNormal\"><span>&nbsp; </span></p></td></tr></table><p class=\"MsoNormal\"><span>&nbsp; </span></p>"+
                        "<p class=\"MsoNormal\"><span>Details of the variation are as follows:</span></p><p class=\"MsoNormal\">"+
                        "<span>&nbsp; </span></p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"margin-bottom: .0pt; font-size: 10.0pt;"+
                        "font-family: Verdana; margin-left: 0pt; margin-right: 0pt; margin-top: 0pt; border-collapse: collapse;border: none;\">"+
                        "<tr style=\"mso-yfti-firstrow: yes\"><td style=\"width: 233.65pt; border: solid black 0pt; background: #C6D9F1; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><b><span>Details</span></b></p></td>"+
                        "<td style=\"width: 177.2pt; border: solid black 0pt; border-left: none; background: #C6D9F1;padding: 0in 5.4pt 0in 5.4pt\" valign=\"top\" width=\"236\">"+
                        "<p class=\"MsoNormal\"><b><span>&nbsp; </span></b></p></td></tr><tr>"+
                        "<td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"312\"><p class=\"MsoNormal\">"+
                        "<span>Variation raised by:</span></p></td><td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(variationRaisedBy);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Author’s contact number</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        
        mailText.Append(authorsContact);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Author’s Email</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\">"+
                        "<p class=\"MsoNormal\">");
        mailText.Append(authorsEmail);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Date</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(date);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Details</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(details);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Justification</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(justification);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Value</span></p></td><td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"236\"><p class=\"MsoNormal\">");
        mailText.Append(value);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>Authoriser</span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0pt\" valign=\"top\"width=\"236\"><p class=\"MsoNormal\">");
        authoriser = "";
        mailText.Append("&nbsp;&nbsp;"+authoriser);
        mailText.Append("</p></td></tr><tr><td style=\"width: 233.65pt; border: solid black 0pt; border-top: none; padding: 0in 5.4pt 0in 5.4pt\""+
                        "valign=\"top\" width=\"312\"><p class=\"MsoNormal\"><span>&nbsp; </span></p></td>"+
                        "<td style=\"width: 177.2pt; border-top: none; border-left: none; padding: 0in 5.4pt 0in 5.4pt\"valign=\"top\" width=\"236\"><p class=\"MsoNormal\">"+
                        "</p></td></tr></table><p class=\"MsoNormal\"><span>To accept this variation please <u>click here</u>.</span></p>"+
                        "<p class=\"MsoNormal\"><u><span><span style=\"text-decoration: none\">&nbsp;</span> </span></u></p>"+
                        "<p class=\"MsoNormal\"><span>To deny this variation please <u>click here.</u></span></p>"+
                        "<p class=\"MsoNormal\"><span>&nbsp; </span></p><p class=\"MsoNormal\">"+
                        "<span>If you would like to contact the author please <u>click here</u> to email them.</span>");
        return mailText.ToString();
    }

    private StringBuilder variationValues(out string variationRaisedBy, out string authorsContact, out string authorsEmail, out string date, out string details, out string justification, out string value, int id, int projectReference)
    {
        string st_proc = "getVariationRaisedDetailsForMail";
        projectDetails objProjectDetails = new projectDetails();
        string[] variationRaisedDetails = objProjectDetails.getData(projectReference, st_proc, id);
        StringBuilder mailText = new StringBuilder();

        variationRaisedBy = variationRaisedDetails[0];
        authorsContact = variationRaisedDetails[1];
        authorsEmail = variationRaisedDetails[2];
        date = variationRaisedDetails[3];
        details = variationRaisedDetails[4];
        justification = variationRaisedDetails[5];
        value = variationRaisedDetails[6];
        return mailText;
    }

    //Govardhan's method for sending mail.
//      private void SendMail(int pref, int status)
//    {
//        try
//        {
//            if (status == 2)
//            {
//                MailAddress addressFrom;
//                MailAddress addressTo;
//                SmtpClient mailClient = new SmtpClient("auth.smtp.1and1.co.uk", 25);
//                addressFrom = new MailAddress(txtFrom.Text);
//                addressTo = new MailAddress(txtTo.Text);
//                MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
//                mailMessage.Subject = "Project " + pref +" has now been made live";
//                mailMessage.Body ="<!DOCTYPE html PUBLIC -//W3C//DTD XHTML 1.0 Transitional//EN http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd><html xmlns=http://www.w3.org/1999/xhtml><head><meta http-equiv=Content-Type content=text/html; charset=iso-8859-1 /><title>Welcome to Deffinity Project Dashboard Charts</title><link href=new_designs/new_designs/css/defficss.css rel=stylesheet type=text/css /><script type=text/javascript language=JavaScript1.2 src=new_designs/new_designs/js/stmenu.js></script></head><body><div class=bodyframe> <div class=content><div class=content_bodyfull><div class=content_header>
//Live Project</div>
//<div class=logo1><img src=new_designs/new_designs/images/deffinity_logo.gif alt=Welcome to deffinity /></div>
//<div style=clear:both></div><div class=sections>    
//&nbsp;    <div class=sec_body>  <font size=2>
//<div class=table_divl style=width: 600px; height: 353px>
//<font size=2>Dear " + eMailTo + ",<p>&nbsp;This is to let you know that Project DEF 121 has been made live by AliSyed.Details of the project are as follows: </font></p>
//<table width=100% border=0 cellspacing=0 cellpadding=10>
    //                  <tr  width=100%>                    <td width=25% >Country</td>                                        <td width=25%>"+ ddlCountry.SelectedValue +"</td>                                        <td width=25%>Site</td>                                        <td width=25%>"+ddlSite.SelectedValue +"</td>            </tr>            <tr>             <div class=clr></div>            </tr>                 <tr  >                    <td  >Details</td>                                        <td colspan=3>"+ txtDesc.Text +"</td>                                                    </tr>                           <tr  >                    <td  >Start Date</td>                                        <td >"+ txtStartDate.Text +"</td>                                        <td >End Date</td>                                        <td >"+ txtEndDate.Text +"</td>            </tr>                           <tr  >                             <div class=clr></div>                    <td  >Project Owners</td>                                        <td >"+ddlOwner.SelectedValue +"</td>                                        <td >Email</td>                                        <td >"+ txtEmail.Text +"</td>            </tr>                           <tr  >                    <td  >Mobile</td>                                        <td >Mobile</td>                                        <td ></td>                                        <td ></td>            </tr>              <tr  >                    <td  >Programme</td>                                        <td >"+ ddlGroup.SelectedValue +"</td>                                        <td ></td>                                        <td ></td>            </tr>                           </table><p></p>
//  The list of activities assigned to you are as follows:
// <table width=100% border=0 cellspacing=0 cellpadding=10>
//                  <tr  width=100% class=tab_header>                    <td width=33% >Item</td>                                        <td width=33%>Start Date</td>                                        <td width=33%>End Date</td>            </tr>            <tr>            <td>item 1</td>                        <td>start date1</td>                                    <td>end date1</td>            </tr>            <tr>                        </table>            <p></p>            <table>            <td colspan=3>            For an up to date list of activities and to update the project.            </td>            </tr>            <tr>            <td colspan=3>           please click here to access the system.                       </td>            </tr>            <tr>            <td colspan=3>            Thank you.            </td>            </tr>            <tr>            <td colspan=3>            System Name            </td>            </tr>            </table>            </div></font>
//<div class=clr></div>
//</div>  </div>
//  </div></div></div><div class=clr></div><div class=footer >&copy;2008 Deffinity.com</div></div></body></html>"
//                mailMessage.IsBodyHtml = true;
//                smailClient.PickupDirectoryLocation = "info@deffinity.com";
//                mailClient.Send(mailMessage);
//            }
//        }
//        catch (Exception ex)
//        {
//            error = ex.ToString();
//        }
//    }
}
 
