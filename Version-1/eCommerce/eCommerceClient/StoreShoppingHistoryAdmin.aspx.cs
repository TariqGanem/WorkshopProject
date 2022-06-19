using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class StoreShoppingHistoryAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // load shopping history for Session["storeIdAdmin"]
            UserHandler a = new UserHandler();
            DataListHistoryShoppingBags.DataSource = a.GetStorePurchaseHistory(Session["userId"].ToString() , Session["storeIdAdmin"].ToString(), Session["admin"] != null );
            DataListHistoryShoppingBags.DataBind();
        }
    }
}