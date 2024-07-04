using System.Collections.Generic;

namespace Incidents.Entity
{
    /// <summary>
    /// Summary description for ChangeControlRisks
    /// </summary>
    public class Risk
    {
        #region Fields

        int id = 0;
        int changeControlID = 0;
        string riskDescription = string.Empty;
        bool rollBackPlan = false;
        bool testPlan = false;
        int assignedTo = 0;
        string resourceName = string.Empty;

        #endregion

        #region Public Properties

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int ChangeControlID
        {
            get { return changeControlID; }
            set { changeControlID = value; }
        }

        public string RiskDescription
        {
            get { return riskDescription; }
            set { riskDescription = value; }
        }

        public bool RollBackPlan
        {
            get { return rollBackPlan; }
            set { rollBackPlan = value; }
        }

        public bool TestPlan
        {
            get { return testPlan; }
            set { testPlan = value; }
        }


        public int AssignedTo
        {
            get { return assignedTo; }
            set { assignedTo = value; }
        }
        public string ResourceName
        {
            get { return resourceName; }
            set { resourceName = value; }
        }

        #endregion

        public Risk()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class RiskCollection : List<Risk>
    { 
        
    }
}