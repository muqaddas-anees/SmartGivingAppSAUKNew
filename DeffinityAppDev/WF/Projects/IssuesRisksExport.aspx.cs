﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using Deffinity.BLL;
using TimeSheet_Admin;
using ProjectMgt.DAL;
using System.Linq;
using System.Data.OleDb;
public partial class IssuesRisksExport : System.Web.UI.Page
{
    public string Status;
    public string ProjectApproval;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";

        //Master.PageHead = "Issues & Risks";
        if (!Page.IsPostBack)
        {
            try
            {

                lblHead.InnerText = "List of Issues & Risks";        
                BindPortfolio();
                BindProjects();


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
    private void BindProjects()
    {
        projectTaskDataContext p = new projectTaskDataContext();
        var projects = from r in p.ProjectDetails
                       where r.ProjectTitle != ""
                       orderby r.ProjectReference
                       select new { r.ProjectReference, Name = r.ProjectReferenceWithPrefix + "-" + r.ProjectTitle };

        ddlProjects.DataSource = projects;
        ddlProjects.DataTextField = "Name";
        ddlProjects.DataValueField = "ProjectReference";
        ddlProjects.DataBind();
        ddlProjects.Items.Insert(0, "Please Select...");
    }
    private void BindGridSearch()
    {
        ExportToExcelController _ExportToExcelController = new ExportToExcelController();
        int intProjects = 0;
        int intIncidentStatus = 0;
        if (!ddlProjects.SelectedValue.Contains("Please Select"))
        {
            intProjects = Convert.ToInt32((ddlProjects.SelectedValue));
        }        
        

        intIncidentStatus = Convert.ToInt32((string.IsNullOrEmpty(ddlIncidentStatus.SelectedValue) ? "0" : ddlIncidentStatus.SelectedValue));
        int intPortfolio = Convert.ToInt32((string.IsNullOrEmpty(ddlPortfolio.SelectedValue) ? "0" : ddlPortfolio.SelectedValue));
        
        Gridview1.DataSource = _ExportToExcelController.GetIssuelistByProject(intPortfolio, intIncidentStatus, intProjects);
        Gridview1.DataBind();
       
        if (Gridview1.Rows.Count > 0)
        {
            btnExportToExcel.Visible = true;
        }
        else
        {
            btnExportToExcel.Visible = false;
        }
    }
    protected void ddlIncidentStatus_DataBound(object sender, EventArgs e)
    {
        if (ddlIncidentStatus.Items.Count > 0)
        {         
            ddlIncidentStatus.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    private void BindPortfolio()
    {
        ddlPortfolio.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        ddlPortfolio.DataTextField = "PortFolio";
        ddlPortfolio.DataValueField = "ID";
        ddlPortfolio.DataBind();
        if (ddlPortfolio.Items.Count > 0)
        {
            ddlPortfolio.SelectedValue = sessionKeys.PortfolioID.ToString();
        }
    }   


    protected void btn_Searchprojects_Click(object sender, EventArgs e)
    {
        BindGridSearch();

    }
    
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        ddlProjects.SelectedIndex = 0;        
        ddlIncidentStatus.SelectedIndex = 0;
        ddlIncidentStatus.SelectedIndex = 0;
    }

    private void BindToExcel()
    {
        try
        {
            DataTable dt = null;

            ExportToExcelController _ExportToExcelController = new ExportToExcelController();
            int intProjects = 0;
            int intIncidentStatus = 0;
            if (!ddlProjects.SelectedValue.Contains("Please Select"))
            {
                intProjects = Convert.ToInt32((ddlProjects.SelectedValue));
            }

            intIncidentStatus = Convert.ToInt32((string.IsNullOrEmpty(ddlIncidentStatus.SelectedValue) ? "0" : ddlIncidentStatus.SelectedValue));
            int intPortfolio = Convert.ToInt32((string.IsNullOrEmpty(ddlPortfolio.SelectedValue) ? "0" : ddlPortfolio.SelectedValue));
            dt = _ExportToExcelController.GetIssuelistByProject(intPortfolio, intIncidentStatus, intProjects);
            string filename = Server.MapPath("~/WF/UploadData/Temp") + "\\IssuesRisk " + string.Format("{0:ddMMyyyymmss}", DateTime.Now) + ".xlsx";
            string sheetName = "IssuesRisksExport";
            ExportToExcel(dt, filename, sheetName);

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
            if (fileInfo.Exists)
            {
                Response.Clear();

                HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=IssuesRisksExport.xlsx");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            else
            {
                string s = "File not found";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public static int ExportToExcel(DataTable dt, string excelFile, string sheetName)
    {
        // Create the connection string
        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            excelFile + ";Extended Properties=Excel 12.0 Xml;";

        int rNumb = 0;
        try
        {
            using (OleDbConnection con = new OleDbConnection(connString))
            {
                con.Open();
                dt.Columns["StatusName"].SetOrdinal(4);
                dt.Columns["IssueTypeName"].SetOrdinal(5);

                dt.Columns["ScheduledDate"].ColumnName = "Scheduled Date";
                dt.Columns["ProjectReferenceWithPrefix"].ColumnName = "Project Ref";
                dt.Columns["ProjectTitle"].ColumnName = "Title";
                dt.Columns["ResolvedDate"].ColumnName = "Resolved Date";
                dt.Columns["IssueTypeName"].ColumnName = "Issue Type";
                dt.Columns["StatusName"].ColumnName = "Status";
               

                // Build the field names string
                StringBuilder strField = new StringBuilder();
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    strField.Append("[" + dt.Columns[i].ColumnName + "],");
                }
                strField = strField.Remove(strField.Length - 1, 1);

                // Create Excel sheet
                var sqlCmd = "CREATE TABLE [" + sheetName + "] (" + strField.ToString().Replace("]", "] text") + ")";
                OleDbCommand cmd = new OleDbCommand(sqlCmd, con);
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    // Insert data into Excel sheet
                    StringBuilder strValue = new StringBuilder();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                            strValue.Append("'" + dt.Rows[i][j].ToString().Replace("'"," ") + "',");
                            // strValue.Append("'" + AddSingleQuotes(dt.Rows[i][j].ToString()) + "',");

                    }
                    strValue = strValue.Remove(strValue.Length - 1, 1);

                    cmd.CommandText = "INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" +
                            strValue.ToString() + ")";
                    cmd.ExecuteNonQuery();
                    rNumb = i + 1;
                }


                con.Close();
            }
            return rNumb;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex); 
        }
        return -1;
    }

    public static string AddSingleQuotes(string origText)
    {
        string s = origText;
        int i = 0;

        while ((i = s.IndexOf("'", i)) != -1)
        {
            // Add single quote after existing
            s = s.Substring(0, i) + "'" + s.Substring(i);

            // Increment the index.
            i += 2;
        }
        return s;
    }
   

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        BindToExcel();
        //ExportToExcel(Gridview1);
    }
    //protected void ExportToExcel(GridView GridView1)
    //{
    //    Response.Clear();
    //    Response.ClearHeaders();
    //    Response.Buffer = true;

    //    Response.AddHeader("content-disposition",
    //     "attachment;filename=IssuesRisksExport.xls");
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);

    //    PrepareForExport(GridView1);
    //    //PrepareForExport(GridView2);

    //    Table tb = new Table();
    //    TableRow tr1 = new TableRow();
    //    TableCell cell1 = new TableCell();
    //    cell1.Controls.Add(GridView1);
    //    tr1.Cells.Add(cell1);
    //    //TableCell cell3 = new TableCell();
    //    //cell3.Controls.Add(GridView2);
    //    TableCell cell2 = new TableCell();
    //    cell2.Text = "&nbsp;";
    //    //if (rbPreference.SelectedValue == "2")
    //    //{
    //    //    tr1.Cells.Add(cell2);
    //    //    //tr1.Cells.Add(cell3);
    //    //    tb.Rows.Add(tr1);
    //    //}
    //    //else
    //    //{
    //        TableRow tr2 = new TableRow();
    //        tr2.Cells.Add(cell2);
    //        //TableRow tr3 = new TableRow();
    //        //tr3.Cells.Add(cell3);
    //        tb.Rows.Add(tr1);
    //        tb.Rows.Add(tr2);
    //        //tb.Rows.Add(tr3);
    //    //}
    //    tb.RenderControl(hw);

    //    //style to format numbers to string
    //    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
    //    Response.Write(style);
    //    Response.Output.Write(sw.ToString());
    //    Response.Flush();
    //    Response.End();
    //}
    //protected void PrepareForExport(GridView Gridview)
    //{
    //    //Gridview.AllowPaging = Convert.ToBoolean(rbPaging.SelectedItem.Value);
    //  //  Gridview.DataBind();

    //    //Change the Header Row back to white color
    //    Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF");

    //    //Apply style to Individual Cells
    //    for (int k = 0; k < Gridview.HeaderRow.Cells.Count; k++)
    //    {
    //        //Gridview.HeaderRow.Cells[k].Style.Add("background-color", "green");
    //        Gridview.HeaderRow.Cells[k].Style.Add("background-color", "white");
    //    }

    //    for (int i = 0; i < Gridview.Rows.Count; i++)
    //    {
    //        GridViewRow row = Gridview.Rows[i];

    //        //Change Color back to white
    //        row.BackColor = System.Drawing.Color.White;

    //        //Apply text style to each Row
    //        row.Attributes.Add("class", "textmode");

    //        //Apply style to Individual Cells of Alternating Row
    //        if (i % 2 != 0)
    //        {
    //            for (int j = 0; j < Gridview.Rows[i].Cells.Count; j++)
    //            {
    //                //row.Cells[j].Style.Add("background-color", "#C2D69B");
    //                row.Cells[j].Style.Add("background-color", "#FFFFFF");

    //            }
    //        }
    //    }
    //}    
}
