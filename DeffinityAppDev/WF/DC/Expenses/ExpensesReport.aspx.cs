using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.Expenses
{
    public partial class ExpensesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindUsers();
                BindGrid();
               
            }
        }
        private void BindUsers()
        {
            try
            {
                var jlist = (from p in UserMgt.BAL.ContractorsBAL.Contractor_SelectAdmins()
                             orderby p.ContractorName
                             select new { ID = p.ID, Text = p.ContractorName }).ToList();
                ddlUsers.DataSource = jlist;
                ddlUsers.DataTextField = "Text";
                ddlUsers.DataValueField = "ID";
                ddlUsers.DataBind();
                ddlUsers.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindGrid()
        {
            try
            {
                List<TimesheetMgt.Entity.v_TimeExpense> vlist = new List<TimesheetMgt.Entity.v_TimeExpense>();
                if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()) && (ddlUsers.SelectedValue != "0"))
                {
                    vlist = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.TimeExpensesDate >= Deffinity.Utility.StartOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim())) && o.TimeExpensesDate <= Deffinity.Utility.EndOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim()))).Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue)).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.TimeExpensesDate).ToList();
                }
                else if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()))
                    vlist = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.TimeExpensesDate >= Deffinity.Utility.StartOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim())) && o.TimeExpensesDate <= Deffinity.Utility.EndOfDay(Convert.ToDateTime(txtweekcommencedate.Text.Trim()))).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.TimeExpensesDate).ToList();
                else if (ddlUsers.SelectedValue != "0")
                    vlist = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue)).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.TimeExpensesDate).ToList();
                else
                    vlist = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.TimeExpensesDate).ToList();
                GridPartner.DataSource = vlist;
                GridPartner.DataBind();


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btn_viewdate_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btn_ExportExcelExpances_Click(object sender, EventArgs e)
        {
            try
            {

                List<TimesheetMgt.Entity.v_TimeExpense> vlist = new List<TimesheetMgt.Entity.v_TimeExpense>();
                if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()) && (ddlUsers.SelectedValue != "0"))
                {
                    vlist = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.TimeExpensesDate >= Convert.ToDateTime(txtweekcommencedate.Text.Trim())).Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue)).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.TimeExpensesDate).ToList();
                }
                else if (!String.IsNullOrEmpty(txtweekcommencedate.Text.Trim()))
                    vlist = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.TimeExpensesDate >= Convert.ToDateTime(txtweekcommencedate.Text.Trim())).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.TimeExpensesDate).ToList();
                else if (ddlUsers.SelectedValue != "0")
                    vlist = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).Where(o => o.ContractorID == Convert.ToInt32(ddlUsers.SelectedValue)).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.TimeExpensesDate).ToList();
                else
                    vlist = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectAll().Where(o => o.PorfolioID == sessionKeys.PortfolioID).OrderByDescending(o => o.ContractorName).OrderByDescending(o => o.TimeExpensesDate).ToList();
                GridPartner.DataSource = vlist;

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Expenses Report");



                // Title
                ws.Cell("A1").Value = "Expenses Report"; //+ string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                ws.Cell("A2").Value = "Items";
                //ws.Cell("B2").Value = "Comapany";
                ws.Cell("B2").Value = "Date";
                ws.Cell("C2").Value = "Item";
                ws.Cell("D2").Value = "Detailes";
                ws.Cell("E2").Value = "Total";
                ws.Cell("F2").Value = "Reimburse To";
                ws.Cell("G2").Value = "Job";
                ws.Cell("H2").Value = "Accounting Code";
                ws.Cell("I2").Value = "	Status";
            
                int i = 3;
                foreach (var item in vlist)
                {
                    ws.Cell("A" + i.ToString()).Value = "" + item.Details;
                    //ws.Cell("B" + i.ToString()).Value = item.ComapanyName;
                    ws.Cell("B" + i.ToString()).Value = item.TimeExpensesDate;
                    ws.Cell("C" + i.ToString()).Value = item.Item;
                    ws.Cell("D" + i.ToString()).Value = item.Details;
                    ws.Cell("B" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetDateformat();
                    ws.Cell("E" + i.ToString()).Value = item.amount;
                    // ws.Cell("E" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetStringTimeformat();
                    ws.Cell("F" + i.ToString()).Value = item.ReimburseToName;
                    //ws.Cell("F" + i.ToString()).Style.DateFormat.Format = Deffinity.systemdefaults.GetTimeformat();
                    ws.Cell("G" + i.ToString()).Value = item.ProjectName;
                    ws.Cell("H" + i.ToString()).Value = item.AccountingCodesName;
                    //ws.Cell("I" + i.ToString()).Value = item.;


                    i = i + 1;
                }

                // From worksheet
                var rngTable = ws.Range("A1:I2");

                var rngHeaders = rngTable.Range("A2:I2");
                rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngHeaders.Style.Font.Bold = true;
                rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;

                rngTable.Cell(1, 1).Style.Font.Bold = true;
                rngTable.Cell(1, 1).Style.Font.FontColor = XLColor.White;
                rngTable.Cell(1, 1).Style.Font.FontSize = 15;
                rngTable.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.DarkGray;
                rngTable.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                rngTable.Row(1).Merge();

                ws.Columns(1, 9).AdjustToContents();

                string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/SAMReports");

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                wb.SaveAs(path + "\\" + "ExpensesReport.xlsx");
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + "ExpensesReport.xlsx");
                if (fileInfo.Exists)
                {
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                    System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=ExpensesReport.xlsx");
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}