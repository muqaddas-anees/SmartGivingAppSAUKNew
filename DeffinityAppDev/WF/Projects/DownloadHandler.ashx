<%@ WebHandler Language="C#" Class="DownloadHandler" %>

using System;
using System.Web;
using ProjectMgt.BAL;
using ProjectMgt.BLL;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using TimesheetMgt.BAL;
using TimesheetMgt.DAL;
using TimesheetMgt.Entity;
using System.Linq;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;


public class DownloadHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    ExcelDownLoad eDownload=null;
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            if (context.Request.QueryString["Project"] != null)
            {
                int pid = int.Parse(context.Request.QueryString["Project"]);
                IProjectRepository<ProjectMgt.Entity.ProjectDefault> pd = new ProjectRepository<ProjectMgt.Entity.ProjectDefault>();
                var pdefaults = pd.GetAll().FirstOrDefault();
                if (context.Request.QueryString["download"] != null)
                {
                    string newFile = context.Server.MapPath(string.Format("WF\\UploadData\\TimesheetFiles\\{0}{1}_excepiton.xlsx", pdefaults.ProjectReferencePrefix, pid));
                    if (File.Exists(newFile))
                    {
                        //File.Copy(resumeFile, newFile);
                        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        context.Response.AppendHeader("content-disposition", "attachment;filename=\"" + pdefaults.ProjectReferencePrefix + pid.ToString() + "_excepiton.xlsx\"");
                        context.Response.TransmitFile(newFile);
                        context.Response.End();
                    }
                }
                else
                {
                    int UID = int.Parse(context.Request.QueryString["UID"]);
                    List<ExcelField> ef = new List<ExcelField>();
                    eDownload = new ExcelDownLoad();
                    string ProjectRef = string.Empty;
                    XLWorkbook wb = eDownload.downloadExcelFile(pid, UID, ef, out ProjectRef);

                    HttpResponse httpResponse = context.Response;
                    httpResponse.Clear();
                    httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + ProjectRef + "_Template.xlsx\"");
                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        wb.SaveAs(memoryStream);
                        memoryStream.WriteTo(httpResponse.OutputStream);
                        memoryStream.Close();
                    }
                    httpResponse.End();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}