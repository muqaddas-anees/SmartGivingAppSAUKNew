using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Net.Http.Formatting;
using System.Web;

using DataTables;
//using DeffinityAppDev.Models;

namespace DeffinityAppDev.Controllers
{
    public class JoinController : ApiController
    {
        [Route("api/join")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Join()
        {
            var request = HttpContext.Current.Request;
            //var settings = Properties.Settings.Default;
            string wid = string.Empty;
            string pid = string.Empty;
            if (request.Form["action"] == null)
            {
                wid = request.Form["worksheetid"].ToString();
                pid = request.Form["project"].ToString();
            }
            else if (request.Form["action"] == "create" || request.Form["action"] == "edit" || request.Form["action"] == "remove")
            {
                pid = request.Form[1].ToString();
                wid = request.Form[2].ToString();
            }
            using (var db = new Database("sqlserver", Constants.DBString))
            {
                var response = new Editor(db, "ProjectBOM", "ID")
                //.Debug(true)
                    .Model<ProjectMgt.Entity.V_ProjectTaskDetail>()
                     .Field(new Field("ProjectBOM.Description")
                     .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("ProjectBOM.CurrencyID")
                        .Options(new Options()
                            .Table("CurrencyList")
                            .Value("ID")
                            .Label("CurrencyName")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .Field(
                    new Field("ProjectBOM.Supplier")
                        .Options(new Options()
                            .Table("v_Vendors")
                            .Value("VendorID")
                            .Label("ContractorName")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                     .Field(
                    new Field("ProjectBOM.ManufactureID")
                        .Options(new Options()
                            .Table("Manufacturer")
                            .Value("id")
                            .Label("Name")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                     .Field(
                    new Field("ProjectBOM.ID")
                        .Options(new Options()
                            .Table("GoodsReceipt")
                            .Value("ID")
                            .Label("QtyReceived")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("CurrencyList", "CurrencyList.ID", "=", "ProjectBOM.CurrencyID")
                    .LeftJoin("v_Vendors", "v_Vendors.VendorID", "=", "ProjectBOM.Supplier")
                    .LeftJoin("Manufacturer", "Manufacturer.id", "=", "ProjectBOM.ManufactureID")
                    .LeftJoin("GoodsReceipt", "GoodsReceipt.BOMID", "=", "ProjectBOM.ID")
                   .Where(p => p.Where("ProjectBOM.ProjectReference", pid, "=").AndWhere("ProjectBOM.WorkSheetID", wid, "=")
                       .AndWhere(q => q.Where("GoodsReceipt.ProjectReference", pid, "=").OrWhere("GoodsReceipt.ProjectReference", null, "=")))
                    
                   
                    .Process(request)
                    .Data();

                return Json(response);
            }
        }

        [Route("api/join/join1")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Join1()
        {
            var request = HttpContext.Current.Request;
            //var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", Constants.DBString))
            {
                var response = new Editor(db, "ProjectBOM", "ID")
                    .Model<ProjectMgt.Entity.V_ProjectTaskDetail>()
                    .Field(new Field("ProjectBOM.CurrencyID")
                        .Options(new Options()
                            .Table("CurrencyList")
                            .Value("ID")
                            .Label("CurrencyName")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .Field(
                    new Field("ProjectBOM.Supplier")
                        .Options(new Options()
                            .Table("v_Vendors")
                            .Value("VendorID")
                            .Label("ContractorName")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                     .Field(
                    new Field("ProjectBOM.ManufactureID")
                        .Options(new Options()
                            .Table("Manufacturer")
                            .Value("id")
                            .Label("Name")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                     .Field(
                    new Field("ProjectBOM.ID")
                        .Options(new Options()
                            .Table("GoodsReceipt")
                            .Value("ID")
                            .Label("QtyReceived")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("CurrencyList", "CurrencyList.ID", "=", "ProjectBOM.CurrencyID")
                    .LeftJoin("v_Vendors", "v_Vendors.VendorID", "=", "ProjectBOM.Supplier")
                    .LeftJoin("Manufacturer", "Manufacturer.id", "=", "ProjectBOM.ManufactureID")
                    .LeftJoin("GoodsReceipt", "GoodsReceipt.BOMID", "=", "ProjectBOM.ID")
                   .Where("ProjectBOM.ProjectReference", "120196", "=")
                    .Process(request)
                    .Data();

                return Json(response);
            }
        }
    }
}
