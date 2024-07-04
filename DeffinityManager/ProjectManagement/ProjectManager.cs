using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using ProjectMgt.DAL;
using System.Linq;
using System.Net;
/// <summary>
/// Summary description for ProjectManager
/// </summary>
namespace Deffinity.ProjectMangers
{
    public class ProjectManager
    {
        /// <summary>
        /// Returns one project data
        /// <param name="projectreference"></param>
        /// </summary>
        public static DataTable ProjectSelect(int projectreference)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_OpsViewProject", new SqlParameter("@ProjectReference", projectreference)).Tables[0]; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectReference"></param>
        /// <param name="PValue"></param>
        /// <param name="type">sell - 1/buy -2</param>
        public static void UpdateProjectValues(int ProjectReference,double Value,int type)
        {
            if(type ==1)
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "Update Projects set BudgetaryCost=@ProjectValue where ProjectReference =@ProjectReference", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@ProjectValue", Value));
            else
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "Update Projects set BuyingPrice=@ProjectBuyingPrice where ProjectReference =@ProjectReference", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@ProjectBuyingPrice", Value));
        }
        public static DataTable GetQABasedOnPortfolio(int PortfolioID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_PortfolioQAMembers", new SqlParameter("@PortfolioID", PortfolioID)).Tables[0];
        }
        public static void ProjectJournalInsert(int ProjectReference, int userID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_InsertprojectsJournal", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@UpDatedBy", userID));
        }
        public static SqlDataReader GetProjectDetails(int ProjectReference)
        {
            return SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetProjectDetails", new SqlParameter("@ProjectReference", ProjectReference));
        }
        public static SqlDataReader GetRiskItemDetails(int RiskItemID)
        {
            return SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "DN_GetRiskDetails", new SqlParameter("@RiskItemID", RiskItemID));
        }
        public static SqlDataReader GetContactDetails(int PortfolioID, int CustomerID)
        {
            return SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_SelectContact", new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@CustomerID", CustomerID));
        }
        public static string RAGImage(string status)
        {
            string returnColour = "";
            if (status != null)
            {
                switch (status.ToUpper())
                {
                    case "RED":
                    case "r":
                        returnColour = @"~\.\images\indcate_red.png";
                        break;
                    case "GREEN":
                    case "g":
                        returnColour = @"~\.\images\indcate_green.png";
                        break;
                    case "AMBER":
                    case "a":
                        returnColour = @"~\.\images\indcate_yellow.png";
                        break;
                    default:
                        returnColour = @"~\.\images\AmberFace.bmp";
                        break;
                }

            }
            return returnColour;
        }

        #region Project Overview page methods

        public static bool CheckMaxLimit()
        {
            bool retval = true;
            SqlParameter outval = new SqlParameter("@OutVal",DbType.Boolean);
            outval.Direction=ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_CheckBuildProject", outval);
            retval = (outval.Value.ToString() == "1") ? true : false;
            return retval;
        }

        public static DataTable BaseCurrency()
        {
            string key = CacheNames.DefaultNames.BaseCurrency.ToString();
            if (BaseCache.Cache_Select(key) == null)
            {
                BaseCache.Cache_Insert(key, SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_SelectCurrency").Tables[0]);
            }
            
            return BaseCache.Cache_Select(key) as DataTable;
        }

        public static DataTable ProjectStatus()
        {
            string key = CacheNames.DefaultNames.ProjectStatus.ToString();
            if (BaseCache.Cache_Select(key) == null)
            {
                BaseCache.Cache_Insert(key, SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_SelectProjectStatus").Tables[0]);
            }

            return BaseCache.Cache_Select(key) as DataTable;
        }
        
        #endregion

        #region Update status to Ready to Invoice Mail
        //project status 9 is Ready to Invoice Mail

        public static void UpdateStatusTOReadytoInvoice(int ProjectReference)
        { 
            UpdateProjectStatus(ProjectReference, 9);
        }
        private static void UpdateProjectStatus(int ProjectReference, int ProjectStatusID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update projects set ProjectStatusID=@ProjectStatusID where ProjectReference=@ProjectReference", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@ProjectStatusID", ProjectStatusID));
            //projectTaskDataContext pd = new projectTaskDataContext();
            //var Precord = from p in pd.ProjectDetails
            //               where p.ProjectReference == ProjectReference
            //               select p;
            //var Precord1 = (from p in pd.ProjectDetails
            //               where p.ProjectReference == ProjectReference
            //               select p).First();

           // Precord.ProjectStatusID = ProjectStatusID;
            //pd.ProjectDetails.Attach(Precord);
           // pd.SubmitChanges();
        }
        #endregion

        #region Project staus - Ready to Invoice Mail
        public static string Get_ReadyToInvoiceMailContent(int ProjectReference)
        {
            string MailContent = string.Empty;

            string Weburl = System.Configuration.ConfigurationManager.AppSettings["Weburl"].ToString();
            string style = Deffinity.MailFormat.MailCss("Ready to Invoice");
            string ProjectRef = string.Empty;
            string ProjectTitle = string.Empty;
            string ProjectOwner = string.Empty;
            
            projectTaskDataContext pd = new projectTaskDataContext();
            var Precord = (from p in pd.ProjectDetails
                       where p.ProjectReference == ProjectReference
                       select p ).First();
            ProjectRef = Precord.ProjectReferenceWithPrefix;
            ProjectTitle = Precord.ProjectTitle;
            ProjectOwner = Precord.OwnerName;
            
            Email mail = new Email();
            //BookingsEntity booking = Bookings.Bookings_GetEmployees(Convert.ToInt32(trid.Value));
            //string url = "/training/coursefeedback.aspx?ID=" + booking.ID.ToString();
            string body = string.Empty;
            body = string.Format(@"
                                <body>
                                <table align='center' width='600' style='border:1px solid #8595a6; margin-top:10px;' cellspacing='0' cellpadding='0'>
                                  <tr>
                                    <td height='30' valign='top' class='style1'><img src='{0}'  style='float:left'/> 
                                    <table width='300' border='0' cellspacing='0' cellpadding='0' align='right' style='float:right'>
                                  <tr>
                                    <td class='hdr1'>Ready to Invoice</td>
                                  </tr>
                                </table> 
                                   </td>
                                  </tr>
                                  <tr>
                                    <td height='9' class='style1' ><img src='{1}' /></td>
                                  </tr>
                                <tr>
                                <td>
                                Dear <b>Finance</b>
                                </td>
                                </tr>
                             <tr><td>Project <b>{2} {3}</b> is now ready for invoicing. Please check the Finance section to determine details of the invoice that needs to be raised. The project manager for this project is: <b>{4}</b>.</td></tr>
                            <tr><td>Thank you</td></tr>
                                                            </table>
                                </body>
                                </html>",System.Configuration.ConfigurationManager.AppSettings["Weburl"] + "/MailLogo/deffinity_emailer_logo.png",
                                        System.Configuration.ConfigurationManager.AppSettings["Weburl"] + "/MailLogo/emailer_bg_bott.gif", 
                                        ProjectRef,ProjectTitle,ProjectOwner);

            

            string htmlBody = style + body;

            return htmlBody;
        }
        #endregion
    }
}
