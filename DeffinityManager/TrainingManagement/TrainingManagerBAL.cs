using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgt.DAL;
using UserMgt.Entity;
using Training.DAL;
using Training.Entity;
using Deffinity.TrainingManager;
namespace Training.BAL
{
    /// <summary>
    /// Summary description for ManageUserSkillsBAL
    /// </summary>
    public class TrainingManagerBAL
    {
        private TrainingManagerBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "User Skills"
       
        public static IEnumerable<UserMgt.Entity.Contractor> GetUserList()
        {
            int[] userTypeIds = new int[] { 1, 2, 3, 4 };
            using (UserDataContext db = new UserDataContext())
            {

                var result = (from c in db.Contractors
                              where c.Status.ToLower() == "active" && userTypeIds.Contains(Convert.ToInt32(c.SID))
                              orderby c.ContractorName
                              select c).ToList();
                return result;
            }
        }

        public static IEnumerable<UserSkill> BindUserSkills(int userId)
        {
            using (UserDataContext db = new UserDataContext())
            {
                return db.UserSkills.Where(u => u.UserId == userId).ToList();
            }
        }

        public static void InsertUserSkills(UserSkill userSkill)
        {
            using (UserDataContext db = new UserDataContext())
            {
                db.UserSkills.InsertOnSubmit(userSkill);
                db.SubmitChanges();
            }
        }

        public static void UpdateUserSkills(UserSkill userSkill)
        {
            using (UserDataContext db = new UserDataContext())
            {
                var currentUserSkill = db.UserSkills.Where(u => u.Id == userSkill.Id).FirstOrDefault();

                if (currentUserSkill != null)
                {
                    currentUserSkill.SkillLevel = userSkill.SkillLevel;
                    currentUserSkill.Skills = userSkill.Skills;
                    currentUserSkill.Notes = userSkill.Notes;
                    db.SubmitChanges();
                }
            }
        }

        public static void DeleteUserSkills(int id)
        {
            using (UserDataContext db = new UserDataContext())
            {
                UserSkill userSkill = db.UserSkills.Where(u => u.Id == id).FirstOrDefault();
                if (userSkill != null)
                {
                    db.UserSkills.DeleteOnSubmit(userSkill);
                    db.SubmitChanges();
                }
            }
        }

        #endregion

        #region "Training Booking Record"

        public static IEnumerable<Training.Entity.Category> CategoryList()
        {
            using (TrainingDataContext db = new TrainingDataContext())
            {
                return db.Categories.OrderBy(c => c.Name).ToList();
            }
        }
        public static IEnumerable<Training.Entity.Course> CourseList()
        {
            using (TrainingDataContext db = new TrainingDataContext())
            {
                return db.Courses.OrderBy(c => c.Title).ToList();
            }
        }
        public static IEnumerable<Training.Entity.Course> CourseListByCaregoryId(int categoryId)
        {
            using (TrainingDataContext db = new TrainingDataContext())
            {
                return db.Courses.Where(c => c.CategoryID == categoryId).OrderBy(c => c.Title).ToList();
            }
        }

        public static void InsertTrainingBooking(Booking booking)
        {
            using (TrainingDataContext db = new TrainingDataContext())
            {
                db.Bookings.InsertOnSubmit(booking); 
                db.SubmitChanges();
            }
        }

        public static void UpdateTrainingBooking(Booking booking)
        {
            using (TrainingDataContext db = new TrainingDataContext())
            {
                Booking currentBooking = db.Bookings.Where(b => b.ID == booking.ID).FirstOrDefault();
                if (currentBooking != null)
                {
                    currentBooking.CategoryID = booking.CategoryID;
                    currentBooking.CourseID = booking.CourseID;
                    currentBooking.DateofCourse = booking.DateofCourse;
                    currentBooking.EndDate = booking.EndDate;
                    currentBooking.StatusID = booking.StatusID;
                    db.SubmitChanges();
                }
            }
        }

        public static void DeleteTrainingBooking(int id)
        {
            using (TrainingDataContext db = new TrainingDataContext())
            {
                Booking booking = db.Bookings.Where(b => b.ID ==id).FirstOrDefault();
                if (booking != null)
                {
                    db.Bookings.DeleteOnSubmit(booking);
                    db.SubmitChanges();
                }
            }
        }

        public static IEnumerable<TrainingBookingEntity> GetTrainingBookingList(int userId)
        {
            using (TrainingDataContext db = new TrainingDataContext())
            {
                var statusList =Status.SelectAll(true);
                var categoryList = db.Categories.ToList();
                var courseList = db.Courses.ToList();
                var bookingList = db.Bookings.Where(b=>b.Employee == userId).ToList();
                var trainingBookingList = (from b in bookingList
                                           join c in categoryList on b.CategoryID equals c.ID
                                           join co in courseList on b.CourseID equals co.ID
                                           join s in statusList on b.StatusID equals Convert.ToInt32(s.Value)

                                           select new TrainingBookingEntity
                                           {
                                               ID = b.ID,
                                               CategoryID = b.CategoryID,
                                               CourseID = b.CourseID,
                                               Category = c.Name,
                                               Course = co.Title,
                                               BookedDate = b.DateofCourse,
                                               CompletedDate = b.EndDate,
                                               StatusID = b.StatusID,
                                               Status = s.Text

                                           }).ToList();

                return trainingBookingList;

            }
        }
        
        #endregion


    }

    public class TrainingBookingEntity
    {
        public int ID { get; set; }
        public int? CategoryID { get; set; }
        public int? CourseID { get; set; }
        public string Category { get; set; }
        public string Course { get; set; }
        public DateTime? BookedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }
    }
}