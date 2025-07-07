<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar_Turnos.aspx.cs" Inherits="Vistas.admin.Administrar_Turnos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Administrador</title>
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

        .columnaIndex-PanelAdmin {
            width: 151px;
            text-align: center;
            font-size: 16px;
            font-weight: bold;
            height: 80px;
        }

        .columnaBody {
            background-color: #e6e6e6;
            padding: 0px 30px 30px 30px;
            font-weight: normal;
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
            height: 93%;
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

        .auto-style31 {
            user-select: none;
            width: 34px;
            height: 24px;
        }

        .auto-style32 {
            user-select: none;
            height: 24px;
            width: 196px;
        }

        .auto-style33 {
            user-select: none;
            width: 196px;
            height: 51px;
        }

        .auto-style34 {
            user-select: none;
            height: 51px;
            width: 909px;
        }

        .auto-style35 {
            user-select: none;
            width: 34px;
            height: 51px;
        }

        .auto-style36 {
            user-select: none;
            height: 25px;
        }

        .auto-style37 {
            width: 157px;
            background-color: #ffffff;
            height: 108%;
        }

        .auto-style38 {
            background-color: #e6e6e6;
            padding: 0px 30px 30px 30px;
            font-weight: normal;
            height: 108%;
        }
        .auto-style39 {
            user-select: none;
            height: 24px;
            width: 909px;
        }
        .auto-style40 {
            user-select: none;
            width: 909px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="tablaDiv">
            <tr>
                <td class="auto-style37">
                    <table class="auto-style3">
                        <tr>
                            <td class="td-btn-index">
                            <asp:Button ID="btnAdmin" runat="server" Text="Panel Admin" CssClass="btn-index" CommandArgument="Admin" OnCommand="navigateButton_Click" /></td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="btnAdministrarMedicos" runat="server" Text="Administrar Médicos" CssClass="btn-index" CommandArgument="Medicos" OnCommand="navigateButton_Click" /></td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="btnAdministrarPacientes" runat="server" Text=" Administrar Pacientes" CssClass="btn-index" CommandArgument="Pacientes" OnCommand="navigateButton_Click" /></td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="btnAdministrarTurnos" runat="server" Text="Administrar Turnos" CssClass="btn-index" CommandArgument="Turnos" OnCommand="navigateButton_Click" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="hl-CerrarSesion">
                                <asp:HyperLink ID="hlCerrarSesion" runat="server" NavigateUrl="~/Login.aspx">Cerrar Sesión</asp:HyperLink></td>
                        </tr>
                    </table>
                </td>
                <td class="auto-style38">

                    <asp:MultiView ID="mvAsignarTurnos" runat="server">
                        <asp:View ID="vwAsignar" runat="server">
                            <table class="auto-style10">
                                <tr>
                                    <td class="auto-style22" colspan="3">
                                        <div class="titulo-con-nombre">
                                            <h2><span class="clinica">Clínica</span> <span class="frgp">FRGP</span></h2>
                                            <div class="sidebarUser">
                                                <asp:Label ID="lblUser" runat="server" Font-Bold="True" Font-Names="Calibri" Text="Administrador"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style33">
                                        <h3>Asignación de Turnos</h3>
                                    </td>
                                    <td class="auto-style34"></td>
                                    <td class="auto-style35"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style32">Especialidad</td>
                                    <td class="auto-style39">
                                        <asp:DropDownList ID="ddlEspecialidad" runat="server" AutoPostBack="True" DataTextField="descripcion" DataValueField="idEspecialidad" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style31"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style32">Médico</td>
                                    <td class="auto-style39">
                                        <asp:DropDownList ID="ddlMedico" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMedico_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td class="auto-style31"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style32">Fecha</td>
                                    <td class="auto-style39">
                                        <asp:DropDownList ID="ddlFecha" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFecha_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td class="auto-style31">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style32">Horario</td>
                                    <td class="auto-style39">
                                        <asp:DropDownList ID="ddlHora" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                                    <td class="auto-style31"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style32">Paciente</td>
                                    <td class="auto-style39"><asp:DropDownList ID="ddlPaciente" runat="server" 
                                                AutoPostBack="True"
                                                DataSourceID="dbPaciente" 
                                                DataTextField="Paciente" 
                                                DataValueField="DNI"
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">--Seleccionar Paciente--</asp:ListItem>
                                            </asp:DropDownList>
                                        <asp:SqlDataSource ID="dbPaciente" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT PA.DNI, P_Pac.nombre + ' ' + P_Pac.apellido AS Paciente FROM Persona P_Pac INNER JOIN Paciente PA ON PA.DNI        = P_Pac.DNI"></asp:SqlDataSource>
                                        <asp:Label ID="lblActionTurno" runat="server"></asp:Label>
                                    </td>
                                    <td class="auto-style31"></td>
                                </tr>
                                <tr>
                                    <td class="no-select"></td>
                                    <td class="auto-style40"></td>
                                    <td class="auto-style36"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style32">
                                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Turno" Width="188px" OnClick="btnRegistrar_Click" /></td>
                                    <td class="auto-style39">
                                        <asp:Button ID="btnMod" runat="server" Text="Modificar Turno" OnClick="btnMod_Click" Width="188px" />
                                        <asp:Button ID="btnBaja" runat="server" Text="Dar de baja" Width="188px" OnClick="btnBaja_Click" OnClientClick="return confirm('¿Está seguro que desea dar de baja el turno?');" /> 
                                        <asp:DropDownList ID="ddlFiltrarTurnosPor" runat="server" AutoPostBack="true" Height="16px" OnSelectedIndexChanged="ddlFiltrarTurnosPor_SelectedIndexChanged" Style="margin-bottom: 0px; margin-top: 0px;" Width="465px">
                                            <asp:ListItem Value="FechaASC">Fecha A</asp:ListItem>
                                            <asp:ListItem Value="FechaDES">Fecha D</asp:ListItem>
                                            <asp:ListItem Value="MedicoASC">Medico A</asp:ListItem>
                                            <asp:ListItem Value="MedicoDES">Medico D</asp:ListItem>
                                            <asp:ListItem Value="DNIPacienteASC">DNI Paciente A</asp:ListItem>
                                            <asp:ListItem Value="DNIPacienteDES">DNI Paciente D</asp:ListItem>
                                            <asp:ListItem>PRESENTES</asp:ListItem>
                                            <asp:ListItem>AUSENTES</asp:ListItem>
                                            <asp:ListItem>PENDIENTES</asp:ListItem>
                                            <asp:ListItem>CANCELADOS</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                        <td class="no-select"></td>
                                        <td class="auto-style40">&nbsp;</td>
                                        <td class="no-select"></td>
                                    <td class="auto-style31">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="no-select"></td>
                                    <td class="auto-style40">
                                        <asp:GridView ID="gvTurnos" runat="server" AllowPaging="true" PageSize="10" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnPageIndexChanging="gvTurnos_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Seleccionar">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="True" OnCheckedChanged="chkSeleccionar_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Legajo" HeaderText="Legajo" />
                                                <asp:BoundField DataField="DNIPaciente" HeaderText="DNI-Paciente" />
                                                <asp:BoundField DataField="FechaPactada" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncode="false" />
                                                <asp:BoundField DataField="EstadoDescripcion" HeaderText="Estado" />
                                                <asp:BoundField DataField="Paciente" HeaderText="Paciente" />
                                                <asp:BoundField DataField="ObraSocial" HeaderText="ObraSocial" />
                                                <asp:BoundField DataField="Medico" HeaderText="Medico" />
                                                <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
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

                                    </td>
                                    <td class="auto-style28"></td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="vwModificar" runat="server">
                            <asp:DropDownList ID="ddlModFecha" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModFecha_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlModHorario" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:TextBox ID="txtObservacion" runat="server"></asp:TextBox>
                            &nbsp;
                            <asp:TextBox ID="txtDiagnostico" runat="server"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnModAplicarCambios" runat="server" OnClick="btnModAplicarCambios_click" Text="Aceptar Cambios" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
            <tr>
                <td class="auto-style37">&nbsp;</td>
                <td class="auto-style38">&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
