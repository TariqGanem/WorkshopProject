using eCommerce.src.ServiceLayer.ResultService;
using eCommerceAcceptanceTests.UserTests.PurchaseOpsGuestUser;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Req II.3(2.2)
namespace eCommerceAcceptanceTests.UserTests.PurchaseOpsRegisteredUser
{   
    // The same code as with Guest User
    // since when it comes for product search both of them have the same functionality
    public class SearchProductsRegUser : SearchProducts
    {
        public SearchProductsRegUser() : base(){}

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyProductSearch_SearchOnNameRegUser()
        {
            HappyProductSearch_SearchOnName();
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void HappyProductSearch_SearchOnPriceRegUser()
        {
            HappyProductSearch_SearchOnPrice();
        }

        [Fact]
        [Trait("Category", "acceptance")]
        public void SadProductSearch_PorductNameDoesNotExistRegUser()
        {
            SadProductSearch_PorductNameDoesNotExist();
        }

    }
}
