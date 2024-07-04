using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;

namespace PortfolioMgt.BAL
{
    public class EquipmentClientCommunicationBAL
    {
        public static List<EquipmentClientCommunication> EquipmentClientCommunicationBAL_Select(int contactID)
        {
            IPortfolioRepository<EquipmentClientCommunication> mRep = new PortfolioRepository<EquipmentClientCommunication>();
            var mlist = mRep.GetAll().Where(o => o.ClientID == contactID).ToList();
            return mlist.ToList();
        }
        public static EquipmentClientCommunication EquipmentClientCommunicationBAL_Add(EquipmentClientCommunication e)
        {
            IPortfolioRepository<EquipmentClientCommunication> mRep = new PortfolioRepository<EquipmentClientCommunication>();

            mRep.Add(e);
            return e;
        }

        public static EquipmentClientCommunication EquipmentClientCommunicationBAL_Edit(EquipmentClientCommunication e)
        {
            IPortfolioRepository<EquipmentClientCommunication> mRep = new PortfolioRepository<EquipmentClientCommunication>();
            var m = mRep.GetAll().Where(o => o.ID == e.ID).FirstOrDefault();
            if (m != null)
            {
                m.AssetID = e.AssetID;
                m.ClientID = e.ClientID;
                m.DateandTimeEmailSent = e.DateandTimeEmailSent;
                m.FromEmail = e.FromEmail;
                m.MailBody = e.MailBody;
                m.MailSentByID = e.MailSentByID;
                m.MailSubject = e.MailSubject;
                m.ToEmail = e.ToEmail;

            }
            mRep.Edit(m);
            return e;
        }

        public static bool EquipmentClientCommunicationBAL_Delete(int id)
        {
            bool retval = false;
            IPortfolioRepository<EquipmentClientCommunication> mRep = new PortfolioRepository<EquipmentClientCommunication>();
            var m = mRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (m != null)
            {
                mRep.Delete(m);
                retval = true;
            }
            return retval;
        }

    }
}
