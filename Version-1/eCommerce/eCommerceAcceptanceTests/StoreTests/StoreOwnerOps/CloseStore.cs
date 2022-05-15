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

        }
    }
}
