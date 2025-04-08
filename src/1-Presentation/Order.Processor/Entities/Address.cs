using System.Diagnostics.CodeAnalysis;

namespace Orders.Worker.Entities
{
    [ExcludeFromCodeCoverage]
    public class Address
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
