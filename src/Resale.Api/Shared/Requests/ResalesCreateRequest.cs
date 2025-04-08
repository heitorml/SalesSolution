namespace Resales.Api.Shared.Requests
{
    public class ResalesCreateRequest : ResalesRequest
    {
        public AddressRequest Address { get; set; }
    }
}
