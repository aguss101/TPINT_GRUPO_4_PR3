<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Vistas.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login</title>
    <link rel="stylesheet" href="Login/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <h2><span class="clinica">Clínica</span> <span class="frgp">FRGP</span></h2>

        <asp:Panel runat="server" DefaultButton="btnLogin" CssClass="login-container">
            <asp:Label ID="LblTitle2" runat="server" Text="Iniciar sesión en Clínica FRGP" AssociatedControlID="lblTitle2" />
            <asp:TextBox ID="txbUser" runat="server" Width="286px" />
            <asp:TextBox ID="TxbPassword" runat="server" TextMode="Password" Width="286px" />
            <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
            <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red"></asp:Label>
        </asp:Panel>
    </form>
</body>
</html>