using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class FundraisersInfoBAL
    {
        public static PortfolioMgt.Entity.FundraisersInfo FundraisersInfoBAL_Add(PortfolioMgt.Entity.FundraisersInfo f)
        {
            IPortfolioRepository<PortfolioMgt.Entity.FundraisersInfo> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersInfo>();

            fRep.Add(f);
            return f;
        }
        public static PortfolioMgt.Entity.FundraisersInfo FundraisersInfoBAL_Update(PortfolioMgt.Entity.FundraisersInfo f)
        {
            IPortfolioRepository<PortfolioMgt.Entity.FundraisersInfo> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersInfo>();
            var f_Update = fRep.GetAll().Where(o => o.ID == f.ID).FirstOrDefault();
            if(f_Update != null)
            {
                f_Update.ContactNo = f.ContactNo;
                f_Update.Email = f.Email;
                f_Update.FirstName = f.FirstName;
                f_Update.FundUNID = f.FundUNID;
                f_Update.InvitationSentOn = f.InvitationSentOn;
                f_Update.IsAddMember = f.IsAddMember;
                f_Update.IsDeleted = f.IsDeleted;
                f_Update.IsInvitationSent = f.IsInvitationSent;
                f_Update.LastName = f.LastName;
                f_Update.Status = f.Status;
                f_Update.ShortCode = f.ShortCode;
                f_Update.MainFundUNID = f.MainFundUNID;
                
                fRep.Edit(f_Update);
            }
           
            return f;
        }
        public static IQueryable< PortfolioMgt.Entity.FundraisersInfo> FundraisersInfoBAL_SelectAll()
        {
            IPortfolioRepository<PortfolioMgt.Entity.FundraisersInfo> fRep = new PortfolioRepository<PortfolioMgt.Entity.FundraisersInfo>();
            return fRep.GetAll();
        }


    }
}
