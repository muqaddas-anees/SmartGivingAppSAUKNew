<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using Microsoft.SqlServer.Server;
using ProjectMgt.BAL;
using ProjectMgt.BLL;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using TimesheetMgt.BAL;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using Location.DAL;
using Location.Entity;
using UserMgt.BAL;
using UserMgt.DAL;
using UserMgt.Entity;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Globalization;
public class UploadHandler : IHttpHandler {
    projectTaskDataContext pdc = null;
    TimeSheetDataContext tdc = null;
    UserDataContext udc = null;
    LocationDataContext ldc = null;
    ExcelField e = null;
    List<ExcelField> elist = null;
    ExcelDownLoad eDownload = null;
    Emailfields efields = null;
    List<Emailfields> e_fields_list = new List<Emailfields>();
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            if (context.Request.QueryString["project"] != null)
            {
                CultureInfo culture  = CultureInfo.CreateSpecificCulture(context.Request.UserLanguages[0]);
                LogExceptions.LogException(culture.DateTimeFormat.ShortDatePattern, "error date");
            
                var pid = Convert.ToInt32(context.Request.QueryString["project"].ToString());
                var UID = Convert.ToInt32(context.Request.QueryString["UID"].ToString());
                HttpFileCollection files = context.Request.Files;
                int count = files.Count;
                if (count != 0)
                {
                    foreach (string key in files)
                    {
                        HttpPostedFile file = files[key];
                        string fileName = file.FileName;
                        string Extension = Path.GetExtension(file.FileName);
                        if (IsValid(fileName))
                        {
                            string path = context.Server.MapPath("~/WF/UploadData/TimesheetFiles");
                            fileName = "\\" + fileName;
                            if (Directory.Exists(path) == false)
                            {
                                Directory.CreateDirectory(path);
                            }
                            file.SaveAs(path + fileName);
                            if (Extension != ".csv")
                            {
                                string conStr = string.Empty;
                                //string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 8.0;"; ;
                                switch (Extension)
                                {
                                    case ".xls": //Excel 97-03
                                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1;HDR={1}'";
                                        break;
                                    case ".xlsx": //Excel 07
                                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1;HDR={1}'";
                                        break;
                                }
                                conStr = String.Format(conStr, path + fileName, "No");

                                int ErrorsCount = 0;

                                XLWorkbook wb = ExcelUpload("Excel", path, fileName, conStr, pid, UID, context, out ErrorsCount);
                                
                                //send mail to user for success uploading
                                IProjectRepository<ProjectMgt.Entity.ProjectDefault> pd = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                                var pdefaults = pd.GetAll().FirstOrDefault();
                                SendingMail(pdefaults.ProjectReferencePrefix,pid, e_fields_list);
                                
                              
                                 string newFile = path + context.Server.MapPath("~/WF/UploadData/TimesheetFiles")  + string.Format("\\{0}{1}_excepiton.xlsx", pdefaults.ProjectReferencePrefix, pid);
                                 if (File.Exists(newFile))
                                 {
                                     File.Delete(newFile);
                                 }
                                 wb.SaveAs(newFile);
                                context.Response.ContentType = "text/plain";
                                context.Response.Write(ErrorsCount.ToString());
                            }
                        }
                        else
                        {
                            string s = "";
                            context.Response.ContentType = "text/plain";
                            context.Response.Write("Please select a valid file.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            throw;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public void SendingMail(string prefix,int pid,List<Emailfields> emfls)
    {
        var listofids = emfls.Select(a => a.Userid).Distinct().ToList();
        udc=new UserDataContext();
        pdc=new projectTaskDataContext();
        tdc = new TimeSheetDataContext();

        var userList = udc.Contractors.Where(o => emfls.Select(a => a.Userid).ToArray().Contains(o.ID)).ToList();
        var projectDetails = pdc.ProjectDetails.Where(o => o.ProjectReference == pid).ToList();
        var ownerDetails = udc.Contractors.Where(o => o.ID == projectDetails.Select(p=>p.OwnerID).FirstOrDefault()).FirstOrDefault();
        string fromemailid = Deffinity.systemdefaults.GetFromEmail();
        var timesheetList = tdc.TimesheetEntries.Where(o => emfls.Select(a => a.TimesheetId).ToArray().Contains(o.ID)).ToList();
        foreach (var efs in listofids)
        {

            int OwnerID = projectDetails.Select(o=>o.OwnerID.Value).FirstOrDefault();
            string rname = userList.Where(a => a.ID == efs).Select(a => a.ContractorName).FirstOrDefault();
            string Description = projectDetails.Select(a => a.ProjectTitle).FirstOrDefault();
            string nameofProjectOwner = projectDetails.Select(o => o.OwnerName).FirstOrDefault();
            string ContactNumber = ownerDetails.ContactNumber;
            string ToEmail = userList.Where(a => a.ID == efs).Select(a => a.EmailAddress).FirstOrDefault();
            string table = string.Empty;
            Emailer em = new Emailer();
            string subject = "Your hours have been booked to project " + prefix + pid.ToString();
            string body = em.ReadFile("~/WF/Projects/EmailTemplates/CasualUsersEmail.html");
            body = body.Replace("[user]", rname);
        //  body = body.Replace("[PRef]", pid.ToString());
            body = body.Replace("[Description]", prefix + pid.ToString() + " - " + Description);
            body = body.Replace("[name of Project Owner]", nameofProjectOwner);
            body = body.Replace("[Contact Number]", ContactNumber);
            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            var timesheetlist = emfls.Where(a => a.Userid == efs ).Select(a => a.TimesheetId).ToList();
            table = "<table style='width:80%'><tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'><th>Date</th><th>Type</th><th>Hours</th></tr>";
            foreach (var x in timesheetlist)
            {
                var t = (from a in timesheetList where a.ID == x select a).FirstOrDefault();
                string EntryType = (from a in tdc.TimesheetEntryTypes where a.ID == t.EntryType select a.EntryType).FirstOrDefault();
                table = table + "<tr><td>" + t.DateEntered.Value.Date.ToShortDateString() + "</td><td>" + EntryType + "</td><td>" + t.Hours + "</td></tr>";
            }
            table = table + "</table>";
            body = body.Replace("[table]", table);
            em.SendingMail(fromemailid, subject, body, ToEmail);
        }
    }
    private bool IsValid(string fileName)
    {
        string ext = Path.GetExtension(fileName);
        switch (ext.ToLower())
        {
            case ".xlsx":
                return true;
            case ".xls":
                return true;
            default:
                return false;
        }
    }
    private DataTable GetProjectTaskList(int project, int resourceid)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_GetProjectTaks", new SqlParameter("@ProjectReference", project), new SqlParameter("@contractorID", resourceid)).Tables[0];
    }
    public string rowInsert(ExcelField erow, int pid, List<UserMgt.Entity.Contractor> CList, List<ProjectItem> pItemsList, List<ProjectTaskItem> projectTaskItems, List<Site> sitesList, HttpContext context)
    {
        string ReturnValue = string.Empty;
        try
        {
            //DateTime tempdate;
            int Resourceid = 0;
            int Taskid = 0;
            int entrytypeId = 0;
            int siteid = 0;

           
            //var datetoEnter1 = DateTime.ParseExact(erow.Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //string rDate = string.Format("{0:d}", erow.Date);

            CultureInfo culture = CultureInfo.CreateSpecificCulture(context.Request.UserLanguages[0]);
            var str = culture.DateTimeFormat.ShortDatePattern;
            string d = erow.Date.Trim();
            DateTime? excelDate = null;

            try
            {
                string[] sp = d.Split('/');
                if (sp.Count() > 1)
                {
                    d = sp[1] + "/" + sp[0] + "/" + sp[2];
                    excelDate = new DateTime(int.Parse(sp[2]), int.Parse(sp[1]), int.Parse(sp[0]));
                }
                else
                {
                    string[] sp1 = d.Split('-');
                    d = sp1[1] + "/" + sp1[0] + "/" + sp1[2];
                    excelDate = new DateTime(int.Parse(sp1[2]), int.Parse(sp1[1]), int.Parse(sp1[0]));
                }
            }
            catch(Exception ex)
            {
                excelDate = null;
                LogExceptions.WriteExceptionLog(ex);
            }
            
            //if the date format is start with month
            //if (str.ToLower().StartsWith("m"))
            //{
            //    string[] sp = d.Split('/');
            //    if (sp.Count() > 1)
            //    {
            //        d = sp[1] + "/" + sp[0] + "/" + sp[2];
            //        excelDate = new DateTime(int.Parse(sp[2]), int.Parse(sp[0]), int.Parse(sp[1]));
            //    }
            //    else
            //    {
            //        string[] sp1 = d.Split('-');
            //        d = sp1[1] + "/" + sp1[0] + "/" + sp1[2];
            //        excelDate = new DateTime(int.Parse(sp1[2]), int.Parse(sp1[0]), int.Parse(sp1[1]));
            //    }
            //}
            ////if the date format is start with date
            //else
            //{
            //    string[] sp = d.Split('/');
            //    if (sp.Count() > 1)
            //    {
            //        d = sp[1] + "/" + sp[0] + "/" + sp[2];
            //        excelDate = new DateTime(int.Parse(sp[2]), int.Parse(sp[1]), int.Parse(sp[0]));
            //    }
            //    else
            //    {
            //        string[] sp1 = d.Split('-');
            //        d = sp1[1] + "/" + sp1[0] + "/" + sp1[2];
            //        excelDate = new DateTime(int.Parse(sp1[2]), int.Parse(sp1[1]), int.Parse(sp1[0]));
            //    }
            //}

            //DateTime date = DateTime.Parse(erow.Date, new CultureInfo("en-GB"));
            var s = excelDate;// Convert.ToDateTime();
            //LogExceptions.LogException(erow.Date.ToString() + " - " + rDate, "error date");
            if (excelDate.HasValue)
            {
                char[] comm = { ':' };
                string[] getva = e.HoursInExcel.Split(comm);
                string newval = "";
                newval = getva[0] + "." + getva[1];
                double hours = Convert.ToDouble(newval);

                string Cname = erow.ResourceName.ToString();


                CList = CList.Where(a => a.ContractorName.ToLower() == Cname.ToLower()).ToList();
                if (CList.Count == 1)
                {
                    Resourceid = CList.Select(a => a.ID).FirstOrDefault();
                }
                else if (CList.Count > 1)
                {
                    foreach (var x in CList)
                    {
                        pItemsList = pItemsList.Where(a => a.ProjectReference == pid && a.ContractorID == x.ID).ToList();
                        if (pItemsList.Count == 1)
                        {
                            Resourceid = pItemsList.Select(a => a.ContractorID.HasValue ? a.ContractorID.Value : 0).FirstOrDefault();
                            break;
                        }
                    }
                }

                string tname = erow.ProjectTask.ToString();
                projectTaskItems = projectTaskItems.Where(a => a.ProjectReference == pid).ToList();
                if (projectTaskItems.Count != 0)
                {
                    Taskid = projectTaskItems.Where(a => a.ItemDescription.ToLower() == tname.ToLower()).Select(a => a.ID).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(erow.Site))
                {
                    string sname = erow.Site;
                    sitesList.Where(a => a.Site1.ToLower() == sname.ToLower()).FirstOrDefault();
                    siteid = sitesList.Where(a => a.Site1.ToLower() == sname.ToLower()).Select(a => a.ID).FirstOrDefault();
                }
                
                string EName = erow.EntryType;
                tdc = new TimeSheetDataContext();
                entrytypeId = tdc.TimesheetEntryTypes.Where(a => a.EntryType.ToLower() == EName.ToLower()).Select(a => a.ID).FirstOrDefault();

                

                DataTable Dt = GetProjectTaskList(pid, Resourceid);
                DataRow dr = Dt.Select("TaskID=" + Taskid.ToString()).FirstOrDefault();
                if (dr != null)
                {
                    Database db = DatabaseFactory.CreateDatabase("DBstring");
                    DbCommand cmd = db.GetStoredProcCommand("DN_TimesheetEntry_Excel");
                    db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, pid);
                    db.AddInParameter(cmd, "@Activity", DbType.String, string.Empty);
                    db.AddInParameter(cmd, "@AssignType", DbType.Int32, 0);
                    db.AddInParameter(cmd, "@ResourceID", DbType.Int32, Resourceid);
                    db.AddInParameter(cmd, "@entryid", DbType.Int32, entrytypeId);
                    
                    //var s = Convert.ToDateTime(string.Format("{0} {1}", d, DateTime.Now.ToShortTimeString()));
                    db.AddInParameter(cmd, "@Timesheetdate", DbType.DateTime, s);
                    db.AddInParameter(cmd, "@hours", DbType.Double, hours);
                    db.AddInParameter(cmd, "@notes", DbType.String, erow.Notes);
                    db.AddInParameter(cmd, "@SiteID", DbType.Int32, siteid);
                    db.AddInParameter(cmd, "@ProjectTaskID", DbType.Int32, Taskid);
                    db.AddOutParameter(cmd, "@output", DbType.Int32, 0);
                    db.AddOutParameter(cmd, "@outid", DbType.Int32, 0);
                    db.ExecuteNonQuery(cmd);
                    int output = (int)db.GetParameterValue(cmd, "@output");
                    int outputID = (int)db.GetParameterValue(cmd, "@outid");
                    cmd.Dispose();
                    ReturnValue = outputvalue(output);
                    if (output == 1)
                    {
                        efields = new Emailfields();
                        efields.TimesheetId = outputID;
                        efields.Userid = Resourceid;
                        e_fields_list.Add(efields);
                    }
                }
                else
                {
                    //ReturnValue = "Please select valid Task.";
                    ReturnValue = "Resource is not assigned to this task. Please check and try again";
                }
            }
            else
            {
                ReturnValue = "Please enter valid date format";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            ReturnValue = "Please enter valid data";
        }
        return ReturnValue;
    }
   
    public string outputvalue(int output)
    {
        string rvalue = string.Empty;
        if (output == 0)
        {
            rvalue = "Error While inserting";
        }
        else if (output == 1)
        {
            rvalue="Timesheet entered successfully" ;
        }
        else if (output == 2)
        {
            rvalue = "You cannot add an entry to a timesheet that has been submitted for approval";
        }
        else if (output == 3)
        {
            rvalue = "Timesheet entry already exists";
        }
        else if (output == 4)
        {
            rvalue = "Vacation request already exists for this date";
        }
        else if (output == 10)
        {
            rvalue = "You have exceeded 24 hours for the date entered ";
        }
        else if (output == 5)
        {
            rvalue = "Please enter date with in the current week.";
        }
        return rvalue;
    }
    private XLWorkbook ExcelUpload(string type, string path, string fileName, string conStr, int pid, int UId, HttpContext context, out int ErrorsCount)
    {
        int E = 0;
        ErrorsCount = 0;
        var wb = new ClosedXML.Excel.XLWorkbook();
        try
        {
            List<UserMgt.Entity.Contractor> Clist = null;
            List<ProjectItem> PitemList = null;
            List<Site> SiteList = null;
            List<ProjectTaskItem> PTaskItems = null;


            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    Clist = Udc.Contractors.ToList();
                    PitemList = Pdc.ProjectItems.Where(o=>o.ProjectReference == pid).ToList();
                }
                PTaskItems = Pdc.ProjectTaskItems.Where(o=>o.ProjectReference == pid).ToList();
            }
            using (LocationDataContext Ldc = new LocationDataContext())
            {
                SiteList = Ldc.Sites.ToList();
            }



            elist = new List<ExcelField>();
            DataTable dtable = Import_To_Grid(conStr);
            //LogExceptions.LogException("table", dtable.Rows.Count.ToString());
            DataRow[] dr_list = dtable.Select("F1 <> '' and F1 <> 'Date'");
            //LogExceptions.LogException("row", dr_list.Count().ToString());
            for (int y = 0; y < dr_list.Count(); y++)
            {
                e = new ExcelField();
                try
                {
                    e.Date = dr_list[y][0].ToString();
                    e.ResourceName = dr_list[y][1].ToString().Trim();
                    //e.projectRef = dtable.Rows[y][2].ToString().Trim();
                    e.ProjectTask = dr_list[y][2].ToString().Trim();
                    e.EntryType = dr_list[y][3].ToString().Trim();
                    e.HoursInExcel = dr_list[y][4].ToString().Trim();
                    e.Site = dr_list[y][5].ToString().Trim();
                    e.Notes = dr_list[y][6].ToString().Trim();
                    //var eDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dr_list[y][0]);
                    //var vDate = string.Format("{0:d}", dr_list[y][0]);
                    e.Comments = rowInsert(e, pid, Clist, PitemList, PTaskItems, SiteList, context);
                    if (e.Comments != "Timesheet entered successfully")
                    {
                        E = E + 1;
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                elist.Add(e);
             
            }
           
            eDownload = new ExcelDownLoad();
            string ProjectRef = string.Empty;
            wb = eDownload.downloadExcelFile(pid, UId, elist, out ProjectRef);
            
            ErrorsCount = E;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        return wb;
       
        
    }
    public void Sheetdownload(XLWorkbook wb, HttpContext context)
    {
        HttpResponse httpResponse = context.Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        httpResponse.AddHeader("content-disposition", "attachment;filename=\"list.xlsx\"");
        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            wb.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }
        httpResponse.End(); 
    }
    public string[] GetExcelSheetNames(string connectionString)
    {
        OleDbConnection con = null; DataTable dt = null;
        String conStr = connectionString;
        con = new OleDbConnection(conStr);
        con.Close();
        con.Open();
        dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        if (dt == null)
        {
            return null;
        }
        String[] excelSheetNames = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            excelSheetNames[i] = row["TABLE_NAME"].ToString();
            i++;
        }
        con.Close();
        return excelSheetNames;
    }
    private DataTable Import_To_Grid(string conStr)
    {
        string[] sheetnames = GetExcelSheetNames(conStr);
        string sheetname = string.Empty;
        if (sheetnames.Length > 0)
            sheetname = sheetnames[1];

        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //string SheetName = "DealerVoice$";
        cmdExcel.CommandText = string.Format("SELECT  * From  [{0}]", sheetname);
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();
        return dt;

    }

}