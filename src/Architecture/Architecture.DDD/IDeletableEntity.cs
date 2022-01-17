using System;

namespace Architecture.DDD
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? TimeOfDeleteUtc { get; set; }
    }
}
