using System;
using System.Collections.Generic;
using System.Web;

namespace AssetConfigurator.Entity
{
    public class AttributesEntity
    {

        #region Fields

        int id = 0;
        int assetId = 0;
        string attributeName = string.Empty;
        string type = string.Empty;
        bool attachment = false;

        #endregion

        #region Properties

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int AssetId
        {
            get { return assetId; }
            set { assetId = value; }
        }

        public string AttributeName
        {
            get { return attributeName; }
            set { attributeName = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public bool Attachment
        {
            get { return attachment; }
            set { attachment = value; }
        }

        #endregion

        public AttributesEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}