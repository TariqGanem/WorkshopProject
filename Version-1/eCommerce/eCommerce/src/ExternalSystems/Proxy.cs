using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ExternalSystems
{
    internal static class Proxy
    {

        public static Boolean Deliver(IDictionary<String, Object> deliveryDetails)
        {
            return Logistics.Deliver(deliveryDetails);
        }

        public static Boolean Pay(double amount, IDictionary<String, Object> paymentDetails)
        {
            return Payments.Pay(amount, paymentDetails);
        }


        public static void CancelTransaction(IDictionary<String, Object> paymentDetails)
        {
            Payments.CancelTransaction(paymentDetails);
        }
    }
}
