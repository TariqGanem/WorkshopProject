using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.ServiceLayer.Objects;
using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using eCommerce.src.DataAccessLayer.DataTransferObjects;

namespace eCommerce.src.DataAccessLayer
{
    public class DBUtil
    {
        private static DBUtil Instance = null;
        public MongoClient dbClient;
        public IMongoDatabase db;

        public DAO<DTO_RegisteredUser> DAO_RegisteredUser;
        public DAO<DTO_Product> DAO_Product;
        public DAO<DTO_StoreManager> DAO_StoreManager;
        public DAO<DTO_StoreOwner> DAO_StoreOwner;
        public DAO<DTO_Store> DAO_Store;
        public DAO<DTO_SystemAdmins> DAO_SystemAdmins;
        public DAO<DTO_Auction> DAO_Auction;
        public DAO<DTO_Lottery> DAO_Lottery;
        public DAO<DTO_MaxProductPolicy> DAO_MaxProductPolicy;
        public DAO<DTO_MinAgePolicy> DAO_MinAgePolicy;
        public DAO<DTO_MinProductPolicy> DAO_MinProductPolicy;
        public DAO<DTO_Offer> DAO_Offer;
        public DAO<DTO_RestrictedHoursPolicy> DAO_RestrictedHoursPolicy;
        public DAO<DTO_AndPolicy> DAO_AndPolicy;
        public DAO<DTO_OrPolicy> DAO_OrPolicy;
        public DAO<DTO_BuyNow> DAO_BuyNow;
        public DAO<DTO_ConditionalPolicy> DAO_ConditionalPolicy;
        public DAO<DTO_VisibleDiscount> DAO_VisibleDiscount;
        public DAO<DTO_DiscountTargetCategories> DAO_DiscountTargetCategories;
        public DAO<DTO_DiscountTargetProducts> DAO_DiscountTargetProducts;
        public DAO<DTO_DiscreetDiscount> DAO_DiscreetDiscount;
        public DAO<DTO_ConditionalDiscount> DAO_ConditionalDiscount;
        public DAO<DTO_MinProductCondition> DAO_MinProductCondition;
        public DAO<DTO_MinBagPriceCondition> DAO_MinBagPriceCondition;
        public DAO<DTO_MaxProductCondition> DAO_MaxProductCondition;
        public DAO<DTO_DiscountConditionOr> DAO_DiscountConditionOr;
        public DAO<DTO_DiscountConditionAnd> DAO_DiscountConditionAnd;
        public DAO<DTO_DiscountXor> DAO_DiscountXor;
        public DAO<DTO_DiscountOr> DAO_DiscountOr;
        public DAO<DTO_DiscountMin> DAO_DiscountMin;
        public DAO<DTO_DiscountMax> DAO_DiscountMax;
        public DAO<DTO_DiscountAnd> DAO_DiscountAnd;
        public DAO<DTO_DiscountAddition> DAO_DiscountAddition;

