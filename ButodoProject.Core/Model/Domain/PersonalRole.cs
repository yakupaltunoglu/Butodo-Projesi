using ButodoProject.Core.Model.FixType;
using System;

namespace ButodoProject.Model.Domain
{
    public class PersonalRole : EntityBase
    {
        public virtual string RolePageType { get; set; }
        public virtual string RoleType { get; set; }
        public virtual Personal Personal { get; set; }

    }
}
