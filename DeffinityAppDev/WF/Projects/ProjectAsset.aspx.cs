using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProjectAsset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        AssetsId.AssestProject = QueryStringValues.Project;
        AssetsId.AssetType_Assign= 1;//   here i am sending the Project Type
        AssetsId.Assign_Project_Incident = 0;//here i am seding the Inicedent Type
    }
}
