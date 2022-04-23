using eCommerce.src.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceIntegrationTests.Utils
{
    public class Bridge
    {
        public static ISystemInterface getService()
        {
            SystemInterfaceProxy proxy = new SystemInterfaceProxy();
            // Uncomment when real application is ready
            proxy.Real = new RealAdapter();
            return proxy;
        }
    }
}
