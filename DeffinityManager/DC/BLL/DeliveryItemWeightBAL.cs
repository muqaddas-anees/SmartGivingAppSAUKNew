using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.DAL;
using DC.Entity;

namespace DC.BLL
{
    /// <summary>
    /// Summary description for DeliveryItemWeightBAL
    /// </summary>
    public class DeliveryItemWeightBAL
    {
        public DeliveryItemWeightBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<DeliveryItemWeight> DeliveryItemWeightBAL_Select()
        {
            List<DeliveryItemWeight> weight_list = new List<DeliveryItemWeight>();
            using(DCDataContext dc = new DCDataContext())
            {
                weight_list = dc.DeliveryItemWeights.Select(p=>p).ToList();
            }
            return weight_list;
        }
    }
}