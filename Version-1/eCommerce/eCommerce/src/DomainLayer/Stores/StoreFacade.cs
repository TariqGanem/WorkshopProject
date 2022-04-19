using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.User.Roles;

namespace eCommerce.src.DomainLayer.Store
{
    public class StoreFacade
    {
        #region parameters
        public ConcurrentDictionary<String, Store> Stores { get; }
        #endregion

        #region cosntructors
        public StoreFacade()
        {
            Stores = new ConcurrentDictionary<String, Store>();
        }
        #endregion

        #region methods
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
                Product res = store.RemoveProduct(userID, productID);
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
                List<Product> storeResult = store.SearchProduct(searchAttributes);
                searchResult.AddRange(storeResult);
            }
            if (searchResult.Count > 0)
            {
                return searchResult;
            }
            else
            {
                throw new Exception($"No has been found");
            }

        }

        public Dictionary<IStaff, Permission> GetStoreStaff(string userID, string storeID)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                return store.GetStoreStaff(userID);
            }
            throw new Exception("The given store ID does not exists");

        }

        public StoreHistory GetStorePurchaseHistory(string userID, string storeID, bool sysAdmin)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
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
        #endregion
    }
}
