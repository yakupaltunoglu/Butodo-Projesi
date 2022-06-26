using ButodoProject.Core.Model.FixType;
using System;

namespace ButodoProject.Model.Domain
{
    public class Personal : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual PersonalType PersonalType { get; set; }
        public virtual Company Company { get; set; }
        public virtual string Email { get; set; }


        public virtual string Username { get; set; }
        public virtual string Password { get; set; }

    }
}
