using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;


/// <summary>
/// Summary description for IncidentExcelUpload
/// </summary>
namespace Incidents.DAL
{
    public class IncidentExcelUpload
    {
       public static void Incident_InsertExcelData(int PortfolioID,int ContractorID,string ProjectCategoryMaster ,
string ProjectCategory,DateTime DateLogged,bool SLAMet,string IncidentType,string Status,string RequesterName,
string RequesterEmail,string PriorityLevel,string Subject,string Details,string Resolution,DateTime InHandTime,
DateTime ClosedTime, bool OutOfHours, bool Callout, string Area, string Custom3, bool InHandSLAMet)
       {
           SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "incident_InsertByExcelRecord",
            new SqlParameter("@PortfolioID",PortfolioID),
            new SqlParameter("@ContractorID",ContractorID),
            new SqlParameter("@ProjectCategoryMaster",ProjectCategoryMaster),
            new SqlParameter("@ProjectCategory",ProjectCategory),
            new SqlParameter("@DateLogged",DateLogged),
            new SqlParameter("@SLAMet",SLAMet),
            new SqlParameter("@IncidentType",IncidentType),
            new SqlParameter("@Status",Status),
            new SqlParameter("@RequesterName",RequesterName),
            new SqlParameter("@RequesterEmail",RequesterEmail),
            new SqlParameter("@PriorityLevel",PriorityLevel),
            new SqlParameter("@Subject",Subject),
            new SqlParameter("@Details",Details),
            new SqlParameter("@Resolution",Resolution),
            new SqlParameter("@InHandTime",InHandTime),
            new SqlParameter("@ClosedTime",ClosedTime),
            new SqlParameter("@OutOfHours",OutOfHours),
            new SqlParameter("@Callout",Callout),
            new SqlParameter("@Area",Area),
            new SqlParameter("@Custom3",Custom3),
            new SqlParameter("@InHandSLAMet", InHandSLAMet));
       }

       public static void Incident_InsertIpatchandMovesData(int PortfolioID, int ContractorID, string ProjectCategoryMaster,
string ProjectCategory, DateTime DateLogged, string IncidentType, string Status, string RequesterName,
string RequesterEmail, string RequesterDepartmentID, string Subject, string Details, string Custom1, string Custom2, string Custom3
           , string Custom4, string Custom5, string Custom6, string Custom7, string Custom8, string Custom9, string Custom10
           , string Custom11, string Custom12, string Custom13, string Custom14, string Custom15, string Custom16)
       {
           SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "IncidentInsertion_IpatchandMoves",
            new SqlParameter("@PortfolioID", PortfolioID),
            new SqlParameter("@ContractorID", ContractorID),
            new SqlParameter("@ProjectCategoryMaster", ProjectCategoryMaster),
            new SqlParameter("@ProjectCategory", ProjectCategory),
            new SqlParameter("@DateLogged", DateLogged),
            new SqlParameter("@IncidentType", IncidentType),
            new SqlParameter("@Status", Status),
            new SqlParameter("@RequesterName", RequesterName),
            new SqlParameter("@RequesterEmail", RequesterEmail),
            new SqlParameter("@RequesterDepartmentID ", RequesterDepartmentID),
            new SqlParameter("@Subject", Subject),
            new SqlParameter("@Details", Details),
           
            new SqlParameter("@Custom1", Custom1),
            new SqlParameter("@Custom2", Custom2),
            new SqlParameter("@Custom3", Custom3),
            new SqlParameter("@Custom4", Custom4),
            new SqlParameter("@Custom5", Custom5),
            new SqlParameter("@Custom6", Custom6),
            new SqlParameter("@Custom7", Custom7),
            new SqlParameter("@Custom8", Custom8),
            new SqlParameter("@Custom9", Custom9),
            new SqlParameter("@Custom10", Custom10),
            new SqlParameter("@Custom11", Custom11),
            new SqlParameter("@Custom12", Custom12),
            new SqlParameter("@Custom13", Custom13),
            new SqlParameter("@Custom14", Custom14),
            new SqlParameter("@Custom15", Custom15),
            new SqlParameter("@Custom16", Custom16));
       }
    }
}
