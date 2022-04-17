using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Store.Policies.Purchase
{
    public class Immediate : IPurchaseStrategy
    {
        public double Price;
        public Immediate(Product pro)
        {
            this.Price  = pro.Price;
        }
        public double calculatePriceToPay(int quantity)
        {
            return quantity * this.Price;
        }
    }
}
