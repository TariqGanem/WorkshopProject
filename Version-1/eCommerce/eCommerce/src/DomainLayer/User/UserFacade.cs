using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User.Roles;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public interface IUserFacade
    {
        GuestUser Login();
        RegisteredUser Login(String userName, String password);
        void Logout(String userId);
        RegisteredUser Register(String userName, string email, String password);
        History GetUserPurchaseHistory(String userId);
        Boolean AddProductToCart(string userId, Product product, int quantity, Store.Store store);
        Boolean UpdateShoppingCart(string userId, string storeId, Product product, int quantity);
        ShoppingCart GetUserShoppingCart(string userId);
        Double GetTotalShoppingCartPrice(String userID);
        ShoppingCart Purchase(String userId, IDictionary<String, Object> paymentDetails, IDictionary<String, Object> deliveryDetails);
        RegisteredUser AddSystemAdmin(String userName);
        RegisteredUser RemoveSystemAdmin(String userName);

    }

    public class UserFacade //: //IUserFacade
    {
        #region parameters
        public ConcurrentDictionary<String, SystemAdmin> SystemAdmins { get; }
        public ConcurrentDictionary<String, RegisteredUser> RegisteredUsers { get; }
        public ConcurrentDictionary<String, GuestUser> GuestUsers { get; }

        private readonly object my_lock = new object();
        public int id_user_counter { get; private set; }
        #endregion

        #region constructors
        public UserFacade()
        {
            SystemAdmins = new ConcurrentDictionary<string, SystemAdmin>();
            RegisteredUsers = new ConcurrentDictionary<string, RegisteredUser>();
            GuestUsers = new ConcurrentDictionary<string, GuestUser>();
            this.id_user_counter = 0;
        }
        #endregion

        #region methods
        public GuestUser EnterSystem()
        {
            GuestUser guest = null;
            int id;
            lock (my_lock)
            {
                id = id_user_counter++;
            }
            lock (GuestUsers) 
            { 
                guest = new GuestUser(id.ToString());
                GuestUsers[id_user_counter.ToString()] = guest;
            }
            return guest;
        }

        public void ExitSystem(string id)
        {
            GuestUser guest;
            GuestUsers.TryRemove(id, out guest);
        }

        public RegisteredUser Login(String userName, String password)
        {
            RegisteredUser registeredUser = GetUserByUserName(userName);
            if (registeredUser == null)
            {
                throw new Exception($"The username {userName} doesn't exist!");
            }
            registeredUser.Login(password);
            return registeredUser;
        }

        public void Logout(String userId)
        {
            if (GuestUsers.TryGetValue(userId, out GuestUser guestUser))
            {
                guestUser.Logout();
                GuestUsers.Remove(userId, out GuestUser dump);
            }
            else if (RegisteredUsers.TryGetValue(userId, out RegisteredUser registeredUser))
            {
                registeredUser.Logout();
            }
            else
            {
                throw new Exception("Cannot logout, user doesn't exist!");
            }
        }

        public RegisteredUser Register(string userName, string email, string password)
        {
            RegisteredUser user;
            int id;
            lock (my_lock)
            {
                id = id_user_counter++;
            }
            lock (RegisteredUsers)
            {
                user = new RegisteredUser(id.ToString(),userName,email,password);
                RegisteredUsers[id.ToString()] = user;
            }
            return user;
        }
        #endregion

        #region privateMethods
        private RegisteredUser GetUserByUserName(String userName)
        {
            foreach (RegisteredUser registeredUser in RegisteredUsers.Values)
            {
                if (registeredUser.UserName.Equals(userName))
                {
                    return registeredUser;
                }
            }
            throw new Exception($"Username {userName} doesn't exist!");
        }
        #endregion
    }
}
