using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthCheckMgt.Entity;
using HealthCheckMgt.DAL;

namespace HealthCheckMgt.BAL
{
    /// <summary>
    /// Summary description for HealthCheckBAL
    /// </summary>
    
    public class HealthCheckBAL
    {
        IHCRepository<HealthCheck_Form> hf_repository = null;
        IHCRepository<HealthCheck_FormPanel> hfpanel_repository = null;
        IHCRepository<HealthCheck_FormControl> hfcontrol_repository = null;
        IHCRepository<HealthCheck_FormData> hfdata_repository = null;
        IHCRepository<HealthCheckList> hclist_repository = null;
        IHCRepository<HealthCheckNameMailID> hcmail_respository = null;
        IHCRepository<HealthCheck_FormAssignToCall> hcassign_respository = null;
        IHCRepository<AppManager> appManager_respository = null;
        IHCRepository<AppManagerAssignedForm> appManagerAssignedForm_respository = null;
       
        public HealthCheckBAL()
        {
            hclist_repository = new HCRepository<HealthCheckList>();
            hf_repository = new HCRepository<HealthCheck_Form>();
            hfpanel_repository = new HCRepository<HealthCheck_FormPanel>();
            hfcontrol_repository = new HCRepository<HealthCheck_FormControl>();
            hfdata_repository = new HCRepository<HealthCheck_FormData>();
            hcmail_respository = new HCRepository<HealthCheckNameMailID>();
            hcassign_respository = new HCRepository<HealthCheck_FormAssignToCall>();
            appManager_respository = new HCRepository<AppManager>();
            appManagerAssignedForm_respository = new HCRepository<AppManagerAssignedForm>();
        }

        public  HealthCheckList HealthCheckList_SelectByID(int HealthcheckID)
        {
           return hclist_repository.GetAll().Where(o => o.ID == HealthcheckID).FirstOrDefault();
        }

        #region Health Check Form to Call
        public HealthCheck_FormAssignToCall HealthCheck_FormAssignToCall_Add(HealthCheck_FormAssignToCall hm)
        {
            
            hcassign_respository.Add(hm);
            return hm;
        }
        public HealthCheck_FormAssignToCall HealthCheck_FormAssignToCall_Update(HealthCheck_FormAssignToCall hm)
        {
            hcassign_respository.Edit(hm);
            return hm;
        }
        public HealthCheck_FormAssignToCall HealthCheck_FormAssignToCall_SelectByID(int id)
        {
            return hcassign_respository.GetAll().Where(o => o.ID == id).FirstOrDefault();
        }
        public HealthCheck_FormAssignToCall HealthCheck_FormAssignToCall_SelectByCallID(int CallID)
        {
            return hcassign_respository.GetAll().Where(o => o.CallID == CallID).FirstOrDefault();
        }
        public List<HealthCheck_FormAssignToCall> HealthCheck_FormAssignToCall_SelectByFormID(int formid)
        {
            return hcassign_respository.GetAll().Where(o => o.FormID == formid).ToList();
        }
        public void HealthCheck_FormAssignToCall_DeleteByID(int id)
        {
            var hm = HealthCheck_FormAssignToCall_SelectByID(id);
            if (hm != null)
                hcassign_respository.Delete(hm);
        }
        public void HealthCheck_FormAssignToCall_DeleteByCallID(int CallID)
        {
            var hm = HealthCheck_FormAssignToCall_SelectByCallID(CallID);
            if (hm != null)
                hcassign_respository.Delete(hm);
        }
        #endregion

        #region Health Check Mail ids
        public HealthCheckNameMailID HealthCheckMail_Add(HealthCheckNameMailID hm)
        {
            hcmail_respository.Add(hm);
            return hm;
        }
        public HealthCheckNameMailID HealthCheckMail_Update(HealthCheckNameMailID hm)
        {
            hcmail_respository.Edit(hm);
            return hm;
        }
        public HealthCheckNameMailID HealthCheckMail_SelectByID(int id)
        {
          return  hcmail_respository.GetAll().Where(o => o.ID== id).FirstOrDefault();
        }
        public List<HealthCheckNameMailID> HealthCheckMail_SelectByFormID(int formid)
        {
            return hcmail_respository.GetAll().Where(o => o.PortfolioHealthCheckID == formid).ToList();
        }
        public void HealthCheckMail_DeleteByID(int id)
        {
            var hm = HealthCheckMail_SelectByID(id);
            if (hm != null)
                hcmail_respository.Delete(hm);
        }
        #endregion

        #region Health Check Form
        public HealthCheck_Form HealthCheck_Form_Add(HealthCheck_Form hf)
        {
            hf_repository.Add(hf);
            return hf;
        }

        public HealthCheck_Form HealthCheck_Form_update(HealthCheck_Form hf)
        {
            hf_repository.Edit(hf);
            return hf;
        }

        public HealthCheck_Form HealthCheck_Form_SelectByID(int formID)
        {
           return hf_repository.GetAll().Where(o=>o.FormID == formID).FirstOrDefault();
        }

        public List<HealthCheck_Form> HealthCheck_Form_SelectByCustomerID(int CustomerID)
        {
            return hf_repository.GetAll().Where(o => o.CustomerID == CustomerID).ToList();
        }
        public List<HealthCheck_Form> HealthCheck_Form_SelectAll()
        {
            return hf_repository.GetAll().ToList();
        }

        public HealthCheck_Form HealthCheck_Form_update_FormName(int formid, string formName)
        {
            var hf = HealthCheck_Form_SelectByID(formid);
            if (hf != null)
            {
                hf.FormName = formName;
                hf_repository.Edit(hf);
            }
            return hf;
        }

        public HealthCheck_Form HealthCheck_Form_update_BackColor(int formid, string backColor)
        {
            var hf = HealthCheck_Form_SelectByID(formid);
            if (hf != null)
            {
                hf.FormBackColor = backColor;
                hf_repository.Edit(hf);
            }
            return hf;
        }

        public bool HealthCheck_Form_NameIsExists(string formname,int customerID)
        {
            var cnt = hf_repository.GetAll().Where(o => o.FormName.ToLower() == formname.ToLower() && o.CustomerID == customerID).Count();
            if (cnt > 0)
                return true;
            else
                return false;
        }
        //public void HealthCheck_Form_Delete(string formid)
        //{
        //    var hc = HealthCheck_Form_SelectByID(Convert.ToInt32(formid));
        //    var hcpanel = hc.HealthCheck_FormPanels.ToList();
        //    if (hcpanel.Count() > 0)
        //    {
        //        foreach (var hp in hcpanel)
        //        {
        //            var hc_controls = hp.HealthCheck_FormControls.ToList();
        //            var hc_controlsids = hc_controls.Select(o => o.ControlID).ToArray();
        //            var d = hfcontrol_repository.GetAll().Where(o => hc_controlsids.Contains(o.ControlID)).ToList();
        //            hfcontrol_repository.DeleteAll(d);
        //            //foreach(var hcon in hc_controls)
        //            //{
        //            //    var d= hfcontrol_repository.GetAll().Where(o => o.ControlID == hcon.ControlID).FirstOrDefault();
        //            //    hfcontrol_repository.Delete(d);
        //            //}
        //            //hfcontrol_repository.DeleteAll(hc_controls);
        //        }

        //        var hp_ids = hcpanel.Select(o => o.PanelID).ToArray();
        //        var hp_list = hfpanel_repository.GetAll().Where(o => hp_ids.Contains(o.PanelID)).ToList();
        //        hfpanel_repository.DeleteAll(hp_list);
        //    }
        //    hf_repository.Delete(hc);
        //}
        public void HealthCheck_Form_Delete(string formid)
        {
            var hc = hf_repository.GetAll().Where(o => o.FormID == Convert.ToInt32(formid)).FirstOrDefault();

            if (hc != null)
            {
                var hcpanel = hfpanel_repository.GetAll().Where(o=>o.FormID == Convert.ToInt32(formid)).ToList();

                if (hcpanel != null)
                {
                    if (hcpanel.Count > 0)
                    {
                        //var hc = HealthCheck_FormPanel_SelectByID(Convert.ToInt32(panelid));
                        //select list of controls
                        var hcctrls = hfcontrol_repository.GetAll().Where(o => hcpanel.Select(p => p.PanelID).ToArray().Contains(o.PanelID.HasValue ? o.PanelID.Value : 0)).ToList();

                        //Check delete controls
                        if (hcctrls != null)
                        {
                            if (hcctrls.Count > 0)
                            {
                                var hcctrlData = hfdata_repository.GetAll().Where(o => hcctrls.Select(c => c.ControlID).ToArray().Contains(o.ControlID.HasValue ? o.ControlID.Value : 0)).ToList();
                                if (hcctrlData.Count > 0)
                                {
                                    //delete HC control data 
                                    hfdata_repository.DeleteAll(hcctrlData);
                                }
                                //Delete controls
                                hfcontrol_repository.DeleteAll(hcctrls);
                            }
                        }
                        //Delete controls Data
                        hfpanel_repository.DeleteAll(hcpanel);
                    }
                }

                //delete refernce forms 
               
                //delete from service desk
                var assignedCalls = hcassign_respository.GetAll().Where(o => o.FormID == hc.FormID).ToList();
                if (assignedCalls != null)
                {
                    hcassign_respository.DeleteAll(assignedCalls);
                }
                //Delete from new 
                var appmanager = appManager_respository.GetAll().Where(o => o.FormID == hc.FormID).ToList();
                {
                    if(appmanager != null)
                    {
                        if(appmanager.Count() >0)
                        {
                            //Delete app forms
                            var appmanagerForms = appManagerAssignedForm_respository.GetAll().Where(o=>appmanager.Select(p=>p.ID).ToArray().Contains(o.AppID.HasValue?o.AppID.Value:0)).ToList();
                            if (appmanagerForms != null)
                            {
                                appManagerAssignedForm_respository.DeleteAll(appmanagerForms);
                            }
                            //delete app manager
                            appManager_respository.DeleteAll(appmanager);
                        }
                    }
                }

                hf_repository.Delete(hc);
            }
        }
        #endregion

        #region Health Check Form Control
        public HealthCheck_FormPanel HealthCheck_FormPanel_Add(HealthCheck_FormPanel hf)
        {
            hfpanel_repository.Add(hf);
            return hf;
        }

        public HealthCheck_FormPanel HealthCheck_FormPanel_update(HealthCheck_FormPanel hf)
        {
            hfpanel_repository.Edit(hf);
            return hf;
        }

        public HealthCheck_FormPanel HealthCheck_FormPanel_SelectByID(int panelID)
        {
            return hfpanel_repository.GetAll().Where(o => o.PanelID == panelID).FirstOrDefault();
        }

        public List<HealthCheck_FormPanel> HealthCheck_FormPanel_SelectByFormID(int formID)
        {
            return hfpanel_repository.GetAll().Where(o => o.FormID == formID).ToList();
        }
        public List<HealthCheck_FormPanel> HealthCheck_FormPanel_SelectAll()
        {
            return hfpanel_repository.GetAll().ToList();
        }

        public HealthCheck_FormPanel HealthCheck_FormPanel_update_PanelName(int panelid, string panelName)
        {
            var hf = HealthCheck_FormPanel_SelectByID(panelid);
            if (hf != null)
            {
                hf.PanelName = panelName;
                hfpanel_repository.Edit(hf);
            }
            return hf;
        }

        public HealthCheck_FormPanel HealthCheck_FormPanel_update_BackColor(int panelid, string backColor)
        {
            var hf = HealthCheck_FormPanel_SelectByID(panelid);
            if (hf != null)
            {
                hf.PanelBackColor = backColor;
                hfpanel_repository.Edit(hf);
            }
            return hf;
        }

        public bool HealthCheck_FormPanel_NameIsExists(string panelname, int formID)
        {
            if (!string.IsNullOrEmpty(panelname.Trim()))
            {
                var cnt = hfpanel_repository.GetAll().Where(o => o.PanelName.ToLower() == panelname.ToLower() && o.FormID == formID).Count();
                if (cnt > 0)
                    return true;
                else
                    return false;
            }
            else
            return false;
        }
        public HealthCheck_FormPanel HealthCheck_FormPanel_ByName(string panelname, int formID)
        {
          return  hfpanel_repository.GetAll().Where(o => o.PanelName.ToLower() == panelname.ToLower() && o.FormID == formID).FirstOrDefault();
        }
        public void HealthCheck_FormPanel_Delete(string panelid)
        {
            var hc = HealthCheck_FormPanel_SelectByID(Convert.ToInt32(panelid));
            //select list of controls
            var hcctrls = hfcontrol_repository.GetAll().Where(o => o.PanelID == Convert.ToInt32(panelid)).ToList();

            //Check delete controls
            if(hcctrls != null)
            {
               if( hcctrls.Count >0)
               {
                   var hcctrlData = hfdata_repository.GetAll().Where(o => hcctrls.Select(c => c.ControlID).ToArray().Contains(o.ControlID.HasValue ? o.ControlID.Value : 0)).ToList();
                   if(hcctrlData.Count >0)
                   {
                       //delete HC control data 
                       hfdata_repository.DeleteAll(hcctrlData);
                   }
                   //Delete controls
                   hfcontrol_repository.DeleteAll(hcctrls);
               }

            }
            
            //Delete controls Data
            hfpanel_repository.Delete(hc);

        }
        #endregion

        #region Health Check Controls
        public HealthCheck_FormControl HealthCheck_FormControl_Add(HealthCheck_FormControl hc)
        {
            hfcontrol_repository.Add(hc);
            return hc;
        }

        public HealthCheck_FormControl HealthCheck_FormControl_update(HealthCheck_FormControl hf)
        {
            hfcontrol_repository.Edit(hf);
            return hf;
        }

       

        public HealthCheck_FormControl HealthCheck_FormControl_SelectByID(int controlID)
        {
            return hfcontrol_repository.GetAll().Where(o => o.ControlID == controlID).FirstOrDefault();
        }

        public List<HealthCheck_FormControl> HealthCheck_FormControl_SelectByPanel(int panelID)
        {
            return hfcontrol_repository.GetAll().Where(o => o.PanelID == panelID).ToList();
        }

        public List<HealthCheck_FormControl> HealthCheck_FormControl_SelectByFormID(int formID)
        {
            return hfcontrol_repository.GetAll().Where(o => o.HealthCheck_FormPanel.FormID == formID).ToList();
        }

        public List<HealthCheck_FormControl> HealthCheck_FormControl_SelectAll()
        {
            return hfcontrol_repository.GetAll().ToList();
        }
        public HealthCheck_FormControl HealthCheck_FormControl_SelectByPanelRowColumn(int panelid,int rowid,int columnid)
        {
            return hfcontrol_repository.GetAll().Where(o => o.PanelID == panelid & o.RowIndex == rowid && o.ColumnIndex == columnid).FirstOrDefault();
        }
        public void HealthCheck_FormControl_Delete(int panelid, int rowid, int columnid)
        {
            var hc = HealthCheck_FormControl_SelectByPanelRowColumn(panelid, rowid,columnid);
            hfcontrol_repository.Delete(hc);

        }
        public void HealthCheck_FormControl_DeleteByPanelid(int panelid, int rowid, int columnid)
        {
            var hc = HealthCheck_FormControl_SelectAll().Where(o=>o.PanelID == panelid);
           // hfcontrol_repository.DeleteAll(hc);

        }
        #endregion

        #region Health Check Control Data
        public HealthCheck_FormData HealthCheck_FormData_Add(HealthCheck_FormData hc)
        {
            hfdata_repository.Add(hc);
            return hc;
        }

        public HealthCheck_FormData HealthCheck_FormData_update(HealthCheck_FormData hf)
        {
            hfdata_repository.Edit(hf);
            return hf;
        }
        public List<HealthCheck_FormData> HealthCheck_FormData_SelectByHealthCheckID(int healthCheckID)
        {
            return hfdata_repository.GetAll().Where(o => o.HealthCheckID == healthCheckID).ToList<HealthCheck_FormData>();
        }
        public List<HealthCheck_FormData> HealthCheck_FormData_SelectByHealthCheckID(int healthCheckID,string Section)
        {
            return hfdata_repository.GetAll().Where(o => o.HealthCheckID == healthCheckID && o.Section.ToLower() == Section.ToLower()).ToList<HealthCheck_FormData>();
        }
        public HealthCheck_FormData HealthCheck_FormData_SelectByID(int id)
        {
            return hfdata_repository.GetAll().Where(o => o.ID == id).FirstOrDefault();
        }
        public HealthCheck_FormData HealthCheck_FormData_SelectByControl(int controlid)
        {
            return hfdata_repository.GetAll().Where(o => o.ControlID == controlid).FirstOrDefault();
        }
        public HealthCheck_FormData HealthCheck_FormData_SelectByControl(int controlid,string Section)
        {
            return hfdata_repository.GetAll().Where(o => o.ControlID == controlid && o.Section.ToLower() == Section.ToLower()).FirstOrDefault();
        }
        //public List<HealthCheck_FormData> HealthCheck_FormData_SelectByControl(int controlID)
        //{
        //    return hfdata_repository.GetAll().Where(o => o.ControlID == controlID).ToList();
        //}
        public List<HealthCheck_FormData> HealthCheck_FormData_SelectAll()
        {
            return hfdata_repository.GetAll().ToList();
        }
        #endregion

        #region copy to all customers
        public bool FormCopyToCustomer(int formid, int CustomerID)
        {

            bool retval = false;
            try
            {
                //Get the form data
                var cForm = HealthCheck_Form_SelectByID(formid);
                //Check form name is exists for customer
                if (!HealthCheck_Form_NameIsExists(cForm.FormName, CustomerID))
                {
                    //insert form data
                    var nForm = HealthCheck_Form_Add(new HealthCheck_Form() { CustomerID = CustomerID, FormBackColor = cForm.FormBackColor, FormName = cForm.FormName });
                    if (nForm != null)
                    {
                        var newFormid = nForm.FormID;
                        //get panels of the form
                        var cpanels = HealthCheck_FormPanel_SelectByFormID(formid);// cForm.HealthCheck_FormPanels.ToList();
                        foreach (var cpanel in cpanels)
                        {
                            //insert new panel
                            var npanel = HealthCheck_FormPanel_Add(new HealthCheck_FormPanel() { FormID = newFormid, PanelBackColor = cpanel.PanelBackColor, PanelColumns = cpanel.PanelColumns, PanelName = cpanel.PanelName, PanelRows = cpanel.PanelRows });
                            if (npanel != null)
                            {
                                var newpanelid = npanel.PanelID;
                                //get control value
                                var cControls = HealthCheck_FormControl_SelectByPanel(cpanel.PanelID); //cpanel.HealthCheck_FormControls.ToList();
                                if (cControls != null)
                                {
                                    foreach (var cControl in cControls)
                                    {
                                        var ncontrol = HealthCheck_FormControl_Add(new HealthCheck_FormControl() { PanelID = newpanelid, ColumnIndex = cControl.ColumnIndex, ControlLabelName = cControl.ControlLabelName, ControlPosition = cControl.ControlPosition, ControlValue = cControl.ControlValue, ControlWidth = cControl.ControlWidth, DefaultText = cControl.DefaultText, ListPosition = cControl.ListPosition, ListValues = cControl.ListValues, Mandatory = cControl.Mandatory, MaxValue = cControl.MaxValue, MinValue = cControl.MinValue, RowIndex = cControl.RowIndex, TypeOfField = cControl.TypeOfField });

                                    }
                                }
                            }
                        }

                    }
                    retval = true;
                }
                else
                {
                    retval = false;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            
            return retval;
        }

        #endregion
    }
}