using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin.Maintenance
{
    public partial class Calender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    BindCustomFields();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public class months
        {
            public int month { set; get; }
            public string monthname { set; get; }
        }
        public class mplanClass
        {
            public string Equipmnet { set; get; }

            public string Material { set; get; }
            public string January { set; get; }
            public string February { set; get; }
            public string March { set; get; }
            public string April { set; get; }
            public string May { set; get; }
            public string June { set; get; }
            public string July { set; get; }
            public string August { set; get; }
            public string September { set; get; }
            public string October { set; get; }
            public string November { set; get; }
            public string December { set; get; }
        }
        private void BindCustomFields()
        {
            try
            {

                List<months> months = new List<months>();
                months.Add(new months() {month=1, monthname= "January" });
                months.Add(new months() { month = 2, monthname = "February" });
                months.Add(new months() { month = 3, monthname = "March" });
                months.Add(new months() { month = 4, monthname = "April" });
                months.Add(new months() { month = 5, monthname = "May" });
                months.Add(new months() { month = 6, monthname = "June" });
                months.Add(new months() { month = 7, monthname = "July" });
                months.Add(new months() { month = 8, monthname = "August" });
                months.Add(new months() { month = 9, monthname = "September" });
                months.Add(new months() { month = 10, monthname = "October" });
                months.Add(new months() { month = 11, monthname = "November" });
                months.Add(new months() { month = 12, monthname = "December" });

                var rlist = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentEquipmentBAL.v_PartnerMaintenacePlanEquipmentEquipmentBAL_SelectAll().Where(o => o.MaintenacePlanID == Convert.ToInt32(QueryStringValues.PlanID)).ToList();
                var eqids = rlist.Select(o => o.TypeOfEquipmentID).ToList();
                var mlist = PortfolioMgt.BAL.PartnerMaintenacePlanEquipmentMaterialBAL.PartnerMaintenacePlanEquipmentMaterialBAL_SelectAll().Where(o => eqids.Contains(o.EquipmentID)).ToList();
                var slist = PortfolioMgt.BAL.PartnerSubCategoryBAL.PartnerSubCategoryBAL_SelectAll().Where(o => eqids.Contains(o.PartnerCategoryID)).ToList();

                List<mplanClass> mpList = new List<mplanClass>();

                foreach(var r in rlist)
                {
                    foreach (var m in slist.Where(o => o.PartnerCategoryID == r.TypeOfEquipmentID).ToList())
                    {
                        var mDate = new DateTime(2020, months.Where(o => o.monthname == r.StartMonth).FirstOrDefault().month, 1,0,0,0);
                        var times = r.TimePerYear;
                        var month = r.StartMonth;
                        var mp = new mplanClass();
                        mp.Equipmnet = r.TypeOfEquipmentName + " " + r.ModelNumber + " " + r.SerialNumber ;
                        //mp.Equipmnet = r.TypeOfEquipmentName + " " + r.ModelNumber + " " + r.SerialNumber + " " + "Starting in " + r.StartMonth;
                        mp.Material = m.SubCategoryName;
                        if (month == "January")
                        {
                            mp.January = GetBlock(r);
                            if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }
                        if (month == "February")
                        {
                            mp.February = GetBlock(r);
                            if (r.ChecklistMinutes > 0)
                                month = months.Where(o => o.month == mDate.AddMonths(12 / Convert.ToInt32(times)).Month).FirstOrDefault().monthname;
                        }
                        if (month == "March")
                        {
                            mp.March = GetBlock(r); if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }
                        if (month == "April")
                        {
                            mp.April = GetBlock(r); if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }
                        if (month == "May")
                        {
                            mp.May = GetBlock(r); if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }
                        if (month == "June")
                        {
                            mp.June = GetBlock(r); if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }
                        if (month == "July")
                        {
                            mp.July = GetBlock(r); if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }
                        if (month == "August")
                        {
                            mp.August = GetBlock(r); if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }
                        if (month == "September")
                        {
                            mp.September = GetBlock(r); if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }
                        if (month == "October")
                        {
                            mp.October = GetBlock(r); if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }
                        if (month == "November")
                        {
                            mp.November = GetBlock(r); if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }
                        if (month == "December")
                        {
                            mp.December = GetBlock(r); if (r.ChecklistMinutes > 0)
                            {
                                mDate = mDate.AddMonths(12 / Convert.ToInt32(times));
                                month = months.Where(o => o.month == mDate.Month).FirstOrDefault().monthname;
                            }
                        }

                        mpList.Add(mp);
                        
                        //for (int i = 0; i <= times; i++)
                        //{
                            
                        //}


                    }

                }
                
                list_Customfields.DataSource = mpList;
                list_Customfields.DataBind();
                
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private static string GetBlock(V_PartnerMaintenacePlanEquipment r)
        {
            return r.ChecklistMinutes > 0 ? string.Format("<div style='border-style: groove'> <div style='background-color: #40bbea;color: white;text-align: center;'>{0}</div><div style='text-align: center'>{1}</div></div>", r.ChecklistName, r.ChecklistMinutes) : "";
        }

        string LastBoardingPoint = string.Empty;
        public string AddGroupingRowIfBoardingHasChanged()
        {
            string values = string.Empty;
            string CurrentBoardingPoint = Eval("Equipmnet").ToString();
           
            if (CurrentBoardingPoint.Length == 0)
            {
                CurrentBoardingPoint = "Unknown";
            }
            else
                if (!LastBoardingPoint.Equals(CurrentBoardingPoint))
            {
                LastBoardingPoint = CurrentBoardingPoint;
                values = String.Format("<tr class='groupheader'><td colspan='13'> {0}  </td></tr>", CurrentBoardingPoint);
            }
            else
            {
                values = string.Empty;
            }
            return values;
        }
        protected void list_Customfields_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                //if (e.Item.ItemType == ListViewItemType.DataItem)
                //{
                //    Label lbleqid = (Label)e.Item.FindControl("lbleqid");
                //    GridView gvMaterial = (GridView)e.Item.FindControl("gridMaterials");

                //    if (gvMaterial != null)
                //    {
                //        BindMaterialGrid(gvMaterial, Convert.ToInt32(lbleqid.Text));
                //    }

                //    //Label lbl = (Label)e.Item.FindControl("lblIsApplied");
                //    var d = e.Item.DataItem as dynamic;

                //    HyperLink hlink = (HyperLink)e.Item.FindControl("hlink");
                //    if (hlink != null)
                //    {
                //        var eqID = Convert.ToInt32(lbleqid.Text);
                //        hlink.NavigateUrl = string.Format("~/WF/CustomerAdmin/Maintenance/MaintenancePlanItems.aspx?ContactID={0}&addressid={1}&planid={2}&eqid={3}", QueryStringValues.ContactID, QueryStringValues.AddressID, hplanid.Value, eqID);

                //    }


                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void list_Customfields_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            list_Customfields.EditIndex = -1;
            //BindCustomFields();
        }
        protected void list_Customfields_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            list_Customfields.EditIndex = e.NewEditIndex;
            //BindCustomFields();
            BindCustomFields();
        }
        protected void list_Customfields_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                //if (e.CommandName == "UpdateItem")
                //{
                //    cb = new CustomFieldsBAL();
                //    CustomField dc = cb.CustomFields_SelectByID(Convert.ToInt32(e.CommandArgument.ToString()));
                //    TextBox txteDescription = (TextBox)e.Item.FindControl("txtLable");
                //    TextBox txtValue = (TextBox)e.Item.FindControl("txtValue");
                //    DropDownList ddltype = (DropDownList)e.Item.FindControl("ddlType");
                //    dc.FieldLable = txteDescription.Text.Trim();
                //    //dc.Cost = Convert.ToDouble(txteCost.Text.Trim());
                //    dc.FieldType = ddltype.SelectedValue;
                //    dc.FieldValue = txtValue.Text.Trim();
                //    cb.CustomFields_update(dc);
                //    lblMsg.Text = "Updated sucessfully";
                //    list_Customfields.EditIndex = -1;
                //    //BindCustomFields();
                //}
                //if (e.CommandName == "Item")
                //{
                //    var optionid = Convert.ToInt32(e.CommandArgument.ToString());
                //    Response.Redirect(string.Format("/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionid, "quote"));
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}