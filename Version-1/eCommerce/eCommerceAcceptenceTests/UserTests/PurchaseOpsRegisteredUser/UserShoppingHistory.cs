using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Req II.3.6
namespace eCommerceAcceptanceTests.UserTests.PurchaseOpsRegisteredUser
{
    public class UserShoppingHistory : XeCommerceTestCase
    {
        public String user_id { set; get; }
        public String store_id { set; get; }
        public String product_id { set; get; }
        public String buyer_id { set; get; }
        public UserShoppingHistory() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.user_id = userId.Value;
            Result<String> productId = this.api.AddProductToStore(user_id, store_id, "CODBO2", 300, 10, "VideoGames");
            this.product_id = productId.Value;
            api.Register("buyer@gmail.com", "BuyerPass");
            Result<String> resBuyer = api.Login("buyer@gmail.com", "BuyerPass");
            this.buyer_id = resBuyer.Value;
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyUserShoppingHistory() 
        {
            Result resPro = api.AddProductToCart(buyer_id, product_id, 5, store_id);
            Assert.False(resPro.ErrorOccured);
            Result purchase = api.Purchase(buyer_id, new Dictionary<String, object>(), new Dictionary<String, object>());
            Assert.False(purchase.ErrorOccured);
            Result<List<String>> history = api.GetUserPurchaseHistory(buyer_id);
            Assert.False(history.ErrorOccured);
            Assert.True(history.Value.Count == 1);
            Result<Dictionary<String,int>> historyPro = api.GetUserPurchaseHistoryProducts(buyer_id,history.Value.ElementAt(0));
            Assert.True(historyPro.ErrorOccured == false);
            Dictionary<String, int> products = historyPro.Value;
            Assert.True(products.ContainsKey("CODBO2"));
            Assert.True(products["CODBO2"] == 5);
            Assert.True(products.Count == 1);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadUserShoppingHistory_InvalidShoppingBagId()
        {
            Result resPro = api.AddProductToCart(buyer_id, product_id, 5, store_id);
            Assert.False(resPro.ErrorOccured);
            Result purchase = api.Purchase(buyer_id, new Dictionary<String, object>(), new Dictionary<String, object>());
            Assert.False(purchase.ErrorOccured);
            Result<List<String>> history = api.GetUserPurchaseHistory(buyer_id);
            Assert.False(history.ErrorOccured);
            Assert.True(history.Value.Count == 1);
            Result<Dictionary<String, int>> historyPro = api.GetUserPurchaseHistoryProducts(buyer_id, "random_id");
            Assert.True(historyPro.ErrorOccured);

        }
    }
}
