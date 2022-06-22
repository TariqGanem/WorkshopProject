<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="AddDiscountCondition.aspx.cs" Inherits="Client.AddDiscountCondition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style13 {
            width: 165px;
        }
        .auto-style14 {
            width: 272px;
        }
        .auto-style15 {
            width: 698px;
        }
        .auto-style16 {
            width: 430px;
        }
        .auto-style17 {
            width: 246px;
        }
        .auto-style18 {
            width: 243px;
        }
        .auto-style19 {
            width: 245px;
        }
        .auto-style20 {
            width: 246px;
            height: 27px;
        }
        .auto-style21 {
            height: 27px;
        }
        .auto-style22 {
            width: 430px;
            height: 27px;
        }
        .addstylee {
            background-color: limegreen;
        }
        .removestylee{
             background-color:red ;
        }
        .btnsuccess {
            background: #1F60F0; 
            color: #eee;

        }
        .btnsuccesshover {
            background: #111; 
            color: #eee; 

        }
        .pgalogin {
background: none repeat scroll 0 0 #2EA2CC;
border-color: #0074A2;
box-shadow: 0 1px 0 rgba(120, 200, 230, 0.5) inset, 0 1px 0 rgba(0, 0, 0, 0.15);
color: #FFFFFF;
text-decoration: none;
text-align: center;
vertical-align: middle;
border-radius: 3px;
padding: 4px;
height: 27px;
font-size: 14px;
margin-bottom: 16px;
}
        .auto-style23 {
            width: 200px;
        }
        .auto-style24 {
            width: 835px;
        }
        .auto-style25 {
            width: 86px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Select which condition you want to add"></asp:Label>
    <table class="auto-style5">
        <tr>
            <td class="auto-style15">
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                      <asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="And Condition" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Or Condition" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Max Product Condition" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Min Product Condition" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Min Bag Price Condition" Value="5"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="auto-style15">
                <table id="AndCondition"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style24">
                <asp:Label ID="Label2" runat="server" Text="And Condition"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td class="auto-style23">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style24">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button1" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonAndCond_Click" Height="39px" Width="90px" />
                        </td>
                        <td class="auto-style23"><asp:label ID="AndLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr>
            <td class="auto-style15">
                <table id="OrCondition"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style24">
                <asp:Label ID="Label4" runat="server" Text="Or Condition"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td class="auto-style23">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style24">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button2" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonOr_Click" Height="39px" Width="90px" />
                        </td>
                        <td class="auto-style23"><asp:label ID="OrLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>
        

        <tr>
            <td class="auto-style15">
                <table id="MaxProductCondition"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label6" runat="server" Text="Max Product Condition"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="ProductIdLabel" runat="server" Text="Max Quantity "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="MaxQuantityBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="MaxLabel" runat="server" Text="ProductId : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="ProductIdBoxMaxPro" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button4" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonMaxPro_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="MaxProductLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="auto-style15">
                <table id="MinProductCondition"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label3" runat="server" Text="Min Product Condition"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Label5" runat="server" Text="Min Quantity "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="MinQuantityBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Label7" runat="server" Text="ProductId : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="ProductIdBoxMinQuan" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button3" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonMinPro_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="MinQuanLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

         <tr>
            <td class="auto-style15">
                <table id="MinBagPriceCondition"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label8" runat="server" Text="Min Bag Price Condition"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Label9" runat="server" Text="Min Price "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="MinPriceBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button5" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonMinBagPrice_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="MinBagPriceLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

        </table>
</asp:Content>
