using FluentNHibernate.Mapping;
using ButodoProject.Model.Domain;
using ButodoProject.Core.Model.Mapping;

namespace ButodoProject.Model.Mapping
{
    public class TaskMessageMap : EntityBaseMap<TaskMessage>
    {
        public TaskMessageMap()
        {
            Map(x => x.Name);
            References(x => x.TaskTable).Column("TaskTable");
        }

    }
}
