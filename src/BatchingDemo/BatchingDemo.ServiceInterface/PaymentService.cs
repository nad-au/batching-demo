using System;
using ServiceStack;
using BatchingDemo.ServiceModel;

namespace BatchingDemo.ServiceInterface
{
    public class PaymentService : Service
    {
        public PaymentResponse Any(PaymentRequest request)
        {
            var response = new PaymentResponse { PaymentRequestId = new Random().Next().ConvertTo<long>() };
            
            PublishMessage(response.ConvertTo<ProcessPaymentRequest>());

            return response;
        }
    }
}
