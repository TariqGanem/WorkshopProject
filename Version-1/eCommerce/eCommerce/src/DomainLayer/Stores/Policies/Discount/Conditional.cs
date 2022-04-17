using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Store.Policies.Discount
{
    public class Conditional : Visible
    {
        public int conditionQuantity { get; set; }
        public Conditional(double Percentage , DateTime experation_date, int conditionalQuantity) : base(Percentage,experation_date)
        {
            this.conditionQuantity = conditionalQuantity;
        }
        public new double CalculatePriceAfterDiscount(double Price, int quantity , String code)
        {
            if(quantity >= this.conditionQuantity)
                return base.CalculatePriceAfterDiscount(Price, quantity ,code);
            return -1;
        }
    }
}
