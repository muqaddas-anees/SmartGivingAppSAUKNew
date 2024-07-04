using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for ServiceManager
/// </summary>
namespace Deffinity.IncidentService
{
    public class incidentServiceEntity
    {
        #region Member Variables

        int _iD;
        int _serviceID;
        int _incidentID;
        int _qTY;
        int _serviceTypeID;
        double _unitPrice;
        double _buyingPrice;
        double _sellingPrice;

        #endregion

        #region Properties

        public int ID
        {
            set { _iD = value; }
            get { return _iD; }
        }

        public int ServiceID
        {
            set { _serviceID = value; }
            get { return _serviceID; }
        }

        public int IncidentID
        {
            set { _incidentID = value; }
            get { return _incidentID; }
        }

        public int QTY
        {
            set { _qTY = value; }
            get { return _qTY; }
        }

        public int ServiceTypeID
        {
            set { _serviceTypeID = value; }
            get { return _serviceTypeID; }
        }

        public double UnitPrice
        {
            set { _unitPrice = value; }
            get { return _unitPrice; }
        }

        public double BuyingPrice
        {
            set { _buyingPrice = value; }
            get { return _buyingPrice; }
        }

        public double SellingPrice
        {
            set { _sellingPrice = value; }
            get { return _sellingPrice; }
        }

        #endregion

        #region Methods

        public incidentServiceEntity()
        {
        }

        #endregion
    }


    public class ServiceManager
    {
        public ServiceManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void Services_BulkInsert(int IncidentID,string UserID,string type )
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "IncidentService_BulkInsert", new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@UserID", UserID), new SqlParameter("@Type", type));
        }
        public static void Services_Update(int ID, double QTY, double SellingPrice,double UnitConsumption,string notes,string desc, int servicetypeid,double VAT)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "IncidentService_Update", new SqlParameter("@ID", ID), new SqlParameter("@QTY", QTY), new SqlParameter("@SellingPrice", SellingPrice), new SqlParameter("@UnitConsumtion", UnitConsumption), new SqlParameter("@Notes", notes), new SqlParameter("@ServiceDescription",desc),new SqlParameter("@FixedRateTypeID",servicetypeid), new SqlParameter("@VAT", VAT));
        }
        public static void BOM_Update(int ID, double QTY, double SellingPrice, double UnitConsumption, string notes, string desc, int servicetypeid, double VAT,double VATRate)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DCBOMItem_Update", new SqlParameter("@ID", ID), new SqlParameter("@QTY", QTY), new SqlParameter("@SellingPrice", SellingPrice), new SqlParameter("@UnitConsumtion", UnitConsumption), new SqlParameter("@Notes", notes), new SqlParameter("@ServiceDescription", desc), new SqlParameter("@FixedRateTypeID", servicetypeid), new SqlParameter("@VAT", VAT), new SqlParameter("@VATRate", VATRate));
        }
        public static void Quotation_Update(int ID, double QTY, double SellingPrice, double UnitConsumption, string notes, string desc, int servicetypeid,double VAT,Guid _guid,double salesprice,double markup,double VATRate)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "QuotationItem_Update", new SqlParameter("@ID", ID), new SqlParameter("@QTY", QTY), new SqlParameter("@SellingPrice", SellingPrice), new SqlParameter("@UnitConsumtion", UnitConsumption), new SqlParameter("@Notes", notes), new SqlParameter("@ServiceDescription", desc), new SqlParameter("@FixedRateTypeID", servicetypeid), new SqlParameter("@VAT", VAT), new SqlParameter("@Image", _guid), new SqlParameter("@SalesPrice", salesprice), new SqlParameter("@Markup", markup), new SqlParameter("@VATRate", VATRate));
        }
        public static DataTable Services_SelectByIncidentID(int IncidentID,string type,int PriceID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "IncidentService_SelectByIncidentID", new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@Type", type), new SqlParameter("@PriceID", PriceID)).Tables[0];
        }
        public static DataTable BOM_SelectByCallID(int IncidentID, string type)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DCBOM_SelectByIncidentID", new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@Type", type)).Tables[0];
        }
        public static DataTable Quotation_SelectByIncidentID(int IncidentID, string type,int option)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Quotation_SelectByIncidentID", new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@Type", type), new SqlParameter("@Option",option)).Tables[0];
        }

        public static void Quotation_CopyToInvoice(int CallID,int PriceID)
        {
           SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "QuotationCopyToInvoice", new SqlParameter("@CallID", CallID), new SqlParameter("@PriceID", PriceID));
        }
    }
}
