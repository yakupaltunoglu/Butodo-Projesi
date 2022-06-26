using System;

namespace ButodoProject.Model.Domain
{
    public class EntityBase
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual DateTime? LastUpdatedAt { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
