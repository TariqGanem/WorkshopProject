using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceIntegrationTests.Utils
{
    public class SystemInterfaceProxy: ISystemInterface
    {
        public ISystemInterface Real { get; set; }

        public Result<Boolean> AddProductToCart(string userID, string ProductID, int ProductQuantity, string StoreID)
        {
            if (Real == null)
                return new Result<Boolean>(true);

            return Real.AddProductToCart(userID, ProductID, ProductQuantity, StoreID);
        }

        public Result<string> AddProductToStore(string userID, string storeID, string productName, double price, int initialQuantity, string category)
        {
            if (Real == null)
                return new Result<string>(null);

            return Real.AddProductToStore(userID, storeID, productName, price, initialQuantity, category);
        }

        public Result<bool> AddStoreManager(string addedManagerID, string currentlyOwnerID, string storeID)
        {
            if (Real == null)
                return new Result<bool>(true);

            return Real.AddStoreManager(addedManagerID, currentlyOwnerID, storeID);
        }

        public Result<bool> AddStoreOwner(string addedOwnerID, string currentlyOwnerID, string storeID)
        {
            if (Real == null)
                return new Result<bool>(true);

            return Real.AddStoreOwner(addedOwnerID, currentlyOwnerID, storeID);
        }

        public Result<bool> EditProductDetails(string userID, string storeID, string productID, IDictionary<string, object> details)
        {
            if (Real == null)
                return new Result<bool>(null);

            return Real.EditProductDetails(userID, storeID, productID, details);
        }

        public Result<List<String>> GetStorePurchaseHistory(string ownerID, string storeID)
        {
            if (Real == null)
                return new Result<List<String>>(null, null);

            return Real.GetStorePurchaseHistory(ownerID, storeID);
        }

        public Result<Dictionary<String, int>> GetUserShoppingBag(String userID, String shoppingBagID)
        {
            if (Real == null)
                return new Result<Dictionary<String, int>>(null, null);

            return Real.GetUserShoppingBag(userID, shoppingBagID);
        }

        public Result<Dictionary<String, List<int>>> GetStoreStaff(string ownerID, string storeID)
        {
            if (Real == null)
                return new Result<Dictionary<string, List<int>>>(null, null);

            return Real.GetStoreStaff(ownerID, storeID);
        }

        public Result<double> GetTotalShoppingCartPrice(string userID)
        {
            if (Real == null)
                return new Result<double>(0.0);

            return Real.GetTotalShoppingCartPrice(userID);
        }

        public Result<List<String>> GetUserPurchaseHistory(string userID)
        {
            if (Real == null)
                return new Result<List<String>>(null, null);

            return Real.GetUserPurchaseHistory(userID);
        }

        public Result<List<String>> GetUserShoppingCart(string userID)
        {
            if (Real == null)
                return new Result<List<String>>(null, null);

            return Real.GetUserShoppingCart(userID);
        }

        public Result<String> Login(string email, string password)
        {
            if (Real == null)
                return new Result<string>(null);

            return Real.Login(email, password);
        }

        public Result<String> Login() // added while writing acceptance testing
        {
            if (Real == null)
                return new Result<string>(null);

            return Real.Login();
        }

        public Result<bool> Logout(string email)
        {
            if (Real == null)
                return new Result<bool>(true);

            return Real.Logout(email);
        }

        public Result<String> OpenNewStore(string storeName, string userID)
        {
            if (Real == null)
                return new Result<string>(null);

            return Real.OpenNewStore(storeName, userID);
        }

        public Result<List<String>> Purchase(string userID, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            if (Real == null)
                return new Result<List<String>>(null, null);

            return Real.Purchase(userID, paymentDetails, deliveryDetails);
        }

        public Result<bool> Register(string email, string password)
        {
            if (Real == null)
                return new Result<bool>(true);

            return Real.Register(email, password);
        }

        public Result<bool> RemoveProductFromStore(string userID, string storeID, string productID)
        {
            if (Real == null)
                return new Result<bool>(true);

            return Real.RemoveProductFromStore(userID, storeID, productID);
        }

        public Result<List<string>> SearchProduct(IDictionary<string, object> productDetails)
        {
            if (Real == null)
                return new Result<List<string>>(null, null);

            return Real.SearchProduct(productDetails);
        }

        public Result<bool> SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions)
        {
            if (Real == null)
                return new Result<bool>(true);

            return Real.SetPermissions(storeID, managerID, ownerID, permissions);
        }

        public Result<bool> UpdateShoppingCart(string userID, string storeId, string productID, int quantity)
        {
            if (Real == null)
                return new Result<bool>(true);

            return Real.UpdateShoppingCart(userID, storeId, productID, quantity);
        }

        public Result<string> AddSystemAdmin(string sysAdminId, string userName)
        {
            if (Real == null)
                return new Result<string>(null);

            return Real.AddSystemAdmin(sysAdminId, userName);
        }
        
        public Result<string> RemoveSystemAdmin(string sysAdminId, string userName)
        {
            if (Real == null)
                return new Result<string>(null);

            return Real.RemoveSystemAdmin(sysAdminId, userName);
        }

        public Result<bool> CloseStore(string userID, string storeName)
        {
            if (Real == null)
                return new Result<bool>(true);

            return Real.CloseStore(userID, storeName);
        }
        public Result<Dictionary<String, int>> GetUserPurchaseHistoryProducts(string userId, String shoppingbagId)
        {
            if (Real == null)
                return new Result<Dictionary<String, int>>(null, null);

            return Real.GetUserPurchaseHistoryProducts(userId, shoppingbagId);
        }



    }
}
