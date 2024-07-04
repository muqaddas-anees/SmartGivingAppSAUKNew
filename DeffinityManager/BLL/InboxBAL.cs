using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgt.DAL;
using UserMgt.Entity;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for InboxBAL
/// </summary>
public class InboxBAL
{
    
    public static int GetUserInboxCount()
    {
        using (UserDataContext db = new UserDataContext())
        {
           return db.InboxMessages.Where(i => i.UserID == sessionKeys.UID && i.IsViewed == false).Select(i => i).Count();
        }
    }
    public static IEnumerable<InboxMessage> BindUserInbox()
    {
        using (UserDataContext db = new UserDataContext())
        {
            return db.InboxMessages.Where(i => i.UserID == sessionKeys.UID).OrderByDescending(i => i.ID).Select(i => i).ToList();
        }
    }
    public static IEnumerable<InboxMessage> BindUserInbox(int userID)
    {
        using (UserDataContext db = new UserDataContext())
        {
            return db.InboxMessages.Where(i => i.UserID == userID).OrderByDescending(i => i.ID).Select(i => i).ToList();
        }
    }
    public static InboxMessage InboxMessageByID(int id)
    {
        using (UserDataContext db = new UserDataContext())
        {
           return db.InboxMessages.Where(i => i.ID == id).Select(i => i).FirstOrDefault();
        }
    }
    #region "Update Inbox Viewed Status by ID"
    public static void UpdateInbox(int id)
    {
        using (UserDataContext db = new UserDataContext())
        {
            InboxMessage inboxMessage = db.InboxMessages.Where(i => i.ID == id).Select(i => i).FirstOrDefault();
            if (inboxMessage != null)
            {
                inboxMessage.IsViewed = true;
                db.SubmitChanges();
            }
        }
    }
    #endregion
    public static void InboxDeleteByGuid(Guid guid)
    {
        using (UserDataContext db = new UserDataContext())
        {
            InboxMessage inboxMessage = db.InboxMessages.Where(i => i.Gid == guid).Select(i => i).FirstOrDefault();
            if (inboxMessage != null)
            {
                db.InboxMessages.DeleteOnSubmit(inboxMessage);
                db.SubmitChanges();
            }
        }
    }
    public static void SaveInboxMessage(string subject, int userId, string toAddress, string message)
    {
        using (UserDataContext ud = new UserDataContext())
        {
            string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/Inbox");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            Guid guid = Guid.NewGuid();
            InboxMessage inboxMessage = new InboxMessage();
            inboxMessage.Gid = guid;
            inboxMessage.ReceivedDate = DateTime.Now;
            inboxMessage.Subject = subject;
            inboxMessage.UserID = userId;
            inboxMessage.IsViewed = false;
            inboxMessage.FromAddress = Deffinity.systemdefaults.GetFromEmail();
            inboxMessage.ToAddress = toAddress;
            ud.InboxMessages.InsertOnSubmit(inboxMessage);
            ud.SubmitChanges();
            string fileName = path + "\\" + guid + ".htm";
            if (!File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.WriteLine(message);

                    }
                }
            }
        }
    }
   
}