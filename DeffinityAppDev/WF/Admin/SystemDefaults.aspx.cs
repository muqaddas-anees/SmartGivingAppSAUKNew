using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Linq;

public partial class ProjectGroups :BasePage
{
    public string SID;
    int OldSID;
    public Database db;
    string stremp = "";
    public System.Data.Common.DbCommand dbCom;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Text = string.Empty;
        //Master.PageHead = Resources.DeffinityRes.SystemDefaults;//"System Defaults";
       // SID=Session["SID"].ToString();
        OldSID = sessionKeys.SID;
        if (!IsPostBack)
        {

            DisplayProjectGroups();
            DisplayProjectCurrency();
            DisplayVal();
        }
    }
    private void DisplayVal()
    {
        try
        {
            int val = 0;
            int ProgSele = 0;
            db = DatabaseFactory.CreateDatabase("DBstring");
           DbCommand cmd = db.GetSqlStringCommand("SELECT * FROM ProjectDefaults");
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                {
                    

                    txtApplicationName.Text = dr["ApplicationName"].ToString();
                    txtCustom1.Text = dr["Custom1"].ToString();
                    txtCustom2.Text = dr["Custom2"].ToString();
                    txtEmailSuffix.Text = dr["EmailSuffix"].ToString();
                    txtenailaddress.Text = dr["OwnerEmail"].ToString();
                    txtURL.Text = dr["WebURL"].ToString();
                    txtProjectPrefix.Text = dr["ProjectReferencePrefix"].ToString();
                    txtmobile.Text = dr["OwnerMobile"].ToString();
                    txtjobdescription.Text = dr["JobDescription"].ToString();
                    txttelephone.Text = dr["OwnerTel"].ToString();
                    txtProjects.Text = dr["Projects"].ToString();
                    ddlDefaultProjectCurrency.SelectedValue = dr["DefaultCurrency"].ToString();
                    ddlprojectdefaults.SelectedValue = dr["ProjectOwnerID"].ToString();
                    txtApproveBoardEmail.Text = dr["ApprovalBoardEmail"].ToString();
                    txtFromEmailAddeess.Text = dr["FromEmail"].ToString();
                    chk_enable.Checked = bool.Parse(dr["Enablejournal"].ToString()) == true ? true : false;
                    DateTime Annaualstart = Convert.ToDateTime(dr["AnnualYearStart"].ToString());
                    DateTime Annaualend = Convert.ToDateTime(dr["AnnualYearEnd"].ToString());
                    txtannualstart.Text = Annaualstart.ToShortDateString();// dr["AnnualYearStart"].ToString(); ;
                    txtannualend.Text = Annaualend.ToShortDateString();// dr["AnnualYearEnd"].ToString(); ;
                    txtVAT.Text = string.Format("{0:F2}", dr["VAT"]);
                    txtFinanceDistributionEmail.Text = dr["FinanceDistributionEmail"].ToString();
                    ddlCulture.SelectedIndex = ddlCulture.Items.IndexOf(ddlCulture.Items.FindByValue(dr["Culture"].ToString()));
                    txtFinancialFromDate.Text = Convert.ToDateTime(dr["FinancialYearStart"].ToString()).ToShortDateString();// dr["AnnualYearStart"].ToString(); ;
                    txtFinancialToDate.Text = Convert.ToDateTime(dr["FinancialYearEnd"].ToString()).ToShortDateString();// dr["AnnualYearEnd"].ToString(); ;
                    val = Convert.ToInt16(string.IsNullOrEmpty(dr["ProjectCancel"].ToString()) ? "0" : dr["ProjectCancel"].ToString());// dr["AnnualYearEnd"].ToString(); ; 
                    if (val == 0)
                    {
                        chkEnablePM.Checked = false;

                    }
                    else
                    {
                        chkEnablePM.Checked = true;
                    }

                    ProgSele = Convert.ToInt16(string.IsNullOrEmpty(dr["ProgSele"].ToString()) ? "0" : dr["ProgSele"].ToString());// dr["AnnualYearEnd"].ToString(); ; 

                    if (ProgSele == 0)
                    {
                        chkEnablePrgSele.Checked = false;

                    }
                    else
                    {
                        chkEnablePrgSele.Checked = true;
                    }

                    txtPOPrefix.Text = dr["RequistionPrefix"].ToString();
                    txtPOStartPoint.Text = dr["RequistionStarting"].ToString();
                    txtIncBy.Text = dr["RequistionIncBy"].ToString();
                }
               // dr.Close();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "select from system defaults ");
        
        }
        
    
    }
    public void DisplayProjectCurrency()
    {
        db = DatabaseFactory.CreateDatabase("DBstring");
            DataSet dbDataSet = new DataSet();
            dbCom = db.GetSqlStringCommand("SELECT ID, CurrencyName, ShortCurrencyName, Display FROM CurrencyList WHERE (Display = N\'Y\') ORDER BY CurrencyName");
            dbCom.CommandType = CommandType.Text;
            dbDataSet = db.ExecuteDataSet(dbCom);
            ddlDefaultProjectCurrency.DataSource = dbDataSet;
            ddlDefaultProjectCurrency.DataTextField = "CurrencyName";
            ddlDefaultProjectCurrency.DataValueField = "ID";
            ddlDefaultProjectCurrency.DataBind();
            ddlDefaultProjectCurrency.Items.Insert(0, "Select...");
       
    }
    public void DisplayProjectGroups()
    {
        try
        {
            IUserRepository<UserMgt.Entity.Contractor> uRepository = new UserRepository<UserMgt.Entity.Contractor>();
            var userList = uRepository.GetAll().Where(o => o.Status.ToLower() == "active" && o.SID == 1).OrderBy(o => o.ContractorName).ToList();
            if (userList != null)
            {
                ddlprojectdefaults.DataSource = userList;
                ddlprojectdefaults.DataTextField = "ContractorName";
                ddlprojectdefaults.DataValueField = "ID";
                ddlprojectdefaults.DataBind();
            }
            ddlprojectdefaults.Items.Insert(0, new ListItem("Please select...", "0"));
            //string strSQL = "";
            //bool _valid=false;
            //try
            //{
            //    if (OldSID == 99)
            //    {
            //        strSQL="Select ContractorName, LoginName, Password, SID, Status, Details, Type, EmailAddress, ID FROM Contractors WHERE  SID in (1,2,5)";
            //        ProjPanel.Visible = true;
            //        _valid=true;
            //    }
            //    else if (OldSID == 1 || OldSID == 2 || OldSID == 5)
            //    {
            //        strSQL = "Select ContractorName, LoginName, Password, SID, Status, Details, Type, EmailAddress, ID FROM Contractors WHERE  SID = " + OldSID.ToString();
            //        ProjPanel.Visible = false;
            //        _valid=true;
            //    }
            //    strSQL = "Select ContractorName, LoginName, Password, SID, Status, Details, Type, EmailAddress, ID FROM Contractors WHERE  SID = 1";
            //    if(_valid)
            //    {
            //        db = DatabaseFactory.CreateDatabase("DBstring");
            //        DataSet dbDataSet = new DataSet();
            //        dbCom = db.GetSqlStringCommand(strSQL);
            //        dbCom.CommandType = CommandType.Text;
            //        dbDataSet = db.ExecuteDataSet(dbCom);
            //        ddlprojectdefaults.DataSource = dbDataSet;
            //        ddlprojectdefaults.DataTextField = "ContractorName";
            //        ddlprojectdefaults.DataValueField = "ID";
            //        ddlprojectdefaults.DataBind();
            //        //ddlprojectdefaults.Items.Insert(0, "Select...");
            //    }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);

        }

    }
    protected void imgbtnsave_Click(object sender, EventArgs e)
    {
        object resultset;
        db = DatabaseFactory.CreateDatabase("DBstring");
        try
        {
            dbCom = db.GetStoredProcCommand("DN_UpdateProjectDefaults");
            dbCom.CommandType = CommandType.StoredProcedure;
            db.AddInParameter(dbCom, "@ProjectOwnerID", DbType.Int32, ddlprojectdefaults.SelectedValue);
            db.AddInParameter(dbCom, "@ProjectOwner", DbType.String, ddlprojectdefaults.SelectedItem.Text);
            db.AddInParameter(dbCom, "@JobDescription", DbType.String, txtjobdescription.Text);
            db.AddInParameter(dbCom, "@OwnerEmail", DbType.String, txtenailaddress.Text);
            db.AddInParameter(dbCom, "@OwnerTel", DbType.String, txttelephone.Text);
            db.AddInParameter(dbCom, "@OwnerMobile", DbType.String, txtmobile.Text);
            db.AddInParameter(dbCom, "@ApplicationName", DbType.String, txtApplicationName.Text);
            db.AddInParameter(dbCom, "@ProjectReferencePrefix", DbType.String, txtProjectPrefix.Text);
            db.AddInParameter(dbCom, "@WebURL", DbType.String, txtURL.Text);
            db.AddInParameter(dbCom, "@DefaultCurrency", DbType.String, ddlDefaultProjectCurrency.SelectedValue);
            db.AddInParameter(dbCom, "@EmailSuffix", DbType.String, txtEmailSuffix.Text);
            db.AddInParameter(dbCom, "@Custom1", DbType.String, txtCustom1.Text);
            db.AddInParameter(dbCom, "@Custom2", DbType.String, txtCustom2.Text);
            db.AddInParameter(dbCom, "@Projects", DbType.Int32, Convert.ToInt32(txtProjects.Text));
            db.AddInParameter(dbCom, "@ApprovalBoardEmail", DbType.String, txtApproveBoardEmail.Text.Trim());
            db.AddInParameter(dbCom, "@FromEmail", DbType.String, txtFromEmailAddeess.Text.Trim());
            db.AddInParameter(dbCom, "@Enablejournal", DbType.Boolean, chk_enable.Checked ? true : false);
            db.AddInParameter(dbCom, "@AnnualYearStart", DbType.DateTime, Convert.ToDateTime(txtannualstart.Text.Trim()));
            db.AddInParameter(dbCom, "@AnnualYearEnd", DbType.DateTime, Convert.ToDateTime(txtannualend.Text.Trim()));
            db.AddInParameter(dbCom, "@VAT", DbType.Double, Convert.ToDouble(string.IsNullOrEmpty(txtVAT.Text.Trim()) ? "0" : txtVAT.Text.Trim()));
            db.AddInParameter(dbCom, "@Culture", DbType.String, ddlCulture.SelectedValue);
            db.AddInParameter(dbCom, "@FinanceDistributionEmail", DbType.String, txtFinanceDistributionEmail.Text.Trim());
            db.AddInParameter(dbCom, "@FinancialYearStart", DbType.DateTime, Convert.ToDateTime(txtFinancialFromDate.Text.Trim()));
            db.AddInParameter(dbCom, "@FinancialYearEnd", DbType.DateTime, Convert.ToDateTime(txtFinancialToDate.Text.Trim()));
            db.AddInParameter(dbCom, "@RequistionPrefix", DbType.String, txtPOPrefix.Text.Trim());
            db.AddInParameter(dbCom, "@RequistionStarting", DbType.Int16, int.Parse(string.IsNullOrEmpty(txtPOStartPoint.Text.Trim()) ? "0" : txtPOStartPoint.Text.Trim()));
            db.AddInParameter(dbCom, "@RequistionIncBy ", DbType.Int16, int.Parse(string.IsNullOrEmpty(txtIncBy.Text.Trim()) ? "0" : txtIncBy.Text.Trim()));

            if (chkEnablePM.Checked == true)
            {
                db.AddInParameter(dbCom, "@EnablePM", DbType.Int16, 1);
            }
            else
            {
                db.AddInParameter(dbCom, "@EnablePM", DbType.Int16, 0);
            }

            if (chkEnablePrgSele.Checked == true)
            {
                db.AddInParameter(dbCom, "@EnableProg", DbType.Int16, 1);
            }
            else
            {
                db.AddInParameter(dbCom, "@EnableProg", DbType.Int16, 0);
            }
            //dbCom = db.GetSqlStringCommand("update ProjectDefaults set EmailSuffix='" + txtEmailSuffix.Text  + "', DefaultCurrency='" + ddlDefaultProjectCurrency.SelectedValue +  "', WebURL='" + txtURL.Text  + "', ApplicationName='" + txtApplicationName.Text  + "', ProjectReferencePrefix='" + txtProjectPrefix.Text + "', ProjectOwner='" + ddlprojectdefaults.SelectedItem.Text + "', ProjectOwnerID='" + ddlprojectdefaults.SelectedValue.ToString() + "', JobDescription='"  +txtjobdescription.Text +  "', OwnerEmail='" + txtenailaddress.Text+ "', OwnerTel='"+ txttelephone.Text+ "', OwnerMobile='" + txtmobile.Text + "' where ID=1");
            //db.ExecuteNonQuery(dbCom);
            resultset = db.ExecuteScalar(dbCom);
            dbCom.Dispose();
            if (Convert.ToInt16(resultset) == 1)
            {
                lblerror.Text = Resources.DeffinityRes.ChkDiffBtwnStDateandEndDate1yr;// "Please check difference between Start Date and End Date must be one year";
                lblerror.ForeColor = System.Drawing.Color.Red;
            }
            else if (Convert.ToInt16(resultset) == 2)
            {
                lblerror.Text = Resources.DeffinityRes.UpdatedSuccessfully;// "updated successfully.";
                lblerror.ForeColor = System.Drawing.Color.Green;
                //clear the instance name
                HttpContext.Current.Application["InstanceTitle"] = null;
            }

            //
            HttpContext.Current.Application["MyUICulture"] = ddlCulture.SelectedValue;
            HttpContext.Current.Application["MyCulture"] = ddlCulture.SelectedValue;
            // remove the cache data
            BaseCache.Cache_Remove(CacheNames.DefaultNames.JournalEnable.ToString());
            //update copy right text
            Deffinity.systemdefaults.UpdateCopyrightText();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Update system defaults table");
        }
    }

    public void Clear()
    {
        lblerror.Text = string.Empty;
        txtjobdescription.Text = string.Empty;
        txttelephone.Text = string.Empty;
        txtmobile.Text = string.Empty;
        txtenailaddress.Text = string.Empty;
        txtProjectPrefix.Text = string.Empty;
        txtEmailSuffix.Text = string.Empty;
        txtURL.Text = string.Empty;
        txtApplicationName.Text = string.Empty;
     
    }
    protected void lnkCurrencyConversion_Click(object sender, EventArgs e)
    {
        Response.Redirect("CurrencyConversion.aspx"); 
    }
    protected void imgbtnApplyToall_Click(object sender, EventArgs e)
    {
        try
        {
            //vt.Allowance_InsertAllowanceByUsers

            db = DatabaseFactory.CreateDatabase("DBstring");
            dbCom = db.GetStoredProcCommand("vt.Allowance_InsertAllowanceByUsers");
            db.ExecuteNonQuery(dbCom);
            dbCom.Dispose();
            lblerror.Text = "Copied previous Annual Leave for All Users successfully";
            lblerror.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
