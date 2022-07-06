using Client.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class AddDiscountCondition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Label1.Text = "Select which condition you want to add";
                AndCondition.Visible = false;
                OrCondition.Visible = false;
                MaxProductCondition.Visible = false;
                MinProductCondition.Visible = false;
                MinBagPriceCondition.Visible = false;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DropDownList1.SelectedItem.Text == "Select")
            {
                AndCondition.Visible = false;
                OrCondition.Visible = false;
                MaxProductCondition.Visible = false;
                MinProductCondition.Visible = false;
                MinBagPriceCondition.Visible = false;
            }

            if (DropDownList1.SelectedItem.Text == "And Condition") 
            {
                AndCondition.Visible = true;
                OrCondition.Visible = false;
                MaxProductCondition.Visible = false;
                MinProductCondition.Visible = false;
                MinBagPriceCondition.Visible = false;

            }
            if (DropDownList1.SelectedItem.Text == "Or Condition")
            {
                AndCondition.Visible = false;
                OrCondition.Visible = true;
                MaxProductCondition.Visible = false;
                MinProductCondition.Visible = false;
                MinBagPriceCondition.Visible = false;
            }
            if (DropDownList1.SelectedItem.Text == "Max Product Condition")
            {

                AndCondition.Visible = false;
                OrCondition.Visible = false;
                MaxProductCondition.Visible = true;
                MinProductCondition.Visible = false;
                MinBagPriceCondition.Visible = false;
            }
            if (DropDownList1.SelectedItem.Text == "Min Product Condition")
            {

                AndCondition.Visible = false;
                OrCondition.Visible = false;
                MaxProductCondition.Visible = false;
                MinProductCondition.Visible = true;
                MinBagPriceCondition.Visible = false;
            }
            if (DropDownList1.SelectedItem.Text == "Min Bag Price Condition")
            {

                AndCondition.Visible = false;
                OrCondition.Visible = false;
                MaxProductCondition.Visible = false;
                MinProductCondition.Visible = false;
                MinBagPriceCondition.Visible = true;
            }
        }
        protected void ButtonAndCond_Click(object sender, EventArgs e)
        {
            string str = new UserHandler().AddDiscountConditionDiscountConditionAnd(Session["storeId"].ToString(), Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                AndLabel.Visible = true;
                AndLabel.Text = "Condition Added";
                return;
            }
            else
            {
                AndLabel.Visible = true;
                AndLabel.Text =str;
                return;
            }
        }

        protected void ButtonOr_Click(object sender, EventArgs e)
        {
            string str = new UserHandler().AddDiscountConditionDiscountConditionOr(Session["storeId"].ToString(), Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                OrLabel.Visible = true;
                OrLabel.Text = "Condition Added";
                return;
            }
            else
            {
                OrLabel.Visible = true;
                OrLabel.Text = str;
                return;
            }
        }

        protected void ButtonMaxPro_Click(object sender, EventArgs e)
        {
            if (MaxQuantityBox.Text.Trim().Length == 0 | ProductIdBoxMaxPro.Text.Trim().Length==0)
            {
                MaxProductLabel.Visible = true;
                MaxProductLabel.Text = "One Of The Fields Are Empty";
                return;
            }
            if(!int.TryParse(MaxQuantityBox.Text,out _))
            {
                MaxProductLabel.Visible = true;
                MaxProductLabel.Text = "Max Quantity must be a number";
                return;
            }
            string str = new UserHandler().AddDiscountConditionMaxProductCondition(Session["storeId"].ToString(), MaxQuantityBox.Text, ProductIdBoxMaxPro.Text, Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                MaxProductLabel.Visible = true;
                MaxProductLabel.Text = "Condition Added";
                return;
            }
            else
            {
                MaxProductLabel.Visible = true;
                MaxProductLabel.Text = str;
                return;
            }
        }

        protected void ButtonMinPro_Click(object sender, EventArgs e)
        {
            if (MinQuantityBox.Text.Trim().Length == 0 | ProductIdBoxMinQuan.Text.Trim().Length == 0)
            {
                MinQuanLabel.Visible = true;
                MinQuanLabel.Text = "One Of The Fields Are Empty";
                return;
            }
            if (!int.TryParse(MinQuantityBox.Text, out _))
            {
                MinQuanLabel.Visible = true;
                MinQuanLabel.Text = "Min Quantity must be a number";
                return;
            }
            string str = new UserHandler().AddDiscountConditionMinProductCondition(Session["storeId"].ToString(), MinQuantityBox.Text, ProductIdBoxMinQuan.Text, Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                MinQuanLabel.Visible = true;
                MinQuanLabel.Text = "Condition Added";
                return;
            }
            else
            {
                MinQuanLabel.Visible = true;
                MinQuanLabel.Text = str;
                return;
            }
        }

        protected void ButtonMinBagPrice_Click(object sender, EventArgs e)
        {
            if (MinPriceBox.Text.Trim().Length == 0)
            {
                MinBagPriceLabel.Visible = true;
                MinBagPriceLabel.Text = "Min Price Field can't be empty";
                return;
            }
            if (!int.TryParse(MinPriceBox.Text.Split('.').First(), out _))
            {
                MinBagPriceLabel.Visible = true;
                MinBagPriceLabel.Text = "Min Price must be a number";
                return;
            }
            string str = new UserHandler().AddDiscountConditionMinBagPriceCondition(Session["storeId"].ToString(), MinPriceBox.Text, Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                MinBagPriceLabel.Visible = true;
                MinBagPriceLabel.Text = "Condition Added";
                return;
            }
            else
            {
                MinBagPriceLabel.Visible = true;
                MinBagPriceLabel.Text =str;
                return;
            }
        }
    }
}