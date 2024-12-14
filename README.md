# Sistema de Gestión de Notas - Colegio Philips Saunders
Sistema desarrollado para optimizar la gestión de notas académicas en un colegio de nivel primario. Implementa funcionalidades de CRUD para la gestión de datos y generación de informes de notas tanto numéricas como alfabéticos utilizando Entity Framework e iTextSharp.

# Instrucciones de configuración
# 1. Creación de la base de datos
Para ejecutar el proyecto, es necesario:

 * Crear la base de datos de llamada ColegioBDv2.
 * Poblarla con los datos de prueba necesarios.

Asegúrese de que la base de datos esté correctamente configurada y accesible desde su servidor local.
# 2. Configuración en Web.config
Es fundamental ajustar la configuración de la conexión a la base de datos en el archivo Web.config. Debes reemplazar NOMBRE-DEL-SERVIDOR por el nombre de tu servidor local en los siguientes parámetros:

# 3. Dependenciuas
El sistema utiliza las siguientes herramientas y paquetes NuGet:

 * Entity Framework : Para el manejo de la base de datos y ORM.
 * iTextSharp : Para la generación de informes de notas en formato PDF.

# 4. Revisión del Entorno de Ejecución
Confirma que tu servidor local está activo y puedes acceder a la base de datos.
Valida que las cadenas de conexión estén alineadas con tu entorno de desarrollo.
Asegúrese de contar con .NET Framework instalado correctamente.
