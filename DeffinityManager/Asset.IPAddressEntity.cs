using System;
using System.Collections.Generic;
using System.Web;

namespace AssetConfigurator.Entity
{
    public class IPAddressesEntity
    {
        int id = 0;
        int assetID = 0;
        string ipAddress = string.Empty;
        string port = string.Empty;
        string description = string.Empty;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int AssetID
        {
            get { return assetID; }
            set { assetID = value; }
        }

        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        public string Port
        {
            get { return port; }
            set { port = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public IPAddressesEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class IPAddressesCollection : List<IPAddressesEntity>
    { }
}