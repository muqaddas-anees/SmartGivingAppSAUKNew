using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deffinity.LabourEntity1
{
    public class LabourEntity
    {
        public LabourEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int LabourIDApproverID;
        public int labouridapproverid
        {
            get { return LabourIDApproverID; }
            set { LabourIDApproverID = value; }
        }
        private int LabourID;
        public int labourid
        {
            get { return LabourID; }
            set { LabourID = value; }
        }
        private string LabourName;
        public string _LabourName
        {
            get { return LabourName; }
            set { LabourName = value; }
        }
        private int LabourContarctorID;
        public int _LabourContractorID
        {
            get { return LabourContarctorID; }
            set { LabourContarctorID = value; }

        }
        private int WCDateID;
        public int _WCdateID
        {
            get { return WCDateID; }
            set { WCDateID = value; }
        }
        private string Labour_DateEnter;
        public string _Labour_DateEnter
        {
            get { return Labour_DateEnter; }
            set { Labour_DateEnter = value; }
        }

        private Double Labour_Hours;
        public Double _Labour_Hours
        {
            get { return Labour_Hours; }
            set { Labour_Hours = value; }
        }
        private int Labour_Project;
        public int _Labour_Project
        {
            get { return Labour_Project; }
            set { Labour_Project = value; }
        }
        private int Labour_Task;
        public int _Labour_Task
        {
            get { return Labour_Task; }
            set { Labour_Task = value; }
        }
        private string Labour_SRNumber;
        public string _Labour_SRNumber
        {
            get { return Labour_SRNumber; }
            set { Labour_SRNumber = value; }
        }
        private int Labour_TypeofHours;
        public int _Labour_TypeofHours
        {
            get { return Labour_TypeofHours; }
            set { Labour_TypeofHours = value; }
        }
        private string Labour_Notes;
        public string _Labour_Notes
        {
            get { return Labour_Notes; }
            set { Labour_Notes = value; }
        }
    }
}
