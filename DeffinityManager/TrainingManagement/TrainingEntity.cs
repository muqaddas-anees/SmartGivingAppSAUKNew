using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TrainingEntity
/// </summary>
namespace Deffinity.TrainingEntity
{
    #region Employee
    public class EmployeeEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Classification { get; set; }
        public string ClassificationName { get; set; }
        public string CourseName { get; set; }
        public string StatusName { get; set; }
        public DateTime BookingDate { get; set; }
        public int FeedBackMail { get; set; }
        public EmployeeEntity() { }

        public EmployeeEntity(int id, string name)
        {
            ID = id;
            Name = name;        
        }
    }
    #endregion

    #region Department
    public class DepartmentEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    #endregion

    #region Category
    public class CategoryEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    #endregion

    #region Course
    public class CourseEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string Venue { get; set; }
        public double Rate { get; set; }
        public string Duration { get; set; }
        public int TrainingTypeID { get; set; }
        public string TrainingTypeName { get; set; }
        public int RequirementID { get; set; }
        public string RequirementName { get; set; }
        public string CourseDesc { get; set; }
    }
    #endregion

    #region DepartmentToCourse
    public class DepartmentToCourseEntity
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int MinRequired { get; set; }
        public int Target { get; set; }
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    #endregion

    #region Bookings
    public class BookingsEntity
    { 
        public int ID { get; set; }
        public DateTime DateofCourse {set;get;}
        public int Employee { get; set; }
        public string EmployeeName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
	    public int CourseID { get; set; }
        public string CourseTitle { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public int CheckedBy { get; set; }
        public string CheckedByName { get; set; }
        public DateTime CheckedDate {set;get;}
        public DateTime RequiredByDate {set;get;}
        public string Instructor { get; set; }
        public string CourseVenue { get; set; }
        public int NotifyDaysPrior { get; set; }
        public double CostofCourse { get; set; }
        public string NotifyUser { get; set; }
        public string NotifyUserName { get; set; }
        public string FeedBack { get; set; }
        public int FileID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double Expenses { get; set; }
        public double Penalties { get; set; }
        public int DurationInDays { get; set; }
        public int Outcome { get; set; }
        public string OutcomeName { get; set; }
        public string Comments { get; set; }
        public string StatusColor { get; set; }
        public int AreaID { get; set; }
        public string FileName { get; set; }
        public double Budget { get; set; }
        public int Classification { get; set; }
        public string ClassificationName { get; set; }
        public string EmailAddress { get; set; }
        public int BookingID { get; set; }
        public int FeedBackMail { get; set; }
        public int CFeedBackID { get; set; }
        //Below field for course re-occurrence.
        public int CourseReoccurs { get; set; }
        public int ReFrequencey { get; set; }
        public string Day { get; set; }
        public DateTime UntilDate { get; set; }
        public string ReFrequenceyName { get; set; }
        public string ContactNumber { get; set; }

    }


    #endregion

    #region  Training Type
    public class TrainingTypeEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    #endregion

    #region  Training Area
    public class AreaEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        
    }
    #endregion


    #region StatusCount
    public class StatusEntity
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public int Count { get; set; }
        public int Employee { get; set; }
        public string StatusColor { get; set; }
        public StatusEntity() { }
        public StatusEntity(int statusid, string statusname,string statuscolor, int count,int employee)
        { StatusID = statusid; StatusName = statusname; StatusColor = statuscolor; Count = count; Employee = employee; }
    }
    #endregion

    #region OutCome
    public class OutcomeEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    #endregion
    #region Contractors
    public class ContratorsEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public string ContactNumber { get; set; }
        public int SID { get; set; }
        public string EmailAddress { get; set; }
    }

    #endregion
    #region UploadFile
    public class UploadFileEntity
    {
      public  int ID { get; set; }
      public string FilePath { get; set; }
      public int BookingID { get; set; }
      public string FileName { get; set; }
    }

    #endregion
    #region Classification
    public class ClassificationEntity
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public string classficationID { get; set; }
    }
    #endregion  

    #region Feedback
    public class FeedbackEntity
    {
        public int ID { get; set; }
        public string programmeTitle{get;set;}
        public string FacilitatorsName{get;set;}
        public DateTime Date{get;set;}
        public string Location {get;set;}
        public string Name{get;set;}
        public string JobTitle{get;set;}
        public string Department {get;set;}
        public string peComments1 {get;set;}
        public string peComments2 {get;set;}
        public string peComments3 {get;set;}
        public int peRating {get;set;}
        public string peRatingComments {get;set;}
        public int teKnowledgeSub {get;set;}
        public int teObviousPrep {get;set;}
        public int teStyleDelivery {get;set;}
        public int teResponsiveness {get;set;}
        public int teLearningClimate {get;set;}
        public string teOtherComments{get;set;}
        public int teLength {get;set;}
        public int tePacing {get;set;}
        public string ptComments1{get;set;}
        public string ptComments2{get;set;}
        public int BookingID { get; set; }
    }
    #endregion

    #region ChartEntity
    public class ChartEntity
    {
        public string Month { get; set; }
        public int Penalities { get; set; }
        public int Expenses { get; set; }
        public int CostOfCourse { get; set; }
    }
    #endregion
    #region CourseFeedBack

    public class CourseFeedBackEntity
    {
        public int ID { get; set; }
        public string CourseTitle {get;set;}
        public  string OrganisedBy {get;set;}
        public DateTime DatesOfAttendance { get; set; }
        public string Name {get;set;}
        public string Objectives{get;set;}
        public string Useful{get;set;}
        public string LearningPoints{get;set;}
        public string Recommend{get;set;}
        public string Objects{get;set;}
        public int BookingID{get;set;}
        public string  ActionPlan {get;set;}
        public string SupportRequired{get;set;}
        public string TimeScale { get; set; }
        public string JobTitle { get; set; }
        public int ActionID { get; set; } 
    }
    #endregion

    #region ActionPlan
    public class ActionPlanEntity
    {
        public int ID { get; set; }
        public string ActionPlan { get; set; }
        public string SupportRequired { get; set; }
        public string TimeScale { get; set; }
        public int FeedBackID { get; set; }
        public int BookingID { get; set; }
    }
    #endregion

    #region Notification
    public class NotificationEntity
    {
        public int ID { get; set; }
        public int AdminID { get; set; }
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public string ContactNumber { get; set; }
    }
    #endregion
    #region CourseReOccurrence

    public class CourseReOccurrence
    {
        public int ID { get; set; }
        public int CourseReOccurres { get; set; }
        public int ReOccuerFrequencey { get; set; }
        public DateTime UntilDate { get; set; }
        public int WeekDay { get; set; }
        public int BookingID { get; set; }
        public int BoolVal { get; set; }
        public int CourseID { get; set; }
        public DateTime BookingDate { get; set; }
        public int DepartmentID { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public string EmployeeName { get; set; }
        public int Month { get; set; }
        public string CourseName { get; set; }


    }

    #endregion
}



