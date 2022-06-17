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
            LabelQuantity.Text = Session["quantity"].ToString();
            Labelbarcode0.Text = Session["productId"].ToString();
            Labelcategories0.Text = Session["category"].ToString();
            Labelprice0.Text = Session["price"].ToString();
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
            UserHandler sh = new UserHandler();
            String storeid = sh.getStoreIdByProductId(Session["productId"].ToString());
            storeid = UserHandler.getID(storeid);
            sh.AddProductToCart(Session["userId"].ToString(), Session["productId"].ToString(),int.Parse(Label1.Text.ToString()), storeid);
            Response.Redirect("~/Home.aspx");

        }
    }
}