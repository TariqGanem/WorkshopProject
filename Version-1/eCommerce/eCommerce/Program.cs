using eCommerce.src.ServiceLayer;
using System;

namespace eCommerce
{
    class Program
    {
        static void Main(string[] args)
        {
            eCommerceSystem system = new eCommerceSystem();
            system.run();
        }
    }
}
