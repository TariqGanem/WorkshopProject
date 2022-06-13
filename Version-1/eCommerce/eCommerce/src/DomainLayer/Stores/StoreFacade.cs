using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountComposition;
using eCommerce.src.DomainLayer.Stores.Policies.Offer;
using eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.ServiceLayer.ResultService;
using MongoDB.Bson;
using MongoDB.Driver;

namespace eCommerce.src.DomainLayer.Store
{
    public interface IStoresFacade
    {
        Store OpenNewStore(RegisteredUser founder, String storeName);
        String AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        void RemoveProductFromStore(String userID, String storeID, String productID);
        void EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details);
        List<Product> SearchProduct(IDictionary<String, Object> productDetails);
        void AddStoreOwner(RegisteredUser futureOwner, String currentlyOwnerID, String storeID);
        void AddStoreManager(RegisteredUser futureManager, String currentlyOwnerID, String storeID);
        void RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID);
        void RemoveStoreOwner(String removedOwnerID, string currentlyOwnerID, String storeID);
        void SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        void RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Dictionary<IStaff, Permission> GetStoreStaff(string ownerID, string storeID);
        History GetStorePurchaseHistory(string userID, string storeID, bool sysAdmin);
        void CloseStore(RegisteredUser founder, string storeID);
        public List<Store> SearchStore(IDictionary<string, object> details);

    }
    public class StoreFacade : IStoresFacade
    {
        public ConcurrentDictionary<String, Store> Stores { get; }
        public DBUtil dbutil;

        public StoreFacade()
        {
            Stores = new ConcurrentDictionary<String, Store>();
            dbutil = DBUtil.getInstance();
            loadstore();
        }
        public List<Store> SearchStore(IDictionary<string, object> details)
        {
            List<Store> searchResult = new List<Store>();
            foreach (Store store in Stores.Values)
            {
                if (checkStoreAttributes(store, details))
                {
                    searchResult.Add(store);
                }
            }
            if (searchResult.Count > 0)
            {
                return searchResult;
            }
            else
            {
                throw new Exception($"No stores have been found\n");
            }
        }

        private bool checkStoreAttributes(Store store, IDictionary<string, object> searchAttributes)
        {
            Boolean result = true;
            ICollection<String> properties = searchAttributes.Keys;
            IDictionary<String, Object> lowerCaseDict = searchAttributes.ToDictionary(k => k.Key.ToLower(), k => k.Value);

            foreach (string property in properties)
            {
                JsonElement jsonElement = (JsonElement)lowerCaseDict[property.ToLower()];
                Object value = null;

                switch (property.ToLower())
                {
                    case "name":
                        value = jsonElement.GetString().ToLower();
                        if (!store.Name.ToLower().Contains((String)value)) { result = false; }
                        break;
                    case "rating":
                        value = jsonElement.GetDouble();
                        if (store.Rate < (Double)value) { result = false; }
                        break;
                }
            }
            return result;
        }

        public String AddProductToStore(String userID, String storeID, String productName, Double price, int initialQuantity, String category, LinkedList<String> keywords = null)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                String product_id = store.AddNewProduct(userID, productName, price, initialQuantity, category, keywords);
                Product prod = GetProduct(storeID, product_id);
                //db
                dbutil.Create(prod);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("InventoryManager", store.getDTO().InventoryManager);
                dbutil.UpdateStore(filter, update);
                //
                return product_id;
            }
            else
            {
                throw new Exception($"Store ID {storeID} not found");
            }
        }

        public void RemoveProductFromStore(string userID, string storeID, string productID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                store.RemoveProduct(userID, productID);
                // db
                dbutil.DeleteProduct(Builders<BsonDocument>.Filter.Eq("_id", productID));
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("InventoryManager", store.getDTO().InventoryManager);
                dbutil.UpdateStore(filter, update);
                // -- 
            }
            else
            {
                throw new Exception($"Store ID {storeID} not found");
            }
        }

        public void EditProductDetails(string userID, string storeID, string productID, IDictionary<String, Object> details)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                store.EditProduct(userID, productID, details);
                Product prod = GetProduct(storeID, productID);
                DTO_Product p_dto = prod.getDTO();
                var filter = Builders<BsonDocument>.Filter.Eq("_id", p_dto._id);
                var update = Builders<BsonDocument>.Update.Set("Name", p_dto.Name)
                                                          .Set("Price", p_dto.Price)
                                                          .Set("Quantity", p_dto.Quantity)
                                                          .Set("Category", p_dto.Category)
                                                          .Set("Rating", p_dto.Rate)
                                                          .Set("NumberOfRates", p_dto.NumberOfRates)
                                                          .Set("Keywords", p_dto.KeyWords);
                dbutil.UpdateProduct(filter, update);
            }
            else
            {
                throw new Exception($"Store ID {storeID} not found");
            }
        }

        public void AddStoreOwner(RegisteredUser futureOwner, string currentlyOwnerID, string storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                store.AddStoreOwner(futureOwner, currentlyOwnerID);
                // db
                store.Owners.TryGetValue(futureOwner.Id, out StoreOwner owner);
                dbutil.Create(owner);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("Owners", store.getDTO().Owners);
                dbutil.UpdateStore(filter, update);
                //
                if (store.Owners.TryGetValue(currentlyOwnerID, out StoreOwner own))
                {
                    own.StoreOwners.AddLast(owner);
                    var filterowner = Builders<BsonDocument>.Filter.Eq("UserId", own.User.Id) & Builders<BsonDocument>.Filter.Eq("StoreId", store.Id);
                    var updateowner = Builders<BsonDocument>.Update.Set("StoreOwners", own.getDTO().StoreOwners);
                    dbutil.UpdateStoreOwner(filterowner, updateowner);
                }
            }
            else
            {
                throw new Exception($"Store ID {storeID} not found");
            }
        }

        public void AddStoreManager(RegisteredUser futureManager, string currentlyOwnerID, string storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                store.AddStoreManager(futureManager, currentlyOwnerID);
                // db
                store.Managers.TryGetValue(futureManager.Id, out StoreManager manager);
                dbutil.Create(manager);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("Managers", store.getDTO().Managers);
                dbutil.UpdateStore(filter, update);
                //
                if (store.Owners.TryGetValue(currentlyOwnerID, out StoreOwner owner))
                {
                    owner.StoreManagers.AddLast(manager);
                    //update owner record
                    var filterowner = Builders<BsonDocument>.Filter.Eq("UserId", owner.User.Id) & Builders<BsonDocument>.Filter.Eq("StoreId", store.Id);
                    var updateowner = Builders<BsonDocument>.Update.Set("StoreManagers", owner.getDTO().StoreManagers);
                    dbutil.UpdateStoreOwner(filterowner, updateowner);
                }
            }
            else
            {
                throw new Exception($"Store ID {storeID} not found");
            }
        }

        public void RemoveStoreManager(String removedManagerID, string currentlyOwnerID, string storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                if (store.Owners.TryGetValue(currentlyOwnerID, out StoreOwner owner))
                {
                    var filterowner = Builders<BsonDocument>.Filter.Eq("UserId", owner.User.Id) & Builders<BsonDocument>.Filter.Eq("StoreId", store.Id);
                    var updateowner = Builders<BsonDocument>.Update.Set("StoreManagers", owner.getDTO().StoreManagers);
                    dbutil.UpdateStoreOwner(filterowner, updateowner);
                }
                store.RemoveStoreManager(removedManagerID, currentlyOwnerID);
                // db
                var filter_manager = Builders<BsonDocument>.Filter.Eq("UserId", removedManagerID) & Builders<BsonDocument>.Filter.Eq("StoreId", store.Id);
                dbutil.DeleteStoreManager(filter_manager);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("Managers", store.getDTO().Managers);
                dbutil.UpdateStore(filter, update);
                //
            }
            else
            {
                throw new Exception($"Store ID {storeID} not found");
            }
        }

        public void RemoveStoreOwner(string removedOwnerID, string currentlyOwnerID, string storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))     // Check if storeID exists
            {
                if (store.Owners.TryGetValue(currentlyOwnerID, out StoreOwner owner))
                {
                    var filterowner = Builders<BsonDocument>.Filter.Eq("UserId", owner.User.Id) & Builders<BsonDocument>.Filter.Eq("StoreId", store.Id);
                    var updateowner = Builders<BsonDocument>.Update.Set("StoreOwners", owner.getDTO().StoreOwners);
                    dbutil.UpdateStoreOwner(filterowner, updateowner);
                }
                store.RemoveStoreOwner(removedOwnerID, currentlyOwnerID);
                var filter_owner = Builders<BsonDocument>.Filter.Eq("Userid", removedOwnerID) & Builders<BsonDocument>.Filter.Eq("Storeid", store.Id);
                dbutil.DeleteStoreOwner(filter_owner);
                // Update Store in DB
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("Owners", store.getDTO().Owners);
                dbutil.UpdateStore(filter, update);
            }
            else
            {
                throw new Exception($"Store ID {storeID} not found.");
            }
        }

        public List<Product> SearchProduct(IDictionary<String, Object> searchAttributes)
        {
            List<Product> searchResult = new List<Product>();
            foreach (Store store in Stores.Values)
            {
                if (store.Active)
                {
                    List<Product> storeResult = store.SearchProduct(searchAttributes);
                    searchResult.AddRange(storeResult);
                }
            }
            if (searchResult.Count > 0)
            {
                return searchResult;
            }
            else
            {
                throw new Exception($"No product has been found");
            }
        }

        public Dictionary<IStaff, Permission> GetStoreStaff(string userID, string storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                if (store.Active || store.Owners.TryGetValue(userID, out _))
                {
                    return store.GetStoreStaff(userID);
                }
                else
                {
                    throw new Exception($"The store closed or the given user Id {userID} is not an owner");
                }
            }
            else
            {
                throw new Exception("The given store ID does not exists");
            }
        }

        public History GetStorePurchaseHistory(string userID, string storeID, bool sysAdmin)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                if (store.Active || store.Owners.TryGetValue(userID, out _))
                {
                    return store.GetStorePurchaseHistory(userID, sysAdmin);
                }
                else
                {
                    throw new Exception($"The store closed or the given user Id {userID} is not an owner");
                }
            }
            else
            {
                throw new Exception("The given store ID does not exists");
            }
        }

        public Store OpenNewStore(RegisteredUser founder, string storeName)
        {
            Store newStore = new Store(storeName, founder);
            Stores.TryAdd(newStore.Id, newStore);
            dbutil.Create(newStore);
            dbutil.Create(newStore.Founder);
            NotificationPublisher NotificationPublisher = new NotificationPublisher(newStore);
            newStore.NotificationPublisher = NotificationPublisher;
            newStore.NotificationPublisher.notifyStoreOpened();
            return newStore;
        }

        public void CloseStore(RegisteredUser founder, string storeID)
        {
            Store currStore = GetStore(storeID);
            if (!founder.Id.Equals(currStore.Founder.GetId()))
            {
                throw new Exception($"Non-founder Trying to close store {currStore.Name}");
            }
            if (currStore.Active == false)
                throw new Exception($"Store {storeID} is already closed ! \n");
            currStore.Active = false;
            currStore.NotificationPublisher.notifyStoreClosed();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", storeID);
            var update = Builders<BsonDocument>.Update.Set("Active", false);
            dbutil.UpdateStore(filter, update);
        }


        public void SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                store.SetPermissions(managerID, ownerID, permissions);
                // db
                store.Managers.TryGetValue(managerID, out StoreManager manager);
                DTO_StoreManager manager_dto = manager.getDTO();
                var filter = Builders<BsonDocument>.Filter.Eq("UserId", manager_dto.UserId) & Builders<BsonDocument>.Filter.Eq("StoreId", manager_dto.StoreId); 
                var update = Builders<BsonDocument>.Update.Set("Permission", manager_dto.Permission);
                dbutil.UpdateStoreManager(filter, update);
                //
            }
            else
            {
                throw new Exception($"No has been found");
            }
        }

        public bool SendOfferToStore(Offer offer)
        {
            if (Stores.TryGetValue(offer.StoreID, out Store store))
            {
                return store.SendOfferToStore(offer);
            }
            throw new Exception("Failed to add an offer: Failed to locate the store\n");
        }

        public void RemovePermissions(string storeID, string managerID, string ownerID, LinkedList<int> permissions)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                store.RemovePermissions(managerID, ownerID, permissions);
                //db
                store.Managers.TryGetValue(managerID, out StoreManager manager);
                DTO_StoreManager manager_dto = manager.getDTO();
                var filter = Builders<BsonDocument>.Filter.Eq("UserId", manager_dto.UserId) & Builders<BsonDocument>.Filter.Eq("StoreId", manager_dto.StoreId); ;
                var update = Builders<BsonDocument>.Update.Set("Permission", manager_dto.Permission);
                dbutil.UpdateStoreManager(filter, update);
                //
            }
            else
            {
                throw new Exception($"No has been found");
            }
        }

        public Store ReOpenStore(RegisteredUser owner, string storeid)
        {
            Stores.TryGetValue(storeid, out Store store);
            if (!store.Active)
            {
                if (store.Owners.ContainsKey(owner.Id))
                {
                    store.Active = true;
                    store.NotificationPublisher.notifyStoreOpened();

                    // Update Store in DB
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                    var update = Builders<BsonDocument>.Update.Set("isClosed", false);
                    dbutil.UpdateStore(filter, update);
                    Logger.GetInstance().LogInfo($"The store {store.Name} is reopened\n");
                    return store;
                }
                throw new Exception($"Registered user (Id:{owner.Id}) is not one of the store owners , therefore can not reopen the store\n");
            }
            //else faild
            throw new Exception("Store is not closed or does not exists, therefore can not reopen it\n");
        }

        public OfferResponse SendOfferResponseToUser(string storeID, string ownerID, string offerID, bool accepted, double counterOffer)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store.SendOfferResponseToUser(ownerID, offerID, accepted, counterOffer);
            }
            throw new Exception("Failed to response to an offer: Failed to locate the store\n");
        }

        public Store GetStore(String storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store;
            }
            else
            {
                throw new Exception("Store does not exists");
            }
        }
        public Product GetProduct(String storeID, String productID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store.GetProduct(productID);
            }
            else
            {
                throw new Exception("Store does not exists");
            }
        }

        public List<Dictionary<string, object>> getStoreOffers(string storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                //mapper.Load_StoreOfferManager(store);
                return store.getStoreOffers();
            }
            throw new Exception("Failed to get store offers: Failed to locate the store\n");
        }

        public void resetSystem()
        {
            Stores.Clear();

        }

        public void loadstore()
        {
            List<Store> storesList = dbutil.LoadAllStores();
            foreach (Store store in storesList)
            {
                Stores.TryAdd(store.Id, store);
            }
        }

        public bool AddDiscountPolicy(string storeId, Dictionary<string, object> info)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                IDiscountPolicy res = store.AddDiscountPolicy(info);
                // DB
                dbutil.Create(res);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainDiscount", store.PolicyManager.MainDiscount.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainDiscount);
                return true;
            }
            throw new Exception("Store does not exists\n");
        }

        private void UpdatePolicyRoot(DiscountAddition discountRoot)
        {
            dbutil.DAO_DiscountAddition.Delete(Builders<BsonDocument>.Filter.Eq("_id", discountRoot.Id));
            dbutil.Create(discountRoot);
        }

        private void UpdatePolicyRoot(BuyNow purchaseRoot)
        {
            dbutil.DAO_BuyNow.Delete(Builders<BsonDocument>.Filter.Eq("_id", purchaseRoot.Id));
            dbutil.Create(purchaseRoot);
            dbutil.Create(purchaseRoot.Policy);
        }

        public bool AddDiscountPolicy(string storeId, Dictionary<string, object> info, string id)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                IDiscountPolicy res = store.AddDiscountPolicy(info, id);
                // DB
                dbutil.Create(res);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainDiscount", store.PolicyManager.MainDiscount.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainDiscount);

                return true;
               
            }
            throw new Exception("Store does not exists\n");
        }

        public bool RemoveDiscountPolicy(string storeId, string id)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                //mapper.Load_StorePolicyManager(store);
                IDiscountPolicy res = store.RemoveDiscountPolicy(id);
                // Update in DB
                dbutil.Delete(res);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainDiscount", store.PolicyManager.MainDiscount.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainDiscount);

            }
            throw new Exception("Store does not exists\n");
        }

        public bool EditDiscountCondition(string storeId, Dictionary<string, object> info, string id)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                bool res = store.EditDiscountCondition(info, id);
                // Update in DB
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainDiscount", store.PolicyManager.MainDiscount.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainDiscount);
                return res;
            }
            throw new Exception("Store does not exists\n");
        }

        public bool AddPurchasePolicy(string storeId, Dictionary<string, object> info)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                IPurchasePolicy res = store.AddPurchasePolicy(info);
                // Update in DB
                dbutil.Create(res);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainPolicy", store.PolicyManager.MainPolicy.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainPolicy);

                return true;
            }
            throw new Exception("Store does not exists\n");
        }

        public bool EditPurchasePolicy(string storeId, Dictionary<string, object> info, string id)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                bool res = store.EditPurchasePolicy(info, id);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainPolicy", store.PolicyManager.MainPolicy.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainPolicy);
                return res;
            }
            throw new Exception("Store does not exists\n");
        }

        public bool RemovePurchasePolicy(string storeId, string id)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                IPurchasePolicy res = store.RemovePurchasePolicy(id);

                // Update in DB
                dbutil.Delete(res);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainPolicy", store.PolicyManager.MainPolicy.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainPolicy);
            }
            throw new Exception("Store does not exists\n");
        }

        public bool AddPurchasePolicy(string storeId, Dictionary<string, object> info, string id)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                IPurchasePolicy res = store.AddPurchasePolicy(info, id);
                // Update in DB
                dbutil.Create(res);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainPolicy", store.PolicyManager.MainPolicy.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainPolicy);
                return true;
            }
            throw new Exception("Store does not exists\n");
        }

        public IDictionary<string, object> GetPurchasePolicyData(string storeId)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                return store.GetPurchasePolicyData();
            }
            throw new Exception("Store does not exists\n");
        }

        public IDictionary<string, object> GetDiscountPolicyData(string storeId)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                return store.GetPoliciesData();
            }
            throw new Exception("Store does not exists\n");
        }

        public bool EditDiscountPolicy(string storeId, Dictionary<string, object> info, string id)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                bool res = store.EditDiscountPolicy(info, id);
                    // Update in DB
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainDiscount", store.PolicyManager.MainDiscount.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainDiscount);
                return res;
            }
            throw new Exception("Store does not exists\n");
        }

        public bool RemoveDiscountCondition(string storeId, string id)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                IDiscountCondition res = store.RemoveDiscountCondition(id);
                // Update in DB
                dbutil.Delete(res);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainDiscount", store.PolicyManager.MainDiscount.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainDiscount);

                
            }
            throw new Exception("Store does not exists\n");
        }

        public bool AddDiscountCondition(string storeId, Dictionary<string, object> info, string id)
        {
            if (Stores.TryGetValue(storeId, out Store store))
            {
                //mapper.Load_StorePolicyManager(store);
                IDiscountCondition res = store.AddDiscountCondition(info, id);
                // Update in DB
                dbutil.Create(res);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
                var update = Builders<BsonDocument>.Update.Set("MainDiscount", store.PolicyManager.MainDiscount.getDTO());
                dbutil.UpdateStore(filter, update);
                UpdatePolicyRoot(store.PolicyManager.MainDiscount);

                return true;

            }
            throw new Exception("Store does not exists\n");
        }
    }
}
