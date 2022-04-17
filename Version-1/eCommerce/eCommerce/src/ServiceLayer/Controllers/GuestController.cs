using eCommerce.src.DomainLayer;
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
