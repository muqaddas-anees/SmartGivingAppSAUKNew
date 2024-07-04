using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataTables;
using System.Web;
using System.Dynamic;
using PortfolioMgt.Entity;
using System.Web.Security;
using UserMgt.DAL;
using PortfolioMgt.DAL;
using DC.BLL;
using System.Web.Script.Serialization;
using PortfolioMgt.BAL;
using PortfolioMgt.BLL;
using DataTables;
using System.IO;
using DC.Entity;

namespace DeffinityAppDev.Controllers
{
    public class ContactController : ApiController
    {

        [Route("api/BindCallAssets")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult BindCallAssets()
        {

            var request = HttpContext.Current.Request;

            var draw = request.Form.GetValues("draw").FirstOrDefault();
            var start = request.Form.GetValues("start").FirstOrDefault();
            var length = request.Form.GetValues("length").FirstOrDefault();
            string id = "0"; string sid = "0";
            if (request.Form["action"] == null)
            {
                if(request.Form["id"] != null)
                id = request.Form["id"].ToString();
                if(request.Form["sid"] != null)
                sid = request.Form["sid"].ToString();
            }
           
            
            JavaScriptSerializer Jsonserializer = new JavaScriptSerializer();
            try
            {
                List<Jqgrid> vlist = new List<Jqgrid>();
                if (id == "0" && sid == "0")
                {
                    string[] s = new[] { "Cancelled", "Closed" };
                    vlist = FLSDetailsBAL.Jqgridlist().Where(o => !s.Contains(o.Status)).OrderByDescending(o => o.CCID).ToList();
                }


                else
                    vlist = FLSDetailsBAL.Jqgridlist().Where(o => o.RequesterID == Convert.ToInt32(id)).ToList();
                //IDCRespository<DC.Entity.V_CallToAsset> pRepository = new DCRepository<DC.Entity.V_CallToAsset>();

                //var clist = pRepository.GetAll().Where(o => o.RequesterID == Convert.ToInt32(id)).ToList();

                var rlist = (from r in vlist
                             orderby r.CCID descending
                             select new
                             {
                                 ID = r.CallID,
                                 LoggedDate = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToDateTime(r.LoggedDateTime)),
                                 MakeName = string.Empty,
                                 ModelName = string.Empty,
                                 PolicyType = string.Empty,
                                 RequesterID = r.RequesterID,
                                 SerialNo = string.Empty,
                                 StatusName = r.Status,
                                 TypeName = string.Empty,
                                 Details = r.Details,
                                 AssignedTechnician = r.AssignedTechnician,
                                 CCID = r.CCID
                             }).ToList();
                return Json(new { draw = draw, recordsFiltered = rlist.Count(), recordsTotal = rlist.Count(), data = rlist });
                //return Json(rlist);
                //grdstudetails.DataSource = StudentRecords;
                //grdstudetails.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Json(string.Empty);
            }
        }
        [Route("api/ContactAdressDetailsByContactID")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult ContactAdressDetailsByContactID()
        {

            var request = HttpContext.Current.Request;
            //var settings = Properties.Settings.Default;
            string ContactID = string.Empty;
            string pid = string.Empty;
            if (request.Form["action"] == null)
            {
                ContactID = request.Form["ContactID"].ToString();

            }
            //else if (request.Form["action"] == "create" || request.Form["action"] == "edit" || request.Form["action"] == "remove")
            //{
            //    ContactID = request.Form[1].ToString();
            //}
            var stext = request.Form["search"].ToString();
            using (var db = new Database("sqlserver", Constants.DBString))
            {
                var response = new Editor(db, "V_ContactAddressDetails", "ID")
                    .Debug(true)
                    .Model<PortfolioMgt.Entity.V_ContactAddressDetail>()
                    .Where(p => p.Where("ContactID", ContactID, "=", false))
                    .Process(request)
                    .Data();
                return Json(response);
            }
        }
        [Route("api/ContactAdressDetails")]
      
        public IHttpActionResult ContactAdressDetails()
        {

            var request = HttpContext.Current.Request;
            //var settings = Properties.Settings.Default;
            string ContactID = string.Empty;
            string pid = string.Empty;
            //if (request.Form["action"] == null)
            //{
            //    ContactID = request.Form["ContactID"].ToString();

            //}
            //else if (request.Form["action"] == "create" || request.Form["action"] == "edit" || request.Form["action"] == "remove")
            //{
            //    ContactID = request.Form[1].ToString();
            //}
            var pd = sessionKeys.PortfolioID;
            var stext =  request.Form["search"].ToString();
            using (var db = new Database("sqlserver", Constants.DBString))
            {
                var response = new Editor(db, "V_ContactAddressDetails", "ID")
                    .Debug(true)
                    .Model<PortfolioMgt.Entity.V_ContactAddressDetail>()
                    .Where("PortfolioID",sessionKeys.PortfolioID,"=")
                    //.Where(p => p.Where("ID", "(select ID from V_ContactAddressDetails p where (p.Name +' ' + p.Email + ' ' + p.Address + ' ' + p.City+ ' ' + p.Mobile + ' ' + p.PostCode +' '+p.PolicyTitle + ' ' +p.PolicyNumber) like '%"+ stext+"%')", "in", false))
                    .Where(p => p.Where("ID", "(select ID from V_ContactAddressDetails p where (p.Name +' ' + p.Email + ' ' + p.Address + ' ' + p.City+ ' ' + p.Mobile + ' ' + p.PostCode ) like '%" + stext + "%')", "in", false))
                    .Process(request)
                    .Data();
                return Json(response);
            }
        }
        [Route("api/ContactAdress")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult ContactAdress()
        {
            try
            {

                var request = HttpContext.Current.Request;
                //var settings = Properties.Settings.Default;
                string ContactID = string.Empty;
                string pid = string.Empty;
                if (request.Form["action"] == null)
                {
                    ContactID = request.Form["ContactID"].ToString();

                }
                else if (request.Form["action"] == "create" || request.Form["action"] == "edit" || request.Form["action"] == "remove")
                {
                    //if first parameter contains 'row_' word and overwite to contactid
                    ContactID = request.Form[1].ToString();
                    if(ContactID.Contains("row_"))
                    {
                        ContactID = request.Form["data[" + ContactID + "][PortfolioContactAddress][ContactID]"].ToString();
                    }
                }
                using (var db = new Database("sqlserver", Constants.DBString))
                {
                    var response = new Editor(db, "PortfolioContactAddress", "ID")
                        .Debug(true)
                        .Model<DeffinityAppDev.Models.ContactModel>()
                        .Field(new Field("PortfolioContactAddress.PolicyTypeID")
                            .Options(new Options()
                                .Table("ProductPolicyType")
                                .Value("ID")
                                .Label("Title")
                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(new Field("PortfolioContactAddress.PolicyStartsID")
                            .Options(new Options()
                                .Table("PolicyStartsIn")
                                .Value("PSIID")
                                .Label("Value")
                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(new Field("PortfolioContactAddress.ContractTermID")
                            .Options(new Options()
                                .Table("PolicyContractTerm")
                                .Value("PCTID")
                                .Label("Name")
                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                        .LeftJoin("ProductPolicyType", "ProductPolicyType.ID", "=", "PortfolioContactAddress.PolicyTypeID")
                         .LeftJoin("PolicyStartsIn", "PolicyStartsIn.PSIID", "=", "PortfolioContactAddress.PolicyStartsID")
                         .LeftJoin("PolicyContractTerm", "PolicyContractTerm.PCTID", "=", "PortfolioContactAddress.ContractTermID")
                       .Where(p => p.Where("PortfolioContactAddress.ContactID", ContactID, "="))
                        .Process(request)
                        .Data();

                    return Json(response);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Json(string.Empty);
            }
        }

        [Route("api/ContactAdressAssetsAll")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult ContactAdressAssetsAll()
        {
            var s = sessionKeys.PortfolioID;
            var request = HttpContext.Current.Request;
            //var settings = Properties.Settings.Default;
            string ContactID = string.Empty;
            string ContactAddressID = string.Empty;
            string pid = string.Empty;
            string AssetID = string.Empty;
            if (request.Form["action"] == null)
            {
                ContactID = request.Form["ContactID"].ToString();
                ContactAddressID = request.Form["ContactAddressID"].ToString();
                if (request.Form["AssetID"] != null)
                    AssetID = request.Form["AssetID"].ToString();
            }
            else if (request.Form["action"] == "create" || request.Form["action"] == "edit" || request.Form["action"] == "remove")
            {
                ContactID = request.Form[1].ToString();
                ContactAddressID = request.Form[2].ToString();

            }
            using (var db = new Database("sqlserver", Constants.DBString))
            {
                if (!string.IsNullOrEmpty(AssetID))
                {
                    //var editor = new Editor(db, "Assets", "ID");
                    // editor.PostCreate += (sender, e) => AssoicateWithAsset(Convert.ToInt32(e.Id), Convert.ToInt32(ContactID), Convert.ToInt32(ContactAddressID));
                    var response = new Editor(db, "Assets", "ID")
                        .Debug(true)
                        .Model<DeffinityAppDev.Models.AssetsModel>()

                        .Field(new Field("Assets.Type")
                            .Options(new Options()
                                .Table("DC.Categories")
                                .Value("ID")
                                .Label("Name")

                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                        .Field(new Field("Assets.Make")
                            .Options(new Options()
                                .Table("DC.SubCategory")
                                .Value("ID")
                                .Label("Name")
                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(new Field("Assets.Model")
                            .Options(new Options()
                                .Table("DC.ProductModel")
                                .Value("ModelID")
                                .Label("ModelName")
                            )
                        // .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(new Field("Assets.AssetsTypeID")
                            .Options(new Options()
                                .Table("WarrantyTerm")
                                .Value("ID")
                                .Label("Name")
                                .Order("ID")
                            )
                        // .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(
                       new Field("Assets.Image")
                          .Upload(
       new DataTables.Upload((file, id) =>
       {
           //+ @"uploads\__ID____EXTN__"
           //file.SaveAs(request.PhysicalApplicationPath + @"WF\uploaddata\Assets\" + "__ID____EXTN__");
           file.SaveAs(request.PhysicalApplicationPath + @"WF\uploaddata\Assets\" + id + ".png");
           return id;
       })
           .Db("files", "id", new Dictionary<string, object>
           {
                {"filename", DataTables.Upload.DbType.FileName},
                                    {"filesize", DataTables.Upload.DbType.FileSize},
                //{"fileName", DataTables.Upload.DbType.FileName},
                //{"webPath", DataTables.Upload.DbType.WebPath},
                //{"systemPath", DataTables.Upload.DbType.SystemPath}
           })
                   //.DbClean(data =>
                   //{
                   //    foreach (var row in data)
                   //    {
                   //        File.Delete(row["systemPath"].ToString());
                   //    }

                   //    return true;
                   //})
                   ))

                        .LeftJoin("files", "files.id", "=", "Assets.Image")
                        .LeftJoin("DC.Categories as Category", "Category.ID", "=", "Assets.Type")
                        .LeftJoin("DC.SubCategory as SubCategory", "SubCategory.ID", "=", "Assets.Make")
                        .LeftJoin("DC.ProductModel as ProductModel", "ProductModel.ModelID", "=", "Assets.Model")
                        .LeftJoin("WarrantyTerm as WarrantyTerm", "WarrantyTerm.ID", "=", "Assets.AssetsTypeID")
                       //.LeftJoin("AssetAssociatedToContacts", "AssetAssociatedToContacts.ContactAddressID", "=", "PortfolioContactAddress.ID")
                       .Where(p => p.Where("Assets.ID", AssetID, "="))
                      //.Where(q => q.Where("Assets.ContactAddressID", "(select AssetId from AssetAssociatedToContacts where ContactAddressID =" + ContactAddressID + " )", "IN", false))
                      .Process(request)
                      .Data();


                    return Json(response);

                }
                else
                {
                    //var editor = new Editor(db, "Assets", "ID");
                    // editor.PostCreate += (sender, e) => AssoicateWithAsset(Convert.ToInt32(e.Id), Convert.ToInt32(ContactID), Convert.ToInt32(ContactAddressID));
                    var response = new Editor(db, "Assets", "ID")
                        .Debug(true)
                        .Model<DeffinityAppDev.Models.AssetsModel>()
                        .Field(new Field("Assets.Type")
                            .Options(new Options()
                                .Table("DC.Categories")
                                .Value("ID")
                                .Label("Name")
                                .Where(q => q.Where("DC.Categories.CustomerID", "(" + sessionKeys.PortfolioID + " )", "IN", false))
                            //.Where(q => q.Where("DC.Categories.TypeOfRequestID", "(select ID from DC.TypeOfRequest where CustomerID = " + sessionKeys.PortfolioID + " )", "IN", false))

                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                        .Field(new Field("Assets.Make")
                            .Options(new Options()
                                .Table("DC.SubCategory")
                                .Value("ID")
                                .Label("Name")
                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(new Field("Assets.Model")
                            .Options(new Options()
                                .Table("DC.ProductModel")
                                .Value("ModelID")
                                .Label("ModelName")
                            )
                        // .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(new Field("Assets.AssetsTypeID")
                            .Options(new Options()
                                .Table("WarrantyTerm")
                                .Value("ID")
                                .Label("Name")
                                .Order("ID")
                            )
                        // .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                          .Field(new Field("Assets.ContactAddressID")
                            .Options(new Options()
                                .Table("PortfolioContactAddress")
                                .Value("ID")
                                .Label("Address")
                                .Where(p => p.Where("PortfolioContactAddress.ContactID", ContactID, "="))
                                .Order("ID")
                            )
                        // .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                           .Field(
                       new Field("Assets.Image")
                          .Upload(
       new DataTables.Upload((file, id) =>
       {
           //get 

           //file.SaveAs(request.PhysicalApplicationPath + @"WF\uploaddata\Assets\" + "__ID____EXTN__");
           file.SaveAs(request.PhysicalApplicationPath + @"WF\uploaddata\Assets\" + id + ".png");
           return id;
       })
           .Db("files", "id", new Dictionary<string, object>
           {
                {"filename", DataTables.Upload.DbType.FileName},
                                    {"filesize", DataTables.Upload.DbType.FileSize},
                //{"fileName", DataTables.Upload.DbType.FileName},
                //{"webPath", DataTables.Upload.DbType.WebPath},
                //{"systemPath", DataTables.Upload.DbType.SystemPath}
           })
                   //.DbClean(data =>
                   //{
                   //    foreach (var row in data)
                   //    {
                   //        File.Delete(row["systemPath"].ToString());
                   //    }

                   //    return true;
                   //}
                   //)
                   ))

                        .LeftJoin("files", "files.id", "=", "Assets.Image")
                         .LeftJoin("DC.Categories as Category", "Category.ID", "=", "Assets.Type")
                        .LeftJoin("DC.SubCategory as SubCategory", "SubCategory.ID", "=", "Assets.Make")
                        .LeftJoin("DC.ProductModel as ProductModel", "ProductModel.ModelID", "=", "Assets.Model")
                        .LeftJoin("WarrantyTerm as WarrantyTerm", "WarrantyTerm.ID", "=", "Assets.AssetsTypeID")
                        .LeftJoin("PortfolioContactAddress as PortfolioContactAddress", "PortfolioContactAddress.ID", "=", "Assets.ContactAddressID")
                        .LeftJoin("PortfolioContacts as PortfolioContacts", "PortfolioContacts.ID", "=", "PortfolioContactAddress.ContactID")
                      //.LeftJoin("AssetAssociatedToContacts", "AssetAssociatedToContacts.ContactAddressID", "=", "PortfolioContactAddress.ID")
                      //
                      //.Where(p => p.Where("PortfolioContactAddress.ContactID", ContactID, "="))
                      //.Where(p => p.Where("Assets.ContactAddressID", ContactAddressID, "="))
                      //.Where(q => q.Where("Assets.ContactAddressID", "(select AssetId from AssetAssociatedToContacts where ContactAddressID =" + ContactAddressID + " )", "IN", false))
                      .Where(q => q.Where("Assets.ContactID", "(select ID from PortfolioContacts where PortfolioID =" + sessionKeys.PortfolioID +" )", "IN", false))
                      .Process(request)
                      .Data();

                    //response.PostEdit += (sender, e) => ForwardPopulate(db, e.Id, e.Values);
                    //response.data.Insert()
                    return Json(response);
                }
            }

        }
        [Route("api/ContactAdressAssets")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult ContactAdressAssets()
        {

            var request = HttpContext.Current.Request;
            //var settings = Properties.Settings.Default;
            string ContactID = string.Empty;
            string ContactAddressID = string.Empty;
            string pid = string.Empty;
            string AssetID = string.Empty;
            if (request.Form["action"] == null)
            {
                ContactID = request.Form["ContactID"].ToString();
                ContactAddressID = request.Form["ContactAddressID"].ToString();
                if (request.Form["AssetID"] != null)
                    AssetID = request.Form["AssetID"].ToString();
            }
            else if (request.Form["action"] == "create" || request.Form["action"] == "edit" || request.Form["action"] == "remove")
            {
                ContactID = request.Form[1].ToString();
                ContactAddressID = request.Form[2].ToString();

            }
            using (var db = new Database("sqlserver", Constants.DBString))
            {
                if (!string.IsNullOrEmpty(AssetID))
                {
                    //var editor = new Editor(db, "Assets", "ID");
                    // editor.PostCreate += (sender, e) => AssoicateWithAsset(Convert.ToInt32(e.Id), Convert.ToInt32(ContactID), Convert.ToInt32(ContactAddressID));
                    var response = new Editor(db, "Assets", "ID")
                        .Debug(true)
                        .Model<DeffinityAppDev.Models.AssetsModel>()

                        .Field(new Field("Assets.Type")
                            .Options(new Options()
                                .Table("DC.Categories")
                                .Value("ID")
                                .Label("Name")

                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                        .Field(new Field("Assets.Make")
                            .Options(new Options()
                                .Table("DC.SubCategory")
                                .Value("ID")
                                .Label("Name")
                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(new Field("Assets.Model")
                            .Options(new Options()
                                .Table("DC.ProductModel")
                                .Value("ModelID")
                                .Label("ModelName")
                            )
                        // .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(new Field("Assets.AssetsTypeID")
                            .Options(new Options()
                                .Table("WarrantyTerm")
                                .Value("ID")
                                .Label("Name")
                                .Order("ID")
                            )
                        // .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(
                       new Field("Assets.Image")
                          .Upload(
       new DataTables.Upload((file, id) =>
       {
           //+ @"uploads\__ID____EXTN__"
           //file.SaveAs(request.PhysicalApplicationPath + @"WF\uploaddata\Assets\" + "__ID____EXTN__");
           file.SaveAs(request.PhysicalApplicationPath + @"WF\uploaddata\Assets\" + id + ".png");
           return id;
       })
           .Db("files", "id", new Dictionary<string, object>
           {
                {"filename", DataTables.Upload.DbType.FileName},
                                    {"filesize", DataTables.Upload.DbType.FileSize},
                //{"fileName", DataTables.Upload.DbType.FileName},
                //{"webPath", DataTables.Upload.DbType.WebPath},
                //{"systemPath", DataTables.Upload.DbType.SystemPath}
           })
                   //.DbClean(data =>
                   //{
                   //    foreach (var row in data)
                   //    {
                   //        File.Delete(row["systemPath"].ToString());
                   //    }

                   //    return true;
                   //})
                   ))

                        .LeftJoin("files", "files.id", "=", "Assets.Image")
                        .LeftJoin("DC.Categories as Category", "Category.ID", "=", "Assets.Type")
                        .LeftJoin("DC.SubCategory as SubCategory", "SubCategory.ID", "=", "Assets.Make")
                        .LeftJoin("DC.ProductModel as ProductModel", "ProductModel.ModelID", "=", "Assets.Model")
                        .LeftJoin("WarrantyTerm as WarrantyTerm", "WarrantyTerm.ID", "=", "Assets.AssetsTypeID")
                        .LeftJoin("PortfolioContactAddress as PortfolioContactAddress", "PortfolioContactAddress.ID", "=", "Assets.ContactAddressID")
                       //.LeftJoin("AssetAssociatedToContacts", "AssetAssociatedToContacts.ContactAddressID", "=", "PortfolioContactAddress.ID")
                       .Where(p => p.Where("Assets.ID", AssetID, "="))
                      //.Where(q => q.Where("Assets.ContactAddressID", "(select AssetId from AssetAssociatedToContacts where ContactAddressID =" + ContactAddressID + " )", "IN", false))
                      .Process(request)
                      .Data();

                    
                    return Json(response);

                }
                else
                {
                    //var editor = new Editor(db, "Assets", "ID");
                    // editor.PostCreate += (sender, e) => AssoicateWithAsset(Convert.ToInt32(e.Id), Convert.ToInt32(ContactID), Convert.ToInt32(ContactAddressID));
                    var response = new Editor(db, "Assets", "ID")
                        .Debug(true)
                        .Model<DeffinityAppDev.Models.AssetsModel>()
                        .Field(new Field("Assets.Type")
                            .Options(new Options()
                                .Table("DC.Categories")
                                .Value("ID")
                                .Label("Name")
                                .Where(q => q.Where("DC.Categories.CustomerID", "(" + sessionKeys.PortfolioID + " )", "IN", false))
                            //.Where(q => q.Where("DC.Categories.TypeOfRequestID", "(select ID from DC.TypeOfRequest where CustomerID = " + sessionKeys.PortfolioID + " )", "IN", false))

                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                        .Field(new Field("Assets.Make")
                            .Options(new Options()
                                .Table("DC.SubCategory")
                                .Value("ID")
                                .Label("Name")
                            )
                            .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(new Field("Assets.Model")
                            .Options(new Options()
                                .Table("DC.ProductModel")
                                .Value("ModelID")
                                .Label("ModelName")
                            )
                        // .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                         .Field(new Field("Assets.AssetsTypeID")
                            .Options(new Options()
                                .Table("WarrantyTerm")
                                .Value("ID")
                                .Label("Name")
                                .Order("ID")
                            )
                        // .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                          .Field(new Field("Assets.ContactAddressID")
                            .Options(new Options()
                                .Table("PortfolioContactAddress")
                                .Value("ID")
                                .Label("Address")
                                .Where(p => p.Where("PortfolioContactAddress.ContactID", ContactID, "="))
                                .Order("ID")
                            )
                        // .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                        )
                           .Field(
                       new Field("Assets.Image")
                          .Upload(
       new DataTables.Upload((file, id) =>
       {
           //get 

           //file.SaveAs(request.PhysicalApplicationPath + @"WF\uploaddata\Assets\" + "__ID____EXTN__");
           file.SaveAs(request.PhysicalApplicationPath + @"WF\uploaddata\Assets\" + id + ".png");
           return id;
       })
           .Db("files", "id", new Dictionary<string, object>
           {
                {"filename", DataTables.Upload.DbType.FileName},
                                    {"filesize", DataTables.Upload.DbType.FileSize},
                //{"fileName", DataTables.Upload.DbType.FileName},
                //{"webPath", DataTables.Upload.DbType.WebPath},
                //{"systemPath", DataTables.Upload.DbType.SystemPath}
           })
           //.DbClean(data =>
           //{
           //    foreach (var row in data)
           //    {
           //        File.Delete(row["systemPath"].ToString());
           //    }

           //    return true;
           //}
           //)
                   ))

                        .LeftJoin("files", "files.id", "=", "Assets.Image")
                         .LeftJoin("DC.Categories as Category", "Category.ID", "=", "Assets.Type")
                        .LeftJoin("DC.SubCategory as SubCategory", "SubCategory.ID", "=", "Assets.Make")
                        .LeftJoin("DC.ProductModel as ProductModel", "ProductModel.ModelID", "=", "Assets.Model")
                        .LeftJoin("WarrantyTerm as WarrantyTerm", "WarrantyTerm.ID", "=", "Assets.AssetsTypeID")
                        .LeftJoin("PortfolioContactAddress as PortfolioContactAddress", "PortfolioContactAddress.ID", "=", "Assets.ContactAddressID")
                      //.LeftJoin("AssetAssociatedToContacts", "AssetAssociatedToContacts.ContactAddressID", "=", "PortfolioContactAddress.ID")
                      //ContactAddressID
                      .Where(p => p.Where("PortfolioContactAddress.ID", ContactAddressID, "="))
                      //.Where(p => p.Where("PortfolioContactAddress.ContactID", ContactID, "="))
                      //.Where(p => p.Where("Assets.ContactAddressID", ContactAddressID, "="))
                      //.Where(q => q.Where("Assets.ContactAddressID", "(select AssetId from AssetAssociatedToContacts where ContactAddressID =" + ContactAddressID + " )", "IN", false))
                      .Process(request)
                      .Data();

                    //response.PostEdit += (sender, e) => ForwardPopulate(db, e.Id, e.Values);
                    //response.data.Insert()
                    return Json(response);
                }
            }

        }
         [Route("api/getpolicyno")]
         [HttpPost]
         public IHttpActionResult GetPolicyno(object typeid)
         {
             string retval = string.Empty;
             var request = HttpContext.Current.Request;
             try
             {
                 // var settings = Properties.Settings.Default;
                 if (request.Params["typeid[rows][0][DT_RowId]"] == null)
                 {
                     if (request.Params["typeid[values][PortfolioContactAddress.ContactID]"] != null)
                     {
                         string ContactID = request.Params["typeid[values][PortfolioContactAddress.ContactID]"];
                         string PolicyTypeID = request.Params["typeid[values][PortfolioContactAddress.PolicyTypeID]"];
                         string PolicyStartsID = request.Params["typeid[values][PortfolioContactAddress.PolicyStartsID]"];
                         string ContractTermID = request.Params["typeid[values][PortfolioContactAddress.ContractTermID]"];
                         string StartDate = request.Params["typeid[values][PortfolioContactAddress.StartDate]"];
                         string ExpiryDate = request.Params["typeid[values][PortfolioContactAddress.ExpiryDate]"];
                         using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                         {
                             using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
                             {
                                 //policy default data
                                 var pdata = pd.PolicyNumberFormatSettings.FirstOrDefault();

                                 if (!string.IsNullOrEmpty(PolicyTypeID))
                                 {
                                     //policy type prefix
                                     var ptdata = pd.ProductPolicyTypes.Where(o => o.ID == Convert.ToInt32(PolicyTypeID)).FirstOrDefault();
                                     var ptaddress = pd.PortfolioContactAddresses.Where(o => o.PolicyTypeID == Convert.ToInt32(PolicyTypeID)).Where(o=>o.PolicyNumber !="").OrderByDescending(o => o.ID).FirstOrDefault();
                                     //PolicyMax no
                                     if (ptdata != null)
                                     {
                                         if(!string.IsNullOrEmpty(StartDate))
                                         {
                                         if (ptaddress == null)
                                             retval = string.Format("{0}{1}", ptdata.PolicyTypePrefix, (pdata.Seed).ToString("D6"));
                                             //retval = string.Format("{0} - {1} - {2}", pdata.Prefix, ptdata.PolicyTypePrefix, (pdata.Seed).ToString("D6"));
                                         else
                                         {
                                            // ptdata.PolicyTypePrefix
                                             int pno = 0;
                                             string policyno = ptaddress.PolicyNumber;
                                             policyno = ptaddress.PolicyNumber.Replace(ptdata.PolicyTypePrefix, "");
                                             //string[] split = policyno.Split('-').ToArray();
                                             //if (split.Count() > 2)
                                             //{
                                                 try
                                                 {
                                                     pno = Convert.ToInt32(policyno);
                                                 }
                                                 catch(Exception ex)
                                                 {
                                                     LogExceptions.WriteExceptionLog(ex);
                                                     pno = 0;
                                                 }
                                            // }
                                             retval = string.Format("{0}{1}", ptdata.PolicyTypePrefix, (pno + pdata.Seed).ToString("D6"));
                                         }
                                         dynamic result = new ExpandoObject();
                                         result.options = new ExpandoObject();
                                         result.options.policyno = retval;

                                         return Json(result);
                                         }
                                          else
                                         return Json(string.Empty);
                                     }
                                     else
                                         return Json(string.Empty);
                                 }
                                 else
                                     return Json(string.Empty);
                             }
                         }
                     }
                     else
                         return Json(string.Empty);
                 }
                 else
                     return Json(string.Empty);
             }
             catch(Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return Json(string.Empty);
             }
         }
         [Route("api/getpolicynonew")]
         [HttpPost]
         public IHttpActionResult GetPolicynonew(object typeid1)
         {
             
             string retval = string.Empty;
             var request = HttpContext.Current.Request;
             try
             {
                        string typeid = request.Params["typeid"].ToString();
                         using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                         {
                             using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
                             {
                                 //policy default data
                                 var pdata = pd.PolicyNumberFormatSettings.FirstOrDefault();

                                 if (!string.IsNullOrEmpty(typeid.ToString()))
                                 {
                                     //policy type prefix
                                     var ptdata = pd.ProductPolicyTypes.Where(o => o.ID == Convert.ToInt32(typeid.ToString())).FirstOrDefault();
                                     var ptaddress = pd.PortfolioContactAddresses.Where(o => o.PolicyTypeID == Convert.ToInt32(typeid.ToString())).Where(o => o.PolicyNumber != "").OrderByDescending(o => o.ID).FirstOrDefault();
                                     //PolicyMax no
                                     if (ptdata != null)
                                     {
                                         
                                             if (ptaddress == null)
                                                 retval = string.Format("{0}{1}", ptdata.PolicyTypePrefix, (pdata.Seed).ToString("D6"));
                                             //retval = string.Format("{0} - {1} - {2}", pdata.Prefix, ptdata.PolicyTypePrefix, (pdata.Seed).ToString("D6"));
                                             else
                                             {
                                                 // ptdata.PolicyTypePrefix
                                                 int pno = 0;
                                                 string policyno = ptaddress.PolicyNumber;
                                                 policyno = ptaddress.PolicyNumber.Replace(ptdata.PolicyTypePrefix, "");
                                                 //string[] split = policyno.Split('-').ToArray();
                                                 //if (split.Count() > 2)
                                                 //{
                                                 try
                                                 {
                                                     pno = Convert.ToInt32(policyno);
                                                 }
                                                 catch (Exception ex)
                                                 {
                                                     LogExceptions.WriteExceptionLog(ex);
                                                     pno = 0;
                                                 }
                                                 // }
                                                 retval = string.Format("{0}{1}", ptdata.PolicyTypePrefix, (pno + pdata.Seed).ToString("D6"));
                                             }
                                             dynamic result = new ExpandoObject();
                                             result.options = new ExpandoObject();
                                             result.options.policyno = retval;

                                             return Json(result);
                                        
                                     }
                                     else
                                         return Json(string.Empty);
                                 }
                                 else
                                     return Json(string.Empty);
                             }
                         }
                    
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return Json(string.Empty);
             }
         }

         [Route("api/getpolicycost")]
         [HttpPost]
         public IHttpActionResult GetPolicycost(object typeid1)
         {
             string retval = string.Empty;
             var request = HttpContext.Current.Request;
             try
             {
                 double monthly = 0.00;
                 double yearly = 0.00;
                 string typeid = request.Params["typeid"].ToString();
                 string ContractTermID = request.Params["ContractTermID"].ToString();
                 string more = "0";
                 if (request.Params["more"] != null)
                     more = request.Params["more"].ToString();
                 else
                     more = "0";

                 using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                 {
                     using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
                     {
                         //policy type prefix
                         var ptData = pd.PolicyContractTerms.Where(o => o.PCTID == Convert.ToInt32(ContractTermID)).FirstOrDefault();
                         //policy default data
                         if (!string.IsNullOrEmpty(typeid.ToString()))
                         {
                             //policy type prefix
                             var pcEntity = pd.ProductPolicyTypes.Where(o=>o.ID == Convert.ToInt32(typeid)).FirstOrDefault();
                             if (ptData != null)
                             {
                                 if (more == "0")
                                 {
                                     monthly = pcEntity.Monthly.HasValue ? pcEntity.Monthly.Value : 0.00;
                                     yearly = pcEntity.Yearly.HasValue ? pcEntity.Yearly.Value : 0.00;
                                 }
                                 else
                                 {
                                     monthly = pcEntity.Monthly_G5000.HasValue ? pcEntity.Monthly_G5000.Value : 0.00;
                                     yearly = pcEntity.Yearly_G5000.HasValue ? pcEntity.Yearly_G5000.Value : 0.00;
                                 }
                                 if (ptData.Name == "Monthly")
                                     retval = string.Format("{0:F2}", monthly);
                                 else if (ptData.Name == "1 Year")
                                     retval = string.Format("{0:F2}", yearly);
                                 else if (ptData.Name == "2 Year")
                                     retval = string.Format("{0:F2}", yearly * 2);
                                 else if (ptData.Name == "3 Year")
                                     retval = string.Format("{0:F2}", yearly * 3);
                                 else if (ptData.Name == "4 Year")
                                     retval = string.Format("{0:F2}", yearly * 4);
                                 else if (ptData.Name == "5 Year")
                                     retval = string.Format("{0:F2}", yearly * 5);
                                 dynamic result = new ExpandoObject();
                                 result.options = new ExpandoObject();
                                 result.options.policycost = retval;
                                 return Json(result);
                             }
                             else
                                 return Json(string.Empty);
                         }
                         else
                             return Json(string.Empty);
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return Json(string.Empty);
             }
         }

         [Route("api/getenddate")]
         [HttpPost]
         public IHttpActionResult GetEnddate(object typeid)
         {
             string retval = string.Empty;
             var request = HttpContext.Current.Request;
             try
             {
                 // var settings = Properties.Settings.Default;
                 if (request.Params["typeid[rows][0][DT_RowId]"] == null)
                 {
                     if (request.Params["typeid[values][PortfolioContactAddress.ContactID]"] != null)
                     {
                         string ContactID = request.Params["typeid[values][PortfolioContactAddress.ContactID]"];
                         string PolicyTypeID = request.Params["typeid[values][PortfolioContactAddress.PolicyTypeID]"];
                         string PolicyStartsID = request.Params["typeid[values][PortfolioContactAddress.PolicyStartsID]"];
                         string ContractTermID = request.Params["typeid[values][PortfolioContactAddress.ContractTermID]"];
                         string StartDate = request.Params["typeid[values][PortfolioContactAddress.StartDate]"];
                         string ExpiryDate = request.Params["typeid[values][PortfolioContactAddress.ExpiryDate]"];
                         using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                         {
                             using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
                             {
                                 //policy type prefix
                                 var ptdata = pd.PolicyContractTerms.Where(o => o.PCTID == Convert.ToInt32(!string.IsNullOrEmpty(ContractTermID)?ContractTermID:"0")).FirstOrDefault();
                                 //PolicyMax no
                                 if (ptdata != null)
                                 {
                                     dynamic result = new ExpandoObject();
                                     result.options = new ExpandoObject();
                                     if (!string.IsNullOrEmpty(StartDate))
                                         result.options.enddate = Convert.ToDateTime(StartDate).AddDays(ptdata.Value);
                                     return Json(result);
                                 }
                                 else
                                     return Json(string.Empty);
                             }
                         }
                     }
                     else
                         return Json(string.Empty);
                 }
                 else
                     return Json(string.Empty);
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return Json(string.Empty);
             }
         }

         [Route("api/getenddatenew")]
         [HttpPost]
         public IHttpActionResult GetEnddateNew()
         {
             string retval = string.Empty;
             var request = HttpContext.Current.Request;
             try
             {
                 // var settings = Properties.Settings.Default;

                 string ContractTermID = request.Params["ContractTermID"];
                 string StartDate = request.Params["StartDate"];
                 using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                 {
                     using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
                     {
                         //policy type prefix
                         var ptdata = pd.PolicyContractTerms.Where(o => o.PCTID == Convert.ToInt32(!string.IsNullOrEmpty(ContractTermID)?ContractTermID:"0")).FirstOrDefault();
                         //PolicyMax no
                         if (ptdata != null)
                         {
                             dynamic result = new ExpandoObject();
                             result.options = new ExpandoObject();
                             if (!string.IsNullOrEmpty(StartDate))
                                 result.options.enddate = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToDateTime(StartDate).AddDays(ptdata.Value));
                             return Json(result);
                         }
                         else
                             return Json(string.Empty);
                     }
                 }
                    
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return Json(string.Empty);
             }
         }
         [Route("api/getstartdate")]
         [HttpPost]
         public IHttpActionResult GetStartdate(object typeid)
         {
             string retval = string.Empty;
             var request = HttpContext.Current.Request;
             try
             {
                 // var settings = Properties.Settings.Default;
                 if (request.Params["typeid[rows][0][DT_RowId]"] == null)
                 {
                     if (request.Params["typeid[values][PortfolioContactAddress.ContactID]"] != null)
                     {
                         string ContactID = request.Params["typeid[values][PortfolioContactAddress.ContactID]"];
                         string PolicyTypeID = request.Params["typeid[values][PortfolioContactAddress.PolicyTypeID]"];
                         string PolicyStartsID = request.Params["typeid[values][PortfolioContactAddress.PolicyStartsID]"];
                         string ContractTermID = request.Params["typeid[values][PortfolioContactAddress.ContractTermID]"];
                         string StartDate = request.Params["typeid[values][PortfolioContactAddress.StartDate]"];
                         string ExpiryDate = request.Params["typeid[values][PortfolioContactAddress.ExpiryDate]"];
                         using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                         {
                             using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
                             {
                                 if (!string.IsNullOrEmpty(PolicyStartsID))
                                 {
                                     //policy type prefix
                                     var ptdata = pd.PolicyStartsIns.Where(o => o.PSIID == Convert.ToInt32(PolicyStartsID)).FirstOrDefault();
                                     //PolicyMax no
                                     if (ptdata != null)
                                     {
                                         dynamic result = new ExpandoObject();
                                         result.options = new ExpandoObject();
                                         if (!string.IsNullOrEmpty(StartDate))
                                             result.options.startdate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddDays(ptdata.Value);
                                             //result.options.startdate = Convert.ToDateTime(StartDate).AddDays(ptdata.Value);
                                         return Json(result);
                                     }
                                     else
                                         return Json(string.Empty);
                                 }
                                 else
                                     return Json(string.Empty);
                             }
                         }
                     }
                     else
                         return Json(string.Empty);
                 }
                 else
                     return Json(string.Empty);
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return Json(string.Empty);
             }
         }

         [Route("api/getstartdatenew")]
         [HttpPost]
         public IHttpActionResult GetStartdateNew(object typeid)
         {
             string retval = string.Empty;
             var request = HttpContext.Current.Request;
             try
             {

                 string StartDate = request.Params["StartDate"];
                 string PolicyStartsID = request.Params["PolicyStartsID"];
                
                 using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                 {
                     using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
                     {
                         if (!string.IsNullOrEmpty(PolicyStartsID))
                         {
                             //policy type prefix
                             var ptdata = pd.PolicyStartsIns.Where(o => o.PSIID == Convert.ToInt32(PolicyStartsID)).FirstOrDefault();
                             //PolicyMax no
                             if (ptdata != null)
                             {
                                 dynamic result = new ExpandoObject();
                                 result.options = new ExpandoObject();
                                 if (!string.IsNullOrEmpty(StartDate))
                                     result.options.startdate = string.Format(Deffinity.systemdefaults.GetStringDateformat(),Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddDays(ptdata.Value));
                                 //result.options.startdate = Convert.ToDateTime(StartDate).AddDays(ptdata.Value);
                                 return Json(result);
                             }
                             else
                                 return Json(string.Empty);
                         }
                         else
                             return Json(string.Empty);
                     }
                 }

             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 return Json(string.Empty);
             }
         }

         [Route("api/countries")]
         [HttpPost]
         public IHttpActionResult CountryOptions()
         {
             var request = HttpContext.Current.Request;
            // var settings = Properties.Settings.Default;

             using (var db = new Database("sqlserver", Constants.DBString))
             {
                 var query = db
                     .Debug(true)
                     .Select(
                     "[DC].[SubCategory] as AssetsMake",
                     new[] { "ID as value", "Name as value" },
                     new Dictionary<string, dynamic>() { { "CategoryID", request.Params["values[Assets.Type]"] } }
                 );

                 dynamic result = new ExpandoObject();
                 result.options = new ExpandoObject();
                 result.options.Assets_Make = query.FetchAll();

                 return Json(result);
             }
         }

         [Route("api/getmakedata")]
         [HttpPost]
         public IHttpActionResult GetMakeData(object typeid)
         {
             
             var request = HttpContext.Current.Request;
             string s = request.Params["typeid"].ToString();
             // var settings = Properties.Settings.Default;

             using (var db = new Database("sqlserver", Constants.DBString))
             {
                 var query = db
                     .Debug(true)
                     .Select(
                      "[DC].[SubCategory] as AssetsMake",
                     new[] { "ID as value", "Name as label" },
                     new Dictionary<string, dynamic>() { { "CategoryID", request.Params["typeid"] } }
                 );

                 dynamic result = new ExpandoObject();
                 result.options = new ExpandoObject();
                 result.options.Assets_Make = query.FetchAll();

                 return Json(result);
             }
         }
         [Route("api/getmodeldata")]
         [HttpPost]
         public IHttpActionResult GetModelData(object makeid)
         {

             var request = HttpContext.Current.Request;
             string s = request.Params["makeid"].ToString();
             // var settings = Properties.Settings.Default;

             using (var db = new Database("sqlserver", Constants.DBString))
             {
                 var query = db
                     .Debug(true)
                     .Select(
                     "[DC].[ProductModel] as AssetsModel",
                      new[] { "ModelID as value", "ModelName as label" },
                     new Dictionary<string, dynamic>() { { "SubCategoryID", request.Params["makeid"] } }
                 );

                 dynamic result = new ExpandoObject();
                 result.options = new ExpandoObject();
                 result.options.Assets_Model = query.FetchAll();

                 return Json(result);
             }
         }
         private void AssoicateWithAsset(int assetid,int contactid, int addressid)
         {
             IAssetRespository<AssetsMgr.Entity.AssetAssociatedToContact> iarepository = new AssetRespository<AssetsMgr.Entity.AssetAssociatedToContact>();
             if(iarepository.GetAll().Where(o=>o.AssetId == assetid && o.ContactId == contactid && o.ContactAddressID == addressid ).Count() == 0)
             {
                 iarepository.Add(new AssetsMgr.Entity.AssetAssociatedToContact()
                     {
                         AssetId = assetid,
                         ContactAddressID = addressid,
                         ContactId = contactid
                     });
             }
         }

         [Route("api/WSAddCustomer")]
         [HttpPost]
         public IHttpActionResult WSAddCustomer()
         {
             string retval = string.Empty;
             try
             {
                 var request = HttpContext.Current.Request;
                 string name =string.Empty;
                 if(request.Params["name"] != null)
                     name = request.Params["name"].ToString();
                 string email = string.Empty;
                 if (request.Params["email"] != null)
                   email=  request.Params["email"].ToString();
                 string phoneno1 = string.Empty;
                 if (request.Params["phoneno1"] != null)
                     phoneno1 = request.Params["phoneno1"].ToString();
                 string phoneno2 = string.Empty;
                 if (request.Params["phoneno2"] != null)
                     phoneno2 = request.Params["phoneno2"].ToString();

                 LogExceptions.LogException("name:"+ name , "\n email:"+email);
                 var contactid = CreateContact(name, email, phoneno1, phoneno2);

                 LogExceptions.LogException("contactid:" + contactid);
                 //add address
                 if (contactid > 0)
                 {
                     string address = string.Empty;
                     if(request.Params["address"] != null)
                      address = request.Params["address"].ToString();
                     string city = string.Empty;
                     if(request.Params["city"] != null)
                      city = request.Params["city"].ToString();
                     string state = string.Empty;
                     if(request.Params["state"]!= null)
                     state = request.Params["state"].ToString();
                     string postcode = string.Empty;
                     if(request.Params["postcode"] != null)
                      postcode = request.Params["postcode"].ToString();
                     string policytype = string.Empty;
                     if(request.Params["policytype"] != null)
                      policytype = request.Params["policytype"].ToString();
                     //string startdate = request.Params["startdate"].ToString();
                     // string expirydate = request.Params["expirydate"].ToString();
                      string amount= string.Empty;
                     if(request.Params["amount"] != null)
                      amount = request.Params["amount"].ToString();

                     string addons =string.Empty;
                     if(request.Params["addons"] != null)
                      addons = request.Params["addons"].ToString();
                     string subscriptiontype= string.Empty;
                     if(request.Params["subscriptiontype"] != null)
                      subscriptiontype = request.Params["subscriptiontype"].ToString();

                     string propertytype = string.Empty;
                     if(request.Params["propertytype"] != null)
                     propertytype = request.Params["propertytype"].ToString();

                      string other= string.Empty;
                     if(request.Params["other"] != null)
                      other = request.Params["other"].ToString();

                     string isLessthan5ksqft= string.Empty;
                     if(request.Params["isLessthan5ksqft"] != null)
                     isLessthan5ksqft = request.Params["isLessthan5ksqft"].ToString();

                     string billingname = string.Empty;
                     if(request.Params["billingname"] != null)
                     billingname = request.Params["billingname"].ToString();

                     string billingemail = string.Empty;
                     if(request.Params["billingemail"] != null)
                      billingemail = request.Params["billingemail"].ToString();

                     string billingaddress = string.Empty;
                     if(request.Params["billingaddress"] != null)
                      billingaddress = request.Params["billingaddress"].ToString();

                     string billingcity = string.Empty;
                     if(request.Params["billingcity"] != null)
                      billingcity = request.Params["billingcity"].ToString();

                     string billingstate = string.Empty;
                     if(request.Params["billingstate"] != null)
                      billingstate = request.Params["billingstate"].ToString();
                     string billingpostcode = string.Empty;
                     if(request.Params["billingpostcode"] != null)
                      billingpostcode = request.Params["billingpostcode"].ToString();

                     string ordernumber = string.Empty;
                     if(request.Params["ordernumber"] != null)
                      ordernumber = request.Params["ordernumber"].ToString();

                     string paypalreference = string.Empty;
                     if(request.Params["paypalreference"] != null)
                      paypalreference = request.Params["paypalreference"].ToString();

                     string logTrack = string.Empty;
                     logTrack = logTrack + "address: " + address+"\n";
                    
                     logTrack = logTrack + "city: " + city + "\n";
                     logTrack = logTrack + "state: " + state + "\n";
                     logTrack = logTrack + "postcode: " + postcode + "\n";
                     logTrack = logTrack + "policytype: " + policytype + "\n";
                     logTrack = logTrack + "amount: " + amount + "\n";
                     logTrack = logTrack + "addons: " + addons + "\n";
                     logTrack = logTrack + "subscriptiontype: " + subscriptiontype + "\n";
                     logTrack = logTrack + "propertytype: " + propertytype + "\n";
                     logTrack = logTrack + "other: " + other + "\n";
                     logTrack = logTrack + "isLessthan5ksqft: " + isLessthan5ksqft + "\n";
                     logTrack = logTrack + "billingname: " + billingname + "\n";
                     logTrack = logTrack + "billingemail: " + billingemail + "\n";
                     logTrack = logTrack + "billingaddress: " + billingaddress + "\n";
                     logTrack = logTrack + "billingcity: " + billingcity + "\n";
                     logTrack = logTrack + "billingstate: " + billingstate + "\n";
                     logTrack = logTrack + "billingpostcode: " + billingpostcode + "\n";
                     logTrack = logTrack + "ordernumber: " + ordernumber + "\n";
                     logTrack = logTrack + "paypalreference: " + paypalreference + "\n";

                     LogExceptions.LogException(logTrack);



                    var addressid = CreateAddress(contactid,
              address, city, state, postcode,
              billingname, billingaddress, billingcity, billingstate, billingpostcode,
             policytype, subscriptiontype, amount, addons,
                     propertytype, other, isLessthan5ksqft == "0" ? false : true, ordernumber, paypalreference);
                     if (addressid > 0)
                     {
                         retval = "Saved Successfully";
                     }
                 }

             }
             catch(Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
                 retval = "Invalid input";
             }
             return Json(retval);
         }


        [Route("api/getexpiredatenew")]
        [HttpPost]
        public IHttpActionResult getexpiredatenew()
        {
            string retval = string.Empty;
            var request = HttpContext.Current.Request;
            try
            {
                // var settings = Properties.Settings.Default;
                //typeid[values][Assets.AssetsTypeID]
                string ContractTermID = request.Params["typeid[values][Assets.AssetsTypeID]"];
                string StartDate = request.Params["typeid[values][Assets.PurchasedDate]"];
                using (AssetsMgr.DAL.AssetsToSoftwareDataContext pd = new AssetsMgr.DAL.AssetsToSoftwareDataContext())
                {
                   
                        //policy type prefix
                        var ptdata = pd.WarrantyTerms.Where(o => o.ID == Convert.ToInt32(!string.IsNullOrEmpty(ContractTermID) ? ContractTermID : "0")).FirstOrDefault();
                        //PolicyMax no
                        if (ptdata != null)
                        {
                            dynamic result = new ExpandoObject();
                            result.options = new ExpandoObject();
                            if (!string.IsNullOrEmpty(StartDate))
                                result.options.enddate = string.Format(Deffinity.systemdefaults.GetStringDateformat(), Convert.ToDateTime(StartDate).AddYears(ptdata.ID));
                            return Json(result);
                        }
                        else
                            return Json(string.Empty);
                    
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return Json(string.Empty);
            }
        }

        #region New User 

        public int CreateContact(string name,string email, string phone1, string phone2)
         {
            int portfolioID = 1;
             bool logintoPortal = true;
             int contactID = 0;
             if (!string.IsNullOrEmpty(name))
             {

                 PortfolioContact pc_check = CustomerContactsBAL.CheckContact(name, email, portfolioID);

                 if (pc_check == null)
                 {
                     PortfolioContact pc = new PortfolioContact();
                     pc.PortfolioID = portfolioID;
                     pc.Name = name;
                     pc.Title = name;
                     pc.Email = email;
                     pc.Mobile = phone1;
                     pc.Telephone = phone2;
                     pc.LogintoPortal = logintoPortal;
                     pc.Key_Contact = false;
                     pc.DateLogged = DateTime.Now;
                     CustomerContactsBAL.AddCustomerContacts(pc);
                     contactID = pc.ID;

                 }
                 else
                 {
                     //PortfolioContact pc = new PortfolioContact();
                     pc_check.Name = name;
                     pc_check.Title = name;
                     pc_check.Email = email;
                     if (!string.IsNullOrEmpty(phone1))
                     pc_check.Mobile = phone1;
                     if (!string.IsNullOrEmpty(phone2))
                     pc_check.Telephone = phone2;
                     pc_check.LogintoPortal = logintoPortal;
                     CustomerContactsBAL.UpdateCustomerContacts(pc_check);
                     contactID = pc_check.ID;
                 }

                 if (logintoPortal && contactID > 0)
                 {
                     ContractorsAndAssociateInsert(contactID);
                 }

             }

             return contactID;
         }

         public void ContractorsAndAssociateInsert(int contactid)
         {
             try
             {
                 using (UserDataContext ud = new UserDataContext())
                 {
                     using (PortfolioDataContext pd = new PortfolioDataContext())
                     {
                         var pcontact = pd.PortfolioContacts.Where(o => o.ID == contactid).FirstOrDefault();
                         var portfolioid = pcontact.PortfolioID;
                         string name = pcontact.Name;
                         string email = pcontact.Email;
                         string contactNo = pcontact.Telephone;
                         var contactUsers = pd.PortfolioContactAssociates.Where(o => o.ContactID == contactid).FirstOrDefault();
                         string password = string.Empty;
                         if (contactUsers == null)
                         {
                             UserMgt.Entity.Contractor cont = new UserMgt.Entity.Contractor();
                             string[] loginname = name.Split(' ');
                             string userName = string.Empty;
                             

                             if (loginname.Length > 1)
                             {
                                 if (loginname[0].Length > 1)
                                     userName = cont.LoginName = loginname[0].Remove(1).ToLower() + loginname[1].ToLower();
                                 else
                                     userName = cont.LoginName = loginname[0].ToLower() + loginname[1].ToLower();
                             }
                             else
                             {
                                 userName = cont.LoginName = loginname[0].Remove(1).ToLower() + loginname[0].ToLower();
                             }
                             //Check the user name is exists 
                             //if exists get new name
                             cont.LoginName = GetUserName(ud.Contractors.Select(p => p).ToList(), userName);
                             password = Membership.GeneratePassword(8, 0);
                             cont.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                             cont.ContractorName = name;
                             cont.EmailAddress = email;
                             var cno = contactNo;
                             if (cno.Length > 20)
                                 cno = contactNo.Substring(0, 20);
                             cont.ContactNumber = cno;
                             cont.Status = "Active";
                             cont.CreatedDate = DateTime.Now;
                             cont.ModifiedDate = DateTime.Now;
                             //customer user type
                             cont.SID = 7;
                             cont.ResetPassword = false;
                             cont.IsImage = false;

                             ud.Contractors.InsertOnSubmit(cont);
                             ud.SubmitChanges();
                             //update password
                             PortfolioContactLoginDeatilsBAL.PortfolioContactLoginDeatils_AddUpdate(contactid, cont.ID, cont.LoginName, password);
                             AssignedCustomerToPortfolio actp = pd.AssignedCustomerToPortfolios.Where(a => a.Portfolio == portfolioid && a.CustomerID == cont.ID).Select(a => a).FirstOrDefault();
                             if (actp == null)
                             {
                                 AssignedCustomerToPortfolio acp = new AssignedCustomerToPortfolio();
                                 acp.Portfolio = portfolioid;
                                 acp.CustomerID = cont.ID;
                                 pd.AssignedCustomerToPortfolios.InsertOnSubmit(acp);
                                 pd.SubmitChanges();
                             }
                             contactUsers = new PortfolioContactAssociate();
                             contactUsers.ContactID = contactid;
                             contactUsers.CustomerUserID = cont.ID;
                             pd.PortfolioContactAssociates.InsertOnSubmit(contactUsers);
                             pd.SubmitChanges();
                             //Add customer user to Assoicate Contact table
                             // DC.BLL.CustomerDetailsBAL.PortfolioContactAssociate_Insert(cont.ID, sessionKeys.PortfolioID);

                             
                             //Mail to New Contractors
                             //LoginDetailsMail(cont.ContractorName, cont.LoginName, password, cont.EmailAddress);
                             //enable login to portal
                             pcontact.LogintoPortal = true;
                             pd.SubmitChanges();


                         }
                         else
                         {
                            
                             //check portfolio associate is working
                             AssignedCustomerToPortfolio actp = pd.AssignedCustomerToPortfolios.Where(a => a.Portfolio == portfolioid && a.CustomerID == contactid).Select(a => a).FirstOrDefault();
                             if (actp == null)
                             {
                                 AssignedCustomerToPortfolio acp = new AssignedCustomerToPortfolio();
                                 acp.Portfolio = portfolioid;
                                 acp.CustomerID = contactid;
                                 pd.AssignedCustomerToPortfolios.InsertOnSubmit(acp);
                                 pd.SubmitChanges();
                             }
                             // IF InActive customer User is ther make active
                             UserMgt.Entity.Contractor Contractor_update = ud.Contractors.Where(c => c.ID == contactid).FirstOrDefault();
                             if (Contractor_update != null)
                             {
                                 //reset password
                                 password = Membership.GeneratePassword(8, 0);
                                 Contractor_update.Password = password;
                                 ud.SubmitChanges();
                                 PortfolioContactLoginDeatilsBAL.PortfolioContactLoginDeatils_AddUpdate(contactid, Contractor_update.ID, Contractor_update.LoginName, password);
                                 //Mail to New Contractors
                                 //LoginDetailsMail(Contractor_update.ContractorName, Contractor_update.LoginName, password, Contractor_update.EmailAddress);
                                 
                                 if (Contractor_update.Status == "InActive")
                                 {
                                     Contractor_update.Status = "Active";
                                     ud.SubmitChanges();
                                 }


                                
                                
                                 //enable login to portal
                                 //pcontact.LogintoPortal = true;
                                 //pd.SubmitChanges();

                             }
                             //Add customer user to Assoicate Contact table
                             //DC.BLL.CustomerDetailsBAL.PortfolioContactAssociate_Insert(contactid, sessionKeys.PortfolioID);
                         }
                     }
                 }


             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
             }

         }

         public string GetUserName(List<UserMgt.Entity.Contractor> userlist, string UserName)
         {
             string retVal = string.Empty;
             bool checkUserExist = false;
             int i = 1;
             while (!checkUserExist)
             {
                 int cnt = userlist.Where(p => p.LoginName == UserName).Count();
                 if (cnt > 0)
                 {

                     UserName = UserName + i.ToString();
                     retVal = UserName;
                     checkUserExist = false;
                     i++;
                 }
                 else
                 {
                     retVal = UserName;
                     checkUserExist = true;
                 }
             }



             return retVal;
         }

         public void LoginDetailsMail(string name, string uname, string password, string toEmail)
         {
             try
             {
                 string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                 string subject = "Welcome to your Liberty Home Guard Portal";
                 Emailer em = new Emailer();
                 string body = em.ReadFile("~/WF/DC/EmailTemplates/ContractorWelcomeMail.htm");
                 body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
                 body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                 body = body.Replace("[user]", name);
                 body = body.Replace("[username]", uname);
                 body = body.Replace("[password]", password);
                 body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                 em.SendingMail(fromemailid, subject, body, toEmail);
             }
             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
             }
         }

         public int CreateAddress(int contactid,
             string address, string city, string state, string postcode,
             string bname, string baddress, string bcity, string bstate, string bpostcode,
             string policytype, string subscriptiontype, string amount, string addons,
             string PropertyType, string Other, bool IsLessThan5KSqft, string ordernumber, string paypalreference)
         {

             int addressid = 0;
             IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress> paRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactAddress>();
             IPortfolioRepository<PortfolioMgt.Entity.PortfolioContact> pRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContact>();
             IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> ppRes = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();
             IPortfolioRepository<PortfolioMgt.Entity.PolicyStartsIn> pstartRes = new PortfolioRepository<PortfolioMgt.Entity.PolicyStartsIn>();
             IPortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate> passRes = new PortfolioRepository<PortfolioMgt.Entity.ProductAddonPriceAssociate>();
             IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail> payRes = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
             var ptypelist = ppRes.GetAll().ToList();
             var contact = pRes.GetAll().Where(o => o.ID == contactid).FirstOrDefault();
             //check order number should not exists
             if (payRes.GetAll().Where(o => o.OrderRef == ordernumber).Count() == 0)
             {
                 if (contact != null)
                 {
                     //var adItem = paRes.GetAll().Where(o => o.ContactID == contact.ID).Where(o => o.Address.ToLower() == address.ToLower() && o.State.ToLower() == state.ToLower() && o.City.ToLower() == city.ToLower() && o.PostCode.ToLower() == postcode.ToLower()).FirstOrDefault();
                     //if (adItem == null)
                     //{
                     var adItem = new PortfolioContactAddress();
                     adItem.ContactID = contactid;

                     adItem.BillingName = bname;

                     adItem.Address = address;
                     adItem.BillingAddress1 = baddress;

                     adItem.City = city;
                     adItem.BillingCity = bcity;

                     adItem.State = state;
                     adItem.BillingState = bstate;

                     adItem.PostCode = postcode;
                     adItem.BillingZipcode = bpostcode;

                     adItem.PolicyTypeID = ptypelist.Where(o => o.Title.ToLower() == policytype.Trim().ToLower()).FirstOrDefault().ID;
                     adItem.PolicyNumber = WSGetPolicyno(adItem.PolicyTypeID.Value);
                     //Policy start from 
                     var psDate = pstartRes.GetAll().Where(o => o.PSIID == 1).FirstOrDefault();
                     adItem.PolicyStartsID = 1;

                     adItem.StartDate = Convert.ToDateTime(DateTime.Now).AddDays(psDate.Value);

                     //1. monthly
                     //2. Yearly
                     if (subscriptiontype == "1")
                         adItem.ExpiryDate = adItem.StartDate.Value.AddMonths(1);
                     else
                         adItem.ExpiryDate = adItem.StartDate.Value.AddYears(1);

                     adItem.ContractTermID = 2;
                     adItem.Amount = Convert.ToDouble(amount);
                     adItem.LoggedDatetime = DateTime.Now;

                     adItem.PropertyType = PropertyType;
                     adItem.Other = Other;
                     adItem.IsLessThan5KSqft = IsLessThan5KSqft;
                     
                     paRes.Add(adItem);
                     addressid = adItem.ID;

                     try
                     {
                         string[] aitem = addons.Split(',');
                         foreach (var a in aitem)
                         {
                             if (!string.IsNullOrEmpty(a))
                             {
                                 if (passRes.GetAll().Where(o => o.AddressID == addressid && o.AddonID == Convert.ToInt32(a)).Count() == 0)
                                     passRes.Add(new ProductAddonPriceAssociate()
                                     {
                                         AddonID = Convert.ToInt32(a),
                                         AddressID = addressid
                                     });
                             }
                         }
                     }
                     catch (Exception ex)
                     {
                         LogExceptions.WriteExceptionLog(ex);
                     }

                     try
                     {
                         var payEntity = payRes.GetAll().Where(o => o.AddressID == addressid).FirstOrDefault();
                         if (payEntity == null)
                         {
                             payEntity = new PortfolioContactPaymentDetail();
                             payEntity.AddressID = addressid;
                             payEntity.IsPaid = true;
                             payEntity.PayPalRef = paypalreference;
                             payEntity.OrderRef = ordernumber;
                             payEntity.PayDate = DateTime.Now;
                             payEntity.PayOnWebsite = true;
                             payEntity.PaidAmount = adItem.Amount.Value;
                             payRes.Add(payEntity);

                             //Generate policy
                             try
                             {
                                 GeneratePolicy.SendPolicyMail(addressid);
                             }
                             catch(Exception ex)
                             {
                                 LogExceptions.WriteExceptionLog(ex);
                             }

                         }
                         //}
                         //else
                         //{
                         //    addressid = adItem.ID;
                         //}
                     }
                     catch (Exception ex)
                     {
                         LogExceptions.WriteExceptionLog(ex);
                     }
                 }
             }
             return addressid;
         }

        public string WSGetPolicyno(int policytypeid)
        {
            string retval = string.Empty;
            using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
                {
                    //policy default data
                    var pdata = pd.PolicyNumberFormatSettings.FirstOrDefault();

                    if (!string.IsNullOrEmpty(policytypeid.ToString()))
                    {
                        //policy type prefix
                        var ptdata = pd.ProductPolicyTypes.Where(o => o.ID == Convert.ToInt32(policytypeid.ToString())).FirstOrDefault();
                        var ptaddress = pd.PortfolioContactAddresses.OrderByDescending(o => o.ID).FirstOrDefault();
                        //PolicyMax no
                        if (ptdata != null)
                        {
                            if (ptaddress == null)
                                retval = string.Format("{0}{1}", ptdata.PolicyTypePrefix, (pdata.Seed).ToString("D6"));
                            //retval = string.Format("{0} - {1} - {2}", pdata.Prefix, ptdata.PolicyTypePrefix, (pdata.Seed).ToString("D6"));
                            else
                            {
                                // ptdata.PolicyTypePrefix
                                int pno = 0;
                                string policyno = ptaddress.PolicyNumber;
                                policyno = ptaddress.PolicyNumber.Replace(ptdata.PolicyTypePrefix, "");
                                //string[] split = policyno.Split('-').ToArray();
                                //if (split.Count() > 2)
                                //{
                                try
                                {
                                    pno = Convert.ToInt32(policyno);
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                    pno = 0;
                                }
                                // }
                                retval = string.Format("{0}{1}", ptdata.PolicyTypePrefix, (pno + pdata.Seed).ToString("D6"));
                            }
                        }
                    }
                }
            }

            return retval;

        }
        #endregion 
    }
}
