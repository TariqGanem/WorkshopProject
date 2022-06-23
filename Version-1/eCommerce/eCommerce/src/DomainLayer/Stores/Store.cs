﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.DomainLayer.User;
using System.Threading;
using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.DomainLayer.Stores.Policies;
using MongoDB.Driver;
using MongoDB.Bson;
using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer.Stores.Policies.Offer;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using eCommerce.src.ServiceLayer.ResultService;
using eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies;
using eCommerce.src.DataAccessLayer.DataTransferObjects;
using eCommerce.src.DomainLayer.Stores.OwnerAppointmennt;

namespace eCommerce.src.DomainLayer.Store
{
    public interface IStoreOperations
    {
        void AddRating(Double rate);
        String AddNewProduct(String userID, String productName, Double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        List<Product> SearchProduct(IDictionary<String, Object> searchAttributes);
        void RemoveProduct(String userID, String productID);
        void EditProduct(String userID, String productID, IDictionary<String, Object> details);
        void UpdateInventory(ShoppingBag bag , MongoDB.Driver.IClientSessionHandle session);
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
        public String Id { get; set; }
        public String Name { get; set; }
        public Boolean Active { get; set; }
        public StoreOwner Founder { get; set; }
        public InventoryManager InventoryManager { get; set; }
        public History History { get; set; }
        public Double Rate { get; private set; }
        public int NumberOfRates { get; private set; }
        public ConcurrentDictionary<String, StoreOwner> Owners { get; set; }
        public ConcurrentDictionary<String, StoreManager> Managers { get; set; }
        public NotificationPublisher NotificationPublisher { get; set; }
        public PolicyManager PolicyManager { get; set; }
        public OfferManager OfferManager { get; set; }
        public OwnerRequestManager RequestManager { get; set; }



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
            PolicyManager = new PolicyManager();  
            OfferManager = new OfferManager();   
            RequestManager = new OwnerRequestManager();
        }

        public Store(string id, string name, InventoryManager inventoryManager, History history, double rate, int numberOfRates, NotificationPublisher notificationManager, bool active)
        {
            Id = id;
            Name = name;
            InventoryManager = inventoryManager;
            History = history;
            Rate = rate;
            NumberOfRates = numberOfRates;
            this.NotificationPublisher = notificationManager;
            Active = active;
            Owners = new ConcurrentDictionary<string, StoreOwner>();
            Managers = new ConcurrentDictionary<string, StoreManager>();
            PolicyManager = new PolicyManager();
            OfferManager = new OfferManager();
            RequestManager = new OwnerRequestManager();

        }

        public void AddRating(Double rate)
        {
            if (rate < 0 || rate > 5)
            {
                throw new Exception($"Store {Name} could not be rated. Please use number between 1 to 5");
            }
            else
            {
                NumberOfRates += 1;
                Rate = (Rate * (NumberOfRates - 1) + rate) / NumberOfRates;
                // db
                var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
                var update_offer = Builders<BsonDocument>.Update.Set("Rate", Rate).Set("NumberOfRates", NumberOfRates);
                DBUtil.getInstance().UpdateStore(filter, update_offer);
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

        public void UpdateInventory(ShoppingBag bag , MongoDB.Driver.IClientSessionHandle session = null)
        {
            ConcurrentDictionary<Product, int> product_quantity = bag.Products;     // <Product, Quantity user bought>
            foreach (var product in product_quantity)
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", product.Key.Id);
                var update_product = Builders<BsonDocument>.Update.Set("Quantity", product.Key.Quantity-product.Value);
                DBUtil.getInstance().UpdateProduct(filter, update_product, session);
            }
            foreach(KeyValuePair<Product,int> pros in product_quantity)
            {
                if(this.InventoryManager.Products.TryGetValue(pros.Key.Id,out Product product))
                    product.Quantity = product.Quantity-pros.Value;
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
                    owner.StoreManagers.Remove(manager);
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
                    ownerBoss.StoreOwners.Remove(ownerToRemove);
                    RemoveAllStaffAppointedByOwner(removedOwner);
                    return;
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

            if (CheckIfStoreOwner(userID) || CheckStoreManagerAndPermissions(userID, Methods.GetStoreStaff))
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

        public bool SendOfferToStore(Offer offer)
        {
            OfferManager.AddOffer(offer);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
            var update_offer = Builders<BsonDocument>.Update.Set("OfferManager", DBUtil.getInstance().Get_DTO_Offers(OfferManager.PendingOffers));
            DBUtil.getInstance().UpdateStore(filter, update_offer);
            return true;
        }

        public bool SendOwnerRequestToStore(OwnerRequest offer)
        {
            foreach(OwnerRequest req in RequestManager.PendingOffers)
            {
                if (req.UserID == offer.Id)
                    throw new Exception("Request Already in place for the current user");
            }
            RequestManager.AddOffer(offer);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
            var update_offer = Builders<BsonDocument>.Update.Set("OwnerRequestManager", DBUtil.getInstance().Get_DTO_OwnerRequests(RequestManager.PendingOffers));
            DBUtil.getInstance().UpdateStore(filter, update_offer);
            return true;
        }

        private void RemoveAllStaffAppointedByOwner(StoreOwner owner)
        {
            this.NotificationPublisher.notifyOwnerSubscriptionRemoved(owner.GetId(), owner);
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
                        var filterowner = Builders<BsonDocument>.Filter.Eq("AppointedBy", owner.User.Id);
                        DBUtil.getInstance().DeleteStoreManager(filterowner);
                    }
                }
            }
        }

        public StoreService getSO()
        {
            LinkedList<String> owners = new LinkedList<String>();
            foreach (var so in Owners)
                owners.AddLast(so.Key);
            LinkedList<String> managers = new LinkedList<String>();
            foreach (var sm in Managers)
                managers.AddLast(sm.Key);
            UserHistorySO history = History.getSO();

            StoreService store = new StoreService(this.Id, this.Name, Founder.User.Id, owners, managers, history, this.Rate, this.NumberOfRates);
            return store;
        }

        public void sendNotificationToAllOwners(Offer offer, bool v)
        {
            this.NotificationPublisher.notifyOfferRecievedUser(offer.UserID, offer.StoreID, offer.ProductID, offer.Amount, offer.Price, offer.CounterOffer, v);
        }

        public void sendNotificationToAllOwners(OwnerRequest offer, bool v)
        {
            this.NotificationPublisher.notifyOfferRecievedUser(offer.UserID, offer.StoreID, v);
        }

        public OfferResponse SendOfferResponseToUser(string ownerID, string offerID, bool accepted, double counterOffer)
        {
            List<string> ids = ownerIDs();
            if (!ids.Contains(ownerID))
                throw new Exception("Failed to reponse to an offer: The responding user is not an owner");
            OfferResponse res = OfferManager.SendOfferResponseToUser(ownerID, offerID, accepted, counterOffer, ids);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
            var update_offer = Builders<BsonDocument>.Update.Set("OfferManager", DBUtil.getInstance().Get_DTO_Offers(OfferManager.PendingOffers));
            DBUtil.getInstance().UpdateStore(filter, update_offer);
            return res;
        }

        public OwnerRequestResponse SendOwnerRequestResponseToUser(string ownerID, string RequestID, bool accepted)
        {
            List<string> ids = ownerIDs();
            if (!ids.Contains(ownerID))
                throw new Exception("Failed to respond to the request: The responding user is not an owner");
            OwnerRequestResponse res = RequestManager.SendOfferResponseToUser(ownerID, RequestID, accepted, ids);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
            var update_offer = Builders<BsonDocument>.Update.Set("OwnerRequestManager", DBUtil.getInstance().Get_DTO_OwnerRequests(RequestManager.PendingOffers));
            DBUtil.getInstance().UpdateStore(filter, update_offer);
            return res;
        }

        private List<string> ownerIDs()
        {
            List<string> ids = new List<string>();
            foreach (string id in Owners.Keys)
                ids.Add(id);
            return ids;
        }

        public List<Dictionary<string, object>> getStoreOffers()
        {
            return OfferManager.getStoreOffers();
        }

        public List<Dictionary<string, string>> getStoreOwnerRequests()
        {
            return RequestManager.getStoreOffers();
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
                inventoryManagerProducts_dto, History.getDTO(), Rate, NumberOfRates, Active,
                PolicyManager.MainDiscount.getDTO(), PolicyManager.MainPolicy.getDTO(), Get_DTO_Offers() , Get_DTO_OwnerRequests());
        }

        public List<DTO_Offer> Get_DTO_Offers()
        {
            List<DTO_Offer> dto_offers = new List<DTO_Offer>();
            foreach (Offer offer in OfferManager.PendingOffers)
            {
                dto_offers.Add(new DTO_Offer(offer.Id, offer.UserID, offer.ProductID, offer.StoreID, offer.Amount, offer.Price, offer.CounterOffer, offer.acceptedOwners));
            }

            return dto_offers;
        }

        public List<DTO_OwnerRequest> Get_DTO_OwnerRequests()
        {
            List<DTO_OwnerRequest> dto_offers = new List<DTO_OwnerRequest>();
            foreach (OwnerRequest offer in RequestManager.PendingOffers)
            {
                dto_offers.Add(new DTO_OwnerRequest(offer.Id, offer.UserID, offer.StoreID, offer.AppointedBy , offer.acceptedOwners));
            }

            return dto_offers;
        }

        public IDiscountPolicy AddDiscountPolicy(Dictionary<string, object> info)
        {
            return this.PolicyManager.AddDiscountPolicy(info);
        }

        public IDiscountPolicy AddDiscountPolicy(Dictionary<string, object> info, string id)
        {
            return PolicyManager.AddDiscountPolicy(info, id);
        }

        public bool[] GetPermission(string userID)
        {
            return getUserPermissionByID(userID);
        }

        private Boolean[] getUserPermissionByID(string userID)
        {
            Boolean[] per = new Boolean[13];
            foreach (var user in Owners)
            {
                if (user.Key == userID)
                {
                    StoreOwner owner = user.Value;
                    for (int i = 0; i < per.Length; i++)
                    {
                        per[i] = true;
                    }
                    return per;
                }
            }
            foreach (var user in Managers)
            {
                if (user.Key == userID)
                {
                    StoreManager manager = user.Value;
                    return (Boolean[])manager.Permission.functionsBitMask;
                }
            }
            for (int i = 0; i < per.Length; i++)
            {
                per[i] = false;
            }
            return per;
        }

        public IDiscountCondition AddDiscountCondition(Dictionary<string, object> info, string id)
        {
            return PolicyManager.AddDiscountCondition(info, id);
        }

        public IDiscountPolicy RemoveDiscountPolicy(string id)
        {
            return PolicyManager.RemoveDiscountPolicy(id);
        }

        public IDiscountCondition RemoveDiscountCondition(string id)
        {
            return PolicyManager.RemoveDiscountCondition(id);
        }

        public bool EditDiscountPolicy(Dictionary<string, object> info, string id)
        {
            return PolicyManager.EditDiscountPolicy(info, id);
        }

        public bool EditDiscountCondition(Dictionary<string, object> info, string id)
        {
            return PolicyManager.EditDiscountCondition(info, id);
        }

        public IDictionary<string, object> GetPoliciesData()
        {
            return PolicyManager.GetDiscountPolicyData();
        }

        public IDictionary<string, object> GetPurchasePolicyData()
        {
            return PolicyManager.GetPurchasePolicyData();
        }

        public IPurchasePolicy AddPurchasePolicy(Dictionary<string, object> info)
        {
            return PolicyManager.AddPurchasePolicy(info);
        }

        public IPurchasePolicy AddPurchasePolicy(Dictionary<string, object> info, string id)
        {
            return PolicyManager.AddPurchasePolicy(info, id);
        }

        public bool EditPurchasePolicy(Dictionary<string, object> info, string id)
        {
            return PolicyManager.EditPurchasePolicy(info, id);
        }

        public IPurchasePolicy RemovePurchasePolicy(string id)
        {
            return PolicyManager.RemovePurchasePolicy(id);
        }

        public void addRatingToProduct(string productid, double rate)
        {
            if (this.InventoryManager.Products.TryGetValue(productid, out Product pro))
            {
                pro.AddRating(rate);
            }
            else
                throw new Exception($"product {productid} does not exist in this store");
        }

        public string getProductId(string productname)
        {
            foreach(Product product in this.InventoryManager.Products.Values)
            {
                if(product.Name.Equals(productname))
                    return product.Id;
            }
            throw new Exception($"product : {productname} does not exit in the store");
        }
    }
}
