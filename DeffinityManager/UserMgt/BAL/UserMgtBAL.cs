using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using UserMgt.Entity;

namespace UserMgt.BAL
{
    public class UserMgtBAL
    {
        public static void User_SaveImageData(int userid, byte[] imagedata)
        {
            IUserRepository<UserMgt.Entity.Contractor> pRep = new UserRepository<UserMgt.Entity.Contractor>();
            var p = pRep.GetAll().Where(o => o.ID == userid).FirstOrDefault();
            if (p != null)
            {
                p.ImageData = imagedata;
                pRep.Edit(p);
            }
        }
        public static bool AddOrUpdateMembers(string email, string firstname, string lastname, string contactno, string address="", string town="", string state="", string zipcode="", string eventname="", string eventstatus="")
        {
            bool retval = false;
            int userid = 0;
            try
            {
               

                if (email.Length > 0)
                {
                    var cRep = new UserRepository<Contractor>();
                    var uRep = new UserRepository<UserDetail>();
                    var cvRep = new UserRepository<v_contractor>();
                    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o=>o.CompanyID == sessionKeys.PortfolioID).Where( o => o.LoginName.ToLower().Trim() == email.ToLower().Trim() && o.SID == UserType.Donor).FirstOrDefault();
                    if (cDetails == null)
                    {

                        cRep = new UserRepository<Contractor>();
                        cvRep = new UserRepository<v_contractor>();

                        var value = new UserMgt.Entity.Contractor();
                        value.ContractorName = firstname.Trim() ;
                        value.LastName = lastname.Trim();
                        value.EmailAddress = email;
                        value.LoginName = email.ToLower().Trim();
                        var pw = "SMG@2022";
                        value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                        value.SID = 2;
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = "";
                        if (contactno.Length > 0)
                            value.ContactNumber = contactno;

                        cRep.Add(value);


                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();


                        var cdEntity = new UserMgt.Entity.UserDetail();
                        cdEntity.Address1 = address;
                        cdEntity.Country = Convert.ToInt32( Deffinity.systemdefaults.GetCoutryID());
                        cdEntity.PostCode = zipcode;
                        cdEntity.State = state;
                        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        cdEntity.Town = town;
                        cdEntity.UserId = value.ID;
                        cdEntity.DenominationDetailsID = 0;
                        cdEntity.SubDenominationDetailsID = 0;

                        cdRep.Add(cdEntity);

                        userid = value.ID;
                    }



                    //update company
                    var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                    if (urRep.GetAll().Where(o => o.UserID == userid && o.CompanyID == sessionKeys.PortfolioID).Count() == 0)
                    {
                        var urEntity = new UserMgt.Entity.UserToCompany();
                        urEntity.CompanyID = sessionKeys.PortfolioID;
                        urEntity.UserID = userid;
                        urRep.Add(urEntity);
                    }

                    var tags = "";
                    var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == userid).FirstOrDefault();
                    if (ud == null)
                    {
                        string toadd = "All,";
                        //if (eventstatus == "Pending")
                        //{
                        //    toadd = eventname + " - Not Attended";
                        //}
                        //else if (eventstatus == "Attended")
                        //{
                        //    toadd = eventname + " - Attended";
                        //}
                        var notes = toadd;// "[{\"value\":\"" + toadd + "\"}]";


                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = notes, UserId = userid });
                    }
                    else
                    {
                        var exitingNotes = ud.Notes;
                        if (!exitingNotes.Contains("All"))
                        {
                            string toadd = "All";
                            //if (eventstatus == "Pending")
                            //{
                            //    toadd = eventname + " - Not Attended";
                            //}
                            //else if (eventstatus == "Attended")
                            //{
                            //    toadd = eventname + " - Attended";
                            //}

                            exitingNotes = exitingNotes.Contains("All") == false ? exitingNotes + "All," : exitingNotes;
                        }


                        ud.Notes = exitingNotes;
                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Update(ud);
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return userid>0?true:false;
        }

