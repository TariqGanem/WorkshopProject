using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Req II.4.6
namespace eCommerceAcceptanceTests.StoreTests.StoreOwnerOps
{
    public class AddStoreManager : XeCommerceTestCase
    {
        public String store_owner { set; get; }
        public String store_id { set; get; }
        public AddStoreManager() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", userId.Value);
            this.store_id = storeId.Value;
            this.store_owner = userId.Value;

        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyAddStoreManager()
        {
            Result<bool> reguserId2 = this.api.Register("Manager@gmail.com", "ManagerPassword");
            Result<String> userId2 = this.api.Login("Manager@gmail.com", "ManagerPassword");
            String user_id = userId2.Value;
            Result<bool> managerRes = api.AddStoreManager(user_id, store_owner, store_id);
            Assert.True(managerRes.Value);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadAddStoreManager_AppoitingToManagerTwice()
        {
            Result<bool> reguserId2 = this.api.Register("Manager@gmail.com", "ManagerPassword");
            Result<String> userId2 = this.api.Login("Manager@gmail.com", "ManagerPassword");
            String user_id = userId2.Value;
            Result<bool> managerRes = api.AddStoreManager(user_id, store_owner, store_id);
            Assert.True(managerRes.Value);
            managerRes = api.AddStoreManager(user_id, store_owner, store_id);
            Assert.True(managerRes.ErrorOccured);
        }

    }
}
