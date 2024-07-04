using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.VT.Entity;
using Deffinity.VT.DAL;
using Deffinity.VT.BAL;
using VT.DAL;
using VT.Entity;


/// <summary>
/// Summary description for ResourcePlanner_Service
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class ResourcePlanner_Service : System.Web.Services.WebService {

    public ResourcePlanner_Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod (EnableSession = true)]
    public string Update_TeamMemberShift(string MemberID, string ShiftID, string FromDate, string SiteID, string TeamType, string Notes, string Todate, string ID,string TypeOfUpdate)
    {
        try
        {
            DateTime f_date = Convert.ToDateTime(string.Format(Deffinity.systemdefaults.GetStringDateformat(), FromDate));
            DateTime t_date;
            if (TypeOfUpdate.Contains("1"))
            {
                t_date = Convert.ToDateTime(string.Format(Deffinity.systemdefaults.GetStringDateformat(), Todate));
            }
            else
            {
                t_date = Convert.ToDateTime(string.Format(Deffinity.systemdefaults.GetStringDateformat(), Todate)).AddDays(1);
            }
           
            ResourcePlanner RP = new ResourcePlanner();
            RP.Membershift_InsertOrUpdate(int.Parse(MemberID), int.Parse(ShiftID), f_date, int.Parse(SiteID),
                int.Parse(TeamType), Notes, t_date, int.Parse(ID));
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            
        }
        return "1";
    }
    [WebMethod]
    public string Delete_TeamMemberShift(string ID)
    {
        try
        {
            ResourcePlanner.Membershift_Delete(int.Parse(ID));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
        return "1";
    }

    [WebMethod]
    public string GetServerResponse(string callerName)
    {
        if (callerName == string.Empty)
            throw new Exception("Web Service Exception: invalid argument");

        return string.Format("Service responded to {0} at {1}", callerName, DateTime.Now.ToString());
    }

    #region VT
    [WebMethod(EnableSession=true)]
    public string Update_Request(string requestid, string absenttype, string fromdate, string todate, string notes, string fromdateperiod, string todateperiod)
    {
        string ret_str = string.Empty;
        VT.Entity.LeaveRequest request = new VT.Entity.LeaveRequest();

        LeaveRequestHelper requesthelper = new LeaveRequestHelper();
        try
        {
            DateTime from_date = Convert.ToDateTime(string.Format(Deffinity.systemdefaults.GetStringDateformat(), fromdate));
            DateTime to_date = Convert.ToDateTime(string.Format(Deffinity.systemdefaults.GetStringDateformat(), todate));
           
             
            bool isLeaveRequestChanged = false;
            DateTime toDate = to_date.AddDays(1);
            Deffinity.VT.Entity.LeaveRequest leaveRequestPast = VTLeaveRequestBAL.GetLeaveRequestByID(int.Parse(requestid));
            if (leaveRequestPast != null)
            {
                if (leaveRequestPast.AbsenseType != Convert.ToInt32(absenttype))
                    isLeaveRequestChanged = true;
                else if (leaveRequestPast.FromDate != from_date)
                    isLeaveRequestChanged = true;
                else if (leaveRequestPast.ToDate != toDate)
                    isLeaveRequestChanged = true;
                else if (leaveRequestPast.FromDatePeriod != float.Parse(fromdateperiod))
                    isLeaveRequestChanged = true;
                else if (leaveRequestPast.ToDatePeriod != float.Parse(todateperiod))
                    isLeaveRequestChanged = true;
                else if (leaveRequestPast.RequestNotes != notes)
                    isLeaveRequestChanged = true;
            }





           
            if (isLeaveRequestChanged)
            {

                request.ID = Convert.ToInt32(requestid);
                request.AbsenseType = Convert.ToInt32(absenttype);
                request.FromDate = from_date;
                request.ToDate = to_date;
                request.FromDatePeriod = float.Parse(fromdateperiod);
                request.ToDatePeriod = float.Parse(todateperiod);
                request.RequestNotes = notes;
                object result = requesthelper.Update(request);

                //journal Insert
                Deffinity.VT.Entity.LeaveRequest leaveRequestCurrent = VTLeaveRequestBAL.GetLeaveRequestByID(int.Parse(requestid));

                Deffinity.VT.Entity.LeaveRequestJournal journal = new Deffinity.VT.Entity.LeaveRequestJournal();
                journal.AbsenseType = Convert.ToInt32(absenttype);
                journal.ApprovalStatus = leaveRequestCurrent.ApprovalStatus;
                journal.FromDate = from_date;
                journal.ToDate = toDate;
                journal.FromDatePeriod = float.Parse(fromdateperiod);
                journal.ToDatePeriod = float.Parse(todateperiod);
                journal.RequestNotes = notes;
                journal.Days = leaveRequestCurrent.Days;
                journal.UserID = leaveRequestCurrent.UserID;
                journal.RequestID = int.Parse(requestid);
                journal.ModifiedBy = sessionKeys.UID;
                journal.ModifiedDate = DateTime.Now;
                VTLeaveRequestBAL.InsertJournal(journal);


                if (Convert.ToInt16(result) == 1)
                {
                    ret_str = "Please check date range contains saturday and sunday";
                }
              
                
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
        return ret_str;
    }
    [WebMethod(EnableSession = true)]
    public string Delete_Request(string requestid)
    {
        int? status = 0;
        LeaveRequestHelper requesthelper = new LeaveRequestHelper();
        try
        {
            status = requesthelper.Delete(int.Parse(requestid), sessionKeys.SID);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
        return status.ToString();
    }

    

    #endregion

    
}

