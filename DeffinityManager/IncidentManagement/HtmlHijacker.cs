using System;
using System.Text;

namespace Deffinity.EmailService
{
    /// <summary>
    /// This class provides methods for retrieving the html content of another page.
    /// </summary>
    public class HtmlHijacker
    {
        public HtmlHijacker()
        {

        }
        public string RetrieveBodyFromAnotherPage(string url, ref string errorMessage)
        {
            string markUpText = string.Empty;
            System.Net.WebClient Http = new System.Net.WebClient();

            // Download the Web resource and save it into a data buffer.
            try
            {
                byte[] Result = Http.DownloadData(url);
                markUpText = Encoding.Default.GetString(Result);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                LogExceptions.WriteExceptionLog(ex);
                return null;
            }
            return markUpText;
        }
    }
}
