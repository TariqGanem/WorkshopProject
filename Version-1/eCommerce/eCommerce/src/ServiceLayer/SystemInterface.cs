using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer
{
    public interface ISystemInterface
    {
        #region User related operations
        Result<Boolean> Register(String email, String password);

        Result<String> Login(String email, String password); //user id

        Result<Boolean> Logout(String email);

        Result<List<String>> SearchProduct(IDictionary<String, Object> productDetails); 

        Result<Boolean> AddProductToCart(String userID, String productID, int productQuantity, String storeID);

        Result<List<String>> GetUserShoppingCart(String userID); //LinkedList<ShoppingBagID> 

        Result<Dictionary<string, int>> GetUserShoppingBag(String userID, String shoppingBagID); //Dictionary<pid , countity>

        Result<Boolean> UpdateShoppingCart(String userID, String storeId, String productID, int quantity);

        Result<List<String>> Purchase(String userID, IDictionary<String, Object> paymentDetails, IDictionary<String, Object> deliveryDetails); //List<ShoppingBagID>

        Result<List<String>> GetUserPurchaseHistory(String userID); //List<shoppingBagID>

        Result<double> GetTotalShoppingCartPrice(String userID);

        Result<string> AddSystemAdmin(string sysAdminId, string userName);

        Result<string> RemoveSystemAdmin(string sysAdminId, string userName);

        #endregion

        #region Store related operations
        Result<String> OpenNewStore(String storeName, String userID); //store id
        Result<Boolean> CloseStore(String userID, String storeName);
        Result<string> AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category); //product id
        Result<Boolean> RemoveProductFromStore(String userID, String storeID, String productID);
        Result<Boolean> EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details); //TODO
        Result<Boolean> AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID);
        Result<Boolean> AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID);
        Result<Boolean> SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Result<Dictionary<String, List<int>>> GetStoreStaff(String ownerID, String storeID);
        Result<List<String>> GetStorePurchaseHistory(String ownerID, String storeID); //userID to List<permissions>

        #endregion
    }
}
