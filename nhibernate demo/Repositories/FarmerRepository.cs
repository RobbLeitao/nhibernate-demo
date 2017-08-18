using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nhibernate_demo.Models;
using NHibernate;
using NHibernate.Linq;

namespace nhibernate_demo.Repositories
{
    public class FarmerRepository : BaseRepository, IFarmerRepository
    {

        public FarmerRepository(ISession session) : base(session) { }

        public IQueryable<Farmer> GetFarmers()
        {
           return _session.Query<Farmer>();
        }

        public Farmer GetByID(int id)
        {
            return _session.Load<Farmer>(id);
        }

        public void Delete(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(_session.Load<Farmer>(id));
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (transaction.IsActive)
                    {
                        transaction.Rollback();
                    }
                    throw ex;
                }
            }
        }

        public void Save(Farmer farmer)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(farmer);
                transaction.Commit();
               
            }
        }

        public void Edit(Farmer farmer)
        {
            var stdnt = _session.Get<Farmer>(farmer.ID);
            using (var transaction = _session.BeginTransaction())
            {
                stdnt.Name = farmer.Name;
                _session.Update(stdnt);
                transaction.Commit();

            }
        }
    }
}