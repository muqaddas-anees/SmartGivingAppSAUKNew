using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using ProjectMgt.DAL;
using TimesheetMgt.DAL;
using UserMgt.BAL;
using UserMgt.DAL;
using UserMgt.Entity;
using ClosedXML.Excel;

public class ExcelField
{
    public string Date { get; set; }
    public string ResourceName { get; set; }
    public string projectRef { get; set; }
    public string ProjectTask { get; set; }
    public string EntryType { get; set; }
    public string HoursInExcel { get; set; }
    public string Site { get; set; }
    public string Notes { get; set; }
    public string Comments { get; set; }
}
public class Emailfields
{
    public int Userid { get; set; }
    public int TimesheetId { get; set; }
}
public class ExcelDownLoad
{
    SqlConnection con = new SqlConnection(Constants.DBString);
    projectTaskDataContext pdc = null;
    UserDataContext udc = null;
    public void SendingMail(int pid, int rid)
    {
        udc = new UserDataContext();
        string fromemailid = Deffinity.systemdefaults.GetFromEmail();
        string rname = udc.Contractors.Where(a => a.ID == rid).Select(a => a.ContractorName).FirstOrDefault();
        string Description=""; 
        string nameofProjectOwner="";
        string ContactNumber="";
        string ToEmail = udc.Contractors.Where(a => a.ID == rid).Select(a => a.EmailAddress).FirstOrDefault();
        Emailer em = new Emailer();
        string subject = "Your hours have been booked to project " + pid;
        string body = em.ReadFile("~/EmailTemplates/CasualUsersEmail.html");
        body = body.Replace("[user]", rname);
        body = body.Replace("[PRef]", pid.ToString());
        body = body.Replace("[Description]", Description);
        body = body.Replace("[name of Project Owner]", nameofProjectOwner);
        body = body.Replace("[Contact Number]", ContactNumber);
        em.SendingMail(fromemailid, subject, body, ToEmail);
    }
    public DataTable resourceslist(int pid, int UID)
    {
        DataTable tblResourceNames = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("DN_Labourcontrators", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjectReference", pid);
            cmd.Parameters.AddWithValue("@UID", UID);
            SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
            myadapter.Fill(tblResourceNames);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return tblResourceNames;
    }
    public DataTable Sitelist(int pid)
    {
        DataTable tblsite = new DataTable();
        try
        {
            SqlCommand cmdsite = new SqlCommand("DN_TimesheetSite_Customer", con);
            cmdsite.CommandType = CommandType.StoredProcedure;
            cmdsite.Parameters.AddWithValue("@ProjectReference", pid);
            SqlDataAdapter myadaptersite = new SqlDataAdapter(cmdsite);
            myadaptersite.Fill(tblsite);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return tblsite;
    }
    public XLWorkbook downloadExcelFile(int pid,int UID, List<ExcelField> exl,out string ProjectRef)
    {
        var wb = new ClosedXML.Excel.XLWorkbook();

        using (projectTaskDataContext pdc = new projectTaskDataContext())
        {
            using (TimeSheetDataContext tdc = new TimeSheetDataContext())
            {
                int rowstart = 2;
                int rowend = 300;

                int Namescount = 1;
                int Taskcount = 1;
                int EntryTypecount = 1;
                int Sitecount = 1;
                int pCount = 1;
                int count = 2;
                var ProjectDefaults = pdc.ProjectDefaults.FirstOrDefault();
                var EntryTypeList = (from a in tdc.TimesheetEntryTypes select a).ToList();
                var tasklist = (from a in pdc.ProjectTaskItems where a.ProjectReference == pid select a).ToList();
                DataTable tblResourceNames = resourceslist(pid, UID);
                DataTable tblsite = Sitelist(pid);
                int i = tblResourceNames.Rows.Count;
                int x = tblsite.Rows.Count;
                ProjectRef = ProjectDefaults.ProjectReferencePrefix + pid.ToString();
                var ws = wb.Worksheets.Add("Project - " + ProjectRef);
                var ws_data = wb.Worksheets.Add("Data");
                var projectList = (from a in pdc.Projects where a.ProjectReference == pid select a).ToList();
                for (int j = 1; j < i; j++)
                {
                    ws_data.Cell(Namescount, 1).Value = tblResourceNames.Rows[j].Field<string>("ContractorName").ToString();
                    Namescount++;
                }
                foreach (var d in tasklist)
                {
                    ws_data.Cell(Taskcount, 2).Value = d.ItemDescription.ToString();
                    Taskcount++;
                }
                foreach (var d in EntryTypeList)
                {
                    ws_data.Cell(EntryTypecount, 3).Value = d.EntryType.ToString();
                    EntryTypecount++;
                }
                for (int y = 0; y < x; y++)
                {
                    ws_data.Cell(Sitecount, 4).Value = tblsite.Rows[y].Field<string>("Site").ToString();
                    Sitecount++;
                }
                //foreach (var p in projectList)
                //{
                //    ws_data.Cell(pCount, 5).Value = p.ProjectTitle.ToString() + "(" + p.ProjectReference + ")".ToString();
                //}
                ws.Cell("A1").Value = "Date";
                ws.Cell("B1").Value = "Resource";
                //  ws.Cell("C1").Value = "Project Reference";
                ws.Cell("C1").Value = "Project Task";
                ws.Cell("D1").Value = "Entry Type";
                ws.Cell("E1").Value = "Hours";
                ws.Cell("F1").Value = "Site";
                ws.Cell("G1").Value = "Notes";
                TimeSpan t1 = TimeSpan.Parse("00:00");
                TimeSpan t2 = TimeSpan.Parse("23:59");

                for (int r = rowstart; r <= rowend; r++)
                {
                    ws.Cell("B" + r.ToString()).DataValidation.List(ws_data.Range("A1:A" + (Namescount - 1).ToString()));
                }
                for (int r = rowstart; r <= rowend; r++)
                {
                    ws.Cell("B" + r.ToString()).DataValidation.List(ws_data.Range("A1:A" + (Namescount - 1).ToString()));
                }
                for (int r = rowstart; r <= rowend; r++)
                {
                    ws.Cell("C" + r.ToString()).DataValidation.List(ws_data.Range("B1:B" + (Taskcount - 1).ToString()));
                }
                for (int r = rowstart; r <= rowend; r++)
                {
                    ws.Cell("D" + r.ToString()).DataValidation.List(ws_data.Range("C1:C" + (EntryTypecount - 1).ToString()));
                }
                for (int r = rowstart; r <= rowend; r++)
                {
                    ws.Cell("F" + r.ToString()).DataValidation.List(ws_data.Range("D1:D" + (Sitecount - 1).ToString()));
                }
                for (int r = rowstart; r <= rowend; r++)
                {
                    ws.Cell("E" + r.ToString()).DataValidation.InputMessage = "please input time in HH:MM (24-hr) format only; from 00:00 to 23:59";
                    ws.Cell("E" + r.ToString()).DataValidation.Time.Between(t1, t2);
                }
                for (int r = rowstart; r <= rowend; r++)
                {
                    ws.Cell("A" + r.ToString()).DataValidation.InputMessage = "please input date in MM/DD/YYYY format only";
                    ws.Cell("A" + r).Style.DateFormat.Format = "dd-MM-yyyy";
                    //ws.Cell("A" + r).DataType = XLCellValues.Text;
                }
                
              

                 ws_data.Hide();
                //add the download data
                 if (exl.Count != 0)
                 {
                     ws.Cell("H1").Value = "Comments";
                     var rngHeadersnew = ws.Range("A1:H1");
                     rngHeadersnew.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                     rngHeadersnew.Style.Font.Bold = true;
                     rngHeadersnew.Style.Font.FontColor = XLColor.White;
                     rngHeadersnew.Style.Fill.BackgroundColor = XLColor.DarkGray;
                     foreach (var e in exl)
                     {
                        
                         ws.Cell("A" + count).Value = string.Format( "{0:dd-MM-yyyy}",  Convert.ToDateTime(e.Date));
                         ws.Cell("B" + count).Value = e.ResourceName;
                         //  ws.Cell("C" + count).Value = e.projectRef;
                         ws.Cell("C" + count).Value = e.ProjectTask;
                         ws.Cell("D" + count).Value = e.EntryType;
                         ws.Cell("E" + count).Value = e.HoursInExcel;
                         ws.Cell("F" + count).Value = e.Site;
                         ws.Cell("G" + count).Value = e.Notes;
                         ws.Cell("H" + count).Value = e.Comments;
                         if (e.Comments.Contains("Timesheet entered successfully"))
                             ws.Cell("H" + count).Style.Font.FontColor = XLColor.Green;
                         else
                             ws.Cell("H" + count).Style.Font.FontColor = XLColor.Red;
                         count++;
                     }
                   
                 }
                 else
                 {
                     var rngHeaders = ws.Range("A1:G1");
                     rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                     rngHeaders.Style.Font.Bold = true;
                     rngHeaders.Style.Font.FontColor = XLColor.White;
                     rngHeaders.Style.Fill.BackgroundColor = XLColor.DarkGray;
                 }
                
            }
        }
        
        return wb;
    }
}
