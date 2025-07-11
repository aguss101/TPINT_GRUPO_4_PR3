<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelMedico.aspx.cs" Inherits="Vistas.PanelMedico" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Médico</title>
    <link rel="stylesheet" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="tablaDiv">
            <tr>
                <td class="columnaIndex">
                    <table class="auto-style3">
                        <tr>
                            <td class="columnaIndex-Bienvenida">Bienvenido/a</td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button runat="server" Text="Ver Turnos" CssClass="btn-index" /></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"></td>
                        </tr>
                        <tr>
                            <td class="hl-CambiarContrasenia">&nbsp;</td>
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
                                        <asp:Label ID="lblUser" runat="server" Font-Bold="True" Font-Names="Calibri" Text="Médico"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style39">
                                <h2>Ver Turnos</h2>
                            </td>
                            <td class="auto-style30"></td>
                            <td class="auto-style31"></td>
                        </tr>
                        <tr>
                            <td class="auto-style42">
                                <div class="div-ddl">
                                    <h3 class="auto-style35" style="margin: 0;">Búsqueda por:</h3>
                                    <asp:DropDownList ID="ddlBusqueda_Turnos" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBusqueda_Turnos_SelectedIndexChanged">
                                        <asp:ListItem Text="--Seleccione una búsqueda--" Value="-1" Selected="True" />
                                        <asp:ListItem>Apellido</asp:ListItem>
                                        <asp:ListItem>DNI</asp:ListItem>
                                        <asp:ListItem>Estado</asp:ListItem>
                                        <asp:ListItem>Fecha</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td class="auto-style43"></td>
                        </tr>
                        <tr>
                            <td class="botonera" colspan="2">
                                <asp:MultiView ID="mwBusqueda" runat="server">
                                    <asp:View ID="vwPorApellido" runat="server">
                                        <div>
                                            <table class="auto-style32">
                                                <tr>
                                                    <td class="auto-style36">Ingrese apellido del paciente:</td>
                                                    <td>
                                                        <asp:TextBox ID="txbPorApellido" runat="server" AutoPostBack="True" OnTextChanged="txbPorApellido_TextChanged"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwPorDNI" runat="server">
                                        <div>
                                            <table class="auto-style32">
                                                <tr>
                                                    <td class="auto-style36">Ingrese DNI del paciente:</td>
                                                    <td>
                                                        <asp:TextBox ID="txbPorDNI" runat="server" OnTextChanged="txbPorDNI_TextChanged"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwEstado" runat="server">
                                        <asp:DropDownList ID="ddlEstados" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged">
                                            <asp:ListItem Value="TODOS">-- Seleccionar --</asp:ListItem>
                                            <asp:ListItem>PRESENTE</asp:ListItem>
                                            <asp:ListItem>AUSENTE</asp:ListItem>
                                            <asp:ListItem>PENDIENTE</asp:ListItem>
                                            <asp:ListItem>CANCELADO</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:View>
                                    <asp:View ID="vwPorFecha" runat="server">
                                        <div>
                                            Seleccione una fecha<br />
                                            <br />
                                            <asp:Calendar ID="calendarMedico" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" OnSelectionChanged="calendarMedico_SelectionChanged" SelectedDate="06/26/2025 16:42:26" TitleFormat="Month" Width="400px">
                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                                                <DayStyle Width="14%" />
                                                <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                                                <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                                                <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                                                <TodayDayStyle BackColor="#CCCC99" />
                                            </asp:Calendar>
                                            <br />
                                        </div>
                                    </asp:View>
                                </asp:MultiView></td>
                            <td class="auto-style31">&nbsp;</td>
                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="botonera" colspan="2">
                                
                                            <table class="auto-style32">
                                                <tr>
                                                    <td class="auto-style38">
                                                        <h3>&nbsp;</h3>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                
                                <asp:GridView ID="gvTurnos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
    CssClass="modern-grid" 
    DataKeyNames="Legajo,FechaPactada,observacion,diagnostico"
    OnPageIndexChanging="gvTurnos_PageIndexChanging" Width="100%">

    <AlternatingRowStyle CssClass="alt-row" />

    <Columns>
        <asp:TemplateField HeaderText="Seleccionar">
            <ItemTemplate>
                <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="True" OnCheckedChanged="chkSeleccionar_CheckedChanged" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="Paciente" HeaderText="Paciente" />
        <asp:BoundField DataField="DNIPaciente" HeaderText="DNI" />
        <asp:BoundField DataField="FechaPactada" HeaderText="Fecha y Hora" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
        <asp:BoundField DataField="EstadoDescripcion" HeaderText="Estado" />

        <asp:TemplateField HeaderText="Observación">
            <ItemTemplate>
                <asp:TextBox ID="txbObs" runat="server" Width="100%" CssClass="form-control" Text='<%# Eval("observacion") %>' Enabled="False" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Diagnóstico">
            <ItemTemplate>
                <asp:TextBox ID="txbDiagnostico" runat="server" Width="100%" CssClass="form-control" Text='<%# Eval("diagnostico") %>' Enabled="False" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Estado de la Consulta">
            <ItemTemplate>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control" Visible="False">
                    <asp:ListItem Text="Ausente" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Presente" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Enviar Diagnóstico">
            <ItemTemplate>
                <asp:Button ID="btnEnviarDiagnostico" runat="server" Text="Enviar" CssClass="btn-enviar" OnClick="btnEnviarDiagnostico_Click" Visible="False" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

    <PagerStyle CssClass="grid-pager" />
</asp:GridView>

                            </td>
                            <td class="auto-style31">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style40">

                                &nbsp;</td>
                            <td class="no-select">&nbsp;</td>
                            <td class="auto-style28">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style41">&nbsp;</td>
                            <td class="no-select">&nbsp;</td>
                            <td class="auto-style28"></td>
                        </tr>
                        <tr>
                            <td class="auto-style29" colspan="2"></td>
                            <td class="auto-style28"></td>
                        </tr>
                        <tr>
                            <td class="auto-style41">&nbsp;</td>
                            <td class="no-select">&nbsp;</td>
                            <td class="auto-style28">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style41"></td>
                            <td class="no-select"></td>
                            <td class="auto-style28"></td>
                        </tr>
                        <tr>
                            <td class="auto-style41"></td>
                            <td class="no-select"></td>
                            <td class="auto-style28"></td>
                        </tr>
                    </table>
    </form>
</body>
</html>
