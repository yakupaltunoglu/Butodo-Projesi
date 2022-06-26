using FluentNHibernate.Mapping;
using ButodoProject.Core.Model.Domain;
using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Model.Mapping
{
    public abstract class EntityBaseMap<T> : ClassMap<T> where T : EntityBase
    {
        protected EntityBaseMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb().Default("NewId()");
            Map(x => x.IsDeleted).Not.Nullable().Default("0");
            Map(x => x.CreatedAt).Default("GetDate()");
            Map(x => x.LastUpdatedAt).Default("GetDate()");
            Map(x => x.DeletedAt).Nullable();
        }
    }
}