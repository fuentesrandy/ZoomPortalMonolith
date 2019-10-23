using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomPortalMonolith.SharedKernal.Domain.Foundation.Entities
{
    [Serializable]
    public abstract class Entity<TId> : IEquatable<Entity<TId>>, IEntity<TId>
    {

        public virtual TId Id { get; protected set; }
        public DateTime CreateDate { get; protected internal set; }
        public DateTime ModifyDate { get; protected internal set; }
        public string CreateUserId { get; set; }
        public string ModifyUserId { get; set; }
        public string Type => this.GetType().Name;





        protected Entity(TId id)
        {
            if (object.Equals(id, default(TId)))
            {
                throw new ArgumentException("The ID cannot be the type's default value.", "id");
            }

            this.Id = id;
            this.CreateDate = DateTime.UtcNow;
            this.ModifyDate = DateTime.UtcNow;
        }


        protected Entity()
        {
        }

        public override bool Equals(object otherObject)
        {
            var entity = otherObject as Entity<TId>;
            if (entity != null)
            {
                return this.Equals(entity);
            }
            return base.Equals(otherObject);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public bool Equals(Entity<TId> other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }

        public static Entity<TId> Create()
        {

            return null;
        }


    }
}
