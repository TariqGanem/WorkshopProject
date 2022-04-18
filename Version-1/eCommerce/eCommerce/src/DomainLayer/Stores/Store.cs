using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using eCommerce.src.ServiceLayer.Response;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.DomainLayer.User;

namespace eCommerce.src.DomainLayer.Store
{
    public class Store
    {
        public String Id { get; }
        public String Name { get; }
        public Boolean IsOpen { get; }
        public StoreOwner Founder { get; }
        public InventoryManager InventoryManager { get; }
        public StoreHistory History { get; set; }
        public ConcurrentDictionary<String,StoreOwner> Owners { get; }
        public ConcurrentDictionary<String,StoreManager> Managers { get; }

        public Store(RegisteredUser founder, String name)
        {
            Owners = new ConcurrentDictionary<string, StoreOwner>();
            Managers = new ConcurrentDictionary<string, StoreManager>();
            IsOpen = true;
            Founder = new StoreOwner();
            Name = name;
            Owners.TryAdd(founder.UserName, Founder);
            History = new StoreHistory();
        }

        public List<Product> SearchProduct(IDictionary<String, Object> searchAttributes)
        {
            return InventoryManager.SearchProduct(searchAttributes);
        }

        public Product AddNewProduct(String userID, String productName, Double price, int initialQuantity, String category, LinkedList<String> keyWords = null)
        {
            return null;
        }

        public Product RemoveProduct(String userID, String productID)
        {
            return null;
        }

        public Product EditProduct(String userID, String productID, IDictionary<String, Object> details)
        {
            return null;
        }

        public Boolean AddStoreOwner(RegisteredUser futureOwner, string currentlyOwnerID)
        {
            return false;
        }

        public Boolean AddStoreManager(RegisteredUser futureManager, string currentlyOwnerID)
        {
            return false;
        }

        public bool RemoveStoreManager(String removedManagerID, string currentlyOwnerID)
        {
            return false;
        }

        public bool SetPermissions(string managerID, string ownerID, LinkedList<int> permissions)
        {
            return false;
        }

        public StoreHistory GetStorePurchaseHistory(string userID, bool sysAdmin)
        {
            return null;
        }

        public bool RemovePermissions(string managerID, string ownerID, LinkedList<int> permissions)
        {
            return false;
        }

        public Product GetProduct(String productID)
        {
            return InventoryManager.GetProduct(productID);
        }

        // Complete functionality for Store




        private Boolean CheckIfStoreOwner(String userID)
        {
            return Owners.ContainsKey(userID);
        }

        private Boolean CheckIfStoreManager(String userID)
        {
            return Managers.ContainsKey(userID);
        }
    }
}
