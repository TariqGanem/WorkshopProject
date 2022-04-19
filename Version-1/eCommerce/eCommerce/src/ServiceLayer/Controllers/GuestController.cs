using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public interface IGuestController
    {
        Response<guestUser> EnterSystem();
        void ExitSystem(String userID);
        Response<registeredUser> Register(string email, string password);
        Response<> SearchStore(IDictionary<String, Object> details);
        Response<> SearchProduct(IDictionary<String, Object> productDetails);
        Response<Boolean> AddProductToCart(String userID, String ProductID, int ProductQuantity, String StoreID);
        Response<> GetUserShoppingCart(String userID);
        Response<Boolean> UpdateShoppingCart(String userID, String shoppingBagID, String productID, int quantity);
        Response<> Purchase(String userID, IDictionary<String, Object> paymentDetails, IDictionary<String, Object> deliveryDetails);
        Response<> GetUserPurchaseHistory(String userID);
        Response<double> GetTotalShoppingCartPrice(String userID);
        Response<List<Tuple<String, String>>> GetProductReview(String storeID, String productID);
    }
    public class GuestController : IGuestController
    {
        protected ISystemFacade SystemFacade;

        public GuestController(ISystemFacade systemFacade)
        {
            SystemFacade = systemFacade;
        }

    }
}
