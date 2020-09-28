using ServiceStack;

namespace BatchingDemo.ServiceModel
{
    [Route("/tenant/{TenantId}/payment/{PaymentRequestId}/enqueue")]
    public class EnqueuePaymentRequest : IReturnVoid
    {
        public long TenantId { get; set; }
        public long PaymentRequestId { get; set; }
    }
}
