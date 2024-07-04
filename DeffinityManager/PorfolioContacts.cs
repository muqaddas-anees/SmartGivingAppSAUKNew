using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DeffinityManager.DAL;
using DeffinityManager.DAL.DBForPortfolioContactsTableAdapters;
/// <summary>
/// Summary description for PorfolioContacts
/// </summary>
namespace Deffinity.PortfolioContact
{
    public class PorfolioContactsCS
    {
        
        
        private static Deffinity_PortfolioContactTableAdapter _deffinityPortfolioContactTableAdapter;
        private static PortfolioContacts_ActivityTableAdapter _portfoliocontacts_ActivityTableAdapter;
        private static Contractors_SelectActiveTableAdapter _contractors_SelectActiveTeableAdapter;
        private static PortfolioContacts_NotesTableAdapter _PortfolioContacts_NotesTableAdapter;
        private static PortfolioContacts_DocsTableAdapter _PortfolioContacts_DocsTableAdapter;
        public static PortfolioContacts_DocsTableAdapter DocsAdapter
        {
            get
            {
                if (_PortfolioContacts_DocsTableAdapter == null)
                    _PortfolioContacts_DocsTableAdapter = new PortfolioContacts_DocsTableAdapter();
                return _PortfolioContacts_DocsTableAdapter;
            }
        }
        public static PortfolioContacts_NotesTableAdapter NotesAdapter
        {
            get
            {
                if (_PortfolioContacts_NotesTableAdapter == null)
                    _PortfolioContacts_NotesTableAdapter = new PortfolioContacts_NotesTableAdapter();
                return _PortfolioContacts_NotesTableAdapter;
            }
        }
        public static Contractors_SelectActiveTableAdapter ContractorsAdapter
        {
            get
            {
                if (_contractors_SelectActiveTeableAdapter == null)
                    _contractors_SelectActiveTeableAdapter = new Contractors_SelectActiveTableAdapter();
                return _contractors_SelectActiveTeableAdapter;
            }
        }
        public static PortfolioContacts_ActivityTableAdapter ActivityAdapter
        {
            get
            {
                if (_portfoliocontacts_ActivityTableAdapter == null)
                    _portfoliocontacts_ActivityTableAdapter = new PortfolioContacts_ActivityTableAdapter();
                return _portfoliocontacts_ActivityTableAdapter;
            }
        }

            
        public static Deffinity_PortfolioContactTableAdapter PortfolioContactAdapter
        {
            get
            {
                if (_deffinityPortfolioContactTableAdapter == null)
                
                    _deffinityPortfolioContactTableAdapter = new Deffinity_PortfolioContactTableAdapter();
                    return _deffinityPortfolioContactTableAdapter;
                
            }
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForPortfolioContacts.PortfolioContacts_DocsDataTable SelectAllDocsByContactID(int ContactID)
        {
            return DocsAdapter.GetDocsDataByContacID(ContactID);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForPortfolioContacts.PortfolioContacts_DocsDataTable SelectFileNameById(int ID)
        {
            return DocsAdapter.GetDocData(ID);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]

        public object InsertDocs(int ContactID, string Filename)
        {
            object retval = DocsAdapter.PortfolioContacts_Docs_Insert(ContactID, Filename);
            
            return retval;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int UpdateDocs(int ID, int ContactID, string Filename)
        {
            int retval = -1;
            DBForPortfolioContacts.PortfolioContacts_DocsDataTable DocsTable = DocsAdapter.GetDocData(ID);
            if (DocsTable.Rows.Count > 0)
            {
                DBForPortfolioContacts.PortfolioContacts_DocsRow DocsRow = DocsTable[0];
                DocsRow.ContactID = ContactID;
                DocsRow.FileName = Filename;
                retval = DocsAdapter.Update(DocsRow);
            }
            return retval;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public int DeleteDocs(int ID)
        {
            int retval = -1;
            DBForPortfolioContacts.PortfolioContacts_DocsDataTable DocsTable = DocsAdapter.GetDocData(ID);
            if (DocsTable.Rows.Count > 0)
            {
                retval = DocsAdapter.Delete(ID);
            }
            return retval;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForPortfolioContacts.PortfolioContacts_NotesDataTable SelectAllNotes()
        {
            return NotesAdapter.GetDataPFNotes();
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForPortfolioContacts.PortfolioContacts_NotesDataTable SelectAllNotesByContactID(int ContactID )
        {
            return NotesAdapter.GetNotesByCotactID(ContactID);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int InsertNotes(int ContactID, string Notes)
        {
            int retval = -1;
            DBForPortfolioContacts.PortfolioContacts_NotesDataTable NotesTable = new DBForPortfolioContacts.PortfolioContacts_NotesDataTable();
            DBForPortfolioContacts.PortfolioContacts_NotesRow NotesRow = NotesTable.NewPortfolioContacts_NotesRow();
            NotesRow.ContactID = ContactID;
            NotesRow.Notes = Notes;
            NotesTable.AddPortfolioContacts_NotesRow(NotesRow);
            retval = NotesAdapter.Update(NotesTable);

            return retval;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int UpdateNotes(int ID, int ContactID, string Notes)
        {
            int retval = -1;
            DBForPortfolioContacts.PortfolioContacts_NotesDataTable NotesTable = new DBForPortfolioContacts.PortfolioContacts_NotesDataTable();
            NotesTable = NotesAdapter.GetNotesDataByID(ID);
            if (NotesTable.Rows.Count > 0)
            {
                DBForPortfolioContacts.PortfolioContacts_NotesRow NotesRow = NotesTable[0];
                NotesRow.ContactID = ContactID;
                NotesRow.Notes = Notes;
                retval = NotesAdapter.Update(NotesRow);
            }
            return retval;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public int DeleteNotes(int ID)
        {
            int retval = -1;
            DBForPortfolioContacts.PortfolioContacts_NotesDataTable NotesTable = new DBForPortfolioContacts.PortfolioContacts_NotesDataTable();
            NotesTable = NotesAdapter.GetNotesDataByID(ID);
            if (NotesTable.Rows.Count > 0)
            {
                NotesAdapter.Delete(ID);
            }
            return retval;
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForPortfolioContacts.Deffinity_PortfolioContactDataTable SelectAllPortfolioContacts(int PortfolioID)
        {
            return PortfolioContactAdapter.GetDataAllContactsByPortfolioID(PortfolioID);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForPortfolioContacts.Deffinity_PortfolioContactDataTable SelectPortfolioContacts(int ID)
        {
            return PortfolioContactAdapter.GetDataPortFolio(ID);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForPortfolioContacts.PortfolioContacts_ActivityDataTable SelectAllActivitesByContact(int ContactID)
        {
            return ActivityAdapter.GetDataByContactID(ContactID);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public object InsertPortfolioContacts(int PortfolioID, string Name, string Title, string Email, 
            string Telephone, string Location, Boolean Key_Contact, string Notes, string Likes_Dislikes, DateTime DateOfBirth,
            string Address1, string Address2, string Country, string Postcode, string Fax, string Mobile, string County, int departmentId, bool isDisabled)
        {
            object obj = PortfolioContactAdapter.Deffinity_PortfolioContact_Insert(PortfolioID, Name, Title, Email,
               Telephone, Location, Key_Contact, Notes, Likes_Dislikes, DateOfBirth,Address1,Address2,Country,Postcode,Fax,Mobile,County,departmentId,isDisabled);
            return obj;
            
        }
        //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        //public object InsertPortfolioContacts1(int PortfolioID, string Name, string Title, string Email, string Telephone, string Location, Boolean Key_Contact, string Notes, string Likes_Dislikes, DateTime DateOfBirth)
        //{
        //    object obj = PortfolioContactAdapter.Deffinity_PortfolioContact_Insert1(PortfolioID, Name, Title, Email,
        //        Telephone, Location, Key_Contact, Notes, Likes_Dislikes, DateOfBirth);
        //    return obj;
        //    //int retval = -1;

        //    //DBForPortfolioContacts.Deffinity_PortfolioContactDataTable PFDatatable = new DBForPortfolioContacts.Deffinity_PortfolioContactDataTable();
        //    //int outval=1;
        //    //string ret;
        //    ////outval = PortfolioContactAdapter.Deffinity_PortfolioContact_Insert1(PortfolioID, Name, Title, Email, Telephone, Location, Key_Contact, Notes, Likes_Dislikes, DateOfBirth, outval);
        //    // PortfolioContactAdapter.Deffinity_PortfolioContact_Insert1(PortfolioID, Name, Title, Email, 
        //    //    Telephone, Location, Key_Contact, Notes, Likes_Dislikes, DateOfBirth);
        //    ////retval = PortfolioContactAdapter.Update(PFDatatable);
           
        //    //return retval;
        //}
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]

        public int UpdatePortfolioContacts(int ID,int PortfolioID, string Name, string Title, string Email, string Telephone, 
            string Location, Boolean Key_Contact, string Notes, string Likes_Dislikes, DateTime DateOfBirth,
            string Address1, string Address2, string Country, string Postcode, string Fax, string Mobile, string County, int departmentId, bool isDisabled)
        {
            int retval = -1;

            DBForPortfolioContacts.Deffinity_PortfolioContactDataTable PFDatatable = new DBForPortfolioContacts.Deffinity_PortfolioContactDataTable();
            //PFDatatable = PortfolioContactAdapter.GetDataByPortfolioIdName(PortfolioID, Name);
            PFDatatable = PortfolioContactAdapter.GetDataByID(ID);
            if (PFDatatable.Rows.Count > 0)
            {
               

                DBForPortfolioContacts.Deffinity_PortfolioContactRow PFContactRow = PFDatatable[0];
                //PFContactRow.ID = ID;
                PFContactRow.PortfolioID = PortfolioID;
                PFContactRow.Name = Name;
                PFContactRow.Title = Title;
                PFContactRow.Email = Email;
                PFContactRow.Telephone = Telephone;
                PFContactRow.Location = Location;
                PFContactRow.Key_Contact = Key_Contact;
                PFContactRow.Notes = Notes;
                PFContactRow.Likes_Dislikes = Likes_Dislikes;
                PFContactRow.DateOfBirth = DateOfBirth;
                PFContactRow.Address1 = Address1;
                PFContactRow.Address2 = Address2;
                PFContactRow.Country = Country;
                PFContactRow.Postcode = Postcode;
                PFContactRow.Fax = Fax;
                PFContactRow.Mobile = Mobile;
                PFContactRow.County = County;
                PFContactRow.DepartmentID = departmentId;
                PFContactRow.isDisabled = isDisabled;
                
                
                retval = PortfolioContactAdapter.Update(PFContactRow);
            }
            return retval;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]

        public int DeletePortfolioContacts(int ID)
        {
            int retval = -1;
            DBForPortfolioContacts.Deffinity_PortfolioContactDataTable PFDatatable = new DBForPortfolioContacts.Deffinity_PortfolioContactDataTable();
            PFDatatable = PortfolioContactAdapter.GetDataByID(ID);
            if (PFDatatable.Rows.Count > 0)
            {
                retval = PortfolioContactAdapter.Delete(ID);
            }
            return retval;
        }
        # region Activity
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForPortfolioContacts.PortfolioContacts_ActivityDataTable SelectAllActivities()
        {
            return ActivityAdapter.GetActivityData();
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int ActivityInsert(int ContactID, string Subject, string ActivityType, string Status, DateTime DueDate, int OwnerID)
        {
            int retval =-1;
            DBForPortfolioContacts.PortfolioContacts_ActivityDataTable ActivityTable = new DBForPortfolioContacts.PortfolioContacts_ActivityDataTable();
            DBForPortfolioContacts.PortfolioContacts_ActivityRow ActivityRow = ActivityTable.NewPortfolioContacts_ActivityRow();
            ActivityRow.ContactID = ContactID;
            ActivityRow.Subject = Subject;
            ActivityRow.ActivityType = ActivityType;
            ActivityRow.Status = Status;
            ActivityRow.DueDate = DueDate;
            ActivityRow.OwnerID = OwnerID;
            ActivityTable.AddPortfolioContacts_ActivityRow(ActivityRow);
                retval = ActivityAdapter.Update(ActivityTable);

            return retval;    
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
        public int ActivityUpdate(int ID, int ContactID, string Subject, string ActivityType, string Status, DateTime DueDate, int OwnerID)
        {
            int retval = -1;
            DBForPortfolioContacts.PortfolioContacts_ActivityDataTable ActivityTable = new DBForPortfolioContacts.PortfolioContacts_ActivityDataTable();
            ActivityTable = ActivityAdapter.GetDataByActivityID(ID);
            if (ActivityTable.Rows.Count > 0)
            {
                DBForPortfolioContacts.PortfolioContacts_ActivityRow ActivityRow = ActivityTable[0];
                ActivityRow.ContactID = ContactID;
                ActivityRow.Subject = Subject;
                ActivityRow.ActivityType = ActivityType;
                ActivityRow.Status = Status;
                ActivityRow.DueDate = DueDate;
                ActivityRow.OwnerID = OwnerID;
                retval = ActivityAdapter.Update(ActivityRow);
            }
            return retval;
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
        public int ActivityDelete(int ID)
        {
            int retval = -1;
            retval= ActivityAdapter.Delete(ID);
            return retval;
        }
        # endregion

        #region Contractors
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DBForPortfolioContacts.Contractors_SelectActiveDataTable GetActiveContractors()
        {
         return   ContractorsAdapter.GetActiveOwnerData();
        }
        #endregion


        #region Docs Download
        
        #endregion
        //public PorfolioContacts()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}
    }
}