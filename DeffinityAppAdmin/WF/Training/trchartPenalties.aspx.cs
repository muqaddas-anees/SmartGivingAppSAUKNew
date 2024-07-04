using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Shared.Styles;
using Deffinity.TrainingManager;
using Deffinity.TrainingEntity;
public partial class Training_trchartPenalties : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTitle.InnerText = "Penalties by Department";
        //Master.PageHead = "Training Management";
        if (!IsPostBack)
        {
            BindDepartments();
            BindArea();
            BindChart();
        }
        //this. UltraChart1.Axis.X.Labels.Layout.BehaviorCollection.AddRange(this.GetCustomLayoutBehaviors());
        //this. UltraChart1.Axis.X.Labels.SeriesLabels.Layout.BehaviorCollection.AddRange(this.GetCustomLayoutBehaviors());
        //this. UltraChart1.Axis.Y.Labels.Layout.BehaviorCollection.AddRange(this.GetCustomLayoutBehaviors());
        UltraChart1.Axis.X.Labels.Visible =false;
        UltraChart1.Axis.Y.Labels.Visible = true;

       

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        string area = "";
        if (ddlArea.SelectedItem.ToString() == "")
        {
            area = "";
        }
        else
        {
            area = ddlArea.SelectedItem.ToString();
        }

        UltraChart1.TitleTop.Text = "Penalties Incurred by Department :" + ddlDepartment.SelectedItem.ToString() + " Area :" + area;
          UltraChart1.TitleTop.FontSizeBestFit = true;
          UltraChart1.TitleTop.HorizontalAlign = System.Drawing.StringAlignment.Center;
            BindChart();
    }
    private AxisLabelLayoutBehavior[] GetCustomLayoutBehaviors()
    {
        // scale fonts down to 8pt if necessary.
        FontScalingAxisLabelLayoutBehavior fontScaling1 = new FontScalingAxisLabelLayoutBehavior();
        fontScaling1.MaximumSize = -1f;
        fontScaling1.MinimumSize = 8f;
        fontScaling1.EnableRollback = false;

        // if collisions are detected, try wrapping the text
        WrapTextAxisLabelLayoutBehavior wrapText1 = new WrapTextAxisLabelLayoutBehavior();
        wrapText1.EnableRollback = true;

        // try rotating to 30 degrees
        RotateAxisLabelLayoutBehavior rotation1 = new RotateAxisLabelLayoutBehavior();
        rotation1.RotationAngle = 30f;
        rotation1.EnableRollback = true;

        // failing that, try rotating to 60 degrees
        RotateAxisLabelLayoutBehavior rotation2 = new RotateAxisLabelLayoutBehavior();
        rotation2.RotationAngle = 60f;
        rotation2.EnableRollback = true;

        // try staggering the labels
        StaggerAxisLabelLayoutBehavior stagger1 = new StaggerAxisLabelLayoutBehavior();
        stagger1.EnableRollback = true;

        // since none of the above worked, scale the fonts down to 6pt
        FontScalingAxisLabelLayoutBehavior fontScaling2 = new FontScalingAxisLabelLayoutBehavior();
        fontScaling2.MaximumSize = -1f;
        fontScaling2.MinimumSize = 6f;
        fontScaling2.EnableRollback = false;

        // if collisions are still detected, just truncate the labels.
        ClipTextAxisLabelLayoutBehavior clipText1 = new ClipTextAxisLabelLayoutBehavior();
        clipText1.EnableRollback = false;

        return new AxisLabelLayoutBehavior[] { fontScaling1, wrapText1, rotation1, rotation2, stagger1, fontScaling2, clipText1 };
    }
    #region BindData
    private void BindChart()
    {
        this.UltraChart1.DeploymentScenario.FilePath = "../ChartImages";
        this.UltraChart1.DeploymentScenario.ImageURL = "../ChartImages/Chart_#SEQNUM(100).png";
        string area = "";
        if (ddlArea.SelectedIndex == -1)
        {
            area = "";
        }
        else
        {
            area = ddlArea.SelectedItem.ToString();
        }
        UltraChart1.TitleTop.Text = "Penalties Incurred by Department :" + ddlDepartment.SelectedItem.ToString() + " Area :" + area;
         UltraChart1.TitleTop.FontSizeBestFit = true;
         UltraChart1.TitleTop.HorizontalAlign = System.Drawing.StringAlignment.Center;
         //UltraChart1.DataSource = Bookings.Booking_Penalties(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlArea.SelectedValue),
         //          Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "1/1/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "1/1/1900" : txtToDate.Text));
         //UltraChart1.DataBind();        

         System.Data.DataTable dt = Bookings.Booking_Penalties(int.Parse(ddlDepartment.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlArea.SelectedValue) ? "0" : ddlArea.SelectedValue),
                   Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "1/1/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "1/1/1900" : txtToDate.Text));
         if (dt.Rows.Count == 0)
         {
             UltraChart1.Visible = false;
             lblException.Visible = true;
             lblException.Text = "No records found";
             lblException.ForeColor = System.Drawing.Color.Red;
         }
         else
         {
             lblException.Visible = false;
             UltraChart1.Visible = true;
             UltraChart1.DataSource = dt;
             UltraChart1.DataBind();
             UltraChart1.Axis.X.Labels.Visible = false;
             UltraChart1.Axis.Y.Labels.Visible = true;
         }
      
    }
    public void BindDepartments()
    {
        ddlDepartment.DataSource = Department.Department_SelectAll();
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();
    }
    public void BindArea()
    {
        ddlArea.DataSource = Area.Area_OrderByAsc(int.Parse(ddlDepartment.SelectedValue));// Area.Area_SelectAll();
        ddlArea.DataValueField = "ID";
        ddlArea.DataTextField = "Name";
        ddlArea.DataBind();

    }
    #endregion
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlArea.DataSource = Area.Area_OrderByAsc(int.Parse(ddlDepartment.SelectedValue));// Area.Area_SelectAll();
        ddlArea.DataValueField = "ID";
        ddlArea.DataTextField = "Name";
        ddlArea.DataBind();
        BindChart();
    }
}
