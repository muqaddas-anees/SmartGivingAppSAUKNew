using Finance.DAL;
using ProjectMgt.DAL;
using ProjectTracker.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetMgt.DAL;
using UserMgt.DAL;
using UserMgt.Entity;
using Finance.DAL;
using Finance.Entity;

public partial class budget_pmbudgetdifference : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = "Project Management";
        if (!IsPostBack)
        {
           // BudgetAlertMail BMail = new BudgetAlertMail();
           // BMail.MailSendingList(QueryStringValues.Project);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Projects/ProjectPipeline.aspx?Status=2");
    }
    public static string ChangeHours(string GetHours)
    {
        string GetActivity = "";
        if (GetHours != "")
        {
            char[] comm1 = { '.', ',' };
            string[] displayTime = GetHours.Split(comm1);
            GetActivity = displayTime[0] + ":" + displayTime[1];
            return GetActivity;
        }
        else
        {
            return "0:00";
        }
    }
     [WebMethod]
     public static List<GraphDataCls> OverallProjectFinancialHealthData(string pid)
    {
        List<GraphDataCls> DataClsList = new List<GraphDataCls>();
        GraphDataCls DataCls = null;
        string Names = "Project Fee,Budgeted Cost,Variations Total,Actual Cost";
        string[] NamesArray = Names.Split(',');

        double BudgetCost = 0;
        double ActualCost = 0;
        double Variation = 0;
        double totalVariation = 0;
        double ProjectFee = 0;

        using (projectTaskDataContext pdc = new projectTaskDataContext())
        {
            var x = pdc.Projects.Where(a => a.ProjectReference == int.Parse(pid)).Select(a => a).FirstOrDefault();
            ProjectFee = (double)x.BudgetaryCost;
            ActualCost = (double)x.ActualCost;
           // Variation = -(BudgetCost - ActualCost);


            using (FinanceModuleDataContext db = new FinanceModuleDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var contractorsList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                    contractorsList.Add(new UserMgt.Entity.Contractor { ID = 0, ContractorName = "" });
                    var expenseList = db.ExternalExpenses.Where(e => e.ProjectReference ==int.Parse(pid)).ToList();
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
                    BudgetCost = result.Select(r => r.Total).Sum().Value;
                }
            }
            var bomList = (from r in pdc.ProjectBOMDetils
                           where r.ProjectReference == int.Parse(pid)
                           select r).ToList();
            BudgetCost = BudgetCost + bomList.Select(a => a.Total).Sum();
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
            BudgetCost = BudgetCost + FinalCost;
        }
        totalVariation = ProjectTrackerBAL.GetVariationCostByProject(int.Parse(pid));

        try
        {
            for (int i = 0; i < NamesArray.Length; i++)
            {
                DataCls = new GraphDataCls();
                DataCls.Name = NamesArray[i].ToString();
                if (i == 0)
                {
                    DataCls.Value = Convert.ToDecimal(ProjectFee);
                    DataCls.ColorName = "color: #ff9300"; 
                }
                else if (i == 1)
                {
                    DataCls.Value = Convert.ToDecimal(string.Format("{0:f2}",BudgetCost));//totalVariation +
                    DataCls.ColorName = "color: #aa66b2";
                }
                else if (i == 2)
                {
                    DataCls.Value = Convert.ToDecimal(totalVariation);
                    DataCls.ColorName = "color: #5bba5c";
                }
                else if (i == 3)
                {
                    DataCls.Value = Convert.ToDecimal(ActualCost);
                    DataCls.ColorName = "color: #4492c5";
                }
                DataClsList.Add(DataCls);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return DataClsList;
    }
     [WebMethod]
     public static List<GraphDataCls> VariationSummaryData(string pid)
     {
         List<GraphDataCls> DataClsList = new List<GraphDataCls>();
         GraphDataCls DataCls = null;
         string Names = "Cost of Unapproved Variations,Value of Unapproved Variations,Cost of Approved Variations,Value of Approved Variations";
         string[] NamesArray = Names.Split(',');

         double ApprovedCost = 0;
         double UnApprovedCost = 0;
         double ApprovedValue = 0;
         double UnApprovedValue = 0;
         using (FinanceModuleDataContext Fdc = new FinanceModuleDataContext())
         {
             var Dreport = Fdc.DeviationReports.Where(a => a.ProjectReference == int.Parse(pid)).Select(a => a).ToList();
             if (Dreport.Where(a => a.Approved == true).Count() != 0)
             {
                 ApprovedCost =(double) Dreport.Where(a => a.Approved == true).Select(a => a.IndirectCost).Sum();
                 ApprovedValue = (double)Dreport.Where(a => a.Approved == true).Select(a => a.DeviationValue).Sum();
             }
             if (Dreport.Where(a => a.Approved == false).Count() != 0)
             {
                 UnApprovedCost = (double)Dreport.Where(a => a.Approved == false).Select(a => a.IndirectCost).Sum();
                 UnApprovedValue = (double)Dreport.Where(a => a.Approved == false).Select(a => a.DeviationValue).Sum();
             }
         }
         try
         {
             for (int i = 0; i < NamesArray.Length; i++)
             {
                 DataCls = new GraphDataCls();
                 DataCls.Name = NamesArray[i].ToString();
                 if (i == 0)
                 {
                     DataCls.Value = Convert.ToDecimal(UnApprovedCost);
                     DataCls.ColorName = "color: #ff9300";
                 }
                 else if (i == 1)
                 {
                     DataCls.Value = Convert.ToDecimal(UnApprovedValue);
                     DataCls.ColorName = "color: #aa66b2";
                 }
                 else if (i == 2)
                 {
                     DataCls.Value = Convert.ToDecimal(ApprovedCost);
                     DataCls.ColorName = "color: #5bba5c";
                 }
                 else if (i == 3)
                 {
                     DataCls.Value = Convert.ToDecimal(ApprovedValue);
                     DataCls.ColorName = "color: #4492c5";
                 }
                 DataClsList.Add(DataCls);
             }
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
         return DataClsList;
     }

     public int GetGraphBudgetedhours()
     {
         decimal value = 0;
         try
         {
             SqlConnection sqlcon = new SqlConnection(Constants.DBString);
             string cmd = "Project_GraphBudgetedhours";
             DataTable dt = new DataTable();
             SqlCommand sqlcmd = new SqlCommand(cmd, sqlcon);
             sqlcmd.CommandType = CommandType.StoredProcedure;
             sqlcmd.Parameters.AddWithValue("@ProjectRef", QueryStringValues.Project);
             SqlDataAdapter myadapter = new SqlDataAdapter(sqlcmd);
             myadapter.Fill(dt);
             foreach (DataRow dr in dt.Rows)
             {
                 value = Convert.ToDecimal(dr["Hours"]);
             }
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
         return Decimal.ToInt32(value);
     }

     [WebMethod]
     public static List<GraphDataCls> PMHoursData(string pid)
     {
         List<GraphDataCls> DataClslist = new List<GraphDataCls>();
         GraphDataCls DataCls = null;
         decimal value = 0;
         decimal value1 = 0;

         try
         {
             string Names = "Saving,Budget";
             string[] NamesArray = Names.Split(',');

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
                 value = Convert.ToDecimal(dr["Hours"].ToString());
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
                 value1 = Convert.ToDecimal(dr1["Hours"].ToString());
             }

             if (value1 != 0)
             {
                 using (TimeSheetDataContext Tdc = new TimeSheetDataContext())
                 {
                     value = Convert.ToDecimal(Tdc.ConvertToMins(Convert.ToDouble(value)));
                     value1 = Convert.ToDecimal(Tdc.ConvertToMins(Convert.ToDouble(value1)));
                     value1 = value - value1;
                     value1 = (decimal)Tdc.ConvertToHours(value1);
                     value = (decimal)Tdc.ConvertToHours(value);
                 }
             }
             else
             {
                 value1 = 0;
             }
           //  pmHourActual = Decimal.ToInt32(value1);


             for (int i = 0; i < NamesArray.Length; i++)
             {
                 DataCls = new GraphDataCls();
                 DataCls.Name = NamesArray[i].ToString();
                 if (i == 0)
                 {
                     DataCls.Value = value1;
                 }
                 else if (i == 1)
                 {
                     DataCls.Value = value;
                 }
                 if (i == 0)
                 {
                     if (value1 > 0)
                     {
                         DataCls.ColorName = "color: #aa66b2";
                     }
                     else
                     {
                         DataCls.ColorName = "color: #e23124";
                     }
                 }
                 else
                 {
                     DataCls.ColorName = "color: #5bba5c";
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
     [WebMethod]
     public static List<GraphDataCls> StaffhoursData(string pid)
     {
         List<GraphDataCls> DataClslist = new List<GraphDataCls>();
         decimal value = 0;
         decimal value1 = 0;
         try
         {
             GraphDataCls DataCls = null;
             string Names = "Saving,Budget";
             string[] NamesArray = Names.Split(',');
             int StaffActual = 0;
             int StaffBudget = 0;

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
                 value = Convert.ToDecimal(dr["Hours"]);
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
                 value1 = Convert.ToDecimal(dr["Hours"]);
             }

             if (value1 != 0)
             {
                 using (TimeSheetDataContext Tdc = new TimeSheetDataContext())
                 {
                     value = Convert.ToDecimal(Tdc.ConvertToMins(Convert.ToDouble(value)));
                     value1 = Convert.ToDecimal(Tdc.ConvertToMins(Convert.ToDouble(value1)));
                     value1 = value - value1;
                     value1 = (decimal)Tdc.ConvertToHours(value1);
                     value = (decimal)Tdc.ConvertToHours(value);
                 }
             }
             else
             {
                 value1 = 0;
             }


             for (int i = 0; i < NamesArray.Length; i++)
             {
                 DataCls = new GraphDataCls();
                 DataCls.Name = NamesArray[i].ToString();
                 if (i == 0)
                 {
                     DataCls.Value = value1;
                 }
                 else if (i == 1)
                 {
                     DataCls.Value = value;
                 }
                 if (i == 0)
                 {
                     if (value1 > 0)
                     {
                         DataCls.ColorName = "color: #aa66b2";
                     }
                     else
                     {
                         DataCls.ColorName = "color: #e23124";
                     }
                 }
                 else
                 {
                     DataCls.ColorName = "color: #5bba5c";
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
     [WebMethod]
     public static List<GraphDataCls> CostOfMiscitemsData(string pid)
     {
         List<GraphDataCls> DataClslist = new List<GraphDataCls>();
         GraphDataCls DataCls = null;
         string Names = "Saving,Budget";
         string[] NamesArray = Names.Split(',');
         double actualValue = 0;
         double BudgetValue = 0;

         using (projectTaskDataContext Pdc = new projectTaskDataContext())
         {
             var BomList = (from r in Pdc.ProjectBOMs
                            join b in Pdc.BOM_Types on r.WorkSheetID equals b.ID
                            where (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false && r.ProjectReference == int.Parse(pid) && r.ID != -99
                            select r).ToList();
             //var BomList = Pdc.ProjectBOMs.Where(a => a.ProjectReference == int.Parse(pid)).Select(a => a).ToList();
             var SavingRecords = Pdc.GoodsReceiptSavings.Where(a => a.projectRef == int.Parse(pid)).ToList();

             foreach (var x in BomList)
             {
                 if (x.Qty != null && x.Mics != null)
                 {
                     BudgetValue = BudgetValue + (double)((x.Qty) * (x.Mics));
                     if (SavingRecords.Count() != 0)
                     {
                         var SavingRecord = SavingRecords.Where(a => a.BOMId == x.ID && a.S_type == "Qty").FirstOrDefault();
                         if (SavingRecord != null)
                         {
                             var ActualQty = SavingRecord.ActualQty;
                             actualValue = actualValue + (double)((x.Qty - ActualQty) * (x.Mics));
                         }
                         else
                         {
                             //actualValue = actualValue + (double)((x.Qty) * (x.Mics));
                             actualValue = actualValue + 0;
                         }
                     }
                     else
                     {
                         //actualValue = actualValue + (double)((x.Qty) * (x.Mics));
                         actualValue = actualValue + 0;
                     }
                 }
             }
         }
         try
         {
             for (int i = 0; i < NamesArray.Length; i++)
             {
                 DataCls = new GraphDataCls();
                 DataCls.Name = NamesArray[i].ToString();
                 if (i == 0)
                 {
                     DataCls.Value = Convert.ToDecimal(actualValue);
                 }
                 else if (i == 1)
                 {
                     DataCls.Value = Convert.ToDecimal(BudgetValue);
                 }
                 if (i == 0)
                 {
                     DataCls.ColorName = "color: #aa66b2";
                     //if (actualValue > BudgetValue)
                     //{
                     //    DataCls.ColorName = "color: #e23124";
                     //}
                     //else
                     //{
                     //    DataCls.ColorName = "color: #5bba5c";
                     //}
                 }
                 else
                 {
                     DataCls.ColorName = "color: #5bba5c";
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
     [WebMethod]
     public static List<GraphDataCls> CostOfMaterialsData(string pid)
     {
         List<GraphDataCls> DataClslist = new List<GraphDataCls>();
         GraphDataCls DataCls = null;
         string Names = "Saving,Budget";
         string[] NamesArray = Names.Split(',');
         double actualValue = 0;
         double BudgetValue = 0;

         using (projectTaskDataContext Pdc = new projectTaskDataContext())
         {
             var BomList = (from r in Pdc.ProjectBOMs
                            join b in Pdc.BOM_Types on r.WorkSheetID equals b.ID
                            where (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false && r.ProjectReference == int.Parse(pid) && r.ID != -99
                            select r).ToList();
             //var BomList = Pdc.ProjectBOMs.Where(a => a.ProjectReference == int.Parse(pid)).Select(a => a).ToList();
             var SavingRecords = Pdc.GoodsReceiptSavings.Where(a => a.projectRef == int.Parse(pid)).ToList();

             foreach (var x in BomList)
             {
                 if (x.Qty != null && x.Material != null)
                 {
                     BudgetValue = BudgetValue + (double)((x.Qty) * (x.Material));
                     if (SavingRecords.Count() != 0)
                     {
                         var SavingRecord = SavingRecords.Where(a => a.BOMId == x.ID && a.S_type == "Qty").FirstOrDefault();
                         if (SavingRecord != null)
                         {
                             var ActualQty = SavingRecord.ActualQty;
                             actualValue = actualValue + (double)((x.Qty - ActualQty) * (x.Material));
                         }
                         else
                         {
                             //actualValue = actualValue + (double)((x.Qty) * (x.Material));
                             actualValue = actualValue + 0;
                         }
                     }
                     else
                     {
                         //actualValue = actualValue + (double)((x.Qty) * (x.Material));
                         actualValue = actualValue + 0;
                     }
                 }
             }
         }
         try
         {
             for (int i = 0; i < NamesArray.Length; i++)
             {
                 DataCls = new GraphDataCls();
                 DataCls.Name = NamesArray[i].ToString();
                 if (i == 0)
                 {
                     DataCls.Value = Convert.ToDecimal(actualValue);
                 }
                 else if (i == 1)
                 {
                     DataCls.Value = Convert.ToDecimal(BudgetValue);
                 }
                 if (i == 0)
                 {
                     DataCls.ColorName = "color: #aa66b2";
                     //if (actualValue > BudgetValue)
                     //{
                     //    DataCls.ColorName = "color: #e23124";
                     //}
                     //else
                     //{
                     //    DataCls.ColorName = "color: #5bba5c";
                     //}
                 }
                 else
                 {
                     DataCls.ColorName = "color: #5bba5c";
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
     [WebMethod]
     public static List<GraphDataCls> CostOfLabourData(string pid)
     {
         List<GraphDataCls> DataClslist = new List<GraphDataCls>();
         GraphDataCls DataCls = null;
         string Names = "Saving,Budget";
         string[] NamesArray = Names.Split(',');
         double actualValue = 0;
         double BudgetValue = 0;

         using (projectTaskDataContext Pdc = new projectTaskDataContext())
         {
             var BomList = (from r in Pdc.ProjectBOMs
                            join b in Pdc.BOM_Types on r.WorkSheetID equals b.ID
                               where (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false && r.ProjectReference == int.Parse(pid) && r.ID != -99
                               select r).ToList();

             //var BomList = Pdc.ProjectBOMs.Where(a => a.ProjectReference == int.Parse(pid)).Select(a => a).ToList();
             var SavingRecords = Pdc.GoodsReceiptSavings.Where(a => a.projectRef == int.Parse(pid)).ToList();

             foreach (var x in BomList)
             {
                 if (x.Qty != null && x.Labour != null)
                 {
                     BudgetValue = BudgetValue + (double)((x.Qty) * (x.Labour));
                     if (SavingRecords.Count() != 0)
                     {
                         var SavingRecord = SavingRecords.Where(a => a.BOMId == x.ID && a.S_type == "Qty").FirstOrDefault();
                         if (SavingRecord != null)
                         {
                             var ActualQty = SavingRecord.ActualQty;
                             actualValue = actualValue + (double)((x.Qty - ActualQty) * (x.Labour));
                         }
                         else
                         {
                             //actualValue = actualValue + (double)((x.Qty) * (x.Material));
                             actualValue = actualValue + 0;
                         }
                     }
                     else
                     {
                         //actualValue = actualValue + (double)((x.Qty) * (x.Material));
                         actualValue = actualValue + 0;
                     }
                 }
             }
         }
         try
         {
             for (int i = 0; i < NamesArray.Length; i++)
             {
                 DataCls = new GraphDataCls();
                 DataCls.Name = NamesArray[i].ToString();
                 if (i == 0)
                 {
                     DataCls.Value = Convert.ToDecimal(actualValue);
                 }
                 else if (i == 1)
                 {
                     DataCls.Value = Convert.ToDecimal(BudgetValue);
                 }
                 if (i == 0)
                 {
                     DataCls.ColorName = "color: #aa66b2";
                     //if (actualValue > BudgetValue)
                     //{
                     //    DataCls.ColorName = "color: #e23124";
                     //}
                     //else
                     //{
                     //    DataCls.ColorName = "color: #5bba5c";
                     //}

                 }
                 else
                 {
                     DataCls.ColorName = "color: #5bba5c";
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
     [WebMethod]
     public static List<GraphDataCls> ExpenseTrackerData(string pid)
     {
         List<GraphDataCls> DataClslist = new List<GraphDataCls>();
         GraphDataCls DataCls = null;
         string Names = "Saving,Budget";
         string[] NamesArray = Names.Split(',');
         double actualValue = 0;
         double BudgetValue = 0;

         using (FinanceModuleDataContext fdc = new FinanceModuleDataContext())
         {
             using (projectTaskDataContext Pdc = new projectTaskDataContext())
             {
                 var expenseReport = fdc.ExternalExpenses.Where(a => a.ProjectReference == int.Parse(pid)).ToList();
                 var SavingRecords = Pdc.GoodsReceiptSavings.Where(a => a.projectRef == int.Parse(pid)).ToList();

                 if (expenseReport.Count() != 0)
                 {
                     foreach (var x in expenseReport)
                     {
                         BudgetValue = BudgetValue + (double)((x.Qty) * (x.ForecastValue));


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
             }
         }


         try
         {
             for (int i = 0; i < NamesArray.Length; i++)
             {
                 DataCls = new GraphDataCls();
                 DataCls.Name = NamesArray[i].ToString();
                 if (i == 0)
                 {
                     DataCls.Value = Convert.ToDecimal(actualValue);
                 }
                 else if (i == 1)
                 {
                     DataCls.Value = Convert.ToDecimal(BudgetValue);
                 }
                 if (i == 0)
                 {
                     DataCls.ColorName = "color: #aa66b2";
                     //if (actualValue > BudgetValue)
                     //{
                     //    DataCls.ColorName = "color: #e23124";
                     //}
                     //else
                     //{
                     //    DataCls.ColorName = "color: #5bba5c";
                     //}
                 }
                 else
                 {
                     DataCls.ColorName = "color: #5bba5c";
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
}
public class GraphDataCls
{
    public string Name { get; set; }
    public decimal Value { get; set; }
    public string ColorName { get; set; }
}
//public class VariationDataCls
//{
//    public string UnApprovedCost { get; set; }
//    public string UnApprovedValue { get; set; }
//    public string ApprovedCost { get; set; }
//    public string ApprovedValue { get; set; }
//}
