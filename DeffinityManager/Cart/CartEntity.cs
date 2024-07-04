using System;
using System.Data;
using System.Configuration;


/// <summary>
/// Summary description for DLiteUserData
/// </summary>
public class CartEntity
{
    int _typeID=0,_productID=0,_quantity=0;
    Guid _userID=Guid.Empty;
    
    public int TypeID
    {
        get { return _typeID; }
        set { _typeID = value; }
    }

    public int ProductID
    {
        get { return _productID; }
        set { _productID = value; }
    }
    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }
    public Guid UserID
    {
        get { return _userID; }
        set { _userID = value; }
    }


    public CartEntity()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
