using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Deffinity.HelpCenterManagers;

/// <summary>
/// Summary description for HelpCenterEntitys
/// </summary>
namespace Deffinity.HelpCenterEntitys
{
    public class HelpCenterEntity
    {
        int _ID=0, _PortfolioID=0; DateTime _DateRaised;
        string _Name = string.Empty, _ContactNumber = string.Empty, _SenderEmail = string.Empty, _OwnerEmail = string.Empty, _Details = string.Empty;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int PortfolioID
        {
            get { return _PortfolioID; }
            set { _PortfolioID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        public string SenderEmail
        {
            get { return _SenderEmail; }
            set { _SenderEmail = value; }
        }
        public string OwnerEmail
        {
            get { return _OwnerEmail; }
            set { _OwnerEmail = value; }
        }
        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }
        public DateTime DateRaised
        {
            get { return _DateRaised; }
            set { _DateRaised = value; }
        }
    
    }
}