using System;
using System.Collections.Generic;

/// <summary>
/// The table for the Portfolio Check List items.
/// </summary>

namespace Health.Entity
{
    
    public class PortfolioHealthCheck
    {
        int id = 0;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        int checkListID = 0;

        public int CheckListID
        {
            get { return checkListID; }
            set { checkListID = value; }
        }

        string checkListText = string.Empty;

        public string CheckListText
        {
            get { return checkListText; }
            set { checkListText = value; }
        }

        int portfolioID = 0;

        public int PortfolioID
        {
            get { return portfolioID; }
            set { portfolioID = value; }
        }

        string portfolioName = string.Empty;

        public string PortfolioName
        {
            get { return portfolioName; }
            set { portfolioName = value; }
        }

        string emailDistributionList = string.Empty;

        public string EmailDistributionList
        {
            get { return emailDistributionList; }
            set { emailDistributionList = value; }
        }
    }

    public class PortfolioHealthCheckCollection : List<PortfolioHealthCheck>
    { 
        
    }
}