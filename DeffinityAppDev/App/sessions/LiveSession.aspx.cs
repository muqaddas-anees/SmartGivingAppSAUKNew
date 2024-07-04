using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.sessions
{
    public partial class LiveSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (QueryStringValues.UNID.Length > 0)
                    {
                        var cRep = new PortfolioRepository<PortfolioMgt.Entity.LiveSeesionDetail>();
                        var value = cRep.GetAll().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                        if (value != null)
                        {
                            lblSessonTitle.Text = value.SessionTitle;
                            lblDescription.Text = value.Description;
                            lblRecordeLink.Text = value.RecordedLink;
                            lblSpeakers.Text = value.Speakers;
                            lblZoomLink.Text = value.ZoomLink;
                            lblDateScheduled.Text = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), value.DateScheduled);
                            frm.Src = value.ZoomLink;
                            //txtLiveSessionTitle.Text = value.SessionTitle;
                            //TextAreaDescription.Text = value.Description;
                            //txtSpeakers.Text = value.Speakers;
                            //txtDateScheduled.Text = value.DateScheduled.Value.ToShortDateString();
                            //CheckBoxEnableTithing.Checked = value.EnableTithing == "True" ? true : false;
                            //txtZoomLink.Text = value.ZoomLink;
                            //txtRecordedLink.Text = value.RecordedLink;
                            //txtZoomUserID.Text = value.metting_userid;
                            //txtZoomAPIKey.Text = value.metting_apikey;
                            //txtZoomSecretCode.Text = value.metting_secrete;
                            //ImagePageBackground.ImageUrl = GetImageUrl(value.unid);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}