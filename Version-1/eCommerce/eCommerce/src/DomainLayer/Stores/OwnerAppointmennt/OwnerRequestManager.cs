using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.src.DomainLayer.Stores.OwnerAppointmennt
{
    public enum OwnerRequestResponse
    {
        None,
        Accepted,
        Declined
    }

    public class OwnerRequestManager
    {
 
        public List<OwnerRequest> PendingOffers { get; set; }

        public OwnerRequestManager()
        {
            PendingOffers = new List<OwnerRequest>();
        }

        public OwnerRequestManager(List<OwnerRequest> pendingOffers)
        {
            PendingOffers = pendingOffers;
        }

        private OwnerRequest getOffer(string id)
        {
            foreach (OwnerRequest offer in PendingOffers)
                if (offer.Id.Equals(id))
                    return offer;
            return null;
        }

        public OwnerRequestResponse SendOfferResponseToUser(string ownerID, string offerID, bool accepted, List<string> allOwners)
        {
            try
            {
                Monitor.Enter(offerID);
                try
                {
                    OwnerRequest offer = getOffer(offerID);
                    if (offer == null)
                        throw new Exception("Failed to response to a Owner Request: Failed to locate the Request");
                    if (accepted)
                        return AcceptedResponse(ownerID, offer, allOwners);

                    PendingOffers.Remove(offer);

                    return DeclinedResponse(offer);
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
        private OwnerRequestResponse DeclinedResponse(OwnerRequest offer)
        {
            return OwnerRequestResponse.Declined;
        }

        private OwnerRequestResponse AcceptedResponse(string ownerID, OwnerRequest offer, List<string> allOwners)
        {
            OwnerRequestResponse response = offer.AcceptedResponse(ownerID, allOwners);
            if (response == OwnerRequestResponse.Accepted)
            {
                PendingOffers.Remove(offer);
            }
            return response;
        }

        internal void AddOffer(OwnerRequest offer)
        {
            PendingOffers.Add(offer);
        }

        public List<Dictionary<string, string>> getStoreOffers()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (OwnerRequest offer in PendingOffers)
                list.Add(offer.GetData());
            return list;
        }

        public List<DTO_OwnerRequest> GetDTO()
        {
            List<DTO_OwnerRequest> dto_offers = new List<DTO_OwnerRequest>();
            foreach (OwnerRequest offer in PendingOffers)
            {
                dto_offers.Add(new DTO_OwnerRequest(offer.Id, offer.UserID, offer.StoreID, offer.AppointedBy ,  offer.acceptedOwners));
            }
            return dto_offers;
        }

    }
}
