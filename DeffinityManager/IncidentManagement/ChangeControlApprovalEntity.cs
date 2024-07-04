using System;
using System.Collections.Generic;

namespace Incidents.Entity
{
    /// <summary>
    /// Summary description for ChangeControlEntity
    /// </summary>
    public class Approval
    {

        #region Fields

        int id=0;
        int changeControlID=0;
        string title=string.Empty;
        int approvalID=0;
        string comments=string.Empty;
        bool? approved;
        DateTime dateApproved;
        string approvalName = string.Empty;
        string emailAddress = string.Empty;

        #endregion

        #region Public Properties

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public string ApprovalName
        {
            get { return approvalName; }
            set { approvalName = value; }
        }

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

        public int ApprovalID
        {
            get { return approvalID; }
            set { approvalID = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public bool? Approved
        {
            get { return approved; }
            set { approved = value; }
        }

        public DateTime DateApproved
        {
            get { return dateApproved; }
            set { dateApproved = value; }
        }

        #endregion

        public Approval()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class ApprovalCollection : List<Approval>
    { 
        
    }
}