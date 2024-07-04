using DC.BLL;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using Infragistics.WebUI.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class TithingProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if(sessionKeys.Message.Length >0)
            {
                DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page,sessionKeys.Message, "OK");

                sessionKeys.Message="";
            }
        }
      
    }
}