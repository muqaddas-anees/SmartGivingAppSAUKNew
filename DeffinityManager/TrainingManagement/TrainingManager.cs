using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

/// <summary>
/// Summary description for TrainingManager
/// </summary>
/// 

namespace Deffinity.TrainingManager
{

#region Department
    public class Department
    {
        
        public static void Department_InsertUpdate(DepartmentEntity dep)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Department_InsertUpdate", new SqlParameter("@ID", dep.ID), new SqlParameter("@Name", dep.Name));
        }
        public static void Department_Delete(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Department_Delete", new SqlParameter("@ID", ID));
        }
        public static DataTable Department_SelectAll_Datatable()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Department_SelectAll").Tables[0];
            return dt;
        }
        public static SqlDataReader Department_Select_Datareader(int id)
        {
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Training.Department_Select", new SqlParameter("@ID", id));
            return dr;
        }
        public static IEnumerable<DepartmentEntity> Department_SelectAll()
        {
            List<DepartmentEntity> DepartmentCollection = new List<DepartmentEntity>();
            DataTable dt = Department_SelectAll_Datatable();
            int R_cont = dt.Rows.Count;
             if (R_cont > 0)
             {
                for (int T1_cnt = 0; T1_cnt <= dt.Rows.Count - 1; T1_cnt++)
                {
                    var dep = new DepartmentEntity
                   {
                       ID = int.Parse(dt.Rows[T1_cnt]["ID"].ToString()),
                       Name = dt.Rows[T1_cnt]["Name"].ToString()
                   };
                    DepartmentCollection.Add(dep);
                }
             }
             return DepartmentCollection;
        }
        public static DepartmentEntity Department_Select(int id)
        {
            DepartmentEntity dep = new DepartmentEntity();
            SqlDataReader dr = Department_Select_Datareader(id);
            try
            {
                while (dr.Read())
                {
                    dep = new DepartmentEntity
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Name = dr["Name"].ToString()
                    };
                }
            }
            catch { }
            finally { dr.Close(); }
            return dep;
        }

        public static DataTable DepartmentCustomer_select(int DepartmentID)
        {
            DataTable dt = new DataTable();
            if(DepartmentID == 0)
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT Contractors.ID, Contractors.ContractorName, Training.Department.Name as DepartmentName,(select Name from Training.Area where ID= AreaID) as AreaName FROM Contractors INNER JOIN Training.Department ON Contractors.DepartmentID = Training.Department.ID ORDER BY  Contractors.ContractorName ASC ").Tables[0];
            else
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT Contractors.ID, Contractors.ContractorName, Training.Department.Name as DepartmentName,(select Name from Training.Area where ID= AreaID) as AreaName FROM Contractors INNER JOIN Training.Department ON Contractors.DepartmentID = Training.Department.ID WHERE (Contractors.DepartmentID = @DepartmentID) ORDER BY  Contractors.ContractorName ASC ", new SqlParameter("@DepartmentID", DepartmentID)).Tables[0];

            return dt;
        }
        public static DataTable DepartmentCustomer_selectAll()
        {
            DataTable dt = new DataTable();

            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT Contractors.ID, Contractors.ContractorName FROM Contractors where Status='Active' order by ContractorName ").Tables[0];

            return dt;
        }
       
    }

#endregion

#region Category
public class Category
{
    public static void Category_InsertUpdate(CategoryEntity dep)
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Category_InsertUpdate", new SqlParameter("@ID", dep.ID), new SqlParameter("@Name", dep.Name));
    }
    
    public static void Category_Delete(int ID)
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Category_Delete", new SqlParameter("@ID", ID));
    }
    public static DataTable Category_SelectAll_Datatable()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Category_SelectAll").Tables[0];
        return dt;
    }
    public static SqlDataReader Category_Select_Datareader(int id)
    {
        SqlDataReader dr = null;
        dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Training.Category_Select", new SqlParameter("@ID", id));
        return dr;
    }
    public static IEnumerable<CategoryEntity> Category_SelectAll()
    {
        List<CategoryEntity> CategoryCollection = new List<CategoryEntity>();
        DataTable dt = Category_SelectAll_Datatable();
        int R_cont = dt.Rows.Count;
        if (R_cont > 0)
        {
            for (int T1_cnt = 0; T1_cnt <= dt.Rows.Count - 1; T1_cnt++)
            {
                var dep = new CategoryEntity
                {
                    ID = int.Parse(dt.Rows[T1_cnt]["ID"].ToString()),
                    Name = dt.Rows[T1_cnt]["Name"].ToString()
                };
                CategoryCollection.Add(dep);
            }
        }
        return CategoryCollection;
    }
    public static IEnumerable<CategoryEntity> Category_OrderByAsc()
    {
        var category = from r in Category_SelectAll()
                       orderby r.Name
                       select r;
        return category;
    }

    public static CategoryEntity Category_Select(int id)
    {
        CategoryEntity dep = new CategoryEntity();
        SqlDataReader dr = Category_Select_Datareader(id);
        try
        {
            while (dr.Read())
            {
                dep = new CategoryEntity
                {
                    ID = int.Parse(dr["ID"].ToString()),
                    Name = dr["Name"].ToString()
                };
            }
        }
        catch { }
        finally { dr.Close(); }
        return dep;
    }


}
#endregion

#region Course
    public class Course
    {

        public static void Classification_InsertDelete(int courseID, string classficationIDs)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Classfication_Insert", new SqlParameter("@CourseID", courseID),
                new SqlParameter("@ClassificationIDs", classficationIDs));
        }
        public static DataTable Classification_selectAll()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Classfication_SelectAll").Tables[0];
            return dt;
        }
        public static int Course_InsertUpdate(CourseEntity dep)
        {
            SqlParameter courseID = new SqlParameter("@CourseID",SqlDbType.Int);
           
            courseID.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Course_InsertUpdate",
                new SqlParameter("@ID", dep.ID), new SqlParameter("@Title", dep.Title),
                new SqlParameter("@CategoryID",dep.CategoryID), new SqlParameter("@VendorID",dep.VendorID),
                new SqlParameter("@Venue",dep.Venue),new SqlParameter("@Rate",dep.Rate)
                , new SqlParameter("@Duration", dep.Duration), new SqlParameter("@TrainingTypeID", dep.TrainingTypeID),
                new SqlParameter("@RequirementID",dep.RequirementID),
                new SqlParameter("@CourseDesc",dep.CourseDesc),courseID);


            BaseCache.Cache_Remove(CacheNames.DefaultNames.TrainingBookings.ToString());
            return Convert.ToInt32(courseID.Value);
           
        }
        public static DataTable Course_SelectAll_Datatable()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Course_SelectAll").Tables[0];
            return dt;
        }
        
        public static DataTable DepartmentCustomer_selectAll()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT Contractors.ID, Contractors.ContractorName FROM Contractors where Status='Active' order by ContractorName").Tables[0];
            return dt;
        }
        public static void Course_Delete(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Course_Delete",
                new SqlParameter("@ID", ID));
        }
        public static SqlDataReader Course_Select_Datareader(int id)
        {
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Training.Course_Select", new SqlParameter("@ID", id));
            return dr;
        }
        public static SqlDataReader Classification_DataReader(int CourseID)
        {
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Training.Classfication_SelectByID", new SqlParameter("@CourseID", CourseID));
            return dr;
        }
        public static IEnumerable<CourseEntity> Course_SelectAll()
        {
            List<CourseEntity> CourseCollection = new List<CourseEntity>();
            DataTable dt = Course_SelectAll_Datatable();
            int R_cont = dt.Rows.Count;
            if (R_cont > 0)
            {
                for (int T1_cnt = 0; T1_cnt <= dt.Rows.Count - 1; T1_cnt++)
                {
                    var dep = new CourseEntity
                    {
                        ID = int.Parse(dt.Rows[T1_cnt]["ID"].ToString()),
                        Title = dt.Rows[T1_cnt]["Title"].ToString(),
                        CategoryID = int.Parse(dt.Rows[T1_cnt]["CategoryID"].ToString()),
                        CategoryName = dt.Rows[T1_cnt]["CategoryName"].ToString(),
                        VendorID = int.Parse(dt.Rows[T1_cnt]["VendorID"].ToString()),
                        VendorName = dt.Rows[T1_cnt]["VendorName"].ToString(),
                        Venue = dt.Rows[T1_cnt]["Venue"].ToString(),
                        Duration = dt.Rows[T1_cnt]["Duration"].ToString(),
                        Rate = double.Parse( dt.Rows[T1_cnt]["Rate"].ToString()),
                        TrainingTypeID = int.Parse(dt.Rows[T1_cnt]["TrainingTypeID"].ToString()),
                        TrainingTypeName = dt.Rows[T1_cnt]["TrainingTypeName"].ToString(),
                        RequirementID = int.Parse(dt.Rows[T1_cnt]["RequirementID"].ToString()),
                        RequirementName = dt.Rows[T1_cnt]["RequirementName"].ToString(),
                        CourseDesc = dt.Rows[T1_cnt] ["CourseDesc"].ToString()
                        
                    };
                    CourseCollection.Add(dep);
                }
            }
            return CourseCollection;
        }

        public static IEnumerable<CourseEntity> Course_ByOrderAsc()
        {
            var courses = from r in Course_SelectAll()
                          orderby r.Title
                          select r;
            return courses;
        }
        //get the courses by category
        public static IEnumerable<CourseEntity> Course_SelectByCategory(int CategoryID)
        {
            IEnumerable<CourseEntity> CourseCollection = from r in Course_SelectAll() 
                                                      where r.CategoryID == CategoryID
                                                      orderby r.Title ascending
                                                  select r;


            return CourseCollection;
        }
        public static ClassificationEntity Course_ClassficationSelect(int CourseID)
        {
            ClassificationEntity dep = new ClassificationEntity();
            SqlDataReader dr = Classification_DataReader(CourseID);
            try
            {
                while (dr.Read())
                {
                    dep = new ClassificationEntity
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        classficationID = dr["ClassficationID"].ToString(),
                        CourseID = int.Parse(dr["CourseID"].ToString())
                    };
                }
            }
            catch { }
            finally { dr.Close(); }
            return dep;
        }
        public static CourseEntity Course_Select(int id)
        {
            CourseEntity dep = new CourseEntity();
            SqlDataReader dr = Course_Select_Datareader(id);
            try
            {
                while (dr.Read())
                {
                    dep = new CourseEntity
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Title = dr["Title"].ToString(),
                        CategoryID = int.Parse(dr["CategoryID"].ToString()),
                        CategoryName = dr["CategoryName"].ToString(),
                        VendorID = int.Parse(dr["VendorID"].ToString()),
                        VendorName = dr["VendorName"].ToString(),
                        Venue = dr["Venue"].ToString(),
                        Duration = dr["Duration"].ToString(),
                        Rate = double.Parse(dr["Rate"].ToString()),
                        TrainingTypeID = int.Parse(dr["TrainingTypeID"].ToString()),
                        TrainingTypeName = dr["TrainingTypeName"].ToString(),
                        RequirementID = int.Parse(dr["RequirementID"].ToString()),
                        RequirementName = dr["RequirementName"].ToString(),
                        CourseDesc = dr["CourseDesc"].ToString()
                    };
                }
            }
            catch { }
            finally { dr.Close(); }
            return dep;
        }
    }

    #endregion
    