        public ConcurrentDictionary<String, RegisteredUser> RegisteredUsers;
        public ConcurrentDictionary<String, GuestUser> GuestUsers;
        public ConcurrentDictionary<String, Product> Products;
        public ConcurrentDictionary<String, LinkedList<StoreManager>> StoreManagers;
        public ConcurrentDictionary<String, LinkedList<StoreOwner>> StoreOwners;
        public ConcurrentDictionary<String, Store> Stores;
        public ConcurrentDictionary<String, Auction> Policy_Auctions;
        public ConcurrentDictionary<String, Lottery> Policy_Lotterys;
        public ConcurrentDictionary<String, MaxProductPolicy> Policy_MaxProductPolicys;
        public ConcurrentDictionary<String, MinAgePolicy> Policy_MinAgePolicys;
        public ConcurrentDictionary<String, MinProductPolicy> Policy_MinProductPolicys;
        public ConcurrentDictionary<String, Offer> Policy_Offers;
        public ConcurrentDictionary<String, RestrictedHoursPolicy> Policy_RestrictedHoursPolicys;
        public ConcurrentDictionary<String, AndPolicy> Policy_AndPolicys;
        public ConcurrentDictionary<String, OrPolicy> Policy_OrPolicys;
        public ConcurrentDictionary<String, BuyNow> Policy_BuyNows;
        public ConcurrentDictionary<String, ConditionalPolicy> Policy_ConditionalPolicys;
        public ConcurrentDictionary<String, VisibleDiscount> Discount_VisibleDiscounts;
        public ConcurrentDictionary<String, DiscountTargetCategories> Discount_DiscountTargetCategories;
        public ConcurrentDictionary<String, DiscountTargetProducts> Discount_DiscountTargetProducts;
        public ConcurrentDictionary<String, DiscreetDiscount> Discount_DiscreetDiscounts;
        public ConcurrentDictionary<String, ConditionalDiscount> Discount_ConditionalDiscounts;
        public ConcurrentDictionary<String, MinProductCondition> Discount_MinProductConditions;
        public ConcurrentDictionary<String, MinBagPriceCondition> Discount_MinBagPriceConditions;
        public ConcurrentDictionary<String, MaxProductCondition> Discount_MaxProductConditions;
        public ConcurrentDictionary<String, DiscountConditionOr> Discount_DiscountConditionOrs;
        public ConcurrentDictionary<String, DiscountConditionAnd> Discount_DiscountConditionAnds;
        public ConcurrentDictionary<String, DiscountXor> Discount_DiscountXors;
        public ConcurrentDictionary<String, DiscountOr> Discount_DiscountOrs;
        public ConcurrentDictionary<String, DiscountMin> Discount_DiscountMins;
        public ConcurrentDictionary<String, DiscountMax> Discount_DiscountMaxs;
        public ConcurrentDictionary<String, DiscountAnd> Discount_DiscountAnds;
        public ConcurrentDictionary<String, DiscountAddition> Discount_DiscountAdditions;

        private DBUtil(String connection_url , String db_name)
        {
            //String dbName = "XMart";

            this.dbClient = new MongoClient(connection_url);
            db = dbClient.GetDatabase(db_name);

            DAO_RegisteredUser = new DAO<DTO_RegisteredUser>(db, "Users");
            DAO_Product = new DAO<DTO_Product>(db, "Products");
            DAO_StoreManager = new DAO<DTO_StoreManager>(db, "StoreStaff");
            DAO_StoreOwner = new DAO<DTO_StoreOwner>(db, "StoreStaff");
            DAO_Store = new DAO<DTO_Store>(db, "Stores");
            DAO_SystemAdmins = new DAO<DTO_SystemAdmins>(db, "SystemAdmins");

            RegisteredUsers = new ConcurrentDictionary<String, RegisteredUser>();
            GuestUsers = new ConcurrentDictionary<String, GuestUser>();
            Products = new ConcurrentDictionary<String, Product>();
            StoreManagers = new ConcurrentDictionary<String, LinkedList<StoreManager>>();
            StoreOwners = new ConcurrentDictionary<String, LinkedList<StoreOwner>>();
            Stores = new ConcurrentDictionary<String, Store>();
        }

        public static DBUtil getInstance()
        {
            return Instance;
        }

        public static DBUtil getInstance(String connection_url , String db_name)
        {
            if (Instance == null)
            {
                Instance = new DBUtil(connection_url , db_name);
            }
            return Instance;
        }

        // 2DTO
        public DTO_ShoppingCart Get_DTO_ShoppingCart(User user)
        {
            ConcurrentDictionary<String, DTO_ShoppingBag> dto_sb = new ConcurrentDictionary<String, DTO_ShoppingBag>();
            foreach (var sb in user.ShoppingCart.ShoppingBags)
            {
                ConcurrentDictionary<String, int> dto_products = new ConcurrentDictionary<string, int>();
                foreach (var p in sb.Value.Products)
                {
                    dto_products.TryAdd(p.Key.Id, p.Value);
                }
                dto_sb.TryAdd(sb.Key, new DTO_ShoppingBag(sb.Value.Id, sb.Value.UserId, sb.Value.Store.Id, dto_products, sb.Value.TotalBagPrice));
            }
            DTO_ShoppingCart dto_sc = new DTO_ShoppingCart(user.ShoppingCart.Id, dto_sb, user.ShoppingCart.TotalCartPrice);

            return dto_sc;
        }

