using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
/// <summary>
/// Summary description for RiseValuation
/// </summary>
public class RiseValuation
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    DbCommand cmd;
    DisBindings getData = new DisBindings();
    DataSet ds = new DataSet();
    int valuevation;
	public RiseValuation()
	{
		
        //
		// TODO: Add constructor logic here
		//
	}
    public List<ListItem> LoadInvoiceStatus()
    {
        List<ListItem> listStatus = new List<ListItem>();
        //listStatus.Add(new ListItem("Please select...", "0"));
        listStatus.Add(new ListItem("Paid", "1"));
        listStatus.Add(new ListItem("Pending", "2"));
        listStatus.Add(new ListItem("Submitted", "3"));
        return listStatus;
    }
    //Select InvoiceReference from ProjectValuations where ValuationID =

    public SqlDataReader GetInvoiceData(int invoiceid)
    {
        SqlDataReader dr = null;
        try
        {
           dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("Select * from ProjectValuations where ValuationID =@invoiceid"), new SqlParameter("@invoiceid", invoiceid));
        }
        catch
        { throw; }

        return dr;
    }
    //Read invoice summary
    public SqlDataReader GetInvoiceSummary(int projectreference)
    {
        SqlDataReader dr = null;
        try
        {
           dr = SqlHelper.ExecuteReader(Constants.DBString,CommandType.StoredProcedure,"Deffinity_ProjectInvoiceSummary",new SqlParameter("@ProjectReference",projectreference));
        }
        catch { throw; }
        return dr;
    }
    public SqlDataReader GetInvoiceTotal(int projectreference,int invoiceid)
    {
        SqlDataReader dr = null;
        try
        {
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectInvoiceTotal", new SqlParameter("@ProjectReference", projectreference), new SqlParameter("@InvoiceID", invoiceid));
        }
        catch { throw; }
        return dr;
    }
    //insert new project valuation item with project valuation 0
    public void TaskItemToInvoice(int Valution ,string task, string QTY, string Price, string Percent, bool CostOrVariation, int ProjectReference)
    {
        try 
        {
            DbCommand cmd = db.GetStoredProcCommand("DN_InsertValuationItems");

            db.AddInParameter(cmd, "@ProjectValuationID", DbType.Int32, Valution);
            db.AddInParameter(cmd, "@ItemDescription",DbType.String, task);
            db.AddInParameter(cmd, "@QTY",DbType.Int32, Convert.ToInt32(QTY));
            db.AddInParameter(cmd, "@Price",DbType.Double, Convert.ToDouble(Price));
            db.AddInParameter(cmd, "@PercentComplete",DbType.Int32, Convert.ToInt32(Percent));
            db.AddInParameter(cmd, "@Approved", DbType.Boolean, false);
            db.AddInParameter(cmd, "@Type", DbType.Boolean, CostOrVariation);
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectReference);    
        
            db.ExecuteNonQuery(cmd);
            cmd.Dispose();
        }
        catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }
    }
    public void TaskItemToInvoiceUpdate(string ID,string task, string QTY, string Price, string Percent)
    { 
        try
        {
            DbCommand cmd = db.GetStoredProcCommand("DN_UpdateValuationItems");
            db.AddInParameter(cmd, "@ID", DbType.Int32, Convert.ToInt32(ID));
            db.AddInParameter(cmd, "@ItemDescription", DbType.String, task);
            db.AddInParameter(cmd, "@QTY", DbType.Int32, Convert.ToInt32(QTY));
            db.AddInParameter(cmd, "@Price", DbType.Double, Convert.ToDouble(Price));
            db.AddInParameter(cmd, "@PercentComplete", DbType.Int32, Convert.ToInt32(Percent));

            db.ExecuteNonQuery(cmd);
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void ProjectValuesJournal(int pref,string BudgetaryCost, string BudgetaryCostLevel3, string ActualCost, string BuyingPrice, string ProjectForecast, string PendingCosts, string AccrualsPriorFinancial, string AccrualsCurrentFinancial, string CurrentMonthAccrual, int RaisedBy)
    {
        try
        {

            DbCommand cmd = db.GetStoredProcCommand("DN_InsertProjectValueJournal");
            
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, pref);
            db.AddInParameter(cmd, "@BudgetaryCost", DbType.Double, getData.getDouble(BudgetaryCost));
            db.AddInParameter(cmd, "@BudgetaryCostLevel3", DbType.Double, getData.getDouble(BudgetaryCostLevel3));
            db.AddInParameter(cmd, "@ActualCost", DbType.Double, getData.getDouble(ActualCost));
            db.AddInParameter(cmd, "@BuyingPrice", DbType.Double, getData.getDouble(BuyingPrice));
            db.AddInParameter(cmd, "@ProjectForecast", DbType.Double, getData.getDouble(ProjectForecast));
            db.AddInParameter(cmd, "@PendingCosts", DbType.Double, getData.getDouble(PendingCosts));
            db.AddInParameter(cmd, "@AccrualsPriorFinancial", DbType.Double, getData.getDouble(AccrualsPriorFinancial));
            db.AddInParameter(cmd, "@AccrualsCurrentFinancial", DbType.Double, getData.getDouble(AccrualsCurrentFinancial));
            db.AddInParameter(cmd, "@CurrentMonthAccrual", DbType.Double, getData.getDouble(CurrentMonthAccrual));            
            db.AddInParameter(cmd, "@RaisedBy", DbType.Int32, RaisedBy);

            db.ExecuteNonQuery(cmd);
            cmd.Dispose();

        }
        catch (Exception ex)
        { }

    }
    public void ListPosition(int pref)
    {
        ArrayList al = new ArrayList();
        try
        {            
            // Initialize the Stored Procedure
            DbCommand cmd = db.GetSqlStringCommand("Select ID from ProjectTaskItems where ProjectReference='" + pref.ToString() + "' order by ListPosition");
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    al.Add(dr["ID"].ToString());
                }
            }
            cmd.Dispose();

            //update list position
            int x = 1;
            foreach (string i in al)
            {
                DbCommand cmd1 = db.GetSqlStringCommand("Update ProjectTaskItems set ListPosition='" + x.ToString() + "'where ID='" + i + "'");
                db.ExecuteNonQuery(cmd1);
                x = x + 1;
                cmd1.Dispose();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    //clear fields
    private void ClearFields()
    {
        ds.Dispose();
        cmd.Dispose();
    }

    

    public string GetPandLString(int pref)
    {
       
            StringBuilder strHtml = new StringBuilder();
            try
            {
                int ProjectReference = pref;
                int tempID = 0;

                string st = "";
                string st1 = "";
                string st2 = "";
                string st3 = "";
                string st4 = "";
                string grossMargin = "";
                string percent = "";
                //get data into data set        
                cmd = db.GetStoredProcCommand("DEFFINITY_pnl");
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, ProjectReference);
                ds = db.ExecuteDataSet(cmd);
                cmd.Dispose();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    strHtml.Append("<html><head></head><body>");
                    strHtml.Append("<table border='1' cellpadding='0' cellspacing='0' width='1100px'>");
                    strHtml.Append("<tr><td colspan='13' style='font-size:x-large;color:#588526'>Project profit and loss account</td></tr>");
                    //strHtml.Append("<tr><td colspan='13' style='font-size:x-large;color:#FD6100'>Project profit and loss account</td></tr>");
                    //table 1

                    int t = 1;
                    strHtml.Append("<tr><td colspan='13'>&nbsp;</td></tr>");
                    strHtml.Append("<tr><td colspan='13'><strong>Project Reference&nbsp;&nbsp;&nbsp;<font style='color:#FD6100'>" + sessionKeys.Prefix + pref.ToString() + "</font></strong></td></tr>");
                    strHtml.Append("<tr><td colspan='13'><strong><font>Project start date&nbsp;&nbsp;&nbsp;" +  ds.Tables[t].Rows[0][1].ToString() + "</strong></td></tr>");
                    strHtml.Append("<tr><td colspan='13'><strong>Project end date&nbsp;&nbsp;&nbsp;&nbsp;" +  ds.Tables[t].Rows[0][2].ToString()+ "</strong></td></tr>");
                    strHtml.Append("<tr><td colspan='13'>&nbsp;</td></tr>");
                    strHtml.Append("<tr ><td colspan='2'></td><td colspan='6' align='center' style='background-color:Orange'><strong>Financial year </strong></td>");
                    strHtml.Append("<td align='right'>&nbsp;</td>");
                    strHtml.Append("<td colspan='4' align='center'  style='background-color:Gray'><strong>Project life</strong></td>");
                    strHtml.Append("<td align='right'>&nbsp;</td></tr>");
                    //startdate row
                    strHtml.Append("<tr><td colspan='2'>Start date</td><td align='right'>" +  ds.Tables[t].Rows[0][1].ToString()+ "</td><td align='right'>" +  ds.Tables[t].Rows[0][5].ToString() + "</td><td align='right'>" + ds.Tables[t].Rows[0][8].ToString() + "</td><td align='right'>" +  ds.Tables[t].Rows[0][5].ToString() + "</td><td align='right'>" +  ds.Tables[t].Rows[0][10].ToString() + "</td><td align='right'>&nbsp;</td>");
                    strHtml.Append("<td align='right'>&nbsp;</td>");
                    strHtml.Append("<td align='right'>" +  ds.Tables[t].Rows[0][1].ToString() + "</td><td align='right'>" +  ds.Tables[t].Rows[0][10].ToString()+ "</td><td align='right'>" + ds.Tables[t].Rows[0][1].ToString() + "</td>");
                    strHtml.Append("<td align='right'>&nbsp;</td><td align='right'>&nbsp;</td></tr>");
                    //end date row
                    strHtml.Append("<tr><td colspan='2'>End date</td><td align='right'>" + ds.Tables[t].Rows[0][4].ToString() + "</td><td align='right'>" +  ds.Tables[t].Rows[0][6].ToString() + "</td><td align='right'>" +  ds.Tables[t].Rows[0][9].ToString() + "</td><td align='right'>" +  ds.Tables[t].Rows[0][9].ToString() + "</td><td align='right'>" +  ds.Tables[t].Rows[0][6].ToString() + "</td><td align='right'>" +  ds.Tables[t].Rows[0][6].ToString() + "</td>");
                    strHtml.Append("<td align='right'>&nbsp;</td>");
                    strHtml.Append("<td align='right'>" +  ds.Tables[t].Rows[0][9].ToString()+ "</td><td align='right'>" +  ds.Tables[t].Rows[0][2].ToString() + "</td><td align='right'>" +  ds.Tables[t].Rows[0][2].ToString() + "</td>");
                    strHtml.Append("<td align='right'>&nbsp;</td><td align='right'>&nbsp;</td></tr>");
                    //empty row
                    strHtml.Append("<tr><td colspan='13'>&nbsp;</td></tr>");
                    //variations
                    strHtml.Append("<tr><td colspan='2'></td><td align='right'><strong>Actual prior FY</strong></td><td align='right'><strong>Actual current FY</strong></td><td align='right'><strong>Current month</strong></td><td align='right'><strong>Actual to date</strong></td><td align='right'><strong>To completion</strong></td><td align='right'><strong>Forecast at completion</strong></td>");
                    strHtml.Append("<td align='right'>&nbsp;</td>");
                    strHtml.Append("<td align='right'><strong>Actual to date</strong></td><td align='right'><strong>To completion</strong></td><td align='right'><strong>Forecast at completion</strong></td>");
                    strHtml.Append("<td align='right'><strong>Project value</strong></td><td align='right'>&nbsp;</td></tr>");
                    //add vairation
                    strHtml.Append(st);

                    strHtml.Append("<tr><td colspan='13'>&nbsp;</td></tr>");


                    //get sales values
                    //table 2
                    t = 2;
                    string st_core = "";
                    for (int i = 0; i < ds.Tables[t].Rows.Count; i++)
                    {
                        if(i==0)
                        st_core = st_core + "<tr><td colspan='2'>Sales  - Core</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][3].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][4].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][5].ToString()) + "</td><td align='right'>&nbsp;</td><td  align='right'>" + getValue(ds.Tables[t].Rows[i][6].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][7].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][8].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][9].ToString()) + "</td></tr>";
                        else
                        st_core = st_core + "<tr><td colspan='2'>Sales  - "+ds.Tables[t].Rows[i][12].ToString()+"</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][3].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][4].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][5].ToString()) + "</td><td align='right'>&nbsp;</td><td  align='right'>" + getValue(ds.Tables[t].Rows[i][6].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][7].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][8].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][9].ToString()) + "</td></tr>";
                    }
                    //total
                    //table 3
                    t = 3;
                    st_core = st_core + "<td align='right'>&nbsp;</td></tr>";
                    st_core = st_core + "<tr><td colspan='2'><strong>TOTAL SALES</strong></td><td align='right'>" + getValue(ds.Tables[t].Rows[0][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][4].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][5].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][6].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][7].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][8].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][9].ToString()) + "</td><td align='right'>&nbsp;</td></tr>";
                    st_core = st_core + "<tr><td colspan='13'>&nbsp;</td></tr>";
                    strHtml.Append(st_core);


                    //get cost values 
                    //table 4
                    t = 4;
                    st_core = "";
                    for (int i = 0; i < ds.Tables[t].Rows.Count; i++)
                    {
                        if(i==0)
                        st_core = st_core + "<tr><td colspan='2'>Cost - Core </td><td align='right'>" + getValue(ds.Tables[t].Rows[i][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][3].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][4].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][5].ToString()) + "</td><td align='right'>&nbsp;<td align='right'>" + getValue(ds.Tables[t].Rows[i][6].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][7].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][8].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][9].ToString()) + "</td><td align='right'>&nbsp;</td></tr>";
                        else
                        st_core = st_core + "<tr><td colspan='2'>Cost - " + ds.Tables[t].Rows[i][12].ToString() + " </td><td align='right'>" + getValue(ds.Tables[t].Rows[i][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][3].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][4].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][5].ToString()) + "</td><td align='right'>&nbsp;<td align='right'>" + getValue(ds.Tables[t].Rows[i][6].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][7].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][8].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[i][9].ToString()) + "</td><td align='right'>&nbsp;</td></tr>";
                    }
                    //total cost
                    //table 5
                    t = 5;
                    st_core = st_core + "<tr><td colspan='13'>&nbsp;</td></tr>";
                    st_core = st_core + "<tr><td colspan='2'><strong>TOTAL COST</strong></td><td align='right'>" + getValue(ds.Tables[t].Rows[0][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][4].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][5].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][6].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][7].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][8].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][9].ToString()) + "</td><td align='right'>&nbsp;</td></tr>";
                    strHtml.Append(st_core);
                    //strHtml.Append(CostVariation(ProjectReference));
                    //Bounus
                    //table 6
                    t = 6;
                    
                    st_core = "";
                    st_core = st_core + "<tr><td colspan='13'>&nbsp;</td></tr>";
                    st_core = st_core + "<tr><td colspan='2'><strong>BONUS</strong></td><td align='right'>" + getValue(ds.Tables[t].Rows[0][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td></tr>";
                    strHtml.Append(st_core);

                    //abatements

                    //table 7
                    t = 7;
                    st_core = "";
                    st_core = st_core + "<tr><td colspan='13'>&nbsp;</td></tr>";
                    st_core = st_core + "<tr><td colspan='2'><strong>ABATEMENTS</strong></td><td align='right'>" + getValue(ds.Tables[t].Rows[0][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td></tr>";
                    strHtml.Append(st_core);
                    //Accurals
                    //table 8
                    t = 8;
                    st2 = st2 + "<tr><td colspan='13'>&nbsp;</td></tr>";
                    st2 = st2 + "<tr><td colspan='2'><strong>ACCRUAL</strong></td><td align='right'>" + getValue(ds.Tables[t].Rows[0][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td></tr>";

                    strHtml.Append(st2);
                    //total cost + accurals
                    //table 9
                    t = 9;
                    st3 = st3 + "<tr><td colspan='13'>&nbsp;</td></tr>";
                    st3 = st3 + "<tr><td colspan='2'><strong>TOTAL COST PLUS ACCRUAL</strong></td><td align='right'>" + getValue(ds.Tables[t].Rows[0][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td></tr>";

                    strHtml.Append(st3);
                    //table 10
                    t = 10;
                    //contingency
                    st4 = st4 + "<tr><td colspan='13'>&nbsp;</td></tr>";
                    st4 = st4 + "<tr><td colspan='2'><strong>CONTINGENCY</strong></td><td align='right'>" + getValue(ds.Tables[t].Rows[0][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[10].Rows[0][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[10].Rows[0][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[10].Rows[0][3].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>" + getValue(ds.Tables[10].Rows[0][4].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td><td align='right'>&nbsp;</td></tr>";
                    strHtml.Append(st4);

                    //table 11
                    t = 11;
                    grossMargin = grossMargin + "<tr><td colspan='13'>&nbsp;</td></tr>";
                    grossMargin = grossMargin + "<tr><td colspan='2'><strong>GROSS MARGIN</strong></td><td align='right'>" + getValue(ds.Tables[t].Rows[0][0].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][1].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][2].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][3].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][4].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][5].ToString()) + "</td><td align='right'>&nbsp;</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][6].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][7].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][8].ToString()) + "</td><td align='right'>" + getValue(ds.Tables[t].Rows[0][9].ToString()) + "</td></tr>";
                    strHtml.Append(grossMargin);

                    //table 12
                    t = 12;
                    percent = percent + "<tr><td colspan='13'>&nbsp;</td></tr>";
                    percent = percent + "<tr><td colspan='2' align='center'><strong>%</strong></td><td align='right'>" + string.Format("{0:p}", ds.Tables[t].Rows[0][0]) + "</td><td align='right'>" + string.Format("{0:p}", ds.Tables[t].Rows[0][1]) + "</td><td align='right'>" + string.Format("{0:p}", ds.Tables[t].Rows[0][2]) + "</td><td align='right'>" + string.Format("{0:p}", ds.Tables[t].Rows[0][3]) + "</td><td align='right'>" + string.Format("{0:p}", ds.Tables[t].Rows[0][4]) + "</td><td align='right'>" + string.Format("{0:p}", ds.Tables[t].Rows[0][5]) + "</td><td align='right'>&nbsp;</td><td align='right'>" + string.Format("{0:p}", ds.Tables[t].Rows[0][6]) + "</td><td align='right'>" + string.Format("{0:p}", ds.Tables[t].Rows[0][7]) + "</td><td align='right'>" + string.Format("{0:p}", ds.Tables[t].Rows[0][8]) + "</td><td align='right'>" + string.Format("{0:p}", ds.Tables[t].Rows[0][9]) + "</td></tr>";

                    strHtml.Append(percent);
                    string emptyString = "<table>";
                    for(int i =0;i<=100; i++)
                    {
                        emptyString = emptyString + "<tr><td>&nbsp;</td</tr>";
                    }
                    emptyString = "</table></body></html>";

                    strHtml.Append(emptyString);
                }
              
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message, "P & L generating");
            }

        //percent
        return strHtml.ToString();

    }
    private string getPercent(string sp)
    {       
        return string.Format("{0:p}", sp);
    }
   
    private string getValue(string val)
    {
        string retval = "";
        if (val.Trim() != null)
        {
            try
            {
                if (Convert.ToDouble(val) >= 0)
                {
                    retval = string.Format("{0:#.00}",Convert.ToDouble(val)).ToString();
                }
                else if (Convert.ToDouble(val) < 0)
                {
                    //<font color="red"></font>
                    retval = "<font color='red'>" + string.Format("{0:#.00}",Convert.ToDouble(val)).ToString() + "</font>";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);
            }
        
        }

       // retval = string.Format("{0:#.00}", retval);
        return retval;

    }
}
