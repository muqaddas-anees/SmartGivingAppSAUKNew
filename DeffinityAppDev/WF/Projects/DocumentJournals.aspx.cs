using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.BLL;
public partial class DocumentJournals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["DocumentID"] != null)
            {
                string DocumentID = Request.QueryString["DocumentID"];
                AC2P_DocumentsController _AC2P_DocumentsController = new AC2P_DocumentsController();
                GridView1.DataSource = _AC2P_DocumentsController.GetDocumentJournal(Convert.ToInt32(DocumentID), sessionKeys.PortfolioID);
                GridView1.DataBind();
            }
        }
    }
}


