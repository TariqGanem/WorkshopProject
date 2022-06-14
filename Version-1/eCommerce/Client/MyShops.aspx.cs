using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class MyShops : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            ShopHandler a = new ShopHandler();

            Data_shop.DataSource = a.GetUserStores(Session["username"].ToString());
            Data_shop.DataBind();
            }
        }
        protected void Data_shop_Command(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "editshop")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                Session["editshop"] = cargs[1];
                UserHandler uh = new UserHandler();
                if (uh.IsOwner(Session["editshop"].ToString(), Session["userId"].ToString()))
                {
                    Response.Redirect("~/EditShop.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('youre not an owner !!!!!!!!!!!!!')", true);
                }
            }
            if (e.CommandName == "Close")
            {
                Session["editshop"] = e.CommandArgument;
                ShopHandler sh = new ShopHandler();
                UserHandler uh = new UserHandler();
                if (uh.IsOwner(Session["editshop"].ToString(), Session["userId"].ToString()))
                {
                    sh.CloseStore(Session["editshop"].ToString(), Session["userId"].ToString());
                    Response.Redirect("~/MyShops.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('youre not an owner !!!!!!!!!!!!!')", true);
                }
            }
        }
    }
}