        private DTO_ShoppingBag Get_DTO_ShoppingBag(ShoppingBag sb)
        {
            ConcurrentDictionary<String, int> dto_products = new ConcurrentDictionary<string, int>();
            foreach (var p in sb.Products)
            {
                dto_products.TryAdd(p.Key.Id, p.Value); //<Product id , quantity>
            }
            return new DTO_ShoppingBag(sb.Id, sb.UserId, sb.Store.Id, dto_products, sb.TotalBagPrice);
        }

        public DTO_History Get_DTO_History(History history)
        {
            LinkedList<DTO_PurchasedShoppingBag> dto_sb = new LinkedList<DTO_PurchasedShoppingBag>();
            foreach (ShoppingBag bag in history.ShoppingBags)
            {
                LinkedList<DTO_PurchasedProduct> dto_products = new LinkedList<DTO_PurchasedProduct>();
                foreach (KeyValuePair<Product, int> tuple in bag.Products)
                {
                    dto_products.AddLast(new DTO_PurchasedProduct(tuple.Key.Id, tuple.Key.Name, tuple.Key.Price, tuple.Value, tuple.Key.Category));
                }

                DTO_PurchasedShoppingBag dto_bag = new DTO_PurchasedShoppingBag(bag.Id, bag.UserId, bag.Store.Id, dto_products, bag.TotalBagPrice);
                dto_sb.AddLast(dto_bag);
            }
            return new DTO_History(dto_sb);
        }

        private LinkedList<DTO_Notification> Get_DTO_Notifications(LinkedList<Notification> pendingNotifications)
        {
            LinkedList<DTO_Notification> dto_pendingNotifications = new LinkedList<DTO_Notification>();
            foreach (Notification n in pendingNotifications)
            {
                dto_pendingNotifications.AddLast(new DTO_Notification(n.Message, n.Date.ToString(), n.isOpened, n.isStoreStaff, n.ClientId));
            }

            return dto_pendingNotifications;
        }

        public LinkedList<String> Get_DTO_ManagerList(LinkedList<StoreManager> list)
        {
            LinkedList<String> managers = new LinkedList<String>();

            foreach (StoreManager manager in list)
            {
                managers.AddLast(manager.User.Id);
            }

            return managers;
        }

        public LinkedList<String> Get_DTO_OwnerList(LinkedList<StoreOwner> list)
        {
            LinkedList<String> owners = new LinkedList<String>();

            foreach (StoreOwner owner in list)
            {
                owners.AddLast(owner.User.Id);
            }

            return owners;
        }
        // 2OBJ
        private ShoppingCart ToObject(DTO_ShoppingCart dto, User user)
        {
            ConcurrentDictionary<String, ShoppingBag> sb = new ConcurrentDictionary<String, ShoppingBag>();
            foreach (var bag in dto.ShoppingBags)
            {
                ConcurrentDictionary<Product, int> products = new ConcurrentDictionary<Product, int>();
                foreach (var p in bag.Value.Products)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", p.Key);
                    products.TryAdd(LoadProduct(filter), p.Value);
                }
                Store store = getStoreById(bag.Value.StoreId);
                sb.TryAdd(bag.Key, new ShoppingBag(bag.Key, user.Id , store , products, bag.Value.TotalBagPrice));
            }
            ShoppingCart sc = new ShoppingCart(dto._id, sb, dto.TotalCartPrice);
            return sc;
        }
        private History ToObject(DTO_History dto)
        {
            LinkedList<ShoppingBag> shoppingBags = new LinkedList<ShoppingBag>();
            foreach (DTO_PurchasedShoppingBag bag in dto.ShoppingBags)
            {
                ConcurrentDictionary<Product, int> products = new ConcurrentDictionary<Product, int>();
                foreach (DTO_PurchasedProduct p in bag.Products)
                {
                    Product toadd = new Product(p._id, p.Name, p.Price, p.ProductQuantity, p.Category);
                    products[toadd] = p.ProductQuantity;
                }
                Store store = getStoreById(bag.StoreId);
                shoppingBags.AddLast(new ShoppingBag(bag._id, bag.UserId, store, products, bag.TotalBagPrice));
            }
            return new History(shoppingBags);
        }

        private LinkedList<Notification> ToObject(LinkedList<DTO_Notification> dto_list)
        {
            LinkedList<Notification> pendingNotifications = new LinkedList<Notification>();
            foreach (DTO_Notification n in dto_list)
            {
                pendingNotifications.AddLast(new Notification(n.ClientId, n.Message, n.isStoreStaff, n.isOpened, n.Date));
            }
            return pendingNotifications;
        }

