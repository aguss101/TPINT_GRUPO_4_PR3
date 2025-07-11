<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Vistas.Admin" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Administrador</title>
    <link rel="stylesheet" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="tablaDiv">
            <tr>
                <td class="columnaIndex">
                    <table class="auto-style3">
                        <tr>
                            <td class="columnaIndex-PanelAdmin">Panel Admin</td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button runat="server" Text="Administrar Médicos" CssClass="btn-index" ID="btnAdministrarMedicos" OnClick="btnAdministrarMedicos_Click" /></td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="Administrar_Pacientes" runat="server" Text=" Administrar Pacientes" CssClass="btn-index" OnClick="Administrar_Pacientes_Click" /></td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="Administrar_Turnos" runat="server" Text="Administrar Turnos" CssClass="btn-index" OnClick="Administrar_Turnos_Click" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="hl-CerrarSesion">
                                <asp:HyperLink ID="hlCerrarSesion" runat="server" NavigateUrl="~/Login/Login.aspx">Cerrar Sesión</asp:HyperLink></td>
                        </tr>
                    </table>
                </td>
                <td class="columnaBody">
                    <table class="auto-style10">
                        <tr>
                            <td class="auto-style22" colspan="3">
                                <div class="titulo-con-nombre">
                                    <h2><span class="clinica">Clínica</span> <span class="frgp">FRGP</span></h2>
                                    <div class="sidebarUser">
                                        <asp:Label ID="lblUser" runat="server" Font-Bold="True" Font-Names="Calibri" Text="Administrador"></asp:Label></div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style29">REPORTES</td>
                            <td class="auto-style29"></td>
                            <td class="auto-style30"></td>
                        </tr>
                        <tr>
                            <td class="auto-style20">
                                <asp:DropDownList ID="ddlReportes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlReportes_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                    <asp:ListItem Value="1">Medico</asp:ListItem>
                                    <asp:ListItem Value="2">Pacientes</asp:ListItem>
                                    <asp:ListItem Value="3">Turnos</asp:ListItem>
                                </asp:DropDownList>
                        <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                            <asp:ListItem Value="1"></asp:ListItem>
                            <asp:ListItem Value="2"></asp:ListItem>
                            <asp:ListItem Value="3"></asp:ListItem>
                        </asp:DropDownList>
                            </td>
                            <td class="no-select">&nbsp;</td>
                            <td class="auto-style28">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="no-select">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td class="no-select">
                                <asp:Chart ID="graficoReportes" runat="server" Height="413px" Width="589px">
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" Name="Series1"
                                            IsValueShownAsLabel="True">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX Interval="1" IsLabelAutoFit="True">
                                                <LabelStyle Angle="-45" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                                </td>
                            <td class="auto-style28"></td>
                        </tr>
                        <tr>
                            <td class="no-select">&nbsp;</td>
                            <td class="no-select">
                            </td>
                            <td class="auto-style28"></td>
                        </tr>
                        <tr>
                            <td class="no-select">&nbsp;</td>
                            <td class="no-select">&nbsp;</td>
                            <td class="auto-style28">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="no-select"></td>
                            <td class="no-select"></td>
                            <td class="auto-style28"></td>
                        </tr>
                        <tr>
                            <td class="no-select"></td>
                            <td class="no-select"></td>
                            <td class="auto-style28"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
