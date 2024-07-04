using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DeffinityManager.DAL.CLPermissionsTableAdapters;
using DeffinityManager.DAL;
/// <summary>
/// Summary description for CLPermissionCS
/// </summary>
namespace CustomerLogicPermissions
{
    public class CLPermissionCS
    {
        private static CLP_TableAdapter _CLPTableAdapter;
        public static CLP_TableAdapter CLPAdapter
        {
            get
            {
                if(_CLPTableAdapter  == null)
                    _CLPTableAdapter = new CLP_TableAdapter ();
                return _CLPTableAdapter;
            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public bool CheckPermission(int UserId, int ProjectReference)
        {
            bool retval = false;
            object obj;
            obj = CLPAdapter.CLP_CheckPermission(UserId, ProjectReference);

            if (obj != null)
            {
                retval = bool.Parse(obj.ToString());
            }

            return retval;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public CLPermissions.CLPermissionsDataTable SelectByID(int ID)
        {
            return CLPAdapter.GetDataByCPLID(ID);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public CLPermissions.CLPermissionsDataTable SelectPRCustomers(int ProjectReference)
        {
            return CLPAdapter.GetDataByProjectCustomers(ProjectReference);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public CLPermissions.CLPermissionsDataTable SelectUsersByPR(int ProjectReference)
        {
            return CLPAdapter.GetDataByProjectReference(ProjectReference);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public CLPermissions.CLPermissionsDataTable SelectByUIDPR(int UserId, int ProjectReference)
        {
            return CLPAdapter.GetDataCLPermission(UserId, ProjectReference);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int InsertCLPermission(int UserId, int ProjectRefernce, bool AccessPermission)
        {
            int retVal = -1;
            try
            {
                retVal = CLPAdapter.Insert(UserId, ProjectRefernce, AccessPermission);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }

        public int UpdateCLPermission(int Id, int UserId, int ProjectReference, bool AccessPermission)
        {
            int retVal = -1;
            try
            {
                CLPermissions.CLPermissionsDataTable CLPTable = CLPAdapter.GetDataCLPermission(UserId, ProjectReference);
                if (CLPTable.Rows.Count > 0)
                {
                    CLPermissions.CLPermissionsRow CLPRow = CLPTable[0];
                    CLPRow.UserId = UserId;
                    CLPRow.ProjectReference = ProjectReference;
                    CLPRow.AccesPermission = AccessPermission;
                    retVal = CLPAdapter.Update(CLPRow);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }

        public int DeleteCLPermission(int Id)
        {
            int retVal = -1;
            try
            {
                retVal = CLPAdapter.Delete(Id);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }

        public CLPermissionCS()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}