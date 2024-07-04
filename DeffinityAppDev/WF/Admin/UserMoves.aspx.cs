using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using Move.DAL;
using Move.Entity;


public partial class UserMoves : System.Web.UI.Page
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "Admin";
            if (Request.QueryString["uid"] != null)
            {
                SelectUserData(Convert.ToInt32(Request.QueryString["uid"]));
            }
        }
           
    }
    protected void btngohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Admin/Adminusers.aspx");
    }
    private void SelectUserData(int cid)
    {

        try
        {
            //edit name panel
            DbCommand cmd = db.GetStoredProcCommand("DN_SelectResource");
            db.AddInParameter(cmd, "@ID", DbType.Int32, cid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {

                    lblusername.Text = dr["ContractorName"].ToString();
                    using(MovesDataContext md=new MovesDataContext())
                    {
                        var result = md.JMoveInformations.Where(j => j.Email == dr["EmailAddress"].ToString()).ToList();
                        gvUserMoves.DataSource = result;
                        gvUserMoves.DataBind();

                        var currentDesk = result.OrderByDescending(r => r.MoveDate).Select(r => r.Desk).FirstOrDefault();
                        if (currentDesk != null)
                        {
                            lblCurrentDesk.Text = currentDesk;
                        }

                    }

                }
                dr.Close();
            }
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
}