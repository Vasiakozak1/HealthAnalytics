using MongoDB.Bson.Serialization.Attributes;
using System;

namespace HealthAnalytics.Data.Entities
{
    public class Entity<TKey> where TKey: struct, IComparable<TKey>
    {
        [BsonId]
        public TKey Guid { get; set; }
    }
}
