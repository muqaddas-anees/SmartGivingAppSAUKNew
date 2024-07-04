using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class _GDefault : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            GetDistance("560061", "560070");
            getLatLong("560061");
        }
    }

    public class geocodes
    {
        public string lat { set; get; }
        public string lng { set; get; }
    }
    public string getLatLong(string Zip)
    {
        string latlong = "", address = "";
        address = "http://maps.googleapis.com/maps/api/geocode/xml?components=postal_code:" + Zip.Trim() + "&sensor=false";
        var result = new System.Net.WebClient().DownloadString(address);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);
        XmlNodeList parentNode = doc.GetElementsByTagName("location");
        var lat = "";
        var lng = "";
        foreach (XmlNode childrenNode in parentNode)
        {
            lat = childrenNode.SelectSingleNode("lat").InnerText;
            lng = childrenNode.SelectSingleNode("lng").InnerText;
        }
        latlong = Zip + "," + Convert.ToString(lat) + "," + Convert.ToString(lng) + "," + "" + Zip;
        return latlong;
    }

    public void GetDistance(string origin, string destination)
    {
        string url = @"http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&sensor=false";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        WebResponse response = request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        StreamReader sreader = new StreamReader(dataStream);
        string responsereader = sreader.ReadToEnd();
        response.Close();

        DataSet ds = new DataSet();
        ds.ReadXml(new XmlTextReader(new StringReader(responsereader)));
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables["element"].Rows[0]["status"].ToString() == "OK")
            {
               // lblDuration.Text = ds.Tables["duration"].Rows[0]["text"].ToString();
               var distance = ds.Tables["distance"].Rows[0]["text"].ToString();
            }
        }

    }
}
