using ServiceStack;

namespace BatchingDemo.ServiceModel
{
    [Route("/tenant/{TenantId}/payment")]
    public class PaymentRequest : IReturn<PaymentResponse>
    {
        public long TenantId { get; set; }
        public long PortfolioId { get; set; }
        public decimal Amount { get; set; }
    }

    public class PaymentResponse
    {
        public long PaymentRequestId { get; set; }
    }
}
