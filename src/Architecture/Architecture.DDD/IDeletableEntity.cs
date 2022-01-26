using System;

namespace Architecture.DDD
{
    public interface IDeletableEntity
    {
        public bool IsDeleted { get; }
        public DateTimeOffset? TimeOfDeleteUtc { get; }
    }
}
