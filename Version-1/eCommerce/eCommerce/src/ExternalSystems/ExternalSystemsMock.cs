using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ExternalSystems
{
    public class ExternalSystemsMock : ExternalSystemsAPIInterface
    {
        public Random random { get; set; }
        public ExternalSystemsMock() { this.random = new Random(); }
        public string CancelPay(IDictionary<string, object> paymentDetails)
        {
            return "" + generateId();
        }

        public string CancelSupply(IDictionary<string, object> paymentDetails)
        {
            return "" + generateId();
        }

        public bool Handshake()
        {
            return true;
        }

        public string Pay(IDictionary<string, object> paymentDetails)
        {
            return "" + generateId();
        }

        public string Supply(IDictionary<string, object> paymentDetails)
        {
            return "" + generateId();
        }

        private int generateId()
        {
            int minId = 10000;
            int maxId = 100000;
            int id = random.Next(maxId - minId) + minId;
            return id;
        }
    }
}
