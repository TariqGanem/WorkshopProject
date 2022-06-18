using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class ShoppingCartSO
    {
        #region parameters
        public string Id { get; }
        public Dictionary<string, ShoppingBagSO> shoppingBags { get; }
        public double TotalPrice { get; }
        #endregion

        #region constructors

        public ShoppingCartSO(ShoppingCart shoppingCart)
        {
            this.Id = shoppingCart.Id;
            shoppingBags = new Dictionary<string, ShoppingBagSO>();
            foreach (string basket_key in shoppingCart.ShoppingBags.Keys)
            {
                shoppingBags[basket_key] = new ShoppingBagSO(shoppingCart.ShoppingBags[basket_key]);
            }
            this.TotalPrice = shoppingCart.TotalCartPrice;
        }

        public String[][] toArray()
        {
            List<String[]> list = new List<string[]>();
            foreach (ShoppingBagSO bag in shoppingBags.Values)
            {
                foreach (KeyValuePair<ProductService, int> prs in bag.Products)
                {
                    String[] str = new string[4];
                    str[0] = bag.StoreId;
                    str[1] = prs.Key.Name;
                    str[2] = prs.Key.Price.ToString();
                    str[3] = prs.Value.ToString();
                    list.Add(str);
                }
            }
            return list.ToArray();

        }
        #endregion
    }
}
