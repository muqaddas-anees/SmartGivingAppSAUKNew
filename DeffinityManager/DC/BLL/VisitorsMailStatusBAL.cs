using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DC.BAL
{
    /// <summary>
    /// Summary description for VisitorsMailStatusBAL
    /// </summary>
    public class VisitorsMailStatusBAL :IDCRespository<DC.Entity.VisitorsMailStatus>
    {

        IDCRespository<DC.Entity.VisitorsMailStatus> vRepository = null;
        public VisitorsMailStatusBAL()
        {
            vRepository = new DCRepository<DC.Entity.VisitorsMailStatus>();
        }

        public void Add(int CallID, int TotalVisitors,int ArrivalVisitors, int DepartStatus)
        {


        }

        public Entity.VisitorsMailStatus GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Entity.VisitorsMailStatus> GetAll()
        {
            return vRepository.GetAll();
        }

        public void Add(Entity.VisitorsMailStatus entity)
        {
            vRepository.Add(entity);
        }

        public void AddAll(List<Entity.VisitorsMailStatus> entityCollection)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entity.VisitorsMailStatus entity)
        {
            vRepository.Delete(entity);
        }

        public void DeleteAll(List<Entity.VisitorsMailStatus> entityCollection)
        {
            throw new NotImplementedException();
        }

        public void Edit(Entity.VisitorsMailStatus entity)
        {
            vRepository.Edit(entity);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (vRepository != null)
                vRepository.Dispose();
        }


    }
}