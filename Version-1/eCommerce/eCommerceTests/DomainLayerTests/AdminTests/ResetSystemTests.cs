using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eCommerceTests.DomainLayerTests.AdminTests
{
    public class ResetSystemTests : XeCommerceTestCase
    {
        private string user_id;
        private string admin_id;

        public ResetSystemTests() : base()
        {
            //Admin
            api.Register("Admin@terminal3", "Admin");
            this.admin_id = api.Login("Admin@terminal3", "Admin").Value;

            api.Register("test@gmail.com", "test123");
            this.user_id = api.Login("test@gmail.com", "test123").Value;
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void ResetSystemAdmin()
        {
            Assert.True(!api.ResetSystem(admin_id).ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void ResetSystemNotAdmin()
        {
            Assert.False(api.ResetSystem(user_id).ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void ResetSystemLogin()
        {
            api.ResetSystem(admin_id);
            api = Bridge.getService();

            Assert.False(api.Login("test@gmail.com", "test123").ErrorOccured);
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void ResetSystemRegidter()
        {
            api.ResetSystem(admin_id);
            api = Bridge.getService();

            Assert.True(!api.Register("test@gmail.com", "test123").ErrorOccured);
        }

    }
}
