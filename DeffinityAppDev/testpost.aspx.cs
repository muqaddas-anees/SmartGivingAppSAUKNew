using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class testpost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!IsPostBack)
            {

            }
        }

        private string SilentPost(string url, Dictionary<string, string> fields, string target = "_self")
        {
            var rtn = new StringBuilder($@"<form id=""silentPost"" action=""{url}"" method=""post"" target=""{target}"">");

            foreach (var f in fields)
            {
                rtn.AppendLine($@"<input type=""hidden"" name=""{f.Key}"" value=""{f.Value}"" /> ");
            }

            rtn.AppendLine(@"
                <noscript><input type=""submit"" value=""Continue""></noscript>
                </form >
                <script >
                            window.setTimeout('document.forms.silentPost.submit()', 0);
                </script > ");

            return rtn.ToString();
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            var fields = new Dictionary<string, string>();
            fields.Add("field1", "filed2");
            var restult =  SilentPost("https://www.google.com/", fields, "threeds_acs");
            newform.Text = $"<iframe name=\"threeds_acs\" style=\"height: 420px; width: 420px;\" ></iframe>"+  restult;
        }
    }
}