using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using net.sf.mpxj.reader;
using net.sf.mpxj.mpp;
using net.sf.mpxj.mspdi;
using net.sf.mpxj;
using java.util;
using java.io;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Microsoft.ApplicationBlocks.Data;





public partial class ProjectMppFileUpload :BasePage
{
    //private InputStream InputFile;
    private ProjectReader Reader;
    Guid d;
    protected void Page_Load(object sender, EventArgs e)
    {
       

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (QueryStringValues.Project > 0)
            {
                //BuilkInsert(RetrunMppTable());
                //ProcessInput();
                //Remove cache
                string Key = string.Format("{0}-{1}", CacheNames.DefaultNames.ProjectReference, QueryStringValues.Project);
                BaseCache.Cache_Remove(Key);

                BuilkInsert(ArraylistToDataTable());
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    

    #region Builk insert data
    private void BuilkInsert(DataTable dt)
    {
        using (SqlConnection conn = new SqlConnection(Constants.DBString))
        {
            SqlBulkCopyColumnMapping mapping1 = new SqlBulkCopyColumnMapping("ProjectReference", "ProjectReference");
            SqlBulkCopyColumnMapping mapping2 = new SqlBulkCopyColumnMapping("ItemDescription", "ItemDescription");
            SqlBulkCopyColumnMapping mapping3 = new SqlBulkCopyColumnMapping("ListPosition", "ListPosition");
            SqlBulkCopyColumnMapping mapping4 = new SqlBulkCopyColumnMapping("ProjectStartDate", "ProjectStartDate");
            SqlBulkCopyColumnMapping mapping5 = new SqlBulkCopyColumnMapping("ProjectEndDate", "ProjectEndDate");
            SqlBulkCopyColumnMapping mapping6 = new SqlBulkCopyColumnMapping("IndentLevel", "IndentLevel");
            SqlBulkCopyColumnMapping mapping7 = new SqlBulkCopyColumnMapping("ItemStatus", "ItemStatus");
            SqlBulkCopyColumnMapping mapping8 = new SqlBulkCopyColumnMapping("isMilestone", "isMilestone");

            SqlBulkCopyColumnMapping mapping9 = new SqlBulkCopyColumnMapping("gid", "gid");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("mppid", "mppid");
            SqlBulkCopyColumnMapping mapping11 = new SqlBulkCopyColumnMapping("dependency", "dependency");
            //SqlBulkCopyColumnMapping mapping12 = new SqlBulkCopyColumnMapping("IndentLevel", "isLeaf");


            using (SqlBulkCopy copy = new SqlBulkCopy(conn))
            {
                copy.ColumnMappings.Add(mapping1);
                copy.ColumnMappings.Add(mapping2);
                copy.ColumnMappings.Add(mapping3);
                copy.ColumnMappings.Add(mapping4);
                copy.ColumnMappings.Add(mapping5);
                copy.ColumnMappings.Add(mapping6);
                copy.ColumnMappings.Add(mapping7);
                copy.ColumnMappings.Add(mapping8);
                copy.ColumnMappings.Add(mapping9);
               // copy.ColumnMappings.Add(mapping12);

                conn.Open();
                copy.DestinationTableName = "ProjectTaskItems";

                try
                {
                    copy.WriteToServer(dt);
                }
                catch (Exception ex)
                {
                    LogExceptions.LogException(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }

            using (SqlBulkCopy copy = new SqlBulkCopy(conn))
            {
                
                copy.ColumnMappings.Add(mapping9);
                copy.ColumnMappings.Add(mapping10);
                copy.ColumnMappings.Add(mapping11);


                conn.Open();
                copy.DestinationTableName = "TempMppFileDependency";

                try
                {
                    copy.WriteToServer(dt);

                    InsertDependencies(QueryStringValues.Project);
                }
                catch (Exception ex)
                {
                    LogExceptions.LogException(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
        }
        //Reorder the inserted items
        Re_order();
        //updates dates 
        Updatedates();
        //update gantt
        GanttUpdate();
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SCRIPT", string.Format("window.parent.location.href = 'ProjectOverviewV4.aspx?project={0}';", QueryStringValues.Project), true);
    }

    public void InsertDependencies(int projectreference)
    {
        SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "projecttaskdependency_bulkinsert", new SqlParameter("@ProjectReference", projectreference));
        
    }

    #endregion

    #region Implentation of MPXJ

    private bool ProcessInput()
    {
        HttpPostedFile HttpPostedFile = filempp.PostedFile;
        if (HttpPostedFile == null)
        {

            return false;
        }
        string[] tmp = HttpPostedFile.FileName.Split(new Char[] { '.' });
        string ext = tmp[tmp.Length - 1].ToLower();
        if (ext != "xml" && ext != "mpp")
        {

            return false;
        }

        int Length = HttpPostedFile.ContentLength;
        byte[] TmpArray = new byte[Length];
        Stream Input = HttpPostedFile.InputStream;
        Input.Read(TmpArray, 0, Length);
        //InputFile = new ByteArrayInputStream(TmpArray);

        if (ext == "xml")
            Reader = new MSPDIReader();
        else
            Reader = new MPPReader();
        return true;
    }
    private ArrayList ProcessOutput()
    {
        string error = string.Empty;
        HttpPostedFile httpPostedFile = filempp.PostedFile;
        if (httpPostedFile == null)
        {
            error = "";
        }
        string[] tmp = httpPostedFile.FileName.Split(new Char[] { '.' });
        string ext = tmp[tmp.Length - 1].ToLower();
        if (ext != "xml" && ext != "mpp")
        {
            error = "";
        }
       
        ArrayList ar = new ArrayList();
        var folderpath = Server.MapPath("~/WF/UploadData/Temp");
        if(!Directory.Exists(folderpath))
        {
            Directory.CreateDirectory(folderpath);
        }
        var filepath = folderpath + "//" + filempp.PostedFile.FileName;
        httpPostedFile.SaveAs(filepath);
        string file = filepath;
        ProjectReader reader = ProjectReaderUtility.getProjectReader(file);
        ProjectFile projectFile = reader.read(file);


        ar = GetEachTask(projectFile);
        return ar;
    }

    private string formatDateTime(Date date)
    {
        string result = "";

        GregorianCalendar c = new GregorianCalendar();

        c.setTime(date);
        int year = c.get(Calendar.YEAR);
        int month = c.get(Calendar.MONTH) + 1;
        int day = c.get(Calendar.DAY_OF_MONTH);
        int hour = c.get(Calendar.HOUR_OF_DAY);
        int minute = c.get(Calendar.MINUTE);



        result = year.ToString() + "." + month.ToString() + "." + day.ToString() + " " + hour.ToString() + ":" + minute.ToString();

        return result;
    }
    private ArrayList GetEachTask(ProjectFile project)
    {
        ArrayList GetTasks = new ArrayList();

        List tables = project.getTables();
        Iterator iter = tables.iterator();

        for (int i = 0; i < project.getAllTasks().size(); i++)
        {
            Task task = (Task)project.getAllTasks().get(i);
            if (task.getName() != null)
            {
                if (task == null) return null;

                Date start = task.getStart();
                if (start == null) { lblMsg.Text = "Dates in your MS Project file are incomplete. Please check the file and try again"; return null; }

                Date finish = task.getFinish();
                if (finish == null) { lblMsg.Text = "Dates in your MS Project file are incomplete. Please check the file and try again"; return null; }
                int progress = System.Convert.ToInt32(task.getPercentageComplete().doubleValue());
                int mppid = System.Convert.ToInt32(task.getID().doubleValue());
                //string s = task.getACWP().toString();
                //s = task.getOutlineNumber().ToString();
                //s = task.getWBSLevel();
                
                
                
                Guid d = Guid.NewGuid();
                StringBuilder predecessors = new StringBuilder();

                if (task.getPredecessors() != null)
                {
                    for (int j = 0; j < task.getPredecessors().size(); j++)
                    {
                        net.sf.mpxj.Relation relation = (net.sf.mpxj.Relation)task.getPredecessors().get(j);
                        //int predeUniqueID =System.Convert.ToInt32( (Task)relation.getTargetTask().getID().doubleValue());
                        Task task1 = (Task)relation.getTargetTask();
                        int predeUniqueID1 = System.Convert.ToInt32(task1.getID().doubleValue());
                        // Predecessor p = task.getPredecessors().get(j).ToString();
                        //Task pTask =   taskIndex.get(p.getUID());
                        //predecessors.append(pTask.getOrder());
                        predecessors.Append(predeUniqueID1.ToString());
                        predecessors.Append(", ");

                    }
                }




                //if (task != null && task.getPredecessors() != null && task.getPredecessors().size() != 0)
                //{
                //    for (int j = 0; j < task.getPredecessors().size(); i++)
                //    {
                //        net.sf.mpxj.Relation relation = (net.sf.mpxj.Relation)task.getPredecessors().get(i);
                //        string predeUniqueID = (net.sf.mpxj.Relation)relation.ger.getTaskUniqueID().toString();
                //        for (int iCount2 = 1; iCount2 <= projectFile.getAllTasks().size(); iCount2++)
                //        {
                //            net.sf.mpxj.Task task = (net.sf.mpxj.Task)projectFile.getAllTasks().get(iCount2);
                //            if (task.getUniqueID().toString() == predeUniqueID)
                //            {
                //                if (data[iCount].predecessorsList != "")
                //                    data[iCount].predecessorsList = data[iCount].predecessorsList + "," + iCount2;
                //                else
                //                    data[iCount].predecessorsList = iCount2.ToString();
                //                break;
                //            }
                //        }
                //    }
                //}

                //string s = task.getPredecessors().ToString();
                GetTasks.add(new TaskEntity1(task.getName(), formatDateTime(start).ToString(), formatDateTime(finish).ToString(), task.getWBS(), progress.ToString(),
                    task.getMilestone(), d.ToString(), mppid, predecessors.ToString()));
            }
        }

        return GetTasks;
    }

    public DataTable ArraylistToDataTable()
    {
        int GetLisPos = GetListposition();
        int project = QueryStringValues.Project;
        ArrayList ALGetData = ProcessOutput();
        DataTable dt = new DataTable();
        //try
        //{

        dt.Columns.Add("ProjectReference");
        dt.Columns.Add("ItemDescription");
        dt.Columns.Add("IndentLevel");
        dt.Columns.Add("ProjectStartDate");
        dt.Columns.Add("ProjectEndDate");
        dt.Columns.Add("ListPosition");
        dt.Columns.Add("ItemStatus");
        dt.Columns.Add("isMilestone");
        dt.Columns.Add("gid");
        dt.Columns.Add("mppid");
        dt.Columns.Add("dependency");

        foreach (TaskEntity1 task in ALGetData)
        {
            if (task != null)
            {
                int i = 0;
                DataRow dr;
                dr = dt.NewRow();
                dr[i++] = project;
                dr[i++] = task.name;
                dr[i++] = task.indentlevel;
                dr[i++] = task.startdate;
                dr[i++] = task.enddate;
                dr[i++] = GetLisPos;
                dr[i++] = 1;// staus 1 is pending
                dr[i++] = task.isMilestone;
                dr[i++] = task.gid;
                dr[i++] = task.mppid;
                dr[i++] = task.dependency;
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }

    #endregion

    //issue for below method is it will work only for 2003 and below versions
    #region Read file and return datatabel 

//    private DataTable RetrunMppTable()
//    {
//        string filePath = filempp.PostedFile.FileName;
//        string Extension = Path.GetExtension(filePath);

//        //Get the server path and save the file
//        string path = Server.MapPath("Upload") + "\\" + filempp.FileName;
//        filempp.SaveAs(path);
//        string conStr = string.Empty;
//        switch (Extension)
//        {
//            case ".mpp": //Excel 97-03
//                conStr = "Provider=Microsoft.Project.OLEDB.12;PROJECT NAME=" + path;
//                break;
           
//        }
//        //string strSqlQuery = "Select TaskID as Id,TaskName as Name,TaskDuration as Duration,TaskStart as Start,TaskFinish as Finish, TaskPredecessors as Predecessors,TaskOutlineLevel,TaskOutlinenumber From Tasks";
//        string strSqlQuery = @"Select TaskName as ItemDescription,TaskStart as ProjectStartDate,TaskFinish as ProjectEndDate,
//        TaskOutlineLevel as IndentLevel From Tasks where TaskName <> ''";
////        string strSqlQuery = string.Format(@"Select {0} as ProjectReference, TaskName as ItemDescription,{1} ListPosition
////TaskStart as ProjectStartDate,TaskFinish as ProjectEndDate,TaskOutlinenumber as IndentLevel,{2} as ItemStatus 
////From Tasks", QueryStringValues.Project, GetListposition(),1);

//        int project = QueryStringValues.Project;
//        int Listposition = GetListposition();
//        //set default status 1-pending
//        int itemstatus = 1;
//        DataTable dt = new DataTable();
//        ADODB.ConnectionClass conn = new ADODB.ConnectionClass();
//        ADODB.RecordsetClass rs = new ADODB.RecordsetClass();
//        conn.CommandTimeout = 30;
//        conn.Open(conStr, null, null, 0);
//        //strSqlQuery = "Select TaskID as Id,TaskStart as Start,TaskFinish as Finish,TaskDuration as Dur,TaskDuration1 as UpdateVar, TaskText1 as Responsibility,TaskStart1 as Start1,TaskFinish1 as Finish1,TaskName as TaskName From Tasks";
//        //strSqlQuery = "Select TaskID as Id,TaskName as Name,TaskDuration as Duration,TaskStart as Start,TaskFinish as Finish, TaskPredecessors as Predecessors From Tasks";
//        rs.Open(strSqlQuery, conn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly, (int)ADODB.CommandTypeEnum.adCmdText);
//        int val = int.Parse(rs.RecordCount.ToString());
//        for (int x = 0; x < rs.Fields.Count; x++)
//        {
//            dt.Columns.Add(new DataColumn(rs.Fields[x].Name, rs.Fields[x].Value.GetType()));
//        }
//        //Add customer columns
//        if (dt.Columns.Count > 1)
//        {
//            dt.Columns.Add(new DataColumn("ProjectReference", System.Type.GetType("System.Int32")));
//            dt.Columns.Add(new DataColumn("ListPosition", System.Type.GetType("System.Int32")));
//            dt.Columns.Add(new DataColumn("ItemStatus", System.Type.GetType("System.Int32")));
//        }
//        int y = int.Parse(rs.PageSize.ToString());
//        while (!rs.EOF)
//        {
//            DataRow dr = dt.NewRow();
//            for (int x = 0; x < rs.Fields.Count; x++)
//            {
//                dr[rs.Fields[x].Name] = rs.Fields[x].Value;
//                dr["ProjectReference"] = project;
//                dr["ListPosition"] = Listposition;
//                dr["ItemStatus"] = itemstatus;
                
//            }
//            dt.Rows.Add(dr);
//            rs.MoveNext();
//        }
//        conn.Close();

//        return dt;
//    }

    private int GetListposition()
    {
        int tListposition = 0;
        SqlConnection cn = new SqlConnection(Constants.DBString);
        SqlCommand cmd = new SqlCommand(string.Format("Select isnull(Max(ListPosition),0)+1 from ProjectTaskItems where ProjectReference ={0}", QueryStringValues.Project), cn);
        cn.Open();
        tListposition = int.Parse(cmd.ExecuteScalar().ToString());
        cmd.Dispose();
        cn.Close();
        return tListposition;
    }
    private void Re_order()
    {
        SqlConnection cn = new SqlConnection(Constants.DBString);
        SqlCommand cmd = new SqlCommand("DN_TaskItemListpositionUpdate", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ProjectReference", QueryStringValues.Project));
        cn.Open();
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        cn.Close();

    }
    private void GanttUpdate()
    {
        SqlConnection cn = new SqlConnection(Constants.DBString);
        SqlCommand cmd = new SqlCommand("ProjectTask_ganttitems_update", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@projectreference", QueryStringValues.Project));
        cn.Open();
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        cn.Close();

    }
    private void Updatedates()
    {
        SqlConnection cn = new SqlConnection(Constants.DBString);
        SqlCommand cmd = new SqlCommand("update ProjectTaskItems set StartDate = ProjectStartDate ,CompletionDate = ProjectEndDate where ProjectReference in (select ProjectReference from projects where ProjectReference=@ProjectReference )and (datepart(yy,StartDate) =1900 or StartDate is null or datepart(yy,CompletionDate) =1900 or CompletionDate is null)", cn);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add(new SqlParameter("@ProjectReference", QueryStringValues.Project));
        cn.Open();
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        cn.Close();

    }
   
    #endregion
}

#region Custom class for mpp
public class TaskEntity1
{
    public int id { get; set; }
    public string name { get; set; }
    public string startdate { get; set; }
    public string enddate { get; set; }
    public int indentlevel { get; set; }
    public string progress { get; set; }
    public bool isMilestone { get; set; }
    public string gid { get; set; }
    public int mppid { get; set; }
    public string dependency { get; set; }

    public TaskEntity1(string g_name, string g_startdate, string g_enddate, string g_indentlevel, string g_progress, bool g_isMilestone, string g_gid, int g_mppid, string g_dependency)
    {
        //id = g_id;
        name = g_name;
        startdate = g_startdate;
        enddate = g_enddate;
        indentlevel = int.Parse(getIndent(g_indentlevel));
        progress = g_progress;
        isMilestone = g_isMilestone;
        gid = g_gid;
        mppid = g_mppid;
        dependency = g_dependency;
    }
    private string getIndent(string strIndent)
    {
        string[] arInfo = new string[15];
        char[] splitter = { '.' };
        arInfo = strIndent.Split(splitter);
        int intCount = arInfo.Length - 1;
        return intCount.ToString();
    }
}
#endregion