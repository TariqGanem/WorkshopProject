﻿<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Client.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style13 {
            width: 955px;
        }
        .auto-style14 {
            width: 184px;
        }
        .auto-style15 {
            width: 653px;
        }
        .auto-style16 {
            margin-left: 3px;
        }
        .auto-style23 {
            width: 1px;
        }
        .auto-style24 {
            width: 7px;
        }
        .label-class {
            display: flex;
            align-items: center;
        }
        .td {
            vertical-align: middle;
            text-align: center;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style5">
        <tr>
            <td class="auto-style14">
                            &nbsp;</td>
            <td class="auto-style15">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            <asp:Label ID="LabelproductName" runat="server" Text="Name : "></asp:Label>
                        </td>
            <td class="auto-style15">
                            <asp:Label ID="LabelproductName0" runat="server"></asp:Label>
                        </td>
            <td class="auto-style13">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            <asp:Label ID="Labelbarcode" runat="server" Text="Id : "></asp:Label>
                        </td>
            <td class="auto-style15">
                            <asp:Label ID="Labelbarcode0" runat="server"></asp:Label>
                        </td>
            <td class="auto-style13">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            quantity</td>
            <td class="auto-style15">
                            <asp:Label ID="LabelQuantity" runat="server"></asp:Label>
                        </td>
            <td class="auto-style13">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            <asp:Label ID="Labelcategories" runat="server" Text="Category : "></asp:Label>
                        </td>
            <td class="auto-style15">
                            <asp:Label ID="Labelcategories0" runat="server"></asp:Label>
                        </td>
            <td class="auto-style13">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            <asp:Label ID="Labelprice" runat="server" Text="Price : "></asp:Label>
                        </td>
            <td class="auto-style15">
                            <asp:Label ID="Labelprice0" runat="server"></asp:Label>
                        </td>
            <td class="auto-style13">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
             <td class="auto-style8">
                <asp:TextBox ID="TextBoxRate" CssClass="txt" placeholder="Rate Store" runat="server" Style="text-align: center" Height="10px" Width="94px"></asp:TextBox></td>
            <td style="auto-style8" class="auto-style19">
                <asp:Button ID="btnRateProduct" runat="server" Text="Rate" CssClass="auto_class8" Height="30px" Width="50px" OnClick="btnRateStore_OnClick"   />
            </td>     
             <td><asp:Label ID="LabelErrorRateProduct" runat="server" Text="Error:"></asp:Label></td>
        </tr>
        <tr>
             <td class="auto-style8">
                <asp:TextBox ID="priceTxt" CssClass="txt" placeholder="Offer Price" runat="server" Style="text-align: center" Height="10px" Width="96px"></asp:TextBox></td>
            <td class="auto-style8">
                <asp:TextBox ID="AmountTxt" CssClass="txt" placeholder="Offer Amount" runat="server" Style="text-align: center" Height="10px" Width="100px"></asp:TextBox></td>
            <td style="auto-style8" class="auto-style19">
                <asp:Button ID="SendOfferBtn" runat="server" Text="Send Offer" CssClass="auto_class8" Height="30px" Width="100px" OnClick="SendOfferBtn_OnClick"   />
            </td>     
             <td><asp:Label ID="ErrorOfferLabel" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style14">
                            &nbsp;</td>
            <td class="auto-style15">&nbsp;</td>
            <td class="auto-style13">
                            <asp:Label ID="Labelprice1" runat="server" Width="90px" Text="Qty :"></asp:Label>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="img/-.PNG" runat="server" CssClass="auto-style16" Height="30px" Width="30px" OnClick="ImageButton1_Click" />
                            <asp:Label ID="Label1"  runat="server" CssClass="td" Height="30px" Text="0" Width="54px"></asp:Label>
                        <asp:ImageButton ID="ImageButton2" ImageUrl="img/plus.PNG" runat="server" CssClass="auto-style16" Height="30px" Width="30px" OnClick="ImageButton2_Click" />
                            <table class="auto-style5">
                                <tr>
                                    <td class="auto-style24">
                                        &nbsp;</td>
                                    <td class="auto-style24" >
                                        &nbsp;</td>
                                    <td class="auto-style23">
                                        &nbsp;</td>
                                </tr>
                            </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">&nbsp;</td>
            <td class="auto-style15">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
            <td>
  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ><img src="img/add_to_cart.PNG" style="width: auto; height: auto;" /></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
