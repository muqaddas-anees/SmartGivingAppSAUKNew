using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class FolderTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            //var folder = new DirectoryInfo(Server.MapPath("WF\\Log"));
            //var security = folder.GetAccessControl();
            //var rule = new FileSystemAccessRule("IIS AppPool\\IIS_IUSRS", FileSystemRights.Modify, AccessControlType.Allow);
            ////
            //security.AddAccessRule(rule);
            //folder.SetAccessControl(security);

            string folderPath= Server.MapPath("WF\\Log");
            DirectorySecurity folderSecurity = Directory.GetAccessControl(Server.MapPath("WF\\Log"));

            // Create a new access rule for the IIS_IUSRS group
            FileSystemAccessRule iisUsersAccessRule = new FileSystemAccessRule("IIS_IUSRS", FileSystemRights.ReadAndExecute | FileSystemRights.Write | FileSystemRights.Modify, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);

            // Add the access rule to the folder's access control list
            folderSecurity.AddAccessRule(iisUsersAccessRule);

            // Apply the updated access control list to the folder
            Directory.SetAccessControl(folderPath, folderSecurity);

        }
    }
}