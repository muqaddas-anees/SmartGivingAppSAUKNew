using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class CustomerSurveyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            IDCRespository<CallCustomerSurvey> cusrep = new DCRepository<CallCustomerSurvey>();
            CallCustomerSurvey data = new CallCustomerSurvey();
            //var id = Convert.ToString(sessionKeys.UID);
            string iiiii = hidden1.Value;
            var fff = lblval1.Text;
            data.CustId = sessionKeys.UID;
           //if(chk1.Checked)
           //{
           //    data.techOntime = "yes";
           //}
           //else
           //{
           //    data.techOntime = "NO";
           //}

            data.likelyService = hidden1.Value;
            //data.professionalism = hidd2.Value;
            data.servicefeedback = txtfeedinfo.Text;
            cusrep.Add(data);
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Thank you for participating in our survey!";

        }
    }
}