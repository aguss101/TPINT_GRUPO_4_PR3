<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Medico.aspx.cs" Inherits="Vistas.Medico" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Médico</title>
    <style>
        body {
            margin: 0;
            font-family: Arial, sans-serif;
        }

        .container {
            display: flex;
            height: 100vh;
            border-radius: 25px; 
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
        .containerCartel {
            display: flex;
            flex-direction: row;
            width: 100%;
            justify-content: right;
        }
        .sidebarUser {
            position: absolute;
            top: 16px;
            right: 16px;
            width: 150px;
            background-color: #f4f4f4;
            border-radius: 12.5px;
            padding: 8px;
            box-shadow: 0 0 4px rgba(0, 0, 0, 0.1);
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
        .tablaDiv {
            width: 100%;
            height: 349px;
        }
        .no-select {
            user-select: none;
        }
        .columnaIndex {
            width: 157px;
            background-color:#ffffff;
        }
        .auto-style3 {
            width: 100%;
            height: 621px;
        }
        .auto-style4 {
            width: 151px;
            height: 310px;
        }
        .columnaIndex-Bienvenida {
            width: 151px;
            text-align: center;
            font-size: 16px;
            font-weight:bold;
        }
        .columnaBody {
            background-color:#e6e6e6;
            padding: 0px 30px 30px 30px;
            font-weight: bold;
        }
        .btn-index {
            background-color: white;
            color: #595959;
            border: none;
            padding: 10px 16px;
            font-size: 16px;
            border-radius: 2px;
            cursor: pointer;
            font-weight: normal;
            width: 100%;
            transition: background-color 0.3s, color 0.3s;
        }

        .btn-index:hover {
            background-color: #4da9ff;
            color: white;
            border-color: #4da9ff;
        }
        
        .hl-CambiarContrasenia {
            width: 151px;
            height: 24px;
            text-align:center;
        }
        .hl-CerrarSesion {
            width: 151px;
            height: 26px;
            text-align:center;
        }
        .nombre-clinica {
            text-align: right;
        }
        .clinica {
            color: #666666;
            font-weight: bold;
        }
        .frgp {
            color: #00aaff;
            font-weight: bold;
        }
        .auto-style9 {
            width: 151px;
            height: 20px;
        }
        .auto-style10 {
            width: 100%;
            height: 623px;
            margin-top: 0px;
        }
        .auto-style18 {
            height: 25px;
            width: 420px;
        }
        .auto-style20 {
            width: 420px;
        }
                
        .auto-style22 {
            text-align: right;
            height: 26px;
        }
        
        .auto-style23 {
            width: 420px;
            height: 74px;
        }
        .auto-style26 {
            user-select: none;
            height: 74px;
        }
        .auto-style27 {
            width: 151px;
            text-align: center;
            font-size: 16px;
            font-weight: bold;
            height: 138px;
        }
        .auto-style28 {
            user-select: none;
            height: 25px;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tablaDiv">
        <tr>
            <td class="columnaIndex">
                <table class="auto-style3">
                    <tr>
                        <td class="auto-style27">Bienvenido,<br />
                            [nombre del medico]</td>
                    </tr>
                    <tr>
                        <td class="auto-style9">
                            <asp:Button runat="server" Text="Ver Turnos" CssClass="btn-index" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style9">
                            <asp:Button ID="Button2" runat="server" Text="Cargar Diagnostico" CssClass="btn-index" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style9">
                            <asp:Button ID="Button3" runat="server" Text="Ver Historial" CssClass="btn-index" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4"></td>
                    </tr>
                    <tr>
                        <td class="hl-CambiarContrasenia">
                            <asp:HyperLink ID="hlCambiarContrasenia" runat="server" NavigateUrl="~/Admin.aspx">Cambiar Contraseña</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="hl-CerrarSesion">
                            <asp:HyperLink ID="hlCerrarSesion" runat="server" NavigateUrl="~/Admin.aspx">Cerrar Sesión</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="columnaBody">
                <table class="auto-style10">
                    <tr>
                        <td class="auto-style22" colspan="3">
                            <h2><span class="clinica">Clínica</span> <span class="frgp">FRGP</span></h2>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style18">Información del médico</td>
                        <td class="auto-style28"></td>
                        <td class="auto-style28">
                        </td>
                    </tr>
                    <tr>
                        <td class="no-select"></td>
                        <td class="no-select"></td>
                        <td class="no-select"></td>
                    </tr>
                    <tr>
                        <td class="auto-style20">Turnos hoy</td>
                        <td class="no-select">&nbsp;</td>
                        <td class="no-select">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="no-select"></td>
                        <td class="no-select"></td>
                        <td class="no-select"></td>
                    </tr>
                    <tr>
                        <td class="auto-style23">Solicitud de turnos</td>
                        <td class="no-select"></td>
                        <td class="no-select"></td>
                    </tr>
                    <tr>
                        <td class="no-select">&nbsp;</td>
                        <td class="no-select">&nbsp;</td>
                        <td class="no-select">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="no-select"></td>
                        <td class="no-select"></td>
                        <td class="no-select"></td>
                    </tr>
                    <tr>
                        <td class="auto-style26"></td>
                        <td class="no-select"></td>
                        <td class="no-select"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
