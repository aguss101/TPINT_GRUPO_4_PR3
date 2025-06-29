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
            font-family: Arial, sans-serif;
            background: url('images/login-bg.jpg') no-repeat center center fixed;
            background-size: cover;
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        h2 { 
            margin-bottom: 20px;
            text-align: center;
        }

        .login-container {
            background-color: #ffffff;
            padding: 40px;
            border-radius: 25px; 
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.15);
            width: 320px;
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
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 12px;
            font-size: 14px;
        }

        .login-container input:focus {
            border-color: #007bff;
            outline: none;
        }

        .login-container button {
            padding: 10px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 12px;
            font-weight: bold;
            cursor: pointer;
        }

        .clinica {
            color: #666666;
            font-weight: bold;
        }

        .frgp {
            color: #00aaff;
            font-weight: bold;
        }

        #lblError {
            margin-top: 10px;
            color: red;
            font-size: 13px;
        }
    </style>
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