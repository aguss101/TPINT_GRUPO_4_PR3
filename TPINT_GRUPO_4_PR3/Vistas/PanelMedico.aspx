<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelMedico.aspx.cs" Inherits="Vistas.PanelMedico" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Médico</title>
    <style>
        html, body, form {
            margin: 0;
            font-family: Arial, sans-serif;
            height: 100%;
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
        /*
        .sidebarUser {
            top: 16px;
            right: 16px;
            width: 150px;
            background-color: #f4f4f4;
            border-radius: 12.5px;
            padding: 8px;
            box-shadow: 0 0 4px rgba(0, 0, 0, 0.1);
        }*/
        .sidebarUser {
            width: fit-content;
            background-color: #f4f4f4;
            border-radius: 12.5px;
            padding: 8px 12px;
            box-shadow: 0 0 4px rgba(0, 0, 0, 0.1);
            margin-top: 4px;
        }

        .titulo-con-nombre {
            display: flex;
            flex-direction: column;
            align-items: flex-end; /* Alinea todo a la derecha */
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
            height: 100%;
        }

        .no-select {
            user-select: none;
        }

        .columnaIndex {
            width: 157px;
            background-color: #ffffff;
            height: 100%;
        }

        .auto-style3 {
            width: 100%;
            height: 100%;
        }

        .auto-style4 {
            width: 151px;
            height: 310px;
        }

        .columnaIndex-Bienvenida {
            width: 151px;
            text-align: center;
            font-size: 16px;
            font-weight: bold;
            height: 80px;
        }

        .columnaBody {
            background-color: #e6e6e6;
            padding: 0px 30px 30px 30px;
            font-weight: bold;
            height: 100%;
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
            height: 100%;
            transition: background-color 0.3s, color 0.3s;
        }

        .td-btn-index {
            width: 151px;
            height: 10px;
        }

        .btn-index:hover {
            background-color: #4da9ff;
            color: white;
            border-color: #4da9ff;
        }

        .hl-CerrarSesion {
            width: 151px;
            height: 26px;
            text-align: center;
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
        .div-ddl {
            display: flex;
            align-items: center;
            gap: 10px; /*separa visualmente el h3 y el DropDownList.*/
        }

        .auto-style10 {
            width: 100%;
            height: 100%;
            margin-top: 0px;
        }

        .auto-style22 {
            text-align: right;
            height: 26px;
        }

        .auto-style28 {
            user-select: none;
            width: 34px;
        }

        .auto-style29 {
            user-select: none;
            }
        .table {}
        .auto-style30 {
            user-select: none;
            height: 23px;
        }
        .auto-style31 {
            user-select: none;
            width: 34px;
            height: 23px;
        }
        .auto-style32 {
            width: 100%;
        }
        .auto-style35 {
            width: 136px;
            height: 20px;
        }
        .auto-style36 {
            width: 227px;
        }
        .auto-style38 {
            height: 25px;
        }
        .auto-style39 {
            user-select: none;
            height: 23px;
            width: 913px;
        }
        .auto-style40 {
            width: 913px;
        }
        .auto-style41 {
            user-select: none;
            width: 913px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="tablaDiv">
            <tr><td class="columnaIndex">
                    <table class="auto-style3">
                        <tr><td class="columnaIndex-Bienvenida">Bienvenido,<br/>[nombre del medico]</td></tr>
                        <tr><td class="td-btn-index"><asp:Button runat="server" Text="Ver Turnos" CssClass="btn-index"/></td></tr>
                        <tr><td class="td-btn-index"><asp:Button ID="Button2" runat="server" Text="Cargar Diagnostico" CssClass="btn-index"/></td></tr>
                        <tr><td class="td-btn-index"><asp:Button ID="Button3" runat="server" Text="Ver Historial" CssClass="btn-index"/></td></tr>
                        <tr><td class="auto-style4"></td></tr>
                        <tr><td class="hl-CambiarContrasenia">&nbsp;</td></tr>
                        <tr><td class="hl-CerrarSesion"><asp:HyperLink ID="hlCerrarSesion" runat="server" NavigateUrl="~/Login.aspx">Cerrar Sesión</asp:HyperLink></td></tr>
                    </table>
                </td>
                <td class="columnaBody">
                    <table class="auto-style10">
                        <tr><td class="auto-style22" colspan="3"><div class="titulo-con-nombre"><h2><span class="clinica">Clínica</span> <span class="frgp">FRGP</span></h2><div class="sidebarUser">
                            <asp:Label ID="lblUser" runat="server" Font-Bold="True" Font-Names="Calibri" Text="Médico"></asp:Label></div></div></td></tr>
                        <tr><td class="auto-style39"><h2>Ver Turnos</h2></td><td class="auto-style30"></td><td class="auto-style31"></td></tr>
                        <tr><td class="auto-style40"><div class="div-ddl">
                            <h3 class="auto-style35" style="margin: 0;">Búsqueda por:</h3>
                            <asp:DropDownList ID="ddlBusqueda" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBusqueda_SelectedIndexChanged">
                                <asp:ListItem Text="--Seleccione una búsqueda--" Value="-1" Selected="True" />
                                <asp:ListItem>Apellido</asp:ListItem>
                                <asp:ListItem>DNI</asp:ListItem>
                                <asp:ListItem>Fecha</asp:ListItem>
                            </asp:DropDownList></div></td><td>&nbsp;</td></tr>
                        <tr><td class="botonera" colspan="2">
                            <asp:MultiView ID="mwBusqueda" runat="server">
                            <asp:View ID="vwPorApellido" runat="server">
                            <div><table class="auto-style32"><tr><td class="auto-style36">Ingrese apellido del paciente:</td><td>
                                <asp:TextBox ID="txbPorApellido" runat="server" AutoPostBack="True" OnTextChanged="txbPorApellido_TextChanged"></asp:TextBox></td></tr></table>
                            </div></asp:View>
                                <asp:View ID="vwPorDNI" runat="server"><div><table class="auto-style32">
                                    <tr><td class="auto-style36">Ingrese DNI del paciente:</td>
                                        <td><asp:TextBox ID="txbPorDNI" runat="server" OnTextChanged="txbPorDNI_TextChanged"></asp:TextBox></td>
                                        </tr></table></div></asp:View>
                                <asp:view ID="vwPorFecha" runat="server"><div><table class="auto-style32">
                                    <tr><td class="auto-style38"><h3>Seleccione una fecha</h3></td></tr><tr><td>
                                    <asp:Calendar ID="calendarMedico" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" OnSelectionChanged="calendarMedico_SelectionChanged" SelectedDate="06/26/2025 16:42:26" TitleFormat="Month" Width="400px">
                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                                    <DayStyle Width="14%" />
                                    <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                                    <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                                    <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                                    <TodayDayStyle BackColor="#CCCC99" /></asp:Calendar></td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table></div></asp:view></asp:MultiView></td><td class="auto-style31">&nbsp;</td></tr>
                        <tr><td class="botonera" colspan="2"><asp:GridView ID="gvTurnos" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table" DataKeyNames="Legajo,FechaPactada" ForeColor="Black" GridLines="Horizontal" OnPageIndexChanging="gvTurnos_PageIndexChanging" Width="976px">
                            <Columns>
                            <asp:BoundField DataField="Paciente" HeaderText="Paciente" />
                            <asp:BoundField DataField="DNIPaciente" HeaderText="DNI" />
                            <asp:BoundField DataField="FechaPactada" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="Fecha y Hora" />
                            <asp:TemplateField HeaderText="Observación">
                            <ItemTemplate>
                            <asp:TextBox ID="txtObs" runat="server" Width="100%" />
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EstadoDescripcion" HeaderText="Estado" />
                            <asp:TemplateField HeaderText="Diagnostico">
                            <ItemTemplate>
                            <asp:TextBox ID="txbDiagnostico" runat="server" Width="100%" />
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones">
                            <itemtemplate>
                            <asp:Button ID="btnPresente" runat="server" CommandArgument="Presente" CssClass="btn-td" OnClick="btnEstado_Click" Text="Presente" />
                            <asp:Button ID="btnAusente" runat="server" CommandArgument="Ausente" CssClass="btn-td" OnClick="btnEstado_Click" Text="Ausente" />
                            </itemtemplate>
                            </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView></td><td class="auto-style31">&nbsp;</td></tr>
                        <tr><td class="auto-style40">&nbsp;</td><td class="no-select">&nbsp;</td><td class="auto-style28">&nbsp;</td></tr>
                        <tr><td class="auto-style41"><asp:Button ID="btnCargar" runat="server" OnClick="btnCargar_Click" Text="Cargar Turno" /> </td><td class="no-select">&nbsp;</td><td class="auto-style28"></td></tr>
                        <tr><td class="auto-style29" colspan="2"></td><td class="auto-style28"></td></tr>
                        <tr><td class="auto-style41">&nbsp;</td><td class="no-select">&nbsp;</td><td class="auto-style28">&nbsp;</td></tr>
                        <tr><td class="auto-style41"></td><td class="no-select"></td><td class="auto-style28"></td></tr>
                        <tr><td class="auto-style41"></td><td class="no-select"></td><td class="auto-style28"></td></tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
