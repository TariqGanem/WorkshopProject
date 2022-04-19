using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.Response;
using System;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public interface IGuestController
    {
       /* public Response<guestUser> EnterSystem();
        public Response<string> ExitSystem(string userID);
        public Response<registeredUser> Register(string username, string email, string password);*/
        
    }

    public class GuestController : IGuestController
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
/*        public Response<guestUser> EnterSystem()
        {
            GuestUser output = SystemFacade.Login();
            guestUser guestUser = new guestUser(output);
            return new Response<guestUser>(guestUser,null);
        }

        public Response<string> ExitSystem(string userID)
        {
            if (userID == null || userID == "")
                return new Response<string>("the userId is empty!!!");
            SystemFacade.ExitSystem(userID);
            return new Response<string>(null);
        }*/

        public Response<registeredUser> Register(string username, string email, string password)
        {
            if (username == null || username == "")
                return new Response<registeredUser>("The username is invalid!!!");
            if (email == null || email == "")
                return new Response<registeredUser>("The email is invalid!!!");
            if (password == null || password == "")
                return new Response<registeredUser>("The password is invalid!!!");
            try
            {
                registeredUser user = new registeredUser(SystemFacade.Register(username, password));
                return new Response<registeredUser>(user, null);
            }catch (Exception e)
            {
                return new Response<registeredUser>(e.Message);
            }
        }
        #endregion
    }
}
