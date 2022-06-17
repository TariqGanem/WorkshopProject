using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using Logger = ServerApi.src.Logger;

namespace ServerApi
{
    //[RoutePrefix("api/facade")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class facadeController : ApiController
    {
        private eCommerceSystem facade = new eCommerceSystem();

        // guest user
        [HttpGet]
        public string GuestLogin()
        {
            Result<GuestUserSO> output = facade.GuestLogin();
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
            Result<RegisteredUserSO> output = facade.Login(username, password);
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
            Result output = facade.Logout(username);
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
            Result<RegisteredUserSO> output = facade.Register(username, password);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return "Error:" + output.ErrorMessage;
            }
            Logger.GetInstance().Event(username + "has Register succesfully ");
            return output.Value.Id;
        }

        [HttpGet]
        public bool Purchase(string userName, string card_number , string month , string year ,
            string holder , string ccv , string id , string name , string address , string city , string country , string zip) // for fixing in gui 
        {
            IDictionary<String, Object> paymentDetails = new Dictionary<String, Object>
                    {
                     { "card_number", card_number },
                     { "month", month },
                     { "year", year },
                     { "holder", holder },
                     { "ccv", ccv },
                     { "id", id }
                    };
            IDictionary<String, Object> deliveryDetails = new Dictionary<String, Object>
                    {
                     { "name", name },
                     { "address", address },
                     { "city", city },
                     { "country", country },
                     { "zip", zip }
                    };
            Result output = facade.Purchase(userName, paymentDetails, deliveryDetails);
            if (!output.ErrorOccured)
                Logger.GetInstance().Event(userName + "has purchased succesfully ");
            else
                Logger.GetInstance().Error(output.ErrorMessage);
            return !output.ErrorOccured;
        }

        [HttpGet]
        public bool UpdateCart(string userId, string storeId, string productId, int newAmount)
        {
            Result output = facade.UpdateShoppingCart(userId, storeId, productId, newAmount);
            if (output.ErrorOccured)
                Logger.GetInstance().Event("cart has updated succesfully ");
            else
                Logger.GetInstance().Error(output.ErrorMessage);

            return !output.ErrorOccured;
        }

        [HttpGet]
        public String[] getNotifications(string userid)
        {
            Result<LinkedList<Notification>> output = facade.getUserNotifications(userid);
            if(output.ErrorOccured)
            {
                Logger.GetInstance().Event(output.ErrorMessage);
                return new string[] {$"Error:{output.ErrorMessage}"};
            }
            String[] res = new String[output.Value.Count];
            int index = 0;
            foreach(Notification notification in output.Value)
            {
                res[index] = notification.ToString();
                index++;
            }
            return res;
        }

        [HttpGet]
        public bool AddProductToCart(string userId, string productId, int quantity, string storeId)
        {
            bool output = !facade.AddProductToCart(userId, productId, quantity, storeId).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(userId + " has has added product :" + productId + "form store: " + storeId + " to his Cart");
            else
                Logger.GetInstance().Error(userId + " could not add product :" + productId + "form store: " + storeId + " to his Cart");
            return output;
        }

        [HttpGet]
        public bool RemoveProductFromCart(string userId, string storeId, string productId)
        {
            bool output = !facade.UpdateShoppingCart(userId, storeId, productId, 0).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(userId + " has has added product :" + productId + "form store: " + storeId + " to his cart");
            else
                Logger.GetInstance().Error(userId + " could not add product :" + productId + "form store: " + storeId + " to his cart");
            return output;
        }

        [HttpGet]
        public bool AddProductToStore(string userId, string storeId, string productName, int price, int quantity, string category)
        {
            bool output = !facade.AddProductToStore(userId, storeId, productName, price, quantity, category).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(" user: " + userId + " product: " + productName + " has been added to the shop : " + storeId);
            else
                Logger.GetInstance().Error(" user: " + userId + " product: " + productName + " has not been added to the shop : " + storeId);
            return output;
        }

        [HttpGet]
        public bool RemoveProductFromStore(String userID, String storeID, String productID)
        {
            bool output = !facade.RemoveProductFromStore(userID, storeID, productID).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(" user: " + userID + " product: " + productID + " has been removed from the shop : " + storeID);
            else
                Logger.GetInstance().Error(" user: " + userID + " product: " + productID + " has not been removed from the shop : " + storeID);
            return output;
        }

        [HttpGet]
        public String OpenShop(string storeName, string userId)
        {
            Result<StoreService> output = facade.OpenNewStore(storeName, userId);
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
            Result output = facade.CloseStore(storeId, userId);
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
            bool output = !facade.AddStoreOwner(newOwnerId, currentOwnerId, storeId).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(currentOwnerId + " has make new owner for " + storeId);
            else
                Logger.GetInstance().Error(currentOwnerId + " could not make new owner for " + storeId);
            return output;
        }

        [HttpGet]
        public bool AddStoreManager(string storeId, string currentOwnerId, string newManagerId, LinkedList<int> permissions)
        {
            bool output = !facade.AddStoreManager(newManagerId, currentOwnerId, storeId).ErrorOccured;
            output = output && !facade.SetPermissions(storeId, newManagerId, currentOwnerId, permissions).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(currentOwnerId + " has make new manager " + newManagerId + " for " + storeId);
            else
                Logger.GetInstance().Error(currentOwnerId + " could not make new manager for " + storeId);
            return output;
        }

