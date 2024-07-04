using System;
using System.Collections.Generic;
using System.Web;

namespace AssetConfigurator.Entity
{
    public class AssetMasterEntity
    {

        #region Fields

        int id = 0;
        string assetType = string.Empty;
        string icon = string.Empty;
        bool ports = false;
        bool ipAddress = false;
        bool applications = false;
        bool virtualisation = false;

        #endregion

        #region Properties

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string AssetType
        {
            get { return assetType; }
            set { assetType = value; }
        }

        public string Icon
        {
            get { return icon; }
            set { icon= value; }
        }
        public bool Ports
        {
            get { return ports; }
            set { ports = value; }
        }
        
        public bool IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        public bool Virtualisation
        {
            get { return virtualisation; }
            set { virtualisation = value; }
        }
        
        public bool Applications
        {
            get { return applications; }
            set { applications = value; }
        }

        #endregion

        public AssetMasterEntity()
        {
           
        }
    }
}
