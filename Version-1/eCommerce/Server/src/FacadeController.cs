﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using Server.src;
using Logger = Server.src.Logger;

namespace Server.src
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FacadeController : ApiController
    {
        eCommerceSystem system = new eCommerceSystem();

        // guest user
        [HttpGet]
        public string Login()
        {
            Result<GuestUserSO> output = system.GuestLogin();
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return "Error:" + output.ErrorMessage;
            }
            Console.WriteLine($"sfds: {output}");
            Logger.GetInstance().Event("Guest has connected with pid : " + output);
            return output.Value.Id;
        }

        [HttpGet]
        public string Login(string username, string password)
        {
            Result< RegisteredUserSO> output = system.Login(username, password);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return "Error:" + output.ErrorMessage;
            }
            Logger.GetInstance().Event(username + "has LoggedIn ");
            return output.Value.Id;
        }

        [HttpGet]
        public bool Logout(string username)
        {
            Result output = system.Logout(username);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event(username + "has LoggedOut ");
            return true;
        }

        [HttpGet]
        public String Register(string username, string password)
        {
            Result<RegisteredUserSO> output = system.Register(username, password);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return "Error:" + output.ErrorMessage;
            }
            Logger.GetInstance().Event(username + "has Register succesfully ");
            return output.Value.Id;
        }

        [HttpGet]
        public bool Purchase(string userName, string creditCard)
        {
            IDictionary<String, Object> paymentDetails = new Dictionary<String, Object>
                    {
                     { "card_number", "2222333344445555" },
                     { "month", "4" },
                     { "year", "2021" },
                     { "holder", "Israel Israelovice" },
                     { "ccv", "262" },
                     { "id", "20444444" }
                    };
            IDictionary<String, Object> deliveryDetails = new Dictionary<String, Object>
                    {
                     { "name", "Israel Israelovice" },
                     { "address", "Rager Blvd 12" },
                     { "city", "Beer Sheva" },
                     { "country", "Israel" },
                     { "zip", "8458527" }
                    };
            Result output = system.Purchase(userName, paymentDetails, deliveryDetails);
            if (!output.ErrorOccured)
                Logger.GetInstance().Event(userName + "has purchased succesfully ");
            else
                Logger.GetInstance().Error(output.ErrorMessage);
            return !output.ErrorOccured;
        }

        [HttpGet]
        public bool UpdateCart(string userId, string storeId, string productId, int newAmount)
        {
            Result output = system.UpdateShoppingCart(userId, storeId, productId, newAmount);
            if (output.ErrorOccured)
                Logger.GetInstance().Event("cart has updated succesfully ");
            else
                Logger.GetInstance().Error(output.ErrorMessage);

            return !output.ErrorOccured;
        }

        [HttpGet]
        public bool AddProductToCart(string userId, string productId, int quantity, string storeId)
        {
            bool output = !system.AddProductToCart(userId, productId, quantity, storeId).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(userId + " has has added product :" + productId + "form store: " + storeId + " to his Cart");
            else
                Logger.GetInstance().Error(userId + " could not add product :" + productId + "form store: " + storeId + " to his Cart");
            return output;
        }

        [HttpGet]
        public bool RemoveProductFromCart(string userId, string storeId, string productId)
        {
            bool output = !system.UpdateShoppingCart(userId, storeId, productId, 0).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(userId + " has has added product :" + productId + "form store: " + storeId + " to his cart");
            else
                Logger.GetInstance().Error(userId + " could not add product :" + productId + "form store: " + storeId + " to his cart");
            return output;
        }

        [HttpGet]
        public bool AddProductToStore(string userId, string storeId, string productName, int price, int quantity, string category)
        {
            bool output = !system.AddProductToStore(userId, storeId, productName, price, quantity, category).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(" user: " + userId + " product: " + productName + " has been added to the shop : " + storeId);
            else
                Logger.GetInstance().Error(" user: " + userId + " product: " + productName + " has not been added to the shop : " + storeId);
            return output;
        }

        [HttpGet]
        public bool RemoveProductFromStore(String userID, String storeID, String productID)
        {
            bool output = !system.RemoveProductFromStore(userID, storeID, productID).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(" user: " + userID + " product: " + productID + " has been removed from the shop : " + storeID);
            else
                Logger.GetInstance().Error(" user: " + userID + " product: " + productID + " has not been removed from the shop : " + storeID);
            return output;
        }

        [HttpGet]
        public String OpenShop(string storeName, string userId)
        {
            Result<StoreService> output = system.OpenNewStore(storeName, userId);
            if(output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return "Error:" + output.ErrorMessage;
            }
            Logger.GetInstance().Event("Store Opened");
            return output.Value.Id;
        }

        [HttpGet]
        public bool CloseShop(string storeId, string userId)
        {
            Result output = system.CloseStore(storeId, userId);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event("Store Closed");
            return true;
        }

        [HttpGet]
        public bool makeNewOwner(string newOwnerId, string currentOwnerId, string storeId)
        {
            bool output = !system.AddStoreOwner(newOwnerId, currentOwnerId, storeId).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(currentOwnerId + " has make new owner for " + storeId);
            else
                Logger.GetInstance().Error(currentOwnerId + " could not make new owner for " + storeId);
            return output;
        }

        [HttpGet]
        public bool AddStoreManager(string storeId, string currentOwnerId, string newManagerId, LinkedList<int> permissions)
        {
            bool output = !system.AddStoreManager(newManagerId, currentOwnerId, storeId).ErrorOccured;
            output = output && !system.SetPermissions(storeId, newManagerId, currentOwnerId, permissions).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(currentOwnerId + " has make new manager " + newManagerId + " for " + storeId);
            else
                Logger.GetInstance().Error(currentOwnerId + " could not make new manager for " + storeId);
            return output;
        }

        [HttpGet]
        public bool removeManager(string currentOwnerId, string storeId, string ManagerToRemove)
        {
            bool output = !system.RemoveStoreManager(ManagerToRemove, currentOwnerId, storeId).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(currentOwnerId + " has has removed manager" + ManagerToRemove + "form store: " + storeId);
            else
                Logger.GetInstance().Error(currentOwnerId + " could not removed manager for " + storeId);
            return output;
        }

        public bool removeOwner(string currentOwnerId, string storeId, string OwnerToRemove)
        {
            bool output = !system.RemoveStoreOwner(OwnerToRemove, currentOwnerId, storeId).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(currentOwnerId + " has has removed manager" + OwnerToRemove + "form store: " + storeId);
            else
                Logger.GetInstance().Error(currentOwnerId + " could not removed manager for " + storeId);
            return output;
        }


        [HttpGet]
        public string[][] SearchProduct(string keyword)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["KeyWords"] = keyword;
            Result<List<ProductService>> results = system.SearchProduct(parameters);
            if (results.ErrorOccured)
            {
                return new string[0][];
            }
            List<ProductService> products = results.Value;
            List<string[]> output = new List<string[]>();
            foreach (ProductService product in products)
            {
                output.Add(product.ToStringArray());
            }
            return output.ToArray();
        }

        [HttpGet]
        public string[][] SearchStore(string name)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["Name"] = name;
            Result<List<StoreService>> results = system.SearchStore(parameters);
            if (results.ErrorOccured)
            {
                return new string[0][];
            }
            List<StoreService> products = results.Value;
            List<string[]> output = new List<string[]>();
            foreach (StoreService product in products)
            {
                output.Add(product.ToStringArray());
            }
            return output.ToArray();
        }

        [HttpGet]
        public string[][] getUserShoppingCart(string userid)
        {
            Result<ShoppingCartSO> output = system.GetUserShoppingCart(userid);
            if(output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return new string[0][];
            }
            return output.Value.toArray();
        }

        [HttpGet]
        public string[][] getUserPurchaseHistory(string userid)
        {
            Result<UserHistorySO> output = system.GetUserPurchaseHistory(userid);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return new string[0][];
            }
            return output.Value.toArray();
        }

        [HttpGet]
        public bool reOpenStore(string storeid, string userid)
        {
            Result<StoreService> output = system.ReOpenStore(storeid,userid);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event("Store re-opened");
            return true;
        }

        [HttpGet]
        public bool editProductDetail(string userID, string storeID, string productID, IDictionary<string, object> details)
        {
            Result output = system.EditProductDetails(userID, storeID, productID, details);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event("Product details updated");
            return true;
        }

        [HttpGet]
        public bool SetPermissions(string storeID, string managerID, string ownerID, LinkedList<int> permissions)
        {
            Result output = system.SetPermissions(storeID, managerID, ownerID, permissions);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event("Permissions Set");
            return true;
        }

        [HttpGet]
        public bool RemovePermissions(string storeID, string managerID, string ownerID, LinkedList<int> permissions)
        {
            Result output = system.RemovePermissions(storeID, managerID, ownerID, permissions);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event("Permissions removed");
            return true;
        }

        [HttpGet]
        public String[] GetStoreStaff(string ownerID, string storeID)
        {
            Result<Dictionary<IStaffService, PermissionService>> output = system.GetStoreStaff(ownerID, storeID);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return new string[0];
            }
            Logger.GetInstance().Event("Store Closed");
            String[] strmat = new string[output.Value.Count];
            int i = 0;
            foreach(KeyValuePair<IStaffService, PermissionService> kvp in output.Value)
            {
                strmat[i] = kvp.Key.Id;
                i++;
            }
            return strmat;
        }

        [HttpGet]
        public bool AddStoreRating(String userid, String storeid, double rate)
        {
            Result<bool> output = system.AddStoreRating(userid, storeid, rate);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event("Store Rated");
            return true;
        }

        [HttpGet]
        public bool addProductRating(String userid, String storeid, String productid, double rate)
        {
            Result<bool> output = system.AddProductRatingInStore(userid, storeid, productid, rate);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event("Store Rated");
            return true;
        }

        [HttpGet]
        public string[][] getAllStores()
        {
            List<StoreService> output = system.GetAllStoresToDisplay();
            String[][] ret = new string[output.Count][];
            int i = 0;
            foreach (StoreService item in output)
            {
                ret[i] = item.ToStringArray();
                i++;
            }
            return ret;
        }

        [HttpGet]
        public String[][] GetAllProductByStoreIDToDisplay(string storeID)
        {
            List<ProductService> output = system.GetAllProductByStoreIDToDisplay(storeID);
            String[][] ret = new string[output.Count][];
            int i = 0;
            foreach (ProductService item in output)
            {
                ret[i] = item.ToStringArray();
                i++;
            }
            return ret;
        }

        [HttpGet]
        public Boolean[] getPermissions(string userID, string storeID)
        {
            Boolean[] output = system.GetPermission(userID,storeID);
            return output;
        }

        [HttpGet]
        public String getTotalShoppingCartPrice(string userid)
        {
            Result<double> output = system.GetTotalShoppingCartPrice(userid);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return "Error:" + output.ErrorMessage;
            }
            Logger.GetInstance().Event("Permissions removed");
            return output.Value.ToString();
        }


        // offers ?
        // system admin func ?
        // discount func ?









    }
}