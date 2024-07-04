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
/// Summary description for RagSectiontoPortfolioEntity
/// </summary>
namespace Deffinity.RagSectionPortfolioEntity
{
    public class RagSectiontoPortfolioEntity
    {
        int _id, _portfolioid,_programmeid,_subprogrammeid,_taskid;
        string _ragdescription, _ragsectionname, _programmename, _subprogrammename;
        public int ID
        {
            get { return _id; }
            set { _id = value; }            
        }
        public int PortfolioID
        {
            get { return _portfolioid; }
            set { _portfolioid = value; }
        }
        public string RAGDescription
        {
            get { return _ragdescription; }
            set { _ragdescription = value; }
        }
        public string RAGSectionName
        {
            get { return _ragsectionname; }
            set { _ragsectionname = value; }
        }
        public int Programmeid
        {
            get { return _programmeid; }
            set { _programmeid = value; }
        }
        public int Subprogrammeid
        {
            get { return _subprogrammeid; }
            set { _subprogrammeid = value; }
        }
        public int Taskid
        {
            get { return _taskid; }
            set { _taskid = value; }
        }
        public string Programmename
        {
            get { return _programmename; }
            set { _programmename = value; }
        }
        public string Subprogrammename
        {
            get { return _subprogrammename; }
            set { _subprogrammename = value; }
        }

    }
}
