using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeffinityManager.DAL.AssetIconLibraryTableAdapters;
using DeffinityManager.DAL;
/// <summary>
/// Summary description for AssetIconLibraryController
/// </summary>

namespace Deffinity.BLL
{
    
   // private static AssetIconLibraryTableAdapter _AssetIconLibraryTableAdapter;
    
    public class AssetIconLibraryController
    {
        private static AssetIconLibraryTableAdapter _AssetIconLibraryTableAdapter;
        private static AssetMoveIconLibraryTableAdapter _AssetMoveIconLibraryTableAdapter;

        
        #region "Asset Move Icon Lib Details"
        public static AssetMoveIconLibraryTableAdapter AssetMoveIconLibraryAdapter
        {
            get
            {
                if (_AssetMoveIconLibraryTableAdapter == null)
                    _AssetMoveIconLibraryTableAdapter = new AssetMoveIconLibraryTableAdapter();
                return _AssetMoveIconLibraryTableAdapter;

            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public AssetIconLibrary.AssetMoveIconLibraryDataTable GetAssetMoveIconLibrary(int ID)
        {
            return AssetMoveIconLibraryAdapter.GetAssetMoveIconLibrary(ID);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public string GetMoveICONInfo  (int ID)
        {

            string ICONInfo = string.Empty;
            AssetIconLibrary.AssetMoveIconLibraryDataTable ICONInfoDataTable = AssetMoveIconLibraryAdapter.GetAssetMoveIconLibrary(ID);

            if (ICONInfoDataTable.Rows.Count > 0)
            {
                AssetIconLibrary.AssetMoveIconLibraryRow AssetMoveIconLibDataRow = ICONInfoDataTable[0];
                ICONInfo=AssetMoveIconLibDataRow.ICONInfo;
            }
            return ICONInfo;
                
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int AssetMoveIconLibrary_Insert(int MoveRef,string ICONInfo)
        {
            AssetIconLibrary.AssetMoveIconLibraryDataTable AssetMoveIconLibDataTable = new AssetIconLibrary.AssetMoveIconLibraryDataTable();
            AssetIconLibrary.AssetMoveIconLibraryRow AssetMoveIconLibDataRow = AssetMoveIconLibDataTable.NewAssetMoveIconLibraryRow();
            AssetMoveIconLibDataRow.MoveRef = MoveRef;
            AssetMoveIconLibDataRow.ICONInfo = ICONInfo;
            AssetMoveIconLibDataTable.AddAssetMoveIconLibraryRow(AssetMoveIconLibDataRow);
            int rowsAffected =AssetMoveIconLibraryAdapter.Update(AssetMoveIconLibDataTable);            

   
            return rowsAffected = 1;
        }


        #endregion "Asset Move Icon Lib Details"

        #region "Asset Icon Lib Details"
        public static AssetIconLibraryTableAdapter AssetIconLibraryAdapter
        {
            get
            {
                if (_AssetIconLibraryTableAdapter == null)
                    _AssetIconLibraryTableAdapter = new AssetIconLibraryTableAdapter();
                return _AssetIconLibraryTableAdapter;

            }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public AssetIconLibrary.AssetIconLibraryDataTable GetDataAssetIconLibrary()
        {
            
            return AssetIconLibraryAdapter.GetDataAssetIconLibrary();

        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public AssetIconLibrary.AssetIconLibraryDataTable GetDataAssetIconLibraryByID(int ID)
        {

            return AssetIconLibraryAdapter.GetDataAssetIconLibraryByID(ID);

        }
        #endregion "Asset Icon Lib Details"
    }
}