#region DepartmentToCourse
    public class DepartmentToCourse
    {
        public static void Category_InsertUpdate(DepartmentToCourseEntity dep)
        {

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.DepartmentToCourse_InsertUpdate", 
                new SqlParameter("@ID", dep.ID), new SqlParameter("@CustomerID",dep.CustomerID),
                new SqlParameter("@siteID", dep.SiteID), new SqlParameter("@DepartmentID",dep.DepartmentID),
                new SqlParameter("@CourseID", dep.CourseID), new SqlParameter("@MinRequired",dep.MinRequired),
                new SqlParameter("@Target", dep.Target), new SqlParameter("@AreaID", dep.AreaID),
                new SqlParameter("@FromDate", dep.FromDate), new SqlParameter("@ToDate", dep.ToDate));
            //remove the cache
            BaseCache.Cache_Remove(CacheNames.DefaultNames.TrainingBookings.ToString());
        }
        public static DataTable DepartmentToCourse_SelectAll_Datatable()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.DepartmentToCourse_SelectAll").Tables[0];
            return dt;
        }
       
        public static SqlDataReader DepartmentToCourse_Select_Datareader(int id)
        {
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Training.DepartmentToCourse_Select", new SqlParameter("@ID", id));
            return dr;
        }
        public static IEnumerable<DepartmentToCourseEntity> DepartmentToCourseCollection_SelectAll()
        {
            List<DepartmentToCourseEntity> DepartmentToCourseCollection = new List<DepartmentToCourseEntity>();
            DataTable dt = DepartmentToCourse_SelectAll_Datatable();
            int R_cont = dt.Rows.Count;
            if (R_cont > 0)
            {
                for (int T1_cnt = 0; T1_cnt <= dt.Rows.Count - 1; T1_cnt++)
                {
                    var dep = new DepartmentToCourseEntity
                    {
                        ID = int.Parse(dt.Rows[T1_cnt]["ID"].ToString()),
                        CustomerID = int.Parse(dt.Rows[T1_cnt]["CustomerID"].ToString()),
                        CustomerName = dt.Rows[T1_cnt]["CustomerName"].ToString(),
                        DepartmentID = int.Parse(dt.Rows[T1_cnt]["DepartmentID"].ToString()),
                        DepartmentName = dt.Rows[T1_cnt]["DepartmentName"].ToString(),
                        CourseID = int.Parse(dt.Rows[T1_cnt]["CourseID"].ToString()),
                        CourseName = dt.Rows[T1_cnt]["CourseTitle"].ToString(),
                        SiteID = int.Parse(dt.Rows[T1_cnt]["SiteID"].ToString()),
                        SiteName = dt.Rows[T1_cnt]["SiteName"].ToString(),
                        MinRequired = int.Parse(dt.Rows[T1_cnt]["MinRequired"].ToString()),
                        Target =int.Parse(dt.Rows[T1_cnt]["Target"].ToString()),
                        AreaID = int.Parse(dt.Rows[T1_cnt]["AreaID"].ToString()),
                        AreaName = dt.Rows[T1_cnt]["AreaName"].ToString(),
                        FromDate = Convert.ToDateTime(string.IsNullOrEmpty(dt.Rows[T1_cnt]["FromDate"].ToString()) ? "01/01/1900" : dt.Rows[T1_cnt]["FromDate"].ToString()),
                        ToDate = Convert.ToDateTime(string.IsNullOrEmpty(dt.Rows[T1_cnt]["ToDate"].ToString()) ? "01/01/1900" : dt.Rows[T1_cnt]["ToDate"].ToString())
                    };
                    DepartmentToCourseCollection.Add(dep);
                }
            }
            return DepartmentToCourseCollection;
        }
        public static DepartmentToCourseEntity DepartmentToCourseCollection_Select(int id)
        {
            DepartmentToCourseEntity dep = new DepartmentToCourseEntity();
            SqlDataReader dr = DepartmentToCourse_Select_Datareader(id);
            try
            {
                while (dr.Read())
                {
                    dep = new DepartmentToCourseEntity
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        CustomerID = int.Parse(dr["CustomerID"].ToString()),
                        CustomerName = dr["CustomerName"].ToString(),
                        DepartmentID = int.Parse(dr["DepartmentID"].ToString()),
                        DepartmentName = dr["DepartmentName"].ToString(),
                        CourseID = int.Parse(dr["CourseID"].ToString()),
                        CourseName = dr["CourseTitle"].ToString(),
                        SiteID = int.Parse(dr["SiteID"].ToString()),
                        SiteName = dr["SiteName"].ToString(),
                        MinRequired = int.Parse(dr["MinRequired"].ToString()),
                        Target = int.Parse(dr["Target"].ToString()),
                        AreaID = int.Parse(dr["AreaID"].ToString()),
                        AreaName = dr["AreaName"].ToString(),
                        FromDate = Convert.ToDateTime(dr["FromDate"].ToString()),
                        ToDate =  Convert.ToDateTime(dr["ToDate"].ToString())
                    };
                }
            }
            catch { }
            finally { dr.Close(); }
            return dep;
        }
        public static DepartmentToCourseEntity DepartmentToCourseCollection_SelectByDeparmentID(int deparmentID)
       {
           DepartmentToCourseEntity CourseCollection = (from r in DepartmentToCourseCollection_SelectAll() 
                                                      where r.DepartmentID == deparmentID
                                                        select r).FirstOrDefault();

            return CourseCollection;
       }
        public static DepartmentToCourseEntity DepartmentToCourseCollection_SelectByAreaID(int deparmentID,int areaID)
        {
            DepartmentToCourseEntity CourseCollection = (from r in DepartmentToCourseCollection_SelectAll()
                                                         where r.DepartmentID == deparmentID && r.AreaID == areaID
                                                         select r).FirstOrDefault();

            return CourseCollection;
        }
        public static DepartmentToCourseEntity DepartmentToCourseCollection_SelectByCourseID(int deparmentID, int areaID,int CourseID)
        {
            DepartmentToCourseEntity CourseCollection = (from r in DepartmentToCourseCollection_SelectAll()
                                                         where r.DepartmentID == deparmentID && r.AreaID == areaID && r.CourseID == CourseID
                                                         select r).FirstOrDefault();

            return CourseCollection;
        }
        //created on 28/08/2010
        public static IEnumerable<DepartmentToCourseEntity> DepartmentToCourseCollection_ToSecondGrid(int deparmentID)
        {
            IEnumerable<DepartmentToCourseEntity> CourseCollection = (from r in DepartmentToCourseCollection_SelectAll()
                                                                      where r.DepartmentID == deparmentID
                                                                      select r).ToList();
            return CourseCollection;
        }
        public static int DepartmentToCourse_GetMinReqDays(int deparmentID, int CourseID)
        {
            int MinReq = 0;
            DepartmentToCourseEntity CourseCollection = (from r in DepartmentToCourseCollection_SelectAll()
                                                         where r.DepartmentID == deparmentID  && r.CourseID == CourseID
                                                         select r).FirstOrDefault();
            if (CourseCollection != null)
                MinReq = CourseCollection.MinRequired;

            return MinReq;
        }
        //new
        public static int DepartmentToCourse_GetMinReqDays(int deparmentID, int CourseID,int areaID)
        {
            int MinReq = 0;
            DepartmentToCourseEntity CourseCollection = (from r in DepartmentToCourseCollection_SelectAll()
                                                         where r.DepartmentID == deparmentID && r.CourseID == CourseID && r.AreaID == areaID
                                                         select r).FirstOrDefault();
            if (CourseCollection != null)
                MinReq =CourseCollection.Target;

            return MinReq;
        }
        public static int DepartmentToCourse_GetMinReqDays_sum(int deparmentID)
        {
            int MinReq = 0;
            MinReq = (from r in DepartmentToCourseCollection_SelectAll()
                                                         where r.DepartmentID == deparmentID
                                                         select r).Sum(n=> n.MinRequired);
           

            return MinReq;
        }
        //new
        public static int DepartmentToCourse_GetMinReqDays_sum(int deparmentID,int areaID)
        {
            int MinReq = 0;
            MinReq = (from r in DepartmentToCourseCollection_SelectAll()
                      where r.DepartmentID == deparmentID && r.AreaID == areaID
                      select r).Sum(n =>n.Target);


            return MinReq;
        }
        public static int DepartmentToCourse_GetMinReq_sum(int deparmentID,int areaID)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            int MinReq = 0;
            if (deparmentID != 0 && areaID != 0 )
            {
                MinReq = (from r in DepartmentToCourseCollection_SelectAll()
                          where r.DepartmentID == deparmentID && r.AreaID == areaID
                          select r).Sum(n => n.MinRequired);
            }

            return MinReq;
        }
        public static double DepartmentToCourse_GetTarget_Avg(int deparmentID, int areaID)
        {
            double MinReq = 0;
            DepartmentToCourseEntity CourseCollection = (from r in DepartmentToCourseCollection_SelectAll()
                                                         where r.DepartmentID == deparmentID && r.AreaID == areaID
                                                         select r).FirstOrDefault();
            if (CourseCollection != null)
            {
                MinReq = (from r in DepartmentToCourseCollection_SelectAll()
                          where r.DepartmentID == deparmentID && r.AreaID == areaID && r.CourseID > 0
                          select r).Average(n => n.Target);
            }
            else
            {
                MinReq = 0;
            }

            return MinReq;
        }
    }
    #endregion

