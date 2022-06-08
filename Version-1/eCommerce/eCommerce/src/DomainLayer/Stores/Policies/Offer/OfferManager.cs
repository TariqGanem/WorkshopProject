using eCommerce.src.DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace eCommerce.src.DomainLayer.Stores.Policies.Offer
{
    public enum OfferResponse
    {
        None,
        Accepted,
        Declined,
        CounterOffered
    }

    public class OfferManager
    {

        public List<Offer> PendingOffers { get; set; }

        public OfferManager()
        {
            PendingOffers = new List<Offer>();
        }

        public OfferManager(List<Offer> pendingOffers)
        {
            PendingOffers = pendingOffers;
        }

        private Offer getOffer(string id)
        {
            foreach (Offer offer in PendingOffers)
                if (offer.Id.Equals(id))
                    return offer;
            return null;
        }

        public OfferResponse SendOfferResponseToUser(string ownerID, string offerID, bool accepted, double counterOffer, List<string> allOwners)
        {
            try
            {
                Monitor.Enter(offerID);
                try
                {
                    Offer offer = getOffer(offerID);
                    if (offer == null)
                        throw new Exception("Failed to response to an offer: Failed to locate the offer");
                    if (accepted)
                        return AcceptedResponse(ownerID, offer, allOwners);

                    PendingOffers.Remove(offer);

                    if (counterOffer == -1)
                    {
                        return DeclinedResponse(offer);
                    }

                    OfferResponse res = CounterOfferResponse(offer, counterOffer);
                    return res;
                }
                finally
                {
                    Monitor.Exit(offerID);
                }
            }
            catch (SynchronizationLockException SyncEx)
            {
                Console.WriteLine("A SynchronizationLockException occurred. Message:");
                Console.WriteLine(SyncEx.Message);
                throw new Exception(SyncEx.Message);
            }
        }

        private OfferResponse CounterOfferResponse(Offer offer, double counterOffer)
        {
            if (counterOffer < 0)
                throw new Exception("Illegal counter offer: can't be negative");
            offer.CounterOffer = counterOffer;
            return OfferResponse.CounterOffered;
        }

        private OfferResponse DeclinedResponse(Offer offer)
        {
            return OfferResponse.Declined;
        }

        private OfferResponse AcceptedResponse(string ownerID, Offer offer, List<string> allOwners)
        {
            OfferResponse response = offer.AcceptedResponse(ownerID, allOwners);
            if (response == OfferResponse.Accepted)
            {
                PendingOffers.Remove(offer);
            }
            return response;
        }

        internal void AddOffer(Offer offer)
        {
            PendingOffers.Add(offer);
        }

        public List<Dictionary<string, object>> getStoreOffers()
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (Offer offer in PendingOffers)
                list.Add(offer.GetData());
            return list ;
        }

        public List<DTO_Offer> GetDTO()
        {
            List<DTO_Offer> dto_offers = new List<DTO_Offer>();
            foreach (Offer offer in PendingOffers)
            {
                dto_offers.Add(new DTO_Offer(offer.Id, offer.UserID, offer.ProductID, offer.StoreID, offer.Amount, offer.Price, offer.CounterOffer, offer.acceptedOwners));
            }
            return dto_offers;
        }
    }
}
