using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eCommerceAcceptanceTests.StoreTests.StoreOwnerOps
{
    public class StoreHistory : XeCommerceTestCase
    {
        public String store_owner { set; get; }
        public String store_id { set; get; }
        public String product_id { set; get; }
        public String buyer_id { set; get; }
        public StoreHistory() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.store_owner = userId.Value;
            Result<String> productId = this.api.AddProductToStore(store_owner, store_id, "CODBO2", 300, 10, "VideoGames");
            this.product_id = productId.Value;
            this.api.Register("Buyer@gmail.com", "StrongPassword");
            Result<String> buyer = this.api.Login("Buyer@gmail.com", "StrongPassword"); // Reg User
            this.buyer_id = buyer.Value;
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyStoreHistory()
        {
            Dictionary<String, object> PaymentDetails = new Dictionary<string, object>();
            Dictionary<String, object> DeliveryDetails = new Dictionary<string, object>();
            api.AddProductToCart(buyer_id, product_id, 5, store_id);
            Result<List<String>> purchaseRes = api.Purchase(buyer_id, PaymentDetails, DeliveryDetails);
            Result<List<String>> StoreHistory = api.GetStorePurchaseHistory(store_owner, store_id);
            Assert.True(StoreHistory.ErrorOccured == false && StoreHistory.Value.Count == 1);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadStoreHistory_gettingStoreHistoryWithNoPermission()
        {
            Dictionary<String, object> PaymentDetails = new Dictionary<string, object>();
            Dictionary<String, object> DeliveryDetails = new Dictionary<string, object>();
            api.AddProductToCart(buyer_id, product_id, 5, store_id);
            Result<List<String>> purchaseRes = api.Purchase(buyer_id, PaymentDetails, DeliveryDetails);
            Result<List<String>> StoreHistory = api.GetStorePurchaseHistory(buyer_id, store_id);
            Assert.True(StoreHistory.ErrorOccured);
        }
    }
}
