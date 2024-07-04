using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Deffinity.ForumEntitys;

/// <summary>
/// Summary description for ForumItemEntity
/// </summary>
namespace Deffinity.ForumEntitys
{
    public class ForumItemEntity
    {
        int _ID = 0, _ForumMasterID = 0, _Rating = 0,_MsgLevel=0;                
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int ForumMasterID
        {
            get { return _ForumMasterID; }
            set { _ForumMasterID = value; }
        }
        public int Rating
        {
            get { return _Rating; }
            set { _Rating = value; }
        }
        public int MsgLevel
        {
            get { return _MsgLevel; }
            set { _MsgLevel = value; }
        }

        public ForumItemEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
