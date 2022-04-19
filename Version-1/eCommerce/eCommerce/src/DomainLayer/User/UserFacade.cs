using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User.Roles;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    internal interface IUserFacade
    {
        GuestUser EnterSystem();
        void ExitSystem(string id);
        RegisteredUser Login(String userName, String password);
        void Logout(String userId);
        RegisteredUser Register(String userName, String password);
        History GetUserPurchaseHistory(String userId);
        Boolean AddProductToCart(string userId, Product product, int quantity, Store.Store store);
        Boolean UpdateShoppingCart(string userId, string storeId, Product product, int quantity);
        ShoppingCart GetUserShoppingCart(string userId);
        Double GetTotalShoppingCartPrice(String userID);
        ShoppingCart Purchase(String userId, IDictionary<String, Object> paymentDetails, IDictionary<String, Object> deliveryDetails);
        RegisteredUser AddSystemAdmin(String userName);
        RegisteredUser RemoveSystemAdmin(String userName);

    }

    internal class UserFacade : IUserFacade
    {
        #region parameters
        public ConcurrentDictionary<String, SystemAdmin> SystemAdmins { get; }
        public ConcurrentDictionary<String, RegisteredUser> RegisteredUsers { get; }
        public ConcurrentDictionary<String, GuestUser> GuestUsers { get; }

        private readonly object my_lock = new object();
        #endregion

        #region constructors
        public int id_user_counter { get; private set; }

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
            lock (my_lock)
            {
                guest = new GuestUser(id_user_counter.ToString());
                GuestUsers[id_user_counter.ToString()] = guest;
                id_user_counter++;
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

        public RegisteredUser Register(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public History GetUserPurchaseHistory(string userId)
        {
            throw new NotImplementedException();
        }

        public bool AddProductToCart(string userId, Product product, int quantity, Store.Store store)
        {
            throw new NotImplementedException();
        }

        public bool UpdateShoppingCart(string userId, string storeId, Product product, int quantity)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart GetUserShoppingCart(string userId)
        {
            throw new NotImplementedException();
        }

        public double GetTotalShoppingCartPrice(string userID)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            throw new NotImplementedException();
        }

        public RegisteredUser AddSystemAdmin(string userName)
        {
            throw new NotImplementedException();
        }

        public RegisteredUser RemoveSystemAdmin(string userName)
        {
            throw new NotImplementedException();
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
