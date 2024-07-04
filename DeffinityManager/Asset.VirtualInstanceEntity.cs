using System;
using System.Collections.Generic;
using System.Web;

namespace AssetConfigurator.Entity
{

    public class VirtualInstanceEntity
    {
        int id = 0, assetId = 0;
        string instanceName = string.Empty, operatingSystem = string.Empty, diskSpace = string.Empty, memory = string.Empty;
        string department = string.Empty, keyContact = string.Empty, applications = string.Empty, ipAddresses = string.Empty;

        public int Id
        { get { return id; } set { id = value; } }

        public int AssetId
        { get { return assetId; } set { assetId = value; } }

        public string InstanceName
        { get { return instanceName; } set { instanceName = value; } }

        public string OperatingSystem
        { get { return operatingSystem; } set { operatingSystem = value; } }

        public string DiskSpace
        { get { return diskSpace; } set { diskSpace = value; } }

        public string Memory
        { get { return memory; } set { memory = value; } }

        public string Department
        { get { return department; } set { department = value; } }

        public string KeyContacts
        { get { return keyContact; } set { keyContact = value; } }

        public string Applications
        { get { return applications; } set { applications = value; } }

        public string IPAddresses
        {
            get { return ipAddresses; }
            set { ipAddresses = value; }
        }

        public VirtualInstanceEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class VirtualInstanceCollection : List<VirtualInstanceEntity>
    { 
        
    }
}