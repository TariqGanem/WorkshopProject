using eCommerce.src.DomainLayer;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public interface IStoreStaffController
    {
        Result AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID);
        Result AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID);
        Result RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID);
        Result RemoveStoreOwner(String removedOwnerID, String currentlyOwnerID, String storeID);
        Result SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Result RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Result<Dictionary<IStaffService, PermissionService>> GetStoreStaff(String ownerID, String storeID);
        Result<String> AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        Result RemoveProductFromStore(String userID, String storeID, String productID);
        Result EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details);
        Result<List<ProductService>> SearchProduct(IDictionary<String, Object> productDetails);
        Result<UserHistorySO> GetStorePurchaseHistory(String ownerID, String storeID, Boolean isSystemAdmin = false);
    }

    public class StoreStaffController : IStoreStaffController
    {
        //Properties
        public ISystemFacade SystemFacade { get; set; }
        Logger logger = Logger.GetInstance();

        //Constructor
        public StoreStaffController(ISystemFacade systemFacade)
        {
            this.SystemFacade = systemFacade;
        }

        public Result<String> AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null)
        {
            try
            {
                string id = SystemFacade.AddProductToStore(userID, storeID, productName, price, initialQuantity, category, keywords);
                logger.LogInfo($"StoreStaffController --> User with id: {userID}, added {productName} product to the store with id: {storeID}.");
                return new Result<String>(id, null);
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result<String>(error.Message);
            }
        }
        public Result RemoveProductFromStore(String userID, String storeID, String productID)
        {
            try
            {
                SystemFacade.RemoveProductFromStore(userID, storeID, productID);
                logger.LogInfo($"StoreStaffController --> User with id: {userID}, removed product with id: {productID} from the store with id: {storeID}.");
                return new Result();
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result(error.Message);
            }
        }
        public Result EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details)
        {
            //SystemFacade.EditProductDetails(userID, storeID, productID, details);
            try
            {
                SystemFacade.EditProductDetails(userID, storeID, productID, details);
                logger.LogInfo($"StoreStaffController --> User with id: {userID}, edited a product: {productID} details.");
                return new Result();
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result(error.Message);
            }
        }
        public Result AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID)
        {
            try
            {
                SystemFacade.AddStoreOwner(addedOwnerID, currentlyOwnerID, storeID);
                logger.LogInfo($"StoreStaffController --> User with id: {currentlyOwnerID}, added a new store owner with id {addedOwnerID} to store {storeID}.");
                return new Result();
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result(error.Message);
            }
        }
        public Result AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID)
        {
            try
            {
                SystemFacade.AddStoreManager(addedManagerID, currentlyOwnerID, storeID);
                logger.LogInfo($"StoreStaffController --> User with id: {currentlyOwnerID}, added a new store owner with id {addedManagerID} to store {storeID}.");
                return new Result();
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result(error.Message);
            }
        }
        public Result SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions)
        {
            try
            {
                SystemFacade.SetPermissions(storeID, managerID, ownerID, permissions);
                logger.LogInfo($"StoreStaffController --> User with id: {ownerID}, added new permissions to the user with id: {managerID} in store {storeID}.");
                return new Result();
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result(error.Message);
            }
        }
        public Result RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions)
        {
            try
            {
                SystemFacade.RemovePermissions(storeID, managerID, ownerID, permissions);
                logger.LogInfo($"StoreStaffController --> User with id: {ownerID}, removed permissions to the user with id: {managerID} in store {storeID}.");
                return new Result();
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result(error.Message);
            }
        }
        public Result<Dictionary<IStaffService, PermissionService>> GetStoreStaff(String ownerID, String storeID)
        {
            try
            {
                Dictionary<IStaffService, PermissionService> res = SystemFacade.GetStoreStaff(ownerID, storeID);
                logger.LogInfo($"StoreStaffController --> Getting store staff details with id: {storeID} successfully.");
                return new Result<Dictionary<IStaffService, PermissionService>>(res);
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result<Dictionary<IStaffService, PermissionService>>(error.Message);
            }
        }
        public Result<UserHistorySO> GetStorePurchaseHistory(String ownerID, String storeID, Boolean isSystemAdmin = false)
        {
            try
            {
                logger.LogInfo($"StoreStaffController --> User with id: {ownerID} getting store purchase history with id: {storeID} successfully.");
                return new Result<UserHistorySO>(SystemFacade.GetStorePurchaseHistory(ownerID, storeID));
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result<UserHistorySO>(error.Message);
            }
        }
        public Result RemoveStoreManager(string removedManagerID, string currentlyOwnerID, string storeID)
        {
            try
            {
                SystemFacade.RemoveStoreManager(removedManagerID, currentlyOwnerID, storeID);
                logger.LogInfo($"StoreStaffController --> User with id: {currentlyOwnerID} has removed user with id: {removedManagerID} from management successfully.");
                return new Result();
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result<UserHistorySO>(error.Message);
            }
        }

        public Result RemoveStoreOwner(String removedOwnerID, String currentlyOwnerID, String storeID)
        {
            try
            {
                SystemFacade.RemoveStoreOwner(removedOwnerID, currentlyOwnerID, storeID);
                logger.LogInfo($"StoreStaffController --> User with id: {currentlyOwnerID} has removed user with id: {removedOwnerID} from management successfully.");
                return new Result();
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result<UserHistorySO>(error.Message);
            }
        }

        public Result<List<ProductService>> SearchProduct(IDictionary<String, Object> productDetails)
        {
            try
            {
                List<ProductService> res = SystemFacade.SearchProduct(productDetails);
                logger.LogInfo($"StoreStaffController --> Getting product details successfully");
                return new Result<List<ProductService>>(res);
            }
            catch (Exception error)
            {
                logger.LogError("StoreStaffController --> " + error.Message);
                return new Result<List<ProductService>>(error.Message);
            }
        }
    }
}
