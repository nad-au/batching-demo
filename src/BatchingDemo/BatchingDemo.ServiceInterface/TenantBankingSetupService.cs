using System;
using System.Threading.Tasks;
using ServiceStack;
using BatchingDemo.ServiceModel;

namespace BatchingDemo.ServiceInterface
{
    public class TenantBankingSetupService : Service
    {
        private readonly ITenantBankingProvider tenantBankingProvider;

        public TenantBankingSetupService(ITenantBankingProvider tenantBankingProvider)
        {
            this.tenantBankingProvider = tenantBankingProvider;
        }
        
        public Task Any(TenantBankingSetupRequest request)
        {
            const int tenantId = 123; // TODO: Get from db

            try
            {
                Console.WriteLine($"Setting up banking for Tenant id {request.TenantId} before processing payment request id {request.PaymentRequestId}");

                tenantBankingProvider.SetupBanking(tenantId);
                
                PublishMessage(request.ConvertTo<EnqueuePaymentRequest>());
            }
            catch
            {
                PublishMessage(request.ConvertTo<TenantBankingSetupError>());
            }
            
            return Task.CompletedTask;
        }

        public Task Any(TenantBankingSetupError request)
        {
            Console.WriteLine($"Error setting up banking for Tenant id {request.TenantId} while processing payment request id {request.PaymentRequestId}");
            
            return Task.CompletedTask;
        }
    }
}