#region Bookings
    public class Bookings
    {
        //BookingDate,EndDate,StartTime,EndTime,Expenses,Penalties,DurationInDays,Outcome
        public static int Bookings_InsertUpdate(BookingsEntity dep)
        {
            SqlParameter sqlReturn = new SqlParameter("@return",SqlDbType.Int);
            sqlReturn.Direction = ParameterDirection.Output;

            SqlParameter BookingID = new SqlParameter("@BookingID ", SqlDbType.Int);
            BookingID.Direction = ParameterDirection.Output;

           SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Bookings_InsertUpdate",
                new SqlParameter("@ID", dep.ID), new SqlParameter("@DateofCourse", dep.DateofCourse),
                new SqlParameter("@Employee", dep.Employee), new SqlParameter("@CategoryID", dep.CategoryID),
                new SqlParameter("@DepartmentID", dep.DepartmentID), new SqlParameter("@CourseID", dep.CourseID),
                new SqlParameter("@StatusID", dep.StatusID), new SqlParameter("@CheckedBy", dep.CheckedBy),
                new SqlParameter("@CheckedDate", dep.CheckedDate), new SqlParameter("@RequiredByDate", dep.RequiredByDate),
                new SqlParameter("@Instructor", dep.Instructor), new SqlParameter("@CourseVenue", dep.CourseVenue),
                new SqlParameter("@NotifyDaysPrior", dep.NotifyDaysPrior), new SqlParameter("@CostofCourse", dep.CostofCourse),
                new SqlParameter("@NotifyUser", dep.NotifyUser), new SqlParameter("@FeedBack", dep.FeedBack),
                new SqlParameter("@FileID", dep.FileID), new SqlParameter("@BookingDate", dep.BookingDate),
                new SqlParameter("@EndDate", dep.EndDate), new SqlParameter("@StartTime", dep.StartTime),
                new SqlParameter("@EndTime", dep.EndTime), new SqlParameter("@Expenses", dep.Expenses),
                new SqlParameter("@Penalties", dep.Penalties), new SqlParameter("@DurationInDays", dep.DurationInDays),
                new SqlParameter("@Outcome", dep.Outcome), new SqlParameter("@Comments", dep.Comments),
                 new SqlParameter("@FileName", dep.FileName), new SqlParameter("@Budget", dep.Budget), new SqlParameter("@CourseReoccurs", dep.CourseReoccurs),
                 new SqlParameter("@ReFrequencey", dep.ReFrequencey),new SqlParameter("@Day",dep.Day),
                 new SqlParameter("@UntilDate", dep.UntilDate), sqlReturn,BookingID
                 );

           int exist = Convert.ToInt32(sqlReturn.Value);
            int bookingId=Convert.ToInt32(BookingID.Value);
            
           // remove the cahe content
            BaseCache.Cache_Remove(CacheNames.DefaultNames.TrainingBookings.ToString());
            if (exist == 1)
            {
                return exist;
            }
            else if (exist == 0 && bookingId!=0)
            {
                return bookingId;
            }
            else
            {
              
              return exist;
            }

        }

        public static void Bookings_UpdateFeedBackMail(int ID,int received)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Bookings_FeedBackMailSent",
                new SqlParameter("@ID", ID),new SqlParameter("@ISExist",received));
            BaseCache.Cache_Remove(CacheNames.DefaultNames.TrainingBookings.ToString());
        }

        //Sani
        public static DataTable Bookings_SelectFilesAll_DataTable()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Booking_FileSelectAll").Tables[0];
            return dt;
        }
        //Sani
        public static void Bookings_UploadFilePaths(UploadFileEntity upfile)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Booking_InsertFilesPath",
                new SqlParameter("FilePath", upfile.FilePath), new SqlParameter("@BookingID", upfile.BookingID), new SqlParameter("@FileName", upfile.FileName));
        }
        public static DataTable Bookings_SelectAll_Datatable()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Bookings_SelectAll").Tables[0];
            return dt;
        }
        public static SqlDataReader Bookings_Select_Datareader(int id)
        {
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Training.Bookings_Select", new SqlParameter("@ID", id));
            //dr.Close();
            return dr;
        }
        //Sani
        public static SqlDataReader Bookings_UploadFile_Reader(int id)
        {
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Training.Booking_FileSelect_ByID", new SqlParameter("@BookingID", id));
            return dr;
        }
        public static SqlDataReader UserDetails(int userID)
        {
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, "select ID,ContractorName,isnull((select Name from Training.Department where ID = Contractors.DepartmentID),'') as DepartmentName,isnull((select ExpClassification from ExperienceClassification where ID= Contractors.ExpClassification),'') as ExperienceName,ContactNumber from Contractors where ID = @ID order by ContractorName", new SqlParameter("@ID", userID));
            return dr;
        }
        public static void UserDetails(int userID, out string name, out string DepartmentName, out string ExperienceName,out string telephone)
        {
            name = string.Empty;
            DepartmentName = string.Empty;
            ExperienceName = string.Empty;
            telephone = string.Empty;
            SqlDataReader dr = UserDetails(userID);
            try
            {
                while (dr.Read())
                {
                    name = dr["ContractorName"].ToString();
                    DepartmentName = dr["DepartmentName"].ToString();
                    ExperienceName = dr["ExperienceName"].ToString();
                    telephone = dr["ContactNumber"].ToString();

                }
            }
            catch { }
            finally
            {
                dr.Close();
            }
        }
        //get the courses by category
        public static IEnumerable<BookingsEntity> Bookings_SelectByDepartment(int DepartmentID)
        {// IEnumerable<BookingsEntity>
            IEnumerable<BookingsEntity> BookingsCollection = from r in Bookings_SelectAll()
                                                             where r.DepartmentID == DepartmentID || r.ID == -99
                                                             select r;


            return BookingsCollection;
        }
        public static List<BookingsEntity> Bookings_SelectByDepartment_list(int DepartmentID)
        {
            List<BookingsEntity> BookingsCollection = (from r in Bookings_SelectAll()
                                                       where (r.DepartmentID == DepartmentID || r.ID == -99) 
                                                       select r).ToList();


            return BookingsCollection;
        }
        public static IEnumerable<BookingsEntity> Bookings_SelectByDates(DateTime fromDate, DateTime toDate)
        {
            IEnumerable<BookingsEntity> BookingsCollection = from r in Bookings_SelectAll()
                                                             where r.ID!=-99 && r.DateofCourse >= fromDate && r.DateofCourse <= toDate
                                                             select r;


            return BookingsCollection;
        }
        public static IEnumerable<BookingsEntity> Bookings_SelectByDatesCourse(DateTime fromDate, DateTime toDate,int courseID)
        {
            IEnumerable<BookingsEntity> BookingsCollection = from r in Bookings_SelectAll()
                                                             where r.ID != -99 && r.DateofCourse >= fromDate && r.DateofCourse <= toDate && r.CourseID == courseID
                                                             select r;


            return BookingsCollection;
        }
        public static IEnumerable<BookingsEntity> Bookings_SelectByCourses(int courseID)
        {
            IEnumerable<BookingsEntity> BookingsCollection = from r in Bookings_SelectAll()
                                                             where r.ID != -99 && r.CourseID == courseID
                                                             select r;


            return BookingsCollection;
        }
        public static IEnumerable<BookingsEntity> Bookings_SelectAll()
        {
            List<BookingsEntity> BookingsEntityCollection= null;

            string key = CacheNames.DefaultNames.TrainingBookings.ToString();
            if (BaseCache.Cache_Select(key) == null)
            {
                
             BookingsEntityCollection = new List<BookingsEntity>();
            DataTable dt = Bookings_SelectAll_Datatable();
            int R_cont = dt.Rows.Count;
            if (R_cont > 0)
            {
                for (int T1_cnt = 0; T1_cnt <= dt.Rows.Count - 1; T1_cnt++)
                {
                    var dep = new BookingsEntity
                    {
                        ID = int.Parse(dt.Rows[T1_cnt]["ID"].ToString()),
                        CategoryID = int.Parse(dt.Rows[T1_cnt]["CategoryID"].ToString()),
                        CategoryName = dt.Rows[T1_cnt]["CategoryName"].ToString(),
                        CourseID = int.Parse(dt.Rows[T1_cnt]["CourseID"].ToString()),
                        CourseTitle = dt.Rows[T1_cnt]["CourseTitle"].ToString(),
                        DepartmentID = int.Parse(dt.Rows[T1_cnt]["DepartmentID"].ToString()),
                        DepartmentName = dt.Rows[T1_cnt]["DepartmentName"].ToString(),
                        CheckedBy = int.Parse(dt.Rows[T1_cnt]["CheckedBy"].ToString()),
                        CheckedByName = dt.Rows[T1_cnt]["CheckedByName"].ToString(),
                        StatusID = int.Parse(dt.Rows[T1_cnt]["StatusID"].ToString()),
                        StatusName = dt.Rows[T1_cnt]["StatusName"].ToString(),
                        NotifyUser = dt.Rows[T1_cnt]["NotifyUser"].ToString(),
                        NotifyUserName = dt.Rows[T1_cnt]["NotifyUserName"].ToString(),
                        Employee = int.Parse(dt.Rows[T1_cnt]["Employee"].ToString()),
                        EmployeeName = dt.Rows[T1_cnt]["EmployeeName"].ToString(),
                        CourseVenue = dt.Rows[T1_cnt]["CourseVenue"].ToString(),
                        FeedBack = dt.Rows[T1_cnt]["FeedBack"].ToString(),
                        CostofCourse = double.Parse(dt.Rows[T1_cnt]["CostofCourse"].ToString()),
                        NotifyDaysPrior = int.Parse(dt.Rows[T1_cnt]["NotifyDaysPrior"].ToString()),
                        Instructor = dt.Rows[T1_cnt]["Instructor"].ToString(),
                        CheckedDate = DateTime.Parse(dt.Rows[T1_cnt]["CheckedDate"].ToString()),
                        DateofCourse = DateTime.Parse(dt.Rows[T1_cnt]["DateofCourse"].ToString()),
                        RequiredByDate = DateTime.Parse(dt.Rows[T1_cnt]["RequiredByDate"].ToString()),
                        FileID = int.Parse(dt.Rows[T1_cnt]["FileID"].ToString()),
                        BookingDate = DateTime.Parse(dt.Rows[T1_cnt]["BookingDate"].ToString()),
                        EndDate = DateTime.Parse(dt.Rows[T1_cnt]["EndDate"].ToString()),
                        StartTime = dt.Rows[T1_cnt]["StartTime"].ToString(),
                        EndTime = dt.Rows[T1_cnt]["EndTime"].ToString(),
                        DurationInDays = int.Parse(dt.Rows[T1_cnt]["DurationInDays"].ToString()),
                        Expenses = double.Parse(dt.Rows[T1_cnt]["Expenses"].ToString()),
                        Penalties = double.Parse(dt.Rows[T1_cnt]["Penalties"].ToString()),
                        Outcome = int.Parse(dt.Rows[T1_cnt]["Outcome"].ToString()),
                        OutcomeName = dt.Rows[T1_cnt]["OutcomeName"].ToString(),
                        Comments = dt.Rows[T1_cnt]["Comments"].ToString(),
                        StatusColor = dt.Rows[T1_cnt]["StatusColor"].ToString(),
                        FileName=dt.Rows[T1_cnt]["FileName"].ToString(),
                        AreaID=int.Parse(dt.Rows[T1_cnt]["AreaID"].ToString()),
                        Budget=double.Parse(dt.Rows[T1_cnt]["Budget"].ToString()),
                        Classification=int.Parse(dt.Rows[T1_cnt]["Classification"].ToString()),
                        ClassificationName=dt.Rows[T1_cnt]["ClassificationName"].ToString(),
                        BookingID=int.Parse(dt.Rows[T1_cnt]["BookingID"].ToString()),
                        FeedBackMail=int.Parse(dt.Rows[T1_cnt]["FeedBackMail"].ToString()),
                        EmailAddress=dt.Rows[T1_cnt]["EmailAddress"].ToString(),
                        CFeedBackID = int.Parse(dt.Rows[T1_cnt]["CFeedBackID"].ToString()),
                        CourseReoccurs=int.Parse(dt.Rows[T1_cnt]["CourseReoccurs"].ToString()),
                        ReFrequencey = int.Parse(dt.Rows[T1_cnt]["ReFrequencey"].ToString()),
                        ReFrequenceyName = dt.Rows[T1_cnt]["ReFrequenceyName"].ToString(),
                        UntilDate =Convert.ToDateTime( dt.Rows[T1_cnt]["UntilDate"].ToString()),
                        Day = dt.Rows[T1_cnt]["Day"].ToString(),
                        ContactNumber=dt.Rows[T1_cnt]["ContactNumber"].ToString()
                    };
                    BookingsEntityCollection.Add(dep);
                }

            }
            BaseCache.Cache_Insert(key, BookingsEntityCollection);
            }

            return BaseCache.Cache_Select(key) as List<BookingsEntity>;
        }

        public static IEnumerable<UploadFileEntity> Bookings_SelectFileAll()
        {
            List<UploadFileEntity> upEntityCollection = new List<UploadFileEntity>();
            DataTable dt = Bookings.Bookings_SelectFilesAll_DataTable();
            if (dt.Rows.Count > 0)
            {
                for (int cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
                {
                    var upEntity=new UploadFileEntity
                    {
                        ID=int.Parse(dt.Rows[cnt]["ID"].ToString()),
                        BookingID = int.Parse(dt.Rows[cnt]["BookingID"].ToString()),
                        FilePath=dt.Rows[cnt]["FilePath"].ToString(),
                        FileName=dt.Rows[cnt]["FileName"].ToString()
                    };
                    upEntityCollection.Add(upEntity);
                }
               
            }
            return upEntityCollection;

        }
        //Sani
        public static int Bookings_TotalFilesUpload(int bookingId)
        {
            var total = from r in Bookings_SelectFileAll()
                        where r.BookingID == bookingId
                        group r by new { r.BookingID, r.ID } into r
                      select r.Key;
            int cnt = (from r in total
                       select r).Count();
            return cnt;
 
        }
        //Sani
        public static IEnumerable<UploadFileEntity> Bookings_SelectFiles(int id)
        {
            var files = from r in Bookings_SelectFileAll()
                        where r.BookingID == id
                  select r;
            return files;
        }
        //Sani
        public static IEnumerable<UploadFileEntity> Bookings_selectFile(int id)
        {
            var filePath =( from r in Bookings_SelectFileAll()
                           where r.ID == id
                            select new UploadFileEntity{FilePath=r.FilePath ,FileName=r.FileName}).ToList();
            return filePath;
        }
        //Sani
        public static UploadFileEntity Booking_select_Files(int id)
        {
            UploadFileEntity dep = new UploadFileEntity();
            SqlDataReader dr = Bookings_UploadFile_Reader(id);
            try
            {
                while (dr.Read())
                {
                    dep = new UploadFileEntity
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        BookingID = int.Parse(dr["BookingID"].ToString()),
                        FilePath = dr["FilePath"].ToString(),
                        FileName = dr["FileName"].ToString()
                    };
                }
            }
            catch { }
            finally { dr.Close(); }
            return dep;
        }

        public static BookingsEntity Bookings_FeedBack(int id)
        {
            BookingsEntity be = (from r in Bookings_SelectAll()
                                where r.ID != -99 && r.ID == id
                                select r).FirstOrDefault();
            return be;
        }
        public static BookingsEntity Bookings_Select(int id)
        {
            BookingsEntity dep = new BookingsEntity();
            SqlDataReader dr = Bookings_Select_Datareader(id);
            if (!dr.IsClosed)
            {
                dr.Close();
            }
            try
            {
                while (dr.Read())
                {
                    dep = new BookingsEntity
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        CategoryID = int.Parse(dr["CategoryID"].ToString()),
                        CategoryName = dr["CategoryName"].ToString(),
                        CourseID = int.Parse(dr["CourseID"].ToString()),
                        CourseTitle = dr["CourseTitle"].ToString(),
                        DepartmentID = int.Parse(dr["DepartmentID"].ToString()),
                        DepartmentName = dr["DepartmentName"].ToString(),
                        CheckedBy = int.Parse(dr["CheckedBy"].ToString()),
                        CheckedByName = dr["CheckedByName"].ToString(),
                        StatusID = int.Parse(dr["StatusID"].ToString()),
                        StatusName = dr["StatusName"].ToString(),
                        NotifyUser = dr["NotifyUser"].ToString(),
                        NotifyUserName = dr["NotifyUserName"].ToString(),
                        Employee = int.Parse(dr["Employee"].ToString()),
                        EmployeeName = dr["EmployeeName"].ToString(),
                        CourseVenue = dr["CourseVenue"].ToString(),
                        FeedBack = dr["FeedBack"].ToString(),
                        CostofCourse = double.Parse(dr["CostofCourse"].ToString()),
                        NotifyDaysPrior = int.Parse(dr["NotifyDaysPrior"].ToString()),
                        Instructor = dr["Instructor"].ToString(),
                        CheckedDate = DateTime.Parse(dr["CheckedDate"].ToString()),
                        DateofCourse = DateTime.Parse(dr["DateofCourse"].ToString()),
                        RequiredByDate = DateTime.Parse(dr["RequiredByDate"].ToString()),
                        FileID = int.Parse(dr["FileID"].ToString()),
                        BookingDate = DateTime.Parse(dr["BookingDate"].ToString()),
                        EndDate = DateTime.Parse(dr["EndDate"].ToString()),
                        StartTime = dr["StartTime"].ToString(),
                        EndTime = dr["EndTime"].ToString(),
                        DurationInDays = int.Parse(dr["DurationInDays"].ToString()),
                        Expenses = double.Parse(dr["Expenses"].ToString()),
                        Penalties = double.Parse(dr["Penalties"].ToString()),
                        Outcome = int.Parse(dr["Outcome"].ToString()),
                        OutcomeName = dr["OutcomeName"].ToString(),
                        Comments = dr["Comments"].ToString(),
                        StatusColor = dr["StatusColor"].ToString(),
                        AreaID = int.Parse(dr["AreaID"].ToString()),
                        FileName = dr["FileName"].ToString(),
                        Budget = double.Parse(dr["Budget"].ToString()),
                        Classification = int.Parse(dr["Classification"].ToString()),
                        EmailAddress = dr["EmailAddress"].ToString(),
                        BookingID=int.Parse(dr["BookingID"].ToString())

                    };
                }
            }
            catch {  }
            finally
            {
                dr.Close();
            }
            return dep;
        }


        public static BookingsEntity Bookings_GetEmployees(int id)
        {
            BookingsEntity be = (from r in Bookings_SelectAll()
                     where r.ID != -99 && r.ID == id
                     select r).FirstOrDefault();
            return be;
        }
        public static BookingsEntity Bookings_GetEmailAddress(int id) //Used in admin notification
        {
            BookingsEntity be = (from r in Bookings_SelectAll()
                                 where r.ID != -99 && r.Employee== id
                                 select r).FirstOrDefault();
            return be;
        }
        public static IEnumerable<EmployeeEntity> Bookings_SelectEmployee(int deptID)
        {
            var ce = from r in Bookings_SelectAll()
                     where r.ID != -99 && r.DepartmentID == deptID
                     group r by new { r.Employee, r.EmployeeName } into g
                     select g.Key;

            var ce1 = (from r in ce
                       select new EmployeeEntity { ID = r.Employee, Name = r.EmployeeName }).Distinct().ToList();

            return ce1;
        }
        //Modified on 08/06/2010 These are used in Gap analysis
        public static IEnumerable<EmployeeEntity> Bookings_SelectEmployee1(int deptID,int areaID)
        {
            var ce = from r in Bookings_SelectAll()
                     where r.ID != -99 && r.DepartmentID == deptID && r.AreaID == areaID
                     group r by new { r.Employee, r.EmployeeName,r.ClassificationName} into g
                     select g.Key;

            var ce1 = (from r in ce
                       orderby r.EmployeeName
                       select new EmployeeEntity { ID = r.Employee, Name = r.EmployeeName,ClassificationName=r.ClassificationName }).Distinct().ToList();

            return ce1;
        }

        public static IEnumerable<EmployeeEntity> Bookings_SelectEmployee12(int deptID, int areaid,int courseID)
        {
            var ce = from r in Bookings_SelectAll()
                     where r.ID != -99 && r.DepartmentID == deptID && r.AreaID == areaid && r.CourseID==courseID
                     group r by new
                     {
                         r.Employee,
                         r.EmployeeName,
                         r.Classification,
                         r.ClassificationName,
                         r.BookingDate
                         ,
                         r.CourseTitle,
                         r.StatusName
                     } into g
                     select g.Key;

            var ce1 = (from r in ce
                       select new EmployeeEntity
                       {
                           ID = r.Employee,
                           Name = r.EmployeeName,
                           Classification = r.Classification,
                           ClassificationName = r.ClassificationName,
                           BookingDate = r.BookingDate,
                           CourseName = r.CourseTitle
                           ,
                           StatusName = r.StatusName
                       }).Distinct().ToList();

            return ce1;
        }
        //end Modified on 8/06/2010
        //new 
        public static IEnumerable<EmployeeEntity> Bookings_SelectEmployee(int deptID,int areaid)
        {
            var ce = from r in Bookings_SelectAll()
                     where r.ID != -99 && r.DepartmentID == deptID && r.AreaID == areaid
                     group r by new { r.Employee, r.EmployeeName,r.Classification,r.ClassificationName,r.BookingDate
                     ,r.CourseTitle,r.StatusName} into g
                     select g.Key;

            var ce1 = (from r in ce
                       select new EmployeeEntity { ID = r.Employee, Name = r.EmployeeName ,Classification=r.Classification,
                           ClassificationName=r.ClassificationName,BookingDate=r.BookingDate,CourseName=r.CourseTitle
                       ,StatusName=r.StatusName}).Distinct().ToList();

            return ce1;
        }
        public static IEnumerable<EmployeeEntity> Bookings_SelectEmployee()
        {
            var ce = from r in Bookings_SelectAll()
                     where r.ID != -99 
                     group r by new { r.Employee, r.EmployeeName } into g
                     select g.Key;

            var ce1 = (from r in ce
                       orderby r.EmployeeName
                       select new EmployeeEntity { ID = r.Employee, Name = r.EmployeeName }).Distinct().ToList();

            return ce1;
        }



        public static IEnumerable<CourseEntity> Bookings_SelectCourse(int deptID)
        {
            var ce = from r in Bookings_SelectAll()
                     where r.ID != -99 && r.DepartmentID == deptID
                     group r by new { r.CourseID, r.CourseTitle } into g
                     select g.Key;

            var ce1 = (from r in ce
                       orderby r.CourseTitle
                       select new CourseEntity { ID = r.CourseID, Title = r.CourseTitle }).Distinct().ToList();
            return ce1;
        }
        public static IEnumerable<CourseEntity> Bookings_SelectCourse(int deptID,int AreaID)
        {
            var ce = from r in Bookings_SelectAll()
                     where r.ID != -99 && r.DepartmentID == deptID && r.AreaID == AreaID
                     group r by new { r.CourseID, r.CourseTitle} into g
                     
                     select g.Key;

            var ce1 = (from r in ce
                       orderby r.CourseTitle 
                       select new CourseEntity { ID = r.CourseID, Title = r.CourseTitle }).Distinct().ToList();
            return ce1;
        }
        /*Outcome operations[Department/Target Progress summary]BEGIN*/
        //Sani--Begin from here
        public static IEnumerable<OutcomeEntity> Booking_SelectOutcomes()
        {
            var oe = from r in Bookings_SelectAll()
                     where r.ID != -99
                     group r by new { r.Outcome, r.OutcomeName } into g
                     select g.Key;
            var oe1 = (from r in oe select new OutcomeEntity { ID = r.Outcome, Name = r.OutcomeName }).ToList();
            return oe1;
        }
        public static int Booking_TotalTarget(int outCome)
        {
            var target = from r in Bookings_SelectAll()
                         where r.ID != -99 && r.Outcome == outCome && r.Outcome != 0
                         group r by new { r.Outcome } into r
                         select r.Key;
            int cnt = (from r in target select r).Count();
            return cnt;
        }

        public static int Booking_GetTotalEmployees()
        {
            var target = from r in Bookings_SelectAll()
                         where r.ID != -99
                         select r;
            int cnt = (from r in target select r).Count();
            return cnt;
        }
        
        public static int Booking_TotalTargetByIDs(int areaID, int deptID, DateTime fromDate, DateTime toDate, int outcomeId)
        {
            
            int cnt = 0;
            if (areaID == 0 && deptID == 0 && fromDate == null && toDate == null)
            {
                var t = from r in Bookings_SelectAll()
                        where r.ID != -99 && r.Outcome == outcomeId && r.Outcome != 0 && r.DepartmentID == deptID &&
                        r.EndDate >= fromDate && r.EndDate <= toDate && r.AreaID == areaID
                        group r by new { r.Outcome, r.Employee } into r
                        select r.Key;
                cnt = (from r in t select r).Count();
            }
            else if (areaID == 0)
            {
                var t = from r in Bookings_SelectAll()
                        where r.ID != -99 && r.Outcome == outcomeId && r.Outcome != 0 && r.DepartmentID == deptID &&
                        r.EndDate >= fromDate && r.EndDate <= toDate
                        group r by new { r.Outcome, r.Employee } into r
                        select r.Key;
                cnt = (from r in t select r).Count();
            }
            else if (deptID == 0)
            {
                var t = from r in Bookings_SelectAll()
                        where r.ID != -99 && r.Outcome == outcomeId && r.Outcome != 0 && r.AreaID == areaID &&
                        r.EndDate >= fromDate && r.EndDate <= toDate
                        group r by new { r.Outcome, r.Employee } into r
                        select r.Key;
                cnt = (from r in t select r).Count();
            }
            return cnt;
        }

        /*Outcome operations[Department/Target Progress summary]END*/


        public static string Booking_GetStatusColor(int CourseID, int EmployeeID, int deptId)
        {
            string statuscolor = string.Empty;
            BookingsEntity be = (from r in Bookings_SelectAll()
                                 where r.CourseID == CourseID && r.Employee == EmployeeID && r.DepartmentID == deptId
                                 select r).FirstOrDefault();
            if (be != null)
                statuscolor = be.StatusColor;

            return statuscolor;
        }
        //new1
        public static string Booking_GetStatusColor(int CourseID, int EmployeeID, int deptId,int AreaID)
        {
            string statuscolor = string.Empty;
            try
            {
                DateTime bookingDate = Convert.ToDateTime("01/01/1900");
               
                bookingDate = (from r in Bookings_SelectAll()
                               where r.CourseID == CourseID && r.Employee == EmployeeID && r.DepartmentID == deptId && r.AreaID == AreaID
                               select r.BookingDate).Max();
                BookingsEntity be = (from r in Bookings_SelectAll()
                                     where r.CourseID == CourseID && r.Employee == EmployeeID && r.DepartmentID == deptId && r.AreaID == AreaID
                                     && r.BookingDate == bookingDate
                                     select r).FirstOrDefault();
                if (be != null)
                    statuscolor = be.StatusColor;
                
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return statuscolor;
        }
        public static string Booking_GetStatusName(int CourseID, int EmployeeID, int deptId, int AreaID)
        {
            DateTime bookingDate = Convert.ToDateTime("01/01/1900");
            string statusName = string.Empty;
            try
            {
             
            bookingDate = (from r in Bookings_SelectAll()
                           where r.CourseID == CourseID && r.Employee == EmployeeID && 
                           r.DepartmentID == deptId && r.AreaID == AreaID
                           select r.BookingDate).Max();
           
            BookingsEntity be = (from r in Bookings_SelectAll()
                                 where r.CourseID == CourseID && r.Employee == EmployeeID && r.DepartmentID == deptId 
                                 && r.AreaID == AreaID && r.BookingDate == bookingDate
                                 select r).FirstOrDefault();
            if (be != null)
                statusName = be.StatusName;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return statusName;
        }
        public static DateTime Booking_GetBookingDate(int CourseID, int EmployeeID, int deptId, int AreaID)
        {
            DateTime bookingDate = Convert.ToDateTime("01/01/1900");
            DateTime bookingDate1 = Convert.ToDateTime("01/01/1900");

             try
            {
             
            bookingDate1 = (from r in Bookings_SelectAll()
                           where r.CourseID == CourseID && r.Employee == EmployeeID && r.DepartmentID == deptId && r.AreaID == AreaID
                           select r.BookingDate).Max();
          
            BookingsEntity be = (from r in Bookings_SelectAll()
                                 where r.CourseID == CourseID && r.Employee == EmployeeID &&
                                 r.DepartmentID == deptId && r.AreaID == AreaID && r.BookingDate == bookingDate1
                                 select r).FirstOrDefault();
            if (be != null)
                bookingDate = be.BookingDate;
            }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
             }
            return bookingDate;
        }

        public static string Booking_GetEmployeeName(int CourseID, int EmployeeID, int deptId, int AreaID)
        {
            string name = string.Empty;
            BookingsEntity be = (from r in Bookings_SelectAll()
                                 where r.CourseID == CourseID && r.Employee == EmployeeID && r.DepartmentID == deptId && r.AreaID == AreaID
                                 select r).FirstOrDefault();
            if (be != null)
                name = be.EmployeeName;

            return name;
        }

        public static int Booking_GetTotalStatus(int employeeId, int statusId, int deptId)
        {
           
            int ts2 = (from r in Bookings_SelectAll()
                       where r.ID != -99 && r.Employee == employeeId && r.StatusID == statusId && r.DepartmentID == deptId
                       group r by new { r.ID,r.StatusID, r.Employee, r.DepartmentID } into r
                       select r.Key).Count(r=>r.ID >0);

          
            return ts2;
        }
        public static int Booking_GetTotalStatus(int employeeId, int statusId, int deptId,int areaID)
        {
           
            int ts2 = (from r in Bookings_SelectAll()
                       where r.ID != -99 && r.Employee == employeeId && r.StatusID == statusId && r.DepartmentID == deptId && r.AreaID== areaID
                       group r by new { r.ID, r.StatusID, r.Employee, r.DepartmentID } into r
                       select r.Key).Count(r => r.ID > 0);

            return ts2;
        }


        public static IEnumerable<StatusEntity> Booking_GetCount()
        {
            var ts = from r in Bookings_SelectAll()
                     where r.ID != -99
                     group r by new { r.StatusID, r.StatusColor, r.StatusName } into r
                     select r.Key;

            var cnt = (from r in ts
                       select new StatusEntity { StatusID = r.StatusID, StatusColor = r.StatusColor, StatusName = r.StatusName });
            return cnt;
        }
        

        public static int Booking_GetStatusOfCourse(int courseId, int statusId, int deptId)
        {
            int count = (from r in Bookings_SelectAll()
                     where r.ID != -99 && r.CourseID == courseId && r.StatusID == statusId && r.DepartmentID == deptId
                     group r by new { r.ID, r.CourseID, r.DepartmentID } into r
                     select r.Key).Count(n => n.ID>0);
          
            return count;
        }
        //new
        public static int Booking_GetStatusOfCourse(int courseId, int statusId, int deptId,int areaID)
        {
            int count = (from r in Bookings_SelectAll()
                         where r.ID != -99 && r.CourseID == courseId && r.StatusID == statusId && r.DepartmentID == deptId && r.AreaID == areaID
                         group r by new { r.ID, r.CourseID, r.DepartmentID } into r
                         select r.Key).Count(n => n.ID > 0);
            
            return count;
        }

        public static int Booking_GetCourseTotalResult(int statusId, int deptId)
        {
            int count = (from r in Bookings_SelectAll()
                     where r.ID != -99 && r.StatusID == statusId && r.DepartmentID == deptId
                     group r by new { r.ID,r.StatusID, r.DepartmentID } into r
                      select r.Key).Count(n => n.ID > 0); 
           
            return count;
        }
        public static int Booking_GetCourseTotalResult(int statusId, int deptId,int areaID)
        {
            int count = (from r in Bookings_SelectAll()
                         where r.ID != -99 && r.StatusID == statusId && r.DepartmentID == deptId && r.AreaID == areaID
                         group r by new { r.ID, r.StatusID, r.DepartmentID } into r
                         select r.Key).Count(n => n.ID > 0);
         
            return count;
        }

        public static IEnumerable<BookingsEntity> Bookings_GetAllEmployees()
        {
            
            var allEmployees = from r in Bookings_SelectAll()
                               where r.ID != -99
                               select r;
            return allEmployees;
        }
        public static IEnumerable<BookingsEntity> Bookings_GetPendingCourses1(int userID, DateTime fromDate, DateTime toDate)
        {
            IEnumerable<BookingsEntity> employeePending = from r in Bookings_SelectAll()
                                                          where r.ID != -99 && r.EndDate >= fromDate && r.EndDate <= toDate && r.Employee == userID
                                                          && (r.StatusID == 1 || r.StatusID == 4)
                                                          select r;
            return employeePending;
        }
        //
        public static List<BookingsEntity> Bookings_GetPendingCourses(int userID, DateTime fromDate, DateTime toDate)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            List<BookingsEntity> employeePending = null;
            if (userID == 0 && fromDate == defaultDate && toDate == defaultDate)
            {
                employeePending = (from r in Bookings_SelectAll()
                                  where r.ID != -99
                                  && (r.StatusID == 1 || r.StatusID == 4)
                                  select r).ToList();
            }
            else if (fromDate == defaultDate && toDate == defaultDate && userID != 0)
            {
                employeePending =( from r in Bookings_SelectAll()
                                  where r.ID != -99 && r.Employee == userID
                                  && (r.StatusID == 1 || r.StatusID == 4)
                                  select r).ToList();
            }
            else if (userID == 0 && fromDate != defaultDate && toDate != defaultDate)
            {
                employeePending = (from r in Bookings_SelectAll()
                                  where r.ID != -99 && r.EndDate >= fromDate && r.EndDate <= toDate
                                  && (r.StatusID == 1 || r.StatusID == 4)
                                  select r).ToList();
            }
            else
            {
                employeePending = (from r in Bookings_SelectAll()
                                  where r.ID != -99 && r.EndDate >= fromDate && r.EndDate <= toDate && r.Employee == userID
                                  && (r.StatusID == 1 || r.StatusID == 4)
                                  select r).ToList();
            }

            return employeePending;
        }

        //
        public static List<BookingsEntity> Bookings_GetPendingCourses()
        {
            List<BookingsEntity> employeePending =(from r in Bookings_SelectAll()
                                                          where r.ID != -99

                                                          && (r.StatusID == 1 || r.StatusID == 4)
                                                          select r).ToList();
            return employeePending;
        }
        public static List<BookingsEntity> Bookings_GetCompletedCourses(int userID, DateTime fromDate, DateTime toDate)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            List<BookingsEntity> employeeCompletedCourses = null;
            if (userID == 0 && fromDate == defaultDate && toDate == defaultDate)
            {
                employeeCompletedCourses =( from r in Bookings_SelectAll()
                                           where r.ID != -99
                                           && (r.StatusID == 2 || r.StatusID == 3)
                                           select r).ToList();

            }
            else if (fromDate == defaultDate && toDate == defaultDate && userID != 0)
            {
                employeeCompletedCourses = (from r in Bookings_SelectAll()
                                           where r.ID != -99 && r.Employee == userID
                                           && (r.StatusID == 2 || r.StatusID == 3)
                                           select r).ToList();

            }
            else if (userID == 0 && fromDate != defaultDate && toDate != defaultDate)
            {
                employeeCompletedCourses =( from r in Bookings_SelectAll()
                                           where r.ID != -99 && r.EndDate >= fromDate && r.EndDate <= toDate
                                           && (r.StatusID == 2 || r.StatusID == 3)
                                           select r).ToList();

            }
            else
            {
                employeeCompletedCourses = (from r in Bookings_SelectAll()
                                           where r.ID != -99 && r.EndDate >= fromDate && r.EndDate <= toDate && r.Employee == userID
                                           && (r.StatusID == 2 || r.StatusID == 3)
                                           select r).ToList();
            }

            return employeeCompletedCourses;
        }
        public static List<BookingsEntity> Bookings_GetCompletedCourses()
        {
            List<BookingsEntity> employeeCompletedCourses = (from r in Bookings_SelectAll()
                                                                   where r.ID != -99
                                                                   && (r.StatusID == 2 || r.StatusID == 3)
                                                                   select r).ToList();
            return employeeCompletedCourses;
        }
        public static List<BookingsEntity> Bookings_GetPendingCoursesByType(int userID, DateTime fromDate, DateTime toDate, int typeID)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            List<BookingsEntity> employeePending = null;
            if (userID == 0 && fromDate == defaultDate && toDate == defaultDate)
            {
                employeePending =(from r in Bookings_SelectAll()
                                  where r.ID != -99
                                  && r.CategoryID == typeID
                                  && (r.StatusID == 1 || r.StatusID == 4)
                                  select r).ToList();

            }
            else if (fromDate == defaultDate && toDate == defaultDate && userID != 0)
            {
                employeePending = (from r in Bookings_SelectAll()
                                  where r.ID != -99 && r.Employee == userID
                                  && r.CategoryID == typeID
                                  && (r.StatusID == 1 || r.StatusID == 4)
                                  select r).ToList();

            }
            else if (userID == 0 && fromDate != defaultDate && toDate != defaultDate)
            {
                employeePending = (from r in Bookings_SelectAll()
                                  where r.ID != -99 && r.EndDate >= fromDate && r.EndDate <= toDate
                                  && r.CategoryID == typeID
                                  && (r.StatusID == 1 || r.StatusID == 4)
                                  select r).ToList();

            }
            else
            {
                employeePending = (from r in Bookings_SelectAll()
                                  where r.ID != -99 && r.EndDate >= fromDate && r.EndDate <= toDate && r.Employee == userID
                                  && r.CategoryID == typeID
                                  && (r.StatusID == 1 || r.StatusID == 4)
                                  select r).ToList();
            }

            return employeePending;
        }

        public static List<BookingsEntity> Bookings_GetCompletedCoursesByType(int userID, DateTime fromDate, DateTime toDate, int typeID)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            List<BookingsEntity> employeeCompletedCourses = null;
            if (userID == 0 && fromDate == defaultDate && toDate == defaultDate)
            {
                employeeCompletedCourses = (from r in Bookings_SelectAll()
                                           where r.ID != -99
                                           && r.CategoryID == typeID
                                           && (r.StatusID == 2 || r.StatusID == 3)
                                           select r).ToList();

            }
            else if (fromDate == defaultDate && toDate == defaultDate && userID != 0)
            {
                employeeCompletedCourses = (from r in Bookings_SelectAll()
                                           where r.ID != -99 && r.Employee == userID
                                           && r.CategoryID == typeID
                                           && (r.StatusID == 2 || r.StatusID == 3)
                                           select r).ToList();

            }
            else if (userID == 0 && fromDate != defaultDate && toDate != defaultDate)
            {
                employeeCompletedCourses =(from r in Bookings_SelectAll()
                                           where r.ID != -99 && r.EndDate >= fromDate && r.EndDate <= toDate
                                           && r.CategoryID == typeID
                                           && (r.StatusID == 2 || r.StatusID == 3)
                                           select r).ToList();

            }
            else
            {
                employeeCompletedCourses = (from r in Bookings_SelectAll()
                                           where r.ID != -99 && r.EndDate >= fromDate && r.EndDate <= toDate && r.Employee == userID
                                           && r.CategoryID == typeID
                                           && (r.StatusID == 2 || r.StatusID == 3)
                                           select r).ToList();
            }

            return employeeCompletedCourses;
        }

        //Code for "Booking record filter"
        public static List<BookingsEntity> Test(int DepartmentID, int employeeId, int courseId
            , int areaId)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            List<BookingsEntity> BookingsCollection = null;
            string strQuery = "";
            if (DepartmentID != 0)
            {
                strQuery += "r.DepartmentID=="+DepartmentID+" && r.ID!=-99";
            }
            //BookingsCollection = from r in Bookings_SelectAll()
            //                         .Where(strQuery)
            //                     select r;
           
            return BookingsCollection;
        }

        public static List<BookingsEntity> Bookings_SelectByFilter(int DepartmentID, int employeeId, int courseId
            ,int areaId)
        {

            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            List<BookingsEntity> BookingsCollection = null;
            if (employeeId == 0 && courseId == 0 && areaId==0) //1
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where r.DepartmentID == DepartmentID || r.ID == -99
                                      select r).ToList();
            }
            else if (employeeId != 0 && courseId == 0 && areaId==0)//2
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID && r.Employee == employeeId) 
                                      || r.ID == -99
                                      
                                      select r).ToList();
            }
            else if (employeeId != 0 && courseId != 0 && areaId==0)//3
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID && r.CourseID == courseId &&
                                      r.Employee == employeeId)
                                      || r.ID == -99
                                      
                                      select r).ToList();
            }
            else if (employeeId != 0 && courseId == 0 && areaId!=0)//4
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID && r.Employee == employeeId
                                        && r.AreaID == areaId) || r.ID == -99
                                      
                                      select r).ToList();
            }
            else if (employeeId == 0 && courseId != 0 && areaId==0)//5
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID && r.CourseID == courseId)
                                      || r.ID == -99
                                     
                                      select r).ToList();
            }
            else if (employeeId == 0 && courseId != 0 && areaId!=0)//6
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID && r.AreaID == areaId 
                                      && r.CourseID == courseId )|| r.ID == -99          
                                      
                                      select r).ToList();
            }
            else if (employeeId == 0 && courseId == 0 && areaId!=0)//7
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID && r.AreaID == areaId)
                                      || r.ID == -99
                                      
                                      select r).ToList();
                //BookingsCollection = (from r in Bookings_SelectAll()
                //                      where (r.DepartmentID == DepartmentID && r.ID == -99)
                //                      && r.AreaID==areaId
                //                      select r).ToList();
            }

            else //8
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID 
                                      && r.AreaID==areaId
                                       && r.CourseID == courseId && r.Employee == employeeId) || r.ID == -99
                                      select r).ToList();
            }
            return BookingsCollection;
        }

        public static int  Booking_ClassifictionToBooking(int employee,int courseID,int categoryID,int DepatID)
        {
            int cnt;
             var t = from r in Bookings_SelectAll()
                        where r.ID != -99 && r.DepartmentID == DepatID &&
                        r.Employee==employee && r.CourseID==courseID && r.CategoryID==categoryID
                        select r;
                cnt = (from r in t select r).Count();
            return cnt;
        }

        public static List<BookingsEntity> Bookings_SelectByFilters(int DepartmentID,int employeeId,int courseId
            ,DateTime fromDate,DateTime toDate)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            List<BookingsEntity> BookingsCollection = null;
            if (employeeId == 0 && courseId == 0 && fromDate == defaultDate && toDate == defaultDate) //1
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID || r.ID == -99)
                                      select r).ToList();
            }
            else if(employeeId != 0 && courseId == 0 && fromDate == defaultDate && toDate == defaultDate)//2
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID || r.ID == -99)
                                      && r.Employee==employeeId
                                      select r).ToList();
            }
            else if (employeeId != 0 && courseId != 0 && fromDate == defaultDate && toDate == defaultDate)//3
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID || r.ID == -99)
                                      && r.CourseID == courseId && r.Employee==employeeId
                                      select r).ToList();
            }
            else if (employeeId != 0 && courseId == 0 && fromDate != defaultDate && toDate != defaultDate)//4
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID || r.ID == -99)
                                       && r.Employee == employeeId
                                        && r.EndDate >= fromDate && r.EndDate <= toDate 
                                      select r).ToList();
            }
            else if (employeeId == 0 && courseId != 0 && fromDate == defaultDate && toDate == defaultDate)//5
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID || r.ID == -99)
                                      && r.CourseID == courseId
                                      select r).ToList();
            }
            else if (employeeId == 0 && courseId != 0 && fromDate != defaultDate && toDate != defaultDate)//6
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID || r.ID == -99)
                                      && r.EndDate >= fromDate && r.EndDate <= toDate && r.CourseID == courseId
                                      select r).ToList();
            }
            else if (employeeId == 0 && courseId == 0 && fromDate != defaultDate && toDate != defaultDate)//7
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID || r.ID == -99)
                                      && r.EndDate >= fromDate && r.EndDate <= toDate 
                                      select r).ToList();
            }
            
            else //8
            {
                BookingsCollection = (from r in Bookings_SelectAll()
                                      where (r.DepartmentID == DepartmentID || r.ID == -99)
                                      && r.EndDate >= fromDate && r.EndDate <= toDate
                                       && r.CourseID == courseId && r.Employee == employeeId
                                      select r).ToList();
            }
            return BookingsCollection;
        }

        public static DataTable Booking_CostForecast(int DeptID,int AreaID,DateTime fromDate,DateTime toDate)
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Bookings_ActualCost",
                new SqlParameter("@DepartmentID", DeptID), new SqlParameter("@AreaID", AreaID), new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)).Tables[0];
            
            return dt;
        }

        public static DataTable Booking_Penalties(int DeptID, int AreaID, DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Bookings_getPenalities",
                new SqlParameter("@DepartmentID", DeptID), new SqlParameter("@AreaID", AreaID), new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)).Tables[0];

            return dt;
        }

        public static DataTable Booking_Budget_Actual(int DeptID, int AreaID, DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Bookings_BudgetVsActualCost",
                new SqlParameter("@DepartmentID", DeptID), new SqlParameter("@AreaID", AreaID), new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)).Tables[0];

            return dt;
        }
        //Following methods for Department summery.
        public static double Booking_CurrentProgressSum(int DeptID, int AreaID, DateTime fromDate, DateTime toDate)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            double currentProgress = 0.0;
            double totalRecords = 0.0;
            double selectedRecords=0.0;
            totalRecords = (from r in Bookings_SelectAll()
                            where r.ID != -99
                            select r).Count();
            if (DeptID != 0 && AreaID != 0 && fromDate == defaultDate && toDate == defaultDate) //1
            {
                

                BookingsEntity bookings1 = (from r in Bookings_SelectAll()
                                           where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.StatusID == 1
                                           select r).FirstOrDefault();
                if (bookings1 != null)
                {
                    selectedRecords = (from r in Bookings_SelectAll()
                                       where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.StatusID == 1
                                       select r).Count();
                    currentProgress = (selectedRecords/totalRecords) * 100;
                }
                else
                {
                    currentProgress = 0;
                }
            }
            else
            {
                BookingsEntity bookings2 = (from r in Bookings_SelectAll()
                                            where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.StatusID == 1
                                  && r.EndDate >= fromDate && r.EndDate <= toDate
                                            select r).FirstOrDefault();
                if (bookings2 != null)
                {
                  
                    selectedRecords = (from r in Bookings_SelectAll()
                                       where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.StatusID == 1
                                       && r.EndDate >= fromDate && r.EndDate <= toDate
                                       select r).Count();

                    currentProgress = (selectedRecords / totalRecords) * 100;

                    
                }
                else
                {
                    currentProgress = 0;
                }
            }
            return currentProgress;
        }

        public static double Booking_PassedPercentage(int DeptID, int AreaID, DateTime fromDate, DateTime toDate)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            double passed = 0.0;
            double totalRecords = 0.0;
            double selectedRecords = 0.0;
            totalRecords = (from r in Bookings_SelectAll()
                            where r.ID != -99
                            select r).Count();
            if (DeptID != 0 && AreaID != 0 && fromDate == defaultDate && toDate == defaultDate) //1
            {


                BookingsEntity bookings1 = (from r in Bookings_SelectAll()
                                            where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.Outcome == 1
                                            select r).FirstOrDefault();
                if (bookings1 != null)
                {
                    selectedRecords = (from r in Bookings_SelectAll()
                                       where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.Outcome == 1
                                       select r).Count();
                    passed = (selectedRecords / totalRecords) * 100;
                }
                else
                {
                    passed = 0;
                }
            }
            else
            {
                BookingsEntity bookings2 = (from r in Bookings_SelectAll()
                                            where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.Outcome == 1
                                  && r.EndDate >= fromDate && r.EndDate <= toDate
                                            select r).FirstOrDefault();
                if (bookings2 != null)
                {

                    selectedRecords = (from r in Bookings_SelectAll()
                                       where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.Outcome == 1
                                       && r.EndDate >= fromDate && r.EndDate <= toDate
                                       select r).Count();

                    passed = (selectedRecords / totalRecords) * 100;


                }
                else
                {
                    passed = 0;
                }
            }
            return passed;
        }

        public static double Booking_PendingPercentage(int DeptID, int AreaID, DateTime fromDate, DateTime toDate)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            double passed = 0.0;
            double totalRecords = 0.0;
            double selectedRecords = 0.0;
            totalRecords = (from r in Bookings_SelectAll()
                            where r.ID != -99
                            select r).Count();
            if (DeptID != 0 && AreaID != 0 && fromDate == defaultDate && toDate == defaultDate) //1
            {


                BookingsEntity bookings1 = (from r in Bookings_SelectAll()
                                            where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.StatusID == 4
                                            select r).FirstOrDefault();
                if (bookings1 != null)
                {
                    selectedRecords = (from r in Bookings_SelectAll()
                                       where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.StatusID == 4
                                       select r).Count();
                    passed = (selectedRecords / totalRecords) * 100;
                }
                else
                {
                    passed = 0;
                }
            }
            else
            {
                BookingsEntity bookings2 = (from r in Bookings_SelectAll()
                                            where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.StatusID == 4
                                  && r.EndDate >= fromDate && r.EndDate <= toDate
                                            select r).FirstOrDefault();
                if (bookings2 != null)
                {

                    selectedRecords = (from r in Bookings_SelectAll()
                                       where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.StatusID == 4
                                       && r.EndDate >= fromDate && r.EndDate <= toDate
                                       select r).Count();

                    passed = (selectedRecords / totalRecords) * 100;


                }
                else
                {
                    passed = 0;
                }
            }
            return passed;
        }
        public static double Booking_FailedPercentage(int DeptID, int AreaID, DateTime fromDate, DateTime toDate)
        {
            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            double failed = 0.0;
            double totalRecords = 0.0;
            double selectedRecords = 0.0;
            totalRecords = (from r in Bookings_SelectAll()
                            where r.ID != -99
                            select r).Count();
            if (DeptID != 0 && AreaID != 0 && fromDate == defaultDate && toDate == defaultDate) //1
            {


                BookingsEntity bookings1 = (from r in Bookings_SelectAll()
                                            where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.Outcome == 2
                                            select r).FirstOrDefault();
                if (bookings1 != null)
                {
                    selectedRecords = (from r in Bookings_SelectAll()
                                       where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.Outcome == 2
                                       select r).Count();
                    failed = (selectedRecords / totalRecords) * 100;
                }
                else
                {
                    failed = 0;
                }
            }
            else
            {
                BookingsEntity bookings2 = (from r in Bookings_SelectAll()
                                            where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.Outcome == 2
                                  && r.EndDate >= fromDate && r.EndDate <= toDate
                                            select r).FirstOrDefault();
                if (bookings2 != null)
                {

                    selectedRecords = (from r in Bookings_SelectAll()
                                       where r.DepartmentID == DeptID && r.ID != -99 && r.AreaID == AreaID && r.Outcome == 2
                                       && r.EndDate >= fromDate && r.EndDate <= toDate
                                       select r).Count();

                    failed = (selectedRecords / totalRecords) * 100;


                }
                else
                {
                    failed = 0;
                }
            }
            return failed;
        }
        //Feed back
        public static bool IsFeedBackExist(int ID)
        {
            bool btnImg=false;
            int i = 0;
          //  SqlParameter feedBack = new SqlParameter("@result", SqlDbType.Int);
          //  feedBack.Direction = ParameterDirection.Output;
          //string i=SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure,"Training.Bookings_FeedBackExist", new SqlParameter("@ID", ID),feedBack).ToString();
           // int exist = Convert.ToInt32(feedBack.Value);
            BookingsEntity be =(from r in Bookings_SelectAll()
                                where r.ID != -99 && r.ID == ID
                                select r).FirstOrDefault();

            var bookings = from r in Bookings_SelectAll()
                           where r.ID != -99 && r.ID == ID
                           select r;
            

            i = (from r in bookings select r).Count();

            if (i!=0 && (be.BookingID!=0 || be.CFeedBackID!=0))
            {
                Bookings_UpdateFeedBackMail(ID, 0);
                btnImg = true;
            }
            return btnImg;
        }
        public static IEnumerable<EmployeeEntity> Bookings_MailSent(int ID)
        {
          
           
           var bookings =(from r in Bookings_SelectAll()
                           where r.ID != -99 && r.ID == ID
                           select new EmployeeEntity { FeedBackMail = r.FeedBackMail }).ToList();
         

          
           return bookings;
        }

        //Sani--Ends here

    }
    #endregion

