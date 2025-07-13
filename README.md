# 🏥 TPINT_GRUPO_4_PR3 - Sistema de Gestión Clínica Médica

Proyecto desarrollado como Trabajo Práctico Integrador para la materia **Programación III** de la **Tecnicatura Universitaria en Programación** - UTN FRGP (1º Cuatrimestre 2025).

## 📌 Descripción

Sistema web para la gestión de una clínica médica con múltiples especialidades, donde interactúan dos tipos de usuarios:

- **Administrador:** realiza el ABML de médicos y pacientes, asigna turnos y genera informes estadísticos.
- **Médico:** puede consultar sus turnos asignados, marcar presencia/ausencia y registrar observaciones clínicas.

## ⚙️ Tecnologías utilizadas

- ASP.NET Web Forms (C#)
- SQL Server
- ADO.NET (tres capas)
- HTML, CSS
- Bootstrap (opcional)
- JavaScript (opcional)
- SQL Management Studio

## 🧩 Funcionalidades principales

### 👤 Login
- Acceso diferenciado para administrador y médico.
- Visualización del usuario activo en cada pantalla.

### 👨‍⚕️ Administrador
- ABML de **pacientes** y **médicos** con validaciones.
- Alta de usuarios médicos con credenciales.
- Asignación de turnos según disponibilidad, especialidad y horarios.
- Generación de informes con procesamiento estadístico (no listados).

### 🩺 Médico
- Consulta de turnos propios con filtros por fecha/DNI/especialidad.
- Marcar pacientes como **Presentes/Ausentes**.
- Carga de observaciones clínicas para los presentes.

### 📊 Informes
- Estadísticas de asistencia (ej: % ausentes vs presentes entre fechas).
- Reportes con agregaciones (SUM, AVG, COUNT).

## ✅ Requisitos del TP

Este sistema cumple con los siguientes puntos obligatorios del TP:
- Acceso con login.
- ABML de entidades con validaciones (campos obligatorios, tipos, repetidos).
- Uso de **programación en tres capas**.
- **Bajas lógicas**, no físicas.
- **Paginación**, búsquedas (`LIKE`) y **filtros** en listados.
- Manejo visual y validado de contraseñas.
- Uso de **desplegables** para provincias, localidades, especialidades.
- **Mensajes de confirmación y alerta**.
- **Carga automática de datos** en modificación.
- Al menos **15 registros por tabla** para prueba completa.
- Reportes **procesados**, no meros listados.

## 🗂️ Estructura del proyecto

El sistema está organizado en **tres capas**:

```
TPINT_GRUPO_4_PR3/
│
├── Datos/
│   ├── ConsultasMedico.cs
│   ├── ConsultasPacientes.cs
│   ├── ConsultasReportes.cs
│   ├── ConsultasTurnos.cs
│   ├── ConsultasUsuario.cs
│   └── DataAccess.cs
│
├── Entidades/
│   ├── Especialidad.cs
│   ├── Localidades.cs
│   ├── Medico.cs
│   ├── ObraSocial.cs
│   ├── Paciente.cs
│   ├── Persona.cs
│   ├── Provincias.cs
│   ├── Rol.cs
│   ├── Sexos.cs
│   ├── Turno.cs
│   └── Usuario.cs
│
├── Negocio/
│   ├── GestorMedico.cs
│   ├── GestorPaciente.cs
│   ├── GestorReportes.cs
│   ├── GestorTurnos.cs
│   └── GestorUsuario.cs
│
├── Vistas/
│   ├── Login/
│   │   ├── Login.aspx
│   │   └── images/
│   │
│   ├── PanelMedico/
│   │   ├── PanelMedico.aspx
│   │   ├── PanelMedico.aspx.cs
│   │   ├── PanelMedico.aspx.designer.cs
│   │   └── style.css
│   │
│   └── admin/
│       ├── Admin.aspx
│       ├── Admin.aspx.designer.cs
│       ├── Administrar_Medicos/
│       ├── Administrar_Pacientes/
│       │   ├── Administrar_Pacientes.aspx
│       │   ├── Administrar_Pacientes.aspx.cs
│       │   ├── Administrar_Pacientes.aspx.designer.cs
│       │   └── style.css
│       ├── Administrar_Turnos/
│       │   ├── Administrar_Turnos.aspx
│       │   ├── Administrar_Turnos.aspx.cs
│       │   ├── Administrar_Turnos.aspx.designer.cs
│       │   └── style.css
│
├── Web.config
├── packages.config
└── README.md
```

## 🏁 Cómo ejecutar el proyecto

1. Clonar el repositorio o descargar el ZIP del proyecto.
2. Abrir la solución en Visual Studio.
3. Configurar la cadena de conexión a SQL Server en `web.config`.
4. Ejecutar el script de la base de datos para crear y poblar las tablas.
5. Correr el proyecto (`F5`) y loguearse con un usuario administrador o médico.

## 🔒 Usuarios de prueba

```txt
Administrador:
  Usuario: admin
  Contraseña: admin123

Médico:
  Usuario: drgomez
  Contraseña: pass123
```

> ⚠️ Nota: los usuarios y contraseñas pueden variar. Revisar los datos precargados en la base.

## 🧑‍💻 Créditos

Proyecto realizado por el grupo **GRUPO_4** para la materia **Programación III** - UTN FRGP - 2025.
