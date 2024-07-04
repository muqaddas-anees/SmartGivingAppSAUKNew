using System;
using System.Collections.Generic;

namespace Incidents.Entity
{
    /// <summary>
    /// Entity class for the Resource.  This is the resource for the the Incidents.
    /// </summary>
    public class Resource
    {

        #region Fields

        int incidentID = 0, memberID = 0, id = 0;

        
        string duration = string.Empty, activity = string.Empty;
        string memberName = string.Empty;

        #endregion

        #region Properties

        public string MemberName
        {
            get { return memberName; }
            set {memberName=value;}
        }

        public int MemberID
        {
            get { return memberID; }
            set { memberID = value; }
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
        

        public string Activity
        {
            get { return activity; }
            set { activity = value; }
        }

        public string Duration
        {
            get { return duration; }
            set { duration = value; }
        }


        /// <summary>
        /// Gets the duration number
        /// </summary>
        public int DurationNumber
        {
            get
            {
                string duration = Duration;
                return Convert.ToInt16(duration.Substring(0,duration.Length-1));
            }
            set
            {
                int dummy = value;
            }
        }

        /// <summary>
        /// Gets the duration string. Eg. Days, Months, Hours, Minutes etc.
        /// </summary>
        public string DurationPeriod
        {
            get
            {
                string duration = Duration;
                char period = Convert.ToChar(duration.Substring(duration.Length - 1));
                switch (period)
                { 
                    case 'd':
                    case 'D':
                        return "Days";
                        break;
                    case 'h':
                    case 'H':
                        return "Hours";
                        break;
                    case 'm':
                    case 'M':
                        return "Minutes";
                        break;
                    default:
                        return "Un specified";
                        break;
                }
            }
            set
            {
                string dummy = value;
            }
        }

        #endregion

        public Resource()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class ResourceCollection : List<Resource>
    { 
        
    }
}
