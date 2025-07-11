<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar_Turnos.aspx.cs" Inherits="Vistas.admin.Administrar_Turnos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Administrador</title>
    <link rel="stylesheet" href="Administrar_Turnos/style.css" />
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
                                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Turno" CssClass="btn-index" Width="188px" OnClick="btnRegistrar_Click" /></td>
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
                                        <asp:GridView ID="gvTurnos" runat="server" AllowPaging="true" PageSize="10" 
    AutoGenerateColumns="False" OnPageIndexChanging="gvTurnos_PageIndexChanging"
    CssClass="modern-grid" PagerStyle-CssClass="grid-pager">
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
        <asp:BoundField DataField="ObraSocial" HeaderText="Obra Social" />
        <asp:BoundField DataField="Medico" HeaderText="Médico" />
        <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
    </Columns>
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
