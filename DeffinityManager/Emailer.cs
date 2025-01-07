using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Data.Common;
using System.Collections;
using System.IO;
using ProjectMgt.DAL;
using System.Linq;

/// <summary>
/// Summary description for Emailer
/// </summary>
public class Emailer
{
    MailAddress addressFrom;
    MailAddress addressTo;
    SmtpClient mailClient = new SmtpClient();
	public Emailer()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ReadFile(string FileName)
    {
        try
        {
            if (System.Web.HttpContext.Current != null)
            {
                string FILENAME = System.Web.HttpContext.Current.Server.MapPath(FileName);
                using (StreamReader objstreamreader = File.OpenText(FILENAME))
                {
                    string contents = objstreamreader.ReadToEnd();
                    return contents;
                }
            }
            else
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    var p = pd.ProjectDefaults.FirstOrDefault();
                    var f = p.LocalDocumentPath + "" + @FileName.Replace("~", "").Replace("/","\\");

                    string FILENAME = f;
                    using (StreamReader objstreamreader = File.OpenText(FILENAME))
                    {
                        string contents = objstreamreader.ReadToEnd();
                        return contents;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return "";
    }
    public void SendingMail(string FromEmailID, string Subject, string strHTML, string ToEmailID)
    {
        try
        {
            addressFrom = new MailAddress("service@plegit.ai");
            addressTo = new MailAddress(ToEmailID);
            MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
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
    public void BulkMailSending(string FromEmailID, string Subject, string strHTML, string ToEmailIDs)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmailID);
            string[] MulTi = ToEmailIDs.Split(',');
            foreach (string emailId in MulTi)
            {
                mailMessage.To.Add(new MailAddress(emailId));
            }
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
}