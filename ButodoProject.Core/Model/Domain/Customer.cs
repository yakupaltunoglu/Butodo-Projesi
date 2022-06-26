using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Model.Domain
{
    public class Customer : EntityBase
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string Email { get; set; }

    }
}