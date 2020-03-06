# Cablemodem
- Fork desde [NetCore3-EFCore](https://github.com/lucianopereira86/NetCore3-EFCore).

# Requisitos
- SDK Net Core [3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1).
- Motor de base de datos MariaDB (10.4.x). Puede encontrar la configuración de la instancia en el proyecto sobre el archivo src/web/appsettings.json.
- Se incluye de forma opcional una imagen de docker lista para usar de MariaDB dentro de la carpeta src/Docker. Para su uso se necesita [docker](https://www.docker.com) y [docker-compose](https://www.docker.com).

# Iniciar proyecto
- Una vez instalado el sdk y el motor de base de datos se necesita:
  - Crear la base de datos cablemodem, o actualizar el appsettings.json con la deseada.
  - Ir a la carpeta src y ejecutrar los siguientes comandos:
     - `dotnet restore`
	 - `dotnet build`
	 - `dotnet test`
	 - `dotnet run --project web/web.csproj`
  - Con esto el sitio quedará levantado en *http://localhost:5000/cablemodem-modelo* y la api en *http://localhost:5000/api/cablemodems/no-verificados/{fabricante}*

## Faltantes
- Checkeo de integridad referencial entre las bases de datos.
- Por cada build se reemplaza el archivo models.json. Esto podría configurarse en un despliegue.
- Se decidió trabajar con el json como almacenamiento de datos. Una alternativa sería precargar los datos del json en memoria o una base de datos y actualizar el archivo en background. Esto representa una mejora en cuanto a performance pero el procesamiento para el usuario dejaría de ser sincrónico.
- Las acciones de los controles no utilizan modelos tipados.