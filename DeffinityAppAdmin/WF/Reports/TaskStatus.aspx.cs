
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace WebSamplesCS.WebCharts.Gallery.Gantt
{
    /// <summary>
    /// Summary description for Gantt.
    /// </summary>
    public partial class TaskStatus : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbConnection oleDbConnection1;
        protected System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
        protected WebSamplesCS.WebCharts.ChartData chartData1;
    
        private void Page_Load(object sender, System.EventArgs e)
        {
            this.oleDbConnection1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBstringOLEDB"].ConnectionString;
            //this.oleDbConnection1.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Password="""";User ID=Admin;Data Source=C:\Documents and Settings\All Users\Documents\Infragistics\NetAdvantage for .NET 2007 Vol. 3 CLR 2.0\Samples\ASP.NET\App_Data\ChartDataBase.mdb;Mode=Share Deny None;Extended Properties="""";Jet OLEDB:System database="""";Jet OLEDB:Registry Path="""";Jet OLEDB:Database Password="""";Jet OLEDB:Engine Type=5;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:New Database Password="""";Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:SFP=False";
            this.oleDbDataAdapter1.Fill(this.chartData1);
            this.UltraChart1.Data.DataSource=this.chartData1.GanttData;
            this.UltraChart1.Data.DataBind();
            //this.UltraChart1.BackgroundImageFileName=Server.MapPath("../images/chart_gray_bg.jpg");
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }
        
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {    
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
            this.chartData1 = new WebSamplesCS.WebCharts.ChartData();
            ((System.ComponentModel.ISupportInitialize)(this.chartData1)).BeginInit();
            // 
            // oleDbSelectCommand1
            // 
            //this.oleDbSelectCommand1.CommandText = "SELECT GanttEnd, GanttID, GanttLinkTo, GanttOwner, GanttPercentComplete, GanttSer" +
            //    "ies, GanttStart, GanttTask FROM GanttData";

            this.oleDbSelectCommand1.CommandText = "DEFFINITY_GANTT_TASKS";
            this.oleDbSelectCommand1.CommandType = CommandType.StoredProcedure;
            this.oleDbSelectCommand1.Parameters.AddWithValue("@PROJECTREFERENCE", Convert.ToInt32(Request.QueryString["Project"]));
            this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
            // 
            // oleDbConnection1
            // 

            //this.oleDbConnection1.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Password="""";User ID=Admin;Data Source=C:\Documents and Settings\All Users\Documents\Infragistics\NetAdvantage for .NET 2007 Vol. 3 CLR 2.0\Samples\ASP.NET\App_Data\ChartDataBase.mdb;Mode=Share Deny None;Extended Properties="""";Jet OLEDB:System database="""";Jet OLEDB:Registry Path="""";Jet OLEDB:Database Password="""";Jet OLEDB:Engine Type=5;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:New Database Password="""";Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:SFP=False";
            this.oleDbConnection1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBstringOLEDB"].ConnectionString;
            
            this.oleDbDataAdapter1.SelectCommand = this.oleDbSelectCommand1;
            this.oleDbDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                                                                                                        new System.Data.Common.DataTableMapping("Table", "GanttData", new System.Data.Common.DataColumnMapping[] {
                                                                                                                                                                                                                     new System.Data.Common.DataColumnMapping("GanttSeries", "GanttSeries"),
                                                                                                                                                                                                                     new System.Data.Common.DataColumnMapping("GanttTask", "GanttTask"),
                                                                                                                                                                                                                     new System.Data.Common.DataColumnMapping("GanttStart", "GanttStart"),
                                                                                                                                                                                                                     new System.Data.Common.DataColumnMapping("GanttEnd", "GanttEnd"),
                                                                                                                                                                                                                     new System.Data.Common.DataColumnMapping("GanttID", "GanttID"),
                                                                                                                                                                                                                     new System.Data.Common.DataColumnMapping("GanttLinkTo", "GanttLinkTo"),
                                                                                                                                                                                                                     new System.Data.Common.DataColumnMapping("GanttPercentComplete", "GanttPercentComplete"),
                                                                                                                                                                                                                     new System.Data.Common.DataColumnMapping("GanttOwner", "GanttOwner")})});
             
            // chartData1
            // 
            //this.UltraChart1.GanttChart.ShowCompletePercentages = true;
            this.UltraChart1.GanttChart.ShowOwners = true;
            int i = GetProjectTitle(Convert.ToInt32(Request.QueryString["Project"]));
            i = i * 75;
            this.UltraChart1.Height = Unit.Pixel(i);
            this.UltraChart1.Width = Unit.Pixel(900);
            this.chartData1.DataSetName = "ChartData";
            this.chartData1.Locale = new System.Globalization.CultureInfo("en-Gb");
            this.chartData1.Namespace = "http://www.tempuri.org/ChartData.xsd";
            this.Load += new System.EventHandler(this.Page_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartData1)).EndInit();

        }
        #endregion

        public int GetProjectTitle(int ProjectReference)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_GETPROJDETAILS");
                db.AddInParameter(cmd, "@PROJECTREFERENCE", DbType.Int32, ProjectReference);
                db.AddOutParameter(cmd, "@Project", DbType.String, 150);
                db.AddOutParameter(cmd, "@count", DbType.Int32, 4);
                db.ExecuteNonQuery(cmd);
                string strProj = (string)db.GetParameterValue(cmd, "Project");
                lblmsg.Text = "Project Plan for " + strProj;
                int getVal = (int)db.GetParameterValue(cmd, "count");
                cmd.Dispose();
                return getVal;
            }
            catch (Exception e)
            {
                LogExceptions.LogException(e.Message);
                return 0;
            }
        }
    }
}

 