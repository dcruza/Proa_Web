<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HorasMinutos.ascx.cs" Inherits="Proa_Web.UserControl.HorasMinutos" %>

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
    </tr>
</table>



