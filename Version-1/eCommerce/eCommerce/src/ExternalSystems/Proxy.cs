using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ExternalSystems
{
    public class Proxy
    {
        ExternalSystemsAPIInterface extsys;
        private static Proxy instance = null;
        private Proxy(String url = "")
        {
            extsys = ExternalSystemAPI.getInstance(url);
        }

        public static Proxy getInstance(String url = "")
        {
            if(instance == null)
                instance = new Proxy(url);
            return instance;
        }
        public static String Deliver(IDictionary<String, Object> deliveryDetails)
        {
            return "" + Logistics.Supply(deliveryDetails);
        }

        public static String Pay(double amount, IDictionary<String, Object> paymentDetails)
        {
            return "" + Payments.Pay(amount, paymentDetails);
        }

        public static String CancelTransaction(IDictionary<String, Object> paymentDetails)
        {
            return "" + Payments.CancelPay(paymentDetails);
        }

        public static String CancelDelivery(IDictionary<String, Object> deliveryDetails)
        {
            return "" + Logistics.CancelSupply(deliveryDetails);
        }
    }
}
