using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Controllers
{
    internal interface IGuestController
    {

    }
    internal class GuestController : IGuestController
    {
        protected ISystemFacade SystemFacade;

        public GuestController(ISystemFacade systemFacade)
        {
            SystemFacade = systemFacade;
        }

    }
}
