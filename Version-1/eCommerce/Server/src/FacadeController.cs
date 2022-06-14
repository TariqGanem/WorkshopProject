using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using ServerApi.src;
using Logger = ServerApi.src.Logger;

namespace Server.src
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    internal class FacadeController : ApiController
    {
        eCommerceSystem system = new eCommerceSystem();

        [HttpGet]
        public string Login()
        {
            string output = system.GuestLogin().Value.Id;
            Console.WriteLine($"sfds: {output}");
            Logger.GetInstance().Event("Guest has connected with pid : " + output);
            return output;
        }

        [HttpGet]
        public string Login(string username, string password)
        {
            Result< RegisteredUserSO> output = system.Login(username, password);
            if (!output.ErrorOccured)
                Logger.GetInstance().Event(username + "has LoggedIn ");
            return output.Value.Id;
        }

        [HttpGet]
        public bool Logout(string username)
        {
            bool output = !system.Logout(username).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(username + "has LoggedOut ");
            return output;
        }

        [HttpGet]
        public bool Register(string username, string password)
        {
            bool output = !system.Register(username, password).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(username + "has Register succesfully ");
            return output;
        }

        [HttpGet]
        public bool Purchase(string userName, string creditCard)
        {
            IDictionary<string, object> payments = new Dictionary<string, object>();
            payments.Add("visa", creditCard);
            IDictionary<string, object> deliverys = new Dictionary<string, object>();
            deliverys.Add("carShipping", "ups");
            bool output = !system.Purchase(userName, payments, deliverys).ErrorOccured;
            if (output)
                Logger.GetInstance().Event(userName + "has purchased succesfully ");
            return output;
        }

        [HttpGet]
        public bool UpdateCart(string userId, string storeId, string productId, int newAmount)
        {
            bool output = !system.UpdateShoppingCart(userId, storeId, productId, newAmount).ErrorOccured;
            if (output)
                Logger.GetInstance().Event("cart has updated succesfully ");
            return output;
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
            LinkedList<string> keywords = new LinkedList<string>();
            keywords.AddLast(userId);
            keywords.AddLast(storeId);
            keywords.AddLast(productName);
            keywords.AddLast(category);
            bool output = !system.AddProductToStore(userId, storeId, productName, price, quantity, category, keywords).ErrorOccured;
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
        public bool OpenShop(string storeName, string userId)
        {
            Logger.GetInstance().Event(userId + " has opened shop : " + storeName);
            return !system.OpenNewStore(storeName, userId).ErrorOccured;
        }

        [HttpGet]
        public bool CloseShop(string storeId, string userId)
        {
            Logger.GetInstance().Event(userId + " has closed shop : " + storeId);
            return !system.CloseStore(userId, storeId).ErrorOccured;
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


        [HttpGet]
        public string[][] Search(string keyword)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["keywords"] = keyword;
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
    }
}
