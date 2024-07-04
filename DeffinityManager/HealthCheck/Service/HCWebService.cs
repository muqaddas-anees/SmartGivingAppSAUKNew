using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using HealthCheckMgt.Entity;
using HealthCheckMgt.BAL;
using HealthCheckMgt.DAL;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;
using AjaxControlToolkit;

/// <summary>
/// Summary description for HCWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class HCWebService : System.Web.Services.WebService {

    public HCWebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }


    [WebMethod(EnableSession=true)]
    public string AddPanel()
    {
        return "Hello World";
    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetForms(string knownCategoryValues, string category)
    {
        HealthCheckBAL hb = new HealthCheckBAL();
        string[] _catgoryValue = knownCategoryValues.Split(':', ';');
        string customerid = (_catgoryValue[1]);

        var x = hb.HealthCheck_Form_SelectByCustomerID(Convert.ToInt32(customerid));
        var result = (from p in x
                      orderby p.FormName
                      select new CascadingDropDownNameValue { value = p.FormID.ToString(), name = p.FormName }).ToArray();
        return result;

    }

    [WebMethod(EnableSession = true)]
    public object ManageFormdata(string formid,string formname,string customerid)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_Form hc = new HealthCheck_Form();
        try
        {
            
            HealthCheckBAL hb = new HealthCheckBAL();
            var cid = Convert.ToInt32(customerid);
            if (cid != null && !string.IsNullOrEmpty(formname))
            {
                //check form name alreay exists
                if (!hb.HealthCheck_Form_NameIsExists(formname, cid))
                {
                    hc.CustomerID = Convert.ToInt32(customerid);
                    hc.FormName = formname;
                    hb.HealthCheck_Form_Add(hc);
                   
                }
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new { hc.CustomerID, hc.FormBackColor, hc.FormID, hc.FormName };
        return jsonSerializer.Serialize(retCls).ToString();
    }
    //
    [WebMethod(EnableSession = true)]
    public object UpdateFormName(string formid, string formname)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_Form hc = new HealthCheck_Form();
        try
        {
            HealthCheckBAL hb = new HealthCheckBAL();
            hc = hb.HealthCheck_Form_SelectByID(Convert.ToInt32(formid));
            if (hc != null)
            {
                //check name already exist or nor 
                hc.FormName = formname;
                hb.HealthCheck_Form_update(hc);
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }

        var retCls = new { hc.CustomerID, hc.FormBackColor, hc.FormID, hc.FormName };

        return jsonSerializer.Serialize(retCls).ToString();
    }
    [WebMethod(EnableSession = true)]
    public object UpdateBackcolor(string formid, string backcolor)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_Form hc = new HealthCheck_Form();
        try
        {
            HealthCheckBAL hb = new HealthCheckBAL();
            hc = hb.HealthCheck_Form_SelectByID(Convert.ToInt32(formid));
            if (hc != null)
            {
                hc.FormBackColor = backcolor;
                hb.HealthCheck_Form_update(hc);
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new { hc.CustomerID, hc.FormBackColor, hc.FormID, hc.FormName };

        return jsonSerializer.Serialize(retCls).ToString();
    }
    [WebMethod(EnableSession = true)]
    public object GetFormdata()
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheckBAL hb = new HealthCheckBAL();
        HealthCheck_Form hc = new HealthCheck_Form();
        try
        {
            var formid = HttpContext.Current.Request.QueryString["fid"].ToString();
            hc = hb.HealthCheck_Form_SelectByID(Convert.ToInt32(formid));
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new { hc.CustomerID, hc.FormBackColor, hc.FormID, hc.FormName };

        return jsonSerializer.Serialize(retCls).ToString();
    }
    //CreatePanel
    [WebMethod(EnableSession = true)]
    public object CreatePanel(string formid, string panelname,string rows, string columns)
    {
        
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormPanel hp = new HealthCheck_FormPanel();
        try
        {
            
            var pnlPs = 1;
            HealthCheckBAL hb = new HealthCheckBAL();
            var cid = Convert.ToInt32(formid);
            //if (cid != null && !string.IsNullOrEmpty(panelname))
            if (cid != null)
            {
                //check form name alreay exists
                if (panelname != "Signature Panel")
                {
                    if (!hb.HealthCheck_FormPanel_NameIsExists(panelname, cid))
                    {
                        using (HealthCheckDataContext hdc = new HealthCheckDataContext())
                        {
                            var PanelDetails = (from a in hdc.HealthCheck_FormPanels
                                                where a.FormID == cid && a.PanelName != "Signature Panel" && a.PanelName != "Header"
                                                orderby a.PnlPosition descending
                                                select a).FirstOrDefault();
                            if (PanelDetails != null)
                            {
                                pnlPs = (int)(PanelDetails.PnlPosition + 1);
                            }
                        }
                        hp.FormID = Convert.ToInt32(formid);
                        hp.PanelName = panelname;
                        hp.PanelRows = Convert.ToInt32(rows);
                        hp.PanelColumns = Convert.ToInt32(columns);
                        hp.PnlPosition = pnlPs;
                        hb.HealthCheck_FormPanel_Add(hp);
                    }
                }
                else
                {
                    hp.FormID = Convert.ToInt32(formid);
                    hp.PanelName = panelname;
                    hp.PanelRows = Convert.ToInt32(rows);
                    hp.PanelColumns = Convert.ToInt32(columns);
                    hp.PnlPosition = pnlPs;
                    hb.HealthCheck_FormPanel_Add(hp);
                }
                if (panelname == "Signature Panel")
                {
                    HealthCheck_FormControl Health_control = null;
                    for (int i = 1; i <=hp.PanelRows; i++)
                    {
                        for (int j = 1; j <= hp.PanelColumns; j++)
                        {
                                using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
                                {
                                    if (j == 1)
                                    {
                                        Health_control = new HealthCheck_FormControl();
                                        Health_control.PanelID = hp.PanelID;
                                        Health_control.TypeOfField = "Textbox";
                                        Health_control.ControlLabelName = "Name:";
                                        Health_control.RowIndex = i;
                                        Health_control.ColumnIndex = j;
                                        Health_control.ControlPosition = "Left";
                                        Health_control.ControlWidth = 70;
                                        Hdc.HealthCheck_FormControls.InsertOnSubmit(Health_control);
                                        Hdc.SubmitChanges();
                                    }
                                    else
                                    {
                                        Health_control = new HealthCheck_FormControl();
                                        Health_control.PanelID = hp.PanelID;
                                        Health_control.TypeOfField = "Textbox";
                                        Health_control.ControlLabelName = "Date:";
                                        Health_control.RowIndex = i;
                                        Health_control.ColumnIndex = j;
                                        Health_control.ControlPosition = "Left";
                                        Health_control.ControlWidth = 15;
                                        Hdc.HealthCheck_FormControls.InsertOnSubmit(Health_control);
                                        Hdc.SubmitChanges();
                                    }
                                }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new { hp.FormID, hp.PanelBackColor, hp.PanelColumns, hp.PanelID, hp.PanelName, hp.PanelRows };

        return jsonSerializer.Serialize(retCls).ToString();
    }
    [WebMethod(EnableSession = true)]
    public object UpdatePanel(string panelid, string panelname)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormPanel hc = new HealthCheck_FormPanel();
        try
        {
            HealthCheckBAL hb = new HealthCheckBAL();
            hc = hb.HealthCheck_FormPanel_SelectByID(Convert.ToInt32(panelid));
            if (hc != null)
            {
                //check name already exist or nor 
                hc.PanelName = panelname;
                hb.HealthCheck_FormPanel_update(hc);
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new { hc.FormID, hc.PanelBackColor, hc.PanelColumns, hc.PanelID, hc.PanelName, hc.PanelRows };
        return jsonSerializer.Serialize(retCls).ToString();
    }
    [WebMethod(EnableSession = true)]
    public object UpdatePanelColor(string panelid, string panelbackcolor)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormPanel hc = new HealthCheck_FormPanel();
        try
        {
            HealthCheckBAL hb = new HealthCheckBAL();
            hc = hb.HealthCheck_FormPanel_SelectByID(Convert.ToInt32(panelid));
            if (hc != null)
            {
                hc.PanelBackColor = panelbackcolor;
                hb.HealthCheck_FormPanel_update(hc);
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new { hc.FormID, hc.PanelBackColor, hc.PanelColumns, hc.PanelID, hc.PanelName, hc.PanelRows };
        return jsonSerializer.Serialize(retCls).ToString();
    }
    [WebMethod(EnableSession=true)]
    public object UpdatePanelName(string panelid, string pname)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormPanel hc = new HealthCheck_FormPanel();
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                hc = (from a in Hdc.HealthCheck_FormPanels where a.PanelID==int.Parse(panelid) select a).FirstOrDefault();
                if (hc != null)
                {
                    hc.PanelName = pname;
                    Hdc.SubmitChanges();
                }
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new {hc.PanelID,hc.PanelName };
        return jsonSerializer.Serialize(retCls).ToString();
    }
    [WebMethod(EnableSession=true)]
    public object GetCallId(string CallId)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormAssignToCall Hac = new HealthCheck_FormAssignToCall();
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                var retcls = Hdc.HealthCheck_FormAssignToCalls.Where(a => a.CallID == int.Parse(CallId)).Select(a => a.FormID).FirstOrDefault();
                return jsonSerializer.Serialize(retcls).ToString();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }
    }
    [WebMethod(EnableSession = true)]
    public object SavePnlPosition(string panelid, string pnlposition, string formid1)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormPanel hc = new HealthCheck_FormPanel();
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                hc = (from a in Hdc.HealthCheck_FormPanels where a.PanelID == int.Parse(panelid) select a).FirstOrDefault();
              
                if (hc != null)
                {
                    if (pnlposition == "Move up")
                    {
                        if (hc.PnlPosition != 1)
                        {
                            int i = (int)hc.PnlPosition;
                            hc.PnlPosition = (i - 1);
                            Hdc.SubmitChanges();

                            List<HealthCheck_FormPanel> x = (from a in Hdc.HealthCheck_FormPanels
                                                             where a.PanelID != int.Parse(panelid) && a.PanelName != "Header" && a.FormID == int.Parse(formid1) &&
                                                            a.PanelName != "Signature Panel" && a.PnlPosition >= hc.PnlPosition
                                                             orderby a.PnlPosition ascending
                                                             select a).ToList();
                            int Index = (int)hc.PnlPosition;
                            foreach (HealthCheck_FormPanel item in x)
                            {
                                Index = Index + 1;
                                item.PnlPosition = Index;
                                Hdc.SubmitChanges();
                            }
                        }
                    }
                    else
                    {
                        int j = (int)hc.PnlPosition;
                        hc.PnlPosition = (j + 1);
                        Hdc.SubmitChanges();
                        List<HealthCheck_FormPanel> y = (from a in Hdc.HealthCheck_FormPanels
                                                         where a.PanelID != int.Parse(panelid) && a.PanelName != "Header" &&
                                                         a.FormID == int.Parse(formid1) &&
                                                        a.PanelName != "Signature Panel" && a.PnlPosition <= hc.PnlPosition
                                                         orderby a.PnlPosition descending
                                                         select a).ToList();
                        int Index1 = (int)hc.PnlPosition;
                        foreach (HealthCheck_FormPanel i1 in y)
                        {
                            Index1 = Index1 - 1;
                            i1.PnlPosition = Index1;
                            Hdc.SubmitChanges();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new { hc.PanelID, hc.PanelName };
        return jsonSerializer.Serialize(retCls).ToString();
    }


    [WebMethod(EnableSession = true)]
    public object GetPaneldata()
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheckBAL hb = new HealthCheckBAL();
        List<HealthCheck_FormPanel> hc = new List<HealthCheck_FormPanel>();
        try
        {
            var formid = HttpContext.Current.Request.QueryString["fid"].ToString();
            hc = hb.HealthCheck_FormPanel_SelectByFormID(Convert.ToInt32(formid));
            var retcls = from h in hc
                         orderby h.PnlPosition ascending
                         select new
                         {
                             h.FormID,
                             h.PanelBackColor,
                             h.PanelColumns,
                             h.PanelID,
                             h.PanelName,
                             h.PanelRows
                         };
            return jsonSerializer.Serialize(retcls).ToString();
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }
    }
    [WebMethod(EnableSession = true)]
    public object GetControldata()
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheckBAL hb = new HealthCheckBAL();
        List<HealthCheck_FormControl> hc = new List<HealthCheck_FormControl>();
        try
        {
            var formid = HttpContext.Current.Request.QueryString["fid"].ToString();
            hc = hb.HealthCheck_FormControl_SelectByFormID(Convert.ToInt32(formid));
            var retcls = from h in hc
                         select new { h.ColumnIndex,h.ControlID,h.ControlLabelName,h.ControlValue,h.DefaultText,h.ListPosition,h.ListValues,h.Mandatory,h.MaxValue,h.MinValue,h.PanelID,h.RowIndex,h.TypeOfField,h.ControlWidth,h.ControlPosition};
            return jsonSerializer.Serialize(retcls).ToString();
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }
    }

    [WebMethod(EnableSession = true)]
    public void InsertPanelPositions(string value)
    {
         int ab = value.Length - 1;
             Array a = value.Split(',');

             int Index = 0;
             foreach (var x in a)
             {
                 //x !=""
                 if (x != string.Empty)
                 {
                     int i = int.Parse(x.ToString());
                     
                     using (HealthCheckDataContext dc = new HealthCheckDataContext())
                     {

                         HealthCheck_FormPanel y = (from c in dc.HealthCheck_FormPanels where c.PanelID == i select c).FirstOrDefault();
                         y.PnlPosition = Index;
                         Index = Index + 1;
                         dc.SubmitChanges();
                     }
                 }
             }
         }
    [WebMethod(EnableSession = true)]
    public object AddPanelControl(string panelid, string typeoffield, string controllablename, string defaulttext, string minvalue, string maxvalue, string listvalue, string mandatory, string controlwidth, string controlposition, string controlHeight, string RbList, string RbCntlList, string ColumnsList, string helptext)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormControl hc = null;
        try
        {
            HealthCheckBAL hb = new HealthCheckBAL();
            var str = panelid.Split('_');
            var pid = str[0];
            var rid = str[1];
            var cid = str[2];
            if (typeoffield == "Table")
            {
                int row = listvalue.Split(',').Length;
                int col = ColumnsList.Split(',').Length;
                for (int i = 1; i <= row; i++)
                {
                    for (int j = 1; j <= col; j++)
                    {
                        hc = new HealthCheck_FormControl();
                        hc.PanelID = Convert.ToInt32(pid);
                        hc.RowIndex = Convert.ToInt32(rid);
                        hc.ColumnIndex = Convert.ToInt32(cid);
                        hc.TypeOfField = typeoffield;
                        hc.ControlLabelName = controllablename;
                        hc.DefaultText = defaulttext;
                        hc.MinValue = minvalue;
                        hc.MaxValue = maxvalue;
                        hc.ListValues = listvalue;
                        hc.Mandatory = Convert.ToBoolean(string.IsNullOrEmpty(mandatory) ? "False" : mandatory);
                        hc.ControlWidth = Convert.ToInt32(string.IsNullOrEmpty(controlwidth) ? "100" : controlwidth);
                        hc.Height = Convert.ToInt32(string.IsNullOrEmpty(controlHeight) ? "100" : controlwidth);
                        hc.ControlPosition = controlposition;
                        hc.CblPosition = RbList;
                        hc.TypeofFieldInTbl = RbCntlList;
                        hc.columnlist = ColumnsList;
                        hc.Helptext = helptext;
                        hb.HealthCheck_FormControl_Add(hc);
                    }
                }
            }
            else
            {
                hc = new HealthCheck_FormControl();
                hc.PanelID = Convert.ToInt32(pid);
                hc.RowIndex = Convert.ToInt32(rid);
                hc.ColumnIndex = Convert.ToInt32(cid);
                hc.TypeOfField = typeoffield;
                hc.ControlLabelName = controllablename;
                hc.DefaultText = defaulttext;
                hc.MinValue = minvalue;
                hc.MaxValue = maxvalue;
                hc.ListValues = listvalue;
                hc.Mandatory = Convert.ToBoolean(string.IsNullOrEmpty(mandatory) ? "False" : mandatory);
                hc.ControlWidth = Convert.ToInt32(string.IsNullOrEmpty(controlwidth) ? "100" : controlwidth);
                hc.Height = Convert.ToInt32(string.IsNullOrEmpty(controlHeight) ? "100" : controlwidth);
                hc.ControlPosition = controlposition;
                hc.CblPosition = RbList;
                hc.TypeofFieldInTbl = RbCntlList;
                hc.columnlist = ColumnsList;
                hc.Helptext = helptext;
                hb.HealthCheck_FormControl_Add(hc);
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new { hc.PanelID, hc.ColumnIndex, hc.ControlID, hc.ControlLabelName, hc.ControlValue, hc.DefaultText, hc.ListPosition, hc.ListValues, hc.Mandatory, hc.MaxValue, hc.RowIndex, hc.TypeOfField, hc.ControlWidth, hc.ControlPosition };
        return jsonSerializer.Serialize(retCls).ToString();

    }
    [WebMethod(EnableSession = true)]
    public object UpdateControl(string panelid, string typeoffield, string controllablename, string defaulttext, string minvalue, string maxvalue, string listvalue, string mandatory, string controlwidth, string controlposition, string controlHeight, string RbList, string RbCntlList, string ColumnsList, string helptext)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormControl hc = null;
        List<HealthCheck_FormControl> hclist = null;
        var retCls = new object();
        try
        {
            HealthCheckBAL hb = new HealthCheckBAL();
            var str = panelid.Split('_');
            var pid = str[0];
            var rid = str[1];
            var cid = str[2];
            if (typeoffield == "Table")
            {
                hclist = new List<HealthCheck_FormControl>();
                using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
                {
                    hclist = Hdc.HealthCheck_FormControls.Where(w => w.PanelID == int.Parse(pid) && w.RowIndex == int.Parse(rid) && w.ColumnIndex == int.Parse(cid)).ToList();
                    foreach (HealthCheck_FormControl h in hclist)
                    {
                        h.TypeOfField = typeoffield;
                        h.ControlLabelName = controllablename;
                        h.DefaultText = defaulttext;
                        h.MinValue = minvalue;
                        h.MaxValue = maxvalue;
                        h.ListValues = listvalue;
                        h.Mandatory = Convert.ToBoolean(string.IsNullOrEmpty(mandatory) ? "False" : mandatory);
                        h.ControlWidth = Convert.ToInt32(string.IsNullOrEmpty(controlwidth) ? "100" : controlwidth);
                        h.Height = Convert.ToInt32(string.IsNullOrEmpty(controlHeight) ? "100" : controlHeight);
                        h.ControlPosition = controlposition;
                        h.CblPosition = RbList;
                        h.TypeofFieldInTbl = RbCntlList;
                        h.columnlist = ColumnsList;
                        h.Helptext = helptext;
                        Hdc.SubmitChanges();
                        retCls = new
                        {
                            h.ColumnIndex,
                            h.ControlID,
                            h.ControlLabelName,
                            h.ControlValue,
                            h.DefaultText,
                            h.ListPosition,
                            h.ListValues,
                            h.Mandatory,
                            h.MaxValue,
                            h.RowIndex,
                            h.TypeOfField,
                            h.ControlWidth,
                            h.ControlPosition
                        };
                    }
                }
            }
            else
            {
                hc = new HealthCheck_FormControl();
                hc = hb.HealthCheck_FormControl_SelectByPanelRowColumn(Convert.ToInt32(pid), Convert.ToInt32(rid), Convert.ToInt32(cid));
                hc.TypeOfField = typeoffield;
                hc.ControlLabelName = controllablename;
                hc.DefaultText = defaulttext;
                hc.MinValue = minvalue;
                hc.MaxValue = maxvalue;
                hc.ListValues = listvalue;
                hc.Mandatory = Convert.ToBoolean(string.IsNullOrEmpty(mandatory) ? "False" : mandatory);
                hc.ControlWidth = Convert.ToInt32(string.IsNullOrEmpty(controlwidth) ? "100" : controlwidth);
                hc.Height = Convert.ToInt32(string.IsNullOrEmpty(controlHeight) ? "100" : controlHeight);
                hc.ControlPosition = controlposition;
                hc.CblPosition = RbList;
                hc.TypeofFieldInTbl = RbCntlList;
                hc.columnlist = ColumnsList;
                hc.Helptext = helptext;
                hb.HealthCheck_FormControl_update(hc);
                retCls = new
                {
                    hc.ColumnIndex,
                    hc.ControlID,
                    hc.ControlLabelName,
                    hc.ControlValue,
                    hc.DefaultText,
                    hc.ListPosition,
                    hc.ListValues,
                    hc.Mandatory,
                    hc.MaxValue,
                    hc.RowIndex,
                    hc.TypeOfField,
                    hc.ControlWidth,
                    hc.ControlPosition
                };
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        return jsonSerializer.Serialize(retCls).ToString();

    }
    [WebMethod(EnableSession = true)]
    public string UploadImage()
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormControl hc = new HealthCheck_FormControl();
        var panelid = HttpContext.Current.Request.QueryString["panelid"].ToString();
        var controlposition = HttpContext.Current.Request.QueryString["controlposition"].ToString();
        var controlwidth = HttpContext.Current.Request.QueryString["controlwidth"].ToString();

        var typeoffield = HttpContext.Current.Request.QueryString["typeoffield"].ToString();

        var helptext = HttpContext.Current.Request.QueryString["helptext"].ToString();

        HttpFileCollection files = HttpContext.Current.Request.Files;
        foreach (string key in files)
        {
            HttpPostedFile file = files[key];
            string fileName = file.FileName;

            var FileLen = file.ContentLength;
            byte[] input = new byte[FileLen];

            // Initialize the stream.
            var MyStream = file.InputStream;

            // Read the file into the byte array.
            MyStream.Read(input, 0, FileLen);

            //byte[] imageBytes = file.InputStream();
            //MemoryStream stream = new MemoryStream(input);
            var newguid = Guid.NewGuid().ToString();
            ImageManager.SaveHC_file(input, newguid);
            //update control value
            HealthCheckBAL hb = new HealthCheckBAL();
            var str = panelid.Split('_');
            var pid = str[0];
            var rid = str[1];
            var cid = str[2];
            hc = hb.HealthCheck_FormControl_SelectByPanelRowColumn(Convert.ToInt32(pid), Convert.ToInt32(rid), Convert.ToInt32(cid));
            if (hc != null)
            {
                hc.ControlPosition = controlposition;
                hc.ControlWidth = Convert.ToInt32(controlwidth);
                hc.Helptext = helptext;
                hc.TypeOfField = typeoffield;
                hc.ControlValue = newguid;
                hb.HealthCheck_FormControl_update(hc);
            }
            else
            {
                hc = new HealthCheck_FormControl();
                hc.TypeOfField = "Image";
                hc.Helptext = helptext;
                hc.ControlValue = newguid;
                hc.PanelID = Convert.ToInt32(pid);
                hc.RowIndex = Convert.ToInt32(rid);
                hc.ColumnIndex = Convert.ToInt32(cid);
                hc.ControlPosition = controlposition;
                hc.ControlWidth = Convert.ToInt32(controlwidth);
                hb.HealthCheck_FormControl_Add(hc);
                //insert data to HealthCheck_FormData

            }
        }
        //var retCls = new { hc.ColumnIndex, hc.ControlID, hc.ControlLabelName, hc.ControlValue, hc.DefaultText, hc.ListPosition, hc.ListValues, hc.Mandatory, hc.MaxValue, hc.RowIndex, hc.TypeOfField };
        //return jsonSerializer.Serialize(retCls).ToString();
        return "Updated";

    }
    //DeletePanel

    //[WebMethod(EnableSession = true)]
    //public string UploadLogo()
    //{
    //    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
    //    HealthCheck_FormControl hc = new HealthCheck_FormControl();
    //    var panelid = HttpContext.Current.Request.QueryString["pid"].ToString();
    //    HttpFileCollection files = HttpContext.Current.Request.Files;
    //    foreach (string key in files)
    //    {
    //        HttpPostedFile file = files[key];
    //        string fileName = file.FileName;

    //        var FileLen = file.ContentLength;
    //        byte[] input = new byte[FileLen];

    //        // Initialize the stream.
    //        var MyStream = file.InputStream;

    //        // Read the file into the byte array.
    //        MyStream.Read(input, 0, FileLen);

    //        //byte[] imageBytes = file.InputStream();
    //        //MemoryStream stream = new MemoryStream(input);
    //        var newguid = Guid.NewGuid().ToString();
    //        ImageManager.SaveHC_file(input, newguid);
    //        //update control value
    //        HealthCheckBAL hb = new HealthCheckBAL();
    //        var str = panelid.Split('_');
    //        var pid = str[0];
    //        var rid = str[1];
    //        var cid = str[2];
    //        hc = hb.HealthCheck_FormControl_SelectByPanelRowColumn(Convert.ToInt32(pid), Convert.ToInt32(rid), Convert.ToInt32(cid));
    //        if (hc != null)
    //        {
    //            hc.ControlValue = newguid;
    //            hb.HealthCheck_FormControl_update(hc);
    //        }
    //        else
    //        {
    //            hc = new HealthCheck_FormControl();
    //            hc.TypeOfField = "FileUpload";
    //            hc.ControlValue = newguid;
    //            hc.PanelID = Convert.ToInt32(pid);
    //            hc.RowIndex = Convert.ToInt32(rid);
    //            hc.ColumnIndex = Convert.ToInt32(cid);
    //            hb.HealthCheck_FormControl_Add(hc);
    //        }
    //    }
    //    //var retCls = new { hc.ColumnIndex, hc.ControlID, hc.ControlLabelName, hc.ControlValue, hc.DefaultText, hc.ListPosition, hc.ListValues, hc.Mandatory, hc.MaxValue, hc.RowIndex, hc.TypeOfField };
    //    //return jsonSerializer.Serialize(retCls).ToString();
    //    return "Updated";

    //}
    //DeletePanel

    [WebMethod(EnableSession = true)]
    public string UploadLogo()
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormControl hc = new HealthCheck_FormControl();
        var panelid = HttpContext.Current.Request.QueryString["pid"].ToString();
        HttpFileCollection files = HttpContext.Current.Request.Files;
        foreach (string key in files)
        {
            HttpPostedFile file = files[key];
            string fileName = file.FileName;

            var FileLen = file.ContentLength;
            byte[] input = new byte[FileLen];

            // Initialize the stream.
            var MyStream = file.InputStream;

            // Read the file into the byte array.
            MyStream.Read(input, 0, FileLen);

            //byte[] imageBytes = file.InputStream();
            //MemoryStream stream = new MemoryStream(input);
            var newguid = Guid.NewGuid().ToString();
            ImageManager.SaveHC_file(input, newguid);
            //update control value
            HealthCheckBAL hb = new HealthCheckBAL();
            var str = panelid.Split('_');
            var pid = str[0];
            var rid = str[1];
            var cid = str[2];
            hc = hb.HealthCheck_FormControl_SelectByPanelRowColumn(Convert.ToInt32(pid), Convert.ToInt32(rid), Convert.ToInt32(cid));
            if (hc != null)
            {
                hc.ControlValue = newguid;
                hb.HealthCheck_FormControl_update(hc);
            }
            else
            {
                hc = new HealthCheck_FormControl();
                hc.TypeOfField = "FileUpload";
                hc.ControlValue = newguid;
                hc.PanelID = Convert.ToInt32(pid);
                hc.RowIndex = Convert.ToInt32(rid);
                hc.ColumnIndex = Convert.ToInt32(cid);
                hb.HealthCheck_FormControl_Add(hc);
            }
        }
        //var retCls = new { hc.ColumnIndex, hc.ControlID, hc.ControlLabelName, hc.ControlValue, hc.DefaultText, hc.ListPosition, hc.ListValues, hc.Mandatory, hc.MaxValue, hc.RowIndex, hc.TypeOfField };
        //return jsonSerializer.Serialize(retCls).ToString();
        return "Updated";

    }
    //DeletePanel

    //[WebMethod (EnableSession=true)]
    //public string UploadLogo()
    //{
    //    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
    //    HealthCheck_FormControl hc = new HealthCheck_FormControl();
    //    var panelid = HttpContext.Current.Request.QueryString["pid"].ToString();
    //    HttpFileCollection files = HttpContext.Current.Request.Files;
    //    foreach (string key in files)
    //    {
    //        HttpPostedFile file = files[key];
    //        string fileName = file.FileName;

    //        var FileLen = file.ContentLength;
    //        byte[] input = new byte[FileLen];

    //        // Initialize the stream.
    //        var MyStream = file.InputStream;

    //        // Read the file into the byte array.
    //        MyStream.Read(input, 0, FileLen);

    //        //byte[] imageBytes = file.InputStream();
    //        //MemoryStream stream = new MemoryStream(input);
    //        var newguid = Guid.NewGuid().ToString();
    //        ImageManager.SaveHC_file(input, newguid);
    //        //update control value
    //        HealthCheckBAL hb = new HealthCheckBAL();
    //        var str = panelid.Split('_');
    //        var pid = str[0];
    //        var rid = str[1];
    //        var cid = str[2];
    //        hc = hb.HealthCheck_FormControl_SelectByPanelRowColumn(Convert.ToInt32(pid), Convert.ToInt32(rid), Convert.ToInt32(cid));
    //        if (hc != null)
    //        {
    //            hc.ControlValue = newguid;
    //            hb.HealthCheck_FormControl_update(hc);
    //        }
    //        else
    //        {
    //            hc = new HealthCheck_FormControl();
    //            hc.TypeOfField = "FileUpload";
    //            hc.ControlValue = newguid;
    //            hc.PanelID = Convert.ToInt32(pid);
    //            hc.RowIndex = Convert.ToInt32(rid);
    //            hc.ColumnIndex = Convert.ToInt32(cid);
    //            hb.HealthCheck_FormControl_Add(hc);
    //        }
    //    }
    //    //var retCls = new { hc.ColumnIndex, hc.ControlID, hc.ControlLabelName, hc.ControlValue, hc.DefaultText, hc.ListPosition, hc.ListValues, hc.Mandatory, hc.MaxValue, hc.RowIndex, hc.TypeOfField };
    //    //return jsonSerializer.Serialize(retCls).ToString();
    //    return "Updated";

    //}
    [WebMethod(EnableSession = true)]
    public object DeletePanel(string panelid)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        try
        {
            HealthCheckBAL hb = new HealthCheckBAL();
            var panelControls = hb.HealthCheck_FormControl_SelectByPanel(Convert.ToInt32(panelid)).ToList();
           // var panelcontrolsData = hb.
            hb.HealthCheck_FormPanel_Delete(panelid);
            
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new { msg="Deleted" };
        return jsonSerializer.Serialize(retCls).ToString();

    }

    //DeleteControl
    [WebMethod(EnableSession = true)]
    public object DeleteControl(string panelid)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        try
        {
            HealthCheckBAL hb = new HealthCheckBAL();
            var str = panelid.Split('_');
            var pid = str[0];
            var rid = str[1];
            var cid = str[2];
            hb.HealthCheck_FormControl_Delete(Convert.ToInt32(pid),Convert.ToInt32(rid),Convert.ToInt32(cid));

        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
        }
        var retCls = new { msg = "Deleted" };
        return jsonSerializer.Serialize(retCls).ToString();

    }
    [WebMethod(EnableSession=true)]
    public object BindPanelName(string panelid)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheck_FormPanel p=new HealthCheck_FormPanel();
        try {
            
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                p = (from a in Hdc.HealthCheck_FormPanels where a.PanelID == int.Parse(panelid) select a).FirstOrDefault();
                var retcls = new {p.PanelID,p.PanelName };
                return jsonSerializer.Serialize(retcls).ToString();
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }

    }
    [WebMethod(EnableSession=true)]
    public object GetTextBoxId(string panelid)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheckBAL hb = new HealthCheckBAL();
        HealthCheck_FormControl h = new HealthCheck_FormControl();
        try
        {
            using(HealthCheckDataContext hdc=new HealthCheckDataContext())
            {
                var retcls = (from a in hdc.HealthCheck_FormPanels
                              join c in hdc.HealthCheck_FormControls on a.PanelID equals c.PanelID
                              where a.FormID == int.Parse(panelid) && a.PanelName == "Signature Panel" && c.ColumnIndex==2
                              orderby c.ControlID descending
                              select new
                              {
                                  CntlID=c.ControlID
                              }).ToList();
            return jsonSerializer.Serialize(retcls).ToString();
            }
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }
    }
    [WebMethod(EnableSession = true)]
    public object GetFormControlData(string panelid)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheckBAL hb = new HealthCheckBAL();
        HealthCheck_FormControl h = new HealthCheck_FormControl();
        try
        {
            // var formid = HttpContext.Current.Request.QueryString["fid"].ToString();
            var str = panelid.Split('_');
            var pid = str[0];
            var rid = str[1];
            var cid = str[2];
            h = hb.HealthCheck_FormControl_SelectByPanel(Convert.ToInt32(pid)).Where(o => o.RowIndex == Convert.ToInt32(rid) && o.ColumnIndex == Convert.ToInt32(cid)).FirstOrDefault();
            var retcls = new
            {
                h.ColumnIndex,
                h.ControlID,
                h.ControlLabelName,
                h.ControlValue,
                h.DefaultText,
                h.ListPosition,
                h.ListValues,
                h.Mandatory,
                h.MaxValue,
                h.MinValue,
                h.PanelID,
                h.RowIndex,
                h.TypeOfField,
                h.ControlWidth,
                h.ControlPosition,
                h.CblPosition,
                h.TypeofFieldInTbl,
                h.columnlist,
                h.Helptext
            };
            return jsonSerializer.Serialize(retcls).ToString();
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }

    }
    [WebMethod(EnableSession = true)]
    public object GetControlValueData(string panelid)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        HealthCheckBAL hb = new HealthCheckBAL();
        HealthCheck_FormData h = new HealthCheck_FormData();
        try
        {
            var str = panelid.Split('_');
            var pid = str[0];
            var rid = str[1];
            var cid = str[2];
            var hcontrol = hb.HealthCheck_FormControl_SelectByPanel(Convert.ToInt32(pid)).Where(o => o.RowIndex == Convert.ToInt32(rid) && o.ColumnIndex == Convert.ToInt32(cid)).FirstOrDefault();
            h = hb.HealthCheck_FormData_SelectAll().Where(o => o.ControlID == hcontrol.ControlID).FirstOrDefault();
            var retcls = new {h.ControlID,h.ControlValue,h.HealthCheckID,h.ID };
            return jsonSerializer.Serialize(retcls).ToString();
        }
        catch (Exception ex)
        {
            //return hc;
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }

    }
}
