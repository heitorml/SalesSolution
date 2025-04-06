using CrossCutting;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Resale : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FantasyName { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        public bool Active { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdadeAt { get; set; }
        public List<Address> Address { get; set; }
            
    }
}
