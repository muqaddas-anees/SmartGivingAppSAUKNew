using System;
using System.Collections.Generic;
using System.Web;


/// <summary>
/// Summary description for Asset
/// </summary>
/// 
namespace AssetConfigurator.Entity
{
    public class ValuesEntity
    {
        private int id=0;
        private int attributeId=0;
        private string attributeName=string.Empty;
        private string attributeValue=string.Empty;
        private Guid uniqueIdentifier;
        private int assetID = 0;

        public int AssetID
        {
            get { return assetID; }
            set { assetID = value; }
        }

        public Guid UniqueIdentifier
        {
            get { return uniqueIdentifier; }
            set { uniqueIdentifier = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int AttributeId
        {
            get { return attributeId; }
            set { attributeId = value; }
        }

        public string AttributeName
        {
            get { return attributeName; }
            set { attributeName = value; }
        }

        public string AttributeValue
        {
            get { return attributeValue; }
            set { attributeValue = value; }
        }

        public ValuesEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
    public class ValuesEntityCollection : List<ValuesEntity>
    { 
        
    }
}