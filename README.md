
# Cablemodem
- Fork desde [NetCore3-EFCore](https://github.com/lucianopereira86/NetCore3-EFCore).

# Requisitos
- SDK Net Core [3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1).
- Motor de base de datos MySQL. Puede encontrar la configuración en el archivo appsettings.json.

## Faltantes
- Checkeo de integridad referencial entre las bases de datos.
- Por cada build se reemplaza el archivo models.json. Esto podría configurarse en un despliegue.
- Se decidió trabajar con el json como almacenamiento de datos. Una alternativa sería precargar los datos del json en memoria o una base de datos y actualizar el archivo en background. Esto representa una mejora en cuanto a performance pero el procesamiento para el usuario dejaría de ser sincrónico.