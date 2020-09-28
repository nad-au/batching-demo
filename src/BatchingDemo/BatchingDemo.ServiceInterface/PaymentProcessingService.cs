using System;
using System.Threading.Tasks;
using ServiceStack;
using BatchingDemo.ServiceModel;

namespace BatchingDemo.ServiceInterface
{
    public class PaymentProcessingService : Service
    {
        private readonly ITenantBankingProvider tenantBankingProvider;

        public PaymentProcessingService(ITenantBankingProvider tenantBankingProvider)
        {
            this.tenantBankingProvider = tenantBankingProvider;
        }
        
        public Task Any(ProcessPaymentRequest request)
        {
            if (!tenantBankingProvider.IsBankingSetup(request.TenantId))
            {
                PublishMessage(request.ConvertTo<TenantBankingSetupRequest>());
                
                return Task.CompletedTask;
            }
            
            PublishMessage(request.ConvertTo<EnqueuePaymentRequest>());
            
            return Task.CompletedTask;
        }

        public Task Any(EnqueuePaymentRequest request)
        {
            Console.WriteLine($"Adding payment request id {request.PaymentRequestId} to batch");
            
            return Task.CompletedTask;
        }
    }
}
