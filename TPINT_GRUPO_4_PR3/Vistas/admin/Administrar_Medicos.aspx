<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrar_Medicos.aspx.cs" Inherits="Vistas.admin.Administrar_Medicos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Administrador</title>
    <style>
        html, body, form {
            margin: 0;
            font-family: Arial, sans-serif;
            height:100%;
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
        }/*
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
            height:100%;
        }
        .no-select {
            user-select: none;
        }
        .columnaIndex {
            width: 157px;
            background-color:#ffffff;
            height:100%;
        }
        .auto-style3 {
            width: 100%;
            height:100%;
        }
        .auto-style4 {
            width: 151px;
            height: 310px;
        }
        .columnaIndex-PanelAdmin {
            width: 151px;
            text-align: center;
            font-size: 16px;
            font-weight:bold;
            height:80px;
        }
        .columnaBody {
            background-color:#e6e6e6;
            padding: 0px 30px 30px 30px;
            font-weight: normal;
            height:100%;
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
        
        .auto-style30 {
            user-select: none;
            height: 24px;
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
        
        .auto-style36 {
            user-select: none;
            height: 25px;
            width: 34px;
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
                            <asp:Button runat="server" Text="Administrar Médicos" CssClass="btn-index" ID="btnAdministrarMedico" OnClick="btnAdministrarMedico_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td-btn-index">
                            <asp:Button ID="btnAdministrarPacientes" runat="server" Text=" Administrar Pacientes" CssClass="btn-index" OnClick="btnAdministrarPacientes_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td-btn-index">
                            <asp:Button ID="btnAdministrarTurnos" runat="server" Text="Administrar Turnos" CssClass="btn-index" OnClick="btnAdministrarTurnos_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4"></td>
                    </tr>
                    <tr>
                        <td> &nbsp;</td>
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
                                    <asp:Label ID="lblUser" runat="server" Font-Bold="True" Font-Names="Calibri" Text="Administrador"></asp:Label>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style34" colspan="2"><h2>Administrar Médico</h2></td>
                        <td class="auto-style35"></td>
                    </tr>
                    <tr>
                        <td class="botonera" colspan="2">
                            <asp:Button ID="btnAlta" runat="server" Text="Registrar" CssClass="btn-td" OnClick="btnAlta_Click" />
                            <asp:Button ID="btnBaja" runat="server" Text="Dar de baja" CssClass="btn-td" OnClick="btnBaja_Click" />
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
                                                <td class="auto-style32">Legajo</td>
                                                <td class="auto-style30">
                                                    <asp:TextBox ID="txbLegajo" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="auto-style31">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">DNI</td>
                                                <td class="auto-style30">
                                                    <asp:TextBox ID="txbDni" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="auto-style31"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Nombre</td>
                                                <td class="auto-style30">
                                                    <asp:TextBox ID="txbNombre" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="auto-style31"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Apellido</td>
                                                <td class="auto-style30">
                                                    <asp:TextBox ID="txbApellido" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="auto-style31"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Especialidad</td>
                                                <td class="auto-style30">
                                                    <asp:DropDownList ID="ddlEspecialidad" runat="server" DataSourceID="dbEspecialidades" DataTextField="descripcion" DataValueField="idEspecialidad">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="dbEspecialidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionString3 %>" SelectCommand="SELECT * FROM [Especialidades]"></asp:SqlDataSource>
                                                </td>
                                                <td class="auto-style31">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Sexo</td>
                                                <td class="auto-style30">
                                                    <asp:DropDownList ID="ddlGenero" runat="server" DataSourceID="dbGenero" DataTextField="descripcion" DataValueField="idSexo">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:SqlDataSource ID="dbGenero" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionString3 %>" ProviderName="<%$ ConnectionStrings:ClinicaDBConnectionString3.ProviderName %>" SelectCommand="SELECT * FROM [Sexos]"></asp:SqlDataSource>
                                                </td>
                                                <td class="auto-style31"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Nacionalidad</td>
                                                <td class="auto-style30">
                                                    <asp:DropDownList ID="ddlNacionalidad" runat="server" DataSourceID="dbNacionalidades" DataTextField="nombrePais" DataValueField="gentilicio">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:SqlDataSource ID="dbNacionalidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionString3 %>" SelectCommand="SELECT * FROM [Paises]"></asp:SqlDataSource>
                                                </td>
                                                <td class="auto-style31"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Fecha de nacimiento</td>
                                                <td class="auto-style30">
                                                    <asp:TextBox ID="txbFechaNacimiento" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="auto-style31"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Dirección</td>
                                                <td class="auto-style30">
                                                    <asp:TextBox ID="txbDireccion" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="auto-style31"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Provincia</td>
                                                <td class="auto-style30">
                                                    <asp:DropDownList ID="ddlProvincia" runat="server" DataSourceID="dbProvincias" DataTextField="nombreProvincia" DataValueField="idProvincia">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:SqlDataSource ID="dbProvincias" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionString3 %>" SelectCommand="SELECT [idProvincia], [nombreProvincia] FROM [Provincias]"></asp:SqlDataSource>
                                                </td>
                                                <td class="auto-style31">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Localidad</td>
                                                <td class="auto-style30">
                                                    <asp:DropDownList ID="ddlLocalidades" runat="server" DataSourceID="dbLocalidades" DataTextField="nombreLocalidad" DataValueField="idLocalidad">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:SqlDataSource ID="dbLocalidades" runat="server" ConnectionString="<%$ ConnectionStrings:ClinicaDBConnectionString3 %>" SelectCommand="SELECT * FROM [Localidades]"></asp:SqlDataSource>
                                                </td>
                                                <td class="auto-style31">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Correo Electrónico</td>
                                                <td class="auto-style30">
                                                    <asp:TextBox ID="txbCorreo" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="auto-style31">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">Teléfono</td>
                                                <td class="auto-style30">
                                                    <asp:TextBox ID="txbTelefono" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="auto-style31"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style37"></td>
                                                <td class="no-select">
                                                </td>
                                                <td class="auto-style36"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style32">
                                                    <asp:Button ID="btnRegistrarMedico" runat="server" Text="Registrar paciente" Width="188px" OnClick="btnRegistrarMedico_Click" />
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
                                    <h3>Dar de baja Médico</h3>
                                    <div>
                                        Ingrese un valor:
                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                        <asp:GridView ID="GridView1" runat="server">
                                        </asp:GridView>
                                    </div>
                                </asp:View>

                                <asp:View ID="vwModificacion" runat="server">
                                    <h3>Modificar Médico</h3>
                                    <div>

                                        Ingrese un valor:<asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                                        <asp:GridView ID="GridView3" runat="server">
                                        </asp:GridView>
                                    </div>
                                </asp:View>

                                <asp:View ID="vwLectura" runat="server">
                                    <h3>Listar Médico</h3>
                                    <div>

                                        Ingrese un valor:<asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                        <asp:GridView ID="GridView2" runat="server">
                                        </asp:GridView>

                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </td>
                        <td class="auto-style41"></td>
                    </tr>
                    <tr>
                        <td class="auto-style33"><h3>&nbsp;</h3></td>
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
