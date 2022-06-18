using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class ShoppingHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserHandler a = new UserHandler();
            DataListsShoppingHistory.DataSource = a.getUserPurchaseHistory(Session["userId"].ToString());
            DataListsShoppingHistory.DataBind();
        }
    }
}