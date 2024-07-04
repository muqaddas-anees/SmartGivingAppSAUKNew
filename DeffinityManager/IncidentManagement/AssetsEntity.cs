using System;
using System.Collections.Generic;

namespace Incidents.Entity
{

    /// <summary>
    /// Summary description for AssetsEntity
    /// </summary>
    public class Asset
    {
        private int id = 0;
        private int incidentID = 0;
        private string assetID = string.Empty;
        private string make = string.Empty;
        private DateTime dateOfChange = DateTime.Now;
        private string model = string.Empty;
        private string detailsOfChange = string.Empty;


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IncidentID
        {
            get { return incidentID; }
            set { incidentID = value; }
        }
        
        public string AssetID
        {
            get { return assetID; }
            set { assetID = value; }
        }
       

        public string Make
        {
            get { return make; }
            set { make = value; }
        }
       

        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        

        public string DetailsOfChange
        {
            get { return detailsOfChange; }
            set { detailsOfChange = value; }
        }
     

        public DateTime DateOfChange
        {
            get { return dateOfChange; }
            set { dateOfChange = value; }
        }

        public Asset()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class AssetCollection : List<Asset>
    { 
        
    }
}