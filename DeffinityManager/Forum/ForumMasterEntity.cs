using System;
using System.Data;


/// <summary>
/// Summary description for ForumMasterEntity
/// </summary>
namespace Deffinity.ForumEntitys
{

    
    public class ForumMasterEntity
    {
        int _ID = 0, _ProjectReference = 0, _AuthorID = 0, _Visited = 0,_Ftype=0;
        string _Title, _Message;
        DateTime _PostedDate;
        bool _MsgType;

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
        public int AuthorID
        {
            get { return _AuthorID; }
            set { _AuthorID = value; }
        }
        public int Visited
        {
            get { return _Visited; }
            set { _Visited = value; }
        }
        public bool MsgType
        {
            get { return _MsgType; }
            set { _MsgType = value; }
        }
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public DateTime PostedDate
        {
            get { return _PostedDate; }
            set { _PostedDate = value; }
        }
        public int Ftype
        {
            get { return _Ftype; }
            set { _Ftype = value; }
        }
        //Forum type
        public enum Fourmtype { Project = 1, Portfolio = 2 }
    }

}