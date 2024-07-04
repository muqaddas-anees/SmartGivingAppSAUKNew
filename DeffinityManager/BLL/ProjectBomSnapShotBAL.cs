using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMgt.BAL;
using ProjectMgt.Entity;
using ProjectMgt.DAL;


/// <summary>
/// Summary description for ProjectBomSnapShotBAL
/// </summary>
public class ProjectBomSnapShotBAL
{
	public ProjectBomSnapShotBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void InsertDataIntoJournal(int ProjectId,string Name,int CreatedBy)
    {
        try 
        {
            using (projectTaskDataContext Pdc = new projectTaskDataContext())
            {
                Pdc.SnapDataInsertIntoJournal(ProjectId, Name, CreatedBy);
                Pdc.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}