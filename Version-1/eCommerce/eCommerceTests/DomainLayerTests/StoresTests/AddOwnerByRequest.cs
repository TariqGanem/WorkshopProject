using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.OwnerAppointmennt;
using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eCommerceTests.DomainLayerTests.StoresTests
{
    public class AddOwnerByRequest
    {
        public RegisteredUser Owner1 { get; }
        public RegisteredUser Owner2 { get; }
        public RegisteredUser RegisteredUser { get; }

        public Store Store { get; }

        public OwnerRequest Offer { get; }

        public AddOwnerByRequest()
        {
            Owner1 = new RegisteredUser("owner1@offer", "pass1");
            Owner2 = new RegisteredUser("owner2@offer", "pass2");
            Store = new Store("store1", Owner1);
            string prodid = Store.AddNewProduct(Owner1.Id, "product", 100, 100, "");
            RegisteredUser = new RegisteredUser("reg@user", "pass3");
            Offer = new OwnerRequest(Owner2.Id, Store.Id,Owner1.Id);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void sendOfferToStoreTest()
        {
            Store.SendOwnerRequestToStore(Offer);
            Assert.Single(Store.RequestManager.PendingOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void sendOfferResponseDeclineTest()
        {
            Store.SendOwnerRequestToStore(Offer);
            Store.SendOwnerRequestResponseToUser(Owner1.Id, Offer.Id,false);
            Store.SendOfferResponseToUser("wrong id", Offer.Id, false, -1);
            Assert.Empty(Store.RequestManager.PendingOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void sendOfferResponseCounterOfferTest()
        {
            Store.SendOwnerRequestToStore(Offer);
            Store.SendOwnerRequestResponseToUser(Owner1.Id, "wrong_id", false);
            Assert.Single(Store.RequestManager.PendingOffers);
            Store.SendOfferResponseToUser(Owner1.Id, Offer.Id, false, 2);
            Assert.Empty(Store.RequestManager.PendingOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void sendOfferResponseAcceptTest()
        {
            Store.SendOwnerRequestToStore(Offer);
            Store.SendOwnerRequestResponseToUser(Owner1.Id, Offer.Id, true);
            Assert.Empty(Store.RequestManager.PendingOffers);
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public void OfferGetDataTest()
        {
            Store.SendOwnerRequestToStore(Offer);
            List<Dictionary<string, string>> result = Store.getStoreOwnerRequests();
            Assert.Single(result);
        }
    }
}
