using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using Xunit;
// Req II.1.2
namespace eCommerceAcceptanceTests.UserTests
{
    public class UserLogout : XeCommerceTestCase
    {
        public UserLogout() : base() { }

        [Fact]
        [Trait("Category", "AcceptanceTests")]
        public void HappyLogout()
        {
            api.Register("goodtest@gmail.com", "GoodPass");
            Result<String> userIdRes = api.Login("goodtest@gmail.com", "GoodPass");
            Result res = this.api.Logout(userIdRes.Value); // logging out by user_id - not email 
            Assert.True(!res.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "AcceptanceTests")]
        public void sadLogoutTest_notLoginedUser()
        {
            api.Register("goodtest@gmail.com", "GoodPass");
            Result res = this.api.Logout("goodtest@gmail.com");
            Assert.True(res.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "AcceptanceTests")]
        public void sadLogoutTest_WrongEmail()
        {
            api.Register("goodtest@gmail.com", "GoodPass");
            api.Login("goodtest@gmail.com", "GoodPass");
            Result res = this.api.Logout("goodtest@gmail.cm");
            Assert.True(res.ErrorOccured);
        }
    }
}
