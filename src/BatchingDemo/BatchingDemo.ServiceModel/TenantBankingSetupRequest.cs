using ServiceStack;

namespace BatchingDemo.ServiceModel
{
    [Route("/tenant/{TenantId}/setup-banking")]
    public class TenantBankingSetupRequest : IReturnVoid
    {
        public long TenantId { get; set; }
        public long PaymentRequestId { get; set; }
    }
}
