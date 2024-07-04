using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.BLL
{
    public class JobDefaultSettingsBAL
    {
        public static JobDefaultSetting JobDefaultSettingsBAL_AddUpdate(DC.Entity.JobDefaultSetting j)
        {
            var jRep = new DCRepository<DC.Entity.JobDefaultSetting>();
            var jEntity = jRep.GetAll().Where(o => o.CallID == j.CallID).FirstOrDefault();
            if (jEntity == null)
            {
                jEntity = new JobDefaultSetting();
                jEntity.CallID = j.CallID;
                jEntity.IsCopiedBomDefault = j.IsCopiedBomDefault;
                jEntity.IsCopiedQuotation = j.IsCopiedQuotation;
                jRep.Add(jEntity);
            }
            else
            {
                jEntity.IsCopiedBomDefault = j.IsCopiedBomDefault;
                jEntity.IsCopiedQuotation = j.IsCopiedQuotation;
                jRep.Edit(jEntity);
            }

            return jEntity;
        }

        //update BOM 

        public static JobDefaultSetting JobDefaultSettingsBAL_UpdateBOMDefaults(int callID,bool IsCopiedBOMDefaults)
        {
            var jRep = new DCRepository<DC.Entity.JobDefaultSetting>();
            var jEntity = jRep.GetAll().Where(o => o.CallID == callID).FirstOrDefault();
            if (jEntity == null)
            {
                jEntity = new JobDefaultSetting();
                jEntity.CallID = callID;
                jEntity.IsCopiedBomDefault = IsCopiedBOMDefaults;
                //jEntity.IsCopiedQuotation = j.IsCopiedQuotation;
                jRep.Add(jEntity);
            }
            else
            {
                jEntity.IsCopiedBomDefault = IsCopiedBOMDefaults;
                jRep.Edit(jEntity);
            }


            return jEntity;
        }

        public static JobDefaultSetting JobDefaultSettingsBAL_UpdateQuotation(int callID, bool IsCopiedQuotation)
        {
            var jRep = new DCRepository<DC.Entity.JobDefaultSetting>();
            var jEntity = jRep.GetAll().Where(o => o.CallID == callID).FirstOrDefault();
            if (jEntity == null)
            {
                jEntity = new JobDefaultSetting();
                jEntity.CallID = callID;
                jEntity.IsCopiedQuotation = IsCopiedQuotation;
                jRep.Add(jEntity);
            }
            else
            {
                jEntity.IsCopiedQuotation = IsCopiedQuotation;
                jRep.Edit(jEntity);
            }


            return jEntity;
        }

        public static bool JobDefaultSettingsBAL_SelectCopiedBOMDefaults(int callID)
        {
            bool retval = false;

            var jRep = new DCRepository<DC.Entity.JobDefaultSetting>();
            var jEntity = jRep.GetAll().Where(o => o.CallID == callID).FirstOrDefault();
            if(jEntity != null)
            {
                if (jEntity.IsCopiedBomDefault.HasValue)
                    retval = jEntity.IsCopiedBomDefault.Value;
            }
            else
            {
                retval = false;
            }

            return retval;

        }


        public static bool JobDefaultSettingsBAL_SelectCopiedQuoteDefaults(int callID)
        {
            bool retval = false;

            var jRep = new DCRepository<DC.Entity.JobDefaultSetting>();
            var jEntity = jRep.GetAll().Where(o => o.CallID == callID).FirstOrDefault();
            if (jEntity != null)
            {
                if (jEntity.IsCopiedQuotation.HasValue)
                    retval = jEntity.IsCopiedQuotation.Value;
            }
            else
            {
                retval = false;
            }

            return retval;

        }


    }
}
