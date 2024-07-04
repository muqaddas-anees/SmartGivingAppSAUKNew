using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.Events.controls
{
    public partial class EventTabs : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected string getUrl(int i)
        {
            string[] array_path = {"BasicInfo.aspx","Details.aspx","Speakers.aspx","Tickets.aspx",
                              "Publish.aspx","sponsors_List.aspx"};

            if (QueryStringValues.UNID.Length >0)
            {
                //if (i == 4)
                //    return array_path[i] + "?unid=" + QueryStringValues.UNID;
                //else
                //    return array_path[i] + "?CCID=" + QueryStringValues.CCID + "&callid=" + QueryStringValues.CallID + "&SDID=" + QueryStringValues.CallID;
                return array_path[i] + "?unid=" + QueryStringValues.UNID;
            }
            //else
            //{
            //    return array_path[0];
            //}
            else
                if (i == 0)
            {
                return array_path[i];
            }
            else
                return "#";

        }
    }
}