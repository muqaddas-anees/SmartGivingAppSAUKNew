using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Activity
{
    public partial class AddActivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PortfolioRepository<PortfolioMgt.Entity.InternalActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.InternalActivityDetail>();
                    //pRep.GetAll().Where(o=>o.or)
                    //var rlist = 
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveAndEdit_Click(object sender, EventArgs e)
        {

        }
    }
}