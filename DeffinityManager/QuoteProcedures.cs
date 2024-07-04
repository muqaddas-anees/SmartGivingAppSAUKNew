using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QuoteProcedures
/// </summary>

namespace Quote.DAL
{
    public static class QuoteAdminProcs
        {
            public const string QuoteAdminInsertUpdate = "QuoteAdmin_InsertUpdate";
            public const string QuoteAdminSelectByPortfolio = "QuoteAdmin_SelectByPortfolio";
            public const string QuoteAdminSelectID = "QuoteAdmin_GetID";
            
        }
        public static class QuoteMainProcs
        {
            public const string QuoteInsertUpdate = "QuoteMain_InsertUpdate";
            public const string SelectQuoteDetailsByQuoteID = "Quote_SelectQuoteDetailsByQuoteID";
            public const string QuoteInvoiceInsert = "Quote_InsertInvoice";
           
        }
        public static class QuoteItemsProcs
        {
            public const string SupplierReqQuoteItemInsert = "SupplierQuoteItems_Insert";
            //added new sp for updating quote --- Giri
            public const string SupplierReqQuoteItemUpdate = "SupplierQuoteItem_Update";
            public const string SupplierReqQuoteItemDelete = "SupplierQuoteItems_Delete";
            public const string QuoteItemInsert = "QuoteItems_Insert";
            public const string QuoteItemUpdate = "QuoteItems_Update";
            public const string QuoteItemDelete = "QuoteItems_Delete";
            public const string QuoteItemsSelectByQuoteID = "Quote_SelectQuoteItemsByQuoteID";
            public const string QuoteItemsSelectByProject_Worksheet = "Quote_SelectQuoteItemsByProject_worksheet";
            public const string QuoteMainUpdate = "QUOTE_MAIN_Update";
        }
}
