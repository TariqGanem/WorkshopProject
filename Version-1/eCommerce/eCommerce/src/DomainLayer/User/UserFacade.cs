using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User.Roles;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace eCommerce.src.DomainLayer.User
{
    public interface IUserFacade
    {
        GuestUser Login();
        RegisteredUser Login(String userName, String password);
        void Logout(String userId);
        RegisteredUser Register(String userName, String password);
        History GetUserPurchaseHistory(String userId);
        void AddProductToCart(string userId, Product product, int quantity, Store.Store store);
        void UpdateShoppingCart(string userId, string storeId, Product product, int quantity);
        ShoppingCart GetUserShoppingCart(string userId);
        Double GetTotalShoppingCartPrice(String userId);
        ShoppingCart Purchase(String userId, IDictionary<String, Object> paymentDetails, IDictionary<String, Object> deliveryDetails);
        RegisteredUser AddSystemAdmin(String userName);
        RegisteredUser RemoveSystemAdmin(String userName);

    }

    public class UserFacade : IUserFacade
    {
        #region parameters
        public ConcurrentDictionary<String, RegisteredUser> SystemAdmins { get; }
        public ConcurrentDictionary<String, RegisteredUser> RegisteredUsers { get; }
        public ConcurrentDictionary<String, GuestUser> GuestUsers { get; }

        private readonly object my_lock = new object();
        #endregion

        #region constructors
        public UserFacade()
        {
            SystemAdmins = new ConcurrentDictionary<string, RegisteredUser>();
            RegisteredUsers = new ConcurrentDictionary<string, RegisteredUser>();
            GuestUsers = new ConcurrentDictionary<string, GuestUser>();
        }
        #endregion

        #region methods
        public GuestUser Login()
        {
            GuestUser guest = new GuestUser();
            GuestUsers.TryAdd(guest.Id, guest);
            return guest;
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
            try
            {
                Monitor.TryEnter(my_lock);
                try
                {
                    if (isUniqueEmail(userName))
                    {
                        RegisteredUser newUser = new RegisteredUser(userName, password);
                        this.RegisteredUsers.TryAdd(newUser.Id, newUser);
                        return newUser;
                    }
                    else
                    {
                        throw new Exception($"{userName} is aleady in user\n Please use different email!");
                    }
                }
                finally
                {
                    Monitor.Exit(my_lock);
                }
            }
            catch (SynchronizationLockException SyncEx)
            {
                throw new Exception("A SynchronizationLockException occurred. Message:" + SyncEx.Message);
            }
        }
        public History GetUserPurchaseHistory(string userId)
        {
            if (RegisteredUsers.TryGetValue(userId, out RegisteredUser user))
            {
                return user.History;
            }
            throw new Exception("Not a registered user!");
        }

        public void AddProductToCart(string userId, Product product, int quantity, Store.Store store)
        {
            if (RegisteredUsers.TryGetValue(userId, out RegisteredUser user))   // Check if user is registered
            {
                user.AddProductToCart(product, quantity, store);
            }
            else if (GuestUsers.TryGetValue(userId, out GuestUser guest))   // Check if active guest
            {
                guest.AddProductToCart(product, quantity, store);
            }
            //else failed
            throw new Exception($"User (ID: {userId}) does not exists!");
        }

        public void UpdateShoppingCart(string userId, string storeId, Product product, int quantity)
        {
            if (GuestUsers.TryGetValue(userId, out GuestUser guest_user))
            {
                guest_user.UpdateShoppingCart(storeId, product, quantity);
            }
            else if (RegisteredUsers.TryGetValue(userId, out RegisteredUser registerd_user))
            {
                registerd_user.UpdateShoppingCart(storeId, product, quantity);
            }
            else
            {
                throw new Exception("User does not exist!");
            }
        }

        public ShoppingCart GetUserShoppingCart(string userId)
        {
            if (GuestUsers.TryGetValue(userId, out GuestUser guest_user))
            {
                return guest_user.ShoppingCart;
            }
            else if (RegisteredUsers.TryGetValue(userId, out RegisteredUser registerd_user))
            {
                return registerd_user.ShoppingCart;
            }
            else
            {
                throw new Exception($"There is no suck user with ID:{userId}!");
            }
        }

        public double GetTotalShoppingCartPrice(string userID)
        {
            ShoppingCart shoppingCart = GetUserShoppingCart(userID);
            return shoppingCart.GetTotalShoppingCartPrice();
        }

        public ShoppingCart Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            if (GuestUsers.TryGetValue(userId, out GuestUser guest_user))
            {
                return guest_user.Purchase(paymentDetails, deliveryDetails);
            }
            else if (RegisteredUsers.TryGetValue(userId, out RegisteredUser registerd_user))
            {
                ShoppingCart shoppingCart = registerd_user.Purchase(paymentDetails, deliveryDetails);
                registerd_user.History.AddPurchasedShoppingCart(shoppingCart);
                return shoppingCart;
            }
            else
            {
                throw new Exception("User does not exist!");
            }
        }

        public RegisteredUser AddSystemAdmin(string userName)
        {
            RegisteredUser admin = GetUserByUserName(userName);
            //registered user has been found
            this.SystemAdmins.TryAdd(admin.Id, admin);
            return admin;
        }

        public RegisteredUser RemoveSystemAdmin(string userName)
        {
            RegisteredUser searchResult = GetUserByUserName(userName);
            RegisteredUser removedUser;
            //registered user has been found
            //Check the constrain for at least one system admin
            if (this.SystemAdmins.Count > 1)
            {
                this.SystemAdmins.TryRemove(searchResult.Id, out removedUser);
                return removedUser;
            }

            //there is only one system admin
            else
            {
                throw new Exception($"{userName} could not be removed as system admin\n The system need at least one system admin!");

            }

        }
        public Boolean isSystemAdmin(String userId)
        {
            return SystemAdmins.ContainsKey(userId);
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
        private Boolean isUniqueEmail(string userName)
        {
            foreach (RegisteredUser registerUser in RegisteredUsers.Values)
            {
                if (registerUser.UserName.Equals(userName))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
