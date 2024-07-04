using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.BLL
{
    public class BOMBAL
    {

        public static List<DC.Entity.BOMItem> BOMBAL_BOMItems_Select(int callID)
        {
            IDCRespository<DC.Entity.BOMItem> bomrep = new DCRepository<DC.Entity.BOMItem>();
            return bomrep.GetAll().Where(o => o.CallID == callID).ToList();

        }

        public static DC.Entity.BOMPrice BOMBAL_BOMPrice_Select(int callID)
        {
            IDCRespository<DC.Entity.BOMPrice> bomrep = new DCRepository<DC.Entity.BOMPrice>();
            return bomrep.GetAll().Where(o => o.CallID == callID).FirstOrDefault();

        }

    }
}
