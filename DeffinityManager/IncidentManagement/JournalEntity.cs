using System;
using System.Collections.Generic;

namespace Incidents.Entity
{
    /// <summary>
    /// Entity class for the Journal.  This is the resource for the the Incidents.
    /// </summary>
    public class Journal
    {

        #region Fields

        int id = 0, assignedTo = 0, incidentID = 0;
        DateTime date;
        string notes = string.Empty, time = string.Empty,assignedName=string.Empty;

        #endregion

        #region Properties

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

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
        public int AssignedTo
        {
            get { return assignedTo; }
            set { assignedTo = value; }
        }
        public string AssignedName
        {
            get { return assignedName; }
            set { assignedName = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        #endregion

        public Journal()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class JournalCollection : List<Journal>
    {
    }
}