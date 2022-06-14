using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using Xunit;

namespace eCommerceTests.DomainLayerTests.StoresTests
{
    public class StoreTests
    {
        public Store store { get; set; }

        public StoreTests()
        {
            RegisteredUser founder = new RegisteredUser("WalterWhite", "PureCrystals100");
            store = new Store("Candy", founder);
        }

        public void addRatingsFurtherTesting()
        {
            this.store.AddRating(2);
            this.store.AddRating(3);
            this.store.AddRating(4);
            this.store.AddRating(3.4);
        }

        [Theory()]
        [Trait("Category", "UnitTesting")]
        [InlineData(2.2)]
        [InlineData(5)]
        [InlineData(1)]
        public void AddRatingsTestshappy(double rating)
        {
            if (rating < 1 | rating > 5)
            {
                try
                {
                    this.store.AddRating(rating);
                }
                catch (Exception e)
                {
                    Assert.Equal("Store Candy could not be rated. Please use number between 1 to 5", e.Message);
                }
            }
            else
            {
                addRatingsFurtherTesting();
                store.AddRating(rating);
                Assert.Equal(store.Rate, (2+3+4+3.4+rating)/5);
            }
        }

        [Trait("Category", "UnitTesting")]
        [InlineData(0.9)]
        public void AddRatingsTestssad(double rating)
        {
            if (rating < 1 | rating > 5)
            {
                try
                {
                    this.store.AddRating(rating);
                }
                catch (Exception e)
                {
                    Assert.Equal("Store Candy could not be rated. Please use number between 1 to 5", e.Message);
                }
            }
            else
            {
                addRatingsFurtherTesting();
                store.AddRating(rating);
                Assert.Equal(store.Rate, (2 + 3 + 4 + 3.4 + rating) / 5);
            }
        }
    }
}
