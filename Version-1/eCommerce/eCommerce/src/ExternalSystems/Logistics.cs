using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ExternalSystems
{
    public class Logistics
    {
        public static int Supply(IDictionary<String, Object> deliveryDetails)
        {
            if (ExternalSystemAPI.getInstance().Handshake())
            {
                String result = ExternalSystemAPI.getInstance().Supply(deliveryDetails);
                if (Int32.TryParse(result, out int id))
                {
                    return id;
                }
            }
            return -1;
        }

        public static int CancelSupply(IDictionary<String, Object> deliveryDetails)
        {
            if (ExternalSystemAPI.getInstance().Handshake())
            {
                String result = ExternalSystemAPI.getInstance().CancelSupply(deliveryDetails);
                if (Int32.TryParse(result, out int id))
                {
                    return id;
                }
            }
            return -1;
        }
    }
}
