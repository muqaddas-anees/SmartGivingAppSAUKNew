using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using Location.DAL;
using Location.Entity;
using UserMgt.DAL;

/// <summary>
/// Summary description for DisplayColumnBAL
/// </summary>
public class DisplayColumnBAL
{
	public DisplayColumnBAL()
	{
		//
		// TODO: Add constructor logic here
		//
      
	}
    public List<DisplayColumn> selectinggridcolumns()
    {
        using (DCDataContext dc = new DCDataContext())
        {
            var col_id = (from a in dc.DisplayColumnsByUsers where a.UserID == sessionKeys.UID select a.DisplayColumnID).ToArray();
            List<DisplayColumn> x = (from b in dc.DisplayColumns where !col_id.Contains(b.ID) orderby(b.ColumnName) ascending  select b).ToList();
            return x;
        }
    }
    public void Insertrecord(int ID)
    {
        using (DCDataContext dc = new DCDataContext())
        {

            var i = (from a in dc.DisplayColumnsByUsers where a.UserID == sessionKeys.UID orderby a.Position descending select a).Count();
          

            if (i!=0)
            {
                var j = (from a in dc.DisplayColumnsByUsers where a.UserID == sessionKeys.UID orderby a.Position descending select a).First();
                
                DisplayColumnsByUser displayrecords = new DisplayColumnsByUser();
                displayrecords.DisplayColumnID = ID;
                displayrecords.UserID = sessionKeys.UID;
                displayrecords.Position = (j.Position) + 1;
                dc.DisplayColumnsByUsers.InsertOnSubmit(displayrecords);
                dc.SubmitChanges();
            }
            else
            {
                DisplayColumnsByUser displayrecords = new DisplayColumnsByUser();
                displayrecords.DisplayColumnID = ID;
                displayrecords.UserID = sessionKeys.UID;
                displayrecords.Position = 0;
                dc.DisplayColumnsByUsers.InsertOnSubmit(displayrecords);
                dc.SubmitChanges();
            }
        }
        
    }
    public void deleterecord(int ID)
    {
        using (DCDataContext dc = new DCDataContext())
        {
            DisplayColumnsByUser removerecord = new DisplayColumnsByUser();
            removerecord = (from a in dc.DisplayColumnsByUsers where a.ID == ID select a).FirstOrDefault();
            dc.DisplayColumnsByUsers.DeleteOnSubmit(removerecord);
            dc.SubmitChanges();
        }
    }
    //Default inserting for first user
    public Array Insertfornewuser()
    {
        DCDataContext dc = new DCDataContext();
        string list = "1,3,4,9,11,12,15,35,36,37";
        Array b = list.Split(',');
        int i = 0;
        foreach (var x in b)
        {
            if (x!=string.Empty)
            {
                DisplayColumnsByUser dis = new DisplayColumnsByUser();
                dis.DisplayColumnID = int.Parse(x.ToString());
                dis.UserID = sessionKeys.UID;
                dis.Position = i;
                dc.DisplayColumnsByUsers.InsertOnSubmit(dis);
                dc.SubmitChanges();
                i = i + 1;
            }
        }
        var col_id = (from a in dc.DisplayColumnsByUsers where a.UserID == sessionKeys.UID select a.DisplayColumnID).ToArray();
        Array ab = (from c in dc.DisplayColumns where !col_id.Contains(c.ID) select c.Value).ToArray();
        return ab;
    }
    //sample
    public int selectMaxPosition()
    {
        using (DCDataContext dc = new DCDataContext())
        {
            DisplayColumnsByUser dis=new DisplayColumnsByUser();
            int i =(int)(from a in dc.DisplayColumnsByUsers where a.UserID == sessionKeys.UID orderby a.Position descending select a.Position).First();
            return i;
        }
    }
}