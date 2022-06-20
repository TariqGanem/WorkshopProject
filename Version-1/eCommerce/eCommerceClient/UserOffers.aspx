<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="True" CodeBehind="UserOffers.aspx.cs" Inherits="Client.UserOffers" %>

<asp:Content ID="Content1"  ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style13 {
        height: 5px;
    }
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DataList ID="DataListOffers" runat="server" OnItemCommand="DataListOffers_ItemCommand1" BackColor="White" BorderStyle="Double" CellPadding="4"  RepeatDirection="Horizontal" RepeatColumns="3" BorderColor="#336666" BorderWidth="3px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" GridLines="Horizontal">
        <FooterStyle BackColor="White" ForeColor="#333333" />
        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        <SelectedItemStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
        <ItemStyle BackColor="White" ForeColor="#333333" />
        <ItemTemplate>
                       
                                <table align="left" style="width: 250px; background-color: #f5f5f5; border: 1px solid #CCC;">

                                    <tr>
                                        <td style="height: 10px;"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                                    <span style="font-weight: 700; font-size: 14px;">OfferId: <%#Eval("OfferId") %> </span></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px;"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-weight: 700; font-size: 20px;">ProductId: <%#Eval("ProductId") %> </span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 16px;">UserId: <%#Eval("UserId") %></span></td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <span style="font-size: 16px;">StoreId: <%#Eval("StoreId") %></span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 16px;">Quantity: <%#Eval("Amount") %></span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 16px;">Price: <%#Eval("Price") %></span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 16px;">CounterOffer: <%#Eval("CounterOfferPrice") %></span></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>                     
                                        <td><asp:label ID="ErrorLabel" runat="server"></asp:label></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style13">
                                            <asp:Button ID="ButtonAccept" runat="server" Text="Accept" CommandArgument='<%#Eval("UserId")+","+ Eval("OfferId")%>' CommandName="AcceptCounterOffer" Height="39px" Width="90px" ></asp:Button>
                                            <asp:Button ID="ButtonDecline" runat="server" Text="Decline" CommandArgument='<%#Eval("UserId")+","+ Eval("OfferId")%>' CommandName="DeclineCounterOffer" Height="39px" Width="90px" ></asp:Button>

                                        </td>
                                        <tr>
                                            <td style="height: 10px;"></td>
                                        </tr>
                                    </tr>

                                    <tr>
                                    </tr>
                                </table>
                        
                        </ItemTemplate>
    </asp:DataList>
</asp:Content>


