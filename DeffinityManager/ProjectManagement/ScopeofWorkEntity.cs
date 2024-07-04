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
/// Summary description for ScopeofWorkEntity
/// </summary>
namespace Deffinity.ScopeofWorkEntitys
{
    public class ScopeofWorkEntity
    {
       
        int _ID, _ProjectReference, _PUID, _RaisedBy;
        string _DetailsofWork,_DetailsofServices,_DetailsofSecurity,_Skills;
        string _StartTime,_EndTime;
        string _Site,_Address1,_Address2,_Address3,_PostCode,_Name,_Number,_Email;
        string _StartDate,_EndDate;

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
        public int PUID
        {
            get { return _PUID; }
            set { _PUID = value; }
        }
        public int RaisedBy
        {
            get { return _RaisedBy; }
            set { _RaisedBy = value; }
        }
        public string DetailsofWork
        {
            get { return _DetailsofWork; }
            set { _DetailsofWork = value; }
        }
        public string DetailsofServices
        {
            get { return _DetailsofServices; }
            set { _DetailsofServices = value; }
        }
        public string DetailsofSecurity
        {
            get { return _DetailsofSecurity; }
            set { _DetailsofSecurity = value; }
        }
        public string Skills
        {
            get { return _Skills; }
            set { _Skills = value; }
        }
        public string StartTime
        {
            get { return _StartTime; }
            set {_StartTime = value; }
        }
        public string EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        public string Site
        {
            get { return _Site; }
            set { _Site = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Address3
        {
            get { return _Address3; }
            set { _Address3 = value; }
        }
        public string PostCode
        {
            get { return _PostCode; }
            set { _PostCode = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        
 
    }
}
