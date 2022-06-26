using System;

namespace ButodoProject.Model.Domain
{
    public class SpendTime : EntityBase
    {
        public virtual double Hour { get; set; }
        public virtual Personal Personal { get; set; }
        public virtual TaskTable TaskTable { get; set; }
    }
}
