<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Vistas.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial;
            background-color: #f2f2f2;
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .login-container {
            background-color: #ffffff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            width: 300px;
            text-align: center;
        }

        .login-container label,
        .login-container input,
        .login-container button {
            display: block;
            width: 100%;
            margin-bottom: 15px;
        }

        .login-container input {
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 6px;
        }

        .login-container button {
            padding: 10px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 6px;
            cursor: pointer;
        }

        .login-container button:hover {
            background-color: #0056b3;
        }
        .clinica {
            color: #666666;
            font-weight: bold;
        }
        .frgp {
            color: #00aaff;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2><span class="clinica">Clinica</span> <span class="frgp">FRGP</span></h2>
        <asp:Panel runat="server" DefaultButton="btnLogin" CssClass="login-container">
            <asp:Label ID="lblUser" runat="server" Text="Usuario" AssociatedControlID="tbxUser" />
            <asp:TextBox ID="tbxUser" runat="server" />

            <asp:Label ID="lblPassword" runat="server" Text="Contraseña" AssociatedControlID="TxbPassword" />
            <asp:TextBox ID="TxbPassword" runat="server" TextMode="Password" />

            

            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click" />
            <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red"></asp:Label>
        </asp:Panel>
    </form>
</body>
</html>
