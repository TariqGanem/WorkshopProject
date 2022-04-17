using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Store.Policies.Discount
{
    public class Visible : IDiscountStrategy
    {
        public double Percentage { get; set; }
        public DateTime experation_date { get; set; }

        public Visible(double Percentage , DateTime experation_date)
        {
            this.Percentage = Percentage > 100 | Percentage < 0 ? throw new Exception($"{Percentage} is not eligible Percentage") : Percentage;
            this.experation_date = experation_date;
        }
        public double CalculatePriceAfterDiscount(double Price ,int quantity , String code)
        {
            if (DateTime.Now.CompareTo(experation_date) > 0)
                throw new Exception("Discount is not eligible , Time Exceeded");
            return (Price * ((100 - Percentage) / 100)) * quantity;
        }
    }
}