#region Training Type

    public class TrainingType
    {
        public static void TrainingType_InsertUpdate(TrainingTypeEntity dep)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.trType_InsertUpdate", new SqlParameter("@ID", dep.ID), new SqlParameter("@Name", dep.Name));
        }
        public static DataTable TrainingType_SelectAll_Datatable()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.trType_SelectAll").Tables[0];
            return dt;
        }
        public static SqlDataReader TrainingType_Select_Datareader(int id)
        {
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Training.trType_Select", new SqlParameter("@ID", id));
            return dr;
        }
        public static IEnumerable<TrainingTypeEntity> TrainingType_SelectAll()
        {
            List<TrainingTypeEntity> CategoryCollection = new List<TrainingTypeEntity>();
            DataTable dt = TrainingType_SelectAll_Datatable();
            int R_cont = dt.Rows.Count;
            if (R_cont > 0)
            {
                for (int T1_cnt = 0; T1_cnt <= dt.Rows.Count - 1; T1_cnt++)
                {
                    var dep = new TrainingTypeEntity
                    {
                        ID = int.Parse(dt.Rows[T1_cnt]["ID"].ToString()),
                        Name = dt.Rows[T1_cnt]["Name"].ToString()
                    };
                    CategoryCollection.Add(dep);
                }
            }
            return CategoryCollection;
        }
        public static TrainingTypeEntity TrainingType_Select(int id)
        {
            TrainingTypeEntity dep = new TrainingTypeEntity();
            SqlDataReader dr = TrainingType_Select_Datareader(id);
            try
            {
                while (dr.Read())
                {
                    dep = new TrainingTypeEntity
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Name = dr["Name"].ToString()
                    };
                }
            }
            catch { }
            finally { dr.Close(); }
            return dep;
        }

    }
    #endregion

