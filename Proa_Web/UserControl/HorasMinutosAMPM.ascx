<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HorasMinutosAMPM.ascx.cs" Inherits="Proa_Web.UserControl.HorasMinutosAMPM" %>

<style type="text/css">
    .auto-style1 {
  
    }
    .auto-style2 {
        color: #007BFF;
    }
    .auto-style3 {
        color: #000000;
    }
    </style>
<table class="auto-style1">
    <tr>
        <td class="auto-style2">
            <strong>|</strong></td>
        <td>
            <strong><em>
            <asp:label ID="lblTitulo" runat="server" Text="Título" CssClass="auto-style3" Width="80px"></asp:label> </em></strong> </td>
        <td class="auto-style2">
            &nbsp;</td>
        <td>
            <asp:DropDownList ID="ddlHH" runat="server" class="btn btn-primary btn-sm dropdown-toggle"  Width="56px">
            </asp:DropDownList>
        </td>
        <td>:
        </td>
        <td>
            <asp:DropDownList ID="ddlMM" runat="server" class="btn btn-primary btn-sm dropdown-toggle" Width="56px">
            </asp:DropDownList></td>
        <td>
            <asp:DropDownList ID="ddlAMPM" runat="server" CssClass="btn btn-primary btn-sm dropdown-toggle">
                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>



