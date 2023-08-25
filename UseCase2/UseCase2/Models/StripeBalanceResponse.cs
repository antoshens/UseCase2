namespace UseCase2.Models
{
    public class StripeBalanceResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
