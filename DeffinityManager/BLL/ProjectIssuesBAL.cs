using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

namespace ProjectMgt.BAL
{
    /// <summary>
    /// Summary description for ProjectIssuesBLL
    /// </summary>
    /// 
    public class ProjectIssuesBAL
    {
        public ProjectIssuesBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static ProjectIssue ProjectIssues_selectByID(int IssueID)
        {
            using (projectTaskDataContext pd = new projectTaskDataContext())
            {
                return pd.ProjectIssues.Where(p => p.ID == IssueID).FirstOrDefault();
            }
        }
    }
}