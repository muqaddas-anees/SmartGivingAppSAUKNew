using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using Deffinity.ServiceCatalogManager;
using Quote.BLL;
public partial class FMQuotes : System.Web.UI.Page
{
    public double Total = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "Quotes";
            BindCustomers();
            BindProjects();
            BindGrid();
        }


    }

    
    private void BindCustomers()
    {
        PortfolioDataContext timeSheet = new PortfolioDataContext();


        try
        {
            var portfolio = from r in timeSheet.ProjectPortfolios
                            where r.Visible == true
                            orderby r.PortFolio
                            select r;
            ddlCustomers.DataSource = portfolio;
            ddlCustomers.DataTextField = "PortFolio";
            ddlCustomers.DataValueField = "ID";
            ddlCustomers.DataBind();
            ddlCustomers.Items.Insert(0, new ListItem("Please select...", "0"));

            if (ddlCustomers.Items.Count>0)
            {
                ddlCustomers.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindProjects()
    {
        try
        {
            projectTaskDataContext db = new projectTaskDataContext();
            var projects = (from r in db.ProjectDetails
                            where r.Portfolio == int.Parse(ddlCustomers.SelectedValue)
                            orderby r.ProjectReference
                            select new { ID = r.ProjectReference, Name = r.ProjectReferenceWithPrefix + "-" + r.ProjectTitle }).ToList();
            if (projects != null)
            {
                ddlProjects.DataSource = projects;
                ddlProjects.DataTextField = "Name";
                ddlProjects.DataValueField = "ID";
                ddlProjects.DataBind();
            }
            ddlProjects.Items.Insert(0, new ListItem("Please select...", "0"));
            if (ddlProjects.Items.Count > 0)
            {
                ddlProjects.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Quote_Details",
                new SqlParameter("@ProjectReference", int.Parse(ddlProjects.SelectedValue)),
                 new SqlParameter("@Portfolio", int.Parse(ddlCustomers.SelectedValue))).Tables[0];

            grdProjets.DataSource = dt;
            grdProjets.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void grdProjets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            grdProjets.PageIndex = e.NewPageIndex;
            BindGrid();
           // BindChart();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdProjets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString,CommandType.StoredProcedure,"FM_Quote_Delete",
                new SqlParameter("@ID", int.Parse(e.CommandArgument.ToString())),
                new SqlParameter("@ProjectReference", int.Parse(ddlProjects.SelectedValue)));
            BindGrid();
        }

        if (e.CommandName == "View")
        {
            //int i = grdProjets.;
            //GridViewRow Row = grdProjets.Rows[i];
            //Label lblVat = (Label)Row.FindControl("lblVat");
            //hidVat.Value = lblVat.Text;
            //int ID = int.Parse(e.CommandArgument.ToString());
            hdQuote.Value = e.CommandArgument.ToString();

            BindPOPGrid(int.Parse(e.CommandArgument.ToString()),int.Parse(ddlProjects.SelectedValue),0);
            mpopBOM.Show();
        }
    }

    protected void grdItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdItems.EditIndex = e.NewEditIndex;
        BindPOPGrid(int.Parse(hdQuote.Value), int.Parse(ddlProjects.SelectedValue), 0);
        mpopBOM.Show();
        //BindGrid();
    }
    protected void grdItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
          
            QuoteItemsEntity Qe = new QuoteItemsEntity();
            if (e.CommandName == "Update")
            {
                int i = grdItems.EditIndex;
                GridViewRow Row = grdItems.Rows[i];
                int ID = int.Parse(e.CommandArgument.ToString());
                TextBox ItemDesc = (TextBox)Row.FindControl("txtitemdesc");
                // TextBox UnitPrice = (TextBox)Row.FindControl("txtunitprice"); //float.Parse(((TextBox)Row.FindControl("txtunitprice")).Text);
                TextBox Total = (TextBox)Row.FindControl("txtTotal");// int.Parse(((TextBox)Row.FindControl("txtqty")).Text);
                Label lblItemNo = (Label)Row.FindControl("lblItemNo");
                Label lbltxtqty = (Label)Row.FindControl("lbltxtqty");
               
                using (SqlConnection con = new SqlConnection(Constants.DBString))
                {
                    using (SqlCommand cmd = new SqlCommand("QuoteItems_Update", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@ItemDescription", ItemDesc.Text);
                        cmd.Parameters.AddWithValue("@Qty", int.Parse(string.IsNullOrEmpty(lbltxtqty.Text) ? "0" : lbltxtqty.Text));
                        cmd.Parameters.AddWithValue("@Unitprice", float.Parse(string.IsNullOrEmpty(Total.Text) ? "0" : Total.Text));
                       
                        cmd.ExecuteScalar();
                       
                    }
                }

                mpopBOM.Show();
              
            }
            else if (e.CommandName == "Delete")
            {
               
                int ID = int.Parse(e.CommandArgument.ToString());

                int status = QuoteItemManager.QuoteItemDelete(ID);
                mpopBOM.Show();
            }

            BindPOPGrid(int.Parse(hdQuote.Value), int.Parse(ddlProjects.SelectedValue), 0);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void grdItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        grdItems.EditIndex =-1;

        BindPOPGrid(int.Parse(hdQuote.Value), int.Parse(ddlProjects.SelectedValue), 0);
    }
    protected void grdItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotal = (Label)e.Row.FindControl("lbltotal");

                if (lblTotal != null)
                {
                    Total = Convert.ToDouble(lblTotal.Text) + Total;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                projectTaskDataContext projectDefault = new projectTaskDataContext();
                var projectDef = (from r in projectDefault.ProjectDefaults
                                  select r).ToList().FirstOrDefault();


                Label lblVatText = (Label)e.Row.FindControl("lblVatText1");
                if (projectDef != null)
                {
                    lblVatText.Text = "VAT(" + projectDef.VAT + "%)";

                    //lblVatText.Text = "VAT(" + hidVat.Value + "%)";
                    // lblVatText.Text = "VAT Payble(" + projectDef.VAT + "%)";
                }

                Label lblVat = (Label)e.Row.FindControl("lblVat1");
                Label lblSum = (Label)e.Row.FindControl("lblSum1");
                Label lblTotal = (Label)e.Row.FindControl("lblTotal1");

                lblTotal.Text = "";
                lblSum.Text = string.Format("{0:#.00}", Total); //Total.ToString();

                //double vat = ((Convert.ToDouble(hidVat.Value)) / 100) * Total;
               double vat = ((Convert.ToDouble(projectDef.VAT)) / 100) * Total;
                lblVat.Text = string.Format("{0:#.00}", vat);//vat.ToString()
                double sum = Total + vat;
                lblTotal.Text = string.Format("{0:#.00}", sum);
                vat = 0;
                sum = 0;
                Total = 0;
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdItems_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
    }
    protected void grdItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdItems.EditIndex = -1;

        BindPOPGrid(int.Parse(hdQuote.Value), int.Parse(ddlProjects.SelectedValue), 0);
    }


    private void BindPOPGrid(int QuoteIDs,int Projectreferences,int wrk)
    {
        QuoteItemsEntity Qe = new QuoteItemsEntity();
        QuoteItemManager obj = new QuoteItemManager();
        IEnumerable<QuoteItemsEntity> ds = obj.SelectQuoteItemsByQuoteID(QuoteIDs, wrk, Projectreferences);
        grdItems.DataSource = ds;
        grdItems.DataBind();
    }
    protected void ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProjects();
    }
    protected void grdProjets_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindGrid();
    }
    protected void grdItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdItems.EditIndex = -1;
        mpopBOM.Show();

        BindPOPGrid(int.Parse(hdQuote.Value), int.Parse(ddlProjects.SelectedValue), 0);
    }
}