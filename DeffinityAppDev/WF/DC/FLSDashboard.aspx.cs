using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using DC.DAL;
using DC.Entity;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

using Google.DataTable.Net.Wrapper;
using Google.DataTable.Net.Wrapper.Extension;
using System.Globalization;
using DC.SRV;
public partial class FLSDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

      
    }
    #region Bind Charts
    [WebMethod]
    public static string GetChartData1(string fromDate, string toDate, string requestType, string customerId, string statusId)
    {


        var dt = new Google.DataTable.Net.Wrapper.DataTable();
        try
        {
            using (DCDataContext db = new DCDataContext())
            {
                var categoryList = (from c in db.CallDetails
                                    join f in db.FLSDetails on c.ID equals f.CallID
                                    join ct in db.Categories on f.CategoryID equals ct.ID
                                    where c.CompanyID == Convert.ToInt32(customerId)
                                    && c.StatusID == Convert.ToInt32(statusId) && f.RequestType == Convert.ToInt32(requestType)
                                    && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(fromDate)) >= Convert.ToDateTime(fromDate)
                                    && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(toDate)) <= Convert.ToDateTime(toDate)
                                    group f by new { f.CategoryID, ct.Name } into grouping
                                    select new
                                    {
                                        ID = grouping.Key.CategoryID,
                                        Name = grouping.Key.Name
                                    }).ToList();

                // var categoryList = db.Categories.ToList();
                dt.AddColumn(new Google.DataTable.Net.Wrapper.Column(ColumnType.String, "Site", "Site"));
                foreach (var item in categoryList)
                {
                    dt.AddColumn(new Google.DataTable.Net.Wrapper.Column(ColumnType.Number, item.Name, item.Name));
                }



                // var siteList = db.OurSites.ToList();
                var siteList = (from c in db.CallDetails
                                join f in db.FLSDetails on c.ID equals f.CallID
                                join s in db.OurSites on c.SiteID equals s.ID
                                where c.CompanyID == Convert.ToInt32(customerId) && c.StatusID == Convert.ToInt32(statusId) && f.RequestType == Convert.ToInt32(requestType)
                                && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(fromDate)) >= Convert.ToDateTime(fromDate)
                                && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(toDate)) <= Convert.ToDateTime(toDate)
                                group c by new { c.SiteID, s.Name } into grouping
                                select new
                                {
                                    ID = grouping.Key.SiteID,
                                    Name = grouping.Key.Name
                                }).ToList();


                List<gChart> chart = new List<gChart>();

                //Dictionary<int, string> myDict = new Dictionary<int, string>();
                int total = 0;
                foreach (var s in siteList)
                {
                    Row r = dt.NewRow();
                    r.AddCellRange(new Cell[]
            {
                new Cell(s.Name),
            });
                    int totalSum = 0;
                    foreach (var ca in categoryList)
                    {
                        var categoryBySiteCount = (from c in db.CallDetails
                                                   join f in db.FLSDetails on c.ID equals f.CallID
                                                   where c.CompanyID == Convert.ToInt32(customerId) && f.RequestType == Convert.ToInt32(requestType) && c.StatusID == Convert.ToInt32(statusId)
                                                  && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(fromDate)) >= Convert.ToDateTime(fromDate)
                                                  && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(toDate)) <= Convert.ToDateTime(toDate) &&
                                                   c.SiteID == s.ID && f.CategoryID == ca.ID
                                                   select f.CategoryID).Count();
                        totalSum = totalSum + categoryBySiteCount;
                        r.AddCellRange(new Cell[]
            {
                new Cell(categoryBySiteCount),
            });
                    }
                    chart.Add(new gChart { Count = totalSum, Name = s.Name });

                    total = total + totalSum;


                    dt.AddRow(r);
                }

                if (dt.Rows.Count() > 0)
                {
                    var result = chart.OrderByDescending(c => c.Count).FirstOrDefault();

                    string strName = "";
                    double countPercentage = ((double)result.Count / (double)total) * 100;
                    //“<Site> had the highest volume of install tasks between <Start Date> and <End Date> with <%> install works being completed
                    strName = strName + result.Name + " had the highest volume of install tasks between " + fromDate + " and " + toDate + " with " + Math.Round(countPercentage).ToString() + "% of the install  works being completed.";
                    Row r1 = dt.NewRow();
                    r1.AddCellRange(new Cell[]
            {
                new Cell(strName),
            });
                    dt.AddRow(r1);
                }


            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }








        string json = dt.GetJson();
        //Let's create a Json string as expected by the Google gCharts API.
        return dt.GetJson();
    }

    [WebMethod]
    public static List<gChart> GetChartData2(string fromDate, string toDate, string requestType, string customerId, string statusId, string requestName)
    {

        List<gChart> dataList = new List<gChart>();
        try
        {
            using (DCDataContext db = new DCDataContext())
            {


                dataList = (from c in db.CallDetails
                            join f in db.FLSDetails on c.ID equals f.CallID
                            where c.CompanyID == Convert.ToInt32(customerId) && f.RequestType == Convert.ToInt32(requestType) && c.StatusID == Convert.ToInt32(statusId)
                            && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(fromDate)) >= Convert.ToDateTime(fromDate)
                            && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(toDate)) <= Convert.ToDateTime(toDate)
                            join s in db.OurSites on c.SiteID equals s.ID
                            group s by new { s.Name } into grouping
                            select new gChart
                            {
                                Count = grouping.Count(),
                                Name = grouping.Key.Name
                            }).ToList();



            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        if (dataList.Count() > 0)
        {
            string strName = "";
            int countTotal = 0;
            for (int i = 0; i < dataList.Count(); i++)
            {

                countTotal = countTotal + dataList[i].Count;

            }
            var maxRowData = dataList.OrderByDescending(d => d.Count).FirstOrDefault();

            //string requestName = (requestType == "1" ? "Faults" : "Service Request");
            strName = strName + "A total of " + countTotal + " " + requestName + " were dealt with between " + fromDate + " and " + toDate + ". ";
            if (maxRowData != null)
            {
                double countPercentage = ((double)maxRowData.Count / (double)countTotal) * 100;
                strName = strName + maxRowData.Name + " had the highest volume of " + requestName + " with " + maxRowData.Count + " (" + Math.Round(countPercentage).ToString() + "%) of the total.";

            }
            gChart obj = new gChart();

            obj.Count = 0;
            obj.Name = strName;
            dataList.Add(obj);
        }
        return dataList;

    }

    [WebMethod]
    public static List<gChart> GetChartData3(string fromDate, string toDate, string requestType, string customerId, string statusId)
    {

        List<gChart> dataList = new List<gChart>();
        try
        {
            using (DCDataContext db = new DCDataContext())
            {


                dataList = (from f in db.FLSDetails
                            join s in db.FLSSourceOfRequests on f.SourceOfRequestID equals s.ID
                            join c in db.CallDetails on f.CallID equals c.ID
                            where c.CompanyID == Convert.ToInt32(customerId) && f.RequestType == Convert.ToInt32(requestType) && c.StatusID == Convert.ToInt32(statusId)
                            && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(fromDate)) >= Convert.ToDateTime(fromDate)
                            && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(toDate)) <= Convert.ToDateTime(toDate)
                            group s by new { s.Name } into grouping
                            select new gChart
                            {
                                Count = grouping.Count(),
                                Name = grouping.Key.Name
                            }).ToList();



            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        if (dataList.Count() > 0)
        {
            dataList = dataList.OrderByDescending(d => d.Count).ToList();

            string strName = "";
            int countTotal = 0;
            for (int i = 0; i < dataList.Count(); i++)
            {

                countTotal = countTotal + dataList[i].Count;

            }


            for (int i = 0; i < dataList.Count(); i++)
            {
                int intCount = dataList[i].Count; ;

                string strSite = dataList[i].Name;
                double countPercentage = ((double)intCount / (double)countTotal) * 100;

                if (i == 0)
                    strName = strName + "   " + strSite + " represented " + Math.Round(countPercentage).ToString() + "% of all works undertaken between " + fromDate + " and " + toDate + ".";
                else
                    strName = strName + " " + Math.Round(countPercentage).ToString() + "% of the tasks were " + strSite + ". ";


            }

            gChart obj = new gChart();
            obj.Count = 0;
            obj.Name = strName;
            dataList.Add(obj);
        }

        return dataList;
    }

    [WebMethod]
    public static List<gChart> GetChartData4(string fromDate, string toDate, string requestType, string customerId, string statusId)
    {

        List<gChart> dataList = new List<gChart>();
        try
        {
            using (DCDataContext db = new DCDataContext())
            {


                dataList = (from f in db.FLSDetails
                            join d in db.AssignedDepartments on f.DepartmentID equals d.ID
                            join c in db.CallDetails on f.CallID equals c.ID
                            where c.CompanyID == Convert.ToInt32(customerId) && f.RequestType == Convert.ToInt32(requestType) && c.StatusID == Convert.ToInt32(statusId)
                            && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(fromDate)) >= Convert.ToDateTime(fromDate)
                            && (f.DateTimeClosed.HasValue ? f.DateTimeClosed : Convert.ToDateTime(toDate)) <= Convert.ToDateTime(toDate)
                            group d by new { d.DepartmentName } into grouping
                            select new gChart
                            {
                                Count = grouping.Count(),
                                Name = grouping.Key.DepartmentName
                            }).ToList();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        if (dataList.Count() > 0)
        {

            dataList = dataList.OrderByDescending(d => d.Count).ToList();
            string strName = "";
            int countTotal = 0;
            for (int i = 0; i < dataList.Count(); i++)
            {

                countTotal = countTotal + dataList[i].Count;

            }


            for (int i = 0; i < dataList.Count(); i++)
            {
                int intCount = dataList[i].Count; ;
                string strTeam = dataList[i].Name;
                double countPercentage = ((double)intCount / (double)countTotal) * 100;

                if (i == 0)
                    strName = strName + " " + Math.Round(countPercentage).ToString() + "% of the tasks completed between " + fromDate + " and " + toDate + " were handed by " + strTeam + " Team. ";
                else
                    strName = strName + "   " + strTeam + " Team represent " + Math.Round(countPercentage).ToString() + "% of the works completed.";



            }

            gChart obj = new gChart();
            obj.Count = 0;
            obj.Name = strName;
            dataList.Add(obj);
        }
        return dataList;
    }
   
    #endregion
       
             
   

}
#region Custom Class
public class gChart
{
    public string Name { get; set; }
    public int Count { get; set; }
}

#endregion





