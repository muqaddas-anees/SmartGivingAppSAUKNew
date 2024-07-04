using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for RagSectiontoProject
/// </summary>
namespace Deffinity.RagSectionProjectEntity
{
    public class RagSectiontoProjectEntity
    {
        int _id, _portfolioid, _ragsectionid, _projectreference, _programmeID,_taskid;
        string _keyissue, _ragsectionname, _ragstatus;
        DateTime _PlannedDate, _LatestDate, _ActualDate;
        public DateTime PlannedDate
        {
            get { return _PlannedDate; }
            set {_PlannedDate=value; }
        }
        public DateTime LatestDate
        {
            get { return _LatestDate; }
            set { _LatestDate = value; }
        }
        public DateTime ActualDate
        {
            get { return _ActualDate; }
            set { _ActualDate = value; }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public int ProjectReference
        {
            get { return _projectreference; }
            set { _projectreference = value; }
        }
        public int PortfolioID
        {
            get { return _portfolioid; }
            set { _portfolioid = value; }
        }
        public int RagSectionid
        {
            get { return _ragsectionid; }
            set { _ragsectionid = value; }
        }
        public int ProgrammeID
        {
            get { return _programmeID; }
            set { _programmeID = value; }
        }
        public string KeyIssue
        {
            get { return _keyissue; }
            set { _keyissue = value; }
        }
        public string RAGStatus
        {
            get { return _ragstatus; }
            set { _ragstatus = value; }
        }
        public string RAGSectionName
        {
            get { return _ragsectionname; }
            set { _ragsectionname = value; }
        }
        public int Taskid
        {
            get { return _taskid; }
            set { _taskid = value; }
        }

    }
}
