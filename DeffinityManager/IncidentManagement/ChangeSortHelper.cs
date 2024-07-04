using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Incidents.Entity;

namespace Incidents
{
    /// <summary>
    /// Summary description for ChangeSortHelper
    /// </summary>
    public class ChangeComparer:IComparer<Change>
    {
        public ChangeComparer()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public ChangeComparer(string p_PropertyName)
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

        public int Compare(Change x,Change y)
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
