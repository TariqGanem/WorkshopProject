using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer
{
    internal interface ISystemFacade
    {

    }

    public class SystemFacade : ISystemFacade
    {
        private UserFacade userFacade;
        private StoreFacade storeFacade;
        public SystemFacade()
        {
            userFacade = new UserFacade();
            storeFacade = new StoreFacade();
        }
    }
}
