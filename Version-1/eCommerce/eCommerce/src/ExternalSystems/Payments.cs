using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ExternalSystems
{
    internal static class Payments
    {

        public static Boolean Pay(double amount, IDictionary<String, Object> paymentDetails)
        {
            return true;
        }


        public static void CancelTransaction(IDictionary<String, Object> paymentDetails)
        {

        }
    }
}
