using Dto.Address;

namespace Dto.Resales.Requests
{
    public class ResalesRequestDto : ResalesDto
    {
        public AddressDto Address { get; set; }
    }
}
