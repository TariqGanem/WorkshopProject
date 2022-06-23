using eCommerce.src.DomainLayer.Stores.Policies.Offer;
using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eCommerceTests.DomainLayerTests.StoresTests.PolicyManagerTests
{
    public class UserOffer
    {
        public RegisteredUser RegisteredUser { get; }

        public UserOffer()
        {
            RegisteredUser = new RegisteredUser("reg@user", "pass3");
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void AddOfferTest()
        {
            Assert.Empty(RegisteredUser.PendingOffers);
            Assert.Empty(RegisteredUser.AcceptedOffers);
            Offer result = RegisteredUser.SendOfferToStore("storeID", "productID", 10, 20);
            Assert.NotNull(result);
            Assert.Equal("storeID", result.StoreID);
            Assert.Equal("productID", result.ProductID);
            Assert.True(10 == result.Amount);
            Assert.True(20 == result.Price);
            Assert.Single(RegisteredUser.PendingOffers);
            Assert.Empty(RegisteredUser.AcceptedOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void RemoveOfferTest()
        {
            Offer Offer = RegisteredUser.SendOfferToStore("storeID", "productID", 10, 10);
            RegisteredUser.RemovePendingOffer(Offer.Id);
            Assert.Empty(RegisteredUser.PendingOffers);
            Assert.Empty(RegisteredUser.AcceptedOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void AcceptOfferTest()
        {
            Offer Offer = RegisteredUser.SendOfferToStore("storeID", "productID", 10, 10);
            RegisteredUser.AcceptOffer(Offer.Id);
            Assert.Empty(RegisteredUser.PendingOffers);
            Assert.Single(RegisteredUser.AcceptedOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void DeclineOfferTest()
        {
            Offer Offer = RegisteredUser.SendOfferToStore("storeID", "productID", 10, 10);
            RegisteredUser.DeclineOffer(Offer.Id);
            Assert.Empty(RegisteredUser.PendingOffers);
            Assert.Empty(RegisteredUser.AcceptedOffers);
        }


        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData(true)]
        [InlineData(false)]
        public void CounterOfferOfferTest(bool accept)
        {
            Offer Offer = RegisteredUser.SendOfferToStore("storeID", "productID", 10, 10);
            Offer.CounterOffer = 20;
            RegisteredUser.CounterOffer(Offer.Id);
            Assert.Single(RegisteredUser.PendingOffers);
            Assert.Empty(RegisteredUser.AcceptedOffers);

            RegisteredUser.AnswerCounterOffer(Offer.Id, accept);
            Assert.Empty(RegisteredUser.PendingOffers);
            if (accept)
                Assert.Single(RegisteredUser.AcceptedOffers);
            else
                Assert.Empty(RegisteredUser.AcceptedOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void OfferGetDataTest()
        {
            RegisteredUser.SendOfferToStore("storeID", "productID", 10, 10);
            List<Dictionary<string, object>> result = RegisteredUser.getUserPendingOffers();
            Assert.Single(result);
        }
    }
}
