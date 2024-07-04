using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthCheckMgt.DAL;
using HealthCheckMgt.BAL;
using HealthCheckMgt.Entity;

public partial class HealthCheckConfigurator_page : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (sessionKeys.PortfolioID > 0)
                {
                    IntioalInsert();
                    GridBinding();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void GridBinding()
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                var HealthCheck_Configurators = Hdc.HealthCheck_Configurators.ToList();
                var x = (from a in HealthCheck_Configurators
                         where a.CustomerId == sessionKeys.PortfolioID
                         select new
                         {
                             ID = a.Id,
                             FieldName = Totalfields().Where(t => t.Value == a.FieldId.Value.ToString()).Select(t => t.Text).FirstOrDefault(),
                             visibility = a.visibility.HasValue ? (a.visibility == true ? "Yes" : "No") : "No"
                         }).ToList();
                gridHcRecords.DataSource = x;
                gridHcRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void IntioalInsert()
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                if (Hdc.HealthCheck_Configurators.Where(a => a.CustomerId == sessionKeys.PortfolioID).Count() == 0)
                {
                    List<HealthCheck_Configurator> HealthC_conList = new List<HealthCheck_Configurator>();
                    HealthCheck_Configurator Hc_config = null;
                    for (int i = 1; i <= 8; i++)
                    {
                        Hc_config = new HealthCheck_Configurator();
                        Hc_config.FieldId = i;
                        Hc_config.CustomerId = sessionKeys.PortfolioID;
                        Hc_config.visibility = true;
                        HealthC_conList.Add(Hc_config);
                    }
                    Hdc.HealthCheck_Configurators.InsertAllOnSubmit(HealthC_conList);
                    Hdc.SubmitChanges();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }    
    }
    public List<System.Web.UI.WebControls.ListItem> Totalfields()
    {
        List<System.Web.UI.WebControls.ListItem> li = new List<System.Web.UI.WebControls.ListItem>();
        try
        {
            li.Add(new System.Web.UI.WebControls.ListItem("Please select...", "0"));
            li.Add(new System.Web.UI.WebControls.ListItem("Notes", "1"));
            li.Add(new System.Web.UI.WebControls.ListItem("Yes No value", "2"));
            li.Add(new System.Web.UI.WebControls.ListItem("RAG", "3"));
            li.Add(new System.Web.UI.WebControls.ListItem("Issues", "4"));
            li.Add(new System.Web.UI.WebControls.ListItem("Status", "5"));
            li.Add(new System.Web.UI.WebControls.ListItem("Email Icon Button", "6"));
            li.Add(new System.Web.UI.WebControls.ListItem("Save and email Button", "7"));
            li.Add(new System.Web.UI.WebControls.ListItem("Due Date", "8"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }    
        return li;
    }
    protected void gridHcRecords_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try 
        {
            Label Lblvisibility = (Label)e.Row.FindControl("Lblvisibility");
            CheckBox checkvisibility = (CheckBox)e.Row.FindControl("checkvisibility");
            if (Lblvisibility != null && checkvisibility != null)
            {
                if (Lblvisibility.Text == "Yes")
                {
                    checkvisibility.Checked = true;
                }
                else
                {
                    checkvisibility.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try 
        {
            foreach (GridViewRow item in gridHcRecords.Rows)
            {
                string ID = ((Label)item.FindControl("LblId")).Text.Trim();
                CheckBox checkvisibility = (CheckBox)item.FindControl("checkvisibility");
                using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
                {
                    HealthCheck_Configurator Hc_config = new HealthCheck_Configurator();
                    Hc_config = Hdc.HealthCheck_Configurators.Where(a => a.CustomerId == sessionKeys.PortfolioID && a.Id == int.Parse(ID)).FirstOrDefault();
                    Hc_config.visibility = checkvisibility.Checked;
                    Hdc.SubmitChanges();
                }
            }
            LblMsg.Text = "Updated successfully.";
            GridBinding();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}