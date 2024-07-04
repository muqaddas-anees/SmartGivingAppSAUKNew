using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for LogExceptions
/// </summary>
public class LogExceptions
{
    public static string _oldLog = string.Format("OldLog_{0:yyyyMMddHHmmss}.txt", System.DateTime.Now);
	public LogExceptions()
	{
		//
		// TODO: Add constructor logic here
		//
	}    
    public static void LogException(string _errMsg)
    {
        try
        {
            if (_errMsg != "Thread was being aborted.")
            {
                // Declaration Of Variables Used in The Function	
                string _FileName;
                string _Path;
                string _dirPath;
                if (HttpContext.Current != null)
                {
                    _Path = HttpContext.Current.Server.MapPath("~/WF/Log/");

                    if (!Directory.Exists(_Path))
                    {
                        try
                        {
                            Directory.CreateDirectory(_Path);
                        }
                        catch (UnauthorizedAccessException UnAuthorized)
                        {
                            throw UnAuthorized;
                        }
                        finally
                        {
                        }
                    }

                    _dirPath = _Path;
                    _FileName = _dirPath + "ErrorLog.txt";

                    if (File.Exists(_FileName))
                    {
                        FileInfo _fi = new FileInfo(_FileName);
                        long _fileSize = _fi.Length;
                        if (_fileSize >= 102400)
                        {

                            if (File.Exists(_dirPath + _oldLog))
                                File.Delete(_dirPath + _oldLog);
                            _fi.MoveTo(_dirPath + _oldLog);
                            File.Delete(_FileName);
                        }
                    }

                    StreamWriter sw = new StreamWriter(_FileName, true);
                    sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("Date: " + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToLongTimeString());
                    sw.WriteLine("Description: " + _errMsg);
                    sw.WriteLine("Location: " + HttpContext.Current.Request.Url.ToString());
                    //sw.WriteLine("User Name: " + sessionKeys.UName.ToString());
                    //sw.WriteLine("Portfolio: " + sessionKeys.PortfolioName.ToString());
                    sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------");
                    sw.Close();
                }
            }
        }
        catch (Exception ex)
        { }
        
    }

    public static void LogException(string _errMsg,string _Location)
    {
        try
        {
            if (_errMsg != "Thread was being aborted.")
            {
                // Declaration Of Variables Used in The Function	
                string _FileName;
                string _Path;
                string _dirPath;

                if (HttpContext.Current != null)
                {
                    _Path = HttpContext.Current.Server.MapPath("~/WF/Log/");

                    if (!Directory.Exists(_Path))
                    {
                        try
                        {
                            Directory.CreateDirectory(_Path);
                        }
                        catch (UnauthorizedAccessException UnAuthorized)
                        {
                            throw UnAuthorized;
                        }
                        finally
                        {
                        }
                    }

                    _dirPath = _Path;
                    _FileName = _dirPath + "ErrorLog.txt";

                    if (File.Exists(_FileName))
                    {
                        FileInfo _fi = new FileInfo(_FileName);
                        long _fileSize = _fi.Length;
                        if (_fileSize >= 102400)
                        {

                            if (File.Exists(_dirPath + _oldLog))
                                File.Delete(_dirPath + _oldLog);
                            _fi.MoveTo(_dirPath + _oldLog);
                            File.Delete(_FileName);
                        }
                    }

                    StreamWriter sw = new StreamWriter(_FileName, true);
                    sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("Date: " + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToLongTimeString());
                    sw.WriteLine("Description: " + _errMsg);
                    sw.WriteLine("Location: " + HttpContext.Current.Request.Url.ToString());
                    sw.WriteLine("Description: " + _Location);
                    sw.WriteLine("User Name: " + sessionKeys.UName.ToString());
                    sw.WriteLine("Portfolio: " + sessionKeys.PortfolioName.ToString());
                    sw.WriteLine("-------------------------------------------------------------------------------------------------------------------------");
                    sw.Close();
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }
	
	///<summary>
    ///Writes the passed exception details into the log file.  
    ///Writes the exception datails including: message, source, stack trace, target site, inner exception, data.
    /// </summary>
    public static void WriteExceptionLog(Exception ex)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Message: " + ex.Message);
        sb.Append("\nSource : " + ex.Source);
        sb.Append("\nStack Trace: " + ex.StackTrace);
        sb.Append("\nTarget Site:" + ex.TargetSite);
        sb.Append("\nInner Exception: " + ex.InnerException);
        sb.Append("\nData: " + ex.Data);
        LogExceptions.LogException(sb.ToString());
    }
	
    public void ExceptionRedirect()
    {
        HttpContext.Current.Server.Transfer("~/WF/Message.aspx");
    }
}
