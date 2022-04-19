using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.Response;
using System;

namespace eCommerce.src.ServiceLayer.Controllers
{
    internal interface IGuestController
    {
        Response<guestUser> EnterSystem();
        Response<string> ExitSystem(string userID);
        Response<registeredUser> Register(string email, string password);
    }

    internal class GuestController : IGuestController
    {
        #region parameters
        protected ISystemFacade SystemFacade;
        #endregion

        #region constructors
        public GuestController(ISystemFacade systemFacade)
        {
            SystemFacade = systemFacade;
        }
        #endregion

        #region GuestControllerMethods
        public Response<guestUser> EnterSystem()
        {
            GuestUser output = SystemFacade.EnterSystem();
            guestUser guestUser = new guestUser(output);
            return new Response<guestUser>(guestUser,"Welcome to the eCommerce");
        }

        public Response<string> ExitSystem(string userID)
        {
            SystemFacade.ExitSystem(userID);
            return new Response<string>("goodbye.");
        }

        public Response<registeredUser> Register(string email, string password)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
