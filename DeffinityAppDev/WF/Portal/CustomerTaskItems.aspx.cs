using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
//using Flan.FutureControls;
using System.Data.Common;

public partial class CustomerTaskItems : System.Web.UI.Page
{
    DisBindings getdata = new DisBindings();
    int ProjectReference = 0;
    string eventname = "";
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        ProjectReference = Convert.ToInt32(Request.QueryString["Project"].ToString());
        eventname = Request.QueryString["Event"].ToString();
        if (!Page.IsPostBack)
        {
            bingrid();
        }
    }
    public void bingrid()
    {
        SqlDataAdapter adp = new SqlDataAdapter("DN_CustomerTaskItems", con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = ProjectReference;
        adp.SelectCommand.Parameters.Add("@Event", SqlDbType.NVarChar, 50).Value = eventname;
        DataSet ds = new DataSet();
        adp.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected string lblResource(string ItemID)
    {
        string res = "";
        try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetSqlStringCommand("SELECT Contractors.ID, Contractors.ContractorName, ProjectItems.ItemReference FROM ProjectItems INNER JOIN Contractors ON ProjectItems.ContractorID = Contractors.ID where ProjectItems.ItemReference =@ItemReference");
            db.AddInParameter(cmd, "@ItemReference", DbType.Int32, Convert.ToInt32(ItemID));

            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    if (res == "")
                    {
                        res = dr["ContractorName"].ToString();
                    }
                    else
                    {
                        res = res + ", " + dr["ContractorName"].ToString();
                    }

                }
            }

            cmd.Dispose();

        }
        catch (Exception ex)
        {
        }
        return res;
    }
}