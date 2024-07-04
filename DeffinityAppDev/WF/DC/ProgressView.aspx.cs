using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCBal= DC.BAL;
using DCBll = DC.BLL;

namespace DeffinityAppDev.WF.DC
{
    public partial class ProgressView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (sessionKeys.Message.Length > 0)
                    {
                        DeffinityManager.ShowMessages.ShowSuccessAlert(this.Page, sessionKeys.Message, "OK");
                        sessionKeys.Message = string.Empty;
                    }


                    BindData();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void BindData()
        {
            IPortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile>();

            IPortfolioRepository<PortfolioMgt.Entity.FundraisersProgress> pRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersProgress>();
            var pList = pRep.GetAll().Where(o => o.CallID == QueryStringValues.CallID).OrderBy(o=>o.LoggedOn).ToList();
            var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
            if(pList.Count() >0)
            {
                var rList = (from p in pList
                              orderby p.ID descending
                              select new
                              {
                                  p.Title,
                                  p.Notes,
                                  p.LoggedOn,
                                  p.UserID,
                                  UserName = (ulist.Where(o => o.ID == p.UserID).FirstOrDefault()?.CompanyName ?? ""),
                                  p.CallID,
                                  p.FundUNID,
                                  Files = fRep.GetAll().Where(o=>o.ProgressId == p.ID).ToList()

                              }).ToList();

                list_progress.DataSource = rList;
                list_progress.DataBind();
            }
        }

        public string showFile(string fileunid)
        {
            string retval = "";

            IPortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile>();

            var f = fRep.GetAll().Where(o => o.FileUNID == fileunid).FirstOrDefault();

            if (f != null)
            {
                var ext = Path.GetExtension(f.FileName);
                string[] exts = { ".jpg", ".jpeg", ".png", ".gif" };
                if (exts.Contains(ext.ToLower()))
                {
                    retval = string.Format("<div class='overlay-wrapper'><img src='../../wf/uploaddata/progress/{0}' class='rounded w-150px'/></div>", f.FileUNID + ext);

                }
                else
                {
                    retval = string.Format("<a href='filedownloader.ashx?unid={0}' class='fs-6 text-hover-primary fw-bold'> {1} </a>", f.FileUNID,f.FileName);
                }

            }



            return retval;
        }

        protected void btnAddProgress_Click(object sender, EventArgs e)
        {
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            huid.Value = "0";
            mdl.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.FundraisersProgress> pRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersProgress>();
                IPortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile>();
                var fDetails = DCBll.FLSDetailsBAL.SelectbyId(QueryStringValues.CallID);
                if (huid.Value == "0")
                {

                    var h = new PortfolioMgt.Entity.FundraisersProgress();
                    h.CallID = QueryStringValues.CallID;
                    h.FundUNID = fDetails.UNID;
                    h.LoggedOn = DateTime.Now;
                    h.Notes = txtDescription.Text;
                    h.Title = txtTitle.Text;
                    h.UserID = sessionKeys.UID;
                    pRep.Add(h);
                    UploadFIles(h.ID);

                    sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;

                    Response.Redirect(Request.RawUrl,false);
                    //save file 

                }
                else
                {
                    var h = pRep.GetAll().Where(o => o.ID == Convert.ToInt32(huid.Value)).FirstOrDefault();
                    if(h != null)
                    {
                        h.LoggedOn = DateTime.Now;
                        h.Notes = txtDescription.Text;
                        h.Title = txtTitle.Text;
                        h.UserID = sessionKeys.UID;
                        pRep.Edit(h);
                        UploadFIles(h.ID);
                        sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                        Response.Redirect(Request.RawUrl, false);
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void UploadFIles(int progressid)
        {
            string[] files;
            int numFiles;

            var folder = Server.MapPath(string.Format("~/WF/UploadData/Progress/"));
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);

            }
            //files = System.IO.Directory.GetFiles(dirFullPath);
            var filesList = Pfiles.PostedFiles;
            numFiles = filesList.Count;
            numFiles = numFiles + 1;
            string str_image = "";
          
            for (int i = 0; i < filesList.Count; i++)
            {
                var f = filesList[i];

                string fileName = f.FileName;
                string fileExtension = f.ContentType;

                if (!string.IsNullOrEmpty(fileName))
                {

                    // var u = UserMgt.BAL.ContractorsBAL.Contractor_SelectByID(Convert.ToInt32( uid));
                    fileExtension = Path.GetExtension(fileName);
                    //str_image = String.Format("Lien Release-upload-{0}-{1}-{2}{3}", p.ProjectTitle, r.ItemTitle, "", fileExtension);// "LeanReleaseDocument" + fileExtension;
                    str_image = String.Format("{0}", fileName);


                    IPortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersProgressFile>();
                    var fNew = new PortfolioMgt.Entity.FundraisersProgressFile() { ProgressId = progressid, DateLogged = DateTime.Now, FileName = str_image, FileUNID = Guid.NewGuid().ToString() };
                    fRep.Add(fNew);
                    string pathToSave_100 = HttpContext.Current.Server.MapPath("~/WF/UploadData/Progress/" + fNew.FileUNID + fileExtension);
                    f.SaveAs(pathToSave_100);
                }

            }
        }

        protected void list_progress_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected void list_progress_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }
    }
}