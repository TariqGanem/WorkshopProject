using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Req II.4.9
// CHECK FUNCTIONALITY AGAIN!
namespace eCommerceAcceptanceTests.StoreTests.StoreOwnerOps
{
    public class CloseStore : XeCommerceTestCase
    {
        public String store_owner { set; get; }
        public String store_id { set; get; }
        public CloseStore() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.store_owner = userId.Value;
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyCloseStore()
        {
            Result<bool> closestore = api.CloseStore(store_owner, store_id);
            Assert.True(closestore.ErrorOccured == false); // closestore.Value = false -> store closed
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadCloseStore_OtherUserTryingToCloseStore()
        {
            Result<bool> reguserIdRand = this.api.Register("Random@gmail.com", "StringPassword");
            Result<String> randomUser = this.api.Login("Random@gmail.com", "StringPassword");
            Result<bool> closestore = api.CloseStore(randomUser.Value, store_id);
            Assert.True(closestore.ErrorOccured);
        }
    }
}
