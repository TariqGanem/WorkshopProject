using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;


namespace eCommerce.src.DomainLayer.Store.Policies.Purchase
{
    public enum LotteryStatus : int
    {
        inProgress = 0 ,
        WinnerFound = 1 ,
        DueDate = 2
    }
    public class Lottery : IPurchaseStrategy
    {
        public Tuple<String, double> productInfo { set; get; } // <productId,Price(what's left to buy a chance to win the product)>
        public ConcurrentDictionary<String, double> purchases { get; set; } // < buyerId , chance >
        public DateTime EndTime { set; get; }
        public LotteryStatus LotteryStatus { set; get; }
        public double ProductPrice { set; get; }
        public Tuple<String,double> LotteryWinner { set; get; }

        public Lottery(DateTime EndTime , String productID , double productPrice)
        {
            this.EndTime = EndTime;
            productInfo = new Tuple<string, double>(productID, productPrice);
            purchases = new ConcurrentDictionary<String, double>();
            LotteryStatus = LotteryStatus.inProgress;
            this.ProductPrice = productPrice;
            LotteryWinner = new Tuple<string, double>("", -1);
        }
        public double calculatePriceToPay(int quantity = 1)
        {
            int size = purchases.Count;
            Random rnd = new Random();
            int rand = rnd.Next(Convert.ToInt32(this.ProductPrice));
            double temp_price = 0;
            foreach (KeyValuePair<String, double> pair in purchases)
            {
                temp_price += pair.Value;
                if (temp_price > rand)
                {
                    LotteryWinner = new Tuple<string, double>(pair.Key, pair.Value);
                    return pair.Value;
                }
            }
            
        } 

        public Tuple<String,double> getWinner()
        {
            return this.LotteryWinner.Item2 == -1 ? throw new Exception($"Winner not found yet , LotteryStatus = {this.LotteryStatus.ToString()}") : this.LotteryWinner;
        }

        public Boolean buyAChance(double money , String buyerID)
        {
            if (money > productInfo.Item2)
                throw new Exception($"Money {money} exceeds the price of {productInfo.Item2}");
            if (DateTime.Now.CompareTo(EndTime) > 0)
            {
                LotteryStatus = LotteryStatus.DueDate;
                return false;
            }
            productInfo = new Tuple<String,double>(productInfo.Item1, productInfo.Item2 - money);
            if(productInfo.Item2 == 0)
                LotteryStatus=LotteryStatus.WinnerFound;
            purchases.AddOrUpdate(buyerID, money, (k, val) => val + money); // one buyer can make multiple contributions each time the money added to his previos one
            return true;
            
        }
    }
}
