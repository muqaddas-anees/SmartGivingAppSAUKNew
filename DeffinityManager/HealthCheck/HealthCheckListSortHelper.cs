using System;
using System.Reflection;
using System.Collections.Generic;
using Health.Entity;
using System.Collections;

namespace Health
{
    /// <summary>
    /// Summary description for HealthSortHelper
    /// </summary>
    public class HealthComparer : IComparer<HealthCheckList>
    {
        public HealthComparer()
        {

        }

        public HealthComparer(string p_PropertyName)
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

        public int Compare(HealthCheckList x, HealthCheckList y)
        {
            Type t = x.GetType();
            PropertyInfo val = t.GetProperty(this.PropertyName);

            if (val != null)
            {
                return Comparer.DefaultInvariant.Compare(val.GetValue(x, null),val.GetValue(y, null));
            }
            else
            {
                throw new Exception(this.PropertyName + " is not a valid property to sort on.  It doesn't exist in the Class.");
            }
        }

        #endregion
    }
}