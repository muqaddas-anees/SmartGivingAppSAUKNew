using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DeffinityManager.DAL.GroupReceiptTableAdapters;
using DeffinityManager.DAL;
/// <summary>
/// Summary description for GoodsReceipt
/// </summary>
/// 
namespace GoodsReceiptClass
{
public class Goods_Receipt
{
    private static GoodsReceipt_SelectAllTableAdapter _GoodsReceiptAdapter;
    #region GoodsReceipt
    public static GoodsReceipt_SelectAllTableAdapter GoodsReceiptAdapter
    {
        get {
            if (_GoodsReceiptAdapter == null)
                _GoodsReceiptAdapter = new GoodsReceipt_SelectAllTableAdapter();
            return _GoodsReceiptAdapter;
        }
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select,true)]
    public static GroupReceipt.GoodsReceipt_SelectAllDataTable GetGoodsReceipt()
    {
        
        return GoodsReceiptAdapter.GetGoodsReceiptData();
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]

    public static GroupReceipt.GoodsReceipt_VendorsDataTable GetVendors()
    {
        GoodsReceipt_VendorsTableAdapter vendor = new GoodsReceipt_VendorsTableAdapter();
        return vendor.GetVendorData();
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert,true)]
    public static void  GoodsReceipt_Insert(string Item,int VendorID,DateTime ExpectedShipmentDate,DateTime NextShipmentDate,
        int QtyOrdered, int QtyReceived, string Location, int OutStandingQty, bool AuthorizedPay, string Notes, 
        int ServiceID, int ProjectReference,int type)
    {
        GoodsReceipt_SelectAllTableAdapter ObjInsert = new GoodsReceipt_SelectAllTableAdapter();
        GroupReceipt.GoodsReceipt_SelectAllDataTable GR_Table = new GroupReceipt.GoodsReceipt_SelectAllDataTable();
        GroupReceipt.GoodsReceipt_SelectAllRow GR_NewRow = GR_Table.NewGoodsReceipt_SelectAllRow();
        GR_NewRow.Item = Item;
        GR_NewRow.VendorID = VendorID;
        GR_NewRow.ExpectedShipmentDate = ExpectedShipmentDate;
        GR_NewRow.NextShipmentDate = NextShipmentDate;
        GR_NewRow.QtyOrdered = QtyOrdered;
        GR_NewRow.QtyReceived = QtyReceived;
        GR_NewRow.Location = Location;
        GR_NewRow.OutStandingQty = OutStandingQty;
        GR_NewRow.AuthorizedPay = AuthorizedPay;
        GR_NewRow.Notes = Notes;
        GR_NewRow.ServiceID = ServiceID;
        GR_NewRow.ProjectReference = ProjectReference;
        GR_NewRow.Types = type;

        GR_Table.AddGoodsReceipt_SelectAllRow(GR_NewRow);
       
        GoodsReceiptAdapter.Update(GR_Table);
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public static void ServiceQty_Update(int ID, int QtyReceived)
    {
        DN_CatalogServices_SelectAllTableAdapter objUpdate = new DN_CatalogServices_SelectAllTableAdapter();
        objUpdate.UpdateService(QtyReceived,ID);
        //objUpdate.Update();
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, true)]
    public static void MaterialQty_Update(int ID, int QtyOrdered)
    {
        DN_MaterialServiceCatalogServices_SelectAllTableAdapter objUpdate = new DN_MaterialServiceCatalogServices_SelectAllTableAdapter();
        objUpdate.GetUpdateMaterialCatlog(QtyOrdered, ID);
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select,true)]
    public static GroupReceipt.GoodsReceipt_SelectAllDataTable GoodsReceipt_ByServiceID(int ServiceID,int type)
    {
        GoodsReceipt_SelectAllTableAdapter ObjInsert = new GoodsReceipt_SelectAllTableAdapter();

        return GoodsReceiptAdapter.GetGoodsDataByServiceID(ServiceID,type);
    }
    #endregion
}
}