using System;

namespace Tracker.Entities.Identity
{
    public class Address
    {
        public Guid AddressId { get; set; } = Guid.NewGuid();
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
