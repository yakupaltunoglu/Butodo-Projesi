using FluentNHibernate.Mapping;
using ButodoProject.Model.Domain;
using ButodoProject.Core.Model.Mapping;

namespace ButodoProject.Model.Mapping
{
    public class SpendTimeMap : EntityBaseMap<SpendTime>
    {
        public SpendTimeMap()
        {
            Map(x => x.Hour);
            References(x => x.Personal).Column("Personal");
            References(x => x.TaskTable).Column("TaskTable");
        }

    }
}
