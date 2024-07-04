using System;
using System.Reflection;
using Incidents.Entity;
using System.Collections.Generic;
using System.Collections;

namespace Incidents
{

    /// <summary>
    /// Summary description for IncidentSortHelper
    /// </summary>
    public class IncidentComparer:IComparer<Incident>
    {
        public IncidentComparer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public IncidentComparer(string p_PropertyName)
        {
            this.PropertyName = p_PropertyName;
        }

        #region Property

        private string _propertyName;

        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        #endregion

        #region IComparable Members

        public int Compare(Incident x,Incident y)
        {
            Type t = x.GetType();
            PropertyInfo val = t.GetProperty(this.PropertyName);

            if (val != null)
            {
                return Comparer.DefaultInvariant.Compare(val.GetValue(x, null),
                val.GetValue(y, null));
            }
            else
            {
                throw new Exception(this.PropertyName + " is not a valid property to sort on.  It doesn't exist in the Class.");
            }
        }

        #endregion

    }
}
