using System;

namespace ButodoProject.Model.Domain
{
    public class PersonalProject : EntityBase
    {
        public virtual Personal Personal { get; set; }
        public virtual Project Project { get; set; }
    }
}
