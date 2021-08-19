using Architecture.DDD.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DDD
{
    public abstract class EntityBase
    {
        public virtual string Id { get; protected set; }

        #region Equality

        public override bool Equals(object obj)
        {
            var other = obj as EntityBase;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(other.Id))
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase a, EntityBase b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType() + Id).GetHashCode();
        }

        #endregion

        #region Events

        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// Domain events occurred.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// Add domain event.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            this._domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clear domain events.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        #endregion

        #region Helpers

        public virtual void ChangeStringProperty(string propertyName, string newValue)
        {
            var propToChange = this.GetType().GetProperty(propertyName);
            if (propToChange is null)
                throw new InvalidOperationException(
                    $"property with requested name: {propertyName} does not exist in type {this.GetType()}");

            if (propToChange.GetValue(this) is not string propToChangeValue)
                throw new InvalidOperationException(
                    $"{propToChange.Name} is not a string but type of {propToChange.PropertyType}");

            if (string.IsNullOrWhiteSpace(newValue))
                throw new InvalidOperationException($"Given {propToChange.Name} to change is empty");

            if (propToChangeValue == newValue)
                throw new InvalidOperationException($"Given new {propToChange.Name} is the same as current");

            propToChange.SetValue(this, newValue);
        }

        #endregion
    }
}
