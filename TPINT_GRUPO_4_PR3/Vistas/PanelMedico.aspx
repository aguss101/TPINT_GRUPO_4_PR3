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

        .auto-style10 {
            width: 100%;
            height: 100%;
            margin-top: 0px;
        }

        .auto-style20 {
            width: 430px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="tablaDiv">
            <tr>
                <td class="columnaIndex">
                    <table class="auto-style3">
                        <tr>
                            <td class="columnaIndex-Bienvenida">Bienvenido,<br />
                                [nombre del medico]</td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button runat="server" Text="Ver Turnos" CssClass="btn-index" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="Button2" runat="server" Text="Cargar Diagnostico" CssClass="btn-index" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="Button3" runat="server" Text="Ver Historial" CssClass="btn-index" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4"></td>
                        </tr>
                        <tr>
                            <td class="hl-CambiarContrasenia">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="hl-CerrarSesion">
                                <asp:HyperLink ID="hlCerrarSesion" runat="server" NavigateUrl="~/Login.aspx">Cerrar Sesión</asp:HyperLink>
                            </td>
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
                                        <asp:Label ID="lblUser" runat="server" Font-Bold="True" Font-Names="Calibri" Text="Médico"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style29">&nbsp;</td>
                            <td class="no-select"></td>
                            <td class="auto-style28"></td>
                        </tr>
                        <tr>
                            <td class="auto-style20">Turnos hoy</td>
                            <td class="no-select">&nbsp;</td>
                            <td class="auto-style28">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style29">
                                <asp:Calendar ID="calendarMedico" runat="server" BackColor="White" BorderColor="Black" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" Width="400px" DayNameFormat="Shortest" OnSelectionChanged="calendarMedico_SelectionChanged" TitleFormat="Month" SelectedDate="06/26/2025 16:42:26">
                                    <DayHeaderStyle Font-Bold="True" Font-Size="7pt" BackColor="#CCCCCC" ForeColor="#333333" Height="10pt" />
                                    <DayStyle Width="14%" />
                                    <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                                    <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                                    <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                                    <TodayDayStyle BackColor="#CCCC99" />
                                </asp:Calendar>
                            </td>
                            <td class="no-select">&nbsp;</td>
                            <td class="auto-style28"></td>
                        </tr>
                        <tr>
                            <td class="auto-style29" colspan="2">



                                <asp:GridView ID="gvTurnos" runat="server"
                                    AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    OnPageIndexChanging="gvTurnos_PageIndexChanging"
                                    DataKeyNames="Legajo,FechaPactada"
                                    CssClass="table" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="976px">
                                    <Columns>

                                        <asp:BoundField DataField="Paciente" HeaderText="Paciente" />
                                        <asp:BoundField DataField="FechaPactada" HeaderText="Fecha y Hora"
                                            DataFormatString="{0:dd/MM/yyyy HH:mm}" />


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
                                                    <asp:Button ID="btnPresente" runat="server" Text="Presente"
                                                        CommandArgument="Presente"
                                                        OnClick="btnEstado_Click"
                                                        CssClass="btn-td" />
                                                    <asp:Button ID="btnAusente" runat="server" Text="Ausente"
                                                        CommandArgument="Ausente"
                                                        OnClick="btnEstado_Click"
                                                        CssClass="btn-td" />
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
                                </asp:GridView>

                                <asp:Button ID="btnCargar" runat="server" OnClick="btnCargar_Click" Text="Cargar Turno" />

                            </td>
                            <td class="auto-style28"></td>
                        </tr>
                        <tr>
                            <td class="auto-style29">&nbsp;</td>
                            <td class="no-select">&nbsp;</td>
                            <td class="auto-style28">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style29"></td>
                            <td class="no-select"></td>
                            <td class="auto-style28"></td>
                        </tr>
                        <tr>
                            <td class="auto-style29"></td>
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
