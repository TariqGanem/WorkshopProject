using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.User.Roles;

namespace eCommerce.src.DomainLayer.Store
{
    public interface IStoresFacade
    {
        Store OpenNewStore(RegisteredUser founder, String storeName);
        Product AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        Boolean RemoveProductFromStore(String userID, String storeID, String productID);
        Product EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details);
        List<Product> SearchProduct(IDictionary<String, Object> productDetails);
        Boolean AddStoreOwner(RegisteredUser futureOwner, String currentlyOwnerID, String storeID);
        Boolean AddStoreManager(RegisteredUser futureManager, String currentlyOwnerID, String storeID);
        Boolean RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID);
        Boolean SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Boolean RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Dictionary<IStaff, Permission> GetStoreStaff(string ownerID, string storeID);
        History GetStorePurchaseHistory(string userID, string storeID, bool sysAdmin);
        Store CloseStore(RegisteredUser founder, string storeID);
    }
    public class StoreFacade : IStoresFacade
    {
        public ConcurrentDictionary<String, Store> Stores { get; }

        public StoreFacade()
        {
            Stores = new ConcurrentDictionary<String, Store>();
        }

        public Product AddProductToStore(String userID, String storeID, String productName, Double price, int initialQuantity, String category, LinkedList<String> keywords = null)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store.AddNewProduct(userID, productName, price, initialQuantity, category, keywords);
            }
            throw new Exception($"Store ID {storeID} not found");

        }

        public Boolean RemoveProductFromStore(string userID, string storeID, string productID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                store.RemoveProduct(userID, productID);
                return true;
            }
            throw new Exception($"Store ID {storeID} not found");
        }

        public Product EditProductDetails(string userID, string storeID, string productID, IDictionary<String, Object> details)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store.EditProduct(userID, productID, details);
            }
            throw new Exception($"Store ID {storeID} not found");
        }

        public Boolean AddStoreOwner(RegisteredUser futureOwner, string currentlyOwnerID, string storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store.AddStoreOwner(futureOwner, currentlyOwnerID);
            }
            throw new Exception($"Store ID {storeID} not found");
        }

        public Boolean AddStoreManager(RegisteredUser futureManager, string currentlyOwnerID, string storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store.AddStoreManager(futureManager, currentlyOwnerID);
            }
            throw new Exception($"Store ID {storeID} not found");
        }

        public Boolean RemoveStoreManager(String removedManagerID, string currentlyOwnerID, string storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store.RemoveStoreManager(removedManagerID, currentlyOwnerID);
            }
            throw new Exception($"Store ID {storeID} not found");
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
                    return store.GetStoreStaff(userID);
            }
            throw new Exception("The given store ID does not exists");

        }

        public History GetStorePurchaseHistory(string userID, string storeID, bool sysAdmin)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                if (store.Active || store.Owners.TryGetValue(userID, out _))
                    return store.GetStorePurchaseHistory(userID, sysAdmin);
            }
            throw new Exception("The given store ID does not exists");
        }

        public Store OpenNewStore(RegisteredUser founder, string storeName)
        {
            Store newStore = new Store(storeName, founder);
            Stores.TryAdd(newStore.Id, newStore);
            return newStore;
        }

        public Store CloseStore(RegisteredUser founder, string storeID)
        {
            Store currStore = GetStore(storeID);
            if (!founder.Id.Equals(currStore.Founder.GetId()))
            {
                throw new Exception($"Non-founder Trying to close store {currStore.Name}");
            }
            currStore.Active = false;
            return currStore;
        }


        public bool SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store.SetPermissions(managerID, ownerID, permissions);
            }
            throw new Exception($"No has been found");
        }

        public bool RemovePermissions(string storeID, string managerID, string ownerID, LinkedList<int> permissions)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store.RemovePermissions(managerID, ownerID, permissions);
            }
            throw new Exception($"No has been found");
        }

        public Store GetStore(String storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store;
            }
            throw new Exception("Store does not exists");
        }
    }
}
