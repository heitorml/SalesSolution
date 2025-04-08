using Orders.Worker.Shared.Requests;

namespace Orders.Worker.Shared.Responses
{
    public class ResalesResponseDto : ResalesRequest
    {
        public string Id { get; set; }
        public List<AddressDto> Addresses { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Active { get; set; }

    }
}
