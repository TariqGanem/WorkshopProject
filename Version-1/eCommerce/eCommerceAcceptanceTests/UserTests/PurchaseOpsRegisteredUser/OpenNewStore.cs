using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Req II.3.2
namespace eCommerceAcceptanceTests.UserTests.PurchaseOpsRegisteredUser
{
    public class OpenNewStore : XeCommerceTestCase
    {
        public String store_owner { set; get; }
        public OpenNewStore() : base()
        {
            Result<bool> reguserId = this.api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = this.api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            this.store_owner = userId.Value;
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyOpenNewStore()
        {
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", store_owner);
            Assert.True(!storeId.ErrorOccured);
            String store_id = storeId.Value;
            Dictionary<String, List<int>> staff = api.GetStoreStaff(store_owner, store_id).Value;
            Assert.True(staff.Count == 1);
            Assert.True(staff.ContainsKey(store_owner));
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadOpenNewStore_GuestUserOpenningNewStore() 
        {
            Result<String> guest_user = api.Login();
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", guest_user.Value);
            Assert.True(storeId.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadOpenNewStore_NotLoginnedUserCantOpenStore()
        {
            Result<bool> logout_store_owner = api.Logout(store_owner);
            Assert.True(!logout_store_owner.Value); // logout succeeded
            Result<String> storeId = this.api.OpenNewStore("AmazonWanaBe", store_owner);
            Assert.True(storeId.ErrorOccured);
        }

    }
}
