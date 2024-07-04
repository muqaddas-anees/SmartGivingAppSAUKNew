using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

public class SearchResult
{
    public int fileID { get; set; }

    public string fileName { get; set; }

    public string searchKeys { get; set; }

    public string searchResults { get; set; }

    public int matches { get; set; }

    public DateTime uploadedTime { get; set; }

    public int fileSize { get; set; }

    public string uploadedBy { get; set; }

    public string version { get; set; }

}

public class SearchResultList : List<SearchResult>
{ 
    //Do nothing..
}