using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Controllers
{
    internal interface IRegisteredUserController
    {
        Response<RegisteredUser> Login(String userName, String password);
    }

    internal class RegisteredUserController : GuestController, IRegisteredUserController
    {
        #region constructors
        public RegisteredUserController(ISystemFacade systemFacade) : base(systemFacade) { }
        #endregion

        #region GuestUserInterfaceMethods
        public Response<RegisteredUser> Login(String userName, String password)
        {
            RegisteredUser registeredUser = null;
            try
            {
                ValidateCredentials(userName, password);
                registeredUser = SystemFacade.Login(userName, password);
            }
            catch (Exception e)
            {
                return new Response<RegisteredUser>(e.Message);
            }
            return new Response<RegisteredUser>(registeredUser);
        }
        #endregion

        #region privateMethods
        private void ValidateCredentials(String userName, String password)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("Username is null!");
            }
            if (password == null)
            {
                throw new ArgumentNullException("Password is null!");
            }
        }
        #endregion
    }
}
