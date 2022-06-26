using FluentNHibernate.Mapping;
using ButodoProject.Model.Domain;
using ButodoProject.Core.Model.Mapping;

namespace ButodoProject.Model.Mapping
{
    public class CompanyMap: EntityBaseMap<Company>
    {
        public CompanyMap()
        {
            Map(x => x.Name);
        }

    }
}
