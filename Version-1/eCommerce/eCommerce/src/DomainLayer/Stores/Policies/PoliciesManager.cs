using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Store.Policies
{
    public enum PurchasePolicy : int
    {
        Immediate = 0,
        Bidding = 1,
        Auction = 2,
        Lottery = 3
    }

    public enum DiscountPolicy : int
    {
        Visible = 0,
        Conditional = 1,
        Hidden = 2
    }

    public class PoliciesManager
    {
        public bool[] DiscountPolicies { get; set; }
        public bool[] PurchasePolicies { get; set; }

        public PoliciesManager()
        {
            DiscountPolicies = new bool[3];
            PurchasePolicies = new bool[3];
        }

        public void setDiscountPolicy(DiscountPolicy toset , Boolean state)
        {
            DiscountPolicies[(int)toset] = state;
        }

        public void setPurchasePolicy(PurchasePolicy toset, Boolean state)
        {
            this.PurchasePolicies[(int)toset] = state;
        }
    }
}
