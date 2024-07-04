using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using System.Web.UI.HtmlControls;

public partial class DC_controls_DeliveryHistory : System.Web.UI.UserControl
{
    public int CallID
    {
        get;
        set;
    }
    //private static int callid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["callid"] != null)
            {
                hid.Value = Request.QueryString["callid"].ToString();
                DisplayHistory(int.Parse(hid.Value));
            }

        }
    }
    public void DisplayHistory(int cid)
    {
        try
        {
            hid.Value = cid.ToString();
            DC.SRV.WebService ws = new DC.SRV.WebService();
            List<History> hList = ws.BindDeliveryHistory(cid);
            //callid = cid;
            var groupedNewsList = from e in hList
                                  group e by e.ModifiedDate
                                      into g
                                      select new
                                      {
                                          ModifiedDate = g.Key,
                                          Events = g.ToList()
                                      };
            var x = groupedNewsList.Distinct().OrderBy(o => o.ModifiedDate).ToList();

            dlstHistory.DataSource = groupedNewsList;
            dlstHistory.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void dlstHistory_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType != ListItemType.Header) && (e.Item.ItemType != ListItemType.Footer))
            {
               
                dynamic data = e.Item.DataItem;
                List<History> hList = data.Events;
                Label lbldate = (Label)e.Item.FindControl("lbldate");

                HiddenField h_callid = (HiddenField)e.Item.FindControl("h_callid");
                HiddenField h_date = (HiddenField)e.Item.FindControl("h_date");
                CheckBox chk_visibility = (CheckBox)e.Item.FindControl("chk_visibility");
                //hide check box if the user type is customer
                if (sessionKeys.SID == 7)
                    chk_visibility.Visible = false;

                PlaceHolder placeholder1 = (PlaceHolder)e.Item.FindControl("placeholder1");
                PlaceHolder placeholder2 = (PlaceHolder)e.Item.FindControl("placeholder2");
                Label lblmby = (Label)e.Item.FindControl("lblmby");
                HtmlTableRow trdata = (HtmlTableRow)e.Item.FindControl("trdata");
                HtmlTableCell cDate = (HtmlTableCell)e.Item.FindControl("cDate");
                HtmlTableCell cBy = (HtmlTableCell)e.Item.FindControl("cBy");
                HtmlTableCell mDate = (HtmlTableCell)e.Item.FindControl("mDate");
                HtmlTableCell mBy = (HtmlTableCell)e.Item.FindControl("mBy");

                string date = data.ModifiedDate.ToString(Deffinity.systemdefaults.GetFullDateTimeformat());
                lbldate.Text = date.Remove(19);

                h_callid.Value = CallID.ToString();
                h_date.Value = data.ModifiedDate.ToString(Deffinity.systemdefaults.GetFullDateTimeformat());
                foreach (History h in hList)
                {
                    int cid;
                    if (int.Parse(hid.Value) != 0)
                        cid = int.Parse(hid.Value);
                    else
                        cid = int.Parse(hid.Value);
                    CallDetailsJournal cdj = CallDetailsJournalBAL.SelectByDate(cid);
                    if (cdj.ModifiedDate.Value.ToString(Deffinity.systemdefaults.GetFullDateTimeformat()) == date)
                    {
                        cDate.Visible = true;
                        cBy.Visible = true;
                        mDate.Visible = false;
                        mBy.Visible = false;
                    }
                    else
                    {
                        cDate.Visible = false;
                        cBy.Visible = false;
                        mDate.Visible = true;
                        mBy.Visible = true;
                    }
                    Label lblfieldname = new Label();
                    Label lblfieldvalue = new Label();
                    lblfieldname.Text = string.Format("{0}<br />", h.FieldName != null ? h.FieldName.ToString() : string.Empty);
                    lblfieldvalue.Text = string.Format("{0}<br />",  h.FieldValue != null ? h.FieldValue.ToString() : string.Empty);
                    lblmby.Text =  h.ModifiedBy != null ? h.ModifiedBy.ToString() : string.Empty;
                    chk_visibility.Checked = h.VisibleToCustomer.HasValue ? h.VisibleToCustomer.Value:false;
                    placeholder1.Controls.Add(lblfieldname);
                    placeholder2.Controls.Add(lblfieldvalue);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void chk_visibility_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox cb = (CheckBox)sender;
            DataListItem item = (DataListItem)cb.NamingContainer;

            HiddenField h_callid = (HiddenField)item.FindControl("h_callid");
            HiddenField h_date = (HiddenField)item.FindControl("h_date");

            DC.SRV.WebService dw = new DC.SRV.WebService();
            dw.Delivery_CustomerVisiblityUpdate(int.Parse(hid.Value), Convert.ToDateTime(h_date.Value), cb.Checked);
            lblmsg_history.Text = "Updated Successfully";
            DisplayHistory(int.Parse(hid.Value));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
}