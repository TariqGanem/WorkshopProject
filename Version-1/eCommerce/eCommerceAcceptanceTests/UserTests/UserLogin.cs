using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using Xunit;

namespace eCommerceAcceptanceTests.UserTests
{
    public class UserLogin : XeCommerceTestCase
    {
        public UserLogin() : base() {}

        [Fact]
        [Trait("Category", "AcceptanceTests")]
        public void HappyLoginTest()
        {
            api.Register("goodtest@gmail.com", "GoodPass");
            Result res = api.Login("goodtest@gmail.com", "GoodPass");
            Assert.True(!res.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "AcceptanceTests")]
        public void sadLoginTest_IncorrectPass()
        {
            api.Register("goodtest@gmail.com", "GoodPass");
            Result res = api.Login("goodtest@gmail.com", "SadPassXD");
            Assert.True(res.ErrorOccured);
        }

        [Fact]
        [Trait("Category", "AcceptanceTests")]
        public void sadLoginTest_UnregisteredUser()
        {
            Result res = api.Login("goodtest@gmail.com", "sadpassxd");
            Assert.True(res.ErrorOccured);
        }

    }
}
