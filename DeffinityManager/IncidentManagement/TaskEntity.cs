using System;
using System.Collections.Generic;

namespace Incidents.Entity
{
    /// <summary>
    /// Summary description for TaskEntity
    /// </summary>
    public class Task
    {
        #region Fields

        int id = 0;
        int changeControlID = 0;
        string taskDescription = string.Empty;
        DateTime originalDate;
        DateTime newDate;
        string change = string.Empty;

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
        
        public string TaskDescription
        {
            get { return taskDescription; }
            set { taskDescription = value; }
        }
        
        public DateTime OriginalDate
        {
            get { return originalDate; }
            set { originalDate = value; }
        }
        
        public DateTime NewDate
        {
            get { return newDate; }
            set { newDate = value; }
        }
        

        public string Change
        {
            get { return change; }
            set { change = value; }
        }

        #endregion

        public Task()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class TaskCollection : List<Task>
    {

    }
}