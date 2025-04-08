namespace Orders.Worker.Events
{
    public class CancelledOrderRequested
    {
        public string OrderId { get; set; }
    }
}
