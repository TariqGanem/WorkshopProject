using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.User.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public interface IStoreStaffInterface
    {
        void AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID);
        void AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID);
        void RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID);
        void SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        void RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Dictionary<IStaff, Permission> GetStoreStaff(String ownerID, String storeID);
        void AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        void RemoveProductFromStore(String userID, String storeID, String productID);
        void EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details);
        List<Product> SearchProduct(IDictionary<String, Object> productDetails);
    }
    public class StoreStaffController : IStoreStaffInterface
    {
        //Properties
        public ISystemFacade SystemFacade { get; }

        //Constructor
        public StoreStaffController(ISystemFacade systemFacade)
        {
            this.SystemFacade = systemFacade;
        }

        public void AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null) { SystemFacade.AddProductToStore(userID, storeID, productName, price, initialQuantity, category, keywords); }
        public void RemoveProductFromStore(String userID, String storeID, String productID) { SystemFacade.RemoveProductFromStore(userID, storeID, productID); }
        public void EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details) { SystemFacade.EditProductDetails(userID, storeID, productID, details); }
        public void AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID) { SystemFacade.AddStoreOwner(addedOwnerID, currentlyOwnerID, storeID); }
        public void AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID) { SystemFacade.AddStoreManager(addedManagerID, currentlyOwnerID, storeID); }
        public void SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions) { SystemFacade.SetPermissions(storeID, managerID, ownerID, permissions); }
        public void RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions) { SystemFacade.RemovePermissions(storeID, managerID, ownerID, permissions); }
        public Dictionary<IStaff, Permission> GetStoreStaff(String ownerID, String storeID) { return SystemFacade.GetStoreStaff(ownerID, storeID); }
        public StoreHistory GetStorePurchaseHistory(String ownerID, String storeID, Boolean isSystemAdmin = false) { return SystemFacade.GetStorePurchaseHistory(ownerID, storeID); }
        public void RemoveStoreManager(string removedManagerID, string currentlyOwnerID, string storeID) { SystemFacade.RemoveStoreManager(removedManagerID, currentlyOwnerID, storeID); }
        public List<Product> SearchProduct(IDictionary<String, Object> productDetails) { return SystemFacade.SearchProduct(productDetails); }
    }
}
