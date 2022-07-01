using FluentNHibernate.Mapping;
using ButodoProject.Model.Domain;
using ButodoProject.Core.Model.Mapping;

namespace ButodoProject.Model.Mapping
{
    public class ProjectMap : EntityBaseMap<Project>
    {
        public ProjectMap()
        {
            Map(x => x.Name);
            Map(x => x.FullName);
            References(x => x.Company).Column("Company");
            Map(x => x.Leftx).Column("Leftx");
            Map(x => x.Rightx).Column("Rightx");
            Map(x => x.Depth);
        }

    }
}
