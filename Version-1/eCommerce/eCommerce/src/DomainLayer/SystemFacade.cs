using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer
{
    internal interface ISystemFacade
    {
        #region User Actions
        RegisteredUser Login(String userName, String password);
        #endregion
    }

    internal class SystemFacade : ISystemFacade
    {
        private UserFacade userFacade;
        private StoreFacade storeFacade;
        public SystemFacade()
        {
            userFacade = new UserFacade();
            storeFacade = new StoreFacade();
        }

        #region User Facade Methods
        public RegisteredUser Login(String userName, String password)
        {
            return userFacade.Login(userName, password);
        }
        #endregion
    }
}
