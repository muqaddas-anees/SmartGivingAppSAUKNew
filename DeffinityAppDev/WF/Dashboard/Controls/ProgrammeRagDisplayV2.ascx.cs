using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using ProjectMgt.Entity;
using ProjectMgt.DAL;
using UserMgt.DAL;



public partial class controls_ProgrammeRagDisplayV2 : System.Web.UI.UserControl
{
   
    public projectTaskDataContext pdt;
    public List<ProjectDetails> pd;
    public List<ProjectIssue> p_issues;
    public List<ProjectIssue> p_issues_temp;
    public List<AC2P_Risk> p_risks;
    public List<AC2P_Risk> p_risks_temp;
    public List<ProjectTaskItem> p_milestones;
    public List<ProjectTaskItem> p_milestones_temp;
    public List<ProjectBenefitItem> p_benefit;
    public List<ProjectBenefitItem> p_benefit_temp;
    protected void Page_Load(object sender, EventArgs e)
    {
        int sProgramme = 0;
        int programmeid = 0;
        if (Session["Programme"] != null)
        {
            sProgramme = int.Parse(Session["Programme"].ToString());
            programmeid = int.Parse(Session["Programme"].ToString());
            hid.Value = programmeid.ToString();
        }
        if (!IsPostBack)
        {
            try
            {
                
                BindData();
                //Bind drop down
                BindDefaultDashboardDropdown();
                //set dashboard values
                BindDefaultDashboard();             
                
                LoadControl();
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
        else
        {

            //LoadCollections();
            //LoadControl();
        }
        // if the user is admin should show the link other wise not
        if (sessionKeys.SID != 1)
        {
            CollapsiblePanelExtender1.Enabled = false;
            btn_show.Visible = false;
            divProgramme.Visible = false;
        }

    }

    public void LoadCollections()
    {
        pd = GetProjectDetails();
        p_issues = GetProjectIssues();
        p_milestones = GetProjectMilestone();
        p_risks = GetProjectRisks();
        p_benefit = GetProjectBenefit();
    }
    #region programme
    private int _programmeid;
    public int programmeid
    {
        get { return _programmeid; }
        set { _programmeid = value; }
    }

    #endregion
    public void LoadControl()
    {
        try
        {
            if (Session["Programme"] != null)
            {
                hid.Value = Session["Programme"].ToString();
            }

            if (int.Parse(hid.Value) > 0)
            {
                LoadCollections();
                litDisplay.Text = RetString(GetCollectionByDropdown(ddlVertical.SelectedValue), GetCollectionByDropdown(ddlHorizontal.SelectedValue));

            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    #region Bind dropdowns country and benifit type
    private void BindData()
    {
        try
        {
            projectTaskDataContext type = new projectTaskDataContext();
            UserDataContext uMgt = new UserDataContext();
            var types = from r in type.ProjectBenefitTypes
                        select r;
            if (types != null)
            {
                ddlBenifitType.DataSource = types;
                ddlBenifitType.DataTextField = "Description";
                ddlBenifitType.DataValueField = "ID";
                ddlBenifitType.DataBind();
            }
            ddlBenifitType.Items.Insert(0, new ListItem("Please select...", "0"));

            //var owner = (from pr in type.ProjectDetails
            //             where pr.OwnerID > 0 && pr.OwnerName != string.Empty
            //             select new { ID = pr.OwnerID, Name = pr.OwnerName }).Distinct().OrderBy(p => p.Name);
            //if (owner != null)
            //{
            //    ddlOwner.DataSource = owner;
            //    ddlOwner.DataTextField = "Name";
            //    ddlOwner.DataValueField = "ID";
            //    ddlOwner.DataBind();
            //}
            ddlOwner.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Get Collections
    //Project details
    private List<ProjectDetails> GetProjectDetails()
    {
        pdt = new projectTaskDataContext();

        if (int.Parse(ddlOwner.SelectedValue) > 0)
            return pdt.ProjectDetails.Where(p => p.OwnerGroupID == int.Parse(hid.Value) && p.OwnerID == int.Parse(ddlOwner.SelectedValue) && p.ProjectStatusID==2).ToList();
        else
            return pdt.ProjectDetails.Where(p => p.OwnerGroupID == int.Parse(hid.Value) && p.ProjectStatusID == 2).ToList();
    }
    //Project issues
    private List<ProjectIssue> GetProjectIssues()
    {
        pdt = new projectTaskDataContext();

        var ps = (from pi in pdt.ProjectIssues
                 join p in pdt.ProjectDetails on pi.Projectreference equals p.ProjectReference
                  where p.OwnerGroupID == int.Parse(hid.Value) && pi.IssueSection.ToLower() == "project"
                 select pi).ToList();

        return ps;
    }
    //Project Milestone
    private List<ProjectTaskItem> GetProjectMilestone()
    {
        pdt = new projectTaskDataContext();

        var pm = (from pi in pdt.ProjectTaskItems
                  join p in pdt.ProjectDetails on pi.ProjectReference equals p.ProjectReference
                  where p.OwnerGroupID == int.Parse(hid.Value) && pi.isMilestone== true
                  select pi).ToList();

        return pm;
    }
    //Risks
    private List<AC2P_Risk> GetProjectRisks()
    {
        pdt = new projectTaskDataContext();

        var pm = (from pi in pdt.AC2P_Risks
                  join p in pdt.ProjectDetails on pi.ProjectReference equals p.ProjectReference
                  where p.OwnerGroupID == int.Parse(hid.Value) && pi.ReportStatus != 3
                  select pi).ToList();

        return pm;
    }
    //Benifit
    private List<ProjectBenefitItem> GetProjectBenefit()
    {
        pdt = new projectTaskDataContext();

        var pm = (from pi in pdt.ProjectBenefitItems
                  join pb in pdt.ProjectBenefits on pi.BenefitID equals pb.ID
                  join p in pdt.ProjectDetails on pb.Projectreference equals p.ProjectReference
                  where p.OwnerGroupID == int.Parse(hid.Value) && pb.BenfitID == int.Parse(ddlBenifitType.SelectedValue)
                  select pi).ToList();

        return pm;
    }

    #endregion

    protected void ddlBenifitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadControl();
    }
    protected void ddlOwner_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadControl();
    }

    #region Get Basic data
    //public List<ProjectDetails> getProjectdetails(int programmeid)
    //{
    //    pdt = new projectTaskDataContext();
        
        
    //    return (from pr in pdt.GetTable<ProjectDetails>()
    //           where pr.OwnerGroupID == programmeid select pr).ToList();
    //}
    private Dictionary<int?, string> GetSubProgrammes()
    {
        return (from pr in pd
                where pr.SubProgramme >0
                select new { ID = pr.SubProgramme, Name = pr.SubProgrammeName }).Distinct().OrderBy(p => p.Name).ToDictionary(p => p.ID, p => p.Name);
    }

    private Dictionary<int?, string> GetSites()
    {
        return (from pr in pd
                where pr.SiteID > 0
                select new { ID = pr.SiteID, Name = pr.SiteName }).Distinct().OrderBy(p => p.Name).ToDictionary(p => p.ID, p => p.Name);
    }
    private Dictionary<int?, string> GetCitys()
    {
        return (from pr in pd
                where pr.CityID > 0
                select new { ID = pr.CityID, Name = pr.CityName }).Distinct().OrderBy(p => p.Name).ToDictionary(p => p.ID, p => p.Name);
    }
    private Dictionary<int?, string> GetOwners()
    {
        return (from pr in pd
                where pr.OwnerID > 0
                select new { ID = pr.OwnerID, Name = pr.OwnerName }).Distinct().OrderBy(p => p.Name).ToDictionary(p => p.ID, p => p.Name);
    }
    private Dictionary<int?, string> GetClass()
    {
        return (from pr in pd
                where pr.CustomerID > 0
                select new { ID = pr.CategoryID, Name = pr.CategoryName }).Distinct().OrderBy(p => p.Name).ToDictionary(p => p.ID, p => p.Name);
    }
    private Dictionary<int?, string> GetCountry()
    {
        return (from pr in pd
                where pr.CountryID > 0
                select new { ID = pr.CountryID, Name = pr.CountryName }).Distinct().OrderBy(p => p.Name).ToDictionary(p => p.ID, p => p.Name);
    }



#endregion

    #region Get String
    private bool GetBenefit()
    {
        bool retval = false;
        //double i_red = 0, i_green = 0;
        int benefitid = int.Parse(ddlBenifitType.SelectedValue);

        if (benefitid > 0)
        {
            //var red = (from r in rd where r.BenefitType == benefitid select r.BudgetRed).Sum();
            //var green = (from r in rd where r.BenefitType == benefitid select r.BudgetGreen).Sum();
            //i_red = red == null ? 0 : red;
            //i_green = green == null ? 0 : green;

            //if (i_red > 0 || i_green > 0)
            //{
            retval = true;
            //}

        }

        return retval;
    }
    private string RetString(Dictionary<int?, string> VerticalColumn_data, Dictionary<int?, string> horizentalColumn_data)
    {
        //<label style='background:white'></label>
        //<a href='Reports/ProgrammeDashboardRpt.aspx?programmeid={0}&countryid={1}' target='_blank'></a>
        string checkTableEmpty = "No data exists.";
        string retval = string.Empty;

        string firstRow = string.Empty;
        string secondRow = string.Empty;
        string midlebody = string.Empty;
        bool showBenifit = GetBenefit();
        retval = "<table CellPadding='0' CellSpacing='1' class='table table-small-font table-bordered table-striped dataTable responsive' >";

        firstRow = firstRow + "<tr class='tab_header' style='font-weight:bold;text-align:center;height:30px'>";
        //country column
        firstRow = firstRow + "<td style='color:White'></td><td style='color:White'></td>";
        secondRow = secondRow + "<tr class='tab_header' style='font-weight:bold;text-align:center;height:25px'>";
        //country column
        secondRow = secondRow + "<td style='color:White'></td><td>No. of Projects</td>";

        //var horizentalColumn_data = (from r in rd where r.subprogrammeid > 0 select r.subprogrammename).Distinct();
        //subprogramme column
        foreach (var s in horizentalColumn_data)
        {
            checkTableEmpty = string.Empty;
            if (showBenifit)
                firstRow = firstRow + string.Format("<td colspan='4' style='text-align:center;'>{0}</td>", s.Value.ToString());
            else
                firstRow = firstRow + string.Format("<td colspan='3' style='text-align:center;'>{0}</td>", s.Value.ToString());
            firstRow = firstRow + "<td></td>";
            secondRow = secondRow + "<td style='text-align:center;width:50px'>Risk</td><td style='text-align:center;width:50px'>Milestone</td>";

            if (showBenifit)
                secondRow = secondRow + "<td style='text-align:center;width:50px'>Benefit</td>";
            secondRow = secondRow + "<td style='text-align:center;width:50px'>Issues</td>";
            secondRow = secondRow + "<td style='color:White'></td>";
        }
        if (showBenifit)
            firstRow = firstRow + "<td style='text-align:center;width:50px'>Benefit to Date</td>";
        firstRow = firstRow + "<td style='text-align:center;width:50px'></td>";
        firstRow = firstRow + "</tr>";

        if (showBenifit)
            secondRow = secondRow + "<td></td>";
        secondRow = secondRow + "<td></td>";
        secondRow = secondRow + " </tr>";
        //set default value
        p_issues_temp= null;
        p_milestones_temp = null;
        p_risks_temp = null;
        p_benefit_temp = null;
        foreach (var s1 in VerticalColumn_data)
        {
            //Get the issue collection based on vertical column data
            p_issues_temp = GetIssueCount_vertical(ddlVertical.SelectedValue, int.Parse(s1.Key.Value.ToString()));
            p_risks_temp = GetRisks_vertical(ddlVertical.SelectedValue, int.Parse(s1.Key.Value.ToString()));
            p_milestones_temp = GetMilestone_vertical(ddlVertical.SelectedValue, int.Parse(s1.Key.Value.ToString()));
            p_benefit_temp = GetBenefit_vertical(ddlVertical.SelectedValue, int.Parse(s1.Key.Value.ToString()));
            checkTableEmpty = string.Empty;
            midlebody = midlebody + "<tr style='text-align:center;' class='even_row'>";
            midlebody = midlebody + string.Format("<td><a href='ProjectPipeline.aspx?status=0&Country={1}'>{0}</a></td>", s1.Value.ToString(), s1.Key.Value.ToString());
            midlebody = midlebody + string.Format("<td>{0}</td>", GetProjectCount(ddlVertical.SelectedValue, int.Parse(s1.Key.Value.ToString())));

            foreach (var s2 in horizentalColumn_data)
            {
                //risk
                midlebody = midlebody + string.Format("<td>{0}</td>", RetRiskImgUrl(GetRisk_horzantal(p_risks_temp, ddlHorizontal.SelectedValue, int.Parse(s2.Key.Value.ToString())), int.Parse(s1.Key.Value.ToString()), int.Parse(s2.Key.Value.ToString())));
                //milestone
                midlebody = midlebody + string.Format("<td>{0}</td>", RetMilestoneImgUrl(GetMilestone_horzantal(p_milestones_temp, ddlHorizontal.SelectedValue, int.Parse(s2.Key.Value.ToString())), int.Parse(s1.Key.Value.ToString()), int.Parse(s2.Key.Value.ToString())));
                //benifit check
                if (showBenifit)
                    midlebody = midlebody + string.Format("<td>{0}</td>", RetBenefitImgUrl(GetBenefit_horzantal(p_benefit_temp, ddlHorizontal.SelectedValue, int.Parse(s2.Key.Value.ToString())), int.Parse(s1.Key.Value.ToString()), int.Parse(s2.Key.Value.ToString())));
                //issues
                //midlebody = midlebody + string.Format("<td>{0}</td>", RetIssueurl(GetIssueCount_horzantal(p_issues_temp, ddlHorizontal.SelectedValue, int.Parse(s2.Key.Value.ToString())), int.Parse(s1.Key.Value.ToString()), int.Parse(s2.Key.Value.ToString())));
                midlebody = midlebody + string.Format("<td>{0}</td>", RetIssueurl(GetIssueCount_horzantal(p_issues_temp, ddlHorizontal.SelectedValue, int.Parse(s2.Key.Value.ToString())),int.Parse(s1.Key.Value.ToString()),int.Parse(s2.Key.Value.ToString())));
                midlebody = midlebody + "<td style='color:White'></td>";
               
            }
            if (showBenifit)
                midlebody = midlebody + string.Format("<td>{0}</td>", RetBenefitStr(p_benefit_temp));
             //if (showBenifit)
              //   midlebody = midlebody + RetBenefitStr(p_benefit_temp);

             midlebody = midlebody + string.Format("<td><a href='Reports/ProgrammeDashboardRpt.aspx?programmeid={0}&vsection={1}&vid={2}&hsection={3}&hid={4}' target='_blank'>{5}</a></td>", hid.Value, ddlVertical.SelectedValue.ToLower(),s1.Key.Value.ToString(),string.Empty,0, "<img src='media/ico_report.png' alt='Report' />");

            midlebody = midlebody + "</tr>";
            //clear the issue collection
            p_issues_temp = null;
            p_milestones_temp = null;
            p_risks_temp = null;
            p_benefit_temp = null;
        }
        // var subprogrammeid = (from r in rd where r.subprogrammeid > 0 select r.subprogrammeid).Distinct();
        if (showBenifit)
        {
            p_benefit_temp = null;
            midlebody = midlebody + "<tr class='tab_header' style='font-weight:bold;text-align:center;height:30px'><td><b>Benefit to Date</a></td><td></td>";
            foreach (var s2 in horizentalColumn_data)
            {
                p_benefit_temp = GetBenefit_vertical(ddlHorizontal.SelectedValue, int.Parse(s2.Key.Value.ToString()));
                //var actual_subprogramme = (from r in rd where r.subprogrammeid > 0 && r.subprogrammeid == s2 select r.sum_actual).Sum();
                //var target_subprogramme = (from r in rd where r.subprogrammeid > 0 && r.subprogrammeid == s2 select r.sum_Target).Sum();
                midlebody = midlebody + "<td></td><td></td>";
                if (showBenifit)
                    midlebody = midlebody + string.Format("<td>{0}</td>", RetBenefitStr(p_benefit_temp));
                midlebody = midlebody + "<td></td><td></td>";
            }
            //midlebody = midlebody + "</tr>";
            var actual_total = (from ps in p_benefit
                      join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                      join pr in pd on pb.Projectreference equals pr.ProjectReference
                      where pr.SubProgramme > 0 
                      select ps.Actual).Sum();
            var target_total = (from ps in p_benefit
                      join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                      join pr in pd on pb.Projectreference equals pr.ProjectReference
                      where pr.SubProgramme > 0
                      select ps.Planned).Sum();
             
            //midlebody = midlebody + string.Format("<td><a href='Reports/ProgrammeDashboardRpt.aspx?programmeid={2}&countryid={3}&subprogrammeid={4}' target='_blank'>{0}/{1}</a></td><td></td></tr>", int.Parse(actual_total != null ? actual_total.ToString() : "0"), int.Parse(target_total != null ? target_total.ToString() : "0"), programmeid, 0, 0);
            midlebody = midlebody + string.Format("<td>{0}/{1}</td><td></td></tr>", actual_total.Value, target_total.Value);
            p_benefit_temp = null;
        }
        retval = retval + firstRow + secondRow + midlebody;
        retval = retval + "</table>";

        if (!string.IsNullOrEmpty(checkTableEmpty))
            retval = checkTableEmpty;

        return retval;
    }


    #endregion 
    protected void ddlVertical_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadControl();
    }
    protected void ddlHorizontal_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadControl();
    }

    private Dictionary<int?, string> GetCollectionByDropdown(string setVal)
    {
        Dictionary<int?, string> retval=null ;
        switch (setVal.ToLower())
        {
            case "site":
                retval=GetSites();
                break;
            case "city":
                retval = GetCitys();
                break;
            case "country":
                retval = GetCountry();
                break;
            case "owner":
                retval = GetOwners();
                break;
            case "class":
                retval = GetClass();
                break;
            case "sub_programme":
                retval = GetSubProgrammes();
                break;
        }
        return retval;
    }

    private int GetProjectCount(string setVal,int id)
    {
        int retval = 0;
        switch (setVal.ToLower())
        {
            case "site":
                retval = (from pr in pd
                          where pr.SiteID > 0 && pr.SiteID == id
                          select pr).Count();
                break;
            case "city":
                retval = (from pr in pd
                          where pr.CityID > 0 && pr.CityID == id
                          select pr).Count();
                break;
            case "country":
                retval = (from pr in pd
                          where pr.CountryID > 0 && pr.CountryID == id
                          select pr).Count();
                break;
            case "owner":
                retval = (from pr in pd
                          where pr.OwnerID > 0 && pr.OwnerID == id
                          select pr).Count();
                break;
            case "class":
                retval = (from pr in pd
                          where pr.CategoryID > 0 && pr.CategoryID == id
                          select pr).Count();
                break;
            case "sub_programme":
                retval = (from pr in pd
                          where pr.SubProgramme > 0 && pr.SubProgramme == id
                          select pr).Count();
                break;
        }
        return retval;
    }

    private List<ProjectIssue> GetIssueCount_vertical(string v_section, int id)
    {
        
        List<ProjectIssue> retval=null;
        switch (v_section.ToLower())
        {
            case "site":
                retval = (from ps in p_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.SiteID > 0 && pr.SiteID == id
                          select ps).ToList();
                break;
            case "city":
                retval = (from ps in p_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.CityID > 0 && pr.CityID == id
                          select ps).ToList();
                break;
            case "country":
                retval = (from ps in p_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.CountryID > 0 && pr.CountryID == id
                          select ps).ToList();
                break;
            case "owner":
                retval = (from ps in p_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.OwnerID > 0 && pr.OwnerID == id
                          select ps).ToList();
                break;
            case "class":
                retval = (from ps in p_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.CategoryID > 0 && pr.CategoryID == id
                          select ps).ToList();
                break;
            case "sub_programme":
                retval = (from ps in p_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.SubProgramme > 0 && pr.SubProgramme == id
                          select ps).ToList();
                break;
        }
        return retval;
    }

    private int GetIssueCount_horzantal(List<ProjectIssue> temp_issues, string h_section, int id)
    {
        int retval = 0;
        switch (h_section.ToLower())
        {
            case "site":
                retval = (from ps in temp_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.SiteID > 0 && pr.SiteID == id
                          select ps).Count();
                break;
            case "city":
                retval = (from ps in temp_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.CityID > 0 && pr.CityID == id
                          select ps).Count();
                break;
            case "country":
                retval = (from ps in temp_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.CountryID > 0 && pr.CountryID == id
                          select ps).Count();
                break;
            case "owner":
                retval = (from ps in temp_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.OwnerID > 0 && pr.OwnerID == id
                          select ps).Count();
                break;
            case "class":
                retval = (from ps in temp_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.CategoryID > 0 && pr.CategoryID == id
                          select ps).Count();
                break;
            case "sub_programme":
                retval = (from ps in temp_issues
                          join pr in pd on ps.Projectreference equals pr.ProjectReference
                          where pr.SubProgramme > 0 && pr.SubProgramme == id
                          select ps).Count();
                break;
        }
        return retval;
    }

    private string RetIssueurl(int issueCount, int v_id, int h_id)
    {
        return string.Format("<a href='#' targer='_blank' onclick=window.open('ShowProgrammePopupV2.aspx?programmeid={0}&v_select={1}&v_id={2}&h_select={3}&h_id={4}&Ownerid={5}&benifit={6}&issuecount={7}&type=issues','mywindow','width=700,height=400,scrollbars=1')> {7}</a>", hid.Value, ddlVertical.SelectedValue, v_id, ddlHorizontal.SelectedValue, h_id, ddlOwner.SelectedValue, ddlBenifitType.SelectedValue, issueCount);
    }

    private List<AC2P_Risk> GetRisks_vertical(string v_section, int id)
    {

        List<AC2P_Risk> retval = null;
        switch (v_section.ToLower())
        {
            case "site":
                retval = (from ps in p_risks
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.SiteID > 0 && pr.SiteID == id
                          select ps).ToList();
                break;
            case "city":
                retval = (from ps in p_risks
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CityID > 0 && pr.CityID == id
                          select ps).ToList();
                break;
            case "country":
                retval = (from ps in p_risks
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CountryID > 0 && pr.CountryID == id
                          select ps).ToList();
                break;
            case "owner":
                retval = (from ps in p_risks
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.OwnerID > 0 && pr.OwnerID == id
                          select ps).ToList();
                break;
            case "class":
                retval = (from ps in p_risks
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CategoryID > 0 && pr.CategoryID == id
                          select ps).ToList();
                break;
            case "sub_programme":
                retval = (from ps in p_risks
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.SubProgramme > 0 && pr.SubProgramme == id
                          select ps).ToList();
                break;
        }
        return retval;
    }

    private List<AC2P_Risk> GetRisk_horzantal(List<AC2P_Risk> temp_issues, string h_section, int id)
    {
        List<AC2P_Risk> retval = null;
        switch (h_section.ToLower())
        {
            case "site":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.SiteID > 0 && pr.SiteID == id
                          select ps).ToList();
                break;
            case "city":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CityID > 0 && pr.CityID == id
                          select ps).ToList();
                break;
            case "country":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CountryID > 0 && pr.CountryID == id
                          select ps).ToList();
                break;
            case "owner":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.OwnerID > 0 && pr.OwnerID == id
                          select ps).ToList();
                break;
            case "class":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CategoryID > 0 && pr.CategoryID == id
                          select ps).ToList();
                break;
            case "sub_programme":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.SubProgramme > 0 && pr.SubProgramme == id
                          select ps).ToList();
                break;
        }
        return retval;
    }


    private List<ProjectTaskItem> GetMilestone_vertical(string v_section, int id)
    {

        List<ProjectTaskItem> retval = null;
        switch (v_section.ToLower())
        {
            case "site":
                retval = (from ps in p_milestones
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.SiteID > 0 && pr.SiteID == id
                          select ps).ToList();
                break;
            case "city":
                retval = (from ps in p_milestones
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CityID > 0 && pr.CityID == id
                          select ps).ToList();
                break;
            case "country":
                retval = (from ps in p_milestones
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CountryID > 0 && pr.CountryID == id
                          select ps).ToList();
                break;
            case "owner":
                retval = (from ps in p_milestones
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.OwnerID > 0 && pr.OwnerID == id
                          select ps).ToList();
                break;
            case "class":
                retval = (from ps in p_milestones
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CategoryID > 0 && pr.CategoryID == id
                          select ps).ToList();
                break;
            case "sub_programme":
                retval = (from ps in p_milestones
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.SubProgramme > 0 && pr.SubProgramme == id
                          select ps).ToList();
                break;
        }
        return retval;
    }

    private List<ProjectTaskItem> GetMilestone_horzantal(List<ProjectTaskItem> temp_issues, string h_section, int id)
    {
        List<ProjectTaskItem> retval = null;
        switch (h_section.ToLower())
        {
            case "site":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.SiteID > 0 && pr.SiteID == id
                          select ps).ToList();
                break;
            case "city":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CityID > 0 && pr.CityID == id
                          select ps).ToList();
                break;
            case "country":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CountryID > 0 && pr.CountryID == id
                          select ps).ToList();
                break;
            case "owner":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.OwnerID > 0 && pr.OwnerID == id
                          select ps).ToList();
                break;
            case "class":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.CategoryID > 0 && pr.CategoryID == id
                          select ps).ToList();
                break;
            case "sub_programme":
                retval = (from ps in temp_issues
                          join pr in pd on ps.ProjectReference equals pr.ProjectReference
                          where pr.SubProgramme > 0 && pr.SubProgramme == id
                          select ps).ToList();
                break;
        }
        return retval;
    }

    private List<ProjectBenefitItem> GetBenefit_vertical(string v_section, int id)
    {

        List<ProjectBenefitItem> retval = null;
        switch (v_section.ToLower())
        {
            case "site":
                retval = (from ps in p_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.SiteID > 0 && pr.SiteID == id
                          select ps).ToList();
                break;
            case "city":
                retval = (from ps in p_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.CityID > 0 && pr.CityID == id
                          select ps).ToList();
                break;
            case "country":
                retval = (from ps in p_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.CountryID > 0 && pr.CountryID == id
                          select ps).ToList();
                break;
            case "owner":
                retval = (from ps in p_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.OwnerID > 0 && pr.OwnerID == id
                          select ps).ToList();
                break;
            case "class":
                retval = (from ps in p_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.CategoryID > 0 && pr.CategoryID == id
                          select ps).ToList();
                break;
            case "sub_programme":
                retval = (from ps in p_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.SubProgramme > 0 && pr.SubProgramme == id
                          select ps).ToList();
                break;
        }
        return retval;
    }


    private List<ProjectBenefitItem> GetBenefit_horzantal(List<ProjectBenefitItem> temp_benefit, string h_section, int id)
    {
        List<ProjectBenefitItem> retval = null;
        switch (h_section.ToLower())
        {
            case "site":
                retval = (from ps in temp_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.SiteID > 0 && pr.SiteID == id
                          select ps).ToList();
                break;
            case "city":
                retval = (from ps in temp_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.CityID > 0 && pr.CityID == id
                          select ps).ToList();
                break;
            case "country":
                retval = (from ps in temp_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.CountryID > 0 && pr.CountryID == id
                          select ps).ToList();
                break;
            case "owner":
                retval = (from ps in temp_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.OwnerID > 0 && pr.OwnerID == id
                          select ps).ToList();
                break;
            case "class":
                retval = (from ps in temp_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.CategoryID > 0 && pr.CategoryID == id
                          select ps).ToList();
                break;
            case "sub_programme":
                retval = (from ps in temp_benefit
                          join pb in pdt.ProjectBenefits on ps.BenefitID equals pb.ID
                          join pr in pd on pb.Projectreference equals pr.ProjectReference
                          where pr.SubProgramme > 0 && pr.SubProgramme == id
                          select ps).ToList();
                break;
        }
        return retval;
    }

    private string RetRiskImgUrl(List<AC2P_Risk> retList,int v_id,int h_id)
    {
        string retval = string.Empty;

        if (retList.Count() > 0)
        {
            int redcount = (from r in retList where r.NextReviewDate.Value < DateTime.Now.AddDays(double.Parse(string.IsNullOrEmpty(txtRedRisk.Text) ? "0" : txtRedRisk.Text)) select r).Count();
            int ambercount = 0;
            if (double.Parse(string.IsNullOrEmpty(txtAmberRisk.Text) ? "0" : txtAmberRisk.Text) > 0)
            ambercount = (from r in retList where r.NextReviewDate < DateTime.Now.AddDays(double.Parse(string.IsNullOrEmpty(txtAmberRisk.Text) ? "0" : txtAmberRisk.Text)) select r).Count();
            int greencount = (from r in retList where r.NextReviewDate >= DateTime.Now select r).Count();

             if (redcount > 0 || ambercount > 0)
            {
                if (redcount > ambercount)
                {
                    retval = string.Format("<a href='#' targer='_blank' onclick=window.open('ShowProgrammePopupV2.aspx?programmeid={0}&v_select={1}&v_id={2}&h_select={3}&h_id={4}&Ownerid={5}&benifit={6}&type=risks&color=red','mywindow','width=700,height=400,scrollbars=1')> <img src='media/icon_red.png' /></a>", hid.Value, ddlVertical.SelectedValue, v_id, ddlHorizontal.SelectedValue, h_id, ddlOwner.SelectedValue, ddlBenifitType.SelectedValue);
                }
                else 
                {
                    retval = string.Format("<a href='#' targer='_blank' onclick=window.open('ShowProgrammePopupV2.aspx?programmeid={0}&v_select={1}&v_id={2}&h_select={3}&h_id={4}&Ownerid={5}&benifit={6}&type=risks&color=amber','mywindow','width=700,height=400,scrollbars=1')> <img src='media/icon_amber.png' /></a>", hid.Value, ddlVertical.SelectedValue, v_id, ddlHorizontal.SelectedValue, h_id, ddlOwner.SelectedValue, ddlBenifitType.SelectedValue);
                }
            }
            else
            {
                retval = string.Format("<a href='#' targer='_blank' onclick=window.open('ShowProgrammePopupV2.aspx?programmeid={0}&v_select={1}&v_id={2}&h_select={3}&h_id={4}&Ownerid={5}&benifit={6}&type=risks&color=green','mywindow','width=700,height=400,scrollbars=1')> <img src='media/icon_green.png' /></a>", hid.Value, ddlVertical.SelectedValue, v_id, ddlHorizontal.SelectedValue, h_id, ddlOwner.SelectedValue, ddlBenifitType.SelectedValue);
            }

            //if (redcount > greencount)
            //{
            //    retval = string.Format("<a href='#' targer='_blank' onclick=window.open('ShowProgrammePopupV2.aspx?programmeid={0}&v_select={1}&v_id={2}&h_select={3}&h_id={4}&Ownerid={5}&benifit={6}&type=risks&color=red','mywindow','width=700,height=400,scrollbars=1')> <img src='media/icon_red.png' /></a>", hid.Value, ddlVertical.SelectedValue, v_id, ddlHorizontal.SelectedValue, h_id, ddlOwner.SelectedValue, ddlBenifitType.SelectedValue);
            //    //retval = "<img src='media/icon_red.png' />";
            //}
            //else
            //{
            //    retval = string.Format("<a href='#' targer='_blank' onclick=window.open('ShowProgrammePopupV2.aspx?programmeid={0}&v_select={1}&v_id={2}&h_select={3}&h_id={4}&Ownerid={5}&benifit={6}&type=risks&color=green','mywindow','width=700,height=400,scrollbars=1')> <img src='media/icon_green.png' /></a>", hid.Value, ddlVertical.SelectedValue, v_id, ddlHorizontal.SelectedValue, h_id, ddlOwner.SelectedValue, ddlBenifitType.SelectedValue);
            //    //retval = "<img src='media/icon_green.png' />";
            //}
        }
        return retval;
        
    }

    private string RetMilestoneImgUrl(List<ProjectTaskItem> retList,int v_id,int h_id)
    {
        string retval = string.Empty;

        if (retList.Count() > 0)
        {
            double amberday_cnt = double.Parse(string.IsNullOrEmpty(txtAmberMilestone.Text) ? "0" : txtAmberMilestone.Text);
            double redday_cnt = double.Parse(string.IsNullOrEmpty(txtRedMilestone.Text) ? "0" : txtRedMilestone.Text);

            int redcount = (from r in retList where r.ItemStatus != 3 && ( DateTime.Now.AddDays(redday_cnt) < r.ProjectEndDate ) select r).Count();
            int ambercount = 0;
            if (double.Parse(string.IsNullOrEmpty(txtAmberMilestone.Text) ? "0" : txtAmberMilestone.Text) > 0)
                ambercount = (from r in retList where r.ItemStatus != 3 && (DateTime.Now.AddDays(amberday_cnt) < r.ProjectEndDate && DateTime.Now.AddDays(redday_cnt) > r.ProjectEndDate) select r).Count();
            int greencount = (from r in retList where r.ProjectEndDate >= DateTime.Now select r).Count();
            if (redcount > 0 || ambercount > 0)
            {
                if (redcount > ambercount)
                {
                    retval = string.Format("<a href='#' targer='_blank' onclick=window.open('ShowProgrammePopupV2.aspx?programmeid={0}&v_select={1}&v_id={2}&h_select={3}&h_id={4}&Ownerid={5}&benifit={6}&type=tasks&color=red','mywindow','width=700,height=400,scrollbars=1')><span style='color:red;'><i class='fa fa-circle'></i></span></a>", hid.Value, ddlVertical.SelectedValue, v_id, ddlHorizontal.SelectedValue, h_id, ddlOwner.SelectedValue, ddlBenifitType.SelectedValue);
                    //retval = "<img src='media/icon_red.png' />";
                }
                else 
                {
                    retval = string.Format("<a href='#' targer='_blank' onclick=window.open('ShowProgrammePopupV2.aspx?programmeid={0}&v_select={1}&v_id={2}&h_select={3}&h_id={4}&Ownerid={5}&benifit={6}&type=tasks&color=amber','mywindow','width=700,height=400,scrollbars=1')><span style='color:yellow;'><i class='fa fa-circle'></i></span></a>", hid.Value, ddlVertical.SelectedValue, v_id, ddlHorizontal.SelectedValue, h_id, ddlOwner.SelectedValue, ddlBenifitType.SelectedValue);
                }
            }
            else
            {
                retval = string.Format("<a href='#' targer='_blank' onclick=window.open('ShowProgrammePopupV2.aspx?programmeid={0}&v_select={1}&v_id={2}&h_select={3}&h_id={4}&Ownerid={5}&benifit={6}&type=tasks&color=green','mywindow','width=700,height=400,scrollbars=1')><span style='color:green;'><i class='fa fa-circle'></i></span></a>", hid.Value, ddlVertical.SelectedValue, v_id, ddlHorizontal.SelectedValue, h_id, ddlOwner.SelectedValue, ddlBenifitType.SelectedValue);
                //retval = "<img src='media/icon_green.png' />";
            }
        }
        return retval;
    }

    private string RetBenefitImgUrl(List<ProjectBenefitItem> retList,int v_id,int h_id)
    {
        string retval = string.Empty;
        int subprogrammeid = 0;
        if (retList.Count() > 0)
        {
            //to get subprogramme id
            if (ddlVertical.SelectedValue.ToLower() == "sub_programme")
                subprogrammeid = v_id;
            if(ddlHorizontal.SelectedValue.ToLower() == "sub_programme")
                subprogrammeid = h_id;
            decimal red_planned = (from r in retList where r.Period < DateTime.Now select r.Planned).Sum().Value;
            decimal green_planned = (from r in retList where r.Period >= DateTime.Now select r.Planned).Sum().Value;
            decimal red_actual = (from r in retList where r.Period < DateTime.Now select r.Actual).Sum().Value;
            decimal green_actual = (from r in retList where r.Period >= DateTime.Now select r.Actual).Sum().Value;
            
            if ((red_planned-red_actual) > (green_planned-green_actual))
            {
                retval = string.Format("<a href='Programme.aspx?Panel=2&programmeid={0}&subprogrammeid={1}&benefittypeid={2}' targer='_blank' > <span style='color:red;'><i class='fa fa-circle'></i></span></a>", hid.Value, subprogrammeid, ddlBenifitType.SelectedValue);
                //retval = "<img src='media/icon_red.png' />";
            }
            else
            {
                retval = string.Format("<a href='Programme.aspx?Panel=2&programmeid={0}&subprogrammeid={1}&benefittypeid={2}' targer='_blank' > <span style='color:green;'><i class='fa fa-circle'></i></span></a>", hid.Value, subprogrammeid, ddlBenifitType.SelectedValue);
                //retval = "<img src='media/icon_green.png' />";
            }
        }
        return retval;

    }
    private string RetBenefitStr(List<ProjectBenefitItem> v_benefitList)
    {
        string retval = string.Empty;

        if (v_benefitList.Count() > 0)
        {
            decimal planned = (from r in v_benefitList select r.Planned).Sum().Value;
            decimal actual = (from r in v_benefitList select r.Actual).Sum().Value;


            if (actual >= 0)
            {
                retval = string.Format("{1}/{0}", planned, actual);
            }
           
        }
        return retval;

    }

    #region Dashboard Config
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            //AddDashboardConfig();

            //bind the RAG collection
            LoadControl();

        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    private void AddDashboardConfig()
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "ProgrammeDashbordConfig_InsertUpdate",
           new SqlParameter("@VerticalValue", ddlVertical.SelectedValue), new SqlParameter("@HorizontalValue", ddlHorizontal.SelectedValue), new SqlParameter("@RiskRedDays", int.Parse(string.IsNullOrEmpty(txtRedRisk.Text.Trim()) ? "0" : txtRedRisk.Text.Trim())),
           new SqlParameter("@RiskGreenDays", int.Parse(string.IsNullOrEmpty(txtAmberRisk.Text.Trim()) ? "0" : txtAmberRisk.Text.Trim()))
           , new SqlParameter("@MilestoneRedDays", int.Parse(string.IsNullOrEmpty(txtRedMilestone.Text.Trim()) ? "0" : txtRedMilestone.Text.Trim()))
           , new SqlParameter("@MilestoneGreenDays", int.Parse(string.IsNullOrEmpty(txtAmberMilestone.Text.Trim()) ? "0" : txtAmberMilestone.Text.Trim()))
           , new SqlParameter("@BenefitID", int.Parse(string.IsNullOrEmpty(ddlBenifitType.SelectedValue) ? "0" : ddlBenifitType.SelectedValue))
           , new SqlParameter("@OwnerID", int.Parse(string.IsNullOrEmpty(ddlOwner.SelectedValue) ? "0" : ddlOwner.SelectedValue))
           , new SqlParameter("@UserID", sessionKeys.UID), new SqlParameter("@ConfigName", txtDashboardConfig.Text.Trim()));
        lblMsgDashboard.ForeColor = System.Drawing.Color.Green;
        lblMsgDashboard.Text = "Added successfully";
    }
    private void BindDefaultDashboardDropdown()
    {
        DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ConfigName from ProgrammeDashbordConfig ").Tables[0];
        ddlDashboardconfig.DataSource = dt;
        ddlDashboardconfig.DataTextField = "ConfigName";
        ddlDashboardconfig.DataValueField = "ConfigName";
        ddlDashboardconfig.DataBind();
        ddlDashboardconfig.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindDefaultDashboard()
    {
        try
        {
            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ProgrammeDashbordConfig_Select",
                new SqlParameter("@ConfigName", ddlDashboardconfig.SelectedValue)).Tables[0];

            if (dt.Rows.Count > 0)
            {
                ddlHorizontal.SelectedValue = dt.Rows[0][2].ToString();
                ddlVertical.SelectedValue = dt.Rows[0][1].ToString();
                txtRedRisk.Text = dt.Rows[0][3].ToString();
                txtAmberRisk.Text = dt.Rows[0][4].ToString();
                txtRedMilestone.Text = dt.Rows[0][5].ToString();
                txtAmberMilestone.Text = dt.Rows[0][6].ToString();
                ddlBenifitType.SelectedValue = dt.Rows[0][7].ToString();
                ddlOwner.SelectedValue = dt.Rows[0][8].ToString();
            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    private bool DashboardConfig_exists(string Configname)
    {
        bool retval = true;
        using (SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "ProgrammeDashbordConfig_Check", new SqlParameter("@ConfigName", txtDashboardConfig.Text.Trim())))
        {
            while (dr.Read())
            {
                if (int.Parse(dr["count"].ToString()) > 0)
                    retval = false;
            }
            dr.Close();
        }
        return retval;
    }
    #endregion

    protected void btnDashboard_Click(object sender, EventArgs e)
    {
        if (DashboardConfig_exists(txtDashboardConfig.Text.Trim()))
        {
            AddDashboardConfig();
            //bind dashboard
            BindDefaultDashboardDropdown();
            //set the inserted data
            //ddlDashboardconfig.SelectedValue = txtDashboardConfig.Text.Trim();
            BindDefaultDashboard();
            LoadControl();

            txtDashboardConfig.Text = string.Empty;

            ConfigAddVisibility(false);
        }
        else
        {
            lblMsgDashboard.Text = "Config name already exists";
        }

    }

    protected void btnSaveconfig_Click(object sender, EventArgs e)
    {
        ConfigAddVisibility(true);
    }

    private void ConfigAddVisibility(bool setval)
    {
        btnSaveconfig.Visible = !setval;
        ddlDashboardconfig.Visible = !setval;
        btnViewDashboard.Visible = !setval;
        txtDashboardConfig.Visible = setval;
        btnDashboardConfig.Visible = setval;
        btnDashboardCancel.Visible = setval;
    }
    protected void btnViewDashboard_Click(object sender, EventArgs e)
    {
        BindDefaultDashboard();
        LoadControl();
    }
    protected void btnDashboardCancel_Click(object sender, EventArgs e)
    {
        txtDashboardConfig.Text = string.Empty;
        ConfigAddVisibility(false);
    }
}


