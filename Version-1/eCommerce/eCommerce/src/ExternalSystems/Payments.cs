using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ExternalSystems
{
    public static class Payments
    {

        public static int Pay(double amount, IDictionary<String, Object> paymentDetails)
        {
            if (ExternalSystemAPI.getInstance().Handshake())
            {
                String result = ExternalSystemAPI.getInstance().Pay(paymentDetails);
                if (Int32.TryParse(result, out int id))
                {
                    return id;
                }
            }
            return -1;
        }

        public static int CancelPay(IDictionary<String, Object> paymentDetails)
        {
            if (ExternalSystemAPI.getInstance().Handshake())
            {
                String result = ExternalSystemAPI.getInstance().CancelPay(paymentDetails);
                if (Int32.TryParse(result, out int id))
                {
                    return id;
                }
            }
            return -1;
        }
    }
}
