using System;
using System.Collections.Generic;
using System.Web;
using Health.Entity;
using System.Collections;
using System.Reflection;

/// <summary>
/// Summary description for HealthCheckListItemsSortHelper
/// </summary>
namespace Health
{
    public class HealthCheckListItemsSortHelper:IComparer<HealthCheckListItems>
    {
        public HealthCheckListItemsSortHelper()
        {

        }

        public HealthCheckListItemsSortHelper(string p_PropertyName)
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

        public int Compare(HealthCheckListItems x, HealthCheckListItems y)
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