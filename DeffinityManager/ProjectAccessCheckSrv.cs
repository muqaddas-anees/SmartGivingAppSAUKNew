using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ProjectMgt.Entity;
using ProjectMgt.BAL;

/// <summary>
/// Summary description for ProjectAccessCheckSrv
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ProjectAccessCheckSrv : System.Web.Services.WebService {
    ProjectAccessCheckBAL pb = null;
    public ProjectAccessCheckSrv () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        pb = new ProjectAccessCheckBAL();
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    public void ProjectAccessInsert(ProjectAccessCheck p)
    {
        pb.ProjectAccessCheckBAL_Insert(p);
    }
    public void ProjectAccessUpdate(ProjectAccessCheck p)
    {
        pb.ProjectAccessCheckBAL_Update(p);
    }
    public IQueryable<ProjectAccessCheck> ProjectAccess_ActiveUser(int ProjectReference)
    {
      return pb.ProjectAccessCheckBAL_SelectByProject(ProjectReference).Where(p=>p.IsActive ==  true);
    }
    public IQueryable<ProjectAccessCheck> ProjectAccess_SelectByProject(int ProjectReference)
    {
        return pb.ProjectAccessCheckBAL_SelectByProject(ProjectReference);
    }
}
