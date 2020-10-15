using System;

namespace Agridea.DomainDrivenDesign
{
    public abstract class Entity
    {
        public long Id { get; }

        //If no need to expose, leave a pure shadow properties
        public DateTimeOffset CreationDate { get; }
        public string CreatedBy { get; }
        public DateTimeOffset ModificationDate { get; }
        public string ModifiedBy { get; }

        protected Entity() { }

        protected Entity(long id) : this()
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetRealType() != other.GetRealType()) return false;
            if (Id == 0 || other.Id == 0) return false;
            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }

        private Type GetRealType() //for EF Core
        {
            Type type = GetType();
            if (type.ToString().Contains("Castle.Proxies.")) return type.BaseType;
            return type;
        }
    }
}