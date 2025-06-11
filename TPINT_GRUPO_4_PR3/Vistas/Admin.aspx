<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Vistas.Admin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Administrador</title>
    <style>
        body {
            margin: 0;
            font-family: Arial, sans-serif;
        }

        .container {
            display: flex;
            height: 100vh;
        }

        .sidebar {
            width: 200px;
            background-color: #f4f4f4;
            padding: 20px;
            box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
        }

        .sidebar h2 {
            font-size: 18px;
            margin-bottom: 20px;
        }

        .sidebar .btn {
            display: block;
            width: 100%;
            margin-bottom: 10px;
            padding: 10px;
            background-color: #007bff;
            color: white;
            border: none;
            cursor: pointer;
            border-radius: 4px;
            text-align: left;
        }

        .sidebar .btn:hover {
            background-color: #0056b3;
        }

        .main-content {
            flex-grow: 1;
            padding: 30px;
            background-color: #fff;
        }

        .header {
            font-size: 22px;
            font-weight: bold;
            margin-bottom: 20px;
        }

        .content-box {
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 4px;
            background-color: #fafafa;
            min-height: 300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
           
            <div class="sidebar">
                <h2>Panel Admin</h2>
                <asp:Button ID="btnMedicos" runat="server" Text="Administrar Médicos" CssClass="btn" />
                <asp:Button ID="btnPacientes" runat="server" Text="Administrar Pacientes" CssClass="btn" />
                <asp:Button ID="btnTurnos" runat="server" Text="Administrar Turnos" CssClass="btn" />
            </div>

            
            <div class="main-content">
                <div class="header">Bienvenido, Administrador</div>
                <div class="content-box">
                    <p>Seleccioná una opción del menú para comenzar.</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>