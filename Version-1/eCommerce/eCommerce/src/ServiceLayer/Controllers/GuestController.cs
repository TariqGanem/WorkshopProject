using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.Response;
using System;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public interface IGuestController
    {
        Response<GuestUserSO> Login();
        void Logout(String userId);
        Response<RegisteredUserSO> Register(string username, string password);

    }

    public class GuestController : IGuestController
    {
        #region parameters
        protected ISystemFacade SystemFacade;
        #endregion

        public GuestController(ISystemFacade systemFacade)
        {
            SystemFacade = systemFacade;
        }

        #region GuestControllerMethods
        public Response<GuestUserSO> Login()
        {
            return new Response<RegisteredUserSO>(SystemFacade.Login());
        }

        public void Logout(string userId)
        {
            throw new NotImplementedException();
        }


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

        public Response<RegisteredUserSO> Register(string username, string password)
        {
            if (username == null || username == "")
                return new Response<RegisteredUserSO>("The username is invalid!!!");
            if (password == null || password == "")
                return new Response<RegisteredUserSO>("The password is invalid!!!");
            try
            {
                RegisteredUserSO user = new RegisteredUserSO(SystemFacade.Register(username, password));
                return new Response<RegisteredUserSO>(user, null);
            }
            catch (Exception e)
            {
                return new Response<RegisteredUserSO>(e.Message);
            }
        }
        #endregion
    }
}
