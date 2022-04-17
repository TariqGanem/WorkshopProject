using System;
using System.Collections.Generic;
using System.Text;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Controllers;
using eCommerce.src.ServiceLayer.Response;

namespace eCommerce.src.ServiceLayer
{

    interface IeCommerceAPI : IGuestController , IRegisteredUserController { }
    class eCommerceSystem : IeCommerceAPI
    {
        public eCommerceSystem() { 
        
        }

        public Response<RegisteredUser> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
