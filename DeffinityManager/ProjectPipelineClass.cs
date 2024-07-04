using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Summary description for ProjectPipelineClass
/// </summary>
public class ProjectPipelineClass
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    DbCommand cmd;
    DataTable dt;
    DataSet ds;
	public ProjectPipelineClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertPipeProjectPlan(string[] ProjectPipeline,string[] ProjectPipelineNew, out int ProjectPlanID) 
    {
        
        int getresult=0;
        ProjectPlanID = 0;
              
            try
            {
                cmd = db.GetStoredProcCommand("DN_InsertProjectPlan");
                db.AddInParameter(cmd, "@ProjectTitle", DbType.String, ProjectPipeline[0]);
                db.AddInParameter(cmd, "@ProjectDescription", DbType.String, ProjectPipeline[1]);
                db.AddInParameter(cmd, "@StratergicObjective", DbType.String, ProjectPipeline[2]);
                db.AddInParameter(cmd, "@Justification", DbType.String, ProjectPipeline[3]);
                db.AddInParameter(cmd, "@BusinessBenefit", DbType.String, ProjectPipeline[4]);
                db.AddInParameter(cmd, "@CountryID", DbType.Int32,Convert.ToInt32(ProjectPipeline[5]));
                db.AddInParameter(cmd, "@CityID",DbType.Int32,Convert.ToInt32(ProjectPipeline[6]));
                db.AddInParameter(cmd, "@SiteID", DbType.Int32, Convert.ToInt32(ProjectPipeline[7]));
                db.AddInParameter(cmd, "@Building", DbType.String, ProjectPipeline[8]);
                db.AddInParameter(cmd, "@Department", DbType.String, ProjectPipeline[9]);
                db.AddInParameter(cmd, "@Owner", DbType.Int32, Convert.ToInt32(ProjectPipeline[10]));
                db.AddInParameter(cmd, "@BudgetaryCost", DbType.String, ProjectPipeline[11]);
                db.AddInParameter(cmd, "@CostCenter", DbType.String, ProjectPipeline[12]);
                db.AddInParameter(cmd, "@ExpectedStartDate", DbType.DateTime,Convert.ToDateTime(ProjectPipeline[13]));
                db.AddInParameter(cmd, "@ExpectedEndDate", DbType.DateTime, Convert.ToDateTime(ProjectPipeline[14]));
                db.AddInParameter(cmd, "@FinacialStatusID", DbType.Int32, Convert.ToInt32(ProjectPipeline[15]));
                db.AddInParameter(cmd, "@StatusID", DbType.Int32, Convert.ToInt32(ProjectPipeline[16]));
                db.AddInParameter(cmd, "@ContributionID", DbType.Int32, Convert.ToInt32(ProjectPipeline[17]));
                db.AddInParameter(cmd, "@RiskScore", DbType.Int32, string.IsNullOrEmpty(ProjectPipeline[18]) ? 0: Convert.ToInt32(ProjectPipeline[18]));
                db.AddInParameter(cmd, "@FinancialScore", DbType.Int32, string.IsNullOrEmpty(ProjectPipeline[19]) ? 0 : Convert.ToInt32(ProjectPipeline[19]));
                db.AddInParameter(cmd, "@BusinessScore", DbType.Int32, string.IsNullOrEmpty(ProjectPipeline[20]) ? 0 : Convert.ToInt32(ProjectPipeline[20]));
                db.AddInParameter(cmd, "@RiskToBussiness", DbType.String, ProjectPipeline[21]);
                db.AddInParameter(cmd, "@RAGstatus", DbType.String, ProjectPipeline[22]);
                db.AddInParameter(cmd, "@Priority", DbType.String, ProjectPipeline[23]);
                //new fields
                db.AddInParameter(cmd, "@ProgrammeID", DbType.Int32, ProjectPipelineNew[0]);
                db.AddInParameter(cmd, "@PortfolioID", DbType.Int32, ProjectPipelineNew[1]);
                db.AddInParameter(cmd, "@SubProgrammeID", DbType.Int32, ProjectPipelineNew[2]);
                db.AddOutParameter(cmd, "@RPPid", DbType.Int32,10);
                db.AddOutParameter(cmd, "@getresult", DbType.Int32, 10);
               
                db.ExecuteNonQuery(cmd);
                //(int)db.GetParameterValue(cmd, "@OutIdt");
                getresult = (int)db.GetParameterValue(cmd, "@getresult");
                if (getresult == 0)
                {
                    ProjectPlanID = (int)db.GetParameterValue(cmd, "@RPPid");
                }

                cmd.Dispose();
                

               }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message, "PPClass - InsertPPPlan");
            }

       
        return getresult;
    }
    public void UpdatePipeProjectPlan(string[] ProjectPipeline,string[] ProjectPipelineNew, string stemp)
    {
            try
            {
                cmd = db.GetStoredProcCommand("DN_UpdateProjectPlan");
                db.AddInParameter(cmd, "@ProjectTitle", DbType.String, ProjectPipeline[0]);
                db.AddInParameter(cmd, "@ProjectDescription", DbType.String, ProjectPipeline[1]);
                db.AddInParameter(cmd, "@StratergicObjective", DbType.String, ProjectPipeline[2]);
                db.AddInParameter(cmd, "@Justification", DbType.String, ProjectPipeline[3]);
                db.AddInParameter(cmd, "@BusinessBenefit", DbType.String, ProjectPipeline[4]);
                db.AddInParameter(cmd, "@CountryID", DbType.Int32, Convert.ToInt32(ProjectPipeline[5]));
                db.AddInParameter(cmd, "@CityID", DbType.Int32, Convert.ToInt32(ProjectPipeline[6]));
                db.AddInParameter(cmd, "@SiteID", DbType.Int32, Convert.ToInt32(ProjectPipeline[7]));
                db.AddInParameter(cmd, "@Building", DbType.String, ProjectPipeline[8]);
                db.AddInParameter(cmd, "@Department", DbType.String, ProjectPipeline[9]);
                db.AddInParameter(cmd, "@Owner", DbType.Int32, Convert.ToInt32(ProjectPipeline[10]));
                db.AddInParameter(cmd, "@BudgetaryCost", DbType.String, ProjectPipeline[11]);
                db.AddInParameter(cmd, "@CostCenter", DbType.String, ProjectPipeline[12]);
                db.AddInParameter(cmd, "@ExpectedStartDate", DbType.DateTime, Convert.ToDateTime(ProjectPipeline[13]));
                db.AddInParameter(cmd, "@ExpectedEndDate", DbType.DateTime, Convert.ToDateTime(ProjectPipeline[14]));
                db.AddInParameter(cmd, "@FinacialStatusID", DbType.Int32, Convert.ToInt32(ProjectPipeline[15]));
                db.AddInParameter(cmd, "@StatusID", DbType.Int32, Convert.ToInt32(ProjectPipeline[16]));
                db.AddInParameter(cmd, "@ContributionID", DbType.Int32, Convert.ToInt32(ProjectPipeline[17]));
                db.AddInParameter(cmd, "@RiskScore", DbType.Int32, string.IsNullOrEmpty(ProjectPipeline[18]) ? 0 : Convert.ToInt32(ProjectPipeline[18]));
                db.AddInParameter(cmd, "@FinancialScore", DbType.Int32, string.IsNullOrEmpty(ProjectPipeline[19]) ? 0 : Convert.ToInt32(ProjectPipeline[19]));
                db.AddInParameter(cmd, "@BusinessScore", DbType.Int32, string.IsNullOrEmpty(ProjectPipeline[20]) ? 0 : Convert.ToInt32(ProjectPipeline[20]));
                db.AddInParameter(cmd, "@RiskToBussiness", DbType.String, ProjectPipeline[21]);
                db.AddInParameter(cmd, "@RAGstatus", DbType.String, ProjectPipeline[22]);
                db.AddInParameter(cmd, "@Priority", DbType.String, ProjectPipeline[23]);
                db.AddInParameter(cmd, "@ProjectPlanID", DbType.Int32, Convert.ToInt32( stemp));                             
                db.AddInParameter(cmd, "@ProgrammeID", DbType.Int32, ProjectPipelineNew[0]);
                db.AddInParameter(cmd, "@PortfolioID", DbType.Int32, ProjectPipelineNew[1]);
                db.AddInParameter(cmd, "@SubProgrammeID", DbType.Int32, ProjectPipelineNew[2]);

                db.ExecuteNonQuery(cmd);
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message, "PPClass - UpdatePPPlan");
            }

      
        
    }    
    public void PlanScopeInsertandUpdate(string[] PPScope, string PPId)
    {
        
        try
        {
            cmd = db.GetStoredProcCommand("Definnity_UpdateProjectPlanScope");
            db.AddInParameter(cmd, "@ProjectPlanID", DbType.Int32, Convert.ToInt32(PPId));
            db.AddInParameter(cmd, "@Assumptions", DbType.String, PPScope[0]);
            db.AddInParameter(cmd, "@Contraints", DbType.String, PPScope[1]);
            db.AddInParameter(cmd, "@Approvals", DbType.String, PPScope[2]);
            db.AddInParameter(cmd, "@Dependencies", DbType.String, PPScope[3]);
            db.AddInParameter(cmd, "@RelatedDocumentLinks", DbType.String, PPScope[4]);
            db.ExecuteNonQuery(cmd);
            cmd.Dispose();
                        
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "PPClass - PPScopeInsert and Update");
        }
      
    }
    public void PPFundingInsertandUpdate(string[] PPFunding, string PPId)
    {
        
        try
        {
            cmd = db.GetStoredProcCommand("Definnity_UpdateProjectPlanFunding");
            db.AddInParameter(cmd, "@ProjectPlanID", DbType.Int32, Convert.ToInt32(PPId));
            db.AddInParameter(cmd, "@FundingRequired", DbType.Single,Convert.ToSingle( PPFunding[0]));
            db.AddInParameter(cmd, "@CriteriaForFunding", DbType.String, PPFunding[1]);
            db.AddInParameter(cmd, "@AvailabilityExternalFunding", DbType.String, PPFunding[2]);
            db.ExecuteNonQuery(cmd);
            cmd.Dispose();
            
            
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "PPClass - Funding(Insert and Update)");
        }
 
    }
    public void BizReqInserandUpdate(string[] PPBizReq, string PPId)
    {
        try
        {
            cmd = db.GetStoredProcCommand("Definnity_UpdateProjectPlanBusReq");
            db.AddInParameter(cmd, "@ProjectPlanID", DbType.Int32, Convert.ToInt32(PPId));
            db.AddInParameter(cmd, "@GeneralBusinessRequirements", DbType.String, PPBizReq[0]);
            db.AddInParameter(cmd, "@ReportingRequirements", DbType.String, PPBizReq[1]);
            db.AddInParameter(cmd, "@OperatingRequirements", DbType.String, PPBizReq[2]);
            db.AddInParameter(cmd, "@DocumentationAndTraining", DbType.String, PPBizReq[3]);
            db.AddInParameter(cmd, "@BusinessContinuity", DbType.String, PPBizReq[4]);
            db.AddInParameter(cmd, "@Testing", DbType.String, PPBizReq[5]);
            db.ExecuteNonQuery(cmd);
            cmd.Dispose();

                       
        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "PPClass -Funding(Insert and update)");
        }
        
    }
    //read the data from Project proposal table 
   // 1.[ProjectPlanID], 2.[ProjectTitle], 3.[ProjectDescription], 4.[StratergicObjective],5.[Justification],6.[BusinessBenefit],7.[CountryID]
   //8.[CityID],9.[SiteID],10.[Building],11.[Department],12.[BudgetaryCost],13.[CostCenter],14.[ExpectedStartDate],15.[ExpectedEndDate]
   //16.[FinacialStatusID],17.[StatusID]18.[ContributionID],19.[RiskScore],20.[FinancialScore],21.[BusinessScore],22.[RiskToBussiness]
   //23.[OwnerID],24.[Priority],25.[RAGstatus],26.[Assumptions],27.[Contraints],28.[Approvals],29.[Dependencies]30.[RelatedDocumentLinks]
   //31.[FundingRequired],32.[CriteriaForFunding],33.[AvailabilityExternalFunding],34.[GeneralBusinessRequirements],35.[ReportingRequirements]
    //36.[OperatingRequirements],37.[DocumentationAndTraining],38.[BusinessContinuity],39.[Testing],40.ProgrammeID,41.PortfolioID,42.SubProgrammeID
    public List<object> GetProjectProjectPlan(int ProjectPlanID)
    { 
        List<object> list1= new List<object>();
        try
        {
            cmd = db.GetStoredProcCommand("DN_SelectProjectPlan");
            db.AddInParameter(cmd,"@ProjectPlanID",DbType.Int32,ProjectPlanID);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    for (int cnt = 0; cnt <= dr.FieldCount; cnt++)
                    {
                        list1.Add(dr[cnt]);
                    }
                }
                dr.Close();
            }
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Select from project plan table");
        }
        return list1;
    }

}
