using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DeffinityAppDev.WF.test
{
    public partial class Mail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                using (MailMessage mm = new MailMessage("indra@deffinity.com","indra@emsysindia.com"))
                {
                    mm.Subject = "Demo";
                    mm.Body = "test";
                    //if (fuAttachment.HasFile)
                    //{
                    //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                    //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                    //}
                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.smartgiving.app";
                   
                    NetworkCredential NetworkCred = new NetworkCredential("team@smartgiving.app", "85xkfBp#FF");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 465;
                  //  smtp.EnableSsl = true;
                    smtp.Send(mm);
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
                }


                //Email em = new Email();
                //em.SendingMail("indra@deffinity.com", "test", "test", "indra@emsysindia.com","");
            }
            catch(Exception ex)
            { Response.Write(ex.Message); }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            IUserRepository<UserMgt.Entity.Contractor> urep = new UserRepository<UserMgt.Entity.Contractor>();
            IUserRepository<UserMgt.Entity.UserSkill> usrep = new UserRepository<UserMgt.Entity.UserSkill>();
           // IUserRepository<UserMgt.Entity.UserSkill> usrep = new UserRepository<UserMgt.Entity.UserSkill>();

            var userlist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany();
            foreach (var u in userlist)
            {
                var c = urep.GetAll().Where(o=>o.ID == u.ID).FirstOrDefault();
                if (c != null)
                {
                    var cnum = c.ContactNumber.Replace("-", "").Replace("(", "").Replace(")", "").Replace("]", "").Replace("[", "").Replace(" ", "").Trim();
                    c.ContactNumber = cnum;

                    if (!c.ContactNumber.Contains('+'))
                    {
                        if(cnum.Length >0)
                        c.ContactNumber = "+1" + cnum;
                    }
                    urep.Edit(c);

                }

                var usDetails = usrep.GetAll().Where(o => o.UserId == u.ID).FirstOrDefault();
                if (usDetails == null)
                {
                    usDetails = new UserMgt.Entity.UserSkill();
                    usDetails.Notes = "All,";
                    usrep.Add(usDetails);
                }
                else
                {
                    if (usDetails.Notes != null)
                    {
                        if (!usDetails.Notes.ToLower().Contains("all"))
                        {
                            usDetails.Notes = "All," + usDetails.Notes.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"value\":", "").Replace("\"", "").Trim();
                            usrep.Edit(usDetails);
                        }
                        else
                        {
                            usDetails.Notes = usDetails.Notes.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"value\":", "").Replace("\"", "").Trim();
                            usrep.Edit(usDetails);
                        }
                    }

                }
            }

        }
    }
}