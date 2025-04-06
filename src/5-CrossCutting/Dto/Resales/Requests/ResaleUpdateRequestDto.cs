using Dto.Address;

namespace Dto.Resales.Requests
{
    public class ResaleUpdateRequestDto : ResalesDto
    {
        public string Id { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
