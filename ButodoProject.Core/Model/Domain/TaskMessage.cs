using System;

namespace ButodoProject.Model.Domain
{
    public class TaskMessage : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual TaskTable TaskTable { get; set; }
    }
}
