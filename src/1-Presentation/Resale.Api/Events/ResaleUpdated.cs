﻿using Resales.Api.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Resales.Api.Events
{
    [ExcludeFromCodeCoverage]
    public class ResaleUpdated
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FantasyName { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }
        public List<Address> Address { get; set; }

        public ResaleUpdated(Resale resale)
        {
            Id = resale.Id;
            Name = resale.Name;
            FantasyName = resale.FantasyName;
            Phone = resale.Phone;
            ContactName = resale.ContactName;
            Email = resale.Email;
            Cnpj = resale.Cnpj;
            Active = resale.Active;
            Date = DateTime.UtcNow;
            Address = resale.Address;
        }
    }
}
