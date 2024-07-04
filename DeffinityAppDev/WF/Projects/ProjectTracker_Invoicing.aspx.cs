using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class ProjectTracker_Invoicing : System.Web.UI.Page
{
    string fileName = "PandL11.xls";
    RiseValuation RiseVal = new RiseValuation();
    protected int getProject = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = Resources.DeffinityRes.ProjectManagement;//"Project Management";
        getProject = QueryStringValues.Project;
    }
    protected void btnPandL_Click(object sender, EventArgs e)
    {
        try
        {
            getVariations(QueryStringValues.Project);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    #region PandL
    private void getVariations(int pref)
    {
        //readfile
        getFileDataToWrite(pref);

        string path = Server.MapPath("Upload") + "\\" + fileName;
        Response.ContentType = "application/ms-excel";
        //Response.ContentType = "text / HTML";
        Response.WriteFile(path);
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);

        Response.End();


    }
    private void getFileDataToWrite(int pref)
    {
        try
        {

            FileInfo fi = new FileInfo(Server.MapPath("Upload") + "\\" + fileName);
            if (fi.Exists)
            {
                fi.Delete();
            }

            //write excel file
            FileStream file = new FileStream(Server.MapPath("Upload") + "\\" + fileName, FileMode.OpenOrCreate, FileAccess.Write);

            // Create a new stream to write to the file
            StreamWriter sw = new StreamWriter(file);

            // Write a string to the file
            sw.Write(RiseVal.GetPandLString(pref));

            // Close StreamWriter
            sw.Close();
            file.Dispose();
            // Close file
            file.Close();
        }
        catch (Exception ex)
        {

        }

    }
    #endregion
    
}