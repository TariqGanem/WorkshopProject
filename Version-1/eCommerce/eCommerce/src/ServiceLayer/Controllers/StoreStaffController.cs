﻿using eCommerce.src.DomainLayer;
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
        Result AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID);
        Result AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID);
        Result RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID);
        Result SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Result RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Result<Dictionary<IStaffService, PermissionService>> GetStoreStaff(String ownerID, String storeID);
        Result AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        Result RemoveProductFromStore(String userID, String storeID, String productID);
        Result EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details);
        Result<List<ProductService>> SearchProduct(IDictionary<String, Object> productDetails);
        Result<UserHistorySO> GetStorePurchaseHistory(String ownerID, String storeID, Boolean isSystemAdmin = false);

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

        public Result AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null) { 
            try
            {
                SystemFacade.AddProductToStore(userID, storeID, productName, price, initialQuantity, category, keywords);
                return new Result();
            }
            catch (Exception error)
            {
                return new Result(error.Message);
            }
        }
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
        public Result EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details) { 
            SystemFacade.EditProductDetails(userID, storeID, productID, details);
            try
            {
                SystemFacade.EditProductDetails(userID, storeID, productID, details);
                return new Result();
            }
            catch (Exception error)
            {
                return new Result(error.Message);
            }
        }
        public Result AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID) { 
            try
            {
                SystemFacade.AddStoreOwner(addedOwnerID, currentlyOwnerID, storeID);
                return new Result();
            }
            catch (Exception error)
            {
                return new Result(error.Message);
            }
        }
        public Result AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID) { 
            try
            {
                SystemFacade.AddStoreManager(addedManagerID, currentlyOwnerID, storeID);
                return new Result();
            }
            catch (Exception error)
            {
                return new Result(error.Message);
            }
        }
        public Result SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions) { 
            try
            {
                SystemFacade.SetPermissions(storeID, managerID, ownerID, permissions);
                return new Result();
            }
            catch (Exception error)
            {
                return new Result(error.Message);
            }
        }
        public Result RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions) { 
            try
            {
                SystemFacade.RemovePermissions(storeID, managerID, ownerID, permissions);
                return new Result();
            }
            catch (Exception error)
            {
                return new Result(error.Message);
            }
        }
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
        public Result<UserHistorySO> GetStorePurchaseHistory(String ownerID, String storeID, Boolean isSystemAdmin = false) {
            try
            {
                return new Result<UserHistorySO>(SystemFacade.GetStorePurchaseHistory(ownerID, storeID));
            }
            catch (Exception error)
            {
                return new Result<UserHistorySO>(error.Message);
            }
        }
        public Result RemoveStoreManager(string removedManagerID, string currentlyOwnerID, string storeID) { 
            try
            {
                SystemFacade.RemoveStoreManager(removedManagerID, currentlyOwnerID, storeID);
                return new Result();
            }
            catch (Exception error)
            {
                return new Result<UserHistorySO>(error.Message);
            }
        }
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
