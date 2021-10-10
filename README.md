# Sistema para control y seguimiento de consultas medicas

![ISIL-2021-2](https://img.shields.io/badge/-ISIL--2021--2-blue)
![.NET](https://img.shields.io/badge/-.NET-blueviolet)
![WCF](https://img.shields.io/badge/-WCF-white)

## Info
- Visual Studio Community 2019
- MSSQL 18
- .NET 4.5
- Entity Framework 6.2.0

## Curso
- Desarrollo de Aplicaciones II (1366)

## Integrantes:
- Felix Chacaltana Trigoso
- Ernesto Gaspard Farfan

## Diagrama base de datos:
![Diagrama de base de datos](/docs/db_diagram.jpg)

## To do:
- [x] Refactorizacion modelo de datos
- [ ] Actualizar informe avance 2 (DER Logico)
- [ ] Mini explicacion de cada tabla y como se relacionan
- [x] Crear base de datos MSSQL
- [ ] 20 registros por tabla (minimo)
- [ ] Libreria WCF + Entity Framework
    - [ ] CRUD para cada tabla
    - [ ] Servicios paciente
    - [ ] Servicios usuario
    - [ ] Publicacion IIS

## Modulos del sistema:
1. Modulo Perfil (Admin)
    - Crear nuevo perfil
    - Actualizar perfil
    - Eliminar perfil
    - Listar perfiles
2. Modulo Area (Admin)
    - Crear nueva area
    - Actualizar area
    - Eliminar area
    - Listar areas
3. Modulo Usuario (Admin)
    - Crear nuevo usuario
    - Actualizar usuario
    - Eliminar usuario
    - Listar usuarios
4. Modulo Paciente (Doctor)
    - Crear nuevo paciente
    - Actualizar paciente
    - Eliminar paciente
    - Listar pacientes
5. Modulo Cita / Cola
    - Crear nueva cita (Doctor)
    - Asignar cita (Doctor)
    - Solicitar cita (Paciente)
    - Aprobar cita (Doctor)
    - Anular cita (Doctor)
    - Cancelar cita (Paciente)
    - Ver citas (Doctor)
    - Realizar seguimiento de citas (Doctor)
7. Modulo Horario (Doctor)
    - Crear nuevo horario
    - Actualizar horario
    - Eliminar horario
    - Listar horarios

## Flujos del sistema:
Paciente:
- Ingreso al sistema
- Asignacion de citas
- Creacion de perfil
- Actualizacion de perfil
- Mis citas
- Visualizacion de historia clinica

Doctores:
- Ingreso al sistema
- Datos de usuario
- Actualizacion de datos de usuario
- Administracion de pacientes
- Crear nuevo paciente
- Actualizacion de datos de paciente
- Visualizacion de historia clinica de paciente
- Administracion de citas
- Asignacion de fechas a horario
- Visualizacion de citas
- Asignacion de citas a pacientes
- Seguimiento de citas
- Creacion de historia clinica a paciente
- Administracion de pacientes en cola

Administradores
- Ingreso al sistema
- Administracion de usuarios
- Crear nuevo usuario
- Actualizar datos de usuario
- Agendar citas a usuario
- Administracion de perfiles
- Crear nuevo perfil
- Actualizar perfil
- Administracion de pacientes
- Crear nuevo paciente
- Actualizacion de datos de paciente
- Administracion de areas
- Crear nueva area
- Actualizar datos de area