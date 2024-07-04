using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ExceptionClass
/// </summary>

[global::System.Serializable]
public class dataFileIncorrectException : Exception
{
    
    public dataFileIncorrectException() { }
    public dataFileIncorrectException(string message) : base(message) { }
    public dataFileIncorrectException(string message, Exception inner) : base(message, inner) { }
    protected dataFileIncorrectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}


[global::System.Serializable]
public class dataFileArgumentException : Exception
{

    public dataFileArgumentException() { }
    public dataFileArgumentException(string message) : base(message) { }
    public dataFileArgumentException(string message, Exception inner) : base(message, inner) { }
    protected dataFileArgumentException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}

[global::System.Serializable]
public class databaseConnectionException : Exception
{
   

    public databaseConnectionException() { }
    public databaseConnectionException(string message) : base(message) { }
    public databaseConnectionException(string message, Exception inner) : base(message, inner) { }
    protected databaseConnectionException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}
[global::System.Serializable]
public class dataBaseException : Exception
{
    
    public dataBaseException() { }
    public dataBaseException(string message) : base(message) { }
    public dataBaseException(string message, Exception inner) : base(message, inner) { }
    protected dataBaseException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}

[global::System.Serializable]
public class moreColumnsException : Exception
{
    
    public moreColumnsException() { }
    public moreColumnsException(string message) : base(message) { }
    public moreColumnsException(string message, Exception inner) : base(message, inner) { }
    protected moreColumnsException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}

[global::System.Serializable]
public class genericException : Exception
{
    public genericException() { }
    public genericException(string message) : base(message) { }
    public genericException(string message, Exception inner) : base(message, inner) { }
    protected genericException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}
[global::System.Serializable]
public class queryStringException : Exception
{

    public queryStringException() { }
    public queryStringException(string message) : base(message) { }
    public queryStringException(string message, Exception inner) : base(message, inner) { }
    protected queryStringException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}