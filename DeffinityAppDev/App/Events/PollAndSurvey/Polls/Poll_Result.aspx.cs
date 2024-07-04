using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.PollAndSurvey.Polls
{
    public partial class Poll_Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hunid.Value = QueryStringValues.UNID;
            OnOptionChanged(null, null);
        }



        class moneycls
        {
            public double id { set; get; }
            public string value { set; get; }

            public int count { set; get; }

            public double percent { set; get; }
        }


        protected void OnOptionChanged(object sender, EventArgs e)
        {
        //    RadioButton radioButton = (RadioButton)sender;

        //    string selectedOption = radioButton.Text;

        //    string id = radioButton.ID;



        //    var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();

        //    var value = new PortfolioMgt.Entity.PollAndSurveySelectedChoise2();


        //    value.QuestionID = 1;
        //    value.SelectedChoise = selectedOption;
        //    value.EventID = 1;
        //    value.userID = 1;




        //    cRep.Add(value);






            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
            var pData  =  pRep.GetAll().Where(o => o.UNID == QueryStringValues.UNID).FirstOrDefault() ;
            lblTitlte.Text = pData.Question;
            var list = SelectedOptionDetails().Where(o => o.UNID == QueryStringValues.UNID);

            double totalcount = list.Count();
            List<moneycls> mcList = new List<moneycls>();
            var mLIst = pData.MultipleChoiseWithAnswer;
            int index = 1;
            foreach (var m in mLIst.Split(','))
            {
                if (m.Trim().Length > 0)
                {
                    int tmcount = list.Where(p => p.SelectedChoise == m).Count();
                    var tmval = (tmcount > 0 ? ((tmcount / totalcount) * 100) : 0);
                    mcList.Add(new moneycls() { id = index, value = m, count = tmcount, percent = tmval });
                }


                index++;
            }


           

            //var results = list.GroupBy(x => x.SelectedChoise)
            //.Select(g => g.OrderBy(x => x.SelectedChoise).FirstOrDefault());




            //var pl = from r in list
            //         orderby r.SelectedChoise
            //         group r by r.SelectedChoise into grp
            //         select new { key = grp.Key, cnt = grp.Count() };








            //foreach (var line in list.GroupBy(info => info.SelectedChoise)
            //            .Select(group => new {
            //                Metric = group.Key,
            //                Count = group.Count()
            //            })
            //            .OrderBy(x => x.Metric))
            //{
            //    Console.WriteLine("{0} {1}", line.Metric, line.Count);
            //}


          



            rptPollCountGrid.DataSource = mcList.ToList();
            rptPollCountGrid.DataBind();








        }







        public static IQueryable<PortfolioMgt.Entity.PollAndSurveySelectedChoise2> SelectedOptionDetails()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();
            return pRep.GetAll();
        }


    }
}