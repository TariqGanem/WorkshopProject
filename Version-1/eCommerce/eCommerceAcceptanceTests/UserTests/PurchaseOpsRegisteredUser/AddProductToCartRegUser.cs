using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
// Req II.3(2.3)
namespace eCommerceAcceptanceTests.UserTests.PurchaseOpsRegisteredUser
{
    public class AddProductsToCartRegUser : XeCommerceTestCase
    {
        public String user_id { set; get; }
        public String store_id { set; get; }
        public String product_id { set; get; }
        public AddProductsToCartRegUser() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.user_id = userId.Value;
            Result<String> productId = this.api.AddProductToStore(user_id, store_id, "CODBO2", 300, 10, "VideoGames");
            this.product_id = productId.Value;
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyAddProductToCart_RegularAddToCart()
        {
            this.api.Register("Buyer@gmail.com", "StrongPassword");
            Result<String> buyer = this.api.Login("Buyer@gmail.com", "StrongPassword"); // Reg User
            String buyer_id = buyer.Value;
            this.api.AddProductToCart(buyer_id, product_id, 10, store_id);
            Result<List<String>> buyer_shopping_bags = this.api.GetUserShoppingCart(buyer_id);
            Assert.True(buyer_shopping_bags.Value.Count == 1);
            Dictionary<String, int> shopping_bag = api.GetUserShoppingBag(buyer_id, store_id).Value;
            Assert.True(shopping_bag.Count == 1); // check number of added products in shopping bag
            int PotentialQuantity;
            shopping_bag.TryGetValue(product_id, out PotentialQuantity);
            Assert.True(shopping_bag.ContainsKey(product_id)); // check productId in shopping bag
            Assert.True(PotentialQuantity == 10); // check quantity in shopping bad
            Assert.True(buyer_shopping_bags.Value.Count == 1);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadAddProductToCart_ExceedingQuantity()
        {
            this.api.Register("Buyer@gmail.com", "StrongPassword");
            Result<String> buyer = this.api.Login("Buyer@gmail.com", "StrongPassword"); // Reg User
            String buyer_id = buyer.Value;
            Result<bool> addproductRes = this.api.AddProductToCart(buyer_id, product_id, 11, store_id);
            //Assert.True(addproductRes.ErrorOccured);
            Assert.True(this.api.GetUserShoppingCart(buyer_id).Value.Count == 0);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadAddProductToCart_BadBagId()
        {
            Result<String> buyer = this.api.Login("Buyer@gmail.com", "StrongPassword"); // Reg User
            String buyer_id = buyer.Value;
            this.api.AddProductToCart(buyer_id, product_id, 10, store_id);
            Result<List<String>> buyer_shopping_bags = this.api.GetUserShoppingCart(user_id);
            Assert.True(buyer_shopping_bags.Value.Count == 0);
        }



    }
}
