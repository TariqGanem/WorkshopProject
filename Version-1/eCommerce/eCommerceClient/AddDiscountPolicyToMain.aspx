<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="AddDiscountPolicyToMain.aspx.cs" Inherits="Client.AddDiscountPolicyToMain" %>
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
        .auto-style26 {
            width: 833px;
        }
        .auto-style27 {
            width: 266px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Select which policy you want to add"></asp:Label>
    <table class="auto-style5">
        <tr>
            <td class="auto-style15">
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                      <asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Visible Discount" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Discreet Discount" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Conditional Discount" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Addition Discount" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="And Discount" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Max Discount" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Min Discount" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="Or Discount" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="Xor Discount" Value="9"></asp:ListItem>


                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="auto-style15">
                <table id="ConditionalDiscount"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style24">
                <asp:Label ID="Label2" runat="server" Text="Conditional Discount"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td class="auto-style23">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style24">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button1" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonAConditional_Click" Height="39px" Width="90px" />
                        </td>
                        <td class="auto-style23"><asp:label ID="ConditionalLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr>
            <td class="auto-style15">
                <table id="AndDiscount"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style24">
                <asp:Label ID="Label4" runat="server" Text="And Discount"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td class="auto-style23">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style24">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button2" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonAnd_Click" Height="39px" Width="90px" />
                        </td>
                        <td class="auto-style23"><asp:label ID="AndLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr>
            <td class="auto-style15">
                <table id="AdditionDiscount"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style24">
                <asp:Label ID="Label5" runat="server" Text="Addition Discount"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td class="auto-style25">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style24">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button3" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonAddition_Click" Height="39px" Width="90px" />
                        </td>
                        <td class="auto-style25"><asp:label ID="additionlabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="auto-style15">
                <table id="MaxDiscount"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label6" runat="server" Text="Max Discount"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button4" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonMax_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="MaxLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="auto-style15">
                <table id="MinDiscount"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label7" runat="server" Text="Min Discount"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button5" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonMin_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="MinLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

         <tr>
            <td class="auto-style15">
                <table id="OrDiscount"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label10" runat="server" Text="Or Discount"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button6" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonOr_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="OrLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

         <tr>
            <td class="auto-style15">
                <table id="XorDiscount"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label8" runat="server" Text="Xor Discount"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button8" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonXor_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="XorLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="auto-style15">
                <table id="VisibleDiscount"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style26">
                <asp:Label ID="Label12" runat="server" Text="Visible Discount"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td class="auto-style27">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style26">
                            <asp:Label ID="Label13" runat="server" Text="ExpirationDate  "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="ExpirationDateBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style26">
                            <asp:Label ID="Label14" runat="server" Text="Percentage : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="PercentageBox" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style27">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style26">
                            <asp:Label ID="Label9" runat="server" Text="TargetType : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="TargetTypeBox" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style27">&nbsp;</td>
                    </tr>
                     <tr>
                        <td class="auto-style26">
                            <asp:Label ID="Label3" runat="server" Text="TargetParams  "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="TargetParamsBox" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style27">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style26">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button7" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonVisibleDis_Click" Height="39px" Width="90px" />
                        </td>
                        <td class="auto-style27"><asp:label ID="VisibleLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="auto-style15">
                <table id="DiscreetDiscount"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label11" runat="server" Text="Discreet Discount"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style26">
                            <asp:Label ID="Label16" runat="server" Text="DiscountCode  "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="DisCodeBox" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style27">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button9" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonDiscreet_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="DiscreetLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
</asp:Content>
