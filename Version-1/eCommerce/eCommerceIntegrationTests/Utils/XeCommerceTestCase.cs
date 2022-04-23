using eCommerce.src.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceIntegrationTests.Utils
{
    public abstract class XeCommerceTestCase
    {
        protected ISystemInterface api;

        protected XeCommerceTestCase()
        {
            api = Bridge.getService();
        }
    }
}
