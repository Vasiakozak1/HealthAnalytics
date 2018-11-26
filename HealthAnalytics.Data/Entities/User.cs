using System;
using System.Collections.Generic;
using System.Text;

namespace HealthAnalytics.Data.Entities
{
    public class User: Entity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        // Other properties
    }
}
