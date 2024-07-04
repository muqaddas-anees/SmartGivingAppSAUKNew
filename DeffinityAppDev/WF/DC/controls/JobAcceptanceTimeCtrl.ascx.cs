using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class JobAcceptanceTimeCtrl : System.Web.UI.UserControl
    {
        IDCRespository<JobAcceptanceTime> jtRepository = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    jtRepository = new DCRepository<JobAcceptanceTime>();
                    var jEntity = jtRepository.GetAll().FirstOrDefault();
                    if (jEntity != null)
                    {
                        txtFirstReminderTime.Text = jEntity.FirstReminderTime;
                        txtSecondReminderTime.Text = jEntity.SecondReminderTime;
                        txtBackToPoolTime.Text = jEntity.BackToPoolTime;
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void imgbtnupdatetime_Click(object sender, EventArgs e)
        {
            try
            {
                jtRepository = new DCRepository<JobAcceptanceTime>();
                var jEntity = jtRepository.GetAll().FirstOrDefault();
                if (jEntity == null)
                {
                    jEntity = new JobAcceptanceTime();
                    jEntity.FirstReminderTime = txtFirstReminderTime.Text;
                    jEntity.SecondReminderTime = txtSecondReminderTime.Text;
                    jEntity.BackToPoolTime = txtBackToPoolTime.Text;
                    jtRepository.Add(jEntity);
                }
                else
                {
                    jEntity.FirstReminderTime = txtFirstReminderTime.Text;
                    jEntity.SecondReminderTime = txtSecondReminderTime.Text;
                    jEntity.BackToPoolTime = txtBackToPoolTime.Text;
                    jtRepository.Edit(jEntity);
                }
                lblsuccess.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}