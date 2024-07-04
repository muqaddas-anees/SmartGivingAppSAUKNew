using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;

/// <summary>
/// Summary description for ProjectPortprofil
/// </summary>
public class ProjectPortFolioBAL
{
    #region Select company by ID
    public static ProjectPortfolio SelectbyId(int id)
    {

        ProjectPortfolio cmpy = new ProjectPortfolio();
        using (PortfolioDataContext pp = new PortfolioDataContext())
        {
            cmpy = pp.ProjectPortfolios.Where(p => p.ID == id).Select(p => p).FirstOrDefault();
        }
        return cmpy;
    }
    #endregion

    #region Select requesters name by ID
    public static PortfolioContact SelectNamebyId(int id)
    {

        PortfolioContact Rname = new PortfolioContact();
        using (PortfolioDataContext pp = new PortfolioDataContext())
        {
            Rname = pp.PortfolioContacts.Where(p => p.ID == id).Select(p => p).FirstOrDefault();
        }
        return Rname;
    }
    #endregion
}