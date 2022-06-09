using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.Offer;
using eCommerce.src.DomainLayer.User.Roles;
using MongoDB.Bson;
using MongoDB.Driver;
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
        ShoppingCart Purchase(String userId, IDictionary<String, Object> paymentDetails, IDictionary<String, Object> deliveryDetails, MongoDB.Driver.IClientSessionHandle session);
        RegisteredUser AddSystemAdmin(String userName);
        RegisteredUser RemoveSystemAdmin(String userName);
        RegisteredUser RemoveRegisteredUser(String userName);

    }

    public class UserFacade : IUserFacade
    {
        #region parameters
        public ConcurrentDictionary<String, RegisteredUser> SystemAdmins { get; }
        public ConcurrentDictionary<String, RegisteredUser> RegisteredUsers { get; }
        public ConcurrentDictionary<String, GuestUser> GuestUsers { get; }
        public DBUtil dbutil = DBUtil.getInstance();

        private readonly object my_lock = new object();
        #endregion

        #region constructors
        public UserFacade()
        {
            SystemAdmins = new ConcurrentDictionary<string, RegisteredUser>();
            RegisteredUsers = new ConcurrentDictionary<string, RegisteredUser>();
            GuestUsers = new ConcurrentDictionary<string, GuestUser>();
            LoadAllRegisterUsers();
            LoadSystemAdmins();
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

            // DB
            var filter = Builders<BsonDocument>.Filter.Eq("_id", registeredUser.Id);
            var update = Builders<BsonDocument>.Update.Set("Active", true);
            dbutil.UpdateRegisteredUser(filter, update);
            // 
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
                var filter = Builders<BsonDocument>.Filter.Eq("_id", registeredUser.Id);
                var update = Builders<BsonDocument>.Update.Set("Active", false);
                dbutil.UpdateRegisteredUser(filter, update);
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
                        dbutil.Create(newUser);
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
                var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
                var update = Builders<BsonDocument>.Update.Set("ShoppingCart", user.ShoppingCart.getDTO());
                dbutil.UpdateRegisteredUser(filter, update);
            }
            else if (GuestUsers.TryGetValue(userId, out GuestUser guest))   // Check if active guest
            {
                guest.AddProductToCart(product, quantity, store);
            }
            else
            {
                throw new Exception($"User (ID: {userId}) does not exists!");
            }
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
                var filter = Builders<BsonDocument>.Filter.Eq("_id", registerd_user.Id);
                var update = Builders<BsonDocument>.Update.Set("ShoppingCart", registerd_user.ShoppingCart.getDTO());
                dbutil.UpdateRegisteredUser(filter, update);
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
                throw new Exception($"There is no such user with ID:{userId}!");
            }
        }

        public double GetTotalShoppingCartPrice(string userID)
        {
            if (RegisteredUsers.ContainsKey(userID))
            {
                Double TotalPrice = RegisteredUsers[userID].ShoppingCart.GetTotalShoppingCartPrice(RegisteredUsers[userID].getAcceptedOffers());
                return TotalPrice;
            }
            else if (GuestUsers.ContainsKey(userID))
            {
                //Guest User Found
                Double TotalPrice = GuestUsers[userID].ShoppingCart.GetTotalShoppingCartPrice(GuestUsers[userID].getAcceptedOffers());
                return TotalPrice;
            }
            else
            {
                throw new Exception($"There is no user with ID:{userID}\n");
            }
        }

        public ShoppingCart Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails , MongoDB.Driver.IClientSessionHandle session = null)
        {
            if (GuestUsers.TryGetValue(userId, out GuestUser guest_user))
            {
                ShoppingCart ShoppingCart = guest_user.Purchase(paymentDetails, deliveryDetails,guest_user, session);
                return ShoppingCart;
            }
            else if (RegisteredUsers.TryGetValue(userId, out RegisteredUser registerd_user))
            {
                ShoppingCart ShoppingCart = registerd_user.Purchase(paymentDetails, deliveryDetails, session);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", registerd_user.Id);
                ShoppingCart sc = registerd_user.ShoppingCart;
                var update_shoppingcart = Builders<BsonDocument>.Update.Set("ShoppingCart", sc.getDTO());
                dbutil.UpdateRegisteredUser(filter, update_shoppingcart, session: session);
                return ShoppingCart;
            }
            else { throw new Exception("User does not exist\n"); }
        }

        public RegisteredUser AddSystemAdmin(string userName)
        {
            RegisteredUser admin = GetUserById(userName);
            //registered user has been found
            this.SystemAdmins.TryAdd(admin.Id, admin);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ""); 
            var update = Builders<BsonDocument>.Update.Set("SystemAdmins", getDTO_admins().SystemAdmins);
            dbutil.UpdateSystemAdmins(filter, update);
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
                var filter = Builders<BsonDocument>.Filter.Eq("_id", "");  
                var update = Builders<BsonDocument>.Update.Set("SystemAdmins", getDTO_admins().SystemAdmins);
                dbutil.UpdateSystemAdmins(filter, update);
                return removedUser;
            }

            //there is only one system admin
            else
            {
                throw new Exception($"{userName} could not be removed as system admin\n The system need at least one system admin!");

            }

        }

        public RegisteredUser RemoveRegisteredUser(string userName)
        {
            RegisteredUser searchResult = GetUserByUserName(userName);
            return searchResult;
        }

        public Boolean isSystemAdmin(String userId)
        {
            return SystemAdmins.ContainsKey(userId);
        }

        public DTO_SystemAdmins getDTO_admins()
        {
            LinkedList<String> admins_dto = new LinkedList<string>();

            foreach (var admin in SystemAdmins)
            {
                admins_dto.AddLast(admin.Key);
            }
            return new DTO_SystemAdmins(admins_dto);
        }

        public void LoadAllRegisterUsers()
        {
            List<RegisteredUser> _registeredUsers = dbutil.LoadAllRegisterUsers();
            foreach (RegisteredUser registered in _registeredUsers)
            {
                RegisteredUsers.TryAdd(registered.Id, registered);
            }
        }
        public void LoadSystemAdmins()
        {
            LinkedList<String> systemAdminsIDs = dbutil.LoadAllSystemAdmins();
            if (!(systemAdminsIDs is null))
            {
                foreach (string id in systemAdminsIDs)
                {
                    RegisteredUser user;
                    RegisteredUsers.TryGetValue(id, out user);
                    SystemAdmins.TryAdd(id, user);
                }
            }

        }

        public String getRegUserIdByUsername(String username)
        {
            foreach (KeyValuePair<String, RegisteredUser> kvp in this.RegisteredUsers)
                if (kvp.Value.UserName == username)
                    return kvp.Key;
            return "";
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

        private RegisteredUser GetUserById(String Id)
        {
            foreach (RegisteredUser registeredUser in RegisteredUsers.Values)
            {
                if (registeredUser.Id.Equals(Id))
                {
                    return registeredUser;
                }
            }
            throw new Exception($"Username {Id} doesn't exist!");
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

        public Offer SendOfferToStore(string storeID, string userID, string productID, int amount, double price)
        {
            if (GuestUsers.TryGetValue(userID, out GuestUser guest_user))
                return guest_user.SendOfferToStore(storeID, productID, amount, price);
            else if (RegisteredUsers.TryGetValue(userID, out RegisteredUser registerd_user))
                return registerd_user.SendOfferToStore(storeID, productID, amount, price);
            throw new Exception("Failed to create offer: Failed to locate the user");
        }

        public void RemoveOffer(string userID, String id)
        {
            if (GuestUsers.TryGetValue(userID, out GuestUser guest_user))
                guest_user.RemovePendingOffer(id);
            else if (RegisteredUsers.TryGetValue(userID, out RegisteredUser registerd_user))
                registerd_user.RemovePendingOffer(id);
            throw new Exception("Failed to remove offer: Failed to locate the user");
        }

        public bool AnswerCounterOffer(string userID, string offerID, bool accepted)
        {
            if (GuestUsers.TryGetValue(userID, out GuestUser guest_user))
                return guest_user.AnswerCounterOffer(offerID, accepted);
            else if (RegisteredUsers.TryGetValue(userID, out RegisteredUser registerd_user))
                return registerd_user.AnswerCounterOffer(offerID, accepted);
            throw new Exception("Failed to respond to a counter offer: Failed to locate the user");
        }
        #endregion
    }
}
