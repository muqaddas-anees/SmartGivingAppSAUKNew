using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Finance.DAL;
using System.Web.Services;
using ProjectMgt.DAL;

public partial class ProjectTracker_General : System.Web.UI.Page
{
    string fileName = "PandL11.xls";
    RiseValuation RiseVal = new RiseValuation();
    protected int getProject = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = Resources.DeffinityRes.ProjectManagement;//"Project Management";
        getProject = QueryStringValues.Project;

    }
    [WebMethod]
    public static List<GraphDataCls> CostOfMiscitemsData(string pid)
    {
        List<GraphDataCls> DataClslist = new List<GraphDataCls>();
        GraphDataCls DataCls = null;
        string Names = "Saving,Actual,Budget";
        string[] NamesArray = Names.Split(',');
        double actualValue = 0;
        double BudgetValue = 0;
        double SavingValue = 0;

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
                            SavingValue = SavingValue + (double)((x.Qty - ActualQty) * (x.Mics));
                        }
                        else
                        {
                            //actualValue = actualValue + (double)((x.Qty) * (x.Mics));
                            SavingValue = SavingValue + 0;
                        }
                    }
                    else
                    {
                        //actualValue = actualValue + (double)((x.Qty) * (x.Mics));
                        SavingValue = SavingValue + 0;
                    }
                }
                var Goodsvalue = Pdc.GoodsReceipts.Where(o => o.BOMID == x.ID).Select(a => a).FirstOrDefault();
                if (Goodsvalue != null)
                {
                    actualValue = (double)(actualValue + ((x.Mics) * Goodsvalue.QtyReceived));
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
                    DataCls.Value = Convert.ToDecimal(SavingValue);
                }
                else if (i == 1)
                {
                    DataCls.Value = Convert.ToDecimal(actualValue);
                }
                else if (i == 2)
                {
                    DataCls.Value = Convert.ToDecimal(BudgetValue);
                }

                if (i == 0)
                {
                    DataCls.ColorName = "color: aa66b2";
                }
                else if (i == 1)
                {
                    if (actualValue > BudgetValue)
                    {
                        DataCls.ColorName = "color: #e23124";
                    }
                    else
                    {
                        DataCls.ColorName = "color: #5bba5c";
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
    public static List<GraphDataCls> CostOfMaterialsData(string pid)
    {
        List<GraphDataCls> DataClslist = new List<GraphDataCls>();
        GraphDataCls DataCls = null;
        string Names = "Saving,Actual,Budget";
        string[] NamesArray = Names.Split(',');
        double actualValue = 0;
        double BudgetValue = 0;
        double SavingValue = 0;

        using (projectTaskDataContext Pdc = new projectTaskDataContext())
        {
            var BomList = (from r in Pdc.ProjectBOMs
                           join b in Pdc.BOM_Types on r.WorkSheetID equals b.ID
                           where (b.IsDeleted.HasValue ? b.IsDeleted.Value : false) == false && r.ProjectReference == int.Parse(pid) && r.ID != -99
                           select r).ToList();
           // var BomList = Pdc.ProjectBOMs.Where(a => a.ProjectReference == int.Parse(pid)).Select(a => a).ToList();
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
                            SavingValue = SavingValue + (double)((x.Qty - ActualQty) * (x.Material));
                        }
                        else
                        {
                            //actualValue = actualValue + (double)((x.Qty) * (x.Material));
                            SavingValue = SavingValue + 0;
                        }
                    }
                    else
                    {
                        //actualValue = actualValue + (double)((x.Qty) * (x.Material));
                        SavingValue = SavingValue + 0;
                    }
                }
                var Goodsvalue = Pdc.GoodsReceipts.Where(o => o.BOMID == x.ID).Select(a => a).FirstOrDefault();
                if (Goodsvalue != null)
                {
                    actualValue = (double)(actualValue + ((x.Material) * Goodsvalue.QtyReceived));
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
                    DataCls.Value = Convert.ToDecimal(SavingValue);
                }
                else if (i == 1)
                {
                    DataCls.Value = Convert.ToDecimal(actualValue);
                }
                else if (i == 2)
                {
                    DataCls.Value = Convert.ToDecimal(BudgetValue);
                }
                if (i == 0)
                {
                    DataCls.ColorName = "color: aa66b2";
                }
                else if (i == 1)
                {
                    if (actualValue > BudgetValue)
                    {
                        DataCls.ColorName = "color: #e23124";
                    }
                    else
                    {
                        DataCls.ColorName = "color: #5bba5c";
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
    public static List<GraphDataCls> CostOfLabourData(string pid)
    {
        List<GraphDataCls> DataClslist = new List<GraphDataCls>();
        GraphDataCls DataCls = null;
        string Names = "Saving,Actual,Budget";
        string[] NamesArray = Names.Split(',');
        double actualValue = 0;
        double BudgetValue = 0;
        double SavingValue = 0;

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
                            SavingValue = SavingValue + (double)((x.Qty - ActualQty) * (x.Labour));
                        }
                        else
                        {
                            //actualValue = actualValue + (double)((x.Qty) * (x.Material));
                            SavingValue = SavingValue + 0;
                        }
                    }
                    else
                    {
                        //actualValue = actualValue + (double)((x.Qty) * (x.Material));
                        SavingValue = SavingValue + 0;
                    }
                }
                var Goodsvalue = Pdc.GoodsReceipts.Where(o => o.BOMID == x.ID).Select(a => a).FirstOrDefault();
                if (Goodsvalue != null)
                {
                    actualValue = (double)(actualValue + ((x.Labour) * Goodsvalue.QtyReceived));
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
                    DataCls.Value = Convert.ToDecimal(SavingValue);
                }
                else if (i == 1)
                {
                    DataCls.Value = Convert.ToDecimal(actualValue);
                }
                else if (i == 2)
                {
                    DataCls.Value = Convert.ToDecimal(BudgetValue);
                }
                if (i == 0)
                {
                    DataCls.ColorName = "color: aa66b2";
                }
                else if (i == 1)
                {
                    if (actualValue > BudgetValue)
                    {
                        DataCls.ColorName = "color: #e23124";
                    }
                    else
                    {
                        DataCls.ColorName = "color: #5bba5c";
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
    public static List<GraphDataCls> ExpenseTrackerData(string pid)
    {
        List<GraphDataCls> DataClslist = new List<GraphDataCls>();
        GraphDataCls DataCls = null;
        string Names = "Saving,Actual,Budget";
        string[] NamesArray = Names.Split(',');
        double actualValue = 0;
        double BudgetValue = 0;
        double SavingValue = 0;

        using (FinanceModuleDataContext fdc = new FinanceModuleDataContext())
        {
            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {
                var expenseReport = fdc.ExternalExpenses.Where(a => a.ProjectReference == int.Parse(pid)).ToList();
                var savingRecords = Pdc.GoodsReceiptSavings.Where(a => a.S_type == "Expenses").ToList();
                if (expenseReport.Count() != 0)
                {
                    foreach (var x in expenseReport)
                    {
                        double Saving = 0;
                        if (savingRecords.Where(a => a.BOMId == x.ID).Select(a => a.UnitCostSaving).FirstOrDefault() != null)
                        {
                            Saving = savingRecords.Where(a => a.BOMId == x.ID).Select(a => a.UnitCostSaving).FirstOrDefault().Value;
                            SavingValue = SavingValue + (double)((x.Qty) * (x.UnitCost - Saving));
                        }
                        BudgetValue = BudgetValue + (double)((x.Qty) * (x.ForecastValue));
                        actualValue = actualValue + (double)((x.Qty) * (x.UnitCost));

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
                    DataCls.Value = Convert.ToDecimal(SavingValue);
                }
                else if (i == 1)
                {
                    DataCls.Value = Convert.ToDecimal(actualValue);
                }
                else if (i == 2)
                {
                    DataCls.Value = Convert.ToDecimal(BudgetValue);
                }
                if (i == 0)
                {
                    DataCls.ColorName = "color: aa66b2";
                }
                else if (i == 1)
                {
                    if (actualValue > BudgetValue)
                    {
                        DataCls.ColorName = "color: #e23124";
                    }
                    else
                    {
                        DataCls.ColorName = "color: #5bba5c";
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