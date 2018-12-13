using System;
using System.Collections.Generic;

namespace HealthAnalytics.Data.Entities
{
    public class HistoryRecord<TKey> : Entity<TKey> where TKey: struct, IComparable<TKey>
    {
        public TKey MedicalInfoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Attachment<TKey>> Attachments { get; set; }
    }
}
