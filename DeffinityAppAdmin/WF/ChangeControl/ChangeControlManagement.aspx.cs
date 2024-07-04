using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Incidents.Entity;
using Incidents.StateManager;
using Incidents.DAL;

public partial class ChangeControlManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //    Master.PageHead = "Change Management";
        
        
    }
    protected void gridChangeControl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Gets the Change from the database and saves to the session and redirects to the ChangeControl.aspx page.
        if (e.CommandName == "Change")
        {
            int changeID = Convert.ToInt32(e.CommandArgument);
            sessionKeys.ChangeControlID = changeID;
            Change change = ChangeHelper.LoadChangesById(changeID);
            ChangeState.ChangeSaver = change;
            sessionKeys.PortfolioID = change.PortfolioID;
            sessionKeys.PortfolioName = change.Customer;
            Response.Redirect("ChangeControl.aspx", false);
        }
    }

    protected void gridChangeControl_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            AddGlyph(gridChangeControl, e.Row);
    }

    private void AddGlyph(GridView grid, GridViewRow item)
    {
        Label glyph = new Label();
        glyph.EnableTheming = false;
        glyph.Font.Name = "webdings";
        glyph.Font.Size = FontUnit.XSmall;
        glyph.ForeColor = System.Drawing.Color.Orange;
        glyph.Text = (grid.SortDirection == SortDirection.Ascending ? "5" : "6");

        //Find the column that is sorted by
        for (int i = 0; i < grid.Columns.Count; i++)
        {
            string colExpr = grid.Columns[i].SortExpression;
            if (colExpr != "" && colExpr == grid.SortExpression)
                item.Cells[i].Controls.Add(glyph);
        }
    }

    protected void imgNewChange_Click(object sender, EventArgs e)
    {
        sessionKeys.ChangeControlID = 0;
        ChangeState.ChangeSaver = null;
       // sessionKeys.PortfolioName = null;
       // sessionKeys.PortfolioName = "";
        Response.Redirect("ChangeControl.aspx", false);
    }
    protected string GetInHandSLA(string InHandSLA, string status)
    {
        string retval = string.Empty;
        //if ((status != "New") || (status != "Pending Approval") || (status != "Approved"))
        if ((status == "In Hand") || (status == "Closed") || (status == "On Hold"))
        {
            if (InHandSLA == "False")
            {

                retval = "<img src='../../WF/media/ico_indcate_red.png' alt=''/>";
            }
            else if (InHandSLA == "True")
            {
                retval = "<img src='../../WF/media/ico_indcate_green.png' alt=''/>";
            }
            else
            { retval = string.Empty; }
        }
        
        return retval;
    }
    protected string GetResolutionSLA(string ResolutionSLA, string status)
    {
        string retval = string.Empty;
        if (status == "Closed")
        {
            if (ResolutionSLA == "False")
            {
                retval = "<img src='../../WF/media/ico_indcate_red.png' alt=''/>";
            }
            else if (ResolutionSLA == "True")
            {
                retval = "<img src='WF/media/ico_indcate_green.png' alt=''/>";
            }
            else
            {
                retval = string.Empty;
            }
        }
        
        return retval;
    }
    //private void BindGrid()
    //{
    //    gridChangeControl.DataSource = ClsChangeControl.objadaChangeControl.GetData();
    //    gridChangeControl.DataBind();
    //}
}
