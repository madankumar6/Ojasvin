using System;

namespace Tracker.Entities.Identity
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsDefault { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
