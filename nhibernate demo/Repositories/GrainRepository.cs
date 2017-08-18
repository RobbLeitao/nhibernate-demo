using System;
using System.Linq;
using nhibernate_demo.Models;
using NHibernate.Linq;
using NHibernate;

namespace nhibernate_demo.Repositories
{
    public class GrainRepository : BaseRepository, IGrainRepository
    {

        public GrainRepository(ISession session) : base(session) { }

        public IQueryable<Grain> GetGrains()
        {
            return _session.Query<Grain>();

        }

        public Grain GetByID(int id)
        {
            return _session.Load<Grain>(id);
        }

        public void Save(Grain grain, int farmerID)
        {
            using (var transaction = _session.BeginTransaction())
            {
                grain.Farmer = _session.Load<Farmer>(farmerID);
                _session.Save(grain);
                transaction.Commit();
            }
        }

        public void Edit(Grain grain, int farmerID)
        {
            var stdnt = _session.Get<Grain>(grain.ID);
            using (var transaction = _session.BeginTransaction())
            {
                grain.Farmer = _session.Load<Farmer>(farmerID);
                stdnt.Name = grain.Name;
                stdnt.Farmer = grain.Farmer;
                stdnt.Features = grain.Features;
                _session.Update(stdnt);
                transaction.Commit();
            }
        }

        public void Delete(int grainID)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(_session.Load<Grain>(grainID));
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (transaction.IsActive)
                    {
                        transaction.Rollback();
                    }
                }

            }
        }

        public void Save(Grain grain)
        {
            throw new NotImplementedException();
        }
    }
}