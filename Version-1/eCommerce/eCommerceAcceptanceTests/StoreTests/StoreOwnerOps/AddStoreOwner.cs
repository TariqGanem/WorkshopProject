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
    public class AddStoreOwner : XeCommerceTestCase // (Adding , Removing , Detail Editing) from products
    {
        public String store_owner { set; get; }
        public String store_id { set; get; }
        public AddStoreOwner() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.store_owner = userId.Value;
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyAddStoreOwner()
        {
            Result<bool> reguserId2 = this.api.Register("Buyer@gmail.com", "BuyerPassword");
            Result<String> userId2 = this.api.Login("Buyer@gmail.com", "BuyerPassword");
            String user_id = userId2.Value;
            Result<bool> addStoreOwnRes = api.AddStoreOwner(user_id, store_owner, store_id);
            Assert.True(addStoreOwnRes.ErrorOccured == false);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadAddStoreOwner_notByStoreOwner()
        {
            Result<bool> reguserId2 = this.api.Register("Buyer@gmail.com", "BuyerPassword");
            Result<String> userId2 = this.api.Login("Buyer@gmail.com", "BuyerPassword");
            String user_id = userId2.Value;
            reguserId2 = this.api.Register("Buyer@gmail.com", "BuyerPassword");
            userId2 = this.api.Login("Buyer@gmail.com", "BuyerPassword");
            String user_id2 = userId2.Value;
            Result<bool> addStoreOwnRes = api.AddStoreOwner(user_id, user_id2, store_id);
            Assert.True(addStoreOwnRes.ErrorOccured);
        }
    }
}
