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
            LabelError.Visible = false;
            if (!Page.IsPostBack)
            {
                UserHandler u = new UserHandler();

                Data_cart.DataSource = u.getUserShoppingCart(Session["userId"].ToString());
                Data_cart.DataBind();
                LabelActualPrice.Text = u.getTotalShoppingCartPrice(Session["userId"].ToString());
            }
            else {

            }

        }

        protected void DataListCart_ItemCommand1(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "Delete_command")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                UserHandler s = new UserHandler();
                string productid = s.getProductId(cargs[1].ToString(), cargs[0].ToString());
                string b = s.RemoveProductFromCart(Session["userId"].ToString(), cargs[1].ToString(), productid);
                if (b.Substring(1, 6) != "Error:") ;
                {
                    UserHandler c = new UserHandler();
                    Data_cart.DataSource = c.getUserShoppingCart(Session["userId"].ToString());
                    Data_cart.DataBind();
                    Response.Redirect("~/Cart.aspx");
                }
            }

            if (e.CommandName == "up_command")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');

                UserHandler s = new UserHandler();
                string productid = s.getProductId(cargs[1].ToString(), cargs[0].ToString());

                string a = s.UpdateCart(Session["userId"].ToString(), cargs[1].ToString(), productid, int.Parse(cargs[2].ToString()) + 1);
                if (a.Substring(1,6) != "Error:")
                {
                    Label b = e.Item.FindControl("Label1") as Label;
                    b.Text = String.Format("{0}", int.Parse(b.Text) + 1);
                }
                Response.Redirect("~/Cart.aspx");
            }
            if (e.CommandName == "down_command")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                UserHandler s = new UserHandler();
                string productid = s.getProductId(cargs[1].ToString(), cargs[0].ToString());

                string a = s.UpdateCart(Session["userId"].ToString(), cargs[1].ToString(), productid, int.Parse(cargs[2].ToString()) - 1);
                if (a.Substring(1,6) != "Error:")
                {
                    Label b = e.Item.FindControl("Label1") as Label;
                    b.Text = String.Format("{0}", int.Parse(b.Text) - 1);
                }
                Response.Redirect("~/Cart.aspx");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            UserHandler s = new UserHandler();
            if (TextBoxCreditcard.Text.Length == 0 | TextBoxAdress.Text.Length == 0 | TextBoxCity.Text.Length == 0 | TextBoxCountry.Text.Length == 0 |
                TextBoxCVV.Text.Length == 0 | TextBoxHolder.Text.Length == 0 | TextBoxId.Text.Length == 0 | TextBoxMonth.Text.Length == 0 | TextBoxName.Text.Length == 0 |
                TextBoxYear.Text.Length == 0 | TextBoxZip.Text.Length == 0)
            {
                LabelError.Visible = true;
            }
            else
            {
                string buy = s.Purchase(Session["userId"].ToString(), TextBoxCreditcard.Text.ToString() , TextBoxMonth.Text.ToString(),TextBoxYear.Text.ToString(),TextBoxHolder.Text.ToString(),
                    TextBoxCVV.Text.ToString() , TextBoxId.Text.ToString() , TextBoxName.Text.ToString(), TextBoxAdress.Text.ToString(), TextBoxCity.Text.ToString() , 
                    TextBoxCountry.Text.ToString() , TextBoxZip.Text.ToString());
                if (buy.Substring(1,6) != "Error:")
                {
                    Response.Redirect("~/PurchaseDone.aspx");
                }
                else
                {
                    LabelError.Text = buy;
                    LabelError.Visible = true;
                }
            }

        }

        protected void Data_cart_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
    }
}