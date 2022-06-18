<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Client.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style21 {
            width: 149px;
        }
        .auto-style22 {
            width: 427px;
        }
        .auto-style24 {
            margin-left: 585px;
        }
        .auto-style25 {
            width: 228px;
        }
        .auto-style26 {
            width: 316px;
        }
        .auto-style27 {
            margin-top: 40px;
        }
        .auto-style28 {
            width: 77px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:DataList ID="Data_cart" OnItemCommand="DataListCart_ItemCommand1" runat="server" Width="100%" Height="335px" OnSelectedIndexChanged="Data_cart_SelectedIndexChanged1" CssClass="auto-style27">
                                            <ItemTemplate>
                                                <table align="center" style="width: 100%; border-bottom: 1px solid #CCC">
                                                    <tr>
                                                        <td style="width: 80px;"><span style="font-size: 22px;"><%#Eval("Name")%></span></td>
                                                        <td style="text-align: center; width: 60px;">
                                                        <td style="width: 10px;"></td>
                                                        <td style="width: 302px">
                                                            <table align="left" class="auto-style22">
                                                                <tr>
                                                                    <td style="text-align: left;" class="auto-style21"><span style="font-size: 14px; font-weight: 700;"><%#Eval("storeid")%></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left;" class="auto-style21"><span style="font-size: 14px;">Price : <%#Eval("Price") %>$</span></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="auto-style21">
                                                                         <td class="auto-style13">
                                                                            <asp:Label ID="Labelprice1" runat="server" Width="90px" Text="Qty :"></asp:Label>
                                                                        <asp:ImageButton ID="ImageButton1" ImageUrl="img/-.PNG" runat="server" CssClass="auto-style16" Height="30px" Width="30px" CommandArgument='<%#Eval("Name")+","+Eval("storeid")+","+Eval("Quantity")   %>' CommandName="down_command" />
                                                                            <asp:Label ID="Label1"  runat="server" CssClass="td" Height="30px" Text=<%#Eval("Quantity")%> Width="54px"></asp:Label>
                                                                        <asp:ImageButton ID="ImageButton2" ImageUrl="img/plus.PNG" runat="server" CssClass="auto-style16" Height="30px" Width="30px"  CommandArgument='<%#Eval("Name")+","+Eval("storeid")+","+Eval("Quantity")     %>' CommandName="up_command" />
                                                                            
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:Button ID="ButtonDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("Name")+","+ Eval("storeid") %>' CommandName="Delete_command" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    <table>
                                        <tr>
                                            <td class="auto-style28">

                                                <asp:Label ID="LabelCartPrice" runat="server" Text="Total Cart Price"></asp:Label>

                                            </td>
                                                <td class="auto-style28">

                                                <asp:Label ID="LabelActualPrice" runat="server" Text="TotalActualPrice"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style28">

                                                <asp:Label ID="LabelCreditcard" runat="server" Text="credit card number "></asp:Label>

                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBoxCreditcard" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
<td>

                                                <asp:Label ID="LabelId" runat="server" Text="Id"></asp:Label>

                                            </td>
                                            <td class="auto-style26">
                                                
                                                <asp:TextBox ID="TextBoxId" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="auto-style28">

                                                <asp:Label ID="MonthLabel" runat="server" Text="month "></asp:Label>

                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBoxMonth" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
                                             <td>

                                                <asp:Label ID="LabelName" runat="server" Text="Name "></asp:Label>

                                            </td>
                                            <td class="auto-style26">

                                                <asp:TextBox ID="TextBoxName" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style28">

                                                <asp:Label ID="LabelYear" runat="server" Text="Year"></asp:Label>

                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBoxYear" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
                                                <td>

                                                <asp:Label ID="LabelAddress" runat="server" Text="Address"></asp:Label>

                                            </td>
                                            <td class="auto-style26">

                                                <asp:TextBox ID="TextBoxAdress" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style28">

                                                <asp:Label ID="LabelHolder" runat="server" Text="Holder"></asp:Label>

                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBoxHolder" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
                                             <td>

                                                <asp:Label ID="LabelCity" runat="server" Text="City"></asp:Label>

                                            </td>
                                            <td class="auto-style26">

                                                <asp:TextBox ID="TextBoxCity" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style28">

                                                <asp:Label ID="LabelCVV" runat="server" Text="cvv"></asp:Label>

                                            </td>
                                            <td>

                                                <asp:TextBox ID="TextBoxCVV" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
                                             <td>

                                                <asp:Label ID="LabelCountry" runat="server" Text="Country"></asp:Label>

                                            </td>
                                            <td class="auto-style26">

                                                <asp:TextBox ID="TextBoxCountry" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style28">

                                            </td>
                                            <td>

                                            </td>
                                            <td>

                                                <asp:Label ID="LabelZip" runat="server" Text="ZIP"></asp:Label>

                                            </td>
                                            <td class="auto-style26">

                                                <asp:TextBox ID="TextBoxZip" runat="server" Height="16px" Width="263px"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style28">

                                            </td>
                                            <td>
                                                     <asp:Label ID="LabelError" runat="server" Text="PROBLEMS WITH CREDENTIALS"></asp:Label>

                                            </td>
                                            <td class="auto-style25">

                                            </td>
                                            <td class="auto-style26" >

                                                <asp:Button ID="Button3" runat="server" Text="Buy Now" Width="79px" CssClass="auto-style24" OnClick="Button3_Click" />

                                            </td>
                                        </tr>
                                    </table>
    </asp:Content>
