using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Req II.3(2.5)
namespace eCommerceAcceptanceTests.UserTests.PurchaseOpsRegisteredUser
{
    public class PurchaseShoppingCartRegUser : XeCommerceTestCase
    {
        public String user_id { set; get; }
        public String store_id { set; get; }
        public String product_id { set; get; }
        public String buyer_id { set; get; }
        public PurchaseShoppingCartRegUser() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.user_id = userId.Value;
            Result<String> productId = this.api.AddProductToStore(user_id, store_id, "CODBO2", 300, 10, "VideoGames");
            this.product_id = productId.Value;
            this.api.Register("Buyer@gmail.com", "StrongPassword");
            Result<String> buyer = this.api.Login("Buyer@gmail.com", "StrongPassword"); // Reg User
            this.buyer_id = buyer.Value;
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyPurchaseCart()
        {
            Dictionary<String, object> PaymentDetails = new Dictionary<string, object>();
            Dictionary<String, object> DeliveryDetails = new Dictionary<string, object>();
            api.AddProductToCart(buyer_id, product_id, 5, store_id);
            Result<List<String>> purchaseRes = api.Purchase(buyer_id, PaymentDetails, DeliveryDetails);
            List<String> sbs = purchaseRes.Value;
            Result<List<String>> sbsAfterPurchase = api.GetUserShoppingCart(buyer_id);
            Assert.True(sbsAfterPurchase.Value.Count == 0);
            Result<List<String>> UserHistory = api.GetUserPurchaseHistory(buyer_id);
            Assert.True(!UserHistory.ErrorOccured && UserHistory.Value.Count == 1);
            Result<List<String>> StoreHistory = api.GetStorePurchaseHistory(user_id, store_id);
            Assert.True(StoreHistory.Value.Count == 1);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadPurchaseCart_PurchasingEmptyCart()
        {
            Dictionary<String, object> PaymentDetails = new Dictionary<string, object>();
            Dictionary<String, object> DeliveryDetails = new Dictionary<string, object>();
            Result<List<String>> purchaseRes = api.Purchase(buyer_id, PaymentDetails, DeliveryDetails);
            Assert.True(purchaseRes.ErrorOccured);
        }
    }
}
