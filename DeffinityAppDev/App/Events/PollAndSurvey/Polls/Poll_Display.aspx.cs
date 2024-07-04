using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.PollAndSurvey.Polls
{
    public partial class Poll_Display : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

                    var value = new PortfolioMgt.Entity.PollAndSurveyDetail();



                    var pEntity = ProjectPortfolioBAL_SelectAll().Where(o => o.UNID == QueryStringValues.UNID).FirstOrDefault();


                    string question = pEntity.Question;

                    //string choise1 = pEntity.MultipleChoiseWithAnswer.Split('%')[0];
                    //string choise2 = pEntity.MultipleChoiseWithAnswer.Split('%')[1];
                    //string choise3 = pEntity.MultipleChoiseWithAnswer.Split('%')[2];
                    //string choise4 = pEntity.MultipleChoiseWithAnswer.Split('%')[3];


                    lblQuestion.Text = pEntity.Question;
                    lblDescription.Text = pEntity.QuestionDescription;
                    hmoney.Value = pEntity.MultipleChoiseWithAnswer;
                    imgQR.ImageUrl = "~/WF/UploadData/Events/" + pEntity.UNID + ".png";
                    UpdateMoneyGrid();
                    //lbloption1.Text = choise1;
                    //lbloption2.Text = choise2;
                    //lbloption3.Text = choise3;
                    //lbloption4.Text = choise4;


                    //R1.Text = choise1;
                    //R2.Text = choise2;
                    //R3.Text = choise3;
                    //R4.Text = choise4;


                    PopulatePollOptions();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

        class moneycls
        {
            public double id { set; get; }
            public string value { set; get; }
        }

        private void UpdateMoneyGrid()
        {
            try
            {
                List<moneycls> mcList = new List<moneycls>();
                var mLIst = hmoney.Value;
                int index = 1;
                foreach (var m in mLIst.Split(','))
                {
                    if (m.Trim().Length > 0)
                        mcList.Add(new moneycls() { id = index, value = m });

                    index++;
                }

                rdList.DataSource = mcList;
                rdList.DataValueField = "id";
                rdList.DataTextField = "value";
                rdList.DataBind();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private DataTable PopulatePollOptions()
        {

            var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

            var value = new PortfolioMgt.Entity.PollAndSurveyDetail();

            

            var pEntity = ProjectPortfolioBAL_SelectAll().Where(o => o.UNID == QueryStringValues.UNID).FirstOrDefault();


            string question = pEntity.Question;

            string choise1 = pEntity.MultipleChoiseWithAnswer.Split('%')[0];
            string choise2 = pEntity.MultipleChoiseWithAnswer.Split('%')[1];
            string choise3 = pEntity.MultipleChoiseWithAnswer.Split('%')[2];
            string choise4 = pEntity.MultipleChoiseWithAnswer.Split('%')[3];




            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();

            var ActivityDetailList = pRep.GetAll().Where(o => o.QuestionID > 0).ToList();


            

                


            //rblPollOptions.DataSource = ActivityDetailList;
            //rblPollOptions.DataTextField = "Question";
            //rblPollOptions.DataValueField = "EventID";
            //rblPollOptions.DataBind();


           



           


            for (int i = 0; i < 4; i++)
            {
                RadioButton r = new RadioButton();
                r.Text = pEntity.MultipleChoiseWithAnswer.Split('%')[i];
                
                r.ID = i.ToString();
                r.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                r.CheckedChanged += new EventHandler(this.ButtonFinish_Click);
                r.Width = 1000;

                r.Font.Size = 20;

               

                //Panel1.Controls.Add(r);

            }










            DataTable dt = new DataTable();
          //  string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd = new SqlCommand("SELECT PollId,PollOptionId,PoleOption FROM PollOptions WHERE PollId = 1", con))
            //    {
            //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            //        {
            //            da.Fill(dt);
            //            rblPollOptions.DataSource = dt;
            //            rblPollOptions.DataTextField = "PoleOption";
            //            rblPollOptions.DataValueField = "PollOptionId";
            //            rblPollOptions.DataBind();
            //        }
            //    }
            //}
            return dt;
        }



        private void ButtonFinish_Click(object sender, EventArgs e)
        {

            RadioButton button = (RadioButton)sender;
            string txt = button.Text;
            string val = button.ID;




        }


        public static List<PortfolioMgt.Entity.PollAndSurveyDetail> GetDetails()
        {
            IUserRepository<PortfolioMgt.Entity.PollAndSurveyDetail> uRep = new UserRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
            return uRep.GetAll().ToList();
        }







        public static IQueryable<PortfolioMgt.Entity.PollAndSurveyDetail> ProjectPortfolioBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
            return pRep.GetAll();
        }




        public static IQueryable<PortfolioMgt.Entity.PollAndSurveySelectedChoise2> SelectedOptionDetails()
        {
            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();
            return pRep.GetAll();
        }





        private string PopulatePollQuestions()
        {
            object question = "";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Question FROM PollQuestions WHERE PollId = 1", con))
                {
                    con.Open();
                    question = cmd.ExecuteScalar();
                    con.Close();
                }
            }
            return question.ToString();
        }




        private DataTable PopulatePollCount(int pollId)
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("PollCount", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PollId", pollId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }


        protected void OnOptionChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            string selectedOption = radioButton.Text;

            string id = radioButton.ID;



            var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();

            var value = new PortfolioMgt.Entity.PollAndSurveySelectedChoise2();

            
            value.QuestionID = 1;
            value.SelectedChoise = selectedOption;
            value.EventID = 1;
            value.userID = 1;


           

            cRep.Add(value);




           

            IPortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveyDetail>();
             pRep.GetAll();



            var list = SelectedOptionDetails();


            var results = list.GroupBy(x => x.SelectedChoise)
            .Select(g => g.OrderBy(x => x.SelectedChoise).FirstOrDefault());




            var pl = from r in list
                     orderby r.SelectedChoise
                     group r by r.SelectedChoise into grp
                     select new { key = grp.Key, cnt = grp.Count() };



           




            foreach (var line in list.GroupBy(info => info.SelectedChoise)
                        .Select(group => new {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
            {
                Console.WriteLine("{0} {1}", line.Metric, line.Count);
            }




           // Server.Transfer("Poll_Result.aspx");


            Response.Redirect("Poll_Result.aspx");


            //rptPollCountGrid.DataSource = pl.ToList();
            //rptPollCountGrid.DataBind();

           





           
        }






        public void createTableData()
        {
            DataTable dt;

            dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("College", typeof(string));
            dt.Rows.Add(1, "Rahul", "8505012345", "MITRC");
            dt.Rows.Add(2, "Pankaj", "8505012346", "MITRC");
            dt.Rows.Add(3, "Sandeep", "8505012347", "MITRC");
            dt.Rows.Add(4, "Sanjeev", "8505012348", "MITRC");
            dt.Rows.Add(5, "Neeraj", "8505012349", "MITRC");
            dt.AcceptChanges();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {


                //if (rdList.SelectedValue.Length > 0)
                //{
                    //var pEntity = ProjectPortfolioBAL_SelectAll().Where(o => o.UNID == QueryStringValues.UNID).FirstOrDefault();


                    //var cRep = new PortfolioRepository<PortfolioMgt.Entity.PollAndSurveySelectedChoise2>();

                    //var value = new PortfolioMgt.Entity.PollAndSurveySelectedChoise2();
                    //value.QuestionID = pEntity.QuestionID;
                    //value.SelectedChoise = rdList.SelectedItem.Text;
                    //value.EventUNID = pEntity.EventUNUID;
                    //value.UNID = QueryStringValues.UNID;
                    //value.UserUNID = Guid.NewGuid().ToString() ;




                    //cRep.Add(value);
                    //DeffinityManager.ShowMessages.ShowSuccessMsg(this.Page, "Submitted successfully", "");
                    Response.Redirect("Poll_Result.aspx?UNID="+ QueryStringValues.UNID);
             //   }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }



   
}