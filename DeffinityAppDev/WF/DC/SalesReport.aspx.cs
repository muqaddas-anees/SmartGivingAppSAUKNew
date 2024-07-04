using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.SRV;
using System.Text;

namespace DeffinityAppDev.WF.DC
{
    public partial class SalesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Deffinity.Utility.StartDateOfMonth(DateTime.Now.AddMonths(-5)));
                txtTodate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Deffinity.Utility.EndDateOfMonth(DateTime.Now));

                lbltable.Text = BuildGrid();
            }
        }

        private string BuildGrid()
        {
            StringBuilder retval = new StringBuilder();
            var d = WebService.GetDataBySalesGridView("", "", "", "");
            if(d.Count >0)
            {
                //Ranking 
                var ranklist = d.GroupBy(a => new { key = a.name }).Select(c => new { name = c.Key.key, count = c.Sum(o => o.count) }).ToList();
                List<ListItem> list = new List<ListItem>();
                int ranking = 1;
                foreach (var item in ranklist.OrderByDescending(i => i.count))
                {
                    if (item.count == 0)
                    {
                        ranking = 0;
                        list.Add(new ListItem(item.name, (ranking).ToString()));
                        ranking++;
                    }
                    else
                        list.Add(new ListItem(item.name, (ranking++).ToString()));
                }

                foreach(var dEntity in d)
                {
                    dEntity.rank = Convert.ToInt32( list.Where(o => o.Text == dEntity.name).Select(o => o.Value).FirstOrDefault());
                }



                retval.Append("<table class='table table-small-font table-bordered table-striped dataTable gridstyle'> <tbody>");

                retval.Append("<tr class='text-center'>");
                retval.Append("<th> </td>");
                retval.Append("<th> Name</td>");

                retval.Append("<th class='cls_rank'> Rank</td>");

                //set ranks
               

                var pdis = d.Select(o => o.dateitem).Distinct();

                foreach(var r in pdis)
                {
                    retval.Append(string.Format("<th scope='col' class='cls_rank'> {0} </td>", r));
                }

                retval.Append("</tr>");


                //new rows
                var rdis = d.GroupBy(a => new { key = a.name }).Select(c => new { name = c.Key.key,id = c.Select(s => s.userid).FirstOrDefault(), Rank = c.Select(s=>s.rank).FirstOrDefault(), count = c.Sum(o => o.count) }).ToList(); //d.Select(o => o.name).Distinct();
                bool iseven = true;
                foreach (var row in rdis.Where(o=>o.Rank>0).OrderBy(o=>o.Rank))
                {
                    
                    if (iseven)
                    {
                        retval.Append( string.Format( "<tr class='{0}'>", iseven?"even":"odd"));
                        iseven = !iseven;
                    }
                    retval.Append(string.Format("<td  class='cls_img'> {0} </td>", "<img src='../Admin/ImageHandler.ashx?type=user&id=" + row.id + "' alt='' height='45px'> "));
                    retval.Append(string.Format("<td> {0} </td>", row.name));

                    retval.Append(string.Format("<td class='cls_rank'> {0} </td>", list.Where(o=>o.Text == row.name).Select(o=>o.Value).FirstOrDefault()));

                    foreach (var r in pdis)
                    {
                        retval.Append(string.Format("<td class='cls_data'> {0} </td>", d.Where(o=>o.name == row.name && o.dateitem == r).Select(o=>o.count).Sum() ));
                    }

                    retval.Append("</tr>");
                }
                foreach (var row in rdis.Where(o => o.Rank == 0).OrderBy(o => o.Rank))
                {

                    if (iseven)
                    {
                        retval.Append(string.Format("<tr class='{0}'>", iseven ? "even" : "odd"));
                        iseven = !iseven;
                    }
                    retval.Append(string.Format("<td  class='cls_img'> {0} </td>", "<img src='../Admin/ImageHandler.ashx?type=user&id=" + row.id + "' alt='' height='45px'> "));
                    retval.Append(string.Format("<td> {0} </td>", row.name));

                    retval.Append(string.Format("<td class='cls_rank'> {0} </td>", list.Where(o => o.Text == row.name).Select(o => o.Value).FirstOrDefault()));

                    foreach (var r in pdis)
                    {
                        retval.Append(string.Format("<td class='cls_data'> {0} </td>", d.Where(o => o.name == row.name && o.dateitem == r).Select(o => o.count).Sum()));
                    }

                    retval.Append("</tr>");
                }

                retval.Append("</tbody> </table>");
            }
            
            



            return retval.ToString();

        }
    }
}