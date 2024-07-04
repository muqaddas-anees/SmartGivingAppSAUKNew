using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.Entity;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class PolicyNumberFormatCtrl : System.Web.UI.UserControl
    {
        IPortfolioRepository<PolicyNumberFormatSetting> pRepository = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    pRepository = new PortfolioRepository<PolicyNumberFormatSetting>();
                    var pe = pRepository.GetAll().FirstOrDefault();
                    if (pe != null)
                    {
                        txtPrefix.Text = pe.Prefix;
                        txtSeed.Text = pe.Seed.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                pRepository = new PortfolioRepository<PolicyNumberFormatSetting>();
                var pe = pRepository.GetAll().FirstOrDefault();
                if (pe != null)
                {
                    pe.Prefix = txtPrefix.Text;
                    pe.Seed = Convert.ToInt32(!string.IsNullOrEmpty(txtSeed.Text.Trim()) ? txtSeed.Text.Trim() : "0");
                    pRepository.Edit(pe);
                }
                else
                {
                    pe = new PolicyNumberFormatSetting();
                    pe.Prefix = txtPrefix.Text;
                    pe.Seed = Convert.ToInt32(!string.IsNullOrEmpty(txtSeed.Text.Trim()) ? txtSeed.Text.Trim() : "0");
                    pRepository.Add(pe);
                }
                lblSuccess.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}