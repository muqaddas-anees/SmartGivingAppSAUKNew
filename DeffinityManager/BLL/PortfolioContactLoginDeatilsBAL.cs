using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMgt.BAL
{
    public class PortfolioContactLoginDeatilsBAL
    {
        public static void PortfolioContactLoginDeatils_AddUpdate(int ContactID, int UserID, string UserName, string Password)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactLoginDeatil> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactLoginDeatil>();
            var cEntity = pRep.GetAll().Where(o => o.ContactID == ContactID).FirstOrDefault();
            if(cEntity == null)
            {
                cEntity = new Entity.PortfolioContactLoginDeatil();
                cEntity.ContactID = ContactID;
                cEntity.Password = Password;
                cEntity.UserID = UserID;
                cEntity.UserName = UserName;
                pRep.Add(cEntity);
            }
            else
            {
                if (ContactID > 0)
                {
                    cEntity.Password = Password;
                    //cEntity.UserID = p.UserID;
                    cEntity.UserName = UserName;
                }
                else if(UserID >0)
                {
                     cEntity = pRep.GetAll().Where(o => o.UserID == UserID).FirstOrDefault();
                    if(cEntity != null)
                    {
                        cEntity.Password = Password;
                        cEntity.UserName = UserName;
                    }

                }
                pRep.Edit(cEntity);
            }
        }
        public static PortfolioMgt.Entity.PortfolioContactLoginDeatil PortfolioContactLoginDeatils_SelectByContactID(int ContactID)
        {
             IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactLoginDeatil> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactLoginDeatil>();
             return pRep.GetAll().Where(o => o.ContactID == ContactID).FirstOrDefault();
        }
        public static PortfolioMgt.Entity.PortfolioContactLoginDeatil PortfolioContactLoginDeatils_SelectByUserID(int UserID)
        {
            IPortfolioRepository<PortfolioMgt.Entity.PortfolioContactLoginDeatil> pRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactLoginDeatil>();
            return pRep.GetAll().Where(o => o.UserID == UserID).FirstOrDefault();
        }
    }
}
