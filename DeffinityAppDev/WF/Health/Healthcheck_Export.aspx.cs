using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Health.Entity;
using Health.DAL;
using Health.StateManager;
//using Infragistics.UltraGauge.Resources;
using System.Data.OleDb;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;

public partial class Healthcheck_Export : System.Web.UI.Page
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";

       // Master.PageHead = "Export of Health checks";
        if (!Page.IsPostBack)
        {
            try
            {
                HealthCheckListState.ClearHealthCheckItemsCache();
                lblHead.InnerText = "List of Health checks";

                BindPortfolio();


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
    private void BindPortfolio()
    {
        ddlPortfolio.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        ddlPortfolio.DataTextField = "PortFolio";
        ddlPortfolio.DataValueField = "ID";
        ddlPortfolio.DataBind();
        ddlPortfolio.Items.RemoveAt(0);
        ddlPortfolio.Items.Insert(0, new ListItem("Please select...", "0"));
        if (ddlPortfolio.Items.Count > 0)
        {
            ddlPortfolio.SelectedValue = sessionKeys.PortfolioID.ToString();
        }
    }
    protected void btn_Searchprojects_Click(object sender, EventArgs e)
    {
        HealthCheckListState.ClearHealthCheckItemsCache();
        try
        {
            List<HealthCheckList> hcol = null;

            if (int.Parse(ddlPortfolio.SelectedValue) > 0)
                hcol = HealthCheckListHelper.LoadAllHealthCheckLists(int.Parse(ddlPortfolio.SelectedValue));
            else
                hcol = HealthCheckListHelper.LoadAllHealthCheckLists();
            if (!string.IsNullOrEmpty(txt_startDate.Text.Trim()) && !string.IsNullOrEmpty(txt_EndDate.Text.Trim()))
            {
                var grid_bind = (from d in hcol
                                 where d.DateRaised >= DateTime.Parse(txt_startDate.Text) && d.DateRaised <= DateTime.Parse(txt_EndDate.Text)
                                 select new { d.DateRaised, d.LocationName, d.HealthCheckTitle, d.PortfolioName, d.RAG, d.Status, d.AssignedTeamName }).ToList();
                Gridview1.DataSource = grid_bind;
                Gridview1.DataBind();
            }
            else
            {
                var grid_bind = (from d in hcol
                                 select new { d.DateRaised, d.LocationName, d.HealthCheckTitle, d.PortfolioName, d.RAG, d.Status, d.AssignedTeamName }).ToList();
                Gridview1.DataSource = grid_bind;
                Gridview1.DataBind();
            }
            if (Gridview1.Rows.Count > 0)
            {
                btnExportToExcel.Visible = true;
            }
            else
            {
                btnExportToExcel.Visible = false;
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }

    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        ddlPortfolio.SelectedIndex = 0;
        txt_EndDate.Text = string.Empty;
        txt_startDate.Text = string.Empty;
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
    //     "attachment;filename=ExportofHealthchecksdata.xls");
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);

    //    PrepareForExport(GridView1);


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
    //    TableRow tr2 = new TableRow();
    //    tr2.Cells.Add(cell2);
    //    //TableRow tr3 = new TableRow();
    //    //tr3.Cells.Add(cell3);
    //    tb.Rows.Add(tr1);
    //    tb.Rows.Add(tr2);
    //    //tb.Rows.Add(tr3);
    //    //}
    //    tb.RenderControl(hw);

    //    //style to format numbers to string
    //    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
    //    Response.Write(style);
    //    Response.Output.Write(sw.ToString());
    //    Response.Flush();
    ////    Response.End();
    //}
    private void BindToExcel()
    {
        try
        {
            List<HealthCheckList> hcol = null;

            if (int.Parse(ddlPortfolio.SelectedValue) > 0)
                hcol = HealthCheckListHelper.LoadAllHealthCheckLists(int.Parse(ddlPortfolio.SelectedValue));
            else
                hcol = HealthCheckListHelper.LoadAllHealthCheckLists();
            var grid_bind = (from d in hcol
                             where d.DateRaised >= DateTime.Parse(txt_startDate.Text) && d.DateRaised <= DateTime.Parse(txt_EndDate.Text)
                             select d).ToList();


            string filename =  Server.MapPath("~/WF/UploadData/Temp") + "\\healthchecks" + string.Format("{0:ddMMyyyymmss}", DateTime.Now) + ".xlsx";
            string sheetName = "ExportofHealthChecksdata";
            ExportToExcel(grid_bind, filename, sheetName);

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
            if (fileInfo.Exists)
            {
                Response.Clear();

                //HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;attachment; filename=\"" + fileInfo.Name + "\"");
                //HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                //HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                //HttpContext.Current.Response.TransmitFile(fileInfo.FullName);
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();

                HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=ExportofHealthChecksdata.xlsx");
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
    public static int ExportToExcel(List<HealthCheckList> dt, string excelFile, string sheetName)
    {
        try
        {
            // Create the connection string
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                excelFile + ";Extended Properties=Excel 12.0 Xml;";

            string strField = "[Date Raised],[Location],[Customer], [Title],[Assigned Team],[RAG], [Status]";

            string strValue = string.Empty;
            using (OleDbConnection con = new OleDbConnection(connString))
            {
                con.Open();

                var sqlCmd = "CREATE TABLE [" + sheetName + "] (" + strField.ToString().Replace("]", "] text") + ")";
                OleDbCommand cmd = new OleDbCommand(sqlCmd, con);
                cmd.ExecuteNonQuery();

                foreach (HealthCheckList hc in dt)
                {

                    strValue = "'" + string.Format(Deffinity.systemdefaults.GetStringDateformat(), hc.DateRaised) + "'," + "'" + hc.LocationName + "'," + "'" + hc.PortfolioName + "'," + "'" + hc.HealthCheckTitle + "'," + "'" + hc.AssignedTeamName + "'," + "'" + hc.RAG + "'," + "'" + hc.Status + "'";

                    cmd.CommandText = "INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" +
                            strValue.ToString() + ")";
                    cmd.ExecuteNonQuery();

                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


        return 1;
    }
    
   

    //protected void PrepareForExport(GridView Gridview)
    //{
    //    //Gridview.AllowPaging = Convert.ToBoolean(rbPaging.SelectedItem.Value);
    //    //  Gridview.DataBind();

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
