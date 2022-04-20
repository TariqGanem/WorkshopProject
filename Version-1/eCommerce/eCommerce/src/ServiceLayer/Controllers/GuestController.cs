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
        Response.Result Logout(String userId);
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
            return new Response<GuestUserSO>(SystemFacade.Login());
        }

        public Response.Result Logout(string userId)
        {
            if (userId == null || userId == "")
                return new Response.Result("The userId is empty!!!");
            SystemFacade.Logout(userId);
            return new Response.Result();
        }

        public Response<RegisteredUserSO> Register(string username, string password)
        {
            if (username == null || username == "")
                return new Response<RegisteredUserSO>("The username is invalid!!!");
            if (password == null || password == "")
                return new Response<RegisteredUserSO>("The password is invalid!!!");
            try
            {
                RegisteredUserSO user = SystemFacade.Register(username, password);
                return new Response<RegisteredUserSO>(user);
            }
            catch (Exception e)
            {
                return new Response<RegisteredUserSO>(e.Message);
            }
        }
        #endregion
    }
}
