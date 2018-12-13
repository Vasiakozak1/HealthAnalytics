using System;

namespace HealthAnalytics.Data.Entities
{
    public class Attachment<TKey>: Entity<TKey> where TKey: struct, IComparable<TKey>
    {
        public TKey HistoryRecordId { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
    }
}
