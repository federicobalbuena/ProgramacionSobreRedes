# ProgramacionSobreRedes
TP Programacion Sobre Redes

===========================
El proyecto consiste en un sistema de envío de archivos, implementando la arquitectura cliente/servidor, por medio de un Socket stream TCP.

Se implementaron:

    Validacion de usuario del lado del servidor, almacenando los datos de usuario en una base de datos SQL.

    Sistema de cifrado de la contraseña con Salt y Master_Key para no almacenar las contraseñas en texto plano.

    Hilos, para el manejo de las conexiones entrantes de los clientes, cada cliente tiene su propio hilo de ejecución.

    Semaforo que solo permite enviar archivos a 2 usuarios al mismo tiempo, dejando los otros usuarios en espera.

    Patrón Singleton para gestión de una unica instancia de la conección a la base de datos.

Requerimientos:

Visual Studio con .Net Framework 4.7.2

SQL Server con localdb.
===========================

Nombre de usuario: usuario1
Contraseña: pass1

============================================================================================================
1- Ejecutar los scripts que se encuentran en _SCRIPT_DB:

Estos scrips crean la db local con la tabla y datos para realizar pruebas de validación

2- Abrir el proyecto ProyectoProgramacionSobreRedes

3- Dentro del Servidor, editar el archivo ConnSingleton

Para conectar a la DB se debe reemplazar el contenido del campo "Data Source" por el  nombre del 
servidor SQL local en la línea 11 del archivo ConnSingleton.cs en el Servidor.
 
============================================================================================================
Origen de archivos: Cliente\bin\Debug

Destino de archivos: Servidor\bin\Debug\ServerFiles
