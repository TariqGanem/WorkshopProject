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
            int counter = 0;
            while (counter < 30)
            {
                try
                {
                    if (Request.QueryString["keyword"] != null)
                    {
                        ShopHandler a = new ShopHandler();
                        DataListproducts.DataSource = a.search(Request.QueryString["keyword"].ToString());
                        DataListproducts.DataBind();
                    }
                    else
                    {
                        ShopHandler a = new ShopHandler();
                        DataListproducts.DataSource = a.getAllProducts();
                        DataListproducts.DataBind();
                    }
                    return;
                }
                catch
                {
                    Thread.Sleep(1000);
                    counter++;
                }
            }
            if(counter >= 10)
                throw new Exception("server not responding");
        }

        protected void DataListproducts_ItemCommand1(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "add_to_cart")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                Session["Id"] = cargs[0];
                Session["storeId"] = cargs[1];
                Session["Name"] = cargs[2];
                Session["Price"] = cargs[3];
                Session["Catagory"] = cargs[4];
                Response.Redirect("~/Product.aspx");
            }
        }
    }
}