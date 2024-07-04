using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for CartManager
/// </summary>
public class CartManager
{
    public CartManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static int AddToCart(CartEntity _CartEntity)
    {
        //add to cart
        SqlParameter[] sqlParams;
        sqlParams = new SqlParameter[]{ new SqlParameter("@TYPEID",_CartEntity.TypeID),
            new SqlParameter("@PRODUCTID",_CartEntity.ProductID),
            new SqlParameter("@QUANTITY",_CartEntity.Quantity),
            new SqlParameter("@USERID",_CartEntity.UserID)
        };
        return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_ADDTOCART", sqlParams);
    }
    public static int UpdateCart(int Id, int Qty)
    {
        SqlParameter[] sqlParams;
        sqlParams = new SqlParameter[] { new SqlParameter("@ID", Id), new SqlParameter("@Quantity", Qty) };
        return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_UpdateCart", sqlParams);
    }
    public static int UpdateCartNotes(int Id, string Notes)
    {
        SqlParameter[] sqlParams;
        sqlParams = new SqlParameter[] { new SqlParameter("@ID", Id), new SqlParameter("@Notes", Notes) };
        return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_UpdateCart_Notes", sqlParams);
    }
    public static int ClearCart(Guid _userID)
    {
        //clears the cart
        SqlParameter[] sqlParams;
        sqlParams = new SqlParameter[]{ new SqlParameter("@UserID",_userID)
        };
        return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_CLEARCART", sqlParams);
    }

    public static DataRow GetCartSummary(Guid _userID)
    {
        //clears the cart
        SqlParameter[] sqlParams;
        sqlParams = new SqlParameter[]{ new SqlParameter("@UserID",_userID)
        };
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_CARTSUMMARY", sqlParams).Tables[0].Rows[0];
    }

    public static int DeleteCartItem(int _ItemID)
    {//DEFFINITY_DELETECARTITEM
        SqlParameter[] sqlParams;
        sqlParams = new SqlParameter[]{ new SqlParameter("@ID",_ItemID)
        };
        return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_DELETECARTITEM", sqlParams);
    }

}
