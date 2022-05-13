using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Req II.2.4
namespace eCommerceAcceptanceTests.UserTests.PurchaseOpsGuestUser
{
    public class UpadeShoppingCart : XeCommerceTestCase
    {
        public String user_id { set; get; }
        public String store_id { set; get; }
        public String product_id { set; get; }
        public String buyer_id { set; get; }
        public UpadeShoppingCart() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.user_id = userId.Value;
            Result<String> productId = this.api.AddProductToStore(user_id, store_id, "CODBO2", 300, 10, "VideoGames");
            this.product_id = productId.Value;
            this.buyer_id = api.Login().Value;
            
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyUpdateShoppingCart()
        {
            api.AddProductToCart(buyer_id, product_id, 5, store_id);
            bool update_success = api.UpdateShoppingCart(buyer_id, store_id, product_id, 2).Value;
            Assert.True(update_success);
            List<String> shopping_bags = api.GetUserShoppingCart(buyer_id).Value;
            Dictionary<String, int> shopping_bag = api.GetUserShoppingBag(buyer_id, store_id).Value;
            Assert.True(shopping_bag.Count == 1); // check number of added products in shopping bag
            int PotentialQuantity;
            bool quanbool = shopping_bag.TryGetValue(product_id, out PotentialQuantity);
            Assert.True(quanbool);
            Assert.True(PotentialQuantity == 2); // check quantity in shopping bad
        }

    }
}
