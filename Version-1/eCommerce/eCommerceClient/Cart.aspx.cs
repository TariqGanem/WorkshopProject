using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Labelerrorcreditcard.Visible = false;
                ShopHandler a = new ShopHandler();
                UserHandler u = new UserHandler();

                Data_cart.DataSource = a.GetUserBaskets(Session["userId"].ToString());
                Data_cart.DataBind();

                //Label3.Text = u.GetTotalCart(Session["userId"].ToString()).ToString();

            }
            else {

            }

        }

        protected void DataListCart_ItemCommand1(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "Delete_command")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                Session["productName"] = cargs[0];
                Session["productId"] = cargs[1];
                Session["price"] = cargs[2];
                Session["storeId"] = cargs[3];
                Session["Amount"] = cargs[4];

                ShopHandler s = new ShopHandler();
                bool b = s.remove_item_from_cart(Session["userId"].ToString(), Session["storeId"].ToString(), Session["productId"].ToString());
                if (b)
                {
                    ShopHandler c = new ShopHandler();
                    Data_cart.DataSource = c.GetUserBaskets(Session["userId"].ToString());
                    Data_cart.DataBind();
                    
                    Response.Redirect("~/Cart.aspx");
                }
            }

            if (e.CommandName == "up_command")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                Session["productName"] = cargs[0];
                Session["productId"] = cargs[1];
                Session["storeId"] = cargs[2];
                Session["Amount"] = cargs[3];

                ShopHandler s = new ShopHandler();
                bool a = s.UpdateCart(Session["userId"].ToString(), Session["storeId"].ToString(), Session["productId"].ToString(), int.Parse(Session["Amount"].ToString()) + 1);
                if (a)
                {
                    //Data_cart.DataSource = s.GetUserBaskets(Session["userId"].ToString());
                    //Data_cart.DataBind();
                    Label b = e.Item.FindControl("Label1") as Label;
                    b.Text = String.Format("{0}", int.Parse(b.Text) + 1);
                }
                Response.Redirect("~/Cart.aspx");
            }
            if (e.CommandName == "down_command")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                Session["productName"] = cargs[0];
                Session["productId"] = cargs[1];
                Session["storeId"] = cargs[2];
                Session["Amount"] = cargs[3];
                ShopHandler s = new ShopHandler();
                bool a = s.UpdateCart(Session["userId"].ToString(), Session["storeId"].ToString(), Session["productId"].ToString(), int.Parse(Session["Amount"].ToString()) - 1);
                if (a){
                    //Data_cart.DataSource = s.GetUserBaskets(Session["userId"].ToString());
                    //Data_cart.DataBind();
                    Label b = e.Item.FindControl("Label1") as Label;
                    b.Text = String.Format("{0}", int.Parse(b.Text) - 1);
                }
                Response.Redirect("~/Cart.aspx");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ShopHandler s = new ShopHandler();
            if (TextBoxCreditcard.Text.Length == 0)
            {
                Labelerrorcreditcard.Visible = true;
                Labelerrorcreditcard.Text = "enter your creditcard!!!";
            }
            else
            {
                bool buy = s.Purchase(Session["userId"].ToString(), TextBoxCreditcard.Text.ToString());
                if (buy)
                {
                    Response.Redirect("~/PurchaseDone.aspx");
                }
            }

        }
    }
}