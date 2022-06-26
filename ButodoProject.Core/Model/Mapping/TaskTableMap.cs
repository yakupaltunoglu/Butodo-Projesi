using FluentNHibernate.Mapping;
using ButodoProject.Model.Domain;
using ButodoProject.Core.Model.Mapping;

namespace ButodoProject.Model.Mapping
{
    public class TaskTableMapping : EntityBaseMap<TaskTable>
    {
        public TaskTableMapping()
        {
            Map(x => x.Name);
            References(x => x.Project).Column("Project");
            Map(x => x.EndDate);
            References(x => x.Personal).Column("Personal");
        }

    }
}
