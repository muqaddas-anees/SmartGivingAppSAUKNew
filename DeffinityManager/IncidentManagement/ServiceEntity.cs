using System.Collections.Generic;

namespace Incidents.Entity
{
    /// <summary>
    /// Summary description for ServiceEntity
    /// </summary>
    public class Service
    {
        #region Fields

        private int serviceID=0;
        private int incidentID=0;
        private int qty=0;
        private double unitPrice=0f;
        private int id = 0;
        private string serviceDescription = string.Empty;
        private string incidentDetails = string.Empty;
        private string incidentResolution = string.Empty;
        private string incidentSubject = string.Empty;
        private string incidentRequesterName = string.Empty;
        private string incidentRequesterEmail = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// Foreign key value
        /// </summary>
        public string ServiceDescription
        {
            get { return serviceDescription; }
            set { serviceDescription = value; }
        }
       
        /// <summary>
        /// Foreign key value
        /// </summary>
        public string IncidentDetails
        {
            get { return incidentDetails; }
            set { incidentDetails = value; }
        }
       
        /// <summary>
        /// Foreign key value
        /// </summary>
        public string IncidentResolution
        {
            get { return incidentResolution; }
            set { incidentResolution = value; }
        }

        /// <summary>
        /// Foreign key value
        /// </summary>
        public string IncidentSubject
        {
            get { return incidentSubject; }
            set { incidentSubject = value; }
        }

        /// <summary>
        /// Foreign key value
        /// </summary>
        public string IncidentRequesterName
        {
            get { return incidentRequesterName; }
            set { incidentRequesterName = value; }
        }

        /// <summary>
        /// Foreign key value
        /// </summary>
        public string IncidentRequesterEmail
        {
            get { return incidentRequesterEmail; }
            set { incidentRequesterEmail = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int ServiceID
        {
            get { return serviceID; }
            set { serviceID = value; }
        }

        public int IncidentID
        {
            get { return incidentID; }
            set { incidentID = value; }
        }

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public double UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        #endregion

        public Service()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }


    public class ServiceCollection : List<Service>
    { 
        
    }
}