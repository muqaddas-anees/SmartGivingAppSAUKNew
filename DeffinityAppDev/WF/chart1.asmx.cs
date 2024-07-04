using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeffinityAppDev.WF
{
    /// <summary>
    /// Summary description for chart1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class chart1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public object GetBarChart()
        {
            //[
            //                                            { valueField: "oil", name: "Oil Production", color: "#0e62c7" },
            //                                            { valueField: "gas", name: "Gas Production", color: "#2c2e2f" },
            //                                            { valueField: "coal", name: "Coal Production", color: "#7c38bc" }
            //                                        ]
            try
            {

                List<dynamic> li_name = new List<dynamic>();

                dynamic l1 = new JObject();
                l1["valueField"] = "oil";
                l1["name"] = "oil";
                l1["color"] = "#0e62c7";
                li_name.Add(l1);
                dynamic l2 = new JObject();
                l2["valueField"] = "gas";
                l2["name"] = "Gas Production";
                l2["color"] = "#2c2e2f";
                li_name.Add(l2);
                dynamic l3 = new JObject();
                l3["valueField"] = "coal";
                l3["name"] = "Coal Production";
                l3["color"] = "#7c38bc";
                li_name.Add(l3);


                List<dynamic> li = new List<dynamic>();
                dynamic product = new JObject();
                product["state"] = "China";
                product["oil"] = 4.95;
                product["coal"] = 2.85;
                dynamic product1 = new JObject();
                product1["state"] = "Russia";
                product1["oil"] = 4.95;
                product1["coal"] = 2.85;
                
                li.Add(product);
                li.Add(product1);


                var rtlist = new { a = li, b = li_name };
                return JsonConvert.SerializeObject(rtlist);
            }
            catch (Exception ex)
            {
                //return hc;
                LogExceptions.WriteExceptionLog(ex);
                return JsonConvert.SerializeObject(string.Empty).ToString();
            }

        }

        [WebMethod]
        public object GetChart()
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

            try
            {
                List<daycls> dlist = new List<daycls>();
                dlist.Add(new daycls() { day = "Monday", sales = "3",color="#2c2e2f" });
                dlist.Add(new daycls() { day = "Tuesday", sales = "2", color = "#0e62c7" });
                dlist.Add(new daycls() { day = "Wednesday", sales = "3", color = "#7c38bc" });
                dlist.Add(new daycls() { day = "Thursday", sales = "4",color="#2c2e2f" });
                //dlist.Add(new daycls() { day = "Friday", sales = "6",color="#2c2e2f" });
                //dlist.Add(new daycls() { day = "Saturday", sales = "11",color="#2c2e2f" });
                //dlist.Add(new daycls() { day = "Sunday", sales = "4",color="#2c2e2f" });

                var rlist = (from p in dlist
                             select new { day = p.day, sales = p.sales ,color = p.color  }).ToList();
                var rtlist = new { a = rlist, b = dlist };

                //return jsonSerializer.Serialize(rtlist).ToString();
                return JsonConvert.SerializeObject(rlist);
            }
            catch (Exception ex)
            {
                //return hc;
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }

        }
        [WebMethod]
        public object ProjectsFinanceChart(int projectreference)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

            try
            {
                ProjectMgt.BAL.ProjectBAL pb = new ProjectMgt.BAL.ProjectBAL();
                var resultlist = pb.Projects_DashboardFinance_Select(projectreference);
                List<dynamic> li_name = new List<dynamic>();
                dynamic d1 = new JObject();
                d1["val"] = resultlist.BudgetedCost;
                d1["name"] = "Budgeted Cost";
                li_name.Add(d1);
                dynamic d2 = new JObject();
                d2["val"] = resultlist.ActaulCost;
                d2["name"] = "Actual Cost";
                li_name.Add(d2);
                dynamic d3 = new JObject();
                d3["val"] = resultlist.VariationsApproved;
                d3["name"] = "Variations Approved";
                li_name.Add(d3);
                dynamic d4 = new JObject();
                d4["val"] = resultlist.VariationsPendingApproval;
                d4["name"] = "Variations Pending Approval";
                li_name.Add(d4);

                return JsonConvert.SerializeObject(li_name);
            }
            catch (Exception ex)
            {
                //return hc;
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }

        }
        public class daycls
        {
            public string day { get; set; }
            public string sales { get; set; }
            public string color { get; set; }
        }
    }
}
