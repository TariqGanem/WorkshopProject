using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using Xunit;
// Req II.1.1
namespace eCommerceAcceptanceTests.UserTests
{
    public class GuestUserLogin : XeCommerceTestCase
    {
        public GuestUserLogin() : base() { }

        [Fact]
        [Trait("Category", "AcceptanceTests")]
        public void HappyGuestUserLoginTest()
        {
            Result res = api.Login();
            Assert.False(res.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "AcceptanceTests")]
        public void HappyGuestUserLoginLogoutTest()
        {
            Result<string> res = api.Login();
            Result res2 = api.Logout(res.Value);
            Assert.False(res2.ErrorOccured);
        }

    }
}
