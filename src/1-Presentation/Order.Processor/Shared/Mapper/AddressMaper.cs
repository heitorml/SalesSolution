using Orders.Worker.Entities;
using Orders.Worker.Shared.Requests;

namespace Orders.Worker.Shared.Mapper
{
    public static class AddressMaper
    {
        public static List<Address> ToEntity(List<AddressDto> dto)
            => dto.Select(x => new Address
            {
                Name = x.Street,
                ZipCode = x.ZipCode,
                City = x.City,
            }).ToList();

        public static List<AddressDto> ToDto(List<Address> dto)
            => dto.Select(x => new AddressDto
            {
                Street = x.Name,
                ZipCode = x.ZipCode,
                City = x.City,
            }).ToList();

        public static List<Address> ToEntity(AddressDto dto)
        {
            var listAddress = new List<Address>();
            listAddress.Add(new Address
            {
                Name = dto.Street,
                ZipCode = dto.ZipCode,
                City = dto.City,
            });
            return listAddress;
        }
    }
}
