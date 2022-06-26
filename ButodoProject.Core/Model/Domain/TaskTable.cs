using System;

namespace ButodoProject.Model.Domain
{
    public class TaskTable : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual Project Project { get; set; }
        public virtual DateTime EndDate { get; set; }


        public virtual Personal Personal { get; set; }
    }
}
