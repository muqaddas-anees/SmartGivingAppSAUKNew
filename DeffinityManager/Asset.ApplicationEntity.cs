using System;
using System.Collections.Generic;
using System.Web;

namespace AssetConfigurator.Entity
{
    public class ApplicationEntity
    {
        int id = 0;
        int assetId = 0;
        string applicationName = string.Empty;
        string version = string.Empty;
        string department = string.Empty;
        string keyContact = string.Empty;
        string serverName = string.Empty;

        public int Id
        { get { return id; } set { id = value; } }

        public int AssetId
        { get { return assetId; } set { assetId = value; } }

        public string ApplicationName
        { get { return applicationName; } set { applicationName = value; } }

        public string Version
        { get { return version; } set { version = value; } }

        public string Department
        { get { return department; } set { department = value; } }

        public string KeyContact
        { get { return keyContact; } set { keyContact = value; } }

        public string ServerName
        { get { return serverName; } set { serverName = value; } }

    }

    public class ApplicationCollection : List<ApplicationEntity>
    {

    }
}