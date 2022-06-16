using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using Xunit;
// Req II.1.3
namespace eCommerceAcceptanceTests.UserTests
{
    public class UserRegister : XeCommerceTestCase
    {
        public UserRegister() : base() { }

        [Fact]
        [Trait("Category", "AcceptanceTests")]
        public void HappyRegisterTest()
        {
            Result<bool> reg = this.api.Register("goodtest@gmail.com", "GoodPass");
            Assert.True(reg.Value);
        }

        [Fact]
        [Trait("Category", "AcceptanceTests")]
        public void sadRegisterTest_sameEmailAddress()
        {
            Result<bool> reg = this.api.Register("goodtest@gmail.com", "GoodPass");
            Result<bool> sadreg = this.api.Register("goodtest@gmail.com", "randompass");
            Assert.True(reg.Value);
            Assert.False(sadreg.Value);
        }
    }
}
