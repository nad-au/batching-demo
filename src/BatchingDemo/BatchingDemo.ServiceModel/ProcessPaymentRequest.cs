using ServiceStack;

namespace BatchingDemo.ServiceModel
{
    [Route("/tenant/{TenantId}/payment/{PaymentRequestId}/process")]
    public class ProcessPaymentRequest : IReturnVoid
    {
        public long TenantId { get; set; }
        public long PaymentRequestId { get; set; }
    }
}
