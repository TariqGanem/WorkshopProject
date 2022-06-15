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
            String[][] strmat = new string[shoppingBags.Count+1][];
            int i = 0;
            foreach(KeyValuePair<string,ShoppingBagSO> shb in shoppingBags)
            {
                strmat[i] = shb.Value.toArray();
                i++;
            }
            strmat[strmat.Length - 1] = new String[3];
            strmat[strmat.Length - 1][0] = TotalPrice.ToString();
            return strmat;

        }
        #endregion
    }
}
