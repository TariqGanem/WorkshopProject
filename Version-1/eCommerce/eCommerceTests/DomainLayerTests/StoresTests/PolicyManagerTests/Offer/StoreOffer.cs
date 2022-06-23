using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;
using eCommerce.src.DomainLayer.Stores.Policies.Offer;
using Xunit;

namespace eCommerceTests.DomainLayerTests.StoresTests.PolicyManagerTests
{
    public class StoreOffer
    {
        public RegisteredUser Owner1 { get; }
        public RegisteredUser Owner2 { get; }
        public RegisteredUser RegisteredUser { get; }

        public Store Store { get; }

        public Product Product { get; }

        public Offer Offer { get; }

        public StoreOffer()
        {
            Owner1 = new RegisteredUser("owner1@offer", "pass1");
            Owner2 = new RegisteredUser("owner2@offer", "pass2");
            Store = new Store("store1", Owner1);
            Store.AddStoreOwner(Owner2, Owner1.Id);
            string prodid = Store.AddNewProduct(Owner1.Id, "product", 100, 100, "");
            RegisteredUser = new RegisteredUser("reg@user", "pass3");
            Offer = new Offer(RegisteredUser.Id, prodid, 10, 10, Store.Id);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void sendOfferToStoreTest()
        {
            Store.SendOfferToStore(Offer);
            Assert.Single(Store.OfferManager.PendingOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void sendOfferResponseDeclineTest()
        {
            Store.SendOfferToStore(Offer);
            Store.SendOfferResponseToUser(Owner1.Id, "wrong id", false, -1);
            Store.SendOfferResponseToUser("wrong id", Offer.Id, false, -1);
            Assert.Single(Store.OfferManager.PendingOffers);
            Store.SendOfferResponseToUser(Owner1.Id, Offer.Id, false, -1);
            Assert.Empty(Store.OfferManager.PendingOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void sendOfferResponseCounterOfferTest()
        {
            Store.SendOfferToStore(Offer);
            Store.SendOfferResponseToUser(Owner1.Id, "wrong id", false, 2);
            Store.SendOfferResponseToUser("wrong id", Offer.Id, false, 2);
            Assert.Single(Store.OfferManager.PendingOffers);
            Store.SendOfferResponseToUser(Owner1.Id, Offer.Id, false, 2);
            Assert.Empty(Store.OfferManager.PendingOffers);
            Assert.True(Offer.CounterOffer == 2);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void sendOfferResponseAcceptTest()
        {
            Store.SendOfferToStore(Offer);
            Store.SendOfferResponseToUser(Owner1.Id, Offer.Id, true, -1);
            Assert.Single(Store.OfferManager.PendingOffers);
            Store.SendOfferResponseToUser(Owner2.Id, Offer.Id, true, -1);
            Assert.Empty(Store.OfferManager.PendingOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void OfferGetDataTest()
        {
            Store.SendOfferToStore(Offer);
            List<Dictionary<string, object>> result = Store.getStoreOffers();
            Assert.Single(result);
        }
    }
}
