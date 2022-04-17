using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Store.Policies.Discount
{
    public class Hidden : Visible
    {
        public String DiscountCode { get; }
        public Hidden(double Percentage, DateTime experation_date , String Code) : base(Percentage, experation_date)
        {
            DiscountCode = Code;
        }
        public new double CalculatePriceAfterDiscount(double Price, int quantity, String code)
        {
            if (DiscountCode.Equals(code))
                return base.CalculatePriceAfterDiscount(Price, quantity, code);
            return -1;
        }

    }
}
