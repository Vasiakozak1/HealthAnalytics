using MongoDB.Bson;
using System;

namespace HealthAnalytics.Data.Entities
{
    public class UserToken<TKey>: Entity<TKey> where TKey: struct, IComparable<TKey>
    {
        public string Token { get; set; }
        public DateTime DateExpired { get; set; }
        public TKey UserId { get; set; }
    }
}
