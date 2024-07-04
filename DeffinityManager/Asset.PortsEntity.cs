using System;
using System.Collections.Generic;
using System.Web;


namespace AssetConfigurator.Entity
{
    public class PortsEntity
    {

        int assetID = 0;
        int id = 0;
        string connectedEquipment = string.Empty;
        string portNumber = string.Empty;
        char side=' ';

        public int AssetID
        {
            get { return assetID; }
            set { assetID = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string ConnectedEquipment
        {
            get { return connectedEquipment; }
            set { connectedEquipment = value; }
        }

        public string PortNumber
        {
            get { return portNumber; }
            set { portNumber = value; }
        }

        public char Side
        {
            get { return side; }
            set { side = value; }
        }

        public PortsEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class PortsCollection : List<PortsEntity>
    { 
    
    }
}