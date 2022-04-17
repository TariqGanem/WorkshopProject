using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Store.Policies.Purchase
{
    public enum BidState : int
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2,
        Counter_Offer = 3
    }

    public class Bidding : IPurchaseStrategy
    {
        public double Price { get; set; }
        public double offer { get; set; }
        public BidState BidState { get; set; }
        public Bidding(double offer , Product product)
        {
            this.offer = offer;
            Price = product.Price;
            BidState = BidState.Pending;
        }
        
        public void acceptOffer()
        {
            this.BidState = BidState.Accepted;
            Price = offer;
        }

        public void rejectOffer()
        {
            this.BidState = BidState.Rejected;
        }

        public void Counter_Offer(double counterOffer)
        {
            this.BidState = BidState.Counter_Offer;
            Price = counterOffer;
        }
        public double calculatePriceToPay(int quantity)
        {
            return this.BidState != BidState.Pending ? quantity*Price : throw new Exception("Still waiting for a decition from the managers about the offer");
        }
        // @todo:
            //notify customer when a decision is made
            //notify owners about a bidding
    }
}
