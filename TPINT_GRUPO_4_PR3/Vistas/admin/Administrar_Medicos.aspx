<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar_Medicos.aspx.cs" Inherits="Vistas.admin.Administrar_Medicos" %>

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
        .botonera {
            width: 100%;
            padding: 0;
        }
        .botonera .btn-td {
            display: inline;
            width: 20%;
            margin-bottom: 10px; /* espacio entre botones */
            box-sizing: border-box;
            padding: 10px;
            font-size: 16px;
            border-radius: 6px;
            border: none;
            background-color: #007bff;
            color: white;
            cursor: pointer;
        }
        .botonera .btn-td:hover {
            background-color: #0056b3;
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
            height: 34px;
        }
        .auto-style34 {
            user-select: none;
            height: 51px;
        }
        .auto-style35 {
            user-select: none;
            width: 34px;
            height: 51px;
        }
        .auto-style37 {
            user-select: none;
            width: 196px;
        }
        .auto-style38 {
            user-select: none;
            height: 34px;
        }
        .auto-style39 {
            user-select: none;
            width: 34px;
            height: 34px;
        }
        .auto-style41 {
            user-select: none;
            width: 34px;
            height: 18px;
        }
        .auto-style43 {
            width: 100%;
            padding: 0;
            height: 18px;
        }
        .auto-style44 {
            width: 100%;
        }
        .auto-style46 {
            width: 1397px;
        }
        .auto-style49 {
            width: 190px;
            height: 22px;
        }
        .auto-style50 {
            height: 22px;
        }
        .auto-style51 {
            width: 190px;
        }
        .auto-style52 {
            user-select: none;
            height: 24px;
            width: 1136px;
        }
        .auto-style56 {
            user-select: none;
            height: 29px;
            width: 196px;
        }
        .auto-style57 {
            user-select: none;
            height: 29px;
            width: 1136px;
        }
        .auto-style58 {
            user-select: none;
            width: 34px;
            height: 29px;
        }
        .auto-style62 {
            user-select: none;
            height: 25px;
            width: 196px;
        }
        .auto-style63 {
            user-select: none;
            height: 25px;
            width: 1136px;
        }
        .auto-style64 {
            user-select: none;
            height: 25px;
            width: 34px;
        }
        .auto-style68 {
            user-select: none;
            height: 30px;
            width: 196px;
        }
        .auto-style69 {
            user-select: none;
            height: 30px;
            width: 1136px;
        }
        .auto-style70 {
            user-select: none;
            width: 34px;
            height: 30px;
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
                            <td class="columnaIndex-PanelAdmin">Panel Admin</td>
                        </tr>
                        <tr>
                            <td class="td-btn-index">
                                <asp:Button ID="btnAdministrarMedico" runat="server" Text="Administrar Médicos" CssClass="btn-index" CommandArgument="Medicos" OnCommand="navigateButton_Click" /></td>
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
                                <h2>Administrar Médico</h2>
                            </td>
                            <td class="auto-style35"></td>
                        </tr>
                        <tr>
                            <td class="botonera" colspan="2">
                                <asp:Button ID="btnAlta" runat="server" Text="Registrar" CssClass="btn-td" OnClick="btnAlta_Click" />
                                <asp:Button ID="btnBaja" runat="server" Text="Dar de baja" CssClass="btn-td" OnClick="btnBaja_Click" OnClientClick="return confirm('¿Está seguro que desea dar de baja el medico?');" />
                                <asp:Button ID="btnMod" runat="server" Text="Modificar" CssClass="btn-td" OnClick="btnMod_Click" />
                                <asp:Button ID="btnLectura" runat="server" Text="Listar" CssClass="btn-td" OnClick="btnLectura_Click" />
                                </td>
                            <td class="auto-style41"></td>
                        </tr>
                        <tr>
                            <td class="auto-style43" colspan="2">
                                <asp:MultiView ID="mvFormularios" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="vwAlta" runat="server">
                                        <h3>Registrar Médico</h3>
                                        <div>
                                            <table class="auto-style44">
                                                <tr>
                                                    <td class="auto-style32">Legajo:</td>
                                                    <td class="auto-style52">
                                                        <asp:TextBox ID="txbLegajo"  runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvLegajo" runat="server" ControlToValidate="txbLegajo" ErrorMessage="* Campo obligatorio" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                        <asp:RegularExpressionValidator ID="revLegajo" runat="server" ControlToValidate="txbLegajo" Display="Dynamic" ErrorMessage="* Hasta 2 digitos" ForeColor="Red" ValidationExpression="^([1-9]|[1-9]\d)$" ValidationGroup="AltaMedico" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">DNI:</td>
                                                    <td class="auto-style52">
                                                        <asp:TextBox ID="txbDni" runat="server"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvDni" runat="server" ControlToValidate="txbDni" ErrorMessage="* Campo obligatorio" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                        <asp:RegularExpressionValidator ID="revDni" runat="server" ControlToValidate="txbDni" ValidationExpression="^\d{8}$" ErrorMessage="* 8 digitos" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Nombre:</td>
                                                    <td class="auto-style52">
                                                        <asp:TextBox ID="txbNombre" runat="server"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvnombre" runat="server" ControlToValidate="txbNombre" ErrorMessage="* Campo obligatorio" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                        <asp:RegularExpressionValidator ID="revnombre" runat="server" ControlToValidate="txbNombre" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$"  ErrorMessage="* Solo letras" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Apellido:</td>
                                                    <td class="auto-style52">
                                                        <asp:TextBox ID="txbApellido" runat="server"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvapellido" runat="server" ControlToValidate="txbApellido" ErrorMessage="* Campo obligatorio" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                        <asp:RegularExpressionValidator ID="revapellido" runat="server" ControlToValidate="txbApellido" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$"  ErrorMessage="* Solo letras" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                     </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Especialidad:</td>
                                                    <td class="auto-style52">
                                                        <asp:DropDownList ID="ddlEspecialidad" runat="server" DataSourceID="dbEspecialidades" DataTextField="descripcion" DataValueField="idEspecialidad"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="dbEspecialidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT * FROM [Especialidades]"></asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Sexo:</td>
                                                    <td class="auto-style52">
                                                        <asp:DropDownList ID="ddlGenero" runat="server" DataSourceID="dbGenero" DataTextField="descripcion" DataValueField="idSexo"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvgenero" runat="server" ControlToValidate="ddlGenero" InitialValue="" ErrorMessage="* Seleccione un genero" Display="Dynamic" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                        <br />
                                                        <asp:SqlDataSource ID="dbGenero" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT * FROM [Sexos]"></asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Fecha de nacimiento:</td>
                                                    <td class="auto-style52">
                                                        <asp:TextBox ID="txbfechanacimiento" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCondiciones" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" Text="Formato: YYYY-MM-DD"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvfechanac" runat="server" ControlToValidate="txbfechanacimiento" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                        <asp:RegularExpressionValidator ID="revfechanac" runat="server" ControlToValidate="txbfechanacimiento" Display="Dynamic" ErrorMessage="* Formato inválido (YYYY-MM-DD)" ForeColor="Red" ValidationExpression="^\d{4}-\d{2}-\d{2}$" ValidationGroup="AltaMedico" />
                                                        <br />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Nacionalidad:</td>
                                                    <td class="auto-style52"> <br />
                                                        <asp:DropDownList ID="ddlNacionalidad" runat="server" DataSourceID="dbNacionalidades" DataTextField="nombrePais" DataValueField="gentilicio"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvpais" runat="server" ControlToValidate="ddlNacionalidad" InitialValue="" ErrorMessage="* Seleccione un país" Display="Dynamic" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                        <asp:SqlDataSource ID="dbNacionalidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT * FROM [Paises]"></asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Provincia:</td>
                                                    <td class="auto-style52">
                                                        <asp:DropDownList ID="ddlProvincia" runat="server" AutoPostBack="True" DataSourceID="dbProvincias" DataTextField="nombreProvincia" DataValueField="idProvincia" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged"> </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvprovincia" runat="server" ControlToValidate="ddlProvincia" InitialValue="" ErrorMessage="* Seleccione una provincia" Display="Dynamic" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                        <asp:SqlDataSource ID="dbProvincias" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT idProvincia, nombreProvincia FROM Provincias"></asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>

                                                <tr>
                                                    <td class="auto-style32">Localidad:</td>
                                                    <td class="auto-style52">
                                                        <asp:DropDownList ID="ddlLocalidades" runat="server" DataSourceID="dbLocalidades" DataTextField="nombreLocalidad" DataValueField="idLocalidad"> </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvlocalidades" runat="server" ControlToValidate="ddlLocalidades" InitialValue="" ErrorMessage="* Seleccione una localidad" Display="Dynamic" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                        <asp:SqlDataSource ID="dbLocalidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT idLocalidad, nombreLocalidad FROM Localidades WHERE idProvincia = @idProvincia">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="ddlProvincia" Name="idProvincia" PropertyName="SelectedValue" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                    <td class="auto-style31">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td class="auto-style68">Dirección:</td>
                                                    <td class="auto-style69">
                                                        <asp:TextBox ID="txbDireccion" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvdireccion" runat="server" ControlToValidate="txbDireccion" ErrorMessage="* Campo obligatorio" Display="Dynamic" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                        <asp:RegularExpressionValidator ID="revdireccion" runat="server" ControlToValidate="txbDireccion" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9 ]+$" ErrorMessage="* Formato inválido" Display="Dynamic" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                    </td>
                                                    <td class="auto-style70"></td>  
                                                    <td class="auto-style70"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style56">Correo:</td>
                                                    <td class="auto-style57">
                                                        <asp:TextBox ID="txbCorreo" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvcorreo" runat="server" ControlToValidate="txbCorreo" ErrorMessage="* Campo obligatorio" Display="Dynamic" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                    <asp:RegularExpressionValidator ID="revcorreo" runat="server" ControlToValidate="txbCorreo" ValidationExpression="^[\w\-\.]+@([\w-]+\.)+[\w-]{2,4}$" ErrorMessage="* Formato inválido" Display="Dynamic" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                        </td>
                                                    <td class="auto-style58"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">Teléfono</td>
                                                    <td class="auto-style52">
                                                        <asp:TextBox ID="txbTelefono" runat="server"></asp:TextBox>
                                                        <asp:Label ID="lblCondicioness" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" Text="Formato: 11-1234-5678 // 11 1234 5678"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvtelefono" runat="server" ControlToValidate="txbTelefono" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                        &nbsp;<asp:RegularExpressionValidator ID="revtelefono" runat="server" ControlToValidate="txbTelefono" Display="Dynamic" ErrorMessage="Invalido" ForeColor="Red" ValidationExpression="^^\d{2}[-\s]\d{4}[-\s]\d{4}$" ValidationGroup="AltaMedico" />
                                                    </td>
                                                    <td class="auto-style31"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style62">Usuario</td>
                                                    <td class="auto-style63">
                                                        <asp:TextBox ID="txbUsuario" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvusuario" runat="server" ControlToValidate="txbUsuario" ErrorMessage="* Campo obligatorio" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                        <asp:RegularExpressionValidator ID="revusuario" runat="server" ControlToValidate="txbUsuario" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$"  ErrorMessage="* Solo letras" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                        </td>
                                                    <td class="auto-style64"></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style56">Contrasenia</td>
                                                    <td class="auto-style57">
                                                        <asp:TextBox ID="txbContrasenia" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvcontrasenia" runat="server" ControlToValidate="txbContrasenia" ErrorMessage="* Campo obligatorio" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                        </td>
                                                    <td class="auto-style58"></td>
                                                </tr>
                                                <tr>
                                                <td class="auto-style56">Contrasenia</td>
                                                <td class="auto-style57">
                                                    <asp:TextBox ID="txbRepContrasenia" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvrepcontrasenia" runat="server" ControlToValidate="txbRepContrasenia" ErrorMessage="* Campo obligatorio" ForeColor="Red" Display="Dynamic" ValidationGroup="AltaMedico" />
                                                    </td>
                                                <td class="auto-style58"></td>

                                                </tr>
                                                <tr>
                                                    <td class="auto-style32">
                                                        <asp:Button ID="btnRegistrarMedico" runat="server" Text="Registrar medico" Width="188px" OnClick="btnRegistrarMedico_Click" ValidationGroup="AltaMedico" CausesValidation="True"/></td>
                                                    <td class="auto-style52">
                                                        <asp:Label ID="lblAddUserState" runat="server" Visible="False"></asp:Label></td>
                                                    <td class="auto-style31">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwBaja" runat="server">
                                        <h3>Dar de baja Médico</h3>
                                        <div></div>
                                    </asp:View>
                                    <asp:View ID="vwModificacion" runat="server">
                                        <h3>Modificar Medico</h3>
                                        <div>
                                            <table class="auto-style44">
                                                <tr>
                                                    <td class="auto-style51">Legajo:</td>
                                                    <td class="auto-style46">
                                                        <asp:TextBox ID="txtbModMedicoLegajo" Enabled="false" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModLegajo" runat="server" ControlToValidate="txtbModMedicoLegajo" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModMedico" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">DNI:</td>
                                                    <td class="auto-style46">
                                                        <asp:TextBox ID="txtbModMedicoDNI" Enabled="false" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModDni" runat="server" ControlToValidate="txtbModMedicoDNI" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModMedico" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Nombre:</td>
                                                    <td class="auto-style46">
                                                        <asp:TextBox ID="txtbModMedicoNombre" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModNombre" runat="server" ControlToValidate="txtbModMedicoNombre" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModMedico" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Apellido:</td>
                                                    <td class="auto-style46">
                                                        <asp:TextBox ID="txtbModMedicoApellido" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModApellido" runat="server" ControlToValidate="txtbModMedicoApellido" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModMedico" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style49">Especialidad:</td>
                                                    <td class="auto-style50">
                                                        <asp:DropDownList ID="ddlModEspecialidad" runat="server" DataSourceID="dbEspecialidades" DataTextField="descripcion" DataValueField="idEspecialidad"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="dbespecialidad" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT * FROM [Especialidades]"></asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Fecha de nacimiento:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtbModFechaNac" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCondiciones0" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" Text="Formato: YYYY-MM-DD"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvModFechaNac" runat="server" ControlToValidate="txtbModFechaNac" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModMedico" />
                                                        <asp:RegularExpressionValidator ID="revModFechaNac" runat="server" ControlToValidate="txtbModFechaNac" Display="Dynamic" ErrorMessage="* Formato inválido (YYYY-MM-DD)" ForeColor="Red" ValidationExpression="^\d{4}-\d{2}-\d{2}$" ValidationGroup="ModMedico" />
                                                    </td>
                                                        
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Sexo:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlModGenero" runat="server" DataSourceID="dbGenero" DataTextField="descripcion" DataValueField="idSexo"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="dbModGenero" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT * FROM [Correos]"></asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Nacionalidad:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlModNacionalidad" runat="server" DataSourceID="dbNacionalidades" DataTextField="nombrePais" DataValueField="gentilicio">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="dbModNacionalidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT * FROM [Correos]"></asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Provincia:</td>
                                                    <td class="auto-style46">
                                                        <asp:DropDownList ID="ddlModProvincia" runat="server" AutoPostBack="True" DataSourceID="dbModProvincia" DataTextField="nombreProvincia" DataValueField="idProvincia" OnSelectedIndexChanged="ddlModProvincia_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="dbModProvincia" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT idProvincia, nombreProvincia FROM Provincias"></asp:SqlDataSource>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td class="auto-style51">Localidad:</td>
                                                    <td class="auto-style46">
                                                        <asp:DropDownList ID="ddlModLocalidad" runat="server" DataSourceID="dbModLocalidad" DataTextField="nombreLocalidad" DataValueField="idLocalidad"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="dbModLocalidad" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionGlobal %>" SelectCommand="SELECT idLocalidad, nombreLocalidad FROM Localidades WHERE idProvincia = @idProvincia">
                                                            <SelectParameters>
                                                                <asp:ControlParameter Name="idProvincia" ControlID="ddlModProvincia" PropertyName="SelectedValue" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                        
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Direccion:</td>
                                                    <td class="auto-style46">
                                                        <asp:TextBox ID="txtbModMedicoDireccion" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModDireccion" runat="server" ControlToValidate="txtbModMedicoDireccion" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="AltaMedico" />
                                                        <asp:RegularExpressionValidator ID="revModDireccion" runat="server" ControlToValidate="txtbModMedicoDireccion" Display="Dynamic" ErrorMessage="* Formato inválido" ForeColor="Red" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9 ]+$" ValidationGroup="ModMedico" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Telefono:</td>
                                                    <td class="auto-style46">
                                                        <asp:TextBox ID="txtbModMedicoTelefono" runat="server"></asp:TextBox>
                                                        <asp:Label ID="lblCondicioness0" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" Text="Formato: 11-1234-5678 // 11 1234 5678"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvModTelefono" runat="server" ControlToValidate="txtbModMedicoTelefono" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModMedico" />
                                                        <asp:RegularExpressionValidator ID="revModTelefono" runat="server" ControlToValidate="txtbModMedicoTelefono" Display="Dynamic" ErrorMessage="Invalido" ForeColor="Red" ValidationExpression="^^\d{2}[-\s]\d{4}[-\s]\d{4}$" ValidationGroup="ModMedico" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Correo:</td>
                                                    <td class="auto-style46">
                                                        <asp:TextBox ID="txtbModMedicoCorreo" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModCorreo" runat="server" ControlToValidate="txtbModMedicoCorreo" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModMedico" />
                                                        <asp:RegularExpressionValidator ID="revModCorreo" runat="server" ControlToValidate="txtbModMedicoCorreo" Display="Dynamic" ErrorMessage="* Formato inválido" ForeColor="Red" ValidationExpression="^[\w\-\.]+@([\w-]+\.)+[\w-]{2,4}$" ValidationGroup="ModMedico" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Usuario:</td>
                                                    <td class="auto-style46">
                                                        <asp:TextBox ID="txtbModMedicoUsuario" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModContrasenia" runat="server" ControlToValidate="txtbModMedicoUsuario" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModMedico" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">Contrasenia:</td>
                                                    <td class="auto-style46">
                                                        <asp:TextBox ID="txtbModMedicoContrasenia" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvModRContrasenia" runat="server" ControlToValidate="txtbModMedicoContrasenia" Display="Dynamic" ErrorMessage="* Campo obligatorio" ForeColor="Red" ValidationGroup="ModMedico" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style51">
                                                        <asp:Button ID="btnModificarMedico" runat="server" OnClick="btnModificarMedico_Click" Text="Modificar Medico" Width="188px" ValidationGroup="ModMedico" /></td>
                                                    <td class="auto-style46">
                                                        <asp:Label ID="lblModificarMedico" runat="server" Text="Medico modificado con éxito" Visible="False"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            <br />
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwLectura" runat="server">
                                        <h3>Listar Médico</h3>
                                         <div class="div-ddl">
                                             <h3 class="auto-style35" style="margin: 0;">Búsqueda por:</h3>
                                             <asp:DropDownList ID="ddlBusqueda_Medicos" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBusqueda_Medicos_SelectedIndexChanged">
                                                 <asp:ListItem Text="--Seleccione una búsqueda--" Value="-1" Selected="True" />
                                                 <asp:ListItem>Apellido</asp:ListItem>
                                                 <asp:ListItem>DNI</asp:ListItem>
                                                 <asp:ListItem>Legajo</asp:ListItem>
                                             </asp:DropDownList>
                                         </div>
                                        <div>
                                            <asp:MultiView ID="mwBusqueda" runat="server">
                                                <asp:View ID="vwPorApellido" runat="server">
                                                    <div>
                                                        <table class="auto-style32">
                                                            <tr>
                                                                <td class="auto-style36">Ingrese apellido del médico:</td>
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
                                                                <td class="auto-style36">Ingrese DNI del médico:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txbPorDNI" runat="server" OnTextChanged="txbPorDNI_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:View>
                                                <asp:View ID="vwPorLegajo" runat="server">
                                                    <div>
                                                        <table class="auto-style32">
                                                            <tr>
                                                                <td class="auto-style36">Ingrese legajo del médico:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txbPorLegajo" runat="server" OnTextChanged="txbPorLegajo_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                            <asp:GridView ID="gvLecturaMedico" runat="server" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Style="margin-right: 0px" OnPageIndexChanging="gvLecturaMedico_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Seleccionar">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="true" OnCheckedChanged="chkSeleccionar_CheckedChanged" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Legajo" HeaderText="Legajo" />
                                                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                                                    <asp:BoundField DataField="Usuario.nombreUsuario" HeaderText="Usuario" />
                                                    <asp:BoundField DataField="Usuario.contrasenia" HeaderText="Contraseña" />
                                                    <asp:BoundField DataField="Sexos.descripcion" HeaderText="Genero" />
                                                    <asp:BoundField DataField="Especialidad.descripcion" HeaderText="Especialidad" />
                                                    <asp:BoundField DataField="nacionalidad" HeaderText="Nacionalidad" />
                                                    <asp:BoundField DataField="Provincias.nombreProvincia" HeaderText="Provincia" />
                                                    <asp:BoundField DataField="Localidades.nombreLocalidad" HeaderText="Localidad" />
                                                    <asp:BoundField DataField="correo" HeaderText="Correo" />
                                                    <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                            <td class="auto-style41"></td>
                        </tr>
                        <tr>
                            <td class="auto-style33">
                                <h3>
                                    <asp:Label ID="lblAddUserState0" runat="server" Visible="False"></asp:Label><asp:Label ID="lblCheck" runat="server"></asp:Label></h3>
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