        [HttpGet]
        public bool removeManager(string currentOwnerId, string storeId, string ManagerToRemove)
        {
            bool output = !facade.RemoveStoreManager(ManagerToRemove, currentOwnerId, storeId).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(currentOwnerId + " has has removed manager" + ManagerToRemove + "form store: " + storeId);
            else
                Logger.GetInstance().Error(currentOwnerId + " could not removed manager for " + storeId);
            return output;
        }

        public bool removeOwner(string currentOwnerId, string storeId, string OwnerToRemove)
        {
            bool output = !facade.RemoveStoreOwner(OwnerToRemove, currentOwnerId, storeId).ErrorOccured;
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
            parameters["Name"] = keyword;
            Result<List<ProductService>> results = facade.SearchProduct(parameters);
            if (results.ErrorOccured)
            {
                return new string[][] { new string[] { $"Error:{results.ErrorMessage}" } };
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
            Result<List<StoreService>> results = facade.SearchStore(parameters);
            if (results.ErrorOccured)
            {
                return new string[][] { new string[] { $"Error:{results.ErrorMessage}" } };
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
            Result<ShoppingCartSO> output = facade.GetUserShoppingCart(userid);
            if(output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return new string[][] { new string[] { $"Error:{output.ErrorMessage}" } };
            }
            return output.Value.toArray();
        }

        [HttpGet]
        public string[][] getUserPurchaseHistory(string userid)
        {
            Result<UserHistorySO> output = facade.GetUserPurchaseHistory(userid);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return new string[][] { new string[] { $"Error:{output.ErrorMessage}" } };
            }
            return output.Value.toArray();
        }

        [HttpGet]
        public bool reOpenStore(string storeid, string userid)
        {
            Result<StoreService> output = facade.ReOpenStore(storeid,userid);
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
            Result output = facade.EditProductDetails(userID, storeID, productID, details);
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
            Result output = facade.SetPermissions(storeID, managerID, ownerID, permissions);
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
            Result output = facade.RemovePermissions(storeID, managerID, ownerID, permissions);
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
            Result<Dictionary<IStaffService, PermissionService>> output = facade.GetStoreStaff(ownerID, storeID);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return new string[] {$"Error:{output.ErrorMessage}"};
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
            Result<bool> output = facade.AddStoreRating(userid, storeid, rate);
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
            Result<bool> output = facade.AddProductRatingInStore(userid, storeid, productid, rate);
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
            List<StoreService> output = facade.GetAllStoresToDisplay();
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
            List<ProductService> output = facade.GetAllProductByStoreIDToDisplay(storeID);
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
            Boolean[] output = facade.GetPermission(userID,storeID);
            return output;
        }

        [HttpGet]
        public String getTotalShoppingCartPrice(string userid)
        {
            Result<double> output = facade.GetTotalShoppingCartPrice(userid);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return "Error:" + output.ErrorMessage;
            }
            Logger.GetInstance().Event("Total shopping cart price fetched");
            return output.Value.ToString();
        }
        
        // system admin func
        [HttpGet]
        public String[][] GetUserPurchaseHistory(string admin , string userid)
        {
            Result<UserHistorySO> output = facade.GetUserPurchaseHistory(admin,userid);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return new string[][] { new string[] { $"Error:{output.ErrorMessage}" } };
            }
            Logger.GetInstance().Event("User purchase history fetched");
            return output.Value.toArray();
        }

        [HttpGet]
        public string AddSystemAdmin(string admin, string email)
        {
            Result<RegisteredUserSO> output = facade.AddSystemAdmin(admin,email);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return "Error" + output.ErrorMessage;
            }
            Logger.GetInstance().Event("system admin added");
            return output.Value.UserName;
        }

        [HttpGet]
        public string RemoveSystemAdmin(string admin, string email)
        {
            Result<RegisteredUserSO> output = facade.RemoveSystemAdmin(admin,email);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return "Error" + output.ErrorMessage;
            }
            Logger.GetInstance().Event("system admin removed");
            return output.Value.UserName;
        }
        
        [HttpGet]
        public bool ResetSystem(string admin)
        {
            Result<bool> output = facade.ResetSystem(admin);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event("system resetted");
            return output.Value;
        }
        [HttpGet]
        public bool isAdminUser(string userid)
        {
            Result<bool> output = facade.isAdminUser(userid);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event($"{userid} is admin");
            return output.Value;
        }

        [HttpGet]
        public bool isRegisteredUser(string userid)
        {
            Result<bool> output = facade.isRegisteredUser(userid);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return false;
            }
            Logger.GetInstance().Event($"{userid} is reg user");
            return output.Value;
        }


        [HttpGet]
        public String getProductId(string storeid, string productname)
        {
            Result<String> output = facade.getProductId(storeid,productname);
            if (output.ErrorOccured)
            {
                Logger.GetInstance().Error(output.ErrorMessage);
                return "Error:" + output.ErrorMessage;
            }
            Logger.GetInstance().Event($"{productname} is not in store {storeid}");
            return output.Value;
        }

        [HttpGet]
        public String[][] getStoresIManage(string userid)
        {
            List<StoreService> output = facade.GetStoresIManage(userid);
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
        public String[][] getStoresIOwn(string userid)
        {
            List<StoreService> output = facade.GetStoresIOwn(userid);
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
        public String[][] getAllProductsInSystem()
        {
            List<ProductService> products = facade.GetAllProducts();
            List<string[]> output = new List<string[]>();
            foreach (ProductService product in products)
            {
                output.Add(product.ToStringArray());
            }
            return output.ToArray();
        }
        // offers ?
        // policy func ? to the end









    }
}
