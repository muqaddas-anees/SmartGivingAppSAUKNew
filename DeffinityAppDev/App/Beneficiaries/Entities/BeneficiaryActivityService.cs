using DeffinityAppDev.App.Beneficiaries.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace DeffinityAppDev.App.Beneficiaries.Entities
{
    public class BeneficiaryActivityService
    {
        private readonly MyDatabaseContext _context;

        public BeneficiaryActivityService()
        {
            _context = new MyDatabaseContext();
        }

        public List<BeneficiaryActivity> GetTodayActivities()
        {
            try
            {
                using (var context = new MyDatabaseContext())
                {
                    var today = DateTime.Today;

                    // Ensure the query uses DbFunctions for proper translation to SQL
                    var activities = context.BeneficiaryActivities
                        .Where(a => DbFunctions.TruncateTime(a.ActivityDate) == today)
                        .ToList();

                    System.Diagnostics.Debug.WriteLine($"Retrieved {activities.Count} activities for today.");
                    return activities;
                }
            }
            catch (Exception ex)
            {
                // Log error details
                System.Diagnostics.Debug.WriteLine($"Error occurred: {ex.Message}");

                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                // Rethrow the exception with detailed message
                var innerException = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                throw new Exception($"Error while processing: {ex.Message}, Inner Exception: {innerException}");
            }
        }

        public List<BeneficiaryActivity> GetThisWeekActivities()
        {
            try
            {
                Debug.WriteLine("in the BeneficiaryActivityService.GetThisWeekActivities() method");
                var today = DateTime.Today;
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek); // Sunday as start of week
                var endOfWeek = startOfWeek.AddDays(7); // End of week is next Sunday

                // Ensure the query is translated to SQL
                var activities = _context.BeneficiaryActivities
                    .Where(a => a.CreatedAt >= startOfWeek && a.CreatedAt < endOfWeek)
                    .ToList();

                Debug.WriteLine($"Retrieved {activities.Count} activities for this week.");
                return activities;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetThisWeekActivities: {ex.Message}");
                return new List<BeneficiaryActivity>(); // Return empty list on error
            }
        }

        public List<BeneficiaryActivity> GetThisMonthActivities()
        {
            try
            {
                var today = DateTime.Today;
                var startOfMonth = new DateTime(today.Year, today.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // Get the last day of the month

                // Ensure the query is translated to SQL
                var activities = _context.BeneficiaryActivities
                    .Where(a => a.CreatedAt >= startOfMonth && a.CreatedAt <= endOfMonth)
                    .ToList();

                Debug.WriteLine($"Retrieved {activities.Count} activities for this month.");
                return activities;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetThisMonthActivities: {ex.Message}");
                return new List<BeneficiaryActivity>(); // Return empty list on error
            }
        }

        public List<BeneficiaryActivity> GetThisYearActivities()
        {
            try
            {
                var today = DateTime.Today;
                var startOfYear = new DateTime(today.Year, 1, 1);
                var endOfYear = new DateTime(today.Year, 12, 31);

                // Ensure the query is translated to SQL
                var activities = _context.BeneficiaryActivities
                    .Where(a => a.CreatedAt >= startOfYear && a.CreatedAt <= endOfYear)
                    .ToList();

                Debug.WriteLine($"Retrieved {activities.Count} activities for this year.");
                return activities;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetThisYearActivities: {ex.Message}");
                return new List<BeneficiaryActivity>(); // Return empty list on error
            }
        }




    }
}
