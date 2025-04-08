namespace Orders.Worker.Shared.Configuration
{
    public class ResiliencePolicyOptions
    {
        public int Retry { get; set; }
        public int RetrySecondInitial { get; set; }
        public int DisarmCircuitAfterErros { get; set; }
        public int DisarmCircuitTimmer { get; set; }
        public int TimeoutPolicy { get; set; }
    }
}