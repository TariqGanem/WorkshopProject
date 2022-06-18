using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class ShoppingHistoryAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // load shopping history for Session["useradminhistoryid"]
            UserHandler a = new UserHandler();
            DataListHistoryShoppingBags.DataSource = a.GetUserPurchaseHistory(Session["userId"].ToString() , Session["useradminhistoryid"].ToString());
            DataListHistoryShoppingBags.DataBind();
        }
    }
}