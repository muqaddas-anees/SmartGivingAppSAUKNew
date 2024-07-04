using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;

namespace PortfolioMgt.BAL
{
    public class PortfolioTrainingBAL
    {


        public static List<PortfolioTraining> PortfolioTrainingBAL_TrainingSelect()
        {
            IPortfolioRepository<PortfolioTraining> pmRes = new PortfolioRepository<PortfolioTraining>();
            return pmRes.GetAll().ToList();
        }
        public static PortfolioTraining PortfolioTrainingBAL_TrainingAdd(PortfolioTraining p)
        {
            IPortfolioRepository<PortfolioTraining> pmRes = new PortfolioRepository<PortfolioTraining>();
            var plist = pmRes.GetAll().ToList();
           
            if (pmRes.GetAll().Where(o => o.TrainingName == p.TrainingName).Count() == 0)
            {
                pmRes.Add(p);
            }
            return p;
        }
      
        public static PortfolioTraining PortfolioTrainingBAL_TrainingUpdate(PortfolioTraining p)
        {
            IPortfolioRepository<PortfolioTraining> pmRes = new PortfolioRepository<PortfolioTraining>();
            var t = pmRes.GetAll().Where(o => o.TrainingID == p.TrainingID).FirstOrDefault();
            if (t != null)
            {
                t.TrainingName = p.TrainingName;
                t.TrainingDescription = p.TrainingDescription;
                t.TrainingImage = p.TrainingImage;
                t.Amount = p.Amount;
                pmRes.Edit(p);
            }
            return p;
        }

        public static bool PortfolioTrainingBAL_TrainingDelete(int TrainingID)
        {
            bool retval = false;
            IPortfolioRepository<PortfolioTraining> pmRes = new PortfolioRepository<PortfolioTraining>();
            var tEntity = pmRes.GetAll().Where(t => t.TrainingID == TrainingID).FirstOrDefault();
            if(tEntity != null)
            {
                pmRes.Delete(tEntity);
                retval = true;
            }
            return retval;
        }
    }
}
