using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eCommerceAcceptanceTests.UserTests.PurchaseOpsGuestUser
{
    public class StoresProductsInfo : XeCommerceTestCase
    {
        public String user_id { set; get; }
        public String store_id { set; get; }
        public StoresProductsInfo () : base() {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.user_id = userId.Value;
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyProductSearch_SearchOnName()
        {
            this.api.AddProductToStore(user_id, store_id, "CODBO2", 300, 10, "VideoGames");
            Assert.False(api.SearchProduct(new Dictionary<String, Object>() { { "Name", "CODBO2" } }).ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyProductSearch_SearchOnPrice()
        {
            this.api.AddProductToStore(user_id, store_id, "CODBO2", 300, 10, "VideoGames");            
            Assert.False(api.SearchProduct(new Dictionary<String, Object>() { { "Price",300 } }).ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadProductSearch_PorductNameDoesNotExist()
        {
            Assert.True(api.SearchProduct(new Dictionary<String, Object>() { { "Name", "test" } }).ErrorOccured);
        }

        // ADD STORE SEARCH ACCEPTANCE TESTS - WHEN SEARCH STORE FUNCTIONALITY IS ADDED


    }
}
