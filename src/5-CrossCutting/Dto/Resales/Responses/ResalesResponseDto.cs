using Dto.Address;

namespace Dto.Resales.Responses
{
    public class ResalesResponseDto : ResalesDto
    {
        public string Id { get; set; }
        public List<AddressDto> Addresses { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Active { get; set; }

    }
}
