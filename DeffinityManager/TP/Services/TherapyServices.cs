using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AjaxControlToolkit;
using Therapy.DAL;
using Therapy.Entity;
using UserMgt.DAL;
using UserMgt.Entity;
using TP.BLL;

using ProjectMgt.DAL;

/// <summary>
/// Summary description for TherapyServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TherapyServices : System.Web.Services.WebService {
   
    [WebMethod]
    public CascadingDropDownNameValue[] GetUser(string knownCategoryValues, string category)
    {
        using (UserDataContext db = new UserDataContext())
        {
          
            var result = (from p in db.Contractors
                          where p.Status.ToLower() == "active" && p.SID !=-99 && p.SID!=7 && p.SID!=10 && p.SID!=8  orderby p.ContractorName
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.ContractorName }).ToArray();
            return result;
        }

    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetTrial(string knownCategoryValues, string category)
    {
        using (TherapyDataContext db = new TherapyDataContext())
        {
            var result = (from p in db.TrialConfigurations  orderby p.Name
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;
        }

    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetTrialByStatus(string knownCategoryValues, string category)
    {
        using (TherapyDataContext db = new TherapyDataContext())
        {
            var result = (from p in db.TrialConfigurations where p.Status==true
                          orderby p.Name
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
            return result;
        }

    }
    [WebMethod]
    public CascadingDropDownNameValue[] GetTrialByUser(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        string userId = (_catgoryValue[1]);
        using (TherapyDataContext db = new TherapyDataContext())
        {
            var result = (from p in db.TrialConfigurations
                          join t in db.PatientTrialsAndTreatments on p.ID equals t.TrialID
                          where p.Status == true && t.UserID == int.Parse(userId) && t.TypeofTreatment == "PrimaryTreatment"
                          select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).Distinct().OrderBy(p => p.name).ToArray();
            return result;
        }

    }
    [WebMethod]
    public string GetMethodOfPrimaryTreatment(string userId, string trialId)
    {
        using (TherapyDataContext db = new TherapyDataContext())
        {
            var treatments = (from p in db.PatientTrialsAndTreatments
                              join o in db.Treatments
                                  on p.TreatmentID equals o.ID
                              where p.UserID == int.Parse(userId) && p.TrialID == int.Parse(trialId) && p.TypeofTreatment == "PrimaryTreatment"
                              orderby o.Name
                              select new { o.Name }).ToList();
            string result = string.Empty;
            foreach (var item in treatments)
            {
                result = result + item.Name + ",";
            }
            return result.TrimEnd(',');

        }
    }
    [WebMethod]
    public string GetMethodOfSecondaryTreatment(string userId, string trialId)
    {
        using (TherapyDataContext db = new TherapyDataContext())
        {
            var treatments = (from p in db.PatientTrialsAndTreatments
                              join o in db.SecondaryTreatments
                                  on p.TreatmentID equals o.ID
                              where p.UserID == int.Parse(userId) && p.TrialID == int.Parse(trialId) && p.TypeofTreatment == "SecondaryTreatment" orderby o.Name
                              select new { o.Name }).ToList();
            string result = string.Empty;
            foreach (var item in treatments)
            {
                result = result + item.Name + ",";
            }
            return result.TrimEnd(',');

        }
    }
    [WebMethod]
    public TherapyPatientDetail GetDetailsByTherapyId(string therapyId)
    {
        using (TherapyDataContext db = new TherapyDataContext())
        {
            TherapyPatientDetail therapyPatientDetail = db.TherapyPatientDetails.Where(p => p.ID ==int.Parse(therapyId)).Select(p => p).FirstOrDefault();

            return therapyPatientDetail;

        }
    }
    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] GetKeyMarkerByTrialId(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        string TrialId = (_catgoryValue[1]);

        using (TherapyDataContext pd = new TherapyDataContext())
        {
            var result = (from k in pd.KeyMarkers
                          orderby k.Name
                          where k.TrialID == int.Parse(TrialId)
                          select new CascadingDropDownNameValue { value = k.ID.ToString(), name = k.Name }).ToArray();
            return result;
        }
    }

   
    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] GetSubjectiveQuestionsByTrialId(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        string TrialId = (_catgoryValue[1]);

        using (TherapyDataContext td = new TherapyDataContext())
        {
            var result = (from s in td.SubjectiveQuestions
                          orderby s.Name
                          where s.TrialID == int.Parse(TrialId)
                          select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.Name }).ToArray();
            return result;
        }
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] GetTreatmentByTrialId(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        string TrialId = (_catgoryValue[1]);

        using (TherapyDataContext td = new TherapyDataContext())
        {
            var result = (from s in td.Treatments
                          orderby s.Name
                          where s.TrialID == int.Parse(TrialId)
                          select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.Name }).ToArray();
            return result;
        }
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] GetSecondaryTreatmentByTrialId(string knownCategoryValues, string category)
    {
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        string TrialId = (_catgoryValue[1]);

        using (TherapyDataContext td = new TherapyDataContext())
        {
            var result = (from s in td.SecondaryTreatments
                          orderby s.Name
                          where s.TrialID == int.Parse(TrialId)
                          select new CascadingDropDownNameValue { value = s.ID.ToString(), name = s.Name }).ToArray();
            return result;
        }
    }

   
    //[WebMethod]
    //[System.Web.Script.Services.ScriptMethod]
    //public CascadingDropDownNameValue[] GetTrialByPatient(string knownCategoryValues, string category)
    //{
    //    string[] _catgoryValue = knownCategoryValues.Split(':', ';');
    //    string userId = (_catgoryValue[1]);

    //    using (TherapyDataContext db = new TherapyDataContext())
    //    {
    //        var result = (from p in db.TrialConfigurations where  p.==
    //                      orderby p.Name
    //                      where p.PortfolioID == int.Parse(companyId)
    //                      select new CascadingDropDownNameValue { value = p.ID.ToString(), name = p.Name }).ToArray();
    //        return result;
    //    }
    //}
   
}
