using DC.DAL;
using DC.Entity;
using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using ProjectMgt.BAL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using PortfolioMgt.BAL;

namespace DeffinityManager.DC.BLL
{
    public class DefaultConfigurationToAllCustomer
    {
        public void InsertSubjectData(List<Subject> S_List,int C_Record_ID)
        {
            try
            {
                using (DCDataContext Ddc = new DCDataContext())
                {
                    Subject s = null;
                    List<Subject> NewInsertedList = new List<Subject>();
                    foreach (Subject S_New in S_List)
                    {
                        var Checking = Ddc.Subjects.Where(a => a.SubjectName.ToLower() == S_New.SubjectName.ToLower() && a.CustomerID == C_Record_ID).FirstOrDefault();
                        if (Checking == null)
                        {
                            s = new Subject();
                            s.SubjectName = S_New.SubjectName;
                            s.CustomerID = C_Record_ID;
                            NewInsertedList.Add(s);
                        }
                    }
                    if (NewInsertedList.Count > 0)
                    {
                        Ddc.Subjects.InsertAllOnSubmit(NewInsertedList);
                        Ddc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void InsertOurSitesData(List<OurSite> S_List, int C_Record_ID)
        {
            try
            {
                using (DCDataContext Ddc = new DCDataContext())
                {
                    OurSite s = null;
                    List<OurSite> NewInsertedList = new List<OurSite>();
                    foreach (OurSite S_New in S_List)
                    {
                        var Checking = Ddc.OurSites.Where(a => a.Name.ToLower() == S_New.Name.ToLower() && a.CustomerID == C_Record_ID).FirstOrDefault();
                        if (Checking == null)
                        {
                            s = new OurSite();
                            s.Name = S_New.Name;
                            s.CustomerID = C_Record_ID;
                            NewInsertedList.Add(s);
                        }
                    }
                    if (NewInsertedList.Count > 0)
                    {
                        Ddc.OurSites.InsertAllOnSubmit(NewInsertedList);
                        Ddc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void InsertFlsSourceOfReqData(List<FLSSourceOfRequest> S_List, int C_Record_ID)
        {
            try
            {
                using (DCDataContext Ddc = new DCDataContext())
                {
                    FLSSourceOfRequest s = null;
                    List<FLSSourceOfRequest> NewInsertedList = new List<FLSSourceOfRequest>();
                    foreach (FLSSourceOfRequest S_New in S_List)
                    {
                        var Checking = Ddc.FLSSourceOfRequests.Where(a => a.Name.ToLower() == S_New.Name.ToLower() && a.CustomerID == C_Record_ID).FirstOrDefault();
                        if (Checking == null)
                        {
                            s = new FLSSourceOfRequest();
                            s.Name = S_New.Name;
                            s.CustomerID = C_Record_ID;
                            NewInsertedList.Add(s);
                        }
                    }
                    if (NewInsertedList.Count > 0)
                    {
                        Ddc.FLSSourceOfRequests.InsertAllOnSubmit(NewInsertedList);
                        Ddc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void InsertAssignedDepartmentData(List<AssignedDepartment> D_List, int C_Record_ID)
        {
            try
            {
                using (DCDataContext Ddc = new DCDataContext())
                {
                    AssignedDepartment s = null;
                    List<AssignedDepartment> NewInsertedList = new List<AssignedDepartment>();
                    foreach (AssignedDepartment S_New in D_List)
                    {
                        var Checking = Ddc.AssignedDepartments.Where(a => a.DepartmentName.ToLower() == S_New.DepartmentName.ToLower() && a.CustomerID == C_Record_ID).FirstOrDefault();
                        if (Checking == null)
                        {
                            s = new AssignedDepartment();
                            s.DepartmentName = S_New.DepartmentName;
                            s.CustomerID = C_Record_ID;
                            Ddc.AssignedDepartments.InsertOnSubmit(s);
                            Ddc.SubmitChanges();
                        }
                        List<DepartmentUser> D_UserList = Ddc.DepartmentUsers.Where(a => a.DeptId == S_New.ID).ToList();
                        foreach (DepartmentUser D_User in D_UserList)
                        {
                            DepartmentUser Checking_Sub = new DepartmentUser();
                            if (Checking == null)
                            {
                                Checking_Sub = Ddc.DepartmentUsers.Where(a => a.DeptId == s.ID && a.CustomerID == C_Record_ID).FirstOrDefault();
                            }
                            else
                            {
                                Checking_Sub = Ddc.DepartmentUsers.Where(a => a.DeptId == Checking.ID && a.CustomerID == C_Record_ID).FirstOrDefault();
                            }
                            if (Checking_Sub == null)
                            {
                                DepartmentUser Dept = new DepartmentUser();
                                Dept.UserIds = D_User.UserIds;
                                if (Checking == null)
                                {
                                    Dept.DeptId = s.ID;
                                }
                                else
                                {
                                    Dept.DeptId = Checking.ID;
                                }
                                Dept.CustomerID = C_Record_ID;
                                Ddc.DepartmentUsers.InsertOnSubmit(Dept);
                                Ddc.SubmitChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void InsertDistributionList(EmailSendingType E_Mail, int C_Record_ID, List<Manager> M_List)
        {
            try
            {
                Manager M_New = null;
                List<Manager> Manager_List = new List<Manager>();
                using (DCDataContext Ddc = new DCDataContext())
                {
                    if (E_Mail != null)
                    {
                        EmailSendingType Checking = Ddc.EmailSendingTypes.Where(a => a.CustomerId == C_Record_ID).FirstOrDefault();
                        if (Checking == null)
                        {
                            EmailSendingType E_Mail_New = new EmailSendingType();
                            E_Mail_New.EmailType = E_Mail.EmailType;
                            E_Mail_New.CustomerId = C_Record_ID;
                            Ddc.EmailSendingTypes.InsertOnSubmit(E_Mail_New);
                            Ddc.SubmitChanges();
                        }
                        else
                        {
                            Checking.EmailType = E_Mail.EmailType;
                            Ddc.SubmitChanges();
                        }
                    }


                    foreach (Manager M_Record in M_List)
                    {
                        var Checking_M = Ddc.Managers.Where(a => a.UserID == M_Record.UserID && a.CustomerID == C_Record_ID && a.RequestTypeID == M_Record.RequestTypeID).FirstOrDefault();
                        if (Checking_M == null)
                        {
                            M_New = new Manager();
                            M_New.UserID = M_Record.UserID;
                            M_New.RequestTypeID = M_Record.RequestTypeID;
                            M_New.CustomerID = C_Record_ID;
                            Manager_List.Add(M_New);
                        }
                    }
                    if (Manager_List.Count > 0)
                    {
                        Ddc.Managers.InsertAllOnSubmit(Manager_List);
                        Ddc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void InsertEmailFooterData(EmailFooter Email_F, int C_Record_ID)
        {
            try
            {
                using (DCDataContext Ddc = new DCDataContext())
                {
                    if (Email_F != null)
                    {
                        EmailFooter Checking = Ddc.EmailFooters.Where(a => a.customerID == C_Record_ID
                             && a.RequestTypeID == Email_F.RequestTypeID).FirstOrDefault();
                        if (Checking == null)
                        {
                            EmailFooter E_Mail_New = new EmailFooter();
                            E_Mail_New.RequestTypeID = Email_F.RequestTypeID;
                            E_Mail_New.EmailFooter1 = Email_F.EmailFooter1;
                            E_Mail_New.customerID = C_Record_ID;
                            Ddc.EmailFooters.InsertOnSubmit(E_Mail_New);
                            Ddc.SubmitChanges();
                        }
                        else
                        {
                            Checking.EmailFooter1 = Email_F.EmailFooter1;
                            Ddc.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void InsertPortfolioContactsDepartmentData(List<PortfolioContactsDepartment> S_List, int C_Record_ID)
        {
            try
            {
                using (PortfolioDataContext Ddc = new PortfolioDataContext())
                {
                    PortfolioContactsDepartment s = null;
                    List<PortfolioContactsDepartment> NewInsertedList = new List<PortfolioContactsDepartment>();
                    foreach (PortfolioContactsDepartment S_New in S_List)
                    {
                        var Checking = Ddc.PortfolioContactsDepartments.Where(a => a.Name.ToLower() == S_New.Name.ToLower() && a.CustomerID == C_Record_ID).FirstOrDefault();
                        if (Checking == null)
                        {
                            s = new PortfolioContactsDepartment();
                            s.Name = S_New.Name;
                            s.CustomerID = C_Record_ID;
                            NewInsertedList.Add(s);
                        }
                    }
                    if (NewInsertedList.Count > 0)
                    {
                        Ddc.PortfolioContactsDepartments.InsertAllOnSubmit(NewInsertedList);
                        Ddc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void AccessControlEmailData(AccessControlEmail A_ControlEmail, int C_Record_ID)
        {
            try
            {
                List<AccessControlEmail> Manager_List = new List<AccessControlEmail>();
                using (DCDataContext Ddc = new DCDataContext())
                {
                    if (A_ControlEmail != null)
                    {
                        AccessControlEmail Checking = Ddc.AccessControlEmails.Where(a => a.CustomerID == C_Record_ID).FirstOrDefault();
                        if (Checking == null)
                        {
                            AccessControlEmail E_Mail_New = new AccessControlEmail();
                            E_Mail_New.EmailAddress = A_ControlEmail.EmailAddress;
                            E_Mail_New.CustomerID = C_Record_ID;
                            Ddc.AccessControlEmails.InsertOnSubmit(E_Mail_New);
                            Ddc.SubmitChanges();
                        }
                        else
                        {
                            Checking.EmailAddress = A_ControlEmail.EmailAddress;
                            Ddc.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void EmailNotificationData(FLSSectionConfig Fls_Section, int C_Record_ID)
        {
            try
            {
                List<FLSSectionConfig> Manager_List = new List<FLSSectionConfig>();
                using (DCDataContext Ddc = new DCDataContext())
                {
                    if (Fls_Section != null)
                    {
                        FLSSectionConfig Checking = Ddc.FLSSectionConfigs.Where(a => a.CustomerID == C_Record_ID).FirstOrDefault();
                        if (Checking == null)
                        {
                            FLSSectionConfig E_Mail_New = new FLSSectionConfig();
                            E_Mail_New.EmailCustomer = Fls_Section.EmailCustomer;
                            E_Mail_New.EmailEngineer = Fls_Section.EmailEngineer;
                            E_Mail_New.Document = Fls_Section.Document;
                            E_Mail_New.CustomerID = C_Record_ID;
                            Ddc.FLSSectionConfigs.InsertOnSubmit(E_Mail_New);
                            Ddc.SubmitChanges();
                        }
                        else
                        {
                            Checking.EmailCustomer = Fls_Section.EmailCustomer;
                            Checking.EmailEngineer = Fls_Section.EmailEngineer;
                            Checking.Document = Fls_Section.Document;
                            Ddc.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void MailSendingWithPriorityData(List<MailSendingWithPriority> Mails_PList, int C_Record_ID)
        {
            try
            {
                using (DCDataContext Ddc = new DCDataContext())
                {
                    MailSendingWithPriority s = null;
                    List<MailSendingWithPriority> NewInsertedList = new List<MailSendingWithPriority>();
                    foreach (MailSendingWithPriority S_New in Mails_PList)
                    {
                        var Checking = Ddc.MailSendingWithPriorities.Where(a => a.UserID == S_New.UserID && a.CustomerID == C_Record_ID).FirstOrDefault();
                        if (Checking == null)
                        {
                            s = new MailSendingWithPriority();
                            s.UserID = S_New.UserID;
                            s.RequestTypeID = S_New.RequestTypeID;
                            s.PriorityId = S_New.PriorityId;
                            s.CustomerID = C_Record_ID;
                            NewInsertedList.Add(s);
                        }
                    }
                    if (NewInsertedList.Count > 0)
                    {
                        Ddc.MailSendingWithPriorities.InsertAllOnSubmit(NewInsertedList);
                        Ddc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void CategoryData(List<TypeOfRequest> Type_List, int C_Record_ID)
        {
            try
            {
                TypeOfRequest type_New = null;
                Category Cat_New = null;
                SubCategory Sub_Cat = null;
                using (DCDataContext Ddc = new DCDataContext())
                {
                    if (Type_List != null)
                    {
                        foreach (TypeOfRequest T in Type_List)
                        {
                            TypeOfRequest Checking = Ddc.TypeOfRequests.Where(a => a.CustomerID == C_Record_ID && a.Name.ToLower() == T.Name.ToLower()).FirstOrDefault();
                            if (Checking == null)
                            {
                                type_New = new TypeOfRequest();
                                type_New.Name = T.Name;
                                type_New.CustomerID = C_Record_ID;
                                Ddc.TypeOfRequests.InsertOnSubmit(type_New);
                                Ddc.SubmitChanges();
                            }

                            List<Category> C_List = Ddc.Categories.Where(a => a.TypeOfRequestID == T.ID).ToList();
                            foreach (Category C in C_List)
                            {
                                Category Checking_C = new Category();
                                if (Checking == null)
                                {
                                    Checking_C = Ddc.Categories.Where(a => a.TypeOfRequestID == type_New.ID && a.Name.ToLower() == C.Name.ToLower()).FirstOrDefault();
                                }
                                else
                                {
                                    Checking_C = Ddc.Categories.Where(a => a.TypeOfRequestID == Checking.ID && a.Name.ToLower() == C.Name.ToLower()).FirstOrDefault();
                                }

                                if (Checking_C == null)
                                {
                                    Cat_New = new Category();
                                    Cat_New.Name = C.Name;
                                    if (Checking == null)
                                    {
                                        Cat_New.TypeOfRequestID = type_New.ID;
                                    }
                                    else
                                    {
                                        Cat_New.TypeOfRequestID = Checking.ID;
                                    }
                                    Ddc.Categories.InsertOnSubmit(Cat_New);
                                    Ddc.SubmitChanges();
                                }
                                List<SubCategory> SubC_List = Ddc.SubCategories.Where(a => a.CategoryID == C.ID).ToList();
                                foreach (SubCategory Sub_C in SubC_List)
                                {
                                    SubCategory Checking_Sub = new SubCategory();

                                    if (Checking_C == null)
                                    {
                                        Checking_Sub = Ddc.SubCategories.Where(a => a.CategoryID == Cat_New.ID && a.Name.ToLower() == C.Name.ToLower()).FirstOrDefault();
                                    }
                                    else
                                    {
                                        Checking_Sub = Ddc.SubCategories.Where(a => a.CategoryID == Checking_C.ID && a.Name.ToLower() == C.Name.ToLower()).FirstOrDefault();
                                    }

                                    if (Checking_Sub == null)
                                    {
                                        Sub_Cat = new SubCategory();
                                        Sub_Cat.Name = C.Name;
                                        if (Checking_C == null)
                                        {
                                            Sub_Cat.CategoryID = Cat_New.ID;
                                        }
                                        else
                                        {
                                            Sub_Cat.CategoryID = Checking_C.ID;
                                        }
                                        Ddc.SubCategories.InsertOnSubmit(Sub_Cat);
                                        Ddc.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void DataBindToTables(string CustomerId=null)
        {
            List<Subject> S_List = null;
            List<AssignedDepartment> Assigned_DList = null;
            EmailSendingType E_Mail = null;
            List<Manager> M_List = null;
            EmailFooter Email_F = null;
            List<OurSite> OurSitesList = null;
            List<FLSSourceOfRequest> FLSSourceList = null;
            List<PortfolioContactsDepartment> P_D_List = null;
            AccessControlEmail A_ControlEmail = null;
            FLSSectionConfig Fls_Section = null;
            List<MailSendingWithPriority> Mails_PList = null;
            List<TypeOfRequest> Type_list = null;

            try
            {
                using (DCDataContext Ddc = new DCDataContext())
                {
                    using (PortfolioDataContext Pdc = new PortfolioDataContext())
                    {
                        var DefaultRecord = Ddc.DefaultConfigCustomers.FirstOrDefault();
                        List<ProjectPortfolio> C_List = new List<ProjectPortfolio>();
                        if (CustomerId != null)
                        {
                            C_List = Pdc.ProjectPortfolios.Where(a => a.ID == int.Parse(CustomerId)).ToList();
                        }
                        else
                        {
                            C_List = Pdc.ProjectPortfolios.Where(a => a.ID != DefaultRecord.CustomerId.Value).ToList();
                        }


                        S_List = new List<Subject>();
                        S_List = Ddc.Subjects.Where(a => a.CustomerID.Value == DefaultRecord.CustomerId.Value).ToList();

                        Assigned_DList = new List<AssignedDepartment>();
                        Assigned_DList = Ddc.AssignedDepartments.Where(a => a.CustomerID == DefaultRecord.CustomerId.Value).ToList();

                        E_Mail = new EmailSendingType();
                        E_Mail = Ddc.EmailSendingTypes.Where(a => a.CustomerId == DefaultRecord.CustomerId.Value).FirstOrDefault();
                        M_List = new List<Manager>();
                        M_List = Ddc.Managers.Where(a => a.RequestTypeID == 6 && a.CustomerID == DefaultRecord.CustomerId.Value).ToList();

                        Email_F = new EmailFooter();
                        Email_F = Ddc.EmailFooters.Where(a => a.RequestTypeID == 6 && a.customerID == DefaultRecord.CustomerId.Value).FirstOrDefault();

                        OurSitesList = new List<OurSite>();
                        OurSitesList = Ddc.OurSites.Where(a => a.CustomerID == DefaultRecord.CustomerId).ToList();

                        FLSSourceList = new List<FLSSourceOfRequest>();
                        FLSSourceList = Ddc.FLSSourceOfRequests.Where(a => a.CustomerID == DefaultRecord.CustomerId.Value).ToList();

                        P_D_List = new List<PortfolioContactsDepartment>();
                        P_D_List = Pdc.PortfolioContactsDepartments.Where(a => a.CustomerID == DefaultRecord.CustomerId.Value).ToList();

                        A_ControlEmail = new AccessControlEmail();
                        A_ControlEmail = Ddc.AccessControlEmails.Where(a => a.CustomerID == DefaultRecord.CustomerId.Value).FirstOrDefault();

                        Fls_Section = new FLSSectionConfig();
                        Fls_Section = Ddc.FLSSectionConfigs.Where(a => a.CustomerID == DefaultRecord.CustomerId.Value).FirstOrDefault();

                        Mails_PList = new List<MailSendingWithPriority>();
                        Mails_PList = Ddc.MailSendingWithPriorities.Where(a => a.CustomerID == DefaultRecord.CustomerId.Value).ToList();

                        Type_list = new List<TypeOfRequest>();
                        Type_list = Ddc.TypeOfRequests.Where(a => a.CustomerID == DefaultRecord.CustomerId.Value).ToList();

                        foreach (var C_Record in C_List)
                        {
                            InsertSubjectData(S_List, C_Record.ID);
                            InsertAssignedDepartmentData(Assigned_DList, C_Record.ID);

                            InsertDistributionList(E_Mail, C_Record.ID, M_List);
                            //Email with priority is not done


                            InsertEmailFooterData(Email_F, C_Record.ID);
                            InsertOurSitesData(OurSitesList, C_Record.ID);
                            InsertFlsSourceOfReqData(FLSSourceList, C_Record.ID);
                            InsertPortfolioContactsDepartmentData(P_D_List, C_Record.ID);

                            AccessControlEmailData(A_ControlEmail, C_Record.ID);
                            EmailNotificationData(Fls_Section, C_Record.ID);
                            MailSendingWithPriorityData(Mails_PList, C_Record.ID);
                            CategoryData(Type_list, C_Record.ID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}
