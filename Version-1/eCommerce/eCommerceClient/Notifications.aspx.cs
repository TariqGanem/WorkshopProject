using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Notifications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserHandler a = new UserHandler();
            Data_cart.DataSource = a.getNotifications(Session["userId"].ToString());
            Data_cart.DataBind();
        }
    }
}