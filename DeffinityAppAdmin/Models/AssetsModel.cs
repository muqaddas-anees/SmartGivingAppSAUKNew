using AssetsMgr.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.Models
{
    public class AssetsModel
    {
        [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Assets")]
        public partial class Assets 
        {
            //public int image { get; set; }

            private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

            private int _ID;

            private string _SerialNo;

            private string _AssetNo;

            private System.Nullable<int> _Make;

            private System.Nullable<int> _Model;

            private System.Nullable<int> _Type;

            private System.Nullable<int> _FromSite;

            private string _FromBuilding;

            private string _FromFloor;

            private string _FromRoom;

            private string _FromLocation;

            private System.Nullable<System.DateTime> _Datemoved;

            private System.Nullable<System.DateTime> _Datecommision;

            private string _FromPort;

            private string _FromOwner;

            private string _FromNotes;

            private string _Technical;

            private System.Nullable<int> _ProjectReference;

            private string _userid;

            private string _FromVLAN;

            private string _FromIPAddress;

            private string _FromSubnet;

            private System.Nullable<int> _NewAsset;

            private System.Nullable<int> _PortfolioID;

            private System.Nullable<int> _ToSite;

            private string _ToBuilding;

            private string _ToFloor;

            private string _ToRoom;

            private string _ToLocation;

            private string _ToIPAddress;

            private string _ToSubnet;

            private string _ToPort;

            private string _ToNotes;

            private string _ToOwner;

            private string _ToVLAN;

            private System.Nullable<bool> _Approve;

            private System.Nullable<int> _AssetsTypeID;

            private System.Nullable<int> _AssignType;

            private System.Nullable<int> _AssignName;

            private System.Nullable<int> _VendorID;

            private System.Nullable<System.DateTime> _PurchasedDate;

            private System.Nullable<System.DateTime> _ExpDate;

            private System.Nullable<double> _AssestValue;

            private System.Nullable<int> _StatusId;

            private System.Nullable<int> _ContactID;

            private System.Nullable<int> _ContactAddressID;

            private System.Nullable<int> _Image;

            private EntityRef<Asset_Status> _Asset_Status;

            #region Extensibility Method Definitions
            partial void OnLoaded();
            partial void OnValidate(System.Data.Linq.ChangeAction action);
            partial void OnCreated();
            partial void OnIDChanging(int value);
            partial void OnIDChanged();
            partial void OnSerialNoChanging(string value);
            partial void OnSerialNoChanged();
            partial void OnAssetNoChanging(string value);
            partial void OnAssetNoChanged();
            partial void OnMakeChanging(System.Nullable<int> value);
            partial void OnMakeChanged();
            partial void OnModelChanging(System.Nullable<int> value);
            partial void OnModelChanged();
            partial void OnTypeChanging(System.Nullable<int> value);
            partial void OnTypeChanged();
            partial void OnFromSiteChanging(System.Nullable<int> value);
            partial void OnFromSiteChanged();
            partial void OnFromBuildingChanging(string value);
            partial void OnFromBuildingChanged();
            partial void OnFromFloorChanging(string value);
            partial void OnFromFloorChanged();
            partial void OnFromRoomChanging(string value);
            partial void OnFromRoomChanged();
            partial void OnFromLocationChanging(string value);
            partial void OnFromLocationChanged();
            partial void OnDatemovedChanging(System.Nullable<System.DateTime> value);
            partial void OnDatemovedChanged();
            partial void OnDatecommisionChanging(System.Nullable<System.DateTime> value);
            partial void OnDatecommisionChanged();
            partial void OnFromPortChanging(string value);
            partial void OnFromPortChanged();
            partial void OnFromOwnerChanging(string value);
            partial void OnFromOwnerChanged();
            partial void OnFromNotesChanging(string value);
            partial void OnFromNotesChanged();
            partial void OnTechnicalChanging(string value);
            partial void OnTechnicalChanged();
            partial void OnProjectReferenceChanging(System.Nullable<int> value);
            partial void OnProjectReferenceChanged();
            partial void OnuseridChanging(string value);
            partial void OnuseridChanged();
            partial void OnFromVLANChanging(string value);
            partial void OnFromVLANChanged();
            partial void OnFromIPAddressChanging(string value);
            partial void OnFromIPAddressChanged();
            partial void OnFromSubnetChanging(string value);
            partial void OnFromSubnetChanged();
            partial void OnNewAssetChanging(System.Nullable<int> value);
            partial void OnNewAssetChanged();
            partial void OnPortfolioIDChanging(System.Nullable<int> value);
            partial void OnPortfolioIDChanged();
            partial void OnToSiteChanging(System.Nullable<int> value);
            partial void OnToSiteChanged();
            partial void OnToBuildingChanging(string value);
            partial void OnToBuildingChanged();
            partial void OnToFloorChanging(string value);
            partial void OnToFloorChanged();
            partial void OnToRoomChanging(string value);
            partial void OnToRoomChanged();
            partial void OnToLocationChanging(string value);
            partial void OnToLocationChanged();
            partial void OnToIPAddressChanging(string value);
            partial void OnToIPAddressChanged();
            partial void OnToSubnetChanging(string value);
            partial void OnToSubnetChanged();
            partial void OnToPortChanging(string value);
            partial void OnToPortChanged();
            partial void OnToNotesChanging(string value);
            partial void OnToNotesChanged();
            partial void OnToOwnerChanging(string value);
            partial void OnToOwnerChanged();
            partial void OnToVLANChanging(string value);
            partial void OnToVLANChanged();
            partial void OnApproveChanging(System.Nullable<bool> value);
            partial void OnApproveChanged();
            partial void OnAssetsTypeIDChanging(System.Nullable<int> value);
            partial void OnAssetsTypeIDChanged();
            partial void OnAssignTypeChanging(System.Nullable<int> value);
            partial void OnAssignTypeChanged();
            partial void OnAssignNameChanging(System.Nullable<int> value);
            partial void OnAssignNameChanged();
            partial void OnVendorIDChanging(System.Nullable<int> value);
            partial void OnVendorIDChanged();
            partial void OnPurchasedDateChanging(System.Nullable<System.DateTime> value);
            partial void OnPurchasedDateChanged();
            partial void OnExpDateChanging(System.Nullable<System.DateTime> value);
            partial void OnExpDateChanged();
            partial void OnAssestValueChanging(System.Nullable<double> value);
            partial void OnAssestValueChanged();
            partial void OnStatusIdChanging(System.Nullable<int> value);
            partial void OnStatusIdChanged();
            partial void OnContactIDChanging(System.Nullable<int> value);
            partial void OnContactIDChanged();
            partial void OnContactAddressIDChanging(System.Nullable<int> value);
            partial void OnContactAddressIDChanged();

            partial void OnImageChanging(System.Nullable<int> value);
            partial void OnImageChanged();
            #endregion

            public Assets()
            {
                this._Asset_Status = default(EntityRef<Asset_Status>);
                OnCreated();
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
            public int ID
            {
                get
                {
                    return this._ID;
                }
                set
                {
                    if ((this._ID != value))
                    {
                        this.OnIDChanging(value);
                        
                        this._ID = value;
                        this.SendPropertyChanged("ID");
                        this.OnIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SerialNo", DbType = "NVarChar(50)")]
            public string SerialNo
            {
                get
                {
                    return this._SerialNo;
                }
                set
                {
                    if ((this._SerialNo != value))
                    {
                        this.OnSerialNoChanging(value);
                        
                        this._SerialNo = value;
                        this.SendPropertyChanged("SerialNo");
                        this.OnSerialNoChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssetNo", DbType = "NVarChar(50)")]
            public string AssetNo
            {
                get
                {
                    return this._AssetNo;
                }
                set
                {
                    if ((this._AssetNo != value))
                    {
                        this.OnAssetNoChanging(value);
                        
                        this._AssetNo = value;
                        this.SendPropertyChanged("AssetNo");
                        this.OnAssetNoChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Make", DbType = "Int")]
            public System.Nullable<int> Make
            {
                get
                {
                    return this._Make;
                }
                set
                {
                    if ((this._Make != value))
                    {
                        this.OnMakeChanging(value);
                        
                        this._Make = value;
                        this.SendPropertyChanged("Make");
                        this.OnMakeChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Model", DbType = "Int")]
            public System.Nullable<int> Model
            {
                get
                {
                    return this._Model;
                }
                set
                {
                    if ((this._Model != value))
                    {
                        this.OnModelChanging(value);
                        
                        this._Model = value;
                        this.SendPropertyChanged("Model");
                        this.OnModelChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type", DbType = "Int")]
            public System.Nullable<int> Type
            {
                get
                {
                    return this._Type;
                }
                set
                {
                    if ((this._Type != value))
                    {
                        this.OnTypeChanging(value);
                        
                        this._Type = value;
                        this.SendPropertyChanged("Type");
                        this.OnTypeChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromSite", DbType = "Int")]
            public System.Nullable<int> FromSite
            {
                get
                {
                    return this._FromSite;
                }
                set
                {
                    if ((this._FromSite != value))
                    {
                        this.OnFromSiteChanging(value);
                        
                        this._FromSite = value;
                        this.SendPropertyChanged("FromSite");
                        this.OnFromSiteChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromBuilding", DbType = "NVarChar(50)")]
            public string FromBuilding
            {
                get
                {
                    return this._FromBuilding;
                }
                set
                {
                    if ((this._FromBuilding != value))
                    {
                        this.OnFromBuildingChanging(value);
                        
                        this._FromBuilding = value;
                        this.SendPropertyChanged("FromBuilding");
                        this.OnFromBuildingChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromFloor", DbType = "NVarChar(50)")]
            public string FromFloor
            {
                get
                {
                    return this._FromFloor;
                }
                set
                {
                    if ((this._FromFloor != value))
                    {
                        this.OnFromFloorChanging(value);
                        
                        this._FromFloor = value;
                        this.SendPropertyChanged("FromFloor");
                        this.OnFromFloorChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromRoom", DbType = "NVarChar(50)")]
            public string FromRoom
            {
                get
                {
                    return this._FromRoom;
                }
                set
                {
                    if ((this._FromRoom != value))
                    {
                        this.OnFromRoomChanging(value);
                        
                        this._FromRoom = value;
                        this.SendPropertyChanged("FromRoom");
                        this.OnFromRoomChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromLocation", DbType = "VarChar(50)")]
            public string FromLocation
            {
                get
                {
                    return this._FromLocation;
                }
                set
                {
                    if ((this._FromLocation != value))
                    {
                        this.OnFromLocationChanging(value);
                        
                        this._FromLocation = value;
                        this.SendPropertyChanged("FromLocation");
                        this.OnFromLocationChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Datemoved", DbType = "SmallDateTime")]
            public System.Nullable<System.DateTime> Datemoved
            {
                get
                {
                    return this._Datemoved;
                }
                set
                {
                    if ((this._Datemoved != value))
                    {
                        this.OnDatemovedChanging(value);
                        
                        this._Datemoved = value;
                        this.SendPropertyChanged("Datemoved");
                        this.OnDatemovedChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Datecommision", DbType = "SmallDateTime")]
            public System.Nullable<System.DateTime> Datecommision
            {
                get
                {
                    return this._Datecommision;
                }
                set
                {
                    if ((this._Datecommision != value))
                    {
                        this.OnDatecommisionChanging(value);
                        
                        this._Datecommision = value;
                        this.SendPropertyChanged("Datecommision");
                        this.OnDatecommisionChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromPort", DbType = "VarChar(50)")]
            public string FromPort
            {
                get
                {
                    return this._FromPort;
                }
                set
                {
                    if ((this._FromPort != value))
                    {
                        this.OnFromPortChanging(value);
                        
                        this._FromPort = value;
                        this.SendPropertyChanged("FromPort");
                        this.OnFromPortChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromOwner", DbType = "NVarChar(50)")]
            public string FromOwner
            {
                get
                {
                    return this._FromOwner;
                }
                set
                {
                    if ((this._FromOwner != value))
                    {
                        this.OnFromOwnerChanging(value);
                        
                        this._FromOwner = value;
                        this.SendPropertyChanged("FromOwner");
                        this.OnFromOwnerChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromNotes", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
            public string FromNotes
            {
                get
                {
                    return this._FromNotes;
                }
                set
                {
                    if ((this._FromNotes != value))
                    {
                        this.OnFromNotesChanging(value);
                        
                        this._FromNotes = value;
                        this.SendPropertyChanged("FromNotes");
                        this.OnFromNotesChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Technical", DbType = "NVarChar(50)")]
            public string Technical
            {
                get
                {
                    return this._Technical;
                }
                set
                {
                    if ((this._Technical != value))
                    {
                        this.OnTechnicalChanging(value);
                        
                        this._Technical = value;
                        this.SendPropertyChanged("Technical");
                        this.OnTechnicalChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProjectReference", DbType = "Int")]
            public System.Nullable<int> ProjectReference
            {
                get
                {
                    return this._ProjectReference;
                }
                set
                {
                    if ((this._ProjectReference != value))
                    {
                        this.OnProjectReferenceChanging(value);
                        
                        this._ProjectReference = value;
                        this.SendPropertyChanged("ProjectReference");
                        this.OnProjectReferenceChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_userid", DbType = "VarChar(50)")]
            public string userid
            {
                get
                {
                    return this._userid;
                }
                set
                {
                    if ((this._userid != value))
                    {
                        this.OnuseridChanging(value);
                        
                        this._userid = value;
                        this.SendPropertyChanged("userid");
                        this.OnuseridChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromVLAN", DbType = "NVarChar(50)")]
            public string FromVLAN
            {
                get
                {
                    return this._FromVLAN;
                }
                set
                {
                    if ((this._FromVLAN != value))
                    {
                        this.OnFromVLANChanging(value);
                        
                        this._FromVLAN = value;
                        this.SendPropertyChanged("FromVLAN");
                        this.OnFromVLANChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromIPAddress", DbType = "NVarChar(50)")]
            public string FromIPAddress
            {
                get
                {
                    return this._FromIPAddress;
                }
                set
                {
                    if ((this._FromIPAddress != value))
                    {
                        this.OnFromIPAddressChanging(value);
                        
                        this._FromIPAddress = value;
                        this.SendPropertyChanged("FromIPAddress");
                        this.OnFromIPAddressChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromSubnet", DbType = "NVarChar(50)")]
            public string FromSubnet
            {
                get
                {
                    return this._FromSubnet;
                }
                set
                {
                    if ((this._FromSubnet != value))
                    {
                        this.OnFromSubnetChanging(value);
                        
                        this._FromSubnet = value;
                        this.SendPropertyChanged("FromSubnet");
                        this.OnFromSubnetChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NewAsset", DbType = "Int")]
            public System.Nullable<int> NewAsset
            {
                get
                {
                    return this._NewAsset;
                }
                set
                {
                    if ((this._NewAsset != value))
                    {
                        this.OnNewAssetChanging(value);
                        
                        this._NewAsset = value;
                        this.SendPropertyChanged("NewAsset");
                        this.OnNewAssetChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PortfolioID", DbType = "Int")]
            public System.Nullable<int> PortfolioID
            {
                get
                {
                    return this._PortfolioID;
                }
                set
                {
                    if ((this._PortfolioID != value))
                    {
                        this.OnPortfolioIDChanging(value);
                        
                        this._PortfolioID = value;
                        this.SendPropertyChanged("PortfolioID");
                        this.OnPortfolioIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToSite", DbType = "Int")]
            public System.Nullable<int> ToSite
            {
                get
                {
                    return this._ToSite;
                }
                set
                {
                    if ((this._ToSite != value))
                    {
                        this.OnToSiteChanging(value);
                        
                        this._ToSite = value;
                        this.SendPropertyChanged("ToSite");
                        this.OnToSiteChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToBuilding", DbType = "NVarChar(50)")]
            public string ToBuilding
            {
                get
                {
                    return this._ToBuilding;
                }
                set
                {
                    if ((this._ToBuilding != value))
                    {
                        this.OnToBuildingChanging(value);
                        
                        this._ToBuilding = value;
                        this.SendPropertyChanged("ToBuilding");
                        this.OnToBuildingChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToFloor", DbType = "NVarChar(50)")]
            public string ToFloor
            {
                get
                {
                    return this._ToFloor;
                }
                set
                {
                    if ((this._ToFloor != value))
                    {
                        this.OnToFloorChanging(value);
                        
                        this._ToFloor = value;
                        this.SendPropertyChanged("ToFloor");
                        this.OnToFloorChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToRoom", DbType = "NVarChar(50)")]
            public string ToRoom
            {
                get
                {
                    return this._ToRoom;
                }
                set
                {
                    if ((this._ToRoom != value))
                    {
                        this.OnToRoomChanging(value);
                        
                        this._ToRoom = value;
                        this.SendPropertyChanged("ToRoom");
                        this.OnToRoomChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToLocation", DbType = "NVarChar(50)")]
            public string ToLocation
            {
                get
                {
                    return this._ToLocation;
                }
                set
                {
                    if ((this._ToLocation != value))
                    {
                        this.OnToLocationChanging(value);
                        
                        this._ToLocation = value;
                        this.SendPropertyChanged("ToLocation");
                        this.OnToLocationChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToIPAddress", DbType = "NVarChar(50)")]
            public string ToIPAddress
            {
                get
                {
                    return this._ToIPAddress;
                }
                set
                {
                    if ((this._ToIPAddress != value))
                    {
                        this.OnToIPAddressChanging(value);
                        
                        this._ToIPAddress = value;
                        this.SendPropertyChanged("ToIPAddress");
                        this.OnToIPAddressChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToSubnet", DbType = "NVarChar(50)")]
            public string ToSubnet
            {
                get
                {
                    return this._ToSubnet;
                }
                set
                {
                    if ((this._ToSubnet != value))
                    {
                        this.OnToSubnetChanging(value);
                        
                        this._ToSubnet = value;
                        this.SendPropertyChanged("ToSubnet");
                        this.OnToSubnetChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToPort", DbType = "NVarChar(50)")]
            public string ToPort
            {
                get
                {
                    return this._ToPort;
                }
                set
                {
                    if ((this._ToPort != value))
                    {
                        this.OnToPortChanging(value);
                        
                        this._ToPort = value;
                        this.SendPropertyChanged("ToPort");
                        this.OnToPortChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToNotes", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
            public string ToNotes
            {
                get
                {
                    return this._ToNotes;
                }
                set
                {
                    if ((this._ToNotes != value))
                    {
                        this.OnToNotesChanging(value);
                        
                        this._ToNotes = value;
                        this.SendPropertyChanged("ToNotes");
                        this.OnToNotesChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToOwner", DbType = "NVarChar(50)")]
            public string ToOwner
            {
                get
                {
                    return this._ToOwner;
                }
                set
                {
                    if ((this._ToOwner != value))
                    {
                        this.OnToOwnerChanging(value);
                        
                        this._ToOwner = value;
                        this.SendPropertyChanged("ToOwner");
                        this.OnToOwnerChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToVLAN", DbType = "NVarChar(50)")]
            public string ToVLAN
            {
                get
                {
                    return this._ToVLAN;
                }
                set
                {
                    if ((this._ToVLAN != value))
                    {
                        this.OnToVLANChanging(value);
                        
                        this._ToVLAN = value;
                        this.SendPropertyChanged("ToVLAN");
                        this.OnToVLANChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Approve", DbType = "Bit")]
            public System.Nullable<bool> Approve
            {
                get
                {
                    return this._Approve;
                }
                set
                {
                    if ((this._Approve != value))
                    {
                        this.OnApproveChanging(value);
                        
                        this._Approve = value;
                        this.SendPropertyChanged("Approve");
                        this.OnApproveChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssetsTypeID", DbType = "Int")]
            public System.Nullable<int> AssetsTypeID
            {
                get
                {
                    return this._AssetsTypeID;
                }
                set
                {
                    if ((this._AssetsTypeID != value))
                    {
                        this.OnAssetsTypeIDChanging(value);
                        
                        this._AssetsTypeID = value;
                        this.SendPropertyChanged("AssetsTypeID");
                        this.OnAssetsTypeIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssignType", DbType = "Int")]
            public System.Nullable<int> AssignType
            {
                get
                {
                    return this._AssignType;
                }
                set
                {
                    if ((this._AssignType != value))
                    {
                        this.OnAssignTypeChanging(value);
                        
                        this._AssignType = value;
                        this.SendPropertyChanged("AssignType");
                        this.OnAssignTypeChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssignName", DbType = "Int")]
            public System.Nullable<int> AssignName
            {
                get
                {
                    return this._AssignName;
                }
                set
                {
                    if ((this._AssignName != value))
                    {
                        this.OnAssignNameChanging(value);
                        
                        this._AssignName = value;
                        this.SendPropertyChanged("AssignName");
                        this.OnAssignNameChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VendorID", DbType = "Int")]
            public System.Nullable<int> VendorID
            {
                get
                {
                    return this._VendorID;
                }
                set
                {
                    if ((this._VendorID != value))
                    {
                        this.OnVendorIDChanging(value);
                        
                        this._VendorID = value;
                        this.SendPropertyChanged("VendorID");
                        this.OnVendorIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PurchasedDate", DbType = "DateTime")]
            public System.Nullable<System.DateTime> PurchasedDate
            {
                get
                {
                    return this._PurchasedDate;
                }
                set
                {
                    if ((this._PurchasedDate != value))
                    {
                        this.OnPurchasedDateChanging(value);
                        
                        this._PurchasedDate = value;
                        this.SendPropertyChanged("PurchasedDate");
                        this.OnPurchasedDateChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExpDate", DbType = "DateTime")]
            public System.Nullable<System.DateTime> ExpDate
            {
                get
                {
                    return this._ExpDate;
                }
                set
                {
                    if ((this._ExpDate != value))
                    {
                        this.OnExpDateChanging(value);
                        
                        this._ExpDate = value;
                        this.SendPropertyChanged("ExpDate");
                        this.OnExpDateChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssestValue", DbType = "Float")]
            public System.Nullable<double> AssestValue
            {
                get
                {
                    return this._AssestValue;
                }
                set
                {
                    if ((this._AssestValue != value))
                    {
                        this.OnAssestValueChanging(value);
                        
                        this._AssestValue = value;
                        this.SendPropertyChanged("AssestValue");
                        this.OnAssestValueChanged();
                    }
                }
            }
            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Image", DbType = "Int")]
            public System.Nullable<int> Image
            {
                get
                {
                    return this._Image;
                }
                set
                {
                    if ((this._Image != value))
                    {
                        this.OnImageChanging(value);
                        
                        this._Image = value;
                        this.SendPropertyChanged("Image");
                        this.OnImageChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StatusId", DbType = "Int")]
            public System.Nullable<int> StatusId
            {
                get
                {
                    return this._StatusId;
                }
                set
                {
                    if ((this._StatusId != value))
                    {
                        if (this._Asset_Status.HasLoadedOrAssignedValue)
                        {
                            throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                        }
                        this.OnStatusIdChanging(value);
                        
                        this._StatusId = value;
                        this.SendPropertyChanged("StatusId");
                        this.OnStatusIdChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ContactID", DbType = "Int")]
            public System.Nullable<int> ContactID
            {
                get
                {
                    return this._ContactID;
                }
                set
                {
                    if ((this._ContactID != value))
                    {
                        this.OnContactIDChanging(value);
                        
                        this._ContactID = value;
                        this.SendPropertyChanged("ContactID");
                        this.OnContactIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ContactAddressID", DbType = "Int")]
            public System.Nullable<int> ContactAddressID
            {
                get
                {
                    return this._ContactAddressID;
                }
                set
                {
                    if ((this._ContactAddressID != value))
                    {
                        this.OnContactAddressIDChanging(value);
                        
                        this._ContactAddressID = value;
                        this.SendPropertyChanged("ContactAddressID");
                        this.OnContactAddressIDChanged();
                    }
                }
            }

           

            public event PropertyChangingEventHandler PropertyChanging;

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void SendPropertyChanging()
            {
                if ((this.PropertyChanging != null))
                {
                    this.PropertyChanging(this, emptyChangingEventArgs);
                }
            }

            protected virtual void SendPropertyChanged(String propertyName)
            {
                if ((this.PropertyChanged != null))
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        //[global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Assetstype")]
        //public partial class Assetstype
        //{

        //    private int _TypeID;

        //    private string _Type;

        //    public Assetstype()
        //    {
        //    }

        //    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TypeID", AutoSync = AutoSync.Always, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
        //    public int TypeID
        //    {
        //        get
        //        {
        //            return this._TypeID;
        //        }
        //        set
        //        {
        //            if ((this._TypeID != value))
        //            {
        //                this._TypeID = value;
        //            }
        //        }
        //    }

        //    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type", DbType = "NVarChar(50)")]
        //    public string Type
        //    {
        //        get
        //        {
        //            return this._Type;
        //        }
        //        set
        //        {
        //            if ((this._Type != value))
        //            {
        //                this._Type = value;
        //            }
        //        }
        //    }
        //}

        //[global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Assetsmodel")]
        //public partial class Assetsmodel
        //{

        //    private int _ModelID;

        //    private string _Model;

        //    public Assetsmodel()
        //    {
        //    }

        //    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ModelID", AutoSync = AutoSync.Always, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
        //    public int ModelID
        //    {
        //        get
        //        {
        //            return this._ModelID;
        //        }
        //        set
        //        {
        //            if ((this._ModelID != value))
        //            {
        //                this._ModelID = value;
        //            }
        //        }
        //    }

        //    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Model", DbType = "NVarChar(50)")]
        //    public string Model
        //    {
        //        get
        //        {
        //            return this._Model;
        //        }
        //        set
        //        {
        //            if ((this._Model != value))
        //            {
        //                this._Model = value;
        //            }
        //        }
        //    }
        //}

        //[global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Assetsmake")]
        //public partial class Assetsmake
        //{

        //    private int _MakeID;

        //    private string _Make;

        //    public Assetsmake()
        //    {
        //    }

        //    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MakeID", AutoSync = AutoSync.Always, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
        //    public int MakeID
        //    {
        //        get
        //        {
        //            return this._MakeID;
        //        }
        //        set
        //        {
        //            if ((this._MakeID != value))
        //            {
        //                this._MakeID = value;
        //            }
        //        }
        //    }

        //    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Make", DbType = "NVarChar(50)")]
        //    public string Make
        //    {
        //        get
        //        {
        //            return this._Make;
        //        }
        //        set
        //        {
        //            if ((this._Make != value))
        //            {
        //                this._Make = value;
        //            }
        //        }
        //    }
        //}

        [global::System.Data.Linq.Mapping.TableAttribute(Name = "DC.Categories")]
        public partial class Category
        {

            private int _ID;

            private string _Name;

            private System.Nullable<int> _TypeOfRequestID;

            public Category()
            {
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", AutoSync = AutoSync.Always, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
            public int ID
            {
                get
                {
                    return this._ID;
                }
                set
                {
                    if ((this._ID != value))
                    {
                        this._ID = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(500)")]
            public string Name
            {
                get
                {
                    return this._Name;
                }
                set
                {
                    if ((this._Name != value))
                    {
                        this._Name = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TypeOfRequestID", DbType = "Int")]
            public System.Nullable<int> TypeOfRequestID
            {
                get
                {
                    return this._TypeOfRequestID;
                }
                set
                {
                    if ((this._TypeOfRequestID != value))
                    {
                        this._TypeOfRequestID = value;
                    }
                }
            }
        }

        [global::System.Data.Linq.Mapping.TableAttribute(Name = "DC.SubCategory")]
        public partial class SubCategory
        {

            private int _ID;

            private System.Nullable<int> _CategoryID;

            private string _Name;

            public SubCategory()
            {
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", AutoSync = AutoSync.Always, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
            public int ID
            {
                get
                {
                    return this._ID;
                }
                set
                {
                    if ((this._ID != value))
                    {
                        this._ID = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CategoryID", DbType = "Int")]
            public System.Nullable<int> CategoryID
            {
                get
                {
                    return this._CategoryID;
                }
                set
                {
                    if ((this._CategoryID != value))
                    {
                        this._CategoryID = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "VarChar(1000)")]
            public string Name
            {
                get
                {
                    return this._Name;
                }
                set
                {
                    if ((this._Name != value))
                    {
                        this._Name = value;
                    }
                }
            }
        }

        [global::System.Data.Linq.Mapping.TableAttribute(Name = "DC.ProductModel")]
        public partial class ProductModel : INotifyPropertyChanging, INotifyPropertyChanged
        {

            private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

            private int _ModelID;

            private int _SubCategoryID;

            private string _ModelName;

            #region Extensibility Method Definitions
            partial void OnLoaded();
            partial void OnValidate(System.Data.Linq.ChangeAction action);
            partial void OnCreated();
            partial void OnModelIDChanging(int value);
            partial void OnModelIDChanged();
            partial void OnSubCategoryIDChanging(int value);
            partial void OnSubCategoryIDChanged();
            partial void OnModelNameChanging(string value);
            partial void OnModelNameChanged();
            #endregion

            public ProductModel()
            {
                OnCreated();
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ModelID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
            public int ModelID
            {
                get
                {
                    return this._ModelID;
                }
                set
                {
                    if ((this._ModelID != value))
                    {
                        this.OnModelIDChanging(value);
                        
                        this._ModelID = value;
                        this.SendPropertyChanged("ModelID");
                        this.OnModelIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SubCategoryID", DbType = "Int NOT NULL")]
            public int SubCategoryID
            {
                get
                {
                    return this._SubCategoryID;
                }
                set
                {
                    if ((this._SubCategoryID != value))
                    {
                        this.OnSubCategoryIDChanging(value);
                        
                        this._SubCategoryID = value;
                        this.SendPropertyChanged("SubCategoryID");
                        this.OnSubCategoryIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ModelName", DbType = "VarChar(500)")]
            public string ModelName
            {
                get
                {
                    return this._ModelName;
                }
                set
                {
                    if ((this._ModelName != value))
                    {
                        this.OnModelNameChanging(value);
                        
                        this._ModelName = value;
                        this.SendPropertyChanged("ModelName");
                        this.OnModelNameChanged();
                    }
                }
            }

            public event PropertyChangingEventHandler PropertyChanging;

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void SendPropertyChanging()
            {
                if ((this.PropertyChanging != null))
                {
                    this.PropertyChanging(this, emptyChangingEventArgs);
                }
            }

            protected virtual void SendPropertyChanged(String propertyName)
            {
                if ((this.PropertyChanged != null))
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.WarrantyTerm")]
        public partial class WarrantyTerm : INotifyPropertyChanging, INotifyPropertyChanged
        {

            private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

            private int _ID;

            private string _Name;

            #region Extensibility Method Definitions
            partial void OnLoaded();
            partial void OnValidate(System.Data.Linq.ChangeAction action);
            partial void OnCreated();
            partial void OnIDChanging(int value);
            partial void OnIDChanged();
            partial void OnNameChanging(string value);
            partial void OnNameChanged();
            #endregion

            public WarrantyTerm()
            {
                OnCreated();
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
            public int ID
            {
                get
                {
                    return this._ID;
                }
                set
                {
                    if ((this._ID != value))
                    {
                        this.OnIDChanging(value);
                        
                        this._ID = value;
                        this.SendPropertyChanged("ID");
                        this.OnIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50)")]
            public string Name
            {
                get
                {
                    return this._Name;
                }
                set
                {
                    if ((this._Name != value))
                    {
                        this.OnNameChanging(value);
                        
                        this._Name = value;
                        this.SendPropertyChanged("Name");
                        this.OnNameChanged();
                    }
                }
            }

            public event PropertyChangingEventHandler PropertyChanging;

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void SendPropertyChanging()
            {
                if ((this.PropertyChanging != null))
                {
                    this.PropertyChanging(this, emptyChangingEventArgs);
                }
            }

            protected virtual void SendPropertyChanged(String propertyName)
            {
                if ((this.PropertyChanged != null))
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
        //[global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.AssetAssociatedToContacts")]
        //public partial class AssetAssociatedToContact : INotifyPropertyChanging, INotifyPropertyChanged
        //{

        //    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        //    private int _ID;

        //    private int _ContactId;

        //    private int _AssetId;

        //    private System.Nullable<int> _ContactAddressID;

        //    #region Extensibility Method Definitions
        //    partial void OnLoaded();
        //    partial void OnValidate(System.Data.Linq.ChangeAction action);
        //    partial void OnCreated();
        //    partial void OnIDChanging(int value);
        //    partial void OnIDChanged();
        //    partial void OnContactIdChanging(int value);
        //    partial void OnContactIdChanged();
        //    partial void OnAssetIdChanging(int value);
        //    partial void OnAssetIdChanged();
        //    partial void OnContactAddressIDChanging(System.Nullable<int> value);
        //    partial void OnContactAddressIDChanged();
        //    #endregion

        //    public AssetAssociatedToContact()
        //    {
        //        OnCreated();
        //    }

        //    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        //    public int ID
        //    {
        //        get
        //        {
        //            return this._ID;
        //        }
        //        set
        //        {
        //            if ((this._ID != value))
        //            {
        //                this.OnIDChanging(value);
        //                
        //                this._ID = value;
        //                this.SendPropertyChanged("ID");
        //                this.OnIDChanged();
        //            }
        //        }
        //    }

        //    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ContactId", DbType = "Int NOT NULL")]
        //    public int ContactId
        //    {
        //        get
        //        {
        //            return this._ContactId;
        //        }
        //        set
        //        {
        //            if ((this._ContactId != value))
        //            {
        //                this.OnContactIdChanging(value);
        //                
        //                this._ContactId = value;
        //                this.SendPropertyChanged("ContactId");
        //                this.OnContactIdChanged();
        //            }
        //        }
        //    }

        //    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssetId", DbType = "Int NOT NULL")]
        //    public int AssetId
        //    {
        //        get
        //        {
        //            return this._AssetId;
        //        }
        //        set
        //        {
        //            if ((this._AssetId != value))
        //            {
        //                this.OnAssetIdChanging(value);
        //                
        //                this._AssetId = value;
        //                this.SendPropertyChanged("AssetId");
        //                this.OnAssetIdChanged();
        //            }
        //        }
        //    }

        //    [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ContactAddressID", DbType = "Int")]
        //    public System.Nullable<int> ContactAddressID
        //    {
        //        get
        //        {
        //            return this._ContactAddressID;
        //        }
        //        set
        //        {
        //            if ((this._ContactAddressID != value))
        //            {
        //                this.OnContactAddressIDChanging(value);
        //                
        //                this._ContactAddressID = value;
        //                this.SendPropertyChanged("ContactAddressID");
        //                this.OnContactAddressIDChanged();
        //            }
        //        }
        //    }

        //    public event PropertyChangingEventHandler PropertyChanging;

        //    public event PropertyChangedEventHandler PropertyChanged;

        //    protected virtual void SendPropertyChanging()
        //    {
        //        if ((this.PropertyChanging != null))
        //        {
        //            this.PropertyChanging(this, emptyChangingEventArgs);
        //        }
        //    }

        //    protected virtual void SendPropertyChanged(String propertyName)
        //    {
        //        if ((this.PropertyChanged != null))
        //        {
        //            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //        }
        //    }
        //}


        [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.PortfolioContactAddress")]
        public partial class PortfolioContactAddress
        {

            private int _ID;

            private int _ContactID;

            private string _Address;

            private string _City;

            private string _State;

            private string _PostCode;

            private System.Nullable<int> _PolicyTypeID;

            private string _PolicyNumber;

            private System.Nullable<System.DateTime> _StartDate;

            private System.Nullable<System.DateTime> _ExpiryDate;

            private System.Nullable<int> _DaysRemaining;
            private System.Nullable<double> _Amount;

            private string _BillingName;

            private string _BillingAddress1;

            private string _BillingAddress2;

            private string _BillingCity;

            private string _BillingState;

            private string _BillingZipcode;
            private string _Address2;
            private System.Nullable<int> _LoggedBy;

            private System.Nullable<System.DateTime> _LoggedDatetime;
            public PortfolioContactAddress()
            {
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", AutoSync = AutoSync.Always, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
            public int ID
            {
                get
                {
                    return this._ID;
                }
                set
                {
                    if ((this._ID != value))
                    {
                        this._ID = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ContactID", DbType = "Int NOT NULL")]
            public int ContactID
            {
                get
                {
                    return this._ContactID;
                }
                set
                {
                    if ((this._ContactID != value))
                    {
                        this._ContactID = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Address", DbType = "NVarChar(MAX)")]
            public string Address
            {
                get
                {
                    return this._Address;
                }
                set
                {
                    if ((this._Address != value))
                    {
                        this._Address = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_City", DbType = "NVarChar(250)")]
            public string City
            {
                get
                {
                    return this._City;
                }
                set
                {
                    if ((this._City != value))
                    {
                        this._City = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_State", DbType = "NVarChar(250)")]
            public string State
            {
                get
                {
                    return this._State;
                }
                set
                {
                    if ((this._State != value))
                    {
                        this._State = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PostCode", DbType = "NVarChar(50)")]
            public string PostCode
            {
                get
                {
                    return this._PostCode;
                }
                set
                {
                    if ((this._PostCode != value))
                    {
                        this._PostCode = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PolicyTypeID", DbType = "Int")]
            public System.Nullable<int> PolicyTypeID
            {
                get
                {
                    return this._PolicyTypeID;
                }
                set
                {
                    if ((this._PolicyTypeID != value))
                    {
                        this._PolicyTypeID = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PolicyNumber", DbType = "NVarChar(200)")]
            public string PolicyNumber
            {
                get
                {
                    return this._PolicyNumber;
                }
                set
                {
                    if ((this._PolicyNumber != value))
                    {
                        this._PolicyNumber = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartDate", DbType = "DateTime")]
            public System.Nullable<System.DateTime> StartDate
            {
                get
                {
                    return this._StartDate;
                }
                set
                {
                    if ((this._StartDate != value))
                    {
                        this._StartDate = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExpiryDate", DbType = "DateTime")]
            public System.Nullable<System.DateTime> ExpiryDate
            {
                get
                {
                    return this._ExpiryDate;
                }
                set
                {
                    if ((this._ExpiryDate != value))
                    {
                        this._ExpiryDate = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DaysRemaining", DbType = "Int")]
            public System.Nullable<int> DaysRemaining
            {
                get
                {
                    return this._DaysRemaining;
                }
                set
                {
                    if ((this._DaysRemaining != value))
                    {
                        this._DaysRemaining = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Amount", DbType = "Float")]
            public System.Nullable<double> Amount
            {
                get
                {
                    return this._Amount;
                }
                set
                {
                    if ((this._Amount != value))
                    {

                        this._Amount = value;

                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingName", DbType = "NVarChar(255)")]
            public string BillingName
            {
                get
                {
                    return this._BillingName;
                }
                set
                {
                    if ((this._BillingName != value))
                    {

                        this._BillingName = value;

                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingAddress1", DbType = "NVarChar(MAX)")]
            public string BillingAddress1
            {
                get
                {
                    return this._BillingAddress1;
                }
                set
                {
                    if ((this._BillingAddress1 != value))
                    {

                        this._BillingAddress1 = value;

                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingAddress2", DbType = "NVarChar(MAX)")]
            public string BillingAddress2
            {
                get
                {
                    return this._BillingAddress2;
                }
                set
                {
                    if ((this._BillingAddress2 != value))
                    {

                        this._BillingAddress2 = value;

                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingCity", DbType = "NVarChar(255)")]
            public string BillingCity
            {
                get
                {
                    return this._BillingCity;
                }
                set
                {
                    if ((this._BillingCity != value))
                    {

                        this._BillingCity = value;

                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingState", DbType = "NVarChar(255)")]
            public string BillingState
            {
                get
                {
                    return this._BillingState;
                }
                set
                {
                    if ((this._BillingState != value))
                    {

                        this._BillingState = value;

                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingZipcode", DbType = "NVarChar(20)")]
            public string BillingZipcode
            {
                get
                {
                    return this._BillingZipcode;
                }
                set
                {
                    if ((this._BillingZipcode != value))
                    {

                        this._BillingZipcode = value;

                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Address2", DbType = "NVarChar(MAX)")]
            public string Address2
            {
                get
                {
                    return this._Address2;
                }
                set
                {
                    if ((this._Address2 != value))
                    {
                        this._Address2 = value;
                    }
                }
            }
            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LoggedBy", DbType = "Int")]
            public System.Nullable<int> LoggedBy
            {
                get
                {
                    return this._LoggedBy;
                }
                set
                {
                    if ((this._LoggedBy != value))
                    {

                        this._LoggedBy = value;

                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LoggedDatetime", DbType = "DateTime")]
            public System.Nullable<System.DateTime> LoggedDatetime
            {
                get
                {
                    return this._LoggedDatetime;
                }
                set
                {
                    if ((this._LoggedDatetime != value))
                    {

                        this._LoggedDatetime = value;

                    }
                }
            }
        }


        [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.PortfolioContacts")]
        public partial class PortfolioContact //: INotifyPropertyChanging, INotifyPropertyChanged
        {

            private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

            private int _ID;

            private System.Nullable<int> _PortfolioID;

            private string _Name;

            private string _Title;

            private string _Email;

            private string _Telephone;

            private string _Location;

            private System.Nullable<bool> _Key_Contact;

            private string _Notes;

            private string _Likes_Dislikes;

            private System.Nullable<System.DateTime> _DateOfBirth;

            private string _Address1;

            private string _Address2;

            private string _Country;

            private string _Postcode;

            private string _Fax;

            private string _Mobile;

            private string _County;

            private System.Nullable<bool> _LogintoPortal;

            private System.Nullable<int> _DepartmentID;

            private System.Nullable<bool> _isDisabled;

            private string _City;

            private string _Town;

            private string _BuildingName;

            private System.Nullable<System.DateTime> _DateLogged;

            private System.Nullable<int> _LoggedBy;

            private string _SourceofLead;

            private string _Tags;

            private EntitySet<PortfolioContactAddress> _PortfolioContactAddresses;

            #region Extensibility Method Definitions
            partial void OnLoaded();
            partial void OnValidate(System.Data.Linq.ChangeAction action);
            partial void OnCreated();
            partial void OnIDChanging(int value);
            partial void OnIDChanged();
            partial void OnPortfolioIDChanging(System.Nullable<int> value);
            partial void OnPortfolioIDChanged();
            partial void OnNameChanging(string value);
            partial void OnNameChanged();
            partial void OnTitleChanging(string value);
            partial void OnTitleChanged();
            partial void OnEmailChanging(string value);
            partial void OnEmailChanged();
            partial void OnTelephoneChanging(string value);
            partial void OnTelephoneChanged();
            partial void OnLocationChanging(string value);
            partial void OnLocationChanged();
            partial void OnKey_ContactChanging(System.Nullable<bool> value);
            partial void OnKey_ContactChanged();
            partial void OnNotesChanging(string value);
            partial void OnNotesChanged();
            partial void OnLikes_DislikesChanging(string value);
            partial void OnLikes_DislikesChanged();
            partial void OnDateOfBirthChanging(System.Nullable<System.DateTime> value);
            partial void OnDateOfBirthChanged();
            partial void OnAddress1Changing(string value);
            partial void OnAddress1Changed();
            partial void OnAddress2Changing(string value);
            partial void OnAddress2Changed();
            partial void OnCountryChanging(string value);
            partial void OnCountryChanged();
            partial void OnPostcodeChanging(string value);
            partial void OnPostcodeChanged();
            partial void OnFaxChanging(string value);
            partial void OnFaxChanged();
            partial void OnMobileChanging(string value);
            partial void OnMobileChanged();
            partial void OnCountyChanging(string value);
            partial void OnCountyChanged();
            partial void OnLogintoPortalChanging(System.Nullable<bool> value);
            partial void OnLogintoPortalChanged();
            partial void OnDepartmentIDChanging(System.Nullable<int> value);
            partial void OnDepartmentIDChanged();
            partial void OnisDisabledChanging(System.Nullable<bool> value);
            partial void OnisDisabledChanged();
            partial void OnCityChanging(string value);
            partial void OnCityChanged();
            partial void OnTownChanging(string value);
            partial void OnTownChanged();
            partial void OnBuildingNameChanging(string value);
            partial void OnBuildingNameChanged();
            partial void OnDateLoggedChanging(System.Nullable<System.DateTime> value);
            partial void OnDateLoggedChanged();
            partial void OnLoggedByChanging(System.Nullable<int> value);
            partial void OnLoggedByChanged();
            partial void OnSourceofLeadChanging(string value);
            partial void OnSourceofLeadChanged();
            partial void OnTagsChanging(string value);
            partial void OnTagsChanged();
            #endregion

            public PortfolioContact()
            {
                //this._PortfolioContactAddresses = new EntitySet<PortfolioContactAddress>(new Action<PortfolioContactAddress>(this.attach_PortfolioContactAddresses), new Action<PortfolioContactAddress>(this.detach_PortfolioContactAddresses));
                OnCreated();
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
            public int ID
            {
                get
                {
                    return this._ID;
                }
                set
                {
                    if ((this._ID != value))
                    {
                        this.OnIDChanging(value);
                        
                        this._ID = value;
                       
                        this.OnIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PortfolioID", DbType = "Int")]
            public System.Nullable<int> PortfolioID
            {
                get
                {
                    return this._PortfolioID;
                }
                set
                {
                    if ((this._PortfolioID != value))
                    {
                        this.OnPortfolioIDChanging(value);
                        
                        this._PortfolioID = value;
                        
                        this.OnPortfolioIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(100)")]
            public string Name
            {
                get
                {
                    return this._Name;
                }
                set
                {
                    if ((this._Name != value))
                    {
                        this.OnNameChanging(value);
                        
                        this._Name = value;
                        
                        this.OnNameChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Title", DbType = "NVarChar(100)")]
            public string Title
            {
                get
                {
                    return this._Title;
                }
                set
                {
                    if ((this._Title != value))
                    {
                        this.OnTitleChanging(value);
                        
                        this._Title = value;
                       
                        this.OnTitleChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Email", DbType = "NVarChar(100)")]
            public string Email
            {
                get
                {
                    return this._Email;
                }
                set
                {
                    if ((this._Email != value))
                    {
                        this.OnEmailChanging(value);
                        
                        this._Email = value;
                        
                        this.OnEmailChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Telephone", DbType = "NVarChar(50)")]
            public string Telephone
            {
                get
                {
                    return this._Telephone;
                }
                set
                {
                    if ((this._Telephone != value))
                    {
                        this.OnTelephoneChanging(value);
                        
                        this._Telephone = value;
                        
                        this.OnTelephoneChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Location", DbType = "NVarChar(100)")]
            public string Location
            {
                get
                {
                    return this._Location;
                }
                set
                {
                    if ((this._Location != value))
                    {
                        this.OnLocationChanging(value);
                        
                        this._Location = value;
                        
                        this.OnLocationChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Key_Contact", DbType = "Bit")]
            public System.Nullable<bool> Key_Contact
            {
                get
                {
                    return this._Key_Contact;
                }
                set
                {
                    if ((this._Key_Contact != value))
                    {
                        this.OnKey_ContactChanging(value);
                        
                        this._Key_Contact = value;
                        
                        this.OnKey_ContactChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Notes", DbType = "NVarChar(MAX)")]
            public string Notes
            {
                get
                {
                    return this._Notes;
                }
                set
                {
                    if ((this._Notes != value))
                    {
                        this.OnNotesChanging(value);
                        
                        this._Notes = value;
                        
                        this.OnNotesChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Likes_Dislikes", DbType = "NVarChar(MAX)")]
            public string Likes_Dislikes
            {
                get
                {
                    return this._Likes_Dislikes;
                }
                set
                {
                    if ((this._Likes_Dislikes != value))
                    {
                        this.OnLikes_DislikesChanging(value);
                        
                        this._Likes_Dislikes = value;
                        
                        this.OnLikes_DislikesChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DateOfBirth", DbType = "Date")]
            public System.Nullable<System.DateTime> DateOfBirth
            {
                get
                {
                    return this._DateOfBirth;
                }
                set
                {
                    if ((this._DateOfBirth != value))
                    {
                        this.OnDateOfBirthChanging(value);
                        
                        this._DateOfBirth = value;
                        
                        this.OnDateOfBirthChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Address1", DbType = "NVarChar(MAX)")]
            public string Address1
            {
                get
                {
                    return this._Address1;
                }
                set
                {
                    if ((this._Address1 != value))
                    {
                        this.OnAddress1Changing(value);
                        
                        this._Address1 = value;
                        
                        this.OnAddress1Changed();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Address2", DbType = "NVarChar(MAX)")]
            public string Address2
            {
                get
                {
                    return this._Address2;
                }
                set
                {
                    if ((this._Address2 != value))
                    {
                        this.OnAddress2Changing(value);
                        
                        this._Address2 = value;
                        
                        this.OnAddress2Changed();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Country", DbType = "NVarChar(50)")]
            public string Country
            {
                get
                {
                    return this._Country;
                }
                set
                {
                    if ((this._Country != value))
                    {
                        this.OnCountryChanging(value);
                        
                        this._Country = value;
                        
                        this.OnCountryChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Postcode", DbType = "NVarChar(50)")]
            public string Postcode
            {
                get
                {
                    return this._Postcode;
                }
                set
                {
                    if ((this._Postcode != value))
                    {
                        this.OnPostcodeChanging(value);
                        
                        this._Postcode = value;
                        
                        this.OnPostcodeChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Fax", DbType = "NVarChar(50)")]
            public string Fax
            {
                get
                {
                    return this._Fax;
                }
                set
                {
                    if ((this._Fax != value))
                    {
                        this.OnFaxChanging(value);
                        
                        this._Fax = value;
                        
                        this.OnFaxChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Mobile", DbType = "NVarChar(50)")]
            public string Mobile
            {
                get
                {
                    return this._Mobile;
                }
                set
                {
                    if ((this._Mobile != value))
                    {
                        this.OnMobileChanging(value);
                        
                        this._Mobile = value;
                        
                        this.OnMobileChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_County", DbType = "NVarChar(50)")]
            public string County
            {
                get
                {
                    return this._County;
                }
                set
                {
                    if ((this._County != value))
                    {
                        this.OnCountyChanging(value);
                        
                        this._County = value;
                       
                        this.OnCountyChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LogintoPortal", DbType = "Bit")]
            public System.Nullable<bool> LogintoPortal
            {
                get
                {
                    return this._LogintoPortal;
                }
                set
                {
                    if ((this._LogintoPortal != value))
                    {
                        this.OnLogintoPortalChanging(value);
                        
                        this._LogintoPortal = value;
                       
                        this.OnLogintoPortalChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DepartmentID", DbType = "Int")]
            public System.Nullable<int> DepartmentID
            {
                get
                {
                    return this._DepartmentID;
                }
                set
                {
                    if ((this._DepartmentID != value))
                    {
                        this.OnDepartmentIDChanging(value);
                        
                        this._DepartmentID = value;
                       
                        this.OnDepartmentIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_isDisabled", DbType = "Bit")]
            public System.Nullable<bool> isDisabled
            {
                get
                {
                    return this._isDisabled;
                }
                set
                {
                    if ((this._isDisabled != value))
                    {
                        this.OnisDisabledChanging(value);
                        
                        this._isDisabled = value;
                        
                        this.OnisDisabledChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_City", DbType = "VarChar(150)")]
            public string City
            {
                get
                {
                    return this._City;
                }
                set
                {
                    if ((this._City != value))
                    {
                        this.OnCityChanging(value);
                        
                        this._City = value;
                       
                        this.OnCityChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Town", DbType = "VarChar(150)")]
            public string Town
            {
                get
                {
                    return this._Town;
                }
                set
                {
                    if ((this._Town != value))
                    {
                        this.OnTownChanging(value);
                        
                        this._Town = value;
                        
                        this.OnTownChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BuildingName", DbType = "VarChar(250)")]
            public string BuildingName
            {
                get
                {
                    return this._BuildingName;
                }
                set
                {
                    if ((this._BuildingName != value))
                    {
                        this.OnBuildingNameChanging(value);
                        
                        this._BuildingName = value;
                       
                        this.OnBuildingNameChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DateLogged", DbType = "DateTime")]
            public System.Nullable<System.DateTime> DateLogged
            {
                get
                {
                    return this._DateLogged;
                }
                set
                {
                    if ((this._DateLogged != value))
                    {
                        this.OnDateLoggedChanging(value);
                        
                        this._DateLogged = value;
                        
                        this.OnDateLoggedChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LoggedBy", DbType = "Int")]
            public System.Nullable<int> LoggedBy
            {
                get
                {
                    return this._LoggedBy;
                }
                set
                {
                    if ((this._LoggedBy != value))
                    {
                        this.OnLoggedByChanging(value);
                        
                        this._LoggedBy = value;
                       
                        this.OnLoggedByChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SourceofLead", DbType = "NVarChar(510)")]
            public string SourceofLead
            {
                get
                {
                    return this._SourceofLead;
                }
                set
                {
                    if ((this._SourceofLead != value))
                    {
                        this.OnSourceofLeadChanging(value);
                        
                        this._SourceofLead = value;
                        
                        this.OnSourceofLeadChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Tags", DbType = "NVarChar(MAX)")]
            public string Tags
            {
                get
                {
                    return this._Tags;
                }
                set
                {
                    if ((this._Tags != value))
                    {
                        this.OnTagsChanging(value);
                        
                        this._Tags = value;
                        
                        this.OnTagsChanged();
                    }
                }
            }

           

          
        }



        [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.files")]
        public partial class files : INotifyPropertyChanging, INotifyPropertyChanged
        {

            private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

            private int _id;

            private string _filename;

            private string _fileSize;

            private string _webPath;

            private string _systemPath;

            #region Extensibility Method Definitions
            partial void OnLoaded();
            partial void OnValidate(System.Data.Linq.ChangeAction action);
            partial void OnCreated();
            partial void OnidChanging(int value);
            partial void OnidChanged();
            partial void OnfilenameChanging(string value);
            partial void OnfilenameChanged();
            partial void OnfileSizeChanging(string value);
            partial void OnfileSizeChanged();
            partial void OnwebPathChanging(string value);
            partial void OnwebPathChanged();
            partial void OnsystemPathChanging(string value);
            partial void OnsystemPathChanged();
            #endregion

            public files()
            {
                OnCreated();
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
            public int id
            {
                get
                {
                    return this._id;
                }
                set
                {
                    if ((this._id != value))
                    {
                        this.OnidChanging(value);
                        
                        this._id = value;
                        this.SendPropertyChanged("id");
                        this.OnidChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_filename", DbType = "NVarChar(255)")]
            public string filename
            {
                get
                {
                    return this._filename;
                }
                set
                {
                    if ((this._filename != value))
                    {
                        this.OnfilenameChanging(value);
                        
                        this._filename = value;
                        this.SendPropertyChanged("filename");
                        this.OnfilenameChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_fileSize", DbType = "NVarChar(255)")]
            public string fileSize
            {
                get
                {
                    return this._fileSize;
                }
                set
                {
                    if ((this._fileSize != value))
                    {
                        this.OnfileSizeChanging(value);
                        
                        this._fileSize = value;
                        this.SendPropertyChanged("fileSize");
                        this.OnfileSizeChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_webPath", DbType = "NVarChar(MAX)")]
            public string webPath
            {
                get
                {
                    return this._webPath;
                }
                set
                {
                    if ((this._webPath != value))
                    {
                        this.OnwebPathChanging(value);
                        
                        this._webPath = value;
                        this.SendPropertyChanged("webPath");
                        this.OnwebPathChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_systemPath", DbType = "NVarChar(MAX)")]
            public string systemPath
            {
                get
                {
                    return this._systemPath;
                }
                set
                {
                    if ((this._systemPath != value))
                    {
                        this.OnsystemPathChanging(value);
                        
                        this._systemPath = value;
                        this.SendPropertyChanged("systemPath");
                        this.OnsystemPathChanged();
                    }
                }
            }

            public event PropertyChangingEventHandler PropertyChanging;

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void SendPropertyChanging()
            {
                if ((this.PropertyChanging != null))
                {
                    this.PropertyChanging(this, emptyChangingEventArgs);
                }
            }

            protected virtual void SendPropertyChanged(String propertyName)
            {
                if ((this.PropertyChanged != null))
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}