#region Area

    public class Area
    {
        public static void Area_InsertUpdate(AreaEntity dep)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Area_InsertUpdate",
                new SqlParameter("@ID", dep.ID), new SqlParameter("@Name", dep.Name), new SqlParameter("@DepartmentID", dep.DepartmentID));
        }
        //public static void Area_InsertUpdate(AreaEntity dep)
        //{
        //    SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Area_InsertUpdate", 
        //        new SqlParameter("@ID", dep.ID),new SqlParameter("@Name",dep.Name));
        //}
        public static void Area_Delete(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Area_Delete", new SqlParameter("@ID", ID));
        }
        public static DataTable Area_SelectAll_Datatable()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Area_SelectAll").Tables[0];
            return dt;
        }
        public static SqlDataReader Area_Select_Datareader(int id)
        {
            SqlDataReader dr = null;
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Training.Area_Select", new SqlParameter("@ID", id));
            return dr;
        }
        public static IEnumerable<AreaEntity> Area_SelectAll()
        {
            List<AreaEntity> CategoryCollection = new List<AreaEntity>();
            DataTable dt = Area_SelectAll_Datatable();
            int R_cont = dt.Rows.Count;
            if (R_cont > 0)
            {
                for (int T1_cnt = 0; T1_cnt <= dt.Rows.Count - 1; T1_cnt++)
                {
                    var dep = new AreaEntity
                    {
                        ID = int.Parse(dt.Rows[T1_cnt]["ID"].ToString()),
                        Name = dt.Rows[T1_cnt]["Name"].ToString(),
                        DepartmentID = int.Parse(dt.Rows[T1_cnt]["DepartmentID"].ToString())
                       
                    };
                    CategoryCollection.Add(dep);
                }
            }
            return CategoryCollection;
        }
        public static IEnumerable<AreaEntity> Area_OrderByAsc(int DeptId)
        {
            var a = from r in Area_SelectAll()
                    where r.DepartmentID == DeptId
                    orderby r.Name 
                    select r;
            return a;
        }

        public static AreaEntity Area_Select(int id)
        {
            AreaEntity dep = new AreaEntity();
            SqlDataReader dr = Area_Select_Datareader(id);
            try
            {

                while (dr.Read())
                {
                    dep = new AreaEntity
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Name = dr["Name"].ToString(),

                    };
                }
            }
            catch { }
            finally { dr.Close(); }
            return dep;
        }

        public static IEnumerable<AreaEntity> Area_SelectByDepartment(int DepartmentID)
        {
            IEnumerable<AreaEntity> AreaCollection = from r in Area_SelectAll()
                                                         where r.DepartmentID == DepartmentID
                                                         orderby r.Name ascending
                                                         select r;
            return AreaCollection;
        }


    }

    #endregion

