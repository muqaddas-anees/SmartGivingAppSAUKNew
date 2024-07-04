using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using UserMgt.DAL;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;
using Finance.DAL;
using InventoryMgt.Entity;
using InventoryMgt.DAL;
using TimesheetMgt.DAL;
using Finance.Entity;

public partial class controls_SavingGrids : System.Web.UI.UserControl
{

    Database db = DatabaseFactory.CreateDatabase("DBstring");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridBind();
            LablesBinding();
            PmHoursGridBinding();
            StaffHoursGridBinding();
            VariationGridBind();
        }
    }

    public void VariationGridBind()
    {
        try
        {
            double ApprovedCost = 0;
            double UnApprovedCost = 0;
            double ApprovedValue = 0;
            double UnApprovedValue = 0;
            VariationDataCls vdatacla = new VariationDataCls();
            List<VariationDataCls> vdataclaList = new List<VariationDataCls>();
            using (FinanceModuleDataContext Fdc = new FinanceModuleDataContext())
            {
                var Dreport = Fdc.DeviationReports.Where(a => a.ProjectReference == QueryStringValues.Project).Select(a => a).ToList();
                if (Dreport.Where(a => a.Approved == true).Count() != 0)
                {
                    ApprovedCost = (double)Dreport.Where(a => a.Approved == true).Select(a => a.VariationCostForcast).Sum();
                    ApprovedValue = (double)Dreport.Where(a => a.Approved == true).Select(a => a.VariationForcast).Sum();
                }
                if (Dreport.Where(a => a.Approved == false).Count() != 0)
                {
                    UnApprovedCost = (double)Dreport.Where(a => a.Approved == false).Select(a => a.VariationCostForcast).Sum();
                    UnApprovedValue = (double)Dreport.Where(a => a.Approved == false).Select(a => a.VariationForcast).Sum();
                }
                vdatacla.UnApprovedCost = UnApprovedCost;
                vdatacla.UnApprovedValue = UnApprovedValue;
                vdatacla.ApprovedCost = ApprovedCost;
                vdatacla.ApprovedValue = ApprovedValue;
                vdataclaList.Add(vdatacla);
                if (vdatacla.ApprovedCost != 0 || vdatacla.ApprovedValue != 0 || vdatacla.UnApprovedCost != 0 || vdatacla.UnApprovedValue != 0)
                {
                    VariationsGrid.DataSource = vdataclaList;
                }
                VariationsGrid.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void PmHoursGridBinding()
    {
        try
        {
            decimal BudgetValue = 0;
            decimal ActualValue = 0;
            List<PmhoursClass> pmclsList = new List<PmhoursClass>();
            PmhoursClass pmcls = new PmhoursClass();
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_GraphBudgetedhours";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", QueryStringValues.Project);
            sqlcmd.Parameters.AddWithValue("@Hours", "PM");
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                BudgetValue = Convert.ToDecimal(dr["Hours"]);
                pmcls.BudgetValue = dr["Hours"].ToString().Replace('.', ':');
            }
            SqlConnection sqlcon1 = new SqlConnection(Constants.DBString);
            string cmd1 = "Project_GraphActualhours";
            DataTable dt1 = new DataTable();
            SqlCommand sqlcmd1 = new SqlCommand(cmd1, sqlcon1);
            sqlcmd1.CommandType = CommandType.StoredProcedure;
            sqlcmd1.Parameters.AddWithValue("@ProjectRef", QueryStringValues.Project);
            sqlcmd1.Parameters.AddWithValue("@Hours", "PM");
            SqlDataAdapter myadapter1 = new SqlDataAdapter(sqlcmd1);
            myadapter1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                ActualValue = Convert.ToDecimal(dr1["Hours"]);
                pmcls.ActualValue = dr1["Hours"].ToString().Replace('.', ':');
            }
            pmcls.Differ = string.Format("{0:f2}", pmHoursSaving()).Replace('.', ':'); //(BudgetValue - ActualValue).ToString().Replace('.', ':').Trim('-');
            pmcls.project = "WRX" + QueryStringValues.Project.ToString();
            pmclsList.Add(pmcls);
            if (pmcls.ActualValue != "0:00" || pmcls.BudgetValue != "0:00")
            {
                if (pmcls.ActualValue != "0:00")
                {
                    PMHoursGrid.DataSource = pmclsList;
                }
            }
            PMHoursGrid.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void StaffHoursGridBinding()
    {
        try
        {
            decimal BudgetValue = 0;
            decimal ActualValue = 0;
            List<PmhoursClass> pmclsList = new List<PmhoursClass>();
            PmhoursClass pmcls = new PmhoursClass();
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_GraphBudgetedhours";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", QueryStringValues.Project);
            sqlcmd.Parameters.AddWithValue("@Hours", "Staff");
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                BudgetValue = Convert.ToDecimal(dr["Hours"]);
                pmcls.BudgetValue = dr["Hours"].ToString().Replace('.', ':');
            }
            SqlConnection sqlcon1 = new SqlConnection(Constants.DBString);
            string cmd1 = "Project_GraphActualhours";
            DataTable dt1 = new DataTable();
            SqlCommand sqlcmd1 = new SqlCommand(cmd1, sqlcon1);
            sqlcmd1.CommandType = CommandType.StoredProcedure;
            sqlcmd1.Parameters.AddWithValue("@ProjectRef", QueryStringValues.Project);
            sqlcmd1.Parameters.AddWithValue("@Hours", "Staff");
            SqlDataAdapter myadapter1 = new SqlDataAdapter(sqlcmd1);
            myadapter1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                ActualValue = Convert.ToDecimal(dr1["Hours"]);
                pmcls.ActualValue = dr1["Hours"].ToString().Replace('.', ':');
            }
            pmcls.Differ = string.Format("{0:f2}", StaffHoursSaving()).Replace('.', ':'); //(BudgetValue - ActualValue).ToString().Replace('.', ':').Trim('-');
            pmcls.project = "WRX" + QueryStringValues.Project.ToString();
            pmclsList.Add(pmcls);
            if (pmcls.ActualValue != "0:00" || pmcls.BudgetValue != "0:00")
            {
                if (pmcls.ActualValue != "0:00")
                {
                    GridStaffHours.DataSource = pmclsList;
                }
            }
            GridStaffHours.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public decimal pmHoursSaving()
    {
        //string PmHoursSaving=string
          decimal value = 0;
          decimal value1 = 0;
          try
          {
              SqlConnection sqlcon = new SqlConnection(Constants.DBString);
              string cmd = "Project_GraphBudgetedhours";
              DataTable dt = new DataTable();
              SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
              sqlcmd.CommandType = CommandType.StoredProcedure;
              sqlcmd.Parameters.AddWithValue("@ProjectRef", QueryStringValues.Project);
              sqlcmd.Parameters.AddWithValue("@Hours", "PM");
              SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
              myadapter.Fill(dt);
              foreach (DataRow dr in dt.Rows)
              {
                  value = Convert.ToDecimal(dr["Hours"].ToString());
              }


              SqlConnection sqlcon1 = new SqlConnection(Constants.DBString);
              string cmd1 = "Project_GraphActualhours";
              DataTable dt1 = new DataTable();
              SqlCommand sqlcmd1 = new SqlCommand(cmd1, sqlcon1);
              sqlcmd1.CommandType = CommandType.StoredProcedure;
              sqlcmd1.Parameters.AddWithValue("@ProjectRef", QueryStringValues.Project);
              sqlcmd1.Parameters.AddWithValue("@Hours", "PM");
              SqlDataAdapter myadapter1 = new SqlDataAdapter(sqlcmd1);
              myadapter1.Fill(dt1);
              foreach (DataRow dr1 in dt1.Rows)
              {
                  value1 = Convert.ToDecimal(dr1["Hours"].ToString());
              }
              if (value1 != 0)
              {
                  using (TimeSheetDataContext Tdc = new TimeSheetDataContext())
                  {
                      value = Convert.ToDecimal(Tdc.ConvertToMins(Convert.ToDouble(value)));
                      value1 = Convert.ToDecimal(Tdc.ConvertToMins(Convert.ToDouble(value1)));
                      value = value - value1;
                      value = (decimal)Tdc.ConvertToHours(value);
                  }
              }
              else
              {
                  value = 0;
              }
              return value;
          }
          catch (Exception ex)
          {
              LogExceptions.WriteExceptionLog(ex);
              return value;
          }
    }
    public decimal StaffHoursSaving()
    {
        //string PmHoursSaving=string
        decimal value = 0;
        decimal value1 = 0;
        try
        {
            SqlConnection sqlcon = new SqlConnection(Constants.DBString);
            string cmd = "Project_GraphBudgetedhours";
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ProjectRef", QueryStringValues.Project);
            sqlcmd.Parameters.AddWithValue("@Hours", "Staff");
            SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
            myadapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                value = Convert.ToDecimal(dr["Hours"].ToString());
            }


            SqlConnection sqlcon1 = new SqlConnection(Constants.DBString);
            string cmd1 = "Project_GraphActualhours";
            DataTable dt1 = new DataTable();
            SqlCommand sqlcmd1 = new SqlCommand(cmd1, sqlcon1);
            sqlcmd1.CommandType = CommandType.StoredProcedure;
            sqlcmd1.Parameters.AddWithValue("@ProjectRef", QueryStringValues.Project);
            sqlcmd1.Parameters.AddWithValue("@Hours", "Staff");
            SqlDataAdapter myadapter1 = new SqlDataAdapter(sqlcmd1);
            myadapter1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                value1 = Convert.ToDecimal(dr1["Hours"].ToString());
            }

            if (value1 != 0)
            {
                using (TimeSheetDataContext Tdc = new TimeSheetDataContext())
                {
                    value = Convert.ToDecimal(Tdc.ConvertToMins(Convert.ToDouble(value)));
                    value1 = Convert.ToDecimal(Tdc.ConvertToMins(Convert.ToDouble(value1)));
                    value = value - value1;
                    value = (decimal)Tdc.ConvertToHours(value);
                }
            }
            else
            {
                value = 0;
            }
            return value;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return value;
        }
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
    public void bindlableForecastedProjectCost()
    {
        try
        {
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
                lblForecastedProjectCost.Text = string.Format("{0:f2}", (StMisCellValue + StBillOfLabourValue + StBillofMaterialValue + StlblHouseCostValue + BindExpenses()));



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
    public void BindExpenselbl()
    {
        try
        {
            double actualValue = 0;
            using (FinanceModuleDataContext fdc = new FinanceModuleDataContext())
            {
                using (projectTaskDataContext Pdc = new projectTaskDataContext())
                {
                    var expenseReport = fdc.ExternalExpenses.Where(a => a.ProjectReference == QueryStringValues.Project).ToList();
                    var SavingRecords = Pdc.GoodsReceiptSavings.Where(a => a.projectRef == QueryStringValues.Project).ToList();

                    if (expenseReport.Count() != 0)
                    {
                        foreach (var x in expenseReport)
                        {
                            var SavingRecord = SavingRecords.Where(a => a.BOMId == x.ID && a.S_type == "Expenses").FirstOrDefault();
                            if (SavingRecord != null)
                            {
                                var ActualQty = SavingRecord.ActualQty;
                                actualValue = actualValue + (double)((x.Qty) * (x.UnitCost - SavingRecord.UnitCostSaving));
                            }
                            else
                            {
                                actualValue = actualValue + 0;
                            }
                        }
                    }
                    lblExpenseSavings.Text = string.Format("{0:f2}", actualValue);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void LablesBinding()
    {
        try
        {
            decimal TotalMaterial = 0;
            decimal TotalLabour = 0;
            decimal TotalMics = 0;
            int Differ = 0;
            decimal materialvalue = 0;
            decimal labourvalue = 0;
            decimal micsvalue = 0;
            decimal TotalExpenses = 0;

            using (projectTaskDataContext pdc = new projectTaskDataContext())
            {
                var plistBOmids = pdc.GoodsReceiptSavings.Where(a => a.projectRef == QueryStringValues.Project && a.S_type == "Qty").Select(a => a.BOMId).Distinct().ToList();
                foreach (int x in plistBOmids)
                {
                    var SavingPList = pdc.GoodsReceiptSavings.Where(a => a.projectRef == QueryStringValues.Project && a.BOMId == x && a.S_type == "Qty").OrderByDescending(a => a.id).FirstOrDefault();
                    if (SavingPList != null)
                    {
                        Differ = (int)(SavingPList.BudgetQty - SavingPList.ActualQty);
                    }
                    var BOMRecord = pdc.ProjectBOMs.Where(a => a.ProjectReference == QueryStringValues.Project && a.ID == x).FirstOrDefault();
                    if (BOMRecord != null)
                    {
                        materialvalue = (decimal)((BOMRecord.Material.HasValue) ? BOMRecord.Material : 0);
                        labourvalue = (decimal)((BOMRecord.Labour.HasValue) ? BOMRecord.Labour : 0);
                        micsvalue = (decimal)((BOMRecord.Mics.HasValue) ? BOMRecord.Mics : 0);
                    }
                    TotalMaterial = TotalMaterial + (Differ * materialvalue);
                    TotalLabour = TotalLabour + (Differ * labourvalue);
                    TotalMics = TotalMics + (Differ * micsvalue);
                }
                BindExpenselbl();
                TotalExpenses = Convert.ToDecimal(lblExpenseSavings.Text);
                string Cname = GetCurrencyName();
                CultureInfo info = new CultureInfo(Cname);
                lblMaterialsSaving.Text = string.Format("{0:f2}", TotalMaterial);
                lblLabourSaving.Text = string.Format("{0:f2}", TotalLabour);
                lblMiscSavings.Text = string.Format("{0:f2}", TotalMics);
                lblSavingstoDate.Text = string.Format("{0:f2}", (TotalMaterial + TotalLabour + TotalMics + TotalExpenses));
                lblPMHoursSaving.Text = string.Format("{0:f2}", pmHoursSaving()).Replace('.', ':');//Trim('-')
                lblStaffHoursSaving.Text = string.Format("{0:f2}", StaffHoursSaving()).Replace('.', ':');
                //lblForecastedProjectCost.Text = string.Format("{0:f2}",pdc.Projects.Where(a => a.ProjectReference == QueryStringValues.Project).Select(a => a.BudgetaryCost).FirstOrDefault());
                bindlableForecastedProjectCost();

                DbCommand cmd = db.GetStoredProcCommand("DN_SelectProject");
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, QueryStringValues.Project);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        if (dr["ActualCost"].ToString() == "0")
                        {
                            lblSpenttoDate.Text = "0.00";
                        }
                        else
                        {
                            //  lblSpenttoDate.Text = string.Format(info, "{0:C}", dr["ActualCost"]);
                            lblSpenttoDate.Text = string.Format("{0:f2}", dr["ActualCost"]);
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
    public string GetCurrencyName()
    {
        string Value = string.Empty;
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                var plist = db.Projects.Where(o => o.ProjectReference == QueryStringValues.Project).FirstOrDefault();
                if (plist.BaseCurrency != null)
                {
                    var cName = db.CurrencyLists.Where(o => o.ID == plist.BaseCurrency).FirstOrDefault();
                    Value = cName.En_Name.ToString();
                }
                else
                {
                    Value = "en-GB";//British Pound
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return Value;
    }
    public void GridBind()
    {
        try
        {
            using (projectTaskDataContext pdc = new projectTaskDataContext())
            {
                using (UserDataContext udc = new UserDataContext())
                {
                    var SavingPList = pdc.GoodsReceiptSavings.Where(a => a.projectRef == QueryStringValues.Project && a.S_type == "Qty").ToList();
                    var plist = pdc.Projects.ToList();
                    var BOMList = pdc.ProjectBOMs.Where(a => a.ProjectReference == QueryStringValues.Project).ToList();
                    var contractorList = udc.Contractors.ToList();

                    var LabourSavingList = (from a in SavingPList
                                            join b in BOMList on a.BOMId equals b.ID
                                            where b.Labour != 0
                                            select new
                                            {
                                                ProjectName = "WRX" + a.projectRef.ToString(),
                                                BOMName = b.Description,
                                                BudgetQty = a.BudgetQty,
                                                ActualQty = a.ActualQty,
                                                Differ = a.BudgetQty - a.ActualQty,
                                                UserName = contractorList.Where(c => c.ID == a.Userid).Select(c => c.ContractorName).FirstOrDefault(),
                                                SavingCost = (a.BudgetQty - a.ActualQty) * b.Labour,
                                                Datemodified = a.DateModified.Value.ToShortDateString()
                                            }).ToList();
                    LabourSavingsGrid.DataSource = LabourSavingList;
                    LabourSavingsGrid.DataBind();
                    var MaterialSavingList = (from a in SavingPList
                                              join b in BOMList on a.BOMId equals b.ID
                                              where b.Material != 0
                                              select new
                                              {
                                                  ProjectName = "WRX" + a.projectRef.ToString(),
                                                  BOMName = b.Description,
                                                  BudgetQty = a.BudgetQty,
                                                  ActualQty = a.ActualQty,
                                                  Differ = a.BudgetQty - a.ActualQty,
                                                  UserName = contractorList.Where(c => c.ID == a.Userid).Select(c => c.ContractorName).FirstOrDefault(),
                                                  SavingCost = (a.BudgetQty - a.ActualQty) * b.Material,
                                                  BudgetedCost = (a.BudgetQty) * b.Material,
                                                  ActualCost = (a.ActualQty) * b.Material,
                                                  Datemodified = a.DateModified.Value.ToShortDateString()
                                              }).ToList();
                    MaterialsSavingGrid.DataSource = MaterialSavingList;
                    MaterialsSavingGrid.DataBind();
                    var MicsSavingList = (from a in SavingPList
                                          join b in BOMList on a.BOMId equals b.ID
                                          where b.Mics != 0
                                          select new
                                          {
                                              PSavingId = a.id,
                                              ProjectName = "WRX" + a.projectRef.ToString(),
                                              BOMName = b.Description,
                                              BudgetQty = a.BudgetQty,
                                              ActualQty = a.ActualQty,
                                              Differ = a.BudgetQty - a.ActualQty,
                                              UserName = contractorList.Where(c => c.ID == a.Userid).Select(c => c.ContractorName).FirstOrDefault(),
                                              SavingCost = (a.BudgetQty - a.ActualQty) * b.Mics,
                                              BudgetedCost = (a.BudgetQty) * b.Mics,
                                              ActualCost = (a.ActualQty) * b.Mics,
                                              Datemodified = a.DateModified.Value.ToShortDateString()
                                          }).ToList();//OrderByDescending(a => a.PSavingId)
                    MiscellaneousSavingsGrid.DataSource = MicsSavingList;
                    MiscellaneousSavingsGrid.DataBind();

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
public class VariationDataCls
{
    public double UnApprovedCost { get; set; }
    public double UnApprovedValue { get; set; }
    public double ApprovedCost { get; set; }
    public double ApprovedValue { get; set; }
}
public class PmhoursClass
{
    public string project { set; get; }
    public string BudgetValue { set; get; }
    public string ActualValue { set; get; }
    public string Differ { set; get; }
}