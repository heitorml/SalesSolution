namespace Resales.Api.Shared.Requests
{
    public class ResaleUpdateRequest : ResalesRequest
    {
        public string Id { get; set; }
        public List<AddressRequest> Addresses { get; set; }
    }
}
