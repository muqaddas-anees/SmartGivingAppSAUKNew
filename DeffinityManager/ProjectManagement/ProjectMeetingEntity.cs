using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for ProjectMeeting
/// </summary>
namespace Deffinity.ProjectMeetingEntitys
{
    public class ProjectMeetingEntity
    {
        int _ID, _RAGStatus;
        int _ProjectReference;
        string _MeetingDate;
        string _MeetingTime, _Location, _Attendees, _GeneralNotes, _LessonsLearnt,_KeyAchievements,_KeyTasks;
        bool _VisibletoCustomer = false;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }        
        }
        public int ProjectReference
        {
            get { return _ProjectReference; }
            set { _ProjectReference = value; }
        }
        public string MeetingTime
        {
            get { return _MeetingTime; }
            set { _MeetingTime = value; }
        }
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        public string Attendees
        {
            get { return _Attendees; }
            set { _Attendees = value; }
        }
        public string GeneralNotes
        {
            get { return _GeneralNotes; }
            set { _GeneralNotes = value; }
        }
        public string LessonsLearnt
        {
            get { return _LessonsLearnt; }
            set { _LessonsLearnt = value; }
        }
        public string MeetingDate
        {
            get { return _MeetingDate; }
            set { _MeetingDate = value; }
        }
        public string KeyAchievements
        {
            get { return _KeyAchievements; }
            set { _KeyAchievements = value; }
        }
        public string KeyTasks
        {
            get { return _KeyTasks; }
            set { _KeyTasks = value; }
        }
        public bool VisibletoCustomer
        {
            get { return _VisibletoCustomer; }
            set { _VisibletoCustomer = value; }
        }
         public int RAGStatus
        {
            get { return _RAGStatus; }
            set { _RAGStatus= value; }
        }
       // RAGStatus

    }
}



