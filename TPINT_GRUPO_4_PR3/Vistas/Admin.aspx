<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Vistas.Admin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Panel de Administración</title>
    <style>
        body {
            margin: 0;
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            height: 100vh;
            display: flex;
            flex-direction: column;
        }

        .header {
            background-color: #ffffff;
            padding: 20px;
            text-align: center;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }

        .header h2 {
            margin: 0;
        }

        .utn {
            color: #666666;
            font-weight: bold;
        }

        .frgp {
            color: #00aaff;
            font-weight: bold;
        }

        .main-container {
            flex: 1;
            display: flex;
        }

        .sidebar {
            width: 250px;
            background-color: #ffffff;
            padding: 30px 20px;
            box-shadow: 2px 0 5px rgba(0,0,0,0.1);
            display: flex;
            flex-direction: column;
        }

        .admin-btn {
            padding: 12px;
            margin-bottom: 15px;
            font-size: 16px;
            background-color: #007bff;
            border: none;
            border-radius: 8px;
            color: white;
            text-align: left;
            cursor: pointer;
        }

        .admin-btn:hover {
            background-color: #0056b3;
        }

        .user-label {
            margin-bottom: 25px;
            font-weight: bold;
            color: #333;
        }

        .content {
            flex: 1;
            padding: 40px;
            background-color: #f9f9f9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <h2>Clínica <span class="utn">UTN</span> <span class="frgp">FRGP</span></h2>
        </div>

        <div class="main-container">
         
            <div class="sidebar">
                <asp:Label ID="lblUser" runat="server" CssClass="user-label" />

                <asp:Button ID="btnPacientes" runat="server" Text="Administrar Pacientes" CssClass="admin-btn" />
                <asp:Button ID="btnMedicos" runat="server" Text="Administrar Médicos" CssClass="admin-btn" />
                <asp:Button ID="btnTurnos" runat="server" Text="Administrar Turnos" CssClass="admin-btn" />
            </div>
            <asp:ListView ID="lvMedicos" runat="server" 

    DataKeyNames="IdMedico" 
    OnItemEditing="lvMedicos_ItemEditing" 
    OnItemUpdating="lvMedicos_ItemUpdating" 
    OnItemDeleting="lvMedicos_ItemDeleting"
    OnItemCanceling="lvMedicos_ItemCanceling"
    OnItemCommand="lvMedicos_ItemCommand"
    OnItemInserting="lvMedicos_ItemInserting">
    
    <ItemTemplate>
        <tr>
            <td><%# Eval("Nombre") %></td>
            <td><%# Eval("Especialidad") %></td>
            <td>
                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" Text="Editar" />
                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Delete" Text="Eliminar" />
            </td>
        </tr>
    </ItemTemplate>

    <EditItemTemplate>
        <tr>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre") %>' />
            </td>
            <td>
                <asp:TextBox ID="txtEspecialidad" runat="server" Text='<%# Bind("Especialidad") %>' />
            </td>
            <td>
                <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Update" Text="Actualizar" />
                <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar" />
            </td>
        </tr>
    </EditItemTemplate>

    <InsertItemTemplate>
        <tr>
            <td><asp:TextBox ID="txtNombreNuevo" runat="server" /></td>
            <td><asp:TextBox ID="txtEspecialidadNueva" runat="server" /></td>
            <td><asp:LinkButton ID="btnAgregar" runat="server" CommandName="Insert" Text="Agregar" /></td>
        </tr>
    </InsertItemTemplate>

    <LayoutTemplate>
        <table>
            <tr>
                <th>Nombre</th>
                <th>Especialidad</th>
                <th>Acciones</th>
            </tr>
            <tr runat="server" id="itemPlaceholder"></tr>
        </table>

        <asp:DataPager ID="DataPager1" runat="server" PageSize="5">
            <Fields>
                <asp:NextPreviousPagerField ShowFirstPageButton="true" ShowLastPageButton="true" />
            </Fields>
        </asp:DataPager>
    </LayoutTemplate>
</asp:ListView>
           
            <div class="content">
               
                <asp:DataList ID="DataList1" runat="server"></asp:DataList>
            </div>
        </div>
    </form>
</body>
</html>