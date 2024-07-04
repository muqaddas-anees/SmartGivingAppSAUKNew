using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Therapy.DAL;
using Therapy.Entity;


/// <summary>
/// Summary description for KeyMarkersBAL
/// </summary>
/// 
namespace TP.BLL
{
    public class KeyMarkersBAL
    {
        #region Insert
        public static void KeyMarker_Insert(KeyMarker km)
        {
            using (TherapyDataContext db = new TherapyDataContext())
            {
                db.KeyMarkers.InsertOnSubmit(km);
                db.SubmitChanges();
            }
        }
        #endregion

        #region Check exist
        public static bool Keymarker_Exist(string strname, int tid)
        {
            KeyMarker km = new KeyMarker();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                km  = (from k in td.KeyMarkers
                      where k.Name.ToLower() == strname.ToLower() && k.TrialID == tid

                      select k).FirstOrDefault();
            }
            if (km == null)
                return false;
            else
                return true;

        }
        #endregion

        #region Delete
        public static void Keymarker_Delete(int id)
        {
            using (TherapyDataContext td = new TherapyDataContext())
            {

                var keymarker = (from km in td.KeyMarkers
                         where km.ID == id
                         select km).FirstOrDefault();
                td.KeyMarkers.DeleteOnSubmit(keymarker);
                td.SubmitChanges();

            }


        }
        #endregion

        #region Select by id
        public static KeyMarker KeyMarker_selectByID(int id)
        {
            KeyMarker km = new KeyMarker();
            using (TherapyDataContext td = new TherapyDataContext())
            {

                km = (from k in td.KeyMarkers
                     where k.ID == id
                     select k).FirstOrDefault();
            }

            return km;
        }
        #endregion

        #region Select all
        public static List<KeyMarkerList> KeyMarker_selectAll()
        {

            List<KeyMarkerList> keymarker = new List<KeyMarkerList>();
            using (TherapyDataContext td = new TherapyDataContext())
            {
                
                    var trial = td.TrialConfigurations.Select(p => p).ToList();
                    var key = td.KeyMarkers.Select(d => d).ToList();
                   
                    keymarker = (from t in trial
                                 join k in key on t.ID equals k.TrialID

                                 select new KeyMarkerList
                             {
                                ID = k.ID,
                                Name = k.Name,
                                Trial = t.Name,
                                Status = t.Status
                             }).ToList();
                
            }
            return keymarker;
        }
        #endregion
    }
}