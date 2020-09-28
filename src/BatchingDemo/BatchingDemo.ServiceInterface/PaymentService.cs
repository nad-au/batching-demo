using System;
using ServiceStack;
using BatchingDemo.ServiceModel;

namespace BatchingDemo.ServiceInterface
{
    public class PaymentService : Service
    {
        public PaymentResponse Any(PaymentRequest request)
        {
            var paymentRequestId = new Random().Next().ConvertTo<long>();
            
            PublishMessage(new ProcessPaymentRequest
            {
                TenantId = request.TenantId,
                PaymentRequestId = paymentRequestId
            });

            return new PaymentResponse
            {
                PaymentRequestId = paymentRequestId
            };
        }
    }
}
