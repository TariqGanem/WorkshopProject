using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelproductName0.Text = Session["Name"].ToString();
            LabelstoreId0.Text = Session["storeId"].ToString();
            Labelbarcode0.Text = Session["Id"].ToString();
            Labelcategories0.Text = Session["Catagory"].ToString();
            Labelprice0.Text = Session["Price"].ToString();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (Label1.Text.ToString().Equals("0")) { }
            else
            {
                Label1.Text = (int.Parse(Label1.Text.ToString()) - 1).ToString();
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
                Label1.Text = (int.Parse(Label1.Text.ToString()) + 1).ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ShopHandler sh = new ShopHandler();
            sh.AddProductToBasket(Session["userId"].ToString(), Session["Id"].ToString(), int.Parse(Label1.Text.ToString()), Session["storeId"].ToString());
            Response.Redirect("~/Home.aspx");

        }
    }
}