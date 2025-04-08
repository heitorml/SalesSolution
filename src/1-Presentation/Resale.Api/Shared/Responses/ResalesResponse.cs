using Resales.Api.Shared.Requests;

namespace Resales.Api.Shared.Responses
{
    public class ResalesResponse : ResalesRequest
    {
        public string Id { get; set; }
        public List<AddressRequest> Addresses { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Active { get; set; }

    }
}
