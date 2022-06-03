using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.DomainLayer.User;
using System.Threading;
using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using eCommerce.src.DomainLayer.Stores;

namespace eCommerce.src.DomainLayer.Store
{
    public interface IStoreOperations
    {
        void AddRating(Double rate);
        String AddNewProduct(String userID, String productName, Double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        List<Product> SearchProduct(IDictionary<String, Object> searchAttributes);
        void RemoveProduct(String userID, String productID);
        void EditProduct(String userID, String productID, IDictionary<String, Object> details);
        void UpdateInventory(ShoppingBag bag);
        void AddStoreOwner(RegisteredUser futureOwner, String currentlyOwnerID);
        void AddStoreManager(RegisteredUser futureManager, String currentlyOwnerID);
        void RemoveStoreManager(String removedManagerID, String currentlyOwnerID);
        void RemoveStoreOwner(String removedOwnerID, string currentlyOwnerID);
        void SetPermissions(String managerID, String ownerID, LinkedList<int> permissions);
        void RemovePermissions(String managerID, String ownerID, LinkedList<int> permissions);
        Dictionary<IStaff, Permission> GetStoreStaff(String userID);
        History GetStorePurchaseHistory(string ownerID, bool sysAdmin);
        Product GetProduct(string productID);
    }
    public class Store : IStoreOperations
    {
        public String Id { get; }
        public String Name { get; }
        public Boolean Active { get; set; }
        public StoreOwner Founder { get; }
        public InventoryManager InventoryManager { get; }
        public History History { get; }
        public Double Rate { get; private set; }
        public int NumberOfRates { get; private set; }
        public ConcurrentDictionary<String, StoreOwner> Owners { get; }
        public ConcurrentDictionary<String, StoreManager> Managers { get; }
        public NotificationPublisher NotificationPublisher { get; set; }
        public PolicyHandler PolicyHandler;

        public Store(String name, RegisteredUser founder)
        {
            Id = Service.GenerateId();
            Name = name;
            Active = true;
            Founder = new StoreOwner(founder, Id, null);
            Owners = new ConcurrentDictionary<string, StoreOwner>();
            Owners.TryAdd(founder.Id, Founder);
            Managers = new ConcurrentDictionary<string, StoreManager>();
            InventoryManager = new InventoryManager();
            History = new History();
            PolicyHandler = new PolicyHandler();
        }

        public void AddRating(Double rate)
        {
            if (rate < 1 || rate > 5)
            {
                throw new Exception($"Store {Name} could not be rated. Please use number between 1 to 5");
            }
            else
            {
                NumberOfRates += 1;
                Rate = (Rate * (NumberOfRates - 1) + rate) / NumberOfRates;
            }
        }

        public List<Product> SearchProduct(IDictionary<String, Object> searchAttributes)
        {
            return InventoryManager.SearchProduct(Rate, searchAttributes);
        }

        public String AddNewProduct(String userID, String productName, Double price, int initialQuantity, String category, LinkedList<String> keyWords = null)
        {
            if (CheckIfStoreOwner(userID) || CheckStoreManagerAndPermissions(userID, Methods.AddNewProduct))
            {
                return InventoryManager.AddNewProduct(productName, price, initialQuantity, category,this.NotificationPublisher ,keyWords);
            }
            else
            {
                throw new Exception($"{userID} does not have permissions to add new product to {this.Name}");
            }
        }

        public void RemoveProduct(String userID, String productID)
        {
            try
            {
                Monitor.TryEnter(productID);
                try
                {
                    if (CheckIfStoreOwner(userID) || CheckStoreManagerAndPermissions(userID, Methods.RemoveProduct))
                    {
                        InventoryManager.RemoveProduct(productID);
                    }
                    else
                    {
                        throw new Exception($"{userID} does not have permissions to remove products from {this.Name}");
                    }
                }
                finally
                {
                    Monitor.Exit(productID);
                }
            }
            catch (SynchronizationLockException SyncEx)
            {
                throw new Exception("A SynchronizationLockException occurred. Message: " + SyncEx.Message);
            }

        }

        public void EditProduct(String userID, String productID, IDictionary<String, Object> details)
        {
            if (CheckIfStoreOwner(userID) || CheckStoreManagerAndPermissions(userID, Methods.EditProduct))
            {
                InventoryManager.EditProduct(productID, details);
            }
            else
            {
                throw new Exception($"{userID} does not have permissions to edit products' information in {this.Name}");
            }
        }

        public void UpdateInventory(ShoppingBag bag)
        {
            ConcurrentDictionary<Product, int> product_quantity = bag.Products;
            foreach (var product in product_quantity)
            {

                product.Key.UpdatePurchasedProductQuantity(product.Value);
            }
        }


        public void AddStoreOwner(RegisteredUser futureOwner, string currentlyOwnerID)
        {
            try
            {
                Monitor.TryEnter(futureOwner);
                try
                {
                    // Check new owner not already an owner + appointing owner is not a fraud or the appointing user is a manager with the right permissions
                    if (!CheckIfStoreOwner(futureOwner.Id))
                    {
                        StoreOwner newOwner;
                        if (Owners.TryGetValue(currentlyOwnerID, out StoreOwner owner))
                        {
                            newOwner = new StoreOwner(futureOwner, Id, owner);
                            Owners.TryAdd(futureOwner.Id, newOwner);
                        }
                        else if (Managers.TryGetValue(currentlyOwnerID, out StoreManager manager) && CheckStoreManagerAndPermissions(currentlyOwnerID, Methods.AddStoreOwner))
                        {
                            newOwner = new StoreOwner(futureOwner, Id, manager);
                            Owners.TryAdd(futureOwner.Id, newOwner);
                        }
                        else
                        {
                            throw new Exception($"Failed to add store owner: Appointing owner (Email: {currentlyOwnerID}) " +
                                $"is not an owner at ${this.Name}");
                        }
                        if (CheckIfStoreManager(futureOwner.Id)) //remove from managers list if needed
                        {
                            Managers.TryRemove(futureOwner.Id, out _);
                        }
                    }
                    else
                    {
                        throw new Exception($"Failed to add store owner: Appointing owner (Email: {currentlyOwnerID}). The user is already an owner.");
                    }
                }
                finally
                {
                    Monitor.Exit(futureOwner);
                }
            }
            catch (SynchronizationLockException SyncEx)
            {
                throw new Exception("A SynchronizationLockException occurred. Message: " + SyncEx.Message);
            }
        }

        public void AddStoreManager(RegisteredUser futureManager, string currentlyOwnerID)
        {
            try
            {
                Monitor.TryEnter(futureManager);
                try
                {
                    // Check new manager not already an owner/manager + appointing owner is not a fraud or the appointing user is a manager with the right permissions
                    if (!CheckIfStoreManager(futureManager.Id) && !CheckIfStoreOwner(futureManager.Id))
                    {
                        StoreManager newManager;
                        if (Owners.TryGetValue(currentlyOwnerID, out StoreOwner owner))
                        {
                            newManager = new StoreManager(futureManager, this, new Permission(), owner);
                            Managers.TryAdd(futureManager.Id, newManager);
                        }
                        else if (Managers.TryGetValue(currentlyOwnerID, out StoreManager manager) && CheckStoreManagerAndPermissions(currentlyOwnerID, Methods.AddStoreManager))
                        {
                            newManager = new StoreManager(futureManager, this, new Permission(), manager);
                            Managers.TryAdd(futureManager.Id, newManager);
                        }
                        else
                        {
                            throw new Exception($"Failed to add store manager because appoitend user is not an owner or manager with relevant permissions at the store");
                        }
                    }
                    else
                    {
                        throw new Exception($"Failed to add store manager. The user is already an manager or owner in the store");
                    }
                }
                finally
                {
                    Monitor.Exit(futureManager);
                }
            }
            catch (SynchronizationLockException SyncEx)
            {
                throw new Exception("A SynchronizationLockException occurred. Message: " + SyncEx.Message);
            }
        }

        public void RemoveStoreManager(String removedManagerID, string currentlyOwnerID)
        {
            if (Owners.TryGetValue(currentlyOwnerID, out StoreOwner owner) && Managers.TryGetValue(removedManagerID, out StoreManager manager))
            {
                if (manager.AppointedBy.Equals(owner))
                {
                    Managers.TryRemove(removedManagerID, out _);
                }
                else
                {
                    throw new Exception($"Failed to remove user (Email: {removedManagerID}) from store management: Unauthorized owner (Email: {currentlyOwnerID})");
                }
            }
            else
            {
                throw new Exception($"Failed to remove user (Email: {removedManagerID}) from store management: Either not a manager or owner not found");
            }
        }

        public void RemoveStoreOwner(String removedOwnerID, string currentlyOwnerID)
        {
            if (Owners.TryGetValue(currentlyOwnerID, out StoreOwner ownerBoss) && Owners.TryGetValue(removedOwnerID, out StoreOwner ownerToRemove))
            {
                if (ownerToRemove.AppointedBy != null && ownerToRemove.AppointedBy.Equals(ownerBoss))
                {
                    Owners.TryRemove(removedOwnerID, out StoreOwner removedOwner);
                    RemoveAllStaffAppointedByOwner(removedOwner);
                }
                //else failed
                throw new Exception($"Failed to remove user (Id: {removedOwnerID}) as store owner: Unauthorized owner (Id: {currentlyOwnerID})");
            }
            //else failed
            throw new Exception($"Failed to remove user (Id: {removedOwnerID}) as store owner: Either currently owner or owner to be romoved not found");
        }

        public void SetPermissions(string managerID, string ownerID, LinkedList<int> permissions)
        {
            if ((CheckIfStoreOwner(ownerID) || CheckStoreManagerAndPermissions(ownerID, Methods.SetPermissions)) && Managers.TryGetValue(managerID, out StoreManager manager))
            {
                if (CheckAppointedBy(manager, ownerID))
                {
                    foreach (int per in permissions)
                    {
                        manager.SetPermission(per, true);
                    }
                }
                else
                {
                    throw new Exception($"Can't set permissions: Manager (ID: {managerID}) was not appointed by given staff member (ID: {ownerID})");
                }
            }
            else
            {
                throw new Exception($"Staff ID not found in store.");
            }
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
            else
            {
                throw new Exception("The given store staff does not have permission to see the stores staff members");
            }
        }

        public History GetStorePurchaseHistory(string userID, bool sysAdmin)
        {
            if (sysAdmin || CheckStoreManagerAndPermissions(userID, Methods.GetStorePurchaseHistory) || CheckIfStoreOwner(userID))
            {
                return History;
            }
            else
            {
                throw new Exception("No permission to see store purchase history");
            }
        }

        public void RemovePermissions(string managerID, string ownerID, LinkedList<int> permissions)
        {
            if ((CheckIfStoreOwner(ownerID) || CheckStoreManagerAndPermissions(ownerID, Methods.SetPermissions)) && Managers.TryGetValue(managerID, out StoreManager manager))
            {
                if (CheckAppointedBy(manager, ownerID))
                {
                    foreach (int per in permissions)
                    {
                        manager.SetPermission(per, false);
                    }
                }
                else
                {
                    throw new Exception($"Can't remove permissions: Manager (ID: {managerID}) was not appointed by given staff member (ID: {ownerID})");
                }
            }
            else
            {
                throw new Exception($"Staff ID not found in store");
            }
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

        private void RemoveAllStaffAppointedByOwner(StoreOwner owner)
        {
            //NotificationManager.notifyOwnerSubscriptionRemoved(owner.GetId(), owner);
            if (Owners.Count != 0)
            {
                foreach (var staff_owner in Owners)
                {
                    if (staff_owner.Value.AppointedBy != null && staff_owner.Value.AppointedBy.GetId() == owner.GetId())
                    {
                        Owners.TryRemove(staff_owner.Value.AppointedBy.GetId(), out StoreOwner removedOwner);
                        RemoveAllStaffAppointedByOwner(removedOwner);
                    }
                }
            }

            if (Managers.Count != 0)
            {
                foreach (var staff_manager in Managers)
                {
                    if (staff_manager.Value.AppointedBy.GetId() == owner.GetId())
                    {
                        Managers.TryRemove(staff_manager.Value.GetId(), out _);
                    }
                }
            }
        }

        public DTO_Store getDTO()
        {
            LinkedList<String> owners_dto = new LinkedList<string>();
            foreach (var owner in Owners)
            {
                owners_dto.AddLast(owner.Key);
            }
            LinkedList<String> managers_dto = new LinkedList<string>();
            foreach (var manager in Managers)
            {
                managers_dto.AddLast(manager.Key);
            }
            LinkedList<String> inventoryManagerProducts_dto = new LinkedList<string>();
            ConcurrentDictionary<String, Product> Products = InventoryManager.Products;
            foreach (var p in Products)
            {
                inventoryManagerProducts_dto.AddLast(p.Key);
            }
            return new DTO_Store(Id, Name, Founder.User.Id, owners_dto, managers_dto,
                       inventoryManagerProducts_dto, History.getDTO(), Rate, NumberOfRates, this.Active);

        }
    }
}
