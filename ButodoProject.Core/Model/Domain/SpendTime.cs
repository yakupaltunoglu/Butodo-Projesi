using System;

namespace ButodoProject.Model.Domain
{
    public class SpendTime : EntityBase
    {
        public virtual int Minute { get; set; }
        public virtual Personal Personal { get; set; }
        public virtual TaskTable TaskTable { get; set; }
    }
}
