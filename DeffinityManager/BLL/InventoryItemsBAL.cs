using InventoryMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgt.BAL
{
    public class InventoryItemsBAL
    {

        public static bool InventoryItemsBAL_Add(InventoryItem d)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryItem> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryItem>();
            d.PortfolioID = sessionKeys.PortfolioID;
            d.LoggedBy = sessionKeys.UID;
            d.LoggedDate = DateTime.Now;
            IvRep.Add(d);
            retval = true;
            return retval;
        }
        public static bool InventoryItemsBAL_Update(InventoryItem d)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryItem> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryItem>();
            var ditem = IvRep.GetAll().Where(o => o.ID == d.ID).FirstOrDefault();
            if (ditem != null)
            {
                ditem.CategoryID = d.CategoryID;
                ditem.SubCategoryID = d.SubCategoryID;
                ditem.Equipment = d.Equipment;
                ditem.Quantity = d.Quantity;
                ditem.StorageLocationID = d.StorageLocationID;
                ditem.SupplierID = d.SupplierID;
                ditem.Aisle = d.Aisle;
                ditem.Bin = d.Bin;
                ditem.Floor = d.Floor;
                ditem.ReorderLevel = d.ReorderLevel;
                ditem.Shelf = d.Shelf;
                IvRep.Edit(ditem);
                retval = true;
            }
            return retval;
        }
        public static InventoryMgt.Entity.InventoryItem InventoryItemsBAL_SelectByID(int id)
        {
            
            InventoryRepository<InventoryMgt.Entity.InventoryItem> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryItem>();
            return IvRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
          
        }
        public static bool InventoryItemsBAL_DeleteByID(int id)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryItem> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryItem>();
            var d = IvRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (d != null)
            {
                IvRep.Delete(d);

                InventoryRepository<InventoryMgt.Entity.InventoryItemsTransfer> IvtranRep = new InventoryRepository<InventoryMgt.Entity.InventoryItemsTransfer>();
                var vditem = IvtranRep.GetAll().Where(o => o.TransferToInventoryID == id).FirstOrDefault();
                if(vditem != null)
                {
                    IvtranRep.Delete(vditem);
                }

                retval = true;
            }
            return retval;
        }
        public static List<InventoryMgt.Entity.V_InventoryItem> InventoryItemsBAL_Select()
        {
            InventoryRepository<InventoryMgt.Entity.V_InventoryItem> IvRep = new InventoryRepository<InventoryMgt.Entity.V_InventoryItem>();
            return IvRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();

        }

        public static bool InventoryItemsAlloatment_Add(int InventoryId, int Qty,int CallID)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryItemsAlloatment> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryItemsAlloatment>();

            var d = IvRep.GetAll().Where(o => o.InventoryID == InventoryId && o.CallID == CallID).FirstOrDefault();
            if (d == null)
            {
                d = new InventoryMgt.Entity.InventoryItemsAlloatment();
                d.CallID = CallID;
                d.InventoryID = InventoryId;
                d.Quantity = Qty;
                d.AllotedBy = sessionKeys.UID;
                d.AllotmentDate = DateTime.Now;
                d.PortfolioID = sessionKeys.PortfolioID;
                IvRep.Add(d);
                retval = true;
            }
            else
            {
                //d.CallID = CallID;
                //d.InventoryID = InventoryId;
                d.Quantity = d.Quantity + Qty;
                d.AllotedBy = sessionKeys.UID;
                d.AllotmentDate = DateTime.Now;
                d.PortfolioID = sessionKeys.PortfolioID;
                IvRep.Edit(d);
                retval = true;

            }
            return retval;
        }

        public static List<InventoryMgt.Entity.InventoryItemsAlloatment> InventoryItemsAlloatment_Select(int InventoryId, int Qty, int CallID)
        {
            InventoryRepository<InventoryMgt.Entity.InventoryItemsAlloatment> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryItemsAlloatment>();
            return IvRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
        }
        public static List<InventoryMgt.Entity.InventoryItemsAlloatment> InventoryItemsAlloatment_SelectByInventoryID(int InventoryID)
        {
            InventoryRepository<InventoryMgt.Entity.InventoryItemsAlloatment> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryItemsAlloatment>();
            return IvRep.GetAll().Where(o => o.InventoryID == InventoryID).ToList();
        }
        public static List<InventoryMgt.Entity.InventoryItemsAlloatment> InventoryItemsAlloatment_SelectByCallID( int CallID)
        {
            InventoryRepository<InventoryMgt.Entity.InventoryItemsAlloatment> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryItemsAlloatment>();
            return IvRep.GetAll().Where(o => o.CallID == CallID).ToList();
        }
        //delete values
        public static bool InventoryItemsAlloatment_Delete(int InventoryId, int CallID)
        {
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryItemsAlloatment> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryItemsAlloatment>();
            if (CallID > 0 && InventoryId > 0)
            {
                var dlist = IvRep.GetAll().Where(o => o.CallID == CallID && o.InventoryID == InventoryId).ToList();

                IvRep.DeleteAll(dlist);
                retval = true;
            }
             else   if (CallID > 0)
            {
                var dlist = IvRep.GetAll().Where(o => o.CallID == CallID).ToList();

                IvRep.DeleteAll(dlist);
                retval = true;
            }
            else if (InventoryId > 0)
            {
                var dlist = IvRep.GetAll().Where(o =>  o.InventoryID == InventoryId).ToList();
                IvRep.DeleteAll(dlist);
                retval = true;
            }

            return retval;
        }

        public static bool InventoryItemsTransfer_Add(int InventoryID, int TransferQty, int StorageLocation, out string errormsg)
        {
            errormsg = string.Empty;
            bool retval = false;
            InventoryRepository<InventoryMgt.Entity.InventoryItem> IvRep = new InventoryRepository<InventoryMgt.Entity.InventoryItem>();
            InventoryRepository<InventoryMgt.Entity.V_InventoryItem> IvItemRep = new InventoryRepository<InventoryMgt.Entity.V_InventoryItem>();
            InventoryRepository<InventoryMgt.Entity.InventoryItemsTransfer> IvtranRep = new InventoryRepository<InventoryMgt.Entity.InventoryItemsTransfer>();
            var vditem = IvItemRep.GetAll().Where(o => o.ID == InventoryID).FirstOrDefault();
            var ditem = IvRep.GetAll().Where(o => o.ID == InventoryID).FirstOrDefault();
            if (ditem != null)
            {
                if (ditem.StorageLocationID != StorageLocation)
                {
                    if (vditem.Quantity >= TransferQty)
                    {
                        var tItem = IvtranRep.GetAll().Where(o => o.TransferFromInventoryID == InventoryID && o.TransferToLocationID == StorageLocation).FirstOrDefault();
                        if (tItem == null)
                        {
                            ditem.Quantity = ditem.Quantity - TransferQty;
                            IvRep.Edit(ditem);
                            //reduce the quntity in original 
                            var newTransferInventory = new InventoryMgt.Entity.InventoryItem();
                            //newTransferInventory.Aisle = ditem.Aisle;
                           // newTransferInventory.Bin = ditem.Bin;
                            newTransferInventory.CategoryID = ditem.CategoryID;
                            newTransferInventory.Equipment = ditem.Equipment;
                            //newTransferInventory.Floor = ditem.Floor;
                            newTransferInventory.LoggedBy = sessionKeys.UID;
                            newTransferInventory.LoggedDate = DateTime.Now;
                            newTransferInventory.PortfolioID = ditem.PortfolioID;
                            newTransferInventory.Quantity = TransferQty;
                            newTransferInventory.ReorderLevel = ditem.ReorderLevel;
                            //newTransferInventory.Shelf = ditem.Shelf;
                            newTransferInventory.StorageLocationID = StorageLocation;
                            newTransferInventory.SubCategoryID = ditem.SubCategoryID;
                            newTransferInventory.SupplierID = ditem.SupplierID;
                            IvRep.Add(newTransferInventory);
                            //add to transer items
                            if (newTransferInventory.ID > 0)
                            {
                                tItem = new InventoryMgt.Entity.InventoryItemsTransfer();
                                tItem.PortfolioID = newTransferInventory.PortfolioID;
                                tItem.Quantity = Convert.ToInt32(ditem.Quantity);
                                tItem.TransferQuantity = Convert.ToInt32(TransferQty);
                                tItem.TransferBy = sessionKeys.UID;
                                tItem.TransferDate = DateTime.Now;
                                tItem.TransferFromInventoryID = ditem.ID;
                                tItem.TransferToInventoryID = newTransferInventory.ID;
                                tItem.TransferFromLocationID = ditem.StorageLocationID;
                                tItem.TransferToLocationID = newTransferInventory.StorageLocationID;
                                IvtranRep.Add(tItem);
                                retval = true;
                            }
                            else
                            {
                                errormsg = "Faild to transfer.Please try again";
                                retval = false;
                            }
                        }
                        else
                        {
                            //reduce in from item 
                            ditem = IvRep.GetAll().Where(o => o.ID == tItem.TransferFromInventoryID).FirstOrDefault();
                            if (ditem != null)
                            {
                                ditem.Quantity = Convert.ToInt32(ditem.Quantity) - TransferQty;
                                IvRep.Edit(ditem);
                                retval = true;

                            }
                            //add to to item
                            var newTransferInventory = IvRep.GetAll().Where(o => o.ID == tItem.TransferToInventoryID).FirstOrDefault();
                            if(newTransferInventory != null)
                            {
                                newTransferInventory.Quantity = Convert.ToInt32(newTransferInventory.Quantity) + TransferQty;
                                IvRep.Edit(ditem);
                                retval = true;
                            }
                            else
                            {
                                newTransferInventory = new InventoryMgt.Entity.InventoryItem();
                                //newTransferInventory.Aisle = ditem.Aisle;
                                //newTransferInventory.Bin = ditem.Bin;
                                newTransferInventory.CategoryID = ditem.CategoryID;
                                newTransferInventory.Equipment = ditem.Equipment;
                                //newTransferInventory.Floor = ditem.Floor;
                                newTransferInventory.LoggedBy = sessionKeys.UID;
                                newTransferInventory.LoggedDate = DateTime.Now;
                                newTransferInventory.PortfolioID = ditem.PortfolioID;
                                newTransferInventory.Quantity = TransferQty;
                                newTransferInventory.ReorderLevel = ditem.ReorderLevel;
                                //newTransferInventory.Shelf = ditem.Shelf;
                                newTransferInventory.StorageLocationID = StorageLocation;
                                newTransferInventory.SubCategoryID = ditem.SubCategoryID;
                                newTransferInventory.SupplierID = ditem.SupplierID;
                                IvRep.Add(newTransferInventory);
                                if (newTransferInventory.ID > 0)
                                {
                                    tItem = IvtranRep.GetAll().Where(o => o.TransferToInventoryID == newTransferInventory.ID && o.TransferToLocationID == StorageLocation).FirstOrDefault();
                                    if (tItem == null)
                                    {
                                        tItem = new InventoryMgt.Entity.InventoryItemsTransfer();
                                        tItem.PortfolioID = newTransferInventory.PortfolioID;
                                        tItem.Quantity = Convert.ToInt32(ditem.Quantity);
                                        tItem.TransferQuantity = Convert.ToInt32(TransferQty);
                                        tItem.TransferBy = sessionKeys.UID;
                                        tItem.TransferDate = DateTime.Now;
                                        tItem.TransferFromInventoryID = ditem.ID;
                                        tItem.TransferToInventoryID = newTransferInventory.ID;
                                        tItem.TransferFromLocationID = ditem.StorageLocationID;
                                        tItem.TransferToLocationID = newTransferInventory.StorageLocationID;
                                        IvtranRep.Add(tItem);
                                        retval = true;
                                    }
                                   
                                }
                               
                            }
                        }
                    }
                    else
                    {
                        errormsg = "Please enter valid quantity";
                        retval = false;
                    }
                }
                else
                {
                    errormsg = "Please select valid storage location";
                    retval = false;
                }
            }


            return retval;
        }

    }
}
