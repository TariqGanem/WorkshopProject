using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Store.Policies.Discount
{
    public interface IDiscountStrategy
    {
        public double CalculatePriceAfterDiscount(double Price , int quantity , String code);
    }
}
