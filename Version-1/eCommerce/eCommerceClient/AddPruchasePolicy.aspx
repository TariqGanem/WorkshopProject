<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="AddPruchasePolicy.aspx.cs" Inherits="Client.AddPurchasePolicy" %>
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
    <asp:Label ID="Label1" runat="server" Text="Select which policy you want to add"></asp:Label>
    <table class="auto-style5">
        <tr>
            <td class="auto-style15">
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                      <asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="And Policy" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Or Policy" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Conditional Policy" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Max Product Policy" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Min Product Policy" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Min Age Policy" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Restricted Hours Policy" Value="7"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td class="auto-style15">
                <table id="AndPolicy"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style24">
                <asp:Label ID="Label2" runat="server" Text="And Policy"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td class="auto-style23">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style24">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button1" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonAdd_Click" Height="39px" Width="90px" />
                        </td>
                        <td class="auto-style23"><asp:label ID="AndLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr>
            <td class="auto-style15">
                <table id="OrPolicy"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style24">
                <asp:Label ID="Label4" runat="server" Text="Or Policy"></asp:Label>
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
                <table id="ConditionalPolicy"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style24">
                <asp:Label ID="Label5" runat="server" Text="Conditional Policy"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td class="auto-style25">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style24">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button3" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonCond_Click" Height="39px" Width="90px" />
                        </td>
                        <td class="auto-style25"><asp:label ID="ConditionalLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="auto-style15">
                <table id="MaxProductPolicy"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label6" runat="server" Text="Max Product Policy"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="ProductIdLabel" runat="server" Text="ProductId : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="ProductIdBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="MaxLabel" runat="server" Text="Max : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="MaxBox" runat="server"></asp:TextBox>
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
                <table id="MinProudctPolicy"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label7" runat="server" Text="Min Product Policy"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Label8" runat="server" Text="ProductId : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="ProductIdBoxMin" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Label9" runat="server" Text="Min : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="MinBox" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button5" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonMinPro_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="MinProductLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

         <tr>
            <td class="auto-style15">
                <table id="MinAgePolicy"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label10" runat="server" Text="Min Age Policy"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Label11" runat="server" Text="Age : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="AgeBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button6" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonMinAge_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="MinAgeLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td class="auto-style15">
                <table id="RestrictedHoursPolicy"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label12" runat="server" Text="Restricted Hours Policy"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Label13" runat="server" Text="StartRestrict : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="StartRestrictBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Label14" runat="server" Text="EndRestrict : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="EndRestrictBox" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                     <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Label3" runat="server" Text="ProductId : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="ProductIdBoxRes" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="Button7" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonRestrictedHours_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="RestrictedHoursLabel" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
</asp:Content>
