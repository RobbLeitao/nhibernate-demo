using System;
using System.Linq;
using nhibernate_demo.Models;

namespace nhibernate_demo.Repositories
{
    interface IFeatureRepository
    {
        IQueryable<Feature> GetFeatures();
        void Save(nhibernate_demo.Models.Feature feature);
    }
}
