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
        public Double Rate { get; private set; }
        public int NumberOfRates { get; private set; }
        public ConcurrentDictionary<String,StoreOwner> Owners { get; }
        public ConcurrentDictionary<String,StoreManager> Managers { get; }

        public Store(String name, RegisteredUser founder)
        {
            Name = name;
            IsOpen = true;
            Founder = new StoreOwner(founder, Id, null);
            Owners = new ConcurrentDictionary<string, StoreOwner>();
            Owners.TryAdd(founder.UserName, Founder);
            Managers = new ConcurrentDictionary<string, StoreManager>();
            InventoryManager = new InventoryManager();
            History = new StoreHistory();
        }

        public Double AddRating(Double rate)
        {
            if (rate < 1 || rate > 5)
            {
                throw new Exception($"Store {Name} could not be rated. Please use number between 1 to 5");
            }
            else
            {
                NumberOfRates += 1;
                Rate = (Rate + rate) / NumberOfRates;
                return Rate;
            }
        }

        public List<Product> SearchProduct(IDictionary<String, Object> searchAttributes)
        {
            return InventoryManager.SearchProduct(Rate, searchAttributes);
        }

        public Product AddNewProduct(String userID, String productName, Double price, int initialQuantity, String category, LinkedList<String> keyWords = null)
        {
            if (CheckIfStoreOwner(userID) || CheckStoreManagerAndPermissions(userID, Methods.AddNewProduct))
            {
                return InventoryManager.AddNewProduct(productName, price, initialQuantity, category, keyWords);
            }
            else
            {
                throw new Exception($"{userID} does not have permissions to add new product to {this.Name}");
            }
        }

        public Product RemoveProduct(String userID, String productID)
        {
            if (CheckIfStoreOwner(userID) || CheckStoreManagerAndPermissions(userID, Methods.RemoveProduct))
            {
                return InventoryManager.RemoveProduct(productID);
            }
            else
            {
                throw new Exception($"{userID} does not have permissions to remove products from {this.Name}");
            }
        }

        public Product EditProduct(String userID, String productID, IDictionary<String, Object> details)
        {
            if (CheckIfStoreOwner(userID) || CheckStoreManagerAndPermissions(userID, Methods.EditProduct))
            {
                return InventoryManager.EditProduct(productID, details);
            }
            else
            {
                throw new Exception($"{userID} does not have permissions to edit products' information in {this.Name}");
            }
        }

        public Boolean AddStoreOwner(RegisteredUser futureOwner, string currentlyOwnerID)
        {
            if (!CheckIfStoreOwner(futureOwner.Id) && Owners.TryGetValue(currentlyOwnerID, out StoreOwner owner))
            {
                StoreOwner newOwner = new StoreOwner(futureOwner, Id, owner);
                Owners.TryAdd(futureOwner.Id, newOwner);

                if (CheckIfStoreManager(futureOwner.Id))
                {
                    Managers.TryRemove(futureOwner.Id, out _);
                }
            }
            throw new Exception($"Failed to add store owner: Appointing owner (Email: {currentlyOwnerID}) " +
                $"is not an owner at ${this.Name}");
        }

        public Boolean AddStoreManager(RegisteredUser futureManager, string currentlyOwnerID)
        {
            if (!CheckIfStoreManager(futureManager.Id) && !CheckIfStoreOwner(futureManager.Id)
                    && Owners.TryGetValue(currentlyOwnerID, out StoreOwner owner))
            {
                StoreManager newManager = new StoreManager(futureManager, this, new Permission(), owner);
                Managers.TryAdd(futureManager.Id, newManager);
            }
            throw new Exception($"Failed to add store owner: Appointing owner (Email: {currentlyOwnerID}) " +
                $"is not an owner at ${this.Name}");
        }

        public bool RemoveStoreManager(String removedManagerID, string currentlyOwnerID)
        {
            if (Owners.TryGetValue(currentlyOwnerID, out StoreOwner owner) && Managers.TryGetValue(removedManagerID, out StoreManager manager))
            {
                if (manager.AppointedBy.Equals(owner))
                {
                    Managers.TryRemove(removedManagerID, out _);
                    return true;
                }
                throw new Exception($"Failed to remove user (Email: {removedManagerID}) from store management: Unauthorized owner (Email: {currentlyOwnerID})");
            }
            throw new Exception($"Failed to remove user (Email: {removedManagerID}) from store management: Either not a manager or owner not found");
        }

        public bool SetPermissions(string managerID, string ownerID, LinkedList<int> permissions)
        {
            if ((CheckIfStoreOwner(ownerID) || CheckStoreManagerAndPermissions(ownerID, Methods.SetPermissions)) && Managers.TryGetValue(managerID, out StoreManager manager))
            {
                if (CheckAppointedBy(manager, ownerID))
                {
                    foreach (int per in permissions)
                    {
                        manager.SetPermission(per, true);
                    }
                    return true;
                }
                throw new Exception($"Can't set permissions: Manager (ID: {managerID}) was not appointed by given staff member (ID: {ownerID})");
            }
            throw new Exception($"Staff ID not found in store.");
        }

        public Dictionary<IStaff, Permission> GetStoreStaff(string userID)
        {
            Dictionary<IStaff, Permission> storeStaff = new Dictionary<IStaff, Permission>();
            Permission ownerPermission = new Permission();
            ownerPermission.SetAllMethodesPermitted();

            if (CheckStoreManagerAndPermissions(userID, Methods.GetStoreStaff) || CheckIfStoreOwner(userID))
            {
                foreach (var owner in Owners)
                {
                    storeStaff.Add(owner.Value, ownerPermission);
                }

                foreach (var manager in Managers)
                {
                    storeStaff.Add(manager.Value, manager.Value.Permission);
                }

                return storeStaff;
            }
            throw new Exception("The given store staff does not have permission to see the stores staff members");
        }

        public StoreHistory GetStorePurchaseHistory(string userID, bool sysAdmin)
        {
            if (sysAdmin || CheckStoreManagerAndPermissions(userID, Methods.GetStorePurchaseHistory) || CheckIfStoreOwner(userID))
            {
                return History;
            }
            throw new Exception("No permission to see store purchase history");
        }

        public bool RemovePermissions(string managerID, string ownerID, LinkedList<int> permissions)
        {
            if ((CheckIfStoreOwner(ownerID) || CheckStoreManagerAndPermissions(ownerID, Methods.SetPermissions)) && Managers.TryGetValue(managerID, out StoreManager manager))
            {
                if (CheckAppointedBy(manager, ownerID))
                {
                    foreach (int per in permissions)
                    {
                        manager.SetPermission(per, false);
                    }
                    return true;
                }
                throw new Exception($"Can't remove permissions: Manager (ID: {managerID}) was not appointed by given staff member (ID: {ownerID})");
            }
            throw new Exception($"Staff ID not found in store");
        }

        public Product GetProduct(String productID)
        {
            return InventoryManager.GetProduct(productID);
        }

        private Boolean CheckAppointedBy(StoreManager manager, String ownerID)
        {
            return manager.AppointedBy.GetId().Equals(ownerID);
        }

        private Boolean CheckStoreManagerAndPermissions(String userID, Methods method)
        {
            return Managers.TryGetValue(userID, out StoreManager manager) && manager.Permission.functionsBitMask[(int)method];
        }

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
