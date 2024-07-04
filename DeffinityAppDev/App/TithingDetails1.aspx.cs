using DC.BLL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
   



    public partial class TithingDetails1 : System.Web.UI.Page
    {

        public class DisplayAmountClass
        {
            public double svalue { set; get; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    string[] Month = new string[] { "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
                    ddlMonth.DataSource = Month;
                    ddlMonth.DataBind();
                    //pre-select one for testing
                    ddlMonth.SelectedIndex = 4;

                    //populate year
                    ddlYear.Items.Add("");
                    int Year = DateTime.Now.Year;
                    for (int i = 0; i < 10; i++)
                    {
                        ddlYear.Items.Add((Year + i).ToString());
                    }
                    //pre-select one for testing
                    ddlYear.SelectedIndex = 3;
                    //  BindReligion();
                    //if (Request.QueryString["mid"] != null)
                    //{
                    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                    IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                    var tithingDetail = pRep.GetAll().Where(o => o.OrganizationID == 0).FirstOrDefault();

                    if (tithingDetail != null)
                    {
                        var vValues = tithingDetail.DefaultValues.Split(',').Where(o => o.Trim().Length > 0).ToList();
                        listamount.DataSource = vValues;
                        listamount.DataBind();
                        string curr = tithingDetail.Currency;
                    }


                   
                    var tithinglist = pRep.GetAll().Where(o => o.OrganizationID > 0).ToList();

                    if (tithinglist != null)
                    {
                        var vValues = tithinglist.OrderByDescending(o=>o.ID).ToList();
                        ListFaithGiving.DataSource = vValues;
                        ListFaithGiving.DataBind();
                        //string curr = tithingDetail.Currency;
                    }
                }

                if (sessionKeys.Message.Length > 0)
                {
                    DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                    sessionKeys.Message = string.Empty;
                }

                // }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }






        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Tithing/") + "Tithing_org_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                //if (isOriginal)
                //    img = string.Format("~/WF/UploadData/Tithing/Tithing_{0}.png", contactsId.ToString());
                //else
                    img = string.Format("~/WF/UploadData/Tithing/Tithing_org_{0}.png", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/Tithing_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }



        protected void btnSaveAndEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var month_year_expiry = ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                var tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).FirstOrDefault();
                var retval = QuickPayBAL.TithingCardConnectPay(txtCardName.Text.Trim(), sessionKeys.PortfolioID, tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                    ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), TxtCSV.Text.Trim(), ddlCurrencyCard.SelectedValue, sessionKeys.UID, Convert.ToDouble(txtOtherAmount.Text.Trim()),"","","",0,0);

                sessionKeys.Message = "Approved";
                Response.Redirect(Request.RawUrl, false);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {

        }


     



        


             protected void btnFindOutMoreNewVan_Click(object sender, EventArgs e)
        {

            


        }





        protected void btnFindOutMoreMicrophone_Click(object sender, EventArgs e)
        {

            



        }


        protected void listamount_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {

                string v = e.CommandArgument.ToString();

                txtOtherAmount.Text = e.CommandArgument.ToString().Trim();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ListFaithGiving_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                
                string v = e.CommandArgument.ToString();

                Response.Redirect("~/App/FaithGivingDetails.aspx?eid="+v);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


    }






}