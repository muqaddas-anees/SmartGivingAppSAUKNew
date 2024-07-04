using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace DeffinityManager
{
    public class ShowMessages
    {

        public static void ShowSuccessMsg(Page page, string title_msg, string msg)
        {
            string script = "window.onload = function() { toastr.success('" + title_msg + "', '" + msg + "'); };";
            page.ClientScript.RegisterStartupScript(page.GetType(), "toastrsuccess", script, true);
        }

        public static void ShowSuccessError(Page page, string title_msg, string msg)
        {
            string script = "window.onload = function() { toastr.error('" + title_msg + "', '" + msg + "'); };";
            page.ClientScript.RegisterStartupScript(page.GetType(), "toastrerror", script, true);
        }

        public static void ShowSuccessWarning(Page page, string title_msg, string msg)
        {
            string script = "window.onload = function() { toastr.warning('" + title_msg + "', '" + msg + "'); };";
            page.ClientScript.RegisterStartupScript(page.GetType(), "toastrWarning", script, true);
        }


        public static void ShowSuccessAlert(Page page, string title_msg, string buttonname="0K")
        {
            string script = "window.onload = function() { showswal('" + title_msg + "', '" + buttonname + "'); };";
            page.ClientScript.RegisterStartupScript(page.GetType(), "sweetalert", script, true);
        }

        public static void ShowErrorAlert(Page page, string title_msg, string buttonname = "0K")
        {
            string script = "window.onload = function() { showswalerror('" + title_msg + "', '" + buttonname + "'); };";
            page.ClientScript.RegisterStartupScript(page.GetType(), "sweetalerterror", script, true);
        }
    }
}
