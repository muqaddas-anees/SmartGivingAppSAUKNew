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
using DeffinityManager.DAL.DBAddUserPermissonTableAdapters;
using DeffinityManager.DAL;


/// <summary>
/// Summary description for AddUserPermissionData
/// </summary>
namespace Deffinity.AddUserPermissionData
{
    public class AddUserPermissionData
    {
        private static ModuleDesc_TableAdapter _ModuleDesc_TableAdpater;
        private static SectionDesc_TableAdapter _SectionDesc_TableAdapter;
        private static UsermodelPermission_TableAdapter _UsermodelPermission_TableAdapater;
        public static UsermodelPermission_TableAdapter UserPermissionAdapter
        {
            get
            {
                if (_UsermodelPermission_TableAdapater == null)
                    _UsermodelPermission_TableAdapater = new UsermodelPermission_TableAdapter();
                return _UsermodelPermission_TableAdapater;
            }
        }
        public static SectionDesc_TableAdapter SectionDescAdapter
        {
            get
            {
                if (_SectionDesc_TableAdapter == null)
                    _SectionDesc_TableAdapter = new SectionDesc_TableAdapter();
                return _SectionDesc_TableAdapter;
            }
        }
        public static ModuleDesc_TableAdapter ModuleDescAdapter
        {
            get
            {
                if(_ModuleDesc_TableAdpater == null)
                    _ModuleDesc_TableAdpater = new ModuleDesc_TableAdapter() ;
                return _ModuleDesc_TableAdpater;
            }

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public static int SectionExists(string PageName)
        {
            int retVal = -1;
            object obj = SectionDescAdapter.SectionDesc_PageExist(PageName);
            if (obj != null)
            {
                retVal = Convert.ToInt32(obj);
            }
            return retVal;
        }



        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public static bool CheckPageAccess(int UserId, string PageName)
        {
            bool retVal = true;
            object obj = UserPermissionAdapter.UPermission_AccesByPage(UserId, PageName);
            if (obj != null)
            {
                retVal = Convert.ToBoolean(obj);
            }
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBAddUserPermisson.UsermodelPermissionDataTable CheckPermissionExist(int Userid, int SectionId)
        {
            return UserPermissionAdapter.GetDataByUserandSectionId(Userid, SectionId);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBAddUserPermisson.UsermodelPermissionDataTable SelectAllUserPermission()
        {
            return UserPermissionAdapter.GetDataUserPermissions();
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int InsertUserPermission(int UserId, int SectionId, bool EnableState)
        {
            int retVal=-1;
            retVal = UserPermissionAdapter.Insert(UserId, SectionId, EnableState);
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int UpdateUserPermission(int Id, int UserId, int SectionId, bool EnableState)
        {
            int retval = -1;
            try
            {
                DBAddUserPermisson.UsermodelPermissionDataTable PTable = UserPermissionAdapter.GetDataByUserandSectionId(UserId, SectionId);
                if (PTable.Rows.Count > 0)
                {
                    DBAddUserPermisson.UsermodelPermissionRow PRow = PTable[0];
                    PRow.UserId = UserId;
                    PRow.SectionId = SectionId;
                    PRow.Enable = EnableState;
                    retval = UserPermissionAdapter.Update(PRow);
                }
                
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBAddUserPermisson.SectionDescDataTable CheckSNameExist(string SectionName,int ModuleId)
        {
            return SectionDescAdapter.CheckExistSName(SectionName,ModuleId);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBAddUserPermisson.SectionDescDataTable SelectSectionById(int SectionId)
        {
            return SectionDescAdapter.GetDataBySectionID(SectionId);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBAddUserPermisson.ModuleDescDataTable SelecModuleById(int ModuleID)
        {
            return ModuleDescAdapter.GetDataByModuleID(ModuleID);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]

        public DBAddUserPermisson.ModuleDescDataTable SelectAllModule()
        {
            return ModuleDescAdapter.GetDataModule();
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int InsertModule(string ModuleName)
        {
            int retVal = -1;
            retVal = ModuleDescAdapter.Insert(ModuleName);
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int UpdateModule(int ModuleId, string ModuleName)
        {
            int retVal = -1;
            try
            {
                DBAddUserPermisson.ModuleDescDataTable ModuleTable = ModuleDescAdapter.GetDataByModuleID(ModuleId);
                if (ModuleTable.Rows.Count > 0)
                {
                    DBAddUserPermisson.ModuleDescRow MRow = ModuleTable[0];
                    MRow.ModuleName = ModuleName;
                    retVal = ModuleDescAdapter.Update(MRow);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]

        public int UpdateSection(int SectionId, int ModuleId, string SectionName)
        {
            int retVal = -1;
            try
            {
                DBAddUserPermisson.SectionDescDataTable STable = SectionDescAdapter.GetDataBySectionID(SectionId);
                if (STable.Rows.Count > 0)
                {
                    DBAddUserPermisson.SectionDescRow Srow = STable[0];
                    Srow.ModuleId = ModuleId;
                    Srow.SectionName = SectionName;
                    retVal = SectionDescAdapter.Update(Srow);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }
            return retVal;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBAddUserPermisson.SectionDescDataTable SelectSection(int ModuleId)
        {
            return SectionDescAdapter.GetDataSection(ModuleId);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int InsertSection(int ModuleId, string SectionName)
        {
            int retVal = -1;
            retVal = SectionDescAdapter.Insert(ModuleId, SectionName);
            return retVal;
        }
        public AddUserPermissionData()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}