using System;

namespace HealthAnalytics.Data.Entities
{
    public class User<TKey>: Entity<TKey> where TKey: struct, IComparable<TKey>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActivated { get; set; }
        public string PasswordHash { get; set; }
        public MedicalInfo<TKey> MedicalInfo { get; set; }
        // Other properties
    }
}