#region Status

    public class Status
    {
        public static  List<ListItem> SelectAll(bool addSelect)
        {
            List<ListItem> ll = new List<ListItem>();
            if(addSelect)
            ll.Add(new ListItem("Please select...", "0"));
            ll.Add(new ListItem("Accredited", "2"));
            ll.Add(new ListItem("In Progress", "1"));
            ll.Add(new ListItem("Pending Not Taken", "4"));
            ll.Add(new ListItem("Support", "3"));
            ll.Add(new ListItem("Pending", "5"));
            ll.Add(new ListItem("Complete", "6"));
            ll.Add(new ListItem("Cancelled", "7"));
            return ll;
        }

        public static IEnumerable<StatusEntity> GetStatus()
        {
            List<StatusEntity> st = new List<StatusEntity>();
            st.Add(new StatusEntity(3, "Support Staff / N/A", "Blue", 0, 0));
            st.Add(new StatusEntity(2, "Training Complete", "Green", 0, 0));
            st.Add(new StatusEntity(1, "Training Booked ", "Yellow", 0, 0));
            st.Add(new StatusEntity(4, "Not Booked", "Red", 0, 0));
            return st;
        }
    }
    #endregion

#region Outcome

    public class Outcome
    {
        public static List<ListItem> SelectAll(bool addSelect)
        {
            List<ListItem> ll = new List<ListItem>();
            if (addSelect)
            ll.Add(new ListItem("Please select...", "0"));            
            ll.Add(new ListItem("Failed", "2"));
            ll.Add(new ListItem("Passed", "1"));
            ll.Add(new ListItem("Retake Required", "3"));
            return ll;
        }
    }
    #endregion

#region ReoccurrenceFrequencey
    public class ReoccurrenceFrequencey
    {
        public static List<ListItem> SelectAll(bool addSelect)
        {
            List<ListItem> ll = new List<ListItem>();
            if (addSelect)
                ll.Add(new ListItem("Please select...", "0"));
            ll.Add(new ListItem("Weekly", "1"));
            ll.Add(new ListItem("Monthly", "2"));
            ll.Add(new ListItem("Yearly", "3"));
            return ll;
        }
    }
    #endregion

#region Vendor

    public class TrainingVendor
    {
        public static DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select VendorID as ID,ContractorName as Name from v_Vendors order by ContractorName ASC").Tables[0];
            return dt;
        }
    }

    #endregion

#region Views
    
    public class Views
    {
        public static List<ListItem> SelectAll(bool addSelect)
        {
            List<ListItem> li = new List<ListItem>();
            if (addSelect)
                li.Add(new ListItem("Please select", "0"));
                li.Add(new ListItem("Budget Vs Actual", "8"));
               // li.Add(new ListItem("Course Reoccurrence", "3"));
                li.Add(new ListItem("Department/Area Target", "5"));
                li.Add(new ListItem("Penalties by Department", "7"));
                li.Add(new ListItem("Report by Individual", "4"));
                li.Add(new ListItem("Training Due Soon", "1"));
                li.Add(new ListItem("Training Cost Forecast", "6"));
                li.Add(new ListItem("Training Gap Analysis", "2"));
           
                   
                    
           


            return li;

        }
    }
    #endregion

#region Contractors
    public class Contractors
    {
        public static DataTable Contractors_SelectAll_DataTable()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Contractors_SelectAll").Tables[0];
            return dt;
        }

        public static SqlDataReader Contractors_ByClassificationsReader(string Classification)
        {
            SqlDataReader dr = null;
           
            dr=SqlHelper.ExecuteReader(Constants.DBString,CommandType.StoredProcedure,"Training.Contractors_ClassificatonSelect",new SqlParameter("@Classification",Classification));
            return dr;
        }

        public static IEnumerable<ContratorsEntity>Contractors_ByClassification(string Classification)
        {
            //ContratorsEntity CE = new ContratorsEntity();
            List<ContratorsEntity> contractorsCollection = new List<ContratorsEntity>();
           
            SqlDataReader dr = Contractors.Contractors_ByClassificationsReader(Classification);
            try
            {
                while (dr.Read())
                {
                    var CE = new ContratorsEntity
                     {
                         ID = int.Parse(dr["ID"].ToString()),
                         DepartmentID = int.Parse(dr["DepartmentID"].ToString()),
                         Name = dr["ContractorName"].ToString()
                         
                     };
                    contractorsCollection.Add(CE);
                }
            }
            catch { }
            finally { dr.Close(); }
            
           
            return contractorsCollection;
        }
        public static IEnumerable<ContratorsEntity>Contractors_OrderByAsc()
        {
            var sid=new int[]{1,2,3,5};
            var p = from r in Contractors_SelectAll()
                    where sid.Contains(r.SID)
                    orderby r.Name ascending
                    select r;
                  return p;
        }
        public static IEnumerable<ContratorsEntity> ContractorsAll_OrderByAsc()
        {
            //var sid = new int[] { 1, 2, 3, 5 };
            var p = from r in Contractors_SelectAll()
                   
                    orderby r.Name ascending
                    select r;
            return p;
        }
        public static IEnumerable<ContratorsEntity> Users_OrderByAsc(int deptID)
        {
            var sid = new int[] { 10,8,6 };
            var p = from r in Contractors_SelectAll()
                    where !sid.Contains(r.SID) && r.DepartmentID == deptID
                    orderby r.Name ascending
                    select r;
            return p;
        }
        public static string  Contractors_Contact(int ID)
        {
            ContratorsEntity ce = (from r in Contractors_SelectAll()
                                   where r.ID == ID
                                   select r).FirstOrDefault();
            return ce.ContactNumber;
        }
        public static ContratorsEntity Contractors_GetEmailAddress(int id) //Used in admin notification
        {
            ContratorsEntity ce = (from r in Contractors_SelectAll()
                                 where  r.ID == id
                                 select r).FirstOrDefault();
            return ce;
        }
        public static IEnumerable<ContratorsEntity> Contractors_SelectAll()
        {
            List<ContratorsEntity> ContractorsCollection = null;
            string key = CacheNames.DefaultNames.Contractors.ToString();
            if (BaseCache.Cache_Select(key) == null)
            {
                ContractorsCollection=new List<ContratorsEntity>();
                DataTable dt = Contractors_SelectAll_DataTable();
                int rowCount = dt.Rows.Count;
                if (rowCount > 0)
                {
                    for (int T_cnt = 0; T_cnt <=dt.Rows.Count - 1; T_cnt++)
                    {
                        var contractors = new ContratorsEntity
                        {
                            ID = int.Parse(dt.Rows[T_cnt]["ID"].ToString()),
                            Name = dt.Rows[T_cnt]["ContractorName"].ToString(),
                            DepartmentID = int.Parse(dt.Rows[T_cnt]["DepartmentID"].ToString()),
                            AreaID = int.Parse(dt.Rows[T_cnt]["AreaID"].ToString()),
                            ContactNumber = dt.Rows[T_cnt]["ContactNumber"].ToString(),
                            SID=int.Parse(dt.Rows[T_cnt]["SID"].ToString()),
                            EmailAddress=dt.Rows[T_cnt]["EmailAddress"].ToString()
                        };
                        ContractorsCollection.Add(contractors);
                    }
                    
                }
                BaseCache.Cache_Insert(key, ContractorsCollection);
            }
            return BaseCache.Cache_Select(key) as List<ContratorsEntity>;
                //return ContractorsCollection;
        
        }
    }
    #endregion

#region Requirement
    public class Requirement
    {
        public static List<ListItem>SelectAll(bool addSelect)
        {
            List<ListItem> Li = new List<ListItem>();
            if (addSelect)
                Li.Add(new ListItem("Please select...", "0"));
                Li.Add(new ListItem("Essential", "1"));
               // Li.Add(new ListItem("Mandatory", "3"));
                //Li.Add(new ListItem("Not Essential", "2"));
                Li.Add(new ListItem("Personal Development", "2"));
            return Li;

        }
    }
#endregion
#region CourseRe_Occurrence
    public class CourseRe_Occurr
    {
        public static void CourseReOccurr_InsertUpdate(CourseReOccurrence ce)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.CourseReOccurrence_InsertUpdate",
                new SqlParameter("@ID", ce.ID), new SqlParameter("@CourseReOccurres", ce.CourseReOccurres),
                new SqlParameter("@ReOccuerFrequencey", ce.ReOccuerFrequencey), new SqlParameter("@UntilDate", ce.UntilDate),
                new SqlParameter("@WeekDay", ce.WeekDay),new SqlParameter("@BookingID",ce.BookingID),
                new SqlParameter("@BoolVal",ce.BoolVal),new SqlParameter("@CourseID",ce.CourseID),
                new SqlParameter("@BookingDate",ce.BookingDate));


        }

        public static DataTable CourseReOccurr_DataTable()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.CourseReOccurrence_SelectAll").Tables[0];
            return dt;
        }
        public static IEnumerable<CourseReOccurrence> CourseReOccurr_SelectAll()
        {
            List<CourseReOccurrence> cr = new List<CourseReOccurrence>();
            DataTable dt=CourseReOccurr_DataTable();
            int cnt = dt.Rows.Count;
            if (cnt > 0)
            {
                for (int i = 0; i < cnt; i++)
                {
                    var items = new CourseReOccurrence {
                         BookingID=int.Parse(dt.Rows[i]["BookingID"].ToString()),
                         BoolVal = int.Parse(dt.Rows[i]["BoolVal"].ToString()),
                         WeekDay=int.Parse(dt.Rows[i]["WeekDay"].ToString()),
                         CourseReOccurres=int.Parse(dt.Rows[i]["CourseReOccurres"].ToString()),
                         UntilDate=Convert.ToDateTime(dt.Rows[i]["UntilDate"].ToString()),
                         ReOccuerFrequencey=int.Parse(dt.Rows[i]["ReOccuerFrequencey"].ToString()),
                         ID=int.Parse(dt.Rows[i]["ID"].ToString()),
                         BookingDate = Convert.ToDateTime(dt.Rows[i]["BookingDate"].ToString()),
                         DepartmentID=int.Parse(dt.Rows[i]["DepartmentID"].ToString()),
                         CourseID=int.Parse(dt.Rows[i]["CourseID"].ToString()),
                         EmployeeName = dt.Rows[i]["EmployeeName"].ToString(),
                         StatusColor = dt.Rows[i]["StatusColor"].ToString(),
                         StatusID = int.Parse(dt.Rows[i]["StatusID"].ToString()),
                         StatusName = dt.Rows[i]["StatusName"].ToString(),
                         Month=int.Parse(dt.Rows[i]["Month"].ToString()),
                         CourseName=dt.Rows[i]["CourseName"].ToString()
                      
                    };
                    cr.Add(items);
                }
            }

            return cr;
        }

        public static CourseReOccurrence CourseReOccurr_selectByBooking(int bookingID)
        {
            CourseReOccurrence ce = (from r in CourseReOccurr_SelectAll()
                                     where r.BookingID == bookingID && r.BoolVal == 0
                                    
                                     select r).FirstOrDefault();
            return ce;
        }

        public static IEnumerable<CourseReOccurrence> CourseReOccurr_SelectByFilter(int DeptID,int CourseID,DateTime fromDate,DateTime toDate)
        {
            IEnumerable<CourseReOccurrence> users = null;
            if (DeptID != 0 && CourseID != 0 && (fromDate ==Convert.ToDateTime("1/1/1900") || toDate==Convert.ToDateTime("1/1/1900")))
            {

                //var ce23 = from r in Bookings_SelectAll()
                //         where r.ID != -99 && r.DepartmentID == deptID && r.AreaID == areaID
                //         group r by new { r.Employee, r.EmployeeName, r.ClassificationName } into g
                //         select g.Key;

                //var ce1 = (from r in ce
                //           orderby r.EmployeeName
                //           select new EmployeeEntity { ID = r.Employee, Name = r.EmployeeName, ClassificationName = r.ClassificationName }).Distinct().ToList();

                //return ce1;
               var ce = from r in CourseReOccurr_SelectAll()
                     where r.CourseID == CourseID && r.DepartmentID == DeptID 
                     group r by new { r.EmployeeName, r.BookingID,r.CourseID } into r
                   select r.Key;

               users = (from r in ce
                        orderby r.EmployeeName
                        select new CourseReOccurrence { BookingID = r.BookingID, EmployeeName = r.EmployeeName, CourseID=r.CourseID }).Distinct().ToList();

            }
            else
            {
                var ce = from r in CourseReOccurr_SelectAll()
                         where r.CourseID == CourseID && r.DepartmentID == DeptID &&
                         r.BookingDate >= fromDate && r.BookingDate <= toDate
                         group r by new { r.EmployeeName, r.BookingID, r.CourseID } into r
                         select r.Key;
             
                users = (from r in ce
                         orderby r.EmployeeName
                         select new CourseReOccurrence { BookingID = r.BookingID, EmployeeName = r.EmployeeName,CourseID=r.CourseID }).Distinct().ToList();

            }

            return users;
        }
        public static IEnumerable<CourseReOccurrence> CourseReOccurr_SelectSuccessfulDates(int bookingID,int courseID)
        {
            var ce = (from r in CourseReOccurr_SelectAll()
                     where r.BookingID == bookingID && r.CourseID==courseID
                    orderby r.BookingDate ascending
                     select r).Take(3);
            return ce;
        }
        public static CourseReOccurrence CourseReOccurr_SelectDetails(int bookingID,int i)
        {
            CourseReOccurrence ce = (from r in CourseReOccurr_SelectAll()
                      where r.BookingID == bookingID && r.Month==i

                                     select r).FirstOrDefault(); ;
            return ce;
        }

        public static DateTime CourseReOccurr_SelectMonth(int bookingID, int month)
        {
            DateTime bookingDate = Convert.ToDateTime("1/1/1900"); 
            CourseReOccurrence date = (from r in CourseReOccurr_SelectAll()
                        where r.BookingID == bookingID && r.Month == month
                        select r).FirstOrDefault();
            if (date != null)
                bookingDate = date.BookingDate;
            return bookingDate;

        }
        public static string CourseReOccurr_SelectStatusColor(int bookingID, int month)
        {
            string color = ""; 
            CourseReOccurrence Color = (from r in CourseReOccurr_SelectAll()
                                       where r.BookingID == bookingID && r.Month == month
                                       select r).FirstOrDefault();
            if (Color != null)
                color = Color.StatusColor;
            return color;

        }

        public static string CourseReOccurr_SelectStatusName(int bookingID,int courseID)
        {
            string Statusname = "";
            CourseReOccurrence status = (from r in CourseReOccurr_SelectAll()
                                        where r.BookingID == bookingID && r.CourseID == courseID
                                        select r).FirstOrDefault();
            if (status != null)
                Statusname = status.StatusName;
            return Statusname;

        }

    }




    #endregion