        private Store getStoreById(String Id)
        {
            Stores.TryGetValue(Id, out Store store);
            return store;
        }

        // load all DTOS
        public List<RegisteredUser> LoadAllRegisterUsers()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> docs = DAO_RegisteredUser.Documents.Find(filter).ToList();
            List<DTO_RegisteredUser> registerUsersDTO = new List<DTO_RegisteredUser>();
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                DTO_RegisteredUser dto = JsonConvert.DeserializeObject<DTO_RegisteredUser>(json);
                registerUsersDTO.Add(dto);
            }
            List<RegisteredUser> registeredUsers = new List<RegisteredUser>();
            foreach (DTO_RegisteredUser dto in registerUsersDTO)
            {
                RegisteredUser registerUser = LoadRegisteredUser(Builders<BsonDocument>.Filter.Eq("_id", dto._id));
                registeredUsers.Add(registerUser);
            }
            return registeredUsers;
        }

        public LinkedList<String> LoadAllSystemAdmins()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> docs = DAO_SystemAdmins.Documents.Find(filter).ToList();
            List<DTO_SystemAdmins> systemAdminDTO = new List<DTO_SystemAdmins>();
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                DTO_SystemAdmins dto = JsonConvert.DeserializeObject<DTO_SystemAdmins>(json);
                systemAdminDTO.Add(dto);
            }
            if (systemAdminDTO.Count > 0)
            {
                return systemAdminDTO[0].SystemAdmins;
            }
            else { return null; }
        }

        public List<Store> LoadAllStores()
        {
            //load all stores dto
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> docs = DAO_Store.Documents.Find(filter).ToList();
            List<DTO_Store> storesDTOs = new List<DTO_Store>();
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                DTO_Store dto = JsonConvert.DeserializeObject<DTO_Store>(json);
                storesDTOs.Add(dto);
            }
            List<Store> stores = new List<Store>();
            foreach (DTO_Store dto in storesDTOs)
            {
                var f = Builders<BsonDocument>.Filter.Eq("_id", dto._id);
                Store s = LoadStore(f);
                stores.Add(s);
            }
            return stores;
        }

        public ConcurrentDictionary<String,StoreOwner> loadAllStoreOwnerForStore(FilterDefinition<BsonDocument> storefilter)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> docs = this.DAO_StoreOwner.Documents.Find(filter).ToList();
            List<DTO_StoreOwner> storesDTOs = new List<DTO_StoreOwner>();
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                DTO_StoreOwner dto = JsonConvert.DeserializeObject<DTO_StoreOwner>(json);
                storesDTOs.Add(dto);
            }
            ConcurrentDictionary<String,StoreOwner> owners = new ConcurrentDictionary<String, StoreOwner>();
            foreach (DTO_StoreOwner dto in storesDTOs)
            {
                var f = Builders<BsonDocument>.Filter.Eq("UserId", dto.UserId);
                StoreOwner s = LoadStoreOwner(f);
                owners.TryAdd(dto.UserId, s);
            }
            return owners;
        }

        public ConcurrentDictionary<String, StoreManager> LoadAllManagersForStore(FilterDefinition<BsonDocument> storefilter)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> docs = this.DAO_StoreManager.Documents.Find(filter).ToList();
            List<DTO_StoreManager> storesDTOs = new List<DTO_StoreManager>();
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                DTO_StoreManager dto = JsonConvert.DeserializeObject<DTO_StoreManager>(json);
                storesDTOs.Add(dto);
            }
            ConcurrentDictionary<String, StoreManager> owners = new ConcurrentDictionary<String, StoreManager>();
            foreach (DTO_StoreManager dto in storesDTOs)
            {
                var f = Builders<BsonDocument>.Filter.Eq("UserId", dto.UserId);
                StoreManager s = LoadStoreManager(f);
                owners.TryAdd(dto.UserId, s);
            }
            return owners;
        }

        // reg user
        public void Create(RegisteredUser ru)
        {
            DAO_RegisteredUser.Create(new DTO_RegisteredUser(ru.Id, Get_DTO_ShoppingCart(ru), ru.UserName, ru._password, ru.Active, Get_DTO_History(ru.History), Get_DTO_Notifications(ru.PendingNotification)));
            RegisteredUsers.TryAdd(ru.Id, ru);
        }

        public RegisteredUser LoadRegisteredUser(FilterDefinition<BsonDocument> filter)
        {
            RegisteredUser ru;
            DTO_RegisteredUser dto = DAO_RegisteredUser.Load(filter);
            if (dto != null && RegisteredUsers.TryGetValue(dto._id, out ru))
            {
                return ru;
            }

            ru = new RegisteredUser(dto._id, dto.UserName, dto._password, dto.Active, ToObject(dto.History), ToObject(dto.PendingNotification));
            ru.ShoppingCart = ToObject(dto.ShoppingCart, ru);
            RegisteredUsers.TryAdd(ru.Id, ru);
            return ru;
        }

        public void UpdateRegisteredUser(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update, Boolean upsert = false)
        {
            DAO_RegisteredUser.Update(filter, update, upsert);
        }

        public void DeleteRegisteredUser(FilterDefinition<BsonDocument> filter)
        {
            DTO_RegisteredUser deletedRegisteredUser = DAO_RegisteredUser.Delete(filter);
            if (!(deletedRegisteredUser is null))
            {
                RegisteredUsers.TryRemove(deletedRegisteredUser._id, out RegisteredUser ru);
            }
        }

        public void Load_RegisteredUserHistory(RegisteredUser user)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
            DTO_RegisteredUser dto = DAO_RegisteredUser.Load(filter);
            user.History = ToObject(dto.History);
        }
        public void Load_RegisteredUserNotifications(RegisteredUser user)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
            DTO_RegisteredUser dto = DAO_RegisteredUser.Load(filter);
            user.PendingNotification = ToObject(dto.PendingNotification);
        }
        public void Load_RegisteredUserShoppingCart(RegisteredUser user)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
            DTO_RegisteredUser dto = DAO_RegisteredUser.Load(filter);
            user.ShoppingCart = ToObject(dto.ShoppingCart, user);
        }

        // store manager
        public void Create(StoreManager sm)
        {
            DAO_StoreManager.Create(new DTO_StoreManager(sm.GetId(), sm.Permission.functionsBitMask, sm.AppointedBy.GetId(), sm.Store.Id));
            LinkedList<StoreManager> list;
            if (StoreManagers.ContainsKey(sm.GetId()))
            {
                StoreManagers.TryGetValue(sm.GetId(), out list);
                list.AddLast(sm);
            }
            else
            {
                list = new LinkedList<StoreManager>();
                list.AddLast(sm);
                StoreManagers.TryAdd(sm.GetId(), list);
            }
        }

        public StoreManager LoadStoreManager(FilterDefinition<BsonDocument> filter)
        {
            StoreManager sm;
            LinkedList<StoreManager> list;
            DTO_StoreManager dto = DAO_StoreManager.Load(filter);       

            bool listExists = StoreManagers.TryGetValue(dto.UserId, out list);
            if (listExists)
            {
                foreach (StoreManager manager in list)
                {
                    if (manager.Store.Id == dto.UserId)
                    {
                        return manager;
                    }
                }

            }

            return null;
        }

        public void UpdateStoreManager(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_StoreManager.Update(filter, update);
        }

        public void DeleteStoreManager(FilterDefinition<BsonDocument> filter)
        {
            DTO_StoreManager deletedStoreManager = DAO_StoreManager.Delete(filter);
            StoreManager sm = null;
            LinkedList<StoreManager> list;
            if (!(deletedStoreManager is null))
            {
                if (StoreManagers.TryGetValue(deletedStoreManager.UserId, out list))
                {
                    foreach (StoreManager manager in list)
                    {
                        if (manager.Store.Id == deletedStoreManager.UserId)
                        {
                            sm = manager;
                        }
                    }
                }

                list.Remove(sm);
                if (list.Count == 0)
                {
                    StoreManagers.TryRemove(sm.GetId(), out _);
                }
            }
        }

        private void AddManagerToIdentityMap(StoreManager manager)
        {
            LinkedList<StoreManager> list;
            bool listExists = StoreManagers.TryGetValue(manager.GetId(), out list);
            if (listExists)
            {
                list.AddLast(manager);
            }
            else
            {
                list = new LinkedList<StoreManager>();
                list.AddLast(manager);
                StoreManagers.TryAdd(manager.GetId(), list);
            }
        }

        // store owner
        public void Create(StoreOwner so)
        {

            String appointedById = "0";
            if (so.AppointedBy != null)
            {
                appointedById = so.AppointedBy.GetId();
            }

            DAO_StoreOwner.Create(new DTO_StoreOwner(so.GetId(), so.StoreId, appointedById, Get_DTO_ManagerList(so.StoreManagers), Get_DTO_OwnerList(so.StoreOwners)));
            LinkedList<StoreOwner> list;
            if (StoreOwners.ContainsKey(so.GetId()))
            {
                StoreOwners.TryGetValue(so.GetId(), out list);
                list.AddLast(so);
            }
            else
            {
                list = new LinkedList<StoreOwner>();
                list.AddLast(so);
                StoreOwners.TryAdd(so.GetId(), list);
            }
        }

        public StoreOwner LoadStoreOwner(FilterDefinition<BsonDocument> filter)
        {
            StoreOwner so;
            LinkedList<StoreOwner> list;
            DTO_StoreOwner dto = DAO_StoreOwner.Load(filter);       

            bool listExists = StoreOwners.TryGetValue(dto.UserId, out list);
            if (listExists)
            {
                foreach (StoreOwner owner in list) { if (owner.StoreId == dto.StoreId) { return owner; } }

            }
            return null;
        }

        public StoreOwner getOwnershipTree(Store store, String founder_id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("UserId", founder_id) & Builders<BsonDocument>.Filter.Eq("StoreId", store.Id);
            DTO_StoreOwner founder_dto = DAO_StoreOwner.Load(filter);
            var filter2 = Builders<BsonDocument>.Filter.Eq("_id", founder_dto.UserId);
            StoreOwner founder = new StoreOwner(LoadRegisteredUser(filter2), store.Id, null);

            if (founder_dto.StoreOwners.Count > 0)
            {
                foreach (String owner_id in founder_dto.StoreOwners)
                {
                    StoreOwner storeowner = getOwnershipTree(store, owner_id);
                    storeowner.AppointedBy = founder;
                    founder.StoreOwners.AddLast(storeowner);
                }
            }
            if (founder_dto.StoreManagers.Count > 0)
            {
                foreach (String manager_id in founder_dto.StoreManagers)
                {
                    var manager_filter = Builders<BsonDocument>.Filter.Eq("UserId", manager_id) & Builders<BsonDocument>.Filter.Eq("StoreId", store.Id);
                    DTO_StoreManager manager_dto = DAO_StoreManager.Load(manager_filter);
                    var user_filter = Builders<BsonDocument>.Filter.Eq("_id", manager_id);
                    StoreManager manager = new StoreManager(LoadRegisteredUser(user_filter), store, new Permission(manager_dto.Permission), founder);
                    founder.StoreManagers.AddLast(manager);
                    store.Managers.TryAdd(manager_id, manager);
                    AddManagerToIdentityMap(manager);
                }
            }
            store.Owners.TryAdd(founder_id, founder);
            AddOwnerToIdentityMap(founder);

            return founder;
        }

        public void UpdateStoreOwner(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_StoreOwner.Update(filter, update);
        }

        public void DeleteStoreOwner(FilterDefinition<BsonDocument> filter)
        {
            DTO_StoreOwner deletedStoreOwner = DAO_StoreOwner.Delete(filter); 
            StoreOwner so = null;
            LinkedList<StoreOwner> list;
            if (!(deletedStoreOwner is null))
            {
                if (StoreOwners.TryGetValue(deletedStoreOwner.UserId, out list))
                {
                    foreach (StoreOwner owner in list)
                    {
                        if (owner.StoreId == deletedStoreOwner.StoreId) { so = owner; }
                    }
                }

                list.Remove(so);
                if (list.Count == 0)
                {
                    StoreOwners.TryRemove(so.GetId(), out _);
                }
            }
        }

        private void AddOwnerToIdentityMap(StoreOwner owner)
        {
            LinkedList<StoreOwner> list;
            bool listExists = StoreOwners.TryGetValue(owner.GetId(), out list);
            if (listExists)
            {
                list.AddLast(owner);
            }
            else
            {
                list = new LinkedList<StoreOwner>();
                list.AddLast(owner);
                StoreOwners.TryAdd(owner.GetId(), list);
            }
        }

        // system admin
        public void UpdateSystemAdmins(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update, Boolean upsert = false)
        {
            DAO_SystemAdmins.Update(filter, update, upsert);
        }

        // product

        public void Create(Product p)
        {
            DAO_Product.Create(new DTO_Product(p.Id, p.Name, p.Price, p.Quantity, p.Category, p.Rate, p.NumberOfRates, p.KeyWords));
            Products.TryAdd(p.Id, p);
        }

        public Product LoadProduct(FilterDefinition<BsonDocument> filter)
        {
            Product p;
            DTO_Product dto = DAO_Product.Load(filter);
            if (Products.TryGetValue(dto._id, out p))
            {
                return p;
            }

            p = new Product(dto._id, dto.Name, dto.Price, dto.Quantity, dto.Category, dto.KeyWords);
            Products.TryAdd(p.Id, p);
            return p;
        }

        public void UpdateProduct(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_Product.Update(filter, update);
        }

        public void DeleteProduct(FilterDefinition<BsonDocument> filter)
        {
            DTO_Product deletedProduct = DAO_Product.Delete(filter);
            if (!(deletedProduct is null))
            {
                Products.TryRemove(deletedProduct._id, out Product p);
            }
        }

        // stores
        public void Create(Store s)
        {
            LinkedList<String> owners = new LinkedList<String>();
            LinkedList<String> managers = new LinkedList<String>();
            LinkedList<String> inventory = new LinkedList<String>();

            foreach (var owner in s.Owners) { owners.AddLast(owner.Key); }
            foreach (var manager in s.Managers) { managers.AddLast(manager.Key); }
            foreach (var product in s.InventoryManager.Products) { inventory.AddLast(product.Key); }

            DAO_Store.Create(new DTO_Store(s.Id, s.Name, s.Founder.GetId(), owners, managers, inventory, Get_DTO_History(s.History),
                                            s.Rate, s.NumberOfRates, s.Active));
            Stores.TryAdd(s.Id, s);
        }

        public Store LoadStore(FilterDefinition<BsonDocument> filter)
        {
            Store s;
            DTO_Store dto = DAO_Store.Load(filter);
            if (Stores.TryGetValue(dto._id, out s)) { return s; }

            ConcurrentDictionary<String, Product> products = new ConcurrentDictionary<String, Product>();
            NotificationPublisher notificationManager = new NotificationPublisher();

            foreach (String product in dto.InventoryManager)
            {
                var filter3 = Builders<BsonDocument>.Filter.Eq("_id", product);
                Product p = LoadProduct(filter3);
                p.NotificationPublisher = notificationManager;
                products.TryAdd(product, p);
            }

            s = new Store(dto._id, dto.Name, new InventoryManager(products), ToObject(dto.History), dto.Rate, dto.NumberOfRates, notificationManager, dto.Active);

            Stores.TryAdd(s.Id, s);

            StoreOwner founder = getOwnershipTree(s, dto.Founder);

            s.Founder = founder;


            // loading staff
            //var filterstaff = Builders<BsonDocument>.Filter.Eq("StoreId",s.Id);
            //s.Managers = LoadAllManagersForStore(filterstaff);
            //s.Owners = loadAllStoreOwnerForStore(filterstaff);
            
            return s;
        }

        public void UpdateStore(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_Store.Update(filter, update);
        }

        public void DeleteStore(FilterDefinition<BsonDocument> filter)
        {
            DTO_Store deletedStore = DAO_Store.Delete(filter);
            if (!(deletedStore is null))
            {
                Stores.TryRemove(deletedStore._id, out Store s);
            }
        }

        public void Load_StoreHistory(Store store)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
            DTO_Store dto = DAO_Store.Load(filter);
            store.History = ToObject(dto.History);
        }

        public void clearDB()
        {
            if (!(Instance is null))
            {
                var emptyFilter = Builders<BsonDocument>.Filter.Empty;
                db.GetCollection<BsonDocument>("Products").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("Stores").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("Users").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("StoreStaffs").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("SystemAdmins").DeleteMany(emptyFilter);

                RegisteredUsers.Clear();
                GuestUsers.Clear();
                Products.Clear();
                StoreManagers.Clear();
                StoreOwners.Clear();
                Stores.Clear();

            }

        }


    }
}
