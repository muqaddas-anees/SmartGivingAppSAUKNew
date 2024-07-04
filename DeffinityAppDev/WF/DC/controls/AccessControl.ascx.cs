using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;
using DC.BAL;
using DC.DAL;
using System.Web.Services;
using System.Web.Script.Services;

public partial class DC_controls_AccessControl : System.Web.UI.UserControl
{

   private static int cid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString["callidedit"] != null) || (Request.QueryString["callid"] != null))
            {
               
                GetRecords();
            }
        }

    }

   
    private void AddRecord()
    {
        try
        {
            
            CallDetail cd = new CallDetail();
            cd.SiteID = int.Parse(ddlsite.SelectedValue);
            cd.RequestTypeID = int.Parse(ddlRtype.SelectedValue);
            cd.StatusID = int.Parse(ddlstatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlRcmpy.SelectedValue);
            cd.RequesterID = int.Parse(ddlRname.SelectedValue);
            cd.LoggedBy = sessionKeys.UID;
            cd.LoggedDate = DateTime.Now;
            CallDetailsBAL.AddCallDetails(cd);
            
            AccessControl ac = new AccessControl();
            cid =cd.ID;
            ac.CallID = cd.ID;
            ac.RequestedDate = Convert.ToDateTime(txtRdate.Text);
            ac.NumberOfDays = int.Parse(txtNodays.Text.Trim());
            ac.AreaID = int.Parse(ddlarea.SelectedValue);
            ac.PurposeOfVisitID = int.Parse(ddlvstngpurp.SelectedValue);
            //ac.DeliveryNumber = txtdelno.Text.Trim();
            ac.Notes = txtnotes.Text.Trim();
            AccessControlBAL.AccessControlDetails_Insert(ac);
            Response.Redirect("AccessControlList.aspx", false);


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void GetRecords()
    {
        try
        {

            if (Request.QueryString["callidedit"] != null)
            {
                CallDetail cd = CallDetailsBAL.SelectbyId(int.Parse(Request.QueryString["callidedit"]));
                AccessControl ac = AccessControlBAL.AccessControlDetails_selectByCallID(int.Parse(Request.QueryString["callidedit"]));
                ccdCompny.SelectedValue = Convert.ToString(cd.CompanyID);
                //ccdSite.DataBind();
                ccdSite.SelectedValue = Convert.ToString(cd.SiteID);
                //ccdreqname.DataBind();
                ccdreqname.SelectedValue = Convert.ToString(cd.RequesterID);
                // ccdarea.DataBind();
                ccdarea.SelectedValue = Convert.ToString(ac.AreaID);

                ccdType.SelectedValue = Convert.ToString(cd.RequestTypeID);
                //ccdStatus.DataBind();
                ccdStatus.SelectedValue = Convert.ToString(cd.StatusID);

                ccdvp.SelectedValue = Convert.ToString(ac.PurposeOfVisitID);

                txtRdate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToString(ac.RequestedDate).Replace("00:00:00", " "));
                txtNodays.Text = Convert.ToString(ac.NumberOfDays);

                //txtdelno.Text = ac.DeliveryNumber;
                txtnotes.Text = ac.Notes;

                

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void UpdateRecord()  
    {
        try
        {
            CallDetail cd = CallDetailsBAL.SelectbyId(int.Parse(Request.QueryString["callidedit"]));
            cd.SiteID = int.Parse(ddlsite.SelectedValue);
            cd.RequestTypeID = int.Parse(ddlRtype.SelectedValue);
            cd.StatusID = int.Parse(ddlstatus.SelectedValue);
            cd.CompanyID = int.Parse(ddlRcmpy.SelectedValue);
            cd.RequesterID = int.Parse(ddlRname.SelectedValue);
            //cd.LoggedDate = DateTime.Now;
            cd.LoggedBy = sessionKeys.UID;
            CallDetailsBAL.CallDetailsUpdate(cd);

            AccessControl ac = AccessControlBAL.AccessControlDetails_selectByID(int.Parse(Request.QueryString["callidedit"]));
            ac.RequestedDate = Convert.ToDateTime(txtRdate.Text);
            ac.NumberOfDays = int.Parse(txtNodays.Text.Trim());
            ac.AreaID = int.Parse(ddlarea.SelectedValue);
            ac.PurposeOfVisitID = int.Parse(ddlvstngpurp.SelectedValue);
            //ac.DeliveryNumber = txtdelno.Text.Trim();
            ac.Notes = txtnotes.Text.Trim();
            AccessControlBAL.AccessControlDetails_update(ac);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        

    }

   
    protected void imgbtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["callidedit"] != null)
            {
                UpdateRecord();
            }
            else
            {
                AddRecord();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    [WebMethod(EnableSession = true)]
    public static object PhotoID(int ID)
    {
        try
        {

            DCDataContext db = new DCDataContext();
            Visitor result = db.Visitors.Where(p => p.ID == ID).FirstOrDefault();
            string check = string.IsNullOrEmpty(result.PhotoID.ToString()) ? "false" : result.PhotoID.ToString();
            bool photo = bool.Parse(check);

            if (photo == true)
            {
                photo = false;
            }
            else
            {
                photo = true;
            }

            result.PhotoID = photo;
            db.SubmitChanges();
            return new { Result = "OK", Options = result };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(EnableSession = true)]
    public static object Get(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
    {
        try
        {
           
            DCDataContext db = new DCDataContext();
            List<Visitor> flsList = VisitorsBAL.Visitors_selectAll();


            var result = flsList.Skip(jtStartIndex).Take(jtPageSize).ToList();
            return new { Result = "OK", Records = result, TotalRecordCount = flsList.Count() };
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }

    [WebMethod(EnableSession = true)]
    public static object Create(Visitor record)
    {
        
        DCDataContext db = new DCDataContext();
        Visitor v = new Visitor();

        v.CallID = cid;
        v.Company = record.Company;
        v.EmailAddress = record.EmailAddress;
        v.Name = record.Name;
        v.PhoneNumber = record.PhoneNumber;
        db.Visitors.InsertOnSubmit(v);
        db.SubmitChanges();
        return new { Result = "OK", Record = v };

    }

    [WebMethod(EnableSession = true)]
    public static object Update(Visitor record)
    {
        
        DCDataContext db = new DCDataContext();
        Visitor v = db.Visitors.Where(p => p.ID == record.ID).SingleOrDefault();
        v.Company = record.Company;
        v.EmailAddress = record.EmailAddress;
        v.Name = record.Name;
        v.PhotoID = record.PhotoID;
        v.ArriveStatus = record.ArriveStatus;
        v.PhoneNumber = record.PhoneNumber;
        db.SubmitChanges();
        return new { Result = "OK" };
    }

    [WebMethod(EnableSession = true)]
    public static object Delete(int ID)
    {
        DCDataContext db = new DCDataContext();
        var result = (from p in db.Visitors
                      where p.ID == ID
                      select p).FirstOrDefault();

        db.Visitors.DeleteOnSubmit(result);
        db.SubmitChanges();
        return new { Result = "OK" };

    }

    [WebMethod(EnableSession = true)]
    public static object Status(int ID)
    {
        try
        {
            DCDataContext db = new DCDataContext();
            Visitor result = db.Visitors.Where(p => p.ID == ID).FirstOrDefault();
            result.ArriveStatus = true;
            db.SubmitChanges();

            return "";
        }
        catch (Exception ex)
        {
            return new { Result = "ERROR", Message = ex.Message };
        }
    }
}