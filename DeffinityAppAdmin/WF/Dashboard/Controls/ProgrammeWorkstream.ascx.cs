using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Text;

public partial class controls_ProgrammeWorkstream : System.Web.UI.UserControl
{
   
    public int ProgrammeID
    { get; set; }
    public int SubProgrammeID
    { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
            LoadControl();
    }

    private int Get_ProgrammeID()
    {
        if (Session["Programme"] != null )
        {
           hid.Value  = Session["Programme"].ToString();
        }

        return int.Parse(string.IsNullOrEmpty(hid.Value) ? "0" : hid.Value);
    }

    #region Workstream vs Period

    private string GetByPeriod()
    {
        //ProgrammeID = 19;
        bool checkDataExits = false;
        string sectionName = string.Empty;
        StringBuilder str = new StringBuilder();
        using (projectTaskDataContext pdt = new projectTaskDataContext())
        {
            //Table starting
            str.Append("<table CellPadding='5' CellSpacing='1' class='table table-small-font table-bordered table-striped dataTable responsive'>");
            //Header row
            str.Append("<tr class='tab_header' style='font-weight:bold;text-align:center;height:30px;'>");
            //add columns
            str.Append("<td>Workstream</td>");
            str.Append("<td>Projects</td>");
            List<Quarter_date_class> l_quarter = DatesOfQuarter(DateTime.Now.Year, 1);
            foreach (Quarter_date_class q in l_quarter)
            {
                str.Append(string.Format("<td>{0}</td>",q.qstring));
            }
            str.Append("</tr>");

            //List<RAGSectionstoPortfolio> list_RagSectionstoPortfolio = new List<RAGSectionstoPortfolio>();
            IOrderedQueryable list_RagSectionstoPortfolio = from p in pdt.RAGSectionstoPortfolios
                                                            where p.ProgrammeID == ProgrammeID
                                                            orderby p.ID ascending
                                                            select p;
            List<ProjectDetails> list_Projectdetails = (from p in pdt.ProjectDetails
                                                    where p.OwnerGroupID == ProgrammeID
                                                    select p).ToList();
            List<RAGSectiontoProject> rsp = (from p in pdt.RAGSectiontoProjects
                                              where p.ProgrammeID == ProgrammeID
                                              && p.ActualDate.HasValue
                                              select p).ToList();
            string projectprefix = pdt.ProjectDefaults.Select(p => p.ProjectReferencePrefix).FirstOrDefault();
            //workstream
            foreach (RAGSectionstoPortfolio rp in list_RagSectionstoPortfolio)
            {
                sectionName = rp.RAGSectionName;
                //project refernce
                foreach (RAGSectiontoProject r in rsp.Where(p => p.RAGSectionID == rp.ID).OrderBy(p=>p.ProjectReference))
                {
                   
                        str.Append("<tr style='text-align:center;' class='even_row'>");
                        str.Append(string.Format("<td><b>{0}</b></td>", sectionName));
                        //Clear after adding first time
                        sectionName = string.Empty;

                        str.Append(string.Format("<td><a href='ProjectOverview.aspx?project={1}'>{0}</a></td>", projectprefix + r.ProjectReference.ToString(), r.ProjectReference.ToString()));
                        //date check
                        foreach (Quarter_date_class a in l_quarter)
                        {
                            //data is exits
                            checkDataExits = true;
                            var re = rsp.Where(p => p.ProjectReference == r.ProjectReference && p.RAGSectionID == rp.ID && (p.ActualDate >= a.sdate && p.ActualDate <= a.edate)).Select(p => p);
                            if (re != null)
                            {
                                if (re.Count() > 0)
                                {
                                    if (r.ActualDate.HasValue)
                                    {
                                        if (r.ActualDate.Value.Year != 1900)
                                        {
                                            if (r.PlannedDate.Value.Year >= r.ActualDate.Value.Year && r.PlannedDate.Value.Month >= r.ActualDate.Value.Month && r.PlannedDate.Value.Date >= r.ActualDate.Value.Date)
                                            { r.RAGStatus = "GREEN"; }
                                            else
                                            { r.RAGStatus = "RED"; }
                                            str.Append(string.Format("<td><img src='media/icon_{2}.png' alt='Planned date: {0}' title='Planned date: {0} \nActual date: {1}' /></a></td>", r.PlannedDate.Value.ToShortDateString(), (r.ActualDate.HasValue ? r.ActualDate.Value.ToShortDateString() : string.Empty), string.IsNullOrEmpty(r.RAGStatus) ? string.Empty : r.RAGStatus));
                                        }
                                        else
                                        {
                                            str.Append("<td></td>");
                                        }
                                    }
                                    else
                                        str.Append("<td></td>");
                                }
                                else
                                    str.Append("<td></td>");
                            }

                            else
                                str.Append("<td></td>");

                        }
                        str.Append("</tr>");
                    
                }
                
            }
            //data is not exists
            if (!checkDataExits)
                str.Clear();
            //Table ending
            str.Append("</table>");
        }

        return str.ToString();
    }
#region temp calss
    class Quarter_date_class
   {
    public DateTime sdate
    {set;get;}
    public DateTime edate
    {set;get;}
     public string qstring
    {set;get;}

   }

#endregion
    

    private List<Quarter_date_class> DatesOfQuarter(int year, int year_count)
    {
        List<Quarter_date_class> list_q = new List<Quarter_date_class>();
        Quarter_date_class q = new Quarter_date_class();
        
        for (int i = 0; i <= year_count; i++)
        {
            DateTime dtNow = DateTime.Parse("1/1/" + (year + i).ToString());

            list_q.Add(new Quarter_date_class
            {
                sdate = DateTime.Parse("1/1/" + dtNow.Year.ToString()),
                edate = DateTime.Parse("31/3/" + dtNow.Year.ToString()),
                qstring = "Q1 " + dtNow.Year.ToString()
            });

            list_q.Add(new Quarter_date_class
            {
                sdate = DateTime.Parse("1/4/" + dtNow.Year.ToString()),
                edate = DateTime.Parse("30/6/" + dtNow.Year.ToString()),
                qstring = "Q2 " + dtNow.Year.ToString()
            });

            list_q.Add(new Quarter_date_class
            {
                sdate = DateTime.Parse("1/7/" + dtNow.Year.ToString()),
                edate = DateTime.Parse("30/9/" + dtNow.Year.ToString()),
                qstring = "Q3 " + dtNow.Year.ToString()
            });
            list_q.Add(new Quarter_date_class
            {
                sdate = DateTime.Parse("1/10/" + dtNow.Year.ToString()),
                edate = DateTime.Parse("31/12/" + dtNow.Year.ToString()),
                qstring = "Q4 " + dtNow.Year.ToString()
            });

        }
        return list_q;
    }


    #endregion

    #region Workstream by Project
    
    private string GetByProject()
    {

        //ProgrammeID = 19;
        bool checkDataExits = false;
        StringBuilder str = new StringBuilder();
        using (projectTaskDataContext pdt = new projectTaskDataContext())
        {
            //Table starting
            str.Append("<table CellPadding='5' CellSpacing='1' class='table table-small-font table-bordered table-striped dataTable responsive' >");
            //Header row
            str.Append("<tr class='tab_header' style='font-weight:bold;text-align:center;height:30px'>");
            //add columns

            str.Append("<td>Workstream</td>");
            //Get list of Workstreams
            //List<RAGSectionstoPortfolio> list_RagSectionstoPortfolio = new List<RAGSectionstoPortfolio>();
            IOrderedQueryable list_RagSectionstoPortfolio = from p in pdt.RAGSectionstoPortfolios
                                                            where p.ProgrammeID == ProgrammeID
                                                            orderby p.ID ascending
                                                            select p;
            //append columns
            foreach(RAGSectionstoPortfolio rag_p in list_RagSectionstoPortfolio)
            {
                str.Append(string.Format("<td>{0}</td>", rag_p.RAGSectionName));            
            }
            str.Append("</tr>");
            IOrderedQueryable list_Projectdetails = from p in pdt.ProjectDetails
                                                            where p.OwnerGroupID == ProgrammeID
                                                            orderby p.ProjectReference ascending
                                                            select p;



            foreach (ProjectDetails pd in list_Projectdetails)
            {
                //check the at least one actual date is exist in the project
                int check_val = (from p in pdt.RAGSectiontoProjects
                                          where p.ProjectReference == pd.ProjectReference
                                         && p.ActualDate.HasValue
                                          select p).Count();
                if (check_val > 0)
                {
                    str.Append("<tr style='text-align:center;' class='even_row'>");
                    str.Append(string.Format("<td><a href='ProjectOverviewV4.aspx?project={1}'>{0}</a></td>", pd.ProjectReferenceWithPrefix, pd.ProjectReference));
                    foreach (RAGSectionstoPortfolio rag_p in list_RagSectionstoPortfolio)
                    {
                        //data is exists
                        checkDataExits = true;
                        RAGSectiontoProject rp = (from p in pdt.RAGSectiontoProjects
                                                  where p.ProjectReference == pd.ProjectReference
                                                  && p.RAGSectionID == rag_p.ID
                                                  select p).FirstOrDefault();
                        if (rp != null)
                        {
                            if (rp.ActualDate.HasValue)
                            {
                                if (rp.ActualDate.Value.Year != 1900)
                                {
                                    if (rp.PlannedDate.Value.Year >= rp.ActualDate.Value.Year && rp.PlannedDate.Value.Month >= rp.ActualDate.Value.Month && rp.PlannedDate.Value.Date >= rp.ActualDate.Value.Date)
                                    { rp.RAGStatus = "GREEN"; }
                                    else
                                    { rp.RAGStatus = "RED"; }
                                    str.Append(string.Format("<td><img src='media/icon_{2}.png' alt='Planned date: {0}' title='Planned date: {0} \nActual date: {1}' /></a></td>", rp.PlannedDate.Value.ToShortDateString(), (rp.ActualDate.HasValue ? rp.ActualDate.Value.ToShortDateString() : string.Empty), string.IsNullOrEmpty(rp.RAGStatus) ? string.Empty : rp.RAGStatus));
                                }
                                else
                                {
                                    str.Append("<td></td>");
                                }
                            }
                            else
                                str.Append("<td></td>");
                        }
                        else
                            str.Append("<td></td>");
                    }

                    str.Append("</tr>");
                }
            }
           

            //Table ending
            str.Append("</table>");
        }
        //data is not exists
        if (!checkDataExits)
            str.Clear();
        return str.ToString();
    }

    #endregion


    protected void Radiolist_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadControl();
    }

    public void LoadControl()
    {
        try
        {
          ProgrammeID = Get_ProgrammeID();
            if (Radiolist.SelectedValue == "1")
                litDisplayHtml.Text = GetByProject();
            else
                litDisplayHtml.Text = GetByPeriod();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}