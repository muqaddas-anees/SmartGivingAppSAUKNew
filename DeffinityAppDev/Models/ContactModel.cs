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
    public class ContactModel
    {
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


        [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.ProductPolicyType")]
        public partial class ProductPolicyType : INotifyPropertyChanging, INotifyPropertyChanged
        {

            private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

            private int _ID;

            private string _Title;

            private string _Description;

            #region Extensibility Method Definitions
            partial void OnLoaded();
            partial void OnValidate(System.Data.Linq.ChangeAction action);
            partial void OnCreated();
            partial void OnIDChanging(int value);
            partial void OnIDChanged();
            partial void OnTitleChanging(string value);
            partial void OnTitleChanged();
            partial void OnDescriptionChanging(string value);
            partial void OnDescriptionChanged();
            #endregion

            public ProductPolicyType()
            {
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
                        this.SendPropertyChanging();
                        this._ID = value;
                        this.SendPropertyChanged("ID");
                        this.OnIDChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Title", DbType = "NVarChar(250)")]
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
                        this.SendPropertyChanging();
                        this._Title = value;
                        this.SendPropertyChanged("Title");
                        this.OnTitleChanged();
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "NVarChar(MAX)")]
            public string Description
            {
                get
                {
                    return this._Description;
                }
                set
                {
                    if ((this._Description != value))
                    {
                        this.OnDescriptionChanging(value);
                        this.SendPropertyChanging();
                        this._Description = value;
                        this.SendPropertyChanged("Description");
                        this.OnDescriptionChanged();
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