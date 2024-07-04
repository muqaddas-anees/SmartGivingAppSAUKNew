using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin
{
    public partial class Training : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                    ModuleDescription();

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void ModuleDescription()
        {
            try
            {
                var c = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();
                CKEditor1.Text = Server.HtmlDecode(c.TrainingDescription);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindGrid()
        {
            try
            {
                var mlist = PortfolioMgt.BAL.PortfolioTrainingBAL.PortfolioTrainingBAL_TrainingSelect();
                GridTraining.DataSource = mlist;
                GridTraining.DataBind();
                if(mlist.Count ==0)
                {
                    AddNewPopup();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridTraining_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editmodule")
                {
                    huid.Value = e.CommandArgument.ToString();
                    var m = PortfolioMgt.BAL.PortfolioTrainingBAL.PortfolioTrainingBAL_TrainingSelect().Where(o => o.TrainingID == Convert.ToInt32(huid.Value)).FirstOrDefault();
                    if (m != null)
                    {
                        txtTrainingName.Text = m.TrainingName;
                        txtDescription.Text = m.TrainingDescription;
                        txtAmount.Text = String.Format("{0:F2}", (m.Amount.HasValue?m.Amount.Value:0));
                        txtImage.Text = m.TrainingImage;
                        mdlManageOptions.Show();
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var moduleid = Convert.ToInt32(huid.Value);

                if (huid.Value == "0")
                {
                    var p = new PortfolioMgt.Entity.PortfolioTraining();
                    p.TrainingName = txtTrainingName.Text.Trim();
                    p.TrainingDescription = txtDescription.Text.Trim();
                    p.TrainingImage = txtImage.Text.Trim();
                    p.Amount = Convert.ToDouble(txtAmount.Text.Trim());
                    PortfolioMgt.BAL.PortfolioTrainingBAL.PortfolioTrainingBAL_TrainingAdd(p);
                    lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;
                    ClearFields();
                    mdlManageOptions.Hide();
                    BindGrid();
                }
                else
                {
                    var p = PortfolioMgt.BAL.PortfolioTrainingBAL.PortfolioTrainingBAL_TrainingSelect().Where(o => o.TrainingID == Convert.ToInt32(huid.Value)).FirstOrDefault();
                    if (p != null)
                    {
                        p.TrainingName = txtTrainingName.Text.Trim();
                        p.TrainingDescription = txtDescription.Text.Trim();
                        p.TrainingImage = txtImage.Text.Trim();
                        p.Amount = Convert.ToDouble(txtAmount.Text.Trim());
                        PortfolioMgt.BAL.PortfolioTrainingBAL.PortfolioTrainingBAL_TrainingUpdate(p);
                        lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                        ClearFields();
                        BindGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnUpdateDescription_Click(object sender, EventArgs e)
        {
            try
            {
                var c = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();

                if (c != null)
                {
                    c.TrainingDescription = Server.HtmlEncode(CKEditor1.Text);
                    UserMgt.BAL.CompanyBAL.CompanyBAL_Update(c);
                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewPopup();
        }

        private void AddNewPopup()
        {
            huid.Value = "0";
            ClearFields();
            mdlManageOptions.Show();
        }

        private void ClearFields()
        {
            txtAmount.Text = "0.00";
            txtDescription.Text = string.Empty;
            txtImage.Text = string.Empty;
            txtTrainingName.Text = string.Empty;
        }
    }
}