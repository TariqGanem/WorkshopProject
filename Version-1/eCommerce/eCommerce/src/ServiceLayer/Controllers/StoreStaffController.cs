using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.Response;
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
        Result<Dictionary<IStaffService, PermissionService>> GetStoreStaff(String ownerID, String storeID);
        void AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        Result RemoveProductFromStore(String userID, String storeID, String productID);
        void EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details);
        Result<List<ProductService>> SearchProduct(IDictionary<String, Object> productDetails);
        Result<StoreHistoryService> GetStorePurchaseHistory(String ownerID, String storeID, Boolean isSystemAdmin = false);

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
        public Result RemoveProductFromStore(String userID, String storeID, String productID)
        {
            try
            {
                SystemFacade.RemoveProductFromStore(userID, storeID, productID);
                return new Result();
            }
            catch (Exception error)
            {
                return new Result(error.Message);
            }
        }
        public void EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details) { SystemFacade.EditProductDetails(userID, storeID, productID, details); }
        public void AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID) { SystemFacade.AddStoreOwner(addedOwnerID, currentlyOwnerID, storeID); }
        public void AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID) { SystemFacade.AddStoreManager(addedManagerID, currentlyOwnerID, storeID); }
        public void SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions) { SystemFacade.SetPermissions(storeID, managerID, ownerID, permissions); }
        public void RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions) { SystemFacade.RemovePermissions(storeID, managerID, ownerID, permissions); }
        public Result<Dictionary<IStaffService, PermissionService>> GetStoreStaff(String ownerID, String storeID) {
            try
            {
                return new Result<Dictionary<IStaffService, PermissionService>>(SystemFacade.GetStoreStaff(ownerID, storeID));
            }
            catch (Exception error)
            {
                return new Result<Dictionary<IStaffService, PermissionService>>(error.Message);
            }
        }
        public Result<StoreHistoryService> GetStorePurchaseHistory(String ownerID, String storeID, Boolean isSystemAdmin = false) {
            try
            {
                return new Result<StoreHistoryService>(SystemFacade.GetStorePurchaseHistory(ownerID, storeID));
            }
            catch (Exception error)
            {
                return new Result<StoreHistoryService>(error.Message);
            }
        }
        public void RemoveStoreManager(string removedManagerID, string currentlyOwnerID, string storeID) { SystemFacade.RemoveStoreManager(removedManagerID, currentlyOwnerID, storeID); }
        public Result<List<ProductService>> SearchProduct(IDictionary<String, Object> productDetails) 
        {
            try
            {
                return new Result<List<ProductService>>(SystemFacade.SearchProduct(productDetails));
            }
            catch (Exception error)
            {
                return new Result<List<ProductService>>(error.Message);
            }
        }
    }
}
