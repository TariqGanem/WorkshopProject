using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Open_shop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Labelerror.Visible = false;
        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            ShopHandler s = new ShopHandler();
            string userId = Session["userId"].ToString();
            bool open = s.OpenShop(userId, TextBoxShopname.Text);
            if (!open) {
                Labelerror.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failed to open store!!!')", true);
            }
            Response.Redirect("~/Home.aspx");
        }
    }
}