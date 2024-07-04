using System;
using System.Data;
using Incidents.Entity;
using Incidents.StateManager;
using System.Data.SqlClient;

namespace Incidents.DAL
{
    /// <summary>
    /// The DAL class for the incident management.
    /// </summary>
    public class TaskHelper
    {
        public TaskHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>Inserts the record into the Task Table</summary>
        /// <returns>Returns true if inserted. False if insertion fails.</returns>
        public static bool Insert(Task task)
        {
            int recordsAffected = InsertUpdateHelper(task, false, TaskCommands.cmdInsert);
            TaskState.ClearTaskCache();
            return ((recordsAffected > 0) ? true : false);
        }


        /// <summary>
        /// Updates the record of the Task Table
        /// </summary>
        /// <returns>Returns true is updated successfully.  If not returns false.</returns>
        public static bool Update(Task task)
        {
            int recordsAffected = InsertUpdateHelper(task, true, TaskCommands.cmdUpdate);
            TaskState.ClearTaskCache();
            return ((recordsAffected > 0) ? true : false);
        }

        private static int InsertUpdateHelper(Task task, bool isUpdate, string sqlCommand)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (isUpdate)
                        cmd.Parameters.AddWithValue("@ID", task.Id);
                    cmd.Parameters.AddWithValue("@changeControlID", task.ChangeControlID);
                    cmd.Parameters.AddWithValue("@task", task.TaskDescription);
                    cmd.Parameters.AddWithValue("@originalDate", task.OriginalDate);
                    cmd.Parameters.AddWithValue("@newDate", task.NewDate);
                    cmd.Parameters.AddWithValue("@change", task.Change);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return recordsAffected;
        }

        /// <summary>
        /// Deletes the record by ID
        /// </summary>
        /// <param name="ID">ID of the record to be deleted</param>
        /// <returns>Returns true if deleted successfully.  If not returns false.</returns>
        public static bool Delete(Task task)
        {
            int recordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(TaskCommands.cmdDelete, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", task.Id);
                    conn.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            TaskState.ClearTaskCache();
            return ((recordsAffected > 0) ? true : false);
        }

        /// <summary>
        /// Gets all the records from the database
        /// </summary>
        /// <returns>Returns the TaskCollection object for all the records</returns>
        public static TaskCollection LoadAllTasks()
        {
            TaskCollection tasks = new TaskCollection();
            if (TaskState.TaskCache == null)
            {
                LoadDataHelper(tasks, TaskCommands.cmdSelectAll,0);
                TaskState.TaskCache = tasks;
            }
            else
                tasks = (TaskCollection)TaskState.TaskCache;
            return tasks;
        }
        public static TaskCollection LoadTasksById(int id)
        {
            TaskCollection tasks = new TaskCollection();
            if (TaskState.TaskCache == null)
            {
                LoadDataHelper(tasks, TaskCommands.cmdSelectById,id);
                TaskState.TaskCache = tasks;
            }
            else
                tasks = (TaskCollection)TaskState.TaskCache;
            return tasks;
        }

        //Adds the Incident to the IncidentCollection and build the table.
        private static void LoadDataHelper(TaskCollection tasks, string sqlStatement,int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@changeControlID",id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Task task = GetTask(reader);
                            tasks.Add(task);
                        }
                    }
                }
                conn.Close();
            }
        }

        //Reads through the datareader. Returns a single record.
        private static Task GetTask(SqlDataReader reader)
        {
            Task task = new Task();
            if (reader.IsClosed)
                reader.Read();
            task.Id = Convert.ToInt32(reader["Id"]);
            task.ChangeControlID = Convert.ToInt32(reader["ChangeControlID"]);
            task.Change = reader["Change"].ToString();
            task.NewDate = Convert.ToDateTime(reader["NewDate"]);
            task.OriginalDate = Convert.ToDateTime(reader["OriginalDate"]);
            task.TaskDescription = reader["TaskDescription"].ToString();
            return task;
        }
    }
}