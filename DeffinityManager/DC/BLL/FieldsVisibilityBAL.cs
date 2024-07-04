using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.Entity;
using DC.DAL; 


namespace DC.BAL
{
    /// <summary>
    /// Summary description for FieldsVisibilityBAL
    /// </summary>

    public class FieldsVisibilityBAL : IDisposable
    {
        public const string CustomerCostCentre = "Customer Cost Centre";
        public const string PONumber = "PO Number";
        IDCRespository<FieldsVisibility> dcContext = null;
        public FieldsVisibilityBAL()
        {
            //
            // TODO: Add constructor logic here
            //
            dcContext = new DCRepository<FieldsVisibility>();
        }

        public void AddDefaultValues()
        {
            if (dcContext.GetAll().Where(p => p.FieldName == CustomerCostCentre).Count() == 0)
            {
                dcContext.Add(new FieldsVisibility() { FieldName = CustomerCostCentre, RequestType = 6, Visible = true });
            }
            if (dcContext.GetAll().Where(p => p.FieldName == PONumber).Count() == 0)
            {
                dcContext.Add(new FieldsVisibility() { FieldName = PONumber, RequestType = 6, Visible = true });
            }
        }
        public List<FieldsVisibility> FieldsVisibility_Select()
        {
            return dcContext.GetAll().ToList();
        }
        public void FieldsVisibility_Update(FieldsVisibility fi)
        {
            dcContext.Edit(fi);
        }
        public void Dispose()
        {
            if (dcContext != null)
                dcContext.Dispose();
        }
    }
}