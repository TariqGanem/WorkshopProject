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
            LabelErrorRateProduct.Visible = false;
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
            string s = sh.AddProductToCart(Session["userId"].ToString(), Session["productId"].ToString(),int.Parse(Label1.Text.ToString()), storeid);
            if(s.Substring(1,6) == "Error:")
            {
                LabelErrorRateProduct.Text = s;
                LabelErrorRateProduct.Visible = true;
                return;
            }
            Response.Redirect("~/Home.aspx");

        }

        protected void btnRateStore_OnClick(object sender, EventArgs e)
        {
            UserHandler sh = new UserHandler();
            String storeid = sh.getStoreIdByProductId(Session["productId"].ToString());
            storeid = UserHandler.getID(storeid);
            if (TextBoxRate.Text.Trim().Length == 0)
            {
                LabelErrorRateProduct.Visible = true;
                LabelErrorRateProduct.Text = "Rate Field is Empty";
                return;
            }
            string str = sh.addProductRating(Session["userId"].ToString(), storeid, Session["productId"].ToString(), int.Parse(TextBoxRate.Text.ToString()));
            if (str.Substring(1,6) != "Error:")
            {
                LabelErrorRateProduct.Visible = true;
                LabelErrorRateProduct.Text = "Product Rate Updated";
                return;
            }
            else
            {
                LabelErrorRateProduct.Visible = true;
                LabelErrorRateProduct.Text = str;
                return;
            }
        }

        protected void SendOfferBtn_OnClick(object sender, EventArgs e)
        {
            UserHandler sh = new UserHandler();
            if (priceTxt.Text.Trim().Length == 0 || AmountTxt.Text.Trim().Length == 0 || !int.TryParse(priceTxt.Text,out int val) || val < 0 || !int.TryParse(AmountTxt.Text, out int val2) || val2 <= 0)
            {
                LabelErrorRateProduct.Visible = true;
                LabelErrorRateProduct.Text = "fields have illegal vals";
                return;
            }
            string storeid = sh.getStoreIdByProductId(Session["productId"].ToString());
            if(storeid.Substring(1,6).Equals("Error:"))
            {
                LabelErrorRateProduct.Visible = true;
                LabelErrorRateProduct.Text = "store is not found";
                return;
            }
            string str = sh.SendOfferToStore(storeid.Substring(1, 32), Session["userId"].ToString(), Session["productId"].ToString(), int.Parse(AmountTxt.Text), int.Parse(priceTxt.Text));
            if (str.Substring(1,6) != "Error:")
            {
                LabelErrorRateProduct.Visible = true;
                LabelErrorRateProduct.Text = "Offer Sent";
                return;
            }
            else
            {
                LabelErrorRateProduct.Visible = true;
                LabelErrorRateProduct.Text = str;
                return;
            }
        }
    }
}