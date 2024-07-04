using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class PurchaseTraining : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindDecription();
                    BindListview();
                    
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }

            HttpContext.Current.Response.AddHeader("Set-Cookie", "HttpOnly;Secure;SameSite=Strict");
        }

        private void BindDecription()
        {
            try
            {
                var c = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();

                if (c.TrainingDescription != null)
                {
                    if (!string.IsNullOrEmpty(c.TrainingDescription))
                    {
                        pnlDescription.Visible = true;
                        lblDescription.Text = Server.HtmlDecode(c.TrainingDescription);
                    }
                    else
                    {
                        pnlDescription.Visible = false;
                    }

                }
                else
                {
                    pnlDescription.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindListview()
        {
            try
            {
                var modulelist = PortfolioMgt.BAL.PortfolioTrainingBAL.PortfolioTrainingBAL_TrainingSelect().ToList();
                list_training.DataSource = modulelist;
                list_training.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected string GetCssImage(dynamic img)
        {
            string retval = "fa-soccer-ball-o";
            if (img != null)
            {
                var t = img;
                retval = t as string;
            }
            return string.Format("<i class='{0}'></i>", retval);
        }

        protected void list_training_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "buy")
                {
                    var arg = e.CommandArgument.ToString();
                    //var optionid = Convert.ToInt32(e.CommandArgument.ToString());

                    //Session["trainingamount"] = Convert.ToDouble(arg);
                    //Session["trainingid"] = arg;

                    Response.Redirect("~/WF/CustomerAdmin/PurchaseTrainingPayment.aspx?tid="+arg.ToString());
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

       
    }
}