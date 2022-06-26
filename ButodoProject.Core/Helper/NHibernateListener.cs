using System;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;
using NHibernate.Persister.Entity;
using ButodoProject.Core.Model.Domain;
using ButodoProject.Model.Domain;

namespace ButodoProject.Core.Helper
{
    public class NHibernateListener : IPreInsertEventListener, IPreUpdateEventListener, IPreDeleteEventListener
    {
        public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            var entity = @event.Entity as EntityBase;
            if (entity == null)
                return Task.FromResult(false);

            entity.IsDeleted = false;
            entity.CreatedAt = DateTime.Now;
            Set(@event.Persister, @event.State, "CreatedAt", entity.CreatedAt);
            return Task.FromResult(false);
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            var entity = @event.Entity as EntityBase;
            if (entity == null)
                return false;

            entity.IsDeleted = false;
            entity.CreatedAt = DateTime.Now;
            Set(@event.Persister, @event.State, "CreatedAt", entity.CreatedAt);
            return false;
        }

        public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }

        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            return false;
        }

        public Task<bool> OnPreDeleteAsync(PreDeleteEvent @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }

        public bool OnPreDelete(PreDeleteEvent @event)
        {
            return false;
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }
    }
}