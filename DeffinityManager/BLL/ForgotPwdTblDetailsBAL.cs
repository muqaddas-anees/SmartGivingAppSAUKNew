using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgt.DAL;
using UserMgt.Entity;

/// <summary>
/// Summary description for ForgotPwdTblDetailsBAL
/// </summary>
public class ForgotPwdTblDetailsBAL
{
	public ForgotPwdTblDetailsBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void InsertRecord(UserMgt.Entity.ForgotPasswordInfo F)
    {
        using (UserDataContext Dc = new UserDataContext())
        {
            Dc.ForgotPasswordInfos.InsertOnSubmit(F);
            Dc.SubmitChanges();
        }
    }
    public ForgotPasswordInfo SelectByVid(string Vid)
    {
        ForgotPasswordInfo F = new ForgotPasswordInfo();
        using (UserDataContext dc = new UserDataContext())
        {
            F = (from a in dc.ForgotPasswordInfos where a.verifyCode == Vid select a).FirstOrDefault();
            return F;
        }
    }
}