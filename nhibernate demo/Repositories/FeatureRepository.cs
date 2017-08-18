using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using nhibernate_demo.Models;
using NHibernate.Linq;

namespace nhibernate_demo.Repositories
{
    public class FeatureRepository:BaseRepository, nhibernate_demo.Repositories.IFeatureRepository
    {
        public FeatureRepository(ISession session) : base(session) { }
        public IQueryable<Feature> GetFeatures()
        {
            return _session.Query<Feature>();
        }

        public void Save(Feature feature)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(feature);
                transaction.Commit();
            }
        }
    }
}