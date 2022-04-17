using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Store.Policies.Purchase
{
    public class Auction : IPurchaseStrategy
    {
        
        public double starting_Price { get; }
        public String HighestBidderId { get; set; }
        public DateTime EndTime { get; }
        public double currentHighestBid { get; set; }

        public Auction(DateTime ClosingTime , double startingPrice)
        { 
            this.EndTime = ClosingTime;
            this.starting_Price = startingPrice;
            this.currentHighestBid = -1;
            this.HighestBidderId = "";
        }

        public Boolean bid(String bidderId , double Price)
        {
            if (Price > starting_Price & Price > currentHighestBid & DateTime.Now.CompareTo(EndTime) < 0)
            {
                currentHighestBid = Price;
                HighestBidderId = bidderId;
                return true;
            }
            return false;
        }
        public double calculatePriceToPay(int quantity = 1)
        {
            return currentHighestBid;
        }

        public String getHighestBidder()
        {
            return HighestBidderId;
        }


    }
}
