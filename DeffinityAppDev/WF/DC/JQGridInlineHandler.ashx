<%@ WebHandler Language="C#" CodeBehind="JQGridInlineHandler.ashx.cs" Class="JQGridInlineHandler" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using DC.DAL;
using DC.Entity;
using DC.BAL;
using System.Web.SessionState;
public class JQGridInlineHandler : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");

            string strResponse = string.Empty;
            using (DCDataContext db = new DCDataContext())
            {
                string callid = string.Empty;
                string accessNo = string.Empty;
                if (HttpContext.Current.Request.QueryString["callid"] != null)
                    callid = HttpContext.Current.Request.QueryString["callid"].ToString();
                if (HttpContext.Current.Request.QueryString["accessno"] != null)
                    accessNo = HttpContext.Current.Request.QueryString["accessno"].ToString();
                if (strOperation == null)
                {
                    var jsonSerializer = new JavaScriptSerializer();
                    List<VisitorDetails> visitors = new List<VisitorDetails>();
                    visitors = VisitorsBAL.BindVisitors(accessNo).Where(p => p.CallID == Convert.ToInt32(callid)).OrderByDescending(p=>p.ID).ToList();
                    context.Response.Write(jsonSerializer.Serialize(visitors));
                }
                else
                {
                    string strOut = string.Empty;
                    AddEdit(forms, context.Session["UID"].ToString(),callid,accessNo, out strOut);
                    context.Response.Write(strOut);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private void AddEdit(NameValueCollection forms, string userId, string callid,string accessNo, out string strResponse)
    {

        try
        {
            string strOperation = forms.Get("oper");
            if (strOperation == "add")
            {
                

                DCDataContext db = new DCDataContext();
                Visitor v = new Visitor();
                VisitorsJournal vj = new VisitorsJournal();
                
                v.CallID = Convert.ToInt32(callid);
                v.Name = forms.Get("Name").ToString();
                v.EmailAddress = forms.Get("EmailAddress").ToString();
                v.Company = forms.Get("Company").ToString();
                v.ArriveStatus = true;
                v.DepartStatus = false;
                if (forms.Get("PhotoID") != null)
                    v.PhotoID = Convert.ToBoolean(forms.Get("PhotoID").ToString() == "Yes" ? true : false);
                else
                    v.PhotoID = false;
                v.AccessNo = accessNo;
                v.NoShow = false;
                //v.Day = day;
                v.PhoneNumber = forms.Get("PhoneNumber").ToString();
                vj.ArriveStatus = true;
                vj.DepartStatus = false;
                db.Visitors.InsertOnSubmit(v);
                vj.CallID = Convert.ToInt32(callid);
                db.SubmitChanges();
                vj.Name = v.Name;
                vj.Company = v.Company;
                vj.EmailAddress = v.EmailAddress;
                if (sessionKeys.SID == 7)
                    vj.VisibleToCustomer = true;
                else
                    vj.VisibleToCustomer = false;
                vj.PhoneNumber = v.PhoneNumber;
                if (v.ArrivalDate != null)
                    vj.ArriveDate = v.ArrivalDate;
                if (v.DepatureDate != null)
                    vj.DepartDate = v.DepatureDate;
                vj.VisitorID = v.ID;
                vj.PhotoID = v.PhotoID;
                vj.AccessNo = accessNo;
                vj.ModifiedBy = int.Parse(userId);
                vj.ModifiedDate = DateTime.Now;
                vj.NoShow = false;
                db.VisitorsJournals.InsertOnSubmit(vj);
                db.SubmitChanges();

               // strResponse = "Sucess ";

            }
            else if (strOperation == "edit")
            {

                string id = forms.Get("visitorId").ToString();
                try
                {

                    using (DCDataContext db = new DCDataContext())
                    {
                        Visitor v = db.Visitors.Where(p => p.ID == int.Parse(id)).SingleOrDefault();
                       
                        VisitorsJournal vj = new VisitorsJournal();
                        if (v.ArrivalDate != null)
                            vj.ArriveDate = v.ArrivalDate;
                        if (v.DepatureDate != null)
                            vj.DepartDate = v.DepatureDate;
                        vj.ArriveStatus = v.ArriveStatus;
                        vj.DepartStatus = v.DepartStatus;
                        //vj.PhotoID = v.PhotoID;
                        if (forms.Get("PhotoID") != null)
                            vj.PhotoID = Convert.ToBoolean(forms.Get("PhotoID").ToString() == "Yes" ? true : false);
                        else
                            vj.PhotoID = v.PhotoID;  
                        vj.AccessNo = v.AccessNo;
                        vj.ModifiedBy =int.Parse(userId);
                        vj.ModifiedDate = DateTime.Now;
                        v.Company = forms.Get("Company").ToString();
                        v.EmailAddress = forms.Get("EmailAddress").ToString();
                        v.Name = forms.Get("Name").ToString();
                        v.PhoneNumber = forms.Get("PhoneNumber").ToString();
                        vj.VisitorID = v.ID;
                        vj.CallID = v.CallID;
                        if (sessionKeys.SID == 7)
                            vj.VisibleToCustomer = true;
                        else
                            vj.VisibleToCustomer = false;
                        vj.Name = v.Name;
                        vj.Company = v.Company;
                        vj.EmailAddress = v.EmailAddress;
                        vj.PhoneNumber = v.PhoneNumber;
                        vj.NoShow = v.NoShow;
                        db.VisitorsJournals.InsertOnSubmit(vj);
                        db.SubmitChanges();
                    }
                   
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
      
               // strResponse = "Sucess";
            }
            else if (strOperation == "del")
            {

                string id = forms.Get("visitorID").ToString();
                try
                {
                    using (DCDataContext db = new DCDataContext())
                    {
                        var result = (from p in db.Visitors
                                      where p.ID == int.Parse(id)
                                      select p).FirstOrDefault();

                        db.Visitors.DeleteOnSubmit(result);
                        db.SubmitChanges();
                    }
                   // strResponse = "Sucess";
                }
                catch (Exception ex)
                {
                   
                }
               
            }
            

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        strResponse = "Sucess";
    } 


}