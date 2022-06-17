using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class UserHistorySO
    {
        //Properties
        public LinkedList<ShoppingBagSO> ShoppingBags { get; }

        //Constructor
        public UserHistorySO(History history)
        {
            ShoppingBags = new LinkedList<ShoppingBagSO>();
            LinkedList<ShoppingBag> shoppingBags = history.ShoppingBags;
            foreach (ShoppingBag bag in shoppingBags)
            {
                ShoppingBags.AddLast(new ShoppingBagSO(bag));
            }
        }

        public String[][] toArray()
        {
            List<String[]> list = new List<string[]>();
            foreach(ShoppingBagSO bag in ShoppingBags)
            {
                foreach(KeyValuePair<ProductService,int> prs in bag.Products)
                {
                    String[] str = new string[4];
                    str[0] = bag.StoreId;
                    str[1] = prs.Key.Name;
                    str[2] = prs.Key.Price.ToString();
                    str[3] = prs.Value.ToString();
                }
            }
        }
    }
}
