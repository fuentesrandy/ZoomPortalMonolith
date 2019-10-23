using System;


namespace ZoomPortalMonolith.SharedKernal.Domain.Foundation.Entities
{
    public interface IEntity
    {
        DateTime CreateDate { get; }
        DateTime ModifyDate { get; }
        string CreateUserId { get; set; }
        string ModifyUserId { get; set; }
        string Type { get; }
    }

    public interface IEntity<TId> : IEntity 
    {
        TId Id { get; }
    }
}
