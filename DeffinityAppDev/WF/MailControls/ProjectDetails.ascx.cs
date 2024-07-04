using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Deffinity.ProjectMangers;
public partial class MailControls_ProjectDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void SetData(int ProjectReference)
    {
        GetProjectDetils(ProjectReference);
    }
   
    private void GetProjectDetils(int projectreference)
    {
        SqlDataReader dr = ProjectManager.GetProjectDetails(projectreference);
        while (dr.Read())
        {
            
            lblcity.InnerText = dr["CityName"].ToString();
            lblCountry.InnerText = dr["CountryName"].ToString();
            lblDescription.InnerText = dr["ProjectDescription"].ToString();
            lblEmail.InnerText = dr["OwnerEmail"].ToString();
            lblEndDate.InnerText = DateTime.Parse(string.Format(Deffinity.systemdefaults.GetStringDateformat(),dr["ProjectEndDate"].ToString())).ToShortDateString();
            lblProjectTitle.InnerHtml = dr["ProjectTitle"].ToString();
            lblPortfolio.InnerText = dr["PortfolioName"].ToString();
            lblProjectStatus.InnerHtml=dr["ProjectStatusName"].ToString();
            lblPrograme.InnerText = dr["ProgrammeName"].ToString();
            lblProjectOwner.InnerText = dr["OwnerName"].ToString();
            lblSite.InnerText = dr["SiteName"].ToString();
            lblStartDate.InnerText = DateTime.Parse(string.Format(Deffinity.systemdefaults.GetStringDateformat(), dr["StartDate"].ToString())).ToShortDateString();
            lblCategory.InnerText =  dr["CategoryName"].ToString();
            lblPriority.InnerText = dr["Priority"].ToString();
            lblSubProgram.InnerText = dr["SubProgrammeName"].ToString();
            ImgRag.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["Weburl"] + RAGImage(dr["RAGstatus"].ToString());
            lblPrimaryQA.InnerText = dr["PrimaryQAName"].ToString();
            
        }
        dr.Close();
        dr.Dispose();
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
                    returnColour = @"/images/indcate_red.png";
                    break;
                case "GREEN":
                case "g":
                    returnColour = @"/images/indcate_green.png";
                    break;
                case "AMBER":
                case "a":
                    returnColour = @"/images/indcate_yellow.png";
                    break;
                default:
                    returnColour = @"/images/AmberFace.bmp";
                    break;
            }

        }
        return returnColour;
    }
#region properties
   
    
#endregion

}