#region Feedback
    public class FeedBack
    {
        public static void feedback_Insert(FeedbackEntity fe)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Feedback_InsertUpdate", new SqlParameter("@programmeTitle", fe.programmeTitle),
                new SqlParameter("@FacilitatorsName", fe.FacilitatorsName), new SqlParameter("@Date", fe.Date),
                new SqlParameter("@Location", fe.Location), new SqlParameter("@Name", fe.Name), new SqlParameter("@JobTitle", fe.JobTitle),
                new SqlParameter("@Department", fe.Department), new SqlParameter("@peComments1", fe.peComments1),
                new SqlParameter("@peComments2", fe.peComments2), new SqlParameter("@peComments3", fe.peComments3),
                new SqlParameter("@peRating", fe.peRating), new SqlParameter("@peRatingComments", fe.peRatingComments), new SqlParameter("@teKnowledgeSub", fe.teKnowledgeSub),
                new SqlParameter("@teObviousPrep", fe.teObviousPrep), new SqlParameter("@teStyleDelivery", fe.teStyleDelivery), new SqlParameter("@teResponsiveness", fe.teResponsiveness),
                new SqlParameter("@teLearningClimate", fe.teLearningClimate), new SqlParameter("@teOtherComments", fe.teOtherComments), new SqlParameter("@teLength", fe.teLength),
                new SqlParameter("@tePacing", fe.tePacing), new SqlParameter("@ptComments1", fe.ptComments1), new SqlParameter("@ptComments2", fe.ptComments2),
                new SqlParameter("@BookingID",fe.BookingID));

        }

        public static SqlDataReader feedBack_DataReader(int bookingId)
        {
            SqlDataReader dr=null;
            dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Training.Feedback_Select", new SqlParameter("@BookingID", bookingId));
                return dr;
        }

        public static FeedbackEntity viewFeedBack(int bookingId)
        {
            FeedbackEntity FE = new FeedbackEntity();
            SqlDataReader dr = FeedBack.feedBack_DataReader(bookingId);
            try
            {
                while (dr.Read())
                {
                    FE = new FeedbackEntity
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        Date = DateTime.Parse(dr["Date"].ToString()),
                        Department = dr["Department"].ToString(),
                        FacilitatorsName = dr["FacilitatorsName"].ToString(),
                        JobTitle = dr["JobTitle"].ToString(),
                        Location = dr["Location"].ToString(),
                        Name = dr["Name"].ToString(),
                        peComments1 = dr["peComments1"].ToString(),
                        peComments2 = dr["peComments2"].ToString(),
                        peComments3 = dr["peComments3"].ToString(),
                        peRating = int.Parse(dr["peRating"].ToString()),
                        peRatingComments = dr["peRatingComments"].ToString(),
                        programmeTitle = dr["programmeTitle"].ToString(),
                        ptComments1 = dr["ptComments1"].ToString(),
                        ptComments2 = dr["ptComments2"].ToString(),
                        teKnowledgeSub = int.Parse(dr["teKnowledgeSub"].ToString()),
                        teLearningClimate = int.Parse(dr["teLearningClimate"].ToString()),
                        teLength = int.Parse(dr["teLength"].ToString()),
                        teObviousPrep = int.Parse(dr["teObviousPrep"].ToString()),
                        teOtherComments = dr["teOtherComments"].ToString(),
                        tePacing = int.Parse(dr["tePacing"].ToString()),
                        teResponsiveness = int.Parse(dr["teResponsiveness"].ToString()),
                        teStyleDelivery = int.Parse(dr["teStyleDelivery"].ToString()),
                        BookingID = int.Parse(dr["BookingID"].ToString()),

                    };
                }
            }
            catch { }
            finally { dr.Close(); }
            return FE;
        }
    }
 #endregion

#region CourseFeedBack
    public class CourseFeedBack
    {
        public static void courseFeedBack_InsertUpdate(CourseFeedBackEntity cf)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.CourseFeedback_InsertUpdate",
                new SqlParameter("@CourseTitle", cf.CourseTitle), new SqlParameter("@OrganisedBy", cf.OrganisedBy),
                new SqlParameter("@DatesOfAttendance", cf.DatesOfAttendance), new SqlParameter("@Name", cf.Name),
                new SqlParameter("@Objectives", cf.Objectives), new SqlParameter("@Useful", cf.Useful),
                new SqlParameter("@LearningPoints", cf.LearningPoints), new SqlParameter("@Recommend", cf.Recommend),
                new SqlParameter("@Objects", cf.Objects), new SqlParameter("BookingID", cf.BookingID), new SqlParameter("@JobTitle", cf.JobTitle),
                new SqlParameter("@ActionPlan", cf.ActionPlan), new SqlParameter("@SupportRequired", cf.SupportRequired),
                new SqlParameter("@TimeScale", cf.TimeScale),new SqlParameter("@ActionID ",cf.ActionID));
         }

        public static void actionPlan_InsertUpdate(ActionPlanEntity ae)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.CourseActionPlan_Insert", new SqlParameter("@ActionPlan", ae.ActionPlan), new SqlParameter("@SupportRequired", ae.SupportRequired),
                new SqlParameter("@TimeScale", ae.TimeScale), new SqlParameter("@ActionID ", ae.ID), new SqlParameter("BookingID", ae.BookingID));
        }

        public static DataTable courseFeedBack_SelectAll_DataTable()
        {
            DataTable dt = new DataTable();
            dt=SqlHelper.ExecuteDataset(Constants.DBString,CommandType.StoredProcedure,
                "Training.CourseFeedBack_SelectAll").Tables[0];
            return dt;

        }
        public static DataTable ActionPlan_SelectAll_DataTable()
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure,
                "Training.CourseActionPlan_SelectAll").Tables[0];
            return dt;

        }
        public static IEnumerable<ActionPlanEntity> ActionPlan_All()
        {
            List<ActionPlanEntity> ActionPlanCollection =new List<ActionPlanEntity>();
            DataTable dt = ActionPlan_SelectAll_DataTable();
            int r_cnt = dt.Rows.Count;
            if (r_cnt > 0)
            {
                for (int i = 0; i <= r_cnt - 1; i++)
                {
                    var dep = new ActionPlanEntity
                    {
                        ID = int.Parse(dt.Rows[i]["ID"].ToString()),
                        FeedBackID = int.Parse(dt.Rows[i]["FeedBackID"].ToString()),
                        ActionPlan = dt.Rows[i]["ActionPlan"].ToString(),                         
                        SupportRequired = dt.Rows[i]["SupportRequired"].ToString(),
                        TimeScale = dt.Rows[i]["TimeScale"].ToString(),
                      
                    };
                    ActionPlanCollection.Add(dep);
                }
            }
            return ActionPlanCollection;
        }
        public static IEnumerable<CourseFeedBackEntity> CourseFeedBack_All()
        {
            List<CourseFeedBackEntity> courseCollection = new List<CourseFeedBackEntity>();
            //courseCollection = null;
            DataTable dt = courseFeedBack_SelectAll_DataTable();
            try
            {
                int r_cnt = dt.Rows.Count;
                if (r_cnt > 0)
                {
                    for (int i = 0; i <= r_cnt - 1; i++)
                    {
                        var dep = new CourseFeedBackEntity
                        {
                            ID = int.Parse(dt.Rows[i]["ID"].ToString()),
                            BookingID = int.Parse(dt.Rows[i]["BookingID"].ToString()),
                            CourseTitle = dt.Rows[i]["CourseTitle"].ToString(),
                            DatesOfAttendance = Convert.ToDateTime(dt.Rows[i]["DatesOfAttendance"].ToString()),
                            LearningPoints = dt.Rows[i]["LearningPoints"].ToString(),
                            Name = dt.Rows[i]["Name"].ToString(),
                            Useful = dt.Rows[i]["Useful"].ToString(),
                            Objectives = dt.Rows[i]["Objectives"].ToString(),
                            Objects = dt.Rows[i]["Objects"].ToString(),
                            OrganisedBy = dt.Rows[i]["OrganisedBy"].ToString(),
                            Recommend = dt.Rows[i]["Recommend"].ToString(),
                            JobTitle = dt.Rows[i]["JobTitle"].ToString()
                        };
                        courseCollection.Add(dep);
                    }
                }
            }
            catch { }
            return courseCollection;
        }

        //public static IEnumerable<CourseFeedBackEntity> CourseFeedback_ID(int BookingID)
        //{
        //    var courseFeedBack = (from r in CourseFeedBack_All()
        //                                                   where r.BookingID == BookingID
        //                                                   select new CourseFeedBackEntity{ID=r.ID}).ToList();
        //    return courseFeedBack;
        //}
        public static CourseFeedBackEntity CourseFeedback_Details(int BookingID)
        {

            //BookingsEntity be = (from r in Bookings_SelectAll()
            //                     where r.ID != -99 && r.ID == id
            //                     select r).FirstOrDefault();
            //return be;

            CourseFeedBackEntity be = (from r in CourseFeedBack_All()
                                       where r.BookingID == BookingID
                                       select r).FirstOrDefault();



            return be;
        }
        public static IEnumerable<CourseFeedBackEntity> CourseFeedback_ID(int ID)
        {


           var  bookings = (from r in CourseFeedBack_All()
                                             where   r.BookingID == ID
                                             select new CourseFeedBackEntity { ID = r.ID }).ToList();


            
            return bookings;
        }
        public static IEnumerable<ActionPlanEntity> ActionPlan_Selected(int ID)
        {// IEnumerable<BookingsEntity>
            IEnumerable<ActionPlanEntity> ActionPlanCollection = from r in ActionPlan_All()
                                                             where r.FeedBackID==ID
                                                             select r;

            return ActionPlanCollection;
        }
        public static void test()
        {

            var test = from r in Bookings.Bookings_SelectAll()
                       where r.Employee==141
                      join cp in CourseFeedBack_All() on r.CFeedBackID equals cp.BookingID
                      join pp in ActionPlan_All() on cp.ID equals pp.FeedBackID
                       select new {cp,pp};
           
        }

    }

    #endregion

#region Notification
    public class Notification
    {
        public static int Notification_InsertUpdate(NotificationEntity ne)
        {
            SqlParameter sqlReturn = new SqlParameter("@return ", SqlDbType.Int);
            sqlReturn.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Notification_InsertUpdate",
                new SqlParameter("@ID", ne.ID), new SqlParameter("@UserID", ne.UserID), new SqlParameter("@AdminID", ne.AdminID),
                new SqlParameter("@DepartmentID", ne.DepartmentID), new SqlParameter("@Email", ne.Email),sqlReturn);
            int exist = int.Parse(sqlReturn.Value.ToString());
            BaseCache.Cache_Remove(CacheNames.DefaultNames.Notification.ToString());
            return exist;
        }



        public static void Notification_Delete(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Training.Notification_DeleteById", new SqlParameter("@ID", ID));

        }

        public static DataTable Notification_DataTable()
        {
            DataTable dt=new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Training.Notification_SelectAll").Tables[0];
            return dt;
        }

        public static IEnumerable<NotificationEntity> Notification_SelectAll()
        {

             List<NotificationEntity> notification= null;

            //string key = CacheNames.DefaultNames.Notification.ToString();
            //if (BaseCache.Cache_Select(key) == null)
            //{

                notification = new List<NotificationEntity>();
                DataTable dt = Notification_DataTable();
                int rows = dt.Rows.Count;
                if (rows > 0)
                {
                    for (int i = 0; i <= rows - 1; i++)
                    {
                        var ne = new NotificationEntity
                        {
                            ID = int.Parse(dt.Rows[i]["ID"].ToString()),
                            UserID = int.Parse(dt.Rows[i]["UserID"].ToString()),
                            AdminID = int.Parse(dt.Rows[i]["AdminID"].ToString()),
                            DepartmentID = int.Parse(dt.Rows[i]["DepartmentID"].ToString()),
                            DepartmentName = dt.Rows[i]["DepartmentName"].ToString(),
                            Email = dt.Rows[i]["Email"].ToString(),
                            UserName = dt.Rows[i]["UserName"].ToString(),
                            ManagerName = dt.Rows[i]["ManagerName"].ToString(),
                            ManagerEmail = dt.Rows[i]["EmailAddress"].ToString(),
                            ContactNumber = dt.Rows[i]["ContactNumber"].ToString()
                        };
                        notification.Add(ne);
                    }
                //}
                // BaseCache.Cache_Insert(key,notification);
            }
                return notification;
           // return BaseCache.Cache_Select(key)as List<NotificationEntity>;

            // BaseCache.Cache_Insert(key, BookingsEntityCollection);
            //}

            //return BaseCache.Cache_Select(key) as List<BookingsEntity>;
        }

        public static List<NotificationEntity> Notifiaction_Select(int mainAdminID)
        {
           List<NotificationEntity> users= (from r in Notification_SelectAll()
                        where r.ID==-99 || r.AdminID==mainAdminID
                        select r).ToList();
            return users;
        }

        public static NotificationEntity Notifiaction_SelectByID(int ID)
        {
            NotificationEntity ne = (from r in Notification_SelectAll()
                                     where r.ID != -99 && r.ID == ID
                                     select r).FirstOrDefault();
            return ne;
        }

        public static List<NotificationEntity> Notification_GetManagers(int deptId)
        {
            List<NotificationEntity> managerMailId = (from r in Notification_SelectAll()
                                                      where r.ID != -99 && r.DepartmentID == deptId
                                                      select r).ToList();
            return managerMailId;
        }

    }
    #endregion

#region mailStyles
    public class mailStyles
    {
        public static string MailCss()
        { string style =@"<html xmlns='http://www.w3.org/1999/xhtml'><head>
                            <title>
                               Feedback/View feedback
                                </title>
                                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                                <style type='text/css'>
                                body{
                                margin:0;
                                font-family:Verdana, Arial, Helvetica, sans-serif;
                                font-size:12px;
                                }
                                table td{
                                padding:5px;
                                }
                                .hdr1{
                                font-size:18px;
                                padding-top:15px;
                                text-align:right;
                                }
                                .hilite{
                                color:#4b0049;
                                }
                                .hdr td{
                                font-size:12px;
                                font-weight:bold;
                                color:#fff;
                                background:#8595a6;
                                text-align:left;
                                }
                                .cont_row td{
                                border:#8595a6 1px solid;
                                color:#219de6;
                                font-weight:bold
                                }
                                .bo_line{
                                border-bottom:#999 10px solid;
                                }
                                .hdrt {
                                font-size:12px;
                                font-weight:bold;
                                color:#fff;
                                height:30px;
                                background:#8595a6;
                                text-align:left;
                                }
                                table.Grid_table {
                                border:#e4dcd3 1px solid;
                                }
                                .Grid_table td{
                                height:15px;
                                padding:5px 0 0 10px;
                                }
                                .odd_row{
                                background:#f2f1f1;
                                color:#636363;
                                }
                                .even_row{
                                background:#fff;
                                color:#636363;
                                }
                                </style>";

        return style;
        }
    }
#endregion
}
