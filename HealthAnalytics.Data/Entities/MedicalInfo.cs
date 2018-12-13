using System;
using System.Collections.Generic;

namespace HealthAnalytics.Data.Entities
{
    public class MedicalInfo<TKey>: Entity<TKey> where TKey: struct, IComparable<TKey>
    {
        public TKey UserId { get; set; }
        public BloodType BloodType { get; set; }

        public ConsumingFrequency AlcoholConsumingFrequency { get; set; }
        public ConsumingFrequency TobaccoConsumingFrequency { get; set; }

        public ICollection<HistoryRecord<TKey>> SurgicalHistoryRecords { get; set; }
        public ICollection<HistoryRecord<TKey>> SicknessHistoryRecords { get; set; }
        public ICollection<HistoryRecord<TKey>> PresentIllnessRecords { get; set; }
        public ICollection<HistoryRecord<TKey>> AllergiesRecords { get; set; }
        public ICollection<HistoryRecord<TKey>> OtherHistoryRecords { get; set; }
    }
}
