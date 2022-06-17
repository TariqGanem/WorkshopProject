using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Client
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["keyword"] != null)
            {
                UserHandler a = new UserHandler();
                DataListproducts.DataSource = a.SearchProduct(Request.QueryString["keyword"].ToString());
                DataListproducts.DataBind();
            }
            else
            {
                UserHandler a = new UserHandler();
                DataListproducts.DataSource = a.getAllProductsInSystem();
                DataListproducts.DataBind();
            }
        }

        protected void DataListproducts_ItemCommand1(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "add_to_cart")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                Session["productId"] = cargs[0];
                Session["Name"] = cargs[1];
                Session["price"] = cargs[2];
                Session["category"] = cargs[3];
                Session["quantity"] = cargs[4];
                Response.Redirect("~/Product.aspx");
            }
        }

        protected void DataListproducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}