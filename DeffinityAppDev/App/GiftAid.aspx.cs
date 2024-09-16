using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class GiftAid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { 
            BindSettings();
            }

        }
        protected void BindSettings()
        {
            using (var cont = new PortfolioDataContext())
            {
                var settings = cont.InternationalSettings.FirstOrDefault(o => o.PortfolioID == sessionKeys.PortfolioID);
                if (settings != null)
                {
                    chkGiftAid.Checked = settings.IsGiftAidEnabled ?? false;
                }
                else
                {
                    chkGiftAid.Checked = true;
                }
            }
        }
            protected void btnSave_Click(object sender, EventArgs e)
            {
            using (var cont = new PortfolioDataContext())
            {
                var settings = cont.InternationalSettings.FirstOrDefault(o => o.PortfolioID == sessionKeys.PortfolioID);
                if (settings != null)
                {
                 settings.IsGiftAidEnabled = chkGiftAid.Checked ;
                }
                else
                {
                    var newsettings = new InternationalSetting
                    {
                        PortfolioID = sessionKeys.PortfolioID,
                        IsGiftAidEnabled = chkGiftAid.Checked
                    };
                    cont.InternationalSettings.InsertOnSubmit(newsettings);
                }
                cont.SubmitChanges();
            }

        }
        }
     }