using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Req II.3.1
namespace eCommerceAcceptanceTests.UserTests.PurchaseOpsRegisteredUser
{
    public class LogoutRegUser : XeCommerceTestCase
    {
        public String user_id { set; get; }
        public String store_id { set; get; }
        public String product_id { set; get; }
        public String buyer_id { set; get; }
        public LogoutRegUser() : base()
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
        public void HappyLogout()
        {
            Result<bool> logoutRes = api.Logout(this.buyer_id); // Regular Logout of Reg User -> Guest User
            Assert.True(!logoutRes.Value);
            Result<bool> addProduct = api.AddProductToCart(buyer_id, product_id, 5, store_id); // add Product to Guest User
            Assert.True(addProduct.Value);
            Result<List<String>> bags = api.GetUserShoppingCart(buyer_id);
            Assert.True(bags.Value.Count == 1);
            this.api.Login("Buyer@gmail.com", "StrongPassword"); // Login again
            bags = api.GetUserShoppingCart(buyer_id); // Check if Shopping Cart was saved after logging in
            Assert.True(bags.Value.Count == 1);

        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadLogout_BadUserId()
        {
            Result<bool> logoutRes = api.Logout("RandomUserId"); // Regular Logout of Reg User -> Guest User
            Assert.True(logoutRes.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadLogout_LogoutTwice()
        {
            Result<bool> logoutRes = api.Logout(buyer_id); // Regular Logout of Reg User -> Guest User
            Assert.True(!logoutRes.Value); // logout succeeded 
            logoutRes = api.Logout(buyer_id); // Regular Logout of Reg User -> Guest User
            Assert.True(logoutRes.ErrorOccured);
        }

    }
}
