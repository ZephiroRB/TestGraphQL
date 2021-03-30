using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.Resolver
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public Status Status { get; set; }
        public User CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public User? UpdatedBy { get; set; }
        public DateTime? Modified { get; set; }

        public AddressType AddressType { get; set; }
    }

    public enum Status
    {
        Active = 1,
        Inactive = 2,
        Incomplete = 3,
        Pending = 4
    }

    public class User
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string CompanyName { get; set; }

        public string Role { get; set; }

        public bool Enabled { get; set; }

    }

    public enum AddressType
    {
        Main = 1,
        Billing = 2
    }
}
