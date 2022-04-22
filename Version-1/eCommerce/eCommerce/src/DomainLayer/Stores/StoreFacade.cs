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
        void AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        void RemoveProductFromStore(String userID, String storeID, String productID);
        void EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details);
        List<Product> SearchProduct(IDictionary<String, Object> productDetails);
        void AddStoreOwner(RegisteredUser futureOwner, String currentlyOwnerID, String storeID);
        void AddStoreManager(RegisteredUser futureManager, String currentlyOwnerID, String storeID);
        void RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID);
        void SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        void RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Dictionary<IStaff, Permission> GetStoreStaff(string ownerID, string storeID);
        History GetStorePurchaseHistory(string userID, string storeID, bool sysAdmin);
        void CloseStore(RegisteredUser founder, string storeID);
    }
    public class StoreFacade : IStoresFacade
    {
        public ConcurrentDictionary<String, Store> Stores { get; }

        public StoreFacade()
        {
            Stores = new ConcurrentDictionary<String, Store>();
        }

        public void AddProductToStore(String userID, String storeID, String productName, Double price, int initialQuantity, String category, LinkedList<String> keywords = null)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                store.AddNewProduct(userID, productName, price, initialQuantity, category, keywords);
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
                store.RemoveStoreManager(removedManagerID, currentlyOwnerID);
            }
            else
            {
                throw new Exception($"Store ID {storeID} not found");
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
            return newStore;
        }

        public void CloseStore(RegisteredUser founder, string storeID)
        {
            Store currStore = GetStore(storeID);
            if (!founder.Id.Equals(currStore.Founder.GetId()))
            {
                throw new Exception($"Non-founder Trying to close store {currStore.Name}");
            }
            currStore.Active = false;
        }


        public void SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                store.SetPermissions(managerID, ownerID, permissions);
            }
            else
            {
                throw new Exception($"No has been found");
            }
        }

        public void RemovePermissions(string storeID, string managerID, string ownerID, LinkedList<int> permissions)
        {
            if (Stores.TryGetValue(storeID, out Store store))
            {
                store.RemovePermissions(managerID, ownerID, permissions);
            }
            else
            {
                throw new Exception($"No has been found");
            }
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
    }
}
