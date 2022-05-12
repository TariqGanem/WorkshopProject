using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.src.ServiceLayer
{
    public class RealAdapter : ISystemInterface
    {
        public eCommerceSystem system = new eCommerceSystem();

        public Result<bool> AddProductToCart(string userId, string productId, int quantity, string storeId)
        {
            Result res = system.AddProductToCart(userId, productId, quantity, storeId);
            if (res.ErrorOccured)
                return new Result<bool>(res.ErrorMessage);
            return new Result<bool>(true);
        }

        public Result<string> AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category)
        {
            Result<string> res = system.AddProductToStore(userID, storeID, productName, price, initialQuantity, category);
            if (res.ErrorOccured)
                return new Result<string>(res.ErrorMessage);
            return new Result<string>(res.Value, null);
        }

        public Result<bool> AddStoreManager(string addedManagerID, string currentlyOwnerID, string storeID)
        {
            Result res = system.AddStoreManager(addedManagerID, currentlyOwnerID, storeID);
            if (res.ErrorOccured)
                return new Result<bool>(res.ErrorMessage);
            return new Result<bool>(true);
        }

        public Result<bool> AddStoreOwner(string addedOwnerID, string currentlyOwnerID, string storeID)
        {
            Result res = system.AddStoreOwner(addedOwnerID, currentlyOwnerID, storeID);
            if (res.ErrorOccured)
                return new Result<bool>(res.ErrorMessage);
            return new Result<bool>(true);
        }

        public Result<bool> EditProductDetails(string userID, string storeID, string productID, IDictionary<string, object> details)
        {
            Result res = system.EditProductDetails(userID, storeID, productID, details);
            if (res.ErrorOccured)
                return new Result<bool>(res.ErrorMessage);
            return new Result<bool>(true);
        }

        public Result<List<string>> GetStorePurchaseHistory(string sysAdminId, string storeId)
        {
            Result<UserHistorySO> res = system.GetStorePurchaseHistory(sysAdminId, storeId);
            if (!res.ErrorOccured)
            {
                List<ShoppingBagSO> ShoppingBags = new List<ShoppingBagSO>(res.Value.ShoppingBags);
                List<string> Ids = new List<string>();
                foreach (ShoppingBagSO bag in ShoppingBags) { Ids.Add(bag.Id); }
                return new Result<List<string>>(Ids);
            }
            else
            {
                return new Result<List<string>>(new List<string>(), res.ErrorMessage);
            }
        }

        public Result<Dictionary<String, List<int>>> GetStoreStaff(string ownerID, string storeID)
        {
            Result<Dictionary<IStaffService, PermissionService>> res =  system.GetStoreStaff(ownerID, storeID);
            if (!res.ErrorOccured)
            {
                List<IStaffService> keys = new List<IStaffService>();
                List<PermissionService> values = new List<PermissionService>();
                foreach (IStaffService key in res.Value.Keys)
                {
                    keys.Add(key);
                    values.Add(res.Value[key]);
                }
                List<string> userIDS = new List<string>();
                foreach (IStaffService item in keys) { userIDS.Add(item.Id); }
                List<List<int>> userPermisions = new List<List<int>>();
                foreach (PermissionService permission in values)
                {
                    List<int> permissionList = new List<int>();
                    if (permission.isOwner) { permissionList.Add((int)Methods.AllPermissions); }
                    else
                    {
                        for (int i = 0; i < permission.functionsBitMask.Length; i++)
                        {
                            if (permission.functionsBitMask[i]) { permissionList.Add(i); }
                        }
                    }
                    userPermisions.Add(permissionList);
                }
                Dictionary<string, List<int>> dic = new Dictionary<string, List<int>>();
                dic = userIDS.Zip(userPermisions, (k, v) => new { k, v })
                                        .ToDictionary(x => x.k, x => x.v);
                return new Result<Dictionary<string, List<int>>>(dic);
            }
            else
            {
                return new Result<Dictionary<string, List<int>>>(res.ErrorMessage);
            }
        }
        
        public Result<double> GetTotalShoppingCartPrice(string userId)
        {
            Result<double> res = system.GetTotalShoppingCartPrice(userId);
            if (res.ErrorOccured)
                return new Result<double>(res.ErrorMessage);
            else
                return new Result<double>(res.Value);
        }

        public Result<List<string>> GetUserPurchaseHistory(string userId)
        {
            Result<UserHistorySO> res = system.GetUserPurchaseHistory(userId);
            if (!res.ErrorOccured)
            {
                List<ShoppingBagSO> ShoppingBags = new List<ShoppingBagSO>(res.Value.ShoppingBags);
                List<string> Ids = new List<string>();
                foreach (ShoppingBagSO bag in ShoppingBags) { Ids.Add(bag.Id); }
                return new Result<List<string>>(Ids);
            }
            else
            {
                return new Result<List<string>>(res.ErrorMessage);
            }
        }

        public Result<string> AddSystemAdmin(string sysAdminId, string userName)
        {
            Result<RegisteredUserSO> res = system.AddSystemAdmin(sysAdminId, userName);
            if (!res.ErrorOccured)
                return new Result<string>(res.Value.Id, null);
            else
                return new Result<string>(res.ErrorMessage);
        }

        public Result<bool> CloseStore(string userId, string storeId)
        {
            Result res = system.CloseStore(userId, storeId);
            if (!res.ErrorOccured)
                return new Result<bool>(false);
            else
                return new Result<bool>(res.ErrorMessage);
        }

        public Result<List<String>> GetUserShoppingCart(String userID)
        {
            Result<ShoppingCartSO> res = system.GetUserShoppingCart(userID);
            if (!res.ErrorOccured)
            {
                List<ShoppingBagSO> ShoppingBags = new List<ShoppingBagSO>(res.Value.shoppingBags.Values);
                List<string> Ids = new List<string>();
                foreach (ShoppingBagSO item in ShoppingBags) { Ids.Add(item.Id); }
                return new Result<List<string>>(Ids);
            }
            else
            {
                return new Result<List<string>>(res.ErrorMessage);
            }
        }

        public Result<Dictionary<string, int>> GetUserShoppingBag(String userID, String shoppingBagID)
        {
            Result<ShoppingCartSO> res = system.GetUserShoppingCart(userID);
            if (!res.ErrorOccured)
            {
                ShoppingCartSO shoppingCart = res.Value;
                foreach (string id in shoppingCart.shoppingBags.Keys)
                {
                    if (id == shoppingBagID)
                    {
                        if(shoppingCart.shoppingBags.TryGetValue(id, out ShoppingBagSO bag))
                        {
                            return new Result<Dictionary<string, int>>(ConvertObjectToID(bag.Products));
                        }
                    }
                }
            }
            return new Result<Dictionary<string, int>>("Failed to find the shopping bag");
        }

        private Dictionary<string, int> ConvertObjectToID(Dictionary<ProductService, int> dct)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach (ProductService p in dct.Keys)
            {
                result.Add(p.Id, p.Quantity);
            }
            return result;
        }
        
        public Result<string> Login(string userName, string password)
        {
            Result<RegisteredUserSO> res = system.Login(userName, password);
            if (!res.ErrorOccured)
                return new Result<string>(res.Value.Id, null);
            else
                return new Result<string>(res.ErrorMessage);
        }

        public Result<string> Login() // added while writing acceptance testing
        {
            Result<GuestUserSO> res = system.Login();
            if (!res.ErrorOccured)
                return new Result<string>(res.Value.Id, null);
            else
                return new Result<string>(res.ErrorMessage);
        }

        public Result<bool> Logout(string userName)
        {
            Result res = system.Logout(userName);
            if (!res.ErrorOccured)
                return new Result<bool>(false);
            else
                return new Result<bool>(res.ErrorMessage);
        }

        public Result<string> OpenNewStore(string storeName, string userId)
        {
            Result<StoreService> res = system.OpenNewStore(storeName, userId);
            if (res.ErrorOccured)
                return new Result<string>(res.ErrorMessage);
            else
                return new Result<string>(res.Value.Id, null);
        }

        public Result<List<String>> Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            Result<ShoppingCartSO> res = system.Purchase(userId, paymentDetails, deliveryDetails);
            if (!res.ErrorOccured)
            {
                List<string> bagsIDS = new List<string>();
                foreach (ShoppingBagSO bag in res.Value.shoppingBags.Values) { bagsIDS.Add(bag.Id); }
                return new Result<List<string>>(bagsIDS);
            }
            else
            {
                return new Result<List<string>>(res.ErrorMessage);
            }
        }

        public Result<bool> Register(string username, string password)
        {
            Result<RegisteredUserSO> res = system.Register(username, password);
            if (!res.ErrorOccured)
                return new Result<bool>(true);
            return new Result<bool>(res.ErrorMessage);
        }

        public Result<bool> RemovePermissions(string storeID, string managerID, string ownerID, LinkedList<int> permissions)
        {
            Result res = system.RemovePermissions(storeID, managerID, ownerID, permissions);
            if (!res.ErrorOccured)
                return new Result<bool>(true);
            return new Result<bool>(res.ErrorMessage);
        }

        public Result<bool> RemoveProductFromStore(string userID, string storeID, string productID)
        {
            Result res = system.RemoveProductFromStore(userID, storeID, productID);
            if (!res.ErrorOccured)
                return new Result<bool>(true);
            return new Result<bool>(res.ErrorMessage);
        }

        public Result<bool> RemoveStoreManager(string removedManagerID, string currentlyOwnerID, string storeID)
        {
            Result res = system.RemoveStoreManager(removedManagerID, currentlyOwnerID, storeID);
            if (!res.ErrorOccured)
                return new Result<bool>(true);
            return new Result<bool>(res.ErrorMessage);
        }

        public Result<string> RemoveSystemAdmin(string sysAdminId, string userName)
        {
            Result<RegisteredUserSO> res = system.RemoveSystemAdmin(sysAdminId, userName);
            if (!res.ErrorOccured)
                return new Result<string>(res.Value.Id, null);
            return new Result<string>(res.ErrorMessage);
        }

        public Result<List<string>> SearchProduct(IDictionary<string, object> productDetails)
        {
            Result<List<ProductService>> res = system.SearchProduct(productDetails);
            if (!res.ErrorOccured)
            {
                List<string> productIDS = new List<string>();
                foreach (ProductService p in res.Value) { productIDS.Add(p.Id); }
                return new Result<List<string>>(productIDS);
            }
            else
            {
                return new Result<List<string>>(res.ErrorMessage);
            }
        }

        public Result<bool> SetPermissions(string storeID, string managerID, string ownerID, LinkedList<int> permissions)
        {
            Result res = system.SetPermissions(storeID, managerID, ownerID, permissions);
            if (!res.ErrorOccured)
                return new Result<bool>(true);
            else
                return new Result<bool>(res.ErrorMessage);
        }

        public Result<bool> UpdateShoppingCart(string userId, string storeId, string productId, int quantity)
        {
            Result res = system.UpdateShoppingCart(userId, storeId, productId, quantity);
            if (!res.ErrorOccured)
                return new Result<bool>(true);
            else
                return new Result<bool>(res.ErrorMessage);
        }
    }
}