        public static UserMgt.Entity.Contractor AddOrUpdateFundriaser(string email, string firstname, string lastname, string contactno,string password, string address = "", string town = "", string state = "", string zipcode = "", string eventname = "", string eventstatus = "",int portfolioid=0)
        {
            bool retval = false;
            int userid = 0;
            var value = new UserMgt.Entity.Contractor();
            try
            {


                if (email.Length > 0)
                {
                    var cRep = new UserRepository<Contractor>();
                    var uRep = new UserRepository<UserDetail>();
                    var cvRep = new UserRepository<v_contractor>();
                    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.LoginName.ToLower().Trim() == email.ToLower().Trim() && o.SID != UserType.Donor).FirstOrDefault();
                    if (cDetails == null)
                    {

                        cRep = new UserRepository<Contractor>();
                        cvRep = new UserRepository<v_contractor>();

                       
                        value.ContractorName = firstname.Trim();
                        value.LastName = lastname.Trim();
                        value.EmailAddress = email;
                        value.LoginName = email.ToLower().Trim();
                        var pw = password;
                        value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                        value.SID = UserType.Fundraiser;
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = "";
                        if (contactno.Length > 0)
                            value.ContactNumber = contactno;

                        cRep.Add(value);


                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();


                        var cdEntity = new UserMgt.Entity.UserDetail();
                        cdEntity.Address1 = address;
                        cdEntity.Country = Convert.ToInt32(Deffinity.systemdefaults.GetCoutryID());
                        cdEntity.PostCode = zipcode;
                        cdEntity.State = state;
                        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        cdEntity.Town = town;
                        cdEntity.UserId = value.ID;
                        cdEntity.DenominationDetailsID = 0;
                        cdEntity.SubDenominationDetailsID = 0;

                        cdRep.Add(cdEntity);



                        userid = value.ID;


                        //update company
                        if (portfolioid > 0)
                        {
                            var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                            if (urRep.GetAll().Where(o => o.UserID == userid && o.CompanyID == portfolioid).Count() == 0)
                            {
                                var urEntity = new UserMgt.Entity.UserToCompany();
                                urEntity.CompanyID = portfolioid;
                                urEntity.UserID = userid;
                                urRep.Add(urEntity);
                            }
                        }
                        else
                        {
                            var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                            if (urRep.GetAll().Where(o => o.UserID == userid && o.CompanyID == sessionKeys.PortfolioID).Count() == 0)
                            {
                                var urEntity = new UserMgt.Entity.UserToCompany();
                                urEntity.CompanyID = sessionKeys.PortfolioID;
                                urEntity.UserID = userid;
                                urRep.Add(urEntity);
                            }
                        }
                    }



                  

                    var tags = "";
                    var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == userid).FirstOrDefault();
                    if (ud == null)
                    {
                        string toadd = "All,";
                        //if (eventstatus == "Pending")
                        //{
                        //    toadd = eventname + " - Not Attended";
                        //}
                        //else if (eventstatus == "Attended")
                        //{
                        //    toadd = eventname + " - Attended";
                        //}
                        var notes = toadd;// "[{\"value\":\"" + toadd + "\"}]";


                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = notes, UserId = userid });
                    }
                    else
                    {
                        var exitingNotes = ud.Notes;
                        if (!exitingNotes.Contains("All"))
                        {
                            string toadd = "All";
                            //if (eventstatus == "Pending")
                            //{
                            //    toadd = eventname + " - Not Attended";
                            //}
                            //else if (eventstatus == "Attended")
                            //{
                            //    toadd = eventname + " - Attended";
                            //}

                            exitingNotes = exitingNotes.Contains("All") == false ? exitingNotes + "All," : exitingNotes;
                        }


                        ud.Notes = exitingNotes;
                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Update(ud);
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return value;
        }

    }
}
