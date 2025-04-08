using Resales.Api.Entities;
using Resales.Api.Shared.Requests;

namespace Resales.Api.Shared.Mapper
{
    public static class AddressMaper
    {
        public static List<Address> ToEntity(List<AddressRequest> dto)
            => dto.Select(x => new Address
            {
                Name = x.Street,
                ZipCode = x.ZipCode,
                City = x.City,
            }).ToList();

        public static List<AddressRequest> ToDto(List<Address> dto)
            => dto.Select(x => new AddressRequest
            {
                Street = x.Name,
                ZipCode = x.ZipCode,
                City = x.City,
            }).ToList();

        public static List<Address> ToEntity(AddressRequest dto)
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
