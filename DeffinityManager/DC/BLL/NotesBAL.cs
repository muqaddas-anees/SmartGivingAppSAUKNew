using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BAL
{
/// <summary>
/// Summary description for NotesBAL
/// </summary>
    public class NotesBAL
    {
        public NotesBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<Note> GetNotesList(int callId)
        {
            using (DCDataContext db = new DCDataContext())
            {
                return db.Notes.Where(n => n.CallID == callId).ToList();
            }
        }
        public static void AddNotes(Note note)
        {
            using (DCDataContext db = new DCDataContext())
            {
                db.Notes.InsertOnSubmit(note);
                db.SubmitChanges();
            }
        }
        public static void UpdateNotes(Note note)
        {
            using (DCDataContext db = new DCDataContext())
            {
                var notesPresent = db.Notes.Where(n => n.ID == note.ID).FirstOrDefault();
                if (notesPresent != null)
                {
                    notesPresent.Notes = note.Notes;
                    db.SubmitChanges();
                }

            }
        }
        public static void DeleteNotesByID(int id)
        {
            using (DCDataContext db = new DCDataContext())
            {
                Note note = db.Notes.Where(n => n.ID == id).FirstOrDefault();
                if (note != null)
                {
                    db.Notes.DeleteOnSubmit(note);
                    db.SubmitChanges();
                }

            }
        }
    }
         

}