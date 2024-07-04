using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Entity;
using DC.DAL;
using DC.BAL;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace DC.BLL
{
    public class QuotationBAL
    {
        public static string policydisount = "Policy Discount";
        public static string policy = "Policy";
        //if you delete policy  then delete policy discount field
        public static void QuotationItem_DeletePlanDiscount(int CallID,int OptionID)
        {
            IDCRespository<QuotationItem> qiRep = new DCRepository<QuotationItem>();
            var vData = qiRep.GetAll().Where(o => o.CallidID == CallID && o.QuotationOptionID == OptionID && o.PolicyNotes.Contains(policy) ).ToList();
            if(vData.Count >0)
            {
                if(vData.Count == 1)
                {
                    var v = vData.Where(o => o.PolicyNotes == policydisount).FirstOrDefault();
                    if(v != null)
                    {
                        qiRep.Delete(v);
                    }
                }
            }
        }

        //public static string[] optionDefaults = { "Parts & Labor", "Replace Unit", "Upgrade Unit" };
        public static string[] optionDefaults = { "Parts & Labor" };
        //public static string[] optionDefaults = { "Option 1: Parts & Labor" };
        //Create Default Options
        public static void AddDefault_Options(int CallID,bool updateItems=false)
        {
            try
            {
                //if (QuotationOptionsBAL.QuotationOption_Select(CallID).Count() == 0)
                //{
                //    //check Option 
                //    foreach (var q in optionDefaults)
                //    {
                //       var qEntity = QuotationOptionsBAL.QuotationOption_Add(new QuotationOption() { CallID = CallID, CustomerID = sessionKeys.PortfolioID, OptionName = q });
                //        //insert default BOM data
                //        //if (qEntity != null)
                //        //{
                //        //    if (q == "Option 1: Parts & Labor")
                //        //        QuoteImportBOM(CallID, qEntity.ID);
                //        //}

                //        var takeFist = QuotationOptionsBAL.QuotationOption_Select(CallID).FirstOrDefault();
                //        if (takeFist != null)
                //        {
                //            QuoteImportBOM(CallID, takeFist.ID);
                //        }
                //    }
                //}
                //else
                //{
                //    if (updateItems)
                //    {
                //        foreach (var q in optionDefaults)
                //        {
                //            var qEntity = QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).Where(o => o.OptionName == q).FirstOrDefault();
                //            //insert default BOM data
                //            //if (qEntity != null)
                //            //{
                //            //    //if (optionDefaults[0] == q)
                //            //    if (q == "Option 1: Parts & Labor")
                //            //        QuoteImportBOM(CallID, qEntity.ID);
                //            //}

                //            var takeFist = QuotationOptionsBAL.QuotationOption_Select(CallID).FirstOrDefault();
                //            if (takeFist != null)
                //            {
                //                QuoteImportBOM(CallID, takeFist.ID);
                //            }
                //        }
                //    }
                //}

               
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //Import BOM data to Quote section
        public static void QuoteImportBOM(int CallID, int OptionID)
        {
            var option1 = optionDefaults[0];

            var f = QuotationOptionsBAL.QuotationOption_Select(CallID).Where(o => o.OptionName == option1).FirstOrDefault();
            if(f != null)
            {
                //insert bom items
                IDCRespository<DC.Entity.BOMItem> bRep = new DCRepository<DC.Entity.BOMItem>();
                IDCRespository<DC.Entity.QuotationItem> qRep = new DCRepository<DC.Entity.QuotationItem>();
                            

                
                var bItem = bRep.GetAll().Where(o => o.CallID == CallID).ToList();
                if(bItem.Count >0)
                {
                    var qlist = qRep.GetAll().Where(o => o.CallidID == CallID && o.QuotationOptionID == OptionID).ToList();
                    if (qlist.Count > 0)
                    {
                        foreach (var q in qlist)
                            qRep.Delete(q);
                    }
                    foreach (var v in bItem)
                    {
                        //check item exists
                        //var qOldItems = qRep.GetAll().Where(o=>CallID == QueryStringValues.CallID && o.QuotationOptionID == OptionID && o.ServiceDescription== v.ServiceDescription).FirstOrDefault()
                        //if (qOldItems == null)
                        //{
                        var qty = Convert.ToDouble(v.QTY.HasValue ? v.QTY.Value : 0);
                        var cost = Convert.ToDouble(v.SellingPrice.HasValue ? v.SellingPrice.Value : 0.00);
                        var margin = PortfolioMgt.BAL.PortolioMarginBAL.PortolioMarginBAL_SelectMargin();
                        var sales = 0.00;
                        if (margin > 0)
                            sales = ((cost * qty) + ((cost * qty) * (margin / 100)));
                        else
                            sales = (cost * qty);
                        //var applyVAT = true;
                        //var vt = 0.00;
                        //if (applyVAT)
                        //{
                        //    vt = VATByCustomerBAL.VATByCustomer_select();
                        //}
                        //var ve = (sales);
                        //if (v.VATRatevt > 0)
                        //    ve = (sales) * (vt / 100);
                        //else
                        //    ve = 0.00;

                        var q = new DC.Entity.QuotationItem();
                            q.CallidID = v.CallID;
                            q.FixedRateTypeID = v.FixedRateTypeID;
                            q.Notes = v.Notes;
                            q.QTY = qty;
                            q.QuotationOptionID = OptionID;
                            q.SellingPrice = cost;
                            q.Markup = margin;
                            q.SalesPrice = sales;// ( (v.SellingPrice.HasValue ? v.SellingPrice.Value : 0.00) * (v.QTY.HasValue ? v.QTY.Value : 0) )+ v.VAT;
                            q.ServiceDescription = v.ServiceDescription;
                            q.ServiceID = v.ServiceID;
                            q.ServiceTypeID = v.ServiceTypeID;
                            q.Type = v.Type;
                            q.VAT = v.VAT;
                            q.Image = v.Image;
                            q.VATRate = v.VATRate;
                            qRep.Add(q);
                        //}
                    }
                }

                //insert bom price 
                IDCRespository<DC.Entity.QuotationPrice> qpRep = new DCRepository<DC.Entity.QuotationPrice>();
                IDCRespository<DC.Entity.BOMPrice> bpRep = new DCRepository<DC.Entity.BOMPrice>();
                var bp = bpRep.GetAll().Where(o => o.CallID == CallID).FirstOrDefault();
                if (bp != null)
                {
                    var q = new DC.Entity.QuotationPrice();
                    q.CallID = bp.CallID;
                    q.DiscountPercent = bp.DiscountPercent;
                    q.DiscountPrice = bp.DiscountPrice;
                    q.LoggedBy = sessionKeys.UID;
                    q.ModifiedDate = DateTime.Now;
                    q.Notes = bp.Notes;
                    q.OriginalPrice = bp.OriginalPrice.HasValue?bp.OriginalPrice.Value:0.00;
                    q.QuotationOptionID = OptionID;
                    q.RevicedPrice = bp.RevicedPrice;
                    q.Type = bp.Type;

                    qpRep.Add(q);
                }
            }
        }

        public static QuotationItem QuotationItem_Add(QuotationItem q)
        {
            IDCRespository<QuotationItem> qiRep = new DCRepository<QuotationItem>();
            q.ServiceTypeID = 2;
            q.Type = "FLS";
            qiRep.Add(q);
            return q;
        }
        public static QuotationItem QuotationItem_Update(QuotationItem q)
        {
            IDCRespository<QuotationItem> qiRep = new DCRepository<QuotationItem>();
            var qEntity = qiRep.GetAll().Where(o => o.ID == q.ID).FirstOrDefault();
            if (qEntity != null)
            {
                qEntity.FixedRateTypeID = q.FixedRateTypeID;
                qEntity.Notes = q.Notes;
                qEntity.QTY = q.QTY;
                qEntity.SellingPrice = q.SellingPrice;
                qEntity.ServiceDescription = q.ServiceDescription;
                qEntity.ServiceID = q.ServiceID;
                qEntity.ServiceTypeID = q.ServiceTypeID;
                qEntity.QuotationOptionID = q.QuotationOptionID;
                qEntity.Markup = q.Markup;
                qiRep.Edit(qEntity);
            }
            return q;
        }

        public static int InsertQuoteItem(int ServiceID, int IncidentID, double QTY, int ServiceTypeID, string type, string servicetext, int servicetypeid, double cost, double VAT, string notes, string imgGuid, double SalesPrice,double markup,double vatrate, bool applyVAT = true, int PolicyID = 0,  string PolicyNotes = "",int optionID=0,bool iSAPICall=false)
        {
            //var vt = 0.00;
            //if (applyVAT)
            //{
            //    vt = VATByCustomerBAL.VATByCustomer_select();
            //}
            if(QueryStringValues.OPTION >0)
             optionID = QueryStringValues.OPTION;

            var v = 0.00;

            if (iSAPICall)
            {
                v = VAT;
            }
            else
            {
                if (vatrate > 0)
                    v = (SalesPrice) * (vatrate / 100);
                else
                    v = 0.00;
            }
            SqlParameter OutVal = new SqlParameter("@OutVal", SqlDbType.Int, 8);
            OutVal.Direction = ParameterDirection.Output;
            var nGuid = new Guid(imgGuid);
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Quotation_Item_Insert",
                new SqlParameter("@ServiceID", ServiceID),
                new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@QTY", QTY), new SqlParameter("@Type", type),
                new SqlParameter("@ServiceTypeID", ServiceTypeID),
                new SqlParameter("@ServiceDescription", servicetext),
            new SqlParameter("@FixedRateTypeID", servicetypeid),
            new SqlParameter("@cost", cost),
            new SqlParameter("@VAT", v),
            new SqlParameter("@Option", optionID)
            , new SqlParameter("@Notes", notes)
            , new SqlParameter("@Image", nGuid)
            , new SqlParameter("@PolicyID", PolicyID)
            , new SqlParameter("@PolicyNotes", PolicyNotes)
            , new SqlParameter("@SalesPrice", SalesPrice )
            , new SqlParameter("@Markup", markup)
             , new SqlParameter("@VATRate", vatrate)
                , OutVal);
           

            return int.Parse(OutVal.Value.ToString());


        }

        public static QuotationItem QuotationItem_Select(int id)
        {
            IDCRespository<QuotationItem> qiRep = new DCRepository<QuotationItem>();
            return qiRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
        }
        public static bool QuotationItem_Delete(int id)
        {
            bool retval = false;
            IDCRespository<QuotationItem> qiRep = new DCRepository<QuotationItem>();
            var dEntity = qiRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            qiRep.Delete(dEntity);
            retval = true;
            return retval;
        }
        public static List<QuotationItem> QuotationItem_SelectByCallid(int Callid)
        {
            IDCRespository<QuotationItem> qiRep = new DCRepository<QuotationItem>();
            return qiRep.GetAll().Where(o => o.CallidID == Callid).ToList();
        }
        public static List<QuotationItem> QuotationItem_SelectByOptionid(int Callid,int optionID)
        {
            IDCRespository<QuotationItem> qiRep = new DCRepository<QuotationItem>();
            return qiRep.GetAll().Where(o => o.CallidID == Callid && o.QuotationOptionID == optionID).ToList();
        }
        public static QuotationPrice QuotationPrice_AddUpdate(int callid, double OriginalPrice, double RevicedPrice, double DiscountPercent,double DiscountPrice,int OptionID)
        {
            IDCRespository<QuotationPrice> qiRep = new DCRepository<QuotationPrice>();
            var qEntity = qiRep.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
            if (qEntity == null)
            {
                qEntity = new QuotationPrice();
                qEntity.CallID = callid;
                qEntity.DateLogged = DateTime.Now;
                qEntity.LoggedBy = sessionKeys.UID;
                qEntity.Type = "FLS";
                qEntity.OriginalPrice = OriginalPrice;
                qEntity.RevicedPrice = RevicedPrice;
                qEntity.DiscountPercent = DiscountPercent;
                qEntity.DiscountPrice = DiscountPrice;
                qEntity.QuotationOptionID = OptionID;
                qEntity.ModifiedDate = DateTime.Now;
                qiRep.Add(qEntity);
            }
            else
            {
                qEntity.OriginalPrice = OriginalPrice;
                qEntity.RevicedPrice = RevicedPrice;
                qEntity.DiscountPercent = DiscountPercent;
                qEntity.DiscountPrice = DiscountPrice;
                qEntity.QuotationOptionID = OptionID;
                qEntity.ModifiedDate = DateTime.Now;
                qiRep.Edit(qEntity);
            }

            return qEntity;
        }
        public static QuotationPrice QuotationPrice_AddUpdate(int callid, double OriginalPrice, double RevicedPrice, double DiscountPercent, double DiscountPrice, int OptionID,int userid)
        {
            IDCRespository<QuotationPrice> qiRep = new DCRepository<QuotationPrice>();
            var qEntity = qiRep.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
            if (qEntity == null)
            {
                qEntity = new QuotationPrice();
                qEntity.CallID = callid;
                qEntity.DateLogged = DateTime.Now;
                qEntity.LoggedBy = userid;
                qEntity.Type = "FLS";
                qEntity.OriginalPrice = OriginalPrice;
                qEntity.RevicedPrice = RevicedPrice;
                qEntity.DiscountPercent = DiscountPercent;
                qEntity.DiscountPrice = DiscountPrice;
                qEntity.QuotationOptionID = OptionID;
                qEntity.ModifiedDate = DateTime.Now;
                qiRep.Add(qEntity);
            }
            else
            {
                qEntity.OriginalPrice = OriginalPrice;
                qEntity.RevicedPrice = RevicedPrice;
                qEntity.DiscountPercent = DiscountPercent;
                qEntity.DiscountPrice = DiscountPrice;
                qEntity.QuotationOptionID = OptionID;
                qEntity.ModifiedDate = DateTime.Now;
                qiRep.Edit(qEntity);
            }

            return qEntity;
        }
        public static QuotationPrice QuotationPrice_select(int callid)
        {
            IDCRespository<QuotationPrice> qiRep = new DCRepository<QuotationPrice>();
            return qiRep.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
        }

        public static List<QuotationPrice> QuotationPrice_selectAll(int callid)
        {
            IDCRespository<QuotationPrice> qiRep = new DCRepository<QuotationPrice>();
            return qiRep.GetAll().Where(o => o.CallID == callid).ToList();
        }

        public static List<QuotationPrice> QuotationPrice_selectAllByCompany(int portfolioid)
        {
            IDCRespository<QuotationPrice> qiRep = new DCRepository<QuotationPrice>();
            IDCRespository<CallDetail> cRep = new DCRepository<CallDetail>();
            var cids = cRep.GetAll().Where(o => o.CompanyID == portfolioid).Select(o=>o.ID).ToList();
            return qiRep.GetAll().Where(o => cids.Contains( o.CallID )).ToList();
        }

        public static List<QuotationPrice> QuotationPrice_selectByOptionID(int callid,int optionID)
        {
            IDCRespository<QuotationPrice> qiRep = new DCRepository<QuotationPrice>();
            return qiRep.GetAll().Where(o => o.CallID == callid && o.QuotationOptionID == optionID).ToList();
        }
        public static QuotationPrice QuotationPrice_selectByActiveOption(int callid, int optionID)
        {
            IDCRespository<QuotationPrice> qiRep = new DCRepository<QuotationPrice>();
            return qiRep.GetAll().Where(o => o.CallID == callid && o.QuotationOptionID == optionID && o.IsOptionActive == true).FirstOrDefault();
        }
        public static QuotationPrice QuotationPrice_selectByActiveOption(int callid)
        {
            IDCRespository<QuotationPrice> qiRep = new DCRepository<QuotationPrice>();
            return qiRep.GetAll().Where(o => o.CallID == callid  && o.IsOptionActive == true).FirstOrDefault();
        }


        public static QuotationPrice QuotationPrice_UpdateFinalPrice(int callid,int optionID,double finalPrice)
        {
            IDCRespository<QuotationPrice> qiRep = new DCRepository<QuotationPrice>();
            var p = qiRep.GetAll().Where(o => o.CallID == callid && o.QuotationOptionID == optionID).FirstOrDefault();
            var slist = QuotationBAL.QuotationItem_SelectByOptionid(callid,optionID);
            if (p != null)
            {
                p.FinalPrice = finalPrice - (slist.Count > 0 ? slist.Where(o => o.CallidID == callid && o.QuotationOptionID == optionID).Sum(o => o.VAT.HasValue ? o.VAT.Value : 0) : 0);
                p.FinalPriceIncludeTax = finalPrice ;
                qiRep.Edit(p);
            }

            return p;
        }
    }
}
