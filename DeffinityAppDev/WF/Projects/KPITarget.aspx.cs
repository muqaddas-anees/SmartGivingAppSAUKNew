using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
public partial class KPITarget : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try{
           // Master.PageHead = "KPI";
            //if (sessionKeys.SID != 1)
            //{
            //    Master.ErrorMsg = "Only administrator can update target values";
            //}
            txtFromDate.Text = DateTime.Now.Year.ToString();
            BindTargetGrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

    }
   

    private void BindTargetGrid()
    {
        DataTable dt = new DataTable();
         string year=string.IsNullOrEmpty(txtFromDate.Text)?DateTime.Now.Year.ToString():txtFromDate.Text;
         string fromdate = "01/01/" + year;
      string query="select KL.LabelID,KL.LabelsName,isnull((select isnull(TargetValues,0) from  KPI_TargetValues where KPI_LabelID=KL.LabelID"+
                " and year(TargetYears)= YEAR('" + fromdate + "')),0) value from KPI_LablesName KL where KL.PageType=@Page";
      dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, query,
          new SqlParameter("@Page",int.Parse(ddlPageType.SelectedValue))).Tables[0];
      gridKPILables.DataSource = dt;
      gridKPILables.DataBind();
        
    }
    protected void ddlPageType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgView_Click(object sender, EventArgs e)
    {
        try{
        BindTargetGrid();
        }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
    }
    protected void gridKPILables_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            try
            {
               
                string year = string.IsNullOrEmpty(txtFromDate.Text) ? DateTime.Now.Year.ToString() : txtFromDate.Text;
                if (year == DateTime.Now.Year.ToString())
                {
                    string fromdate = "01/01/" + year;
                    projectTaskDataContext kpi = new projectTaskDataContext();
                    int CountRow = gridKPILables.Rows.Count;
                    for (int i = 0; i < CountRow; i++)
                    {
                        GridViewRow Row = gridKPILables.Rows[i];
                        TextBox txtValue = (TextBox)Row.FindControl("txtDescription");
                        Label lblID = (Label)Row.FindControl("lblID");
                        var IsExist = (from r in kpi.KPI_TargetValues
                                       where r.KPI_LabelID == int.Parse(lblID.Text) &&
                                       r.pagetype == int.Parse(ddlPageType.SelectedValue)
                                       && r.TargetYears.Value.Year == Convert.ToInt32(year)
                                       select r).ToList();

                        if (IsExist != null)
                        {
                            if (IsExist.Count == 0)
                            {
                                KPI_TargetValue kpiVal = new KPI_TargetValue();
                                kpiVal.TargetYears = Convert.ToDateTime(fromdate);
                                kpiVal.TargetValues = Convert.ToDouble(txtValue.Text);
                                kpiVal.pagetype = int.Parse(ddlPageType.SelectedValue);
                                kpiVal.KPI_LabelID = int.Parse(lblID.Text);
                                kpi.KPI_TargetValues.InsertOnSubmit(kpiVal);
                                kpi.SubmitChanges();
                            }
                            else
                            {
                                KPI_TargetValue kpiVal = kpi.KPI_TargetValues.Single(P => P.KPI_LabelID == int.Parse(lblID.Text)
                                    && P.pagetype == int.Parse(ddlPageType.SelectedValue) && P.TargetYears == Convert.ToDateTime(fromdate));
                                kpiVal.TargetValues = Convert.ToDouble(txtValue.Text);
                                kpi.SubmitChanges();
                            }
                        }

                    }
                    lblMessage.Visible = true;
                    lblMessage.Text = "Updated successfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Sorry,only current year targets can update";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    protected void gridKPILables_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gridKPILables_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
    }
    protected bool showButton()
    {
        bool yes = true;
        if (txtFromDate.Text != DateTime.Now.Year.ToString())
               
        {
            yes = false;
        }
        return yes;
    }
}
