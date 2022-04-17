using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Store.Policies.Purchase
{
    public interface IPurchaseStrategy
    {
        public double calculatePriceToPay(int quantity);
    }
}
