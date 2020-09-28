using ServiceStack;

namespace BatchingDemo.ServiceModel
{
    [Route("/tenant/{TenantId}/setup-banking-error")]
    public class TenantBankingSetupError : IReturnVoid
    {
        public long TenantId { get; set; }
        public long PaymentRequestId { get; set; }
    }
}
