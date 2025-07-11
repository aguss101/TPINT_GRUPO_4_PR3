<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar_Pacientes.aspx.cs" Inherits="Vistas.admin.Administrar_Pacientes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Administrador</title>
    <link rel="stylesheet" href="Administrar_Pacientes/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="tablaDiv">
            <tr>
                <td class="columnaIndex">
                    <table class="auto-style3">
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="btnAdmin" runat="server" Text="Panel Admin" CssClass="btn-index" CommandArgument="Admin" OnCommand="navigateButton_Click" /></td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="btnAdministrarMedico" runat="server" Text="Administrar Médicos" CssClass="btn-index" CommandArgument="Medicos" OnCommand="navigateButton_Click" /></td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="btnAdministrarPaciente" runat="server" Text=" Administrar Pacientes" CssClass="btn-index" CommandArgument="Pacientes" OnCommand="navigateButton_Click" /></td>
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
                <td class="columnaBody">
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
                            <td class="auto-style34" colspan="2">
                                <h2>Administrar Paciente</h2>
                            </td>
                            <td class="auto-style35"></td>
                        </tr>
                        <tr>
                            <td class="botonera" colspan="2">
                                <asp:Button ID="btnAlta" runat="server" Text="Registrar" CssClass="btn-td" OnClick="btnAlta_Click" />
                                <asp:Button ID="btnBaja" runat="server" Text="Dar de baja" CssClass="btn-td" OnClick="btnBaja_Click" OnClientClick="return confirm('¿Está seguro que desea dar de baja el paciente?');" />
                                <asp:Button ID="btnMod" runat="server" Text="Modificar" CssClass="btn-td" OnClick="btnMod_Click" />
                                <asp:Button ID="btnLectura" runat="server" Text="Listar" CssClass="btn-td" OnClick="btnLectura_Click" /></td>
                            <td class="auto-style41"></td>
                        </tr>
                        <tr>
                            <td class="auto-style43" colspan="2">
                                <asp:MultiView ID="mvFormularios" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="vwAlta" runat="server">
                                        <h3>Registrar Paciente</h3>
                                        <div>
                                            <table class="auto-style44">
                                                <tr>
                                                    <td class="auto-style32">DNI:</td>
                                                    <td class="auto-style52">
                                                        <asp:TextBox ID="txbDni" runat="server" ValidationGroup="AltaPaciente"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvDni" runat="server" ControlToValidate="txbDni" ErrorMessage="* Campo obligatorio" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaPaciente" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txbDni" ValidationExpression="^\d{8}$" ErrorMessage="* 8 digitos" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Nombre</td>
                                                    <td class="auto-style30">
                                                        <asp:TextBox ID="txbNombre" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txbNombre" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                        <asp:RegularExpressionValidator ID="revnombre" runat="server" ControlToValidate="txbNombre" Display="Dynamic" ErrorMessage="* Solo letras" ForeColor="Red" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$" ValidationGroup="AltaMedico" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Apellido</td>
                                                    <td class="auto-style30">
                                                        <asp:TextBox ID="txbApellido" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvapellido" runat="server" ControlToValidate="txbNombre" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="AltaPaciente" />
                                                        <asp:RegularExpressionValidator ID="revapellido" runat="server" ControlToValidate="txbApellido" Display="Dynamic" ErrorMessage="* Solo letras" ForeColor="Red" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$" ValidationGroup="AltaPaciente" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Sexo</td>
                                                    <td class="auto-style30">
                                                        <asp:DropDownList ID="ddlGenero" runat="server" AutoPostBack="True" DataSourceID="dbGenero" DataTextField="descripcion" DataValueField="idSexo"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvgenero" runat="server" ControlToValidate="ddlGenero" Display="Dynamic" ErrorMessage="* Seleccione un genero" ForeColor="Red" InitialValue="" ValidationGroup="AltaPaciente" />
                                                        <asp:SqlDataSource ID="dbGenero" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT * FROM [Sexos]"></asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Fecha de nacimiento</td>
                                                    <td class="auto-style30">
                                                        <asp:TextBox ID="txbFechaNacimiento" runat="server" TextMode="Date"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvfechanac" runat="server" ControlToValidate="txbFechaNacimiento" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="AltaPaciente" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Nacionalidad</td>
                                                    <td class="auto-style30">
                                                        <asp:DropDownList ID="ddlNacionalidad" runat="server" DataSourceID="dbNacionalidades" DataTextField="gentilicio" DataValueField="gentilicio"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvpais" runat="server" ControlToValidate="ddlNacionalidad" Display="Dynamic" ErrorMessage="* Seleccione un país" ForeColor="Red" InitialValue="" ValidationGroup="AltaPaciente" />
                                                        <asp:SqlDataSource ID="dbNacionalidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT * FROM [Paises]"></asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Provincia:</td>
                                                    <td class="auto-style30">
                                                        <asp:DropDownList
                                                            ID="ddlProvincia" runat="server" AutoPostBack="True" DataSourceID="dbProvincias" DataTextField="nombreProvincia" DataValueField="idProvincia" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvprovincia" runat="server" ControlToValidate="ddlProvincia" Display="Dynamic" ErrorMessage="* Seleccione una provincia" ForeColor="Red" InitialValue="" ValidationGroup="AltaPaciente" />
                                                        <asp:SqlDataSource ID="dbProvincias" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT idProvincia, nombreProvincia FROM Provincias"></asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Localidad:</td>
                                                    <td class="auto-style30">
                                                        <asp:DropDownList ID="ddlLocalidades" runat="server" DataSourceID="dbLocalidades" DataTextField="nombreLocalidad" DataValueField="idLocalidad">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvlocalidades" runat="server" ControlToValidate="ddlLocalidades" Display="Dynamic" ErrorMessage="* Seleccione una localidad" ForeColor="Red" InitialValue="" ValidationGroup="AltaPaciente" />
                                                        <asp:SqlDataSource ID="dbLocalidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT idLocalidad, nombreLocalidad FROM Localidades WHERE idProvincia = @idProvincia">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="ddlProvincia" Name="idProvincia" PropertyName="SelectedValue" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Dirección</td>
                                                    <td class="auto-style30">
                                                        <asp:TextBox ID="txbDireccion" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvdireccion" runat="server" ControlToValidate="txbDireccion" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="AltaPaciente" />
                                                        <asp:RegularExpressionValidator ID="revdireccion" runat="server" ControlToValidate="txbDireccion" Display="Dynamic" ErrorMessage="* Formato inválido" ForeColor="Red" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9 ]+$" ValidationGroup="AltaPaciente" />
                                                    </td>
                                                    <td class="auto-style31">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Obra Social</td>
                                                    <td class="auto-style30">
                                                        <asp:DropDownList ID="ddlObraSocial" runat="server" AutoPostBack="True" DataSourceID="dbObraSocial" DataTextField="nombre" DataValueField="idObraSocial">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="dbObraSocial" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT [nombre], [idObraSocial] FROM [ObraSocial]"></asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Teléfono</td>
                                                    <td class="auto-style30">
                                                        <asp:TextBox ID="txbTelefono" runat="server"></asp:TextBox>
                                                        <asp:Label ID="lblCondicioness0" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" Text="Formato: 11-1234-5678 // 11 1234 5678"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvtelefono" runat="server" ControlToValidate="txbTelefono" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="AltaPaciente" />
                                                        <asp:RegularExpressionValidator ID="revtelefono" runat="server" ControlToValidate="txbTelefono" Display="Dynamic" ErrorMessage="Invalido" ForeColor="Red" ValidationExpression="^^\d{2}[-\s]\d{4}[-\s]\d{4}$" ValidationGroup="AltaPaciente" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style37">Correo</td>
                                                    <td class="no-select">
                                                        <asp:TextBox ID="txbCorreo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvcorreo" runat="server" ControlToValidate="txbCorreo" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="AltaPaciente" />
                                                        <asp:RegularExpressionValidator ID="revcorreo" runat="server" ControlToValidate="txbCorreo" Display="Dynamic" ErrorMessage="* Formato inválido" ForeColor="Red" ValidationExpression="^[\w\-\.]+@([\w-]+\.)+[\w-]{2,4}$" ValidationGroup="AltaPaciente" />
                                                    </td>
                                                    <td class="auto-style36"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">
                                                        <asp:Button ID="btnRegistrarPaciente" runat="server" OnClick="btnRegistrarPaciente_Click" Text="Registrar paciente" Width="188px" CssClass="btn-td" ValidationGroup="AltaPaciente" />
                                                    </td>
                                                    <td class="auto-style30">
                                                        <asp:Label ID="lblAddUserState" runat="server" Visible="False"></asp:Label>
                                                    </td>
                                                    <td class="auto-style31">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwBaja" runat="server">
                                        <h3>Dar de baja Paciente</h3>
                                        <div></div>
                                    </asp:View>
                                    <asp:View ID="vwModificar" runat="server">
                                        <h3>Modificar Paciente</h3>
                                        <div>
                                            <table class="auto-style44">
                                                <tr>
                                                    <td class="auto-style32">DNI</td>
                                                    <td class="auto-style30">
                                                        <asp:TextBox ID="txbModDni" Enabled="false" runat="server" ValidationGroup="ModPaciente"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModDni" runat="server" ControlToValidate="txbModDni" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModPaciente" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Nombre</td>
                                                    <td class="auto-style30">
                                                        <asp:TextBox ID="txbModNombre" runat="server" ValidationGroup="ModPaciente"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModNombre" runat="server" ControlToValidate="txbModNombre" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModPaciente" />
                                                        <asp:RegularExpressionValidator ID="revModNombre" runat="server" ControlToValidate="txbModNombre" Display="Dynamic" ErrorMessage="* Solo letras" ForeColor="Red" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$" ValidationGroup="ModPaciente" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Apellido</td>
                                                    <td class="auto-style30">
                                                        <asp:TextBox ID="txbModApellido" runat="server" ValidationGroup="ModPaciente"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModApellido" runat="server" ControlToValidate="txbModApellido" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModPaciente" />
                                                        <asp:RegularExpressionValidator ID="revModApellido" runat="server" ControlToValidate="txbModApellido" Display="Dynamic" ErrorMessage="* Solo letras" ForeColor="Red" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$" ValidationGroup="ModPaciente" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Sexo</td>
                                                    <td class="auto-style30">
                                                        <asp:DropDownList ID="ddlModGenero" runat="server" AutoPostBack="True" DataSourceID="dbModGenero" DataTextField="descripcion" DataValueField="idSexo"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="dbModGenero" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT * FROM [Sexos]"></asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Fecha de nacimiento:</td>
                                                    <td class="auto-style30">
                                                        <asp:TextBox ID="txbModFechaNacimiento" runat="server" ValidationGroup="ModPaciente" TextMode="Date"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModFechaNac" runat="server" ControlToValidate="txbModFechaNacimiento" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModPaciente" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>

                                                <tr>
                                                    <td class="auto-style32">Nacionalidad</td>
                                                    <td class="auto-style30">
                                                        <asp:DropDownList ID="ddlModNacionalidad" runat="server" DataSourceID="dbModNacionalidades" DataTextField="gentilicio" DataValueField="gentilicio">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="dbModNacionalidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT [idPais], [gentilicio] FROM [Paises]"></asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31">&nbsp;</td>
                                                </tr>
                                                <caption>
                                                    <tr>
                                                        <td class="auto-style32">Provincia</td>
                                                        <td class="auto-style30">
                                                            <asp:DropDownList ID="ddlModProvincia" runat="server" AutoPostBack="True" DataSourceID="dbModProvincia" DataTextField="nombreProvincia" DataValueField="idProvincia" OnSelectedIndexChanged="ddlModProvincia_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:SqlDataSource ID="dbModProvincia" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT idProvincia, nombreProvincia FROM Provincias"></asp:SqlDataSource>
                                                        </td>
                                                        <td class="auto-style31">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style32">Localidad</td>
                                                        <td class="auto-style30">
                                                            <asp:DropDownList ID="ddlModLocalidades" runat="server" DataSourceID="dbModLocalidades" DataTextField="nombreLocalidad" DataValueField="idLocalidad"></asp:DropDownList>
                                                            <asp:SqlDataSource ID="dbModLocalidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT idLocalidad, nombreLocalidad FROM Localidades WHERE idProvincia = @idProvincia">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter Name="idProvincia" ControlID="ddlModProvincia" PropertyName="SelectedValue" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                        </td>
                                                        <td class="auto-style31">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style32">Dirección</td>
                                                        <td class="auto-style30">
                                                            <asp:TextBox ID="txbModDireccion" runat="server" ValidationGroup="ModPaciente"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvModDireccion" runat="server" ControlToValidate="txbModDireccion" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModPaciente" />
                                                            <asp:RegularExpressionValidator ID="revModDireccion" runat="server" ControlToValidate="txbModDireccion" Display="Dynamic" ErrorMessage="* Formato inválido" ForeColor="Red" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9 ]+$" ValidationGroup="ModPaciente" />
                                                        </td>
                                                        <td class="auto-style31">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style32">Obra Social</td>
                                                        <td class="auto-style30">
                                                            <asp:DropDownList ID="ddlModObraSocial" runat="server" AutoPostBack="True" DataSourceID="dbModObraSocial" DataTextField="nombre" DataValueField="idObraSocial">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="dbModObraSocial" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT [nombre], [idObraSocial] FROM [ObraSocial]"></asp:SqlDataSource>
                                                        </td>
                                                        <td class="auto-style31">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style32">Teléfono</td>
                                                        <td class="auto-style30">
                                                            <asp:TextBox ID="txbModTelefono" runat="server" ValidationGroup="ModPaciente"></asp:TextBox>
                                                            <asp:Label ID="lblCondicioness" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" Text="Formato: 11-1234-5678 // 11 1234 5678"></asp:Label>
                                                            <asp:RequiredFieldValidator ID="rfvModTelefono" runat="server" ControlToValidate="txbModTelefono" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModPaciente" />
                                                            <asp:RegularExpressionValidator ID="revModTelefono" runat="server" ControlToValidate="txbModTelefono" Display="Dynamic" ErrorMessage="Invalido" ForeColor="Red" ValidationExpression="^^\d{2}[-\s]\d{4}[-\s]\d{4}$" ValidationGroup="ModPaciente" />
                                                        </td>
                                                        <td class="auto-style31"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style37">Correo</td>
                                                        <td class="no-select">
                                                            <asp:TextBox ID="txbModCorreo" runat="server" ValidationGroup="ModPaciente"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvModCorreo" runat="server" ControlToValidate="txbModCorreo" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModPaciente" />
                                                            <asp:RegularExpressionValidator ID="revcorreo0" runat="server" ControlToValidate="txbModCorreo" Display="Dynamic" ErrorMessage="* Formato inválido" ForeColor="Red" ValidationExpression="^[\w\-\.]+@([\w-]+\.)+[\w-]{2,4}$" ValidationGroup="ModPaciente" />
                                                        </td>
                                                        <td class="auto-style36"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style32">
                                                            <asp:Button ID="btnModificarPaciente" runat="server" OnClick="btnModificarPaciente_Click" Text="Modificar paciente" CssClass="btn-td" Width="188px" ValidationGroup="ModPaciente" />
                                                        </td>
                                                        <td class="auto-style30">
                                                            <asp:Label ID="lblModUser" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <td class="auto-style31">&nbsp;</td>
                                                    </tr>
                                                </caption>
                                            </table>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwLectura" runat="server">
                                        <h3>Listar Pacientes</h3>
                                        <div class="div-ddl">
                                            <h3 class="auto-style71" style="margin: 0;">Seleccione una búsqueda o filtro:</h3>
                                            <asp:DropDownList ID="ddlBusqueda_Pacientes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBusqueda_Pacientes_SelectedIndexChanged">
                                                <asp:ListItem Text="--Seleccione una búsqueda o filtro--" Value="-1" Selected="True" />
                                                <asp:ListItem>Búsqueda por Apellido</asp:ListItem>
                                                <asp:ListItem>Búsqueda por DNI</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <asp:MultiView ID="mwBusqueda" runat="server">
                                            <asp:View ID="vwPorApellido" runat="server">
                                                <div>
                                                    <table class="auto-style32">
                                                        <tr>
                                                            <td class="auto-style46">Ingrese apellido:</td>
                                                            <td>
                                                                <asp:TextBox ID="txbPorApellido" runat="server" AutoPostBack="True" OnTextChanged="txbPorApellido_TextChanged"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwPorDNI" runat="server">
                                                <div>
                                                    <table class="auto-style32">
                                                        <tr>
                                                            <td class="auto-style45">Ingrese DNI:</td>
                                                            <td>
                                                                <asp:TextBox ID="txbPorDNI" runat="server" OnTextChanged="txbPorDNI_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                        <div>
                                            <asp:Label ID="lblEliminado" runat="server" Font-Size="Large"></asp:Label>
                                            <asp:GridView ID="gvLecturaPaciente" runat="server" AllowPaging="true" PageSize="10"
                                                AutoGenerateColumns="False" CssClass="modern-grid" OnPageIndexChanging="GridView2_PageIndexChanging">
                                                <AlternatingRowStyle CssClass="alt-row" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Seleccionar">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="True" OnCheckedChanged="chkSeleccionar_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                                    <asp:BoundField DataField="Sexos.descripcion" HeaderText="Género" />
                                                    <asp:BoundField DataField="ObraSocial.Onombre" HeaderText="Obra Social" />
                                                    <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" />
                                                    <asp:BoundField DataField="Provincias.nombreProvincia" HeaderText="Provincia" />
                                                    <asp:BoundField DataField="Localidades.nombreLocalidad" HeaderText="Localidad" />
                                                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                                                    <asp:BoundField HeaderText="Correo" DataField="correo" />
                                                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                                </Columns>
                                                <PagerStyle CssClass="grid-pager" />
                                            </asp:GridView>
                                        </div>
                                    </asp:View>
                                </asp:MultiView></td>
                            <td class="auto-style41"></td>
                        </tr>
                        <tr>
                            <td class="auto-style33">
                                <h3></h3>
                            </td>
                            <td class="auto-style38"></td>
                            <td class="auto-style39"></td>
                        </tr>
                        <tr>
                            <td class="auto-style37"></td>
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
