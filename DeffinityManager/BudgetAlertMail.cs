using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using ProjectMgt.DAL;
using System.Data.SqlClient;
using System.Data;
using UserMgt.DAL;

/// <summary>
/// Summary description for BudgetAlertMail
/// </summary>
public class BudgetAlertMail
{
    MailAddress addressFrom;
    MailAddress addressTo;
    SmtpClient mailClient;
	public BudgetAlertMail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ReadFile(string FileName)
    {
        try
        {
            string FILENAME = System.Web.HttpContext.Current.Server.MapPath(FileName);
            StreamReader objstreamreader = File.OpenText(FILENAME);
            string contents = objstreamreader.ReadToEnd();
            return contents;

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return "";
    }
    public string PMHoursdata(string pid)
    {
        string ReturnValue = string.Empty;
        decimal Budgetvalue = 0;
        decimal Actualvalue1 = 0;
        try
        {

            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_GraphBudgetedhours";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", int.Parse(pid));
            sqlcmd.Parameters.AddWithValue("@Hours", "PM");
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Budgetvalue = Convert.ToDecimal(dr["Hours"].ToString());
            }
            //  pmHourBudget = Decimal.ToInt32(value);


            SqlConnection sqlcon1 = new SqlConnection(Constants.DBString);
            string cmd1 = "Project_GraphActualhours";
            DataTable dt1 = new DataTable();
            SqlCommand sqlcmd1 = new SqlCommand(cmd1, sqlcon1);
            sqlcmd1.CommandType = CommandType.StoredProcedure;
            sqlcmd1.Parameters.AddWithValue("@ProjectRef", int.Parse(pid));
            sqlcmd1.Parameters.AddWithValue("@Hours", "PM");
            SqlDataAdapter myadapter1 = new SqlDataAdapter(sqlcmd1);
            myadapter1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                Actualvalue1 = Convert.ToDecimal(dr1["Hours"].ToString());
            }
            ReturnValue = Budgetvalue.ToString() + "/" + Actualvalue1.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ReturnValue;
    }
    public string StaffHoursdata(string pid)
    {
        string ReturnValue = string.Empty;
        decimal Budgetvalue = 0;
        decimal Actualvalue1 = 0;
        try
        {
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_GraphBudgetedhours";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", int.Parse(pid));
            sqlcmd.Parameters.AddWithValue("@Hours", "Staff");
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Budgetvalue = Convert.ToDecimal(dr["Hours"]);
            }
            //  StaffBudget = Decimal.ToInt32(value);

            string cmd1 = "Project_GraphActualhours";
            DataTable dt1 = new DataTable();
            SqlCommand sqlcmd1 = new SqlCommand(cmd1, sqlcon);
            sqlcmd1.CommandType = CommandType.StoredProcedure;
            sqlcmd1.Parameters.AddWithValue("@ProjectRef", int.Parse(pid));
            sqlcmd1.Parameters.AddWithValue("@Hours", "Staff");
            SqlDataAdapter myadapter1 = new SqlDataAdapter(sqlcmd1);
            myadapter1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                Actualvalue1 = Convert.ToDecimal(dr["Hours"]);
            }
            ReturnValue = Budgetvalue.ToString() + "/" + Actualvalue1.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ReturnValue;
    }
    public void MailSendingList(int ProjectId)
    {
        try
        {
            string mailsendChecking = string.Empty;
            string BodyHTML = ReadFile("~/WF/Projects/EmailTemplates/BudgetexceededalertTemplate.html");
            string FromMail = Deffinity.systemdefaults.GetFromEmail();
            string Subject = "Ref:" + sessionKeys.Prefix + ProjectId.ToString() + " Budget exceeded alert";

            string tableHtml = string.Empty;
            tableHtml = " <table style='float:left;width:100%;'><tr><th>Item:</th><th>Budget:</th><th>Actual:</th><th>Difference:</th></tr>";

            string StaffValue = StaffHoursdata(ProjectId.ToString()).ToString();
            string[] StaffValueArray = StaffValue.Split('/');

            if (Convert.ToDecimal(StaffValueArray[0]) < Convert.ToDecimal(StaffValueArray[1]))
            {
                tableHtml = tableHtml + "<tr><td>Staff Hours</td><td style='text-align:center'>" + StaffValueArray[0].ToString() + "</td><td style='text-align:center'>" + StaffValueArray[1].ToString() + "</td><td style='text-align:center'>" + (Convert.ToDecimal(StaffValueArray[0]) - Convert.ToDecimal(StaffValueArray[1])).ToString().Replace('-', ' ') + "</td></tr>";
                mailsendChecking = "Send";
            }


            string PmValue = PMHoursdata(ProjectId.ToString());
            string[] PmValueArray = PmValue.Split('/');

            if (Convert.ToDecimal(PmValueArray[0]) < Convert.ToDecimal(PmValueArray[1]))
            {
                tableHtml = tableHtml + "<tr><td>PM Hours</td><td style='text-align:center'>" + PmValueArray[0].ToString() + "</td><td style='text-align:center'>" + PmValueArray[1].ToString() + "</td><td style='text-align:center'>" + (Convert.ToDecimal(PmValueArray[0]) - Convert.ToDecimal(PmValueArray[1])).ToString().Replace('-', ' ') + "</td></tr>";
                mailsendChecking = "Send";
            }

            tableHtml = tableHtml + "</table>";
            BodyHTML = BodyHTML.Replace("[TableData]", tableHtml);
            BodyHTML = BodyHTML.Replace("[ref]", sessionKeys.Prefix + ProjectId.ToString());
            BodyHTML = BodyHTML.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
            BodyHTML = BodyHTML.Replace("[mail_head]", "Alert");
            BodyHTML = BodyHTML.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            BodyHTML = BodyHTML.Replace("[AccessLink]", Deffinity.systemdefaults.GetWebUrl());


            if (mailsendChecking == "Send")
            {
                using (projectTaskDataContext pdc = new projectTaskDataContext())
                {
                    using (UserDataContext udc = new UserDataContext())
                    {
                        var EmailList = pdc.Projects.Where(a => a.ProjectReference == ProjectId).Select(a => a).ToList();
                        foreach (var x in EmailList)
                        {
                            string Name = udc.Contractors.Where(a => a.ID == x.OwnerID).Select(a => a.ContractorName).FirstOrDefault();
                            BodyHTML = BodyHTML.Replace("[user name]", Name);
                            SendingMail(FromMail, Subject, BodyHTML, x.OwnerEmail);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void SendingMail(string FromEmailID, string Subject, string strHTML, string ToEmailID)
    {
        try
        {
            addressFrom = new MailAddress(FromEmailID);
            addressTo = new MailAddress(ToEmailID);
            MailMessage mailMessage = new MailMessage(addressFrom, addressTo);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = strHTML;

            mailClient = new SmtpClient();
            mailClient.UseDefaultCredentials = true;
            mailClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "mail.send function");
        }
    }

}