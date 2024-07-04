using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using Deffinity.ProgrammeManagers;
using ProjectMgt.Entity;
using UserMgt.DAL;
using InventoryMgt.DAL;
using InventoryMgt.Entity;
using System.Globalization;
using Finance.DAL;
using System.Data.SqlClient;
using System.Data;
using TimesheetMgt.DAL;
using Finance.Entity;
using System.Web.Services;

public partial class Budget_General : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "Project Management";
            bindlable();
            using (projectTaskDataContext pdc = new projectTaskDataContext())
            {
                if (pdc.Projects.Where(a => a.ProjectReference == QueryStringValues.Project).Select(a => a.ProjectStatusID).FirstOrDefault() == 2)
                {
                    ChangeControlStatus();
                }
            }
        }
    }
    public void ControlDisable(ControlCollection ptext, bool status)
    {
        try
        {
            foreach (Control ctrl in ptext)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Enabled = status;
                }
                else if (ctrl is Button)
                {
                    ((Button)ctrl).Enabled = status;
                }
                else if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Enabled = status;
                }
                else if (ctrl is CheckBoxList)
                {
                    ((CheckBoxList)ctrl).Enabled = status;
                }
                else if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).Enabled = status;
                }
                else if (ctrl is RadioButtonList)
                {
                    ((RadioButtonList)ctrl).Enabled = status;
                }
                else if (ctrl is Image)
                {
                    ((Image)ctrl).Enabled = status;
                }
                //else if (ctrl is Calendar)
                //{
                //    ((Calendar)ctrl).Enabled = status;
                //}
                else if (ctrl is ImageButton)
                {
                    ((ImageButton)ctrl).Enabled = status;
                }
                else if (ctrl is DropDownList)
                {
                    ((DropDownList)ctrl).Enabled = status;
                }
                else if (ctrl is HyperLink)
                {
                    ((HyperLink)ctrl).Enabled = status;
                }
                else if (ctrl is Label)
                {
                    ((Label)ctrl).Enabled = status;
                }
                else if (ctrl is GridView)
                {
                    ((GridView)ctrl).Enabled = status;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void ChangeControlStatus()
    {
        var cl = this.Form.Controls;
        var M1Content = this.Form.FindControl("MainContent") as ContentPlaceHolder;
        if (M1Content != null)
        {
            var M2Content = M1Content.FindControl("MainContent") as ContentPlaceHolder;
            var M3Content = M2Content.FindControl("ProjectCost1") as UserControl;
            if (M3Content != null)
            {
                var Ccntrl = M3Content.Controls;
                ControlDisable(Ccntrl, false);
            }
        }
    }
    public string InHouseHours()
    {
        string value = string.Empty;
        try
        {
            int ProjectId = QueryStringValues.Project;
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_InHouseHours";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", ProjectId);
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                value = dr["Hours"].ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return value;
    }
    public decimal InHouseHoursMints(int ResourceId, int ContractorID)
    {
        decimal value = 0;
        try
        {
            int ProjectId = QueryStringValues.Project;
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_InHouseHoursMints";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", ProjectId);
            sqlcmd.Parameters.AddWithValue("@ResourceId", ResourceId);
            sqlcmd.Parameters.AddWithValue("@ContractorID", ContractorID);
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                value = Convert.ToDecimal(dr["Cost"]);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return value;
    }
    public decimal InHouseHoursCost()
    {
        decimal FinalCost = 0;
        try
        {
            using (FinanceModuleDataContext fdc = new FinanceModuleDataContext())
            {
                using (TimeSheetDataContext tdc = new TimeSheetDataContext())
                {
                    var PmHoursList = fdc.ProjectPMHours.Where(a => a.ProjectReference == QueryStringValues.Project).ToList();
                    var ResourceIds = PmHoursList.Select(a => a.ResourceID).Distinct().ToList();
                    foreach (int id in ResourceIds)
                    {
                        int Contractorid = Convert.ToInt32(fdc.AssignedContractorsToProjects.Where(a => a.ID == id).Select(a => a.ContractorID).FirstOrDefault());
                        FinalCost = FinalCost + InHouseHoursMints(id, Contractorid);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return FinalCost;
    }
    public void bindlable()
    {
        try {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                var labourList = (from r in db.ProjectBOMDetils
                                  join b in db.BOM_Types on r.WorkSheetID equals b.ID
                                  where r.ProjectReference == QueryStringValues.Project && r.ID != -99 && (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false
                                  select new
                                  {
                                      ID = r.ID,
                                      r.Worksheet,
                                      r.WorkSheetID,
                                      r.Labour,
                                      r.Material,
                                      r.Mics,
                                      r.Qty,
                                      r.Description,
                                      r.ExpectedShipmentDate,
                                      TotalMaterial = r.Qty * r.Material,
                                      TotalMics = r.Qty * r.Mics,
                                      TotalLabour = r.Qty * r.Labour, // QTY * Price
                                      NumberComplete = (r.QtyReceived == null ? 0 : r.QtyReceived),
                                      SpentToDate = (r.Labour * (r.QtyReceived == null ? 0 : r.QtyReceived)), // Price * NumberComplete
                                      TotalBudgetRemaining = ((r.Qty * r.Labour) - (r.Labour * (r.QtyReceived == null ? 0 : r.QtyReceived))) // Total - SpentToDate
                                  }).ToList();
                string Cname = GetCurrencyName();
                CultureInfo info = new CultureInfo(Cname);


                double StMisCellValue = labourList.Where(o => o.Mics != 0).Select(m => m.TotalMics).Sum();
                double StBillOfLabourValue = labourList.Where(o => o.Labour != 0).Select(l => l.TotalLabour).Sum();
                double StBillofMaterialValue = labourList.Where(o => o.Material != 0).Select(m => m.TotalMaterial).Sum();
                double StlblHouseCostValue = Convert.ToDouble(InHouseHoursCost());
                double ExpessesValue = BindExpenses();



                lblMisCellValue.Text = StMisCellValue.ToString("C", info); //labourList.Where(o => o.Mics != 0).Select(m => m.TotalMics).Sum().ToString("C", info);
                LblBillOfLabourValue.Text = StBillOfLabourValue.ToString("C", info); //labourList.Where(o => o.Labour != 0).Select(l => l.TotalLabour).Sum().ToString("C", info);
                lblBillofMaterialValue.Text = StBillofMaterialValue.ToString("C", info); //labourList.Where(o => o.Material != 0).Select(m => m.TotalMaterial).Sum().ToString("C", info);

                lblHouseHoursValue.Text = InHouseHours().ToString().Replace('.', ':');
                lblHouseCostValue.Text = InHouseHoursCost().ToString("C", info);

                lblExpensesValue.Text = BindExpenses().ToString("C", info);

                LblTotalbudgetCost.Text = (StMisCellValue + StBillOfLabourValue + StBillofMaterialValue + StlblHouseCostValue + BindExpenses()).ToString("C", info);
                


            //    string s11 = lblBillofMaterialValue.Text.Substring(1).Replace(',',' ').ToString();

              //  int s1 = int.Parse(s11);


                //LblTotalbudgetCost.Text = (Convert.ToInt32()
                //                          + Convert.ToInt32(LblBillOfLabourValue.Text.Substring(1))
                //                          + Convert.ToInt32(lblMisCellValue.Text.Substring(1))).ToString("C", info);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public double BindExpenses()
    {
        double ExpensesValue = 0;
        try
        {
            using (FinanceModuleDataContext db = new FinanceModuleDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var contractorsList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                    contractorsList.Add(new UserMgt.Entity.Contractor { ID = 0, ContractorName = "" });
                    var expenseList = db.ExternalExpenses.Where(e => e.ProjectReference == QueryStringValues.Project).ToList();
                    var expenseEntryTypeList = db.Expensesentrytypes.ToList();
                    expenseEntryTypeList.Add(new Expensesentrytype { ID = 0, ExpensesentryType1 = "" });
                    var result = (from e in expenseList
                                  join t in expenseEntryTypeList on e.ExpensesentrytypeID equals t.ID
                                  join c in contractorsList on e.AssignedTo.HasValue ? e.AssignedTo : 0 equals c.ID
                                  select new
                                  {
                                      Expensed = e.Expensed.HasValue ? e.Expensed : false,
                                      Total = (e.Qty.HasValue ? e.Qty : 0) * (e.ForecastValue.HasValue ? e.ForecastValue : 0), // Qty * UnitCost
                                      AssignedToName = c.ContractorName
                                  }).ToList();
                    //ExpensesValue = result.Where(r => r.Expensed == true).Select(r => r.Total).Sum().Value;
                    ExpensesValue = result.Select(r => r.Total).Sum().Value;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return ExpensesValue;
    }
    public string GetCurrencyName()
    {
        string Value = string.Empty;
        Value = "en-GB";//British Pound
        //try {
        //    using (projectTaskDataContext db = new projectTaskDataContext())
        //    {
        //        var plist = db.Projects.Where(o => o.ProjectReference == QueryStringValues.Project).FirstOrDefault();
        //        if (plist.BaseCurrency != null)
        //        {
        //            var cName = db.CurrencyLists.Where(o => o.ID == plist.BaseCurrency).FirstOrDefault();
        //            Value = cName.En_Name.ToString();
        //        }
        //        else
        //        {
        //            Value = "en-GB";//British Pound
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    LogExceptions.WriteExceptionLog(ex);
        //}
        return Value;
    }
    [WebMethod]
    public static List<GraphDataClsForPieChart> GetPieChartData(string pid)
    {
        double Labour = 0;
        double Materials = 0;
        double Miscellaneous = 0;
        double Expenses = 0;
        List<GraphDataClsForPieChart> DataClslist = new List<GraphDataClsForPieChart>();
        GraphDataClsForPieChart DataCls = null;
        try
        {
            string Names = "Labour,Materials,Miscellaneous,Expenses,Hours cost";
            string[] NamesArray = Names.Split(',');
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                var labourList = (from r in db.ProjectBOMDetils
                                  join b in db.BOM_Types on r.WorkSheetID equals b.ID
                                  where r.ProjectReference == int.Parse(pid) && r.ID != -99 && (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false
                                  select new
                                  {
                                      ID = r.ID,
                                      r.Worksheet,
                                      r.WorkSheetID,
                                      r.Labour,
                                      r.Material,
                                      r.Mics,
                                      r.Qty,
                                      r.Description,
                                      r.ExpectedShipmentDate,
                                      TotalMaterial = r.Qty * r.Material,
                                      TotalMics = r.Qty * r.Mics,
                                      TotalLabour = r.Qty * r.Labour, // QTY * Price
                                      NumberComplete = (r.QtyReceived == null ? 0 : r.QtyReceived),
                                      SpentToDate = (r.Labour * (r.QtyReceived == null ? 0 : r.QtyReceived)), // Price * NumberComplete
                                      TotalBudgetRemaining = ((r.Qty * r.Labour) - (r.Labour * (r.QtyReceived == null ? 0 : r.QtyReceived))) // Total - SpentToDate
                                  }).ToList();
                Miscellaneous = labourList.Where(o => o.Mics != 0).Select(m => m.TotalMics).Sum();
                Labour = labourList.Where(o => o.Labour != 0).Select(l => l.TotalLabour).Sum();
                Materials = labourList.Where(o => o.Material != 0).Select(m => m.TotalMaterial).Sum();
            }
            using (FinanceModuleDataContext db = new FinanceModuleDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var contractorsList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                    contractorsList.Add(new UserMgt.Entity.Contractor { ID = 0, ContractorName = "" });
                    var expenseList = db.ExternalExpenses.Where(e => e.ProjectReference == int.Parse(pid)).ToList();
                    var expenseEntryTypeList = db.Expensesentrytypes.ToList();
                    expenseEntryTypeList.Add(new Expensesentrytype { ID = 0, ExpensesentryType1 = "" });
                    var result = (from e in expenseList
                                  join t in expenseEntryTypeList on e.ExpensesentrytypeID equals t.ID
                                  join c in contractorsList on e.AssignedTo.HasValue ? e.AssignedTo : 0 equals c.ID
                                  select new
                                  {
                                      Expensed = e.Expensed.HasValue ? e.Expensed : false,
                                      Total = (e.Qty.HasValue ? e.Qty : 0) * (e.ForecastValue.HasValue ? e.ForecastValue : 0), // Qty * UnitCost
                                      AssignedToName = c.ContractorName
                                  }).ToList();
                    //Expenses = result.Where(r => r.Expensed == true).Select(r => r.Total).Sum().Value;
                    Expenses = result.Select(r => r.Total).Sum().Value;
                }
            }
            double FinalCost = 0;
            using (FinanceModuleDataContext fdc = new FinanceModuleDataContext())
            {
                using (TimeSheetDataContext tdc = new TimeSheetDataContext())
                {
                    var PmHoursList = fdc.ProjectPMHours.Where(a => a.ProjectReference == int.Parse(pid)).ToList();
                    var ResourceIds = PmHoursList.Select(a => a.ResourceID).Distinct().ToList();
                    foreach (int id in ResourceIds)
                    {
                        int Contractorid = Convert.ToInt32(fdc.AssignedContractorsToProjects.Where(a => a.ID == id).Select(a => a.ContractorID).FirstOrDefault());

                        int ProjectId = int.Parse(pid);
                        SqlConnection sqlcon = new SqlConnection(Constants.DBString);
                        string cmd = "Project_InHouseHoursMints";
                        DataTable dt = new DataTable();
                        SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@ProjectRef", ProjectId);
                        sqlcmd.Parameters.AddWithValue("@ResourceId", id);
                        sqlcmd.Parameters.AddWithValue("@ContractorID", Contractorid);
                        SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
                        myadapter.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            FinalCost = FinalCost + Convert.ToDouble(dr["Cost"]);
                        }
                    }
                }
            }

            for (int i = 0; i < NamesArray.Length; i++)
            {
                DataCls = new GraphDataClsForPieChart();
                DataCls.Name = NamesArray[i].ToString();
                if (i == 0)
                {
                    DataCls.Value = Convert.ToDecimal(Labour);
                }
                else if (i == 1)
                {
                    DataCls.Value =Convert.ToDecimal(Materials);
                }
                else if (i == 2)
                {
                    DataCls.Value = Convert.ToDecimal(Miscellaneous);
                }
                else if (i == 3)
                {
                    DataCls.Value = Convert.ToDecimal(Expenses);
                }
                else if (i == 4)
                {
                    DataCls.Value = Convert.ToDecimal(string.Format("{0:f2}",FinalCost));
                }
                DataClslist.Add(DataCls);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return DataClslist;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Projects/ProjectPipeline.aspx?Status=2");
    }
}
public class GraphDataClsForPieChart
{
    public string Name { get; set; }
    public decimal Value { get; set; }
   // public string ColorName { get; set; }
}