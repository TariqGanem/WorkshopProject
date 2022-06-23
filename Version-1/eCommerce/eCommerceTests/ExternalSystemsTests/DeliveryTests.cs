using eCommerce.src.ExternalSystems;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eCommerceTests.ExternalSystemsTests
{
    public class DeliveryTests
    {
        public IDictionary<String, Object> deliveryDetails;
        public bool isMock { get; }
        public DeliveryTests()
        {
            isMock = true;
            if (isMock)
                ExternalSystemAPI.getInstance("");
            else
                ExternalSystemAPI.getInstance("https://cs-bgu-wsep.herokuapp.com/");
            deliveryDetails = new Dictionary<String, Object>
                    {
                     { "name", "Israel Israelovice" },
                     { "address", "Rager Blvd 12" },
                     { "city", "Beer Sheva" },
                     { "country", "Israel" },
                     { "zip", "8458527" }
                    };
        }

        [Theory()]
        [InlineData(false)]
        [InlineData(true)]

        public void SupplyTest(bool isEmpty)
        {
            int result;
            if (isEmpty)
                result = Logistics.Supply(new Dictionary<String, Object>());
            else
                result = Logistics.Supply(deliveryDetails);

            if (isEmpty)
                Assert.Equal(-1, result);
            else
                Assert.InRange(result, 10000, 100000);
        }

        [Theory()]
        [InlineData(false)]
        [InlineData(true)]

        public void CancelSupplyTest(bool isEmpty)
        {
            int supplyId = Logistics.Supply(deliveryDetails);
            int result;
            if (isEmpty)
                result = Logistics.CancelSupply(new Dictionary<String, Object>());
            else
                result = Logistics.CancelSupply(new Dictionary<String, Object>() { { "transaction_id", supplyId.ToString() } });

            if (isEmpty)
                Assert.Equal(-1, result);
            else
                Assert.Equal(1, result);
        }
    }
}
