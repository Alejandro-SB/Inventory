# Inventory
Prueba técnica de inventario de productos.

## Resumen
La aplicación está compuesta por 7 proyectos:
* Domain
* Application
* Infrastructure
* API
* Web
* Test
* Integration Test

La aplicación expone una API REST con autenticación por JWT y dispone de una interfaz MVC para su consulta usando el framework MaterializeCSS.
Para la capa de aplicación, se ha optado por un acercamiento en base a casos de uso, promoviendo un patrón CQRS y manteniendo métodos sencillos.

Para todos los proyectos, excepto para el proyecto Web, se han utilizado los [Nullable Reference Types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references), que permiten ser más explícito en el tipado de los objetos. Esto permite obviar muchas comprobaciónes con null y tener un código más seguro. También se han utilizado las anotaciones en las extensiones (definidas en el proyecto de Domain) para una buena propagación de las advertencias del compilador.

## Ejecución
Lo primero que hay que ejecutar es la API REST, que es la que dará acceso a la aplicación. Para ello, desde el directorio Inventory.API se ejecuta en un terminal:

`dotnet run`

Una vez ejecutado, la API se expondrá a través de swagger en `https://localhost:5003`.

Una vez lanzado swagger, para poder acceder a los métodos securizados es necesario autenticarse primero. Para la autenticación, es necesario llamar al método _Authenticate_. Las credenciales por defecto (hardcoded) son:

Usuario: admin

Password: 123456

Una vez iniciada la sesión, pulsando en el botón Authorize dentro de swagger, hay que copiar el token generado por el método _Authorize_ en la forma:

>Bearer *_Token_*

Si se desea tener acceso a la interfaz MVC que conecta con la API, lo ideal es ejecutarla desde el propio Visual Studio. También se puede ejecutar directamente con IIS yendo al directorio de instalación de IIS y ejecutando:
`"%ProgramFiles%\IIS Express\iisexpress.exe" /path:"<RUTA A LA APLICACION>\src\Inventory.Web"`

Esto expondrá la web en la ruta `localhost:8080` (por defecto en IIS express).

A continuación se desglosan en detalle los proyectos que forman la aplicación.

---
## Domain
### Dependencias
Ninguna
### Contenido del proyecto
Este proyecto contiene la información básica para el funcionamiento. Desglosando por carpetas:
* _DateTimeProvider_ contiene una interfaz para proveer de fecha actual de cara al testeo.
* _Entities_ contiene todas las entidades de la aplicación más la entidad base, que sirve de referencia. La entidad base contiene además campos de auditoría que se completan durante el guardado de los cambios.
* _Events_ contiene los eventos base de la capa del dominio junto con la interfaz para el bus de eventos, creado para propagar eventos a sistemas externos (ej. RabbitMQ)
* _Exceptions_ contiene las excepciones base de la aplicación, de las que podrán heredar otras excepciones.
* _Extensions_ contiene _extension methods_ que son usados comunmente para dar facilidad al desarrollo.
* _Persistence_ contiene todo lo relacionado con el guardado de datos, en concreto los repositorios, unit of work y el usuario de la auditoría, que se usa para rellenar los campos de auditoría.
* _UseCases_ contiene las interfaces básicas para crear los casos de uso de los que se compone la aplicación.
---
## Application
### Dependencias
Ninguna
### Contenido del proyecto
Debido a la sencillez de los casos de uso, este proyecto solo contiene una carpeta, que a su vez contiene la información sobre todos los casos de uso de la aplicación.
Cada subcarpeta contiene a su vez la información necesaria para cada caso de uno, es decir:
* _Request_, que contiene la información que se necesita para ejecutar el caso de uso.
* _Response_, que define la información que se devolverá.
* _Handler_ junto con su interfaz que definen cómo se ejecuta ese caso de uso.
* También están dentro de cada carpeta sus excepciones y eventos, de cara a la organización del proyecto.

---
## Infrastructure
### Dependencias
Para el proyecto de infrastructura se utilizan dos dependencias, ambas de Entity Framework (EF), para poder trabajar en memoria y definir las estructuras de las tablas. El uso de EF en memoria nos permite un testeo rápido sin necesidad de recurrir a proveedores externos, dejando además código hecho que funcionará al cambiar de proveedor de BBDD (MySql, PostgreSQL, etc.)

### Contenido del proyecto
Debido a la mayor complejidad del proyecto, detallamos por fichero cada una de las funciones.
* `AuditUser.cs` es una definición básica del usuario de auditoría, que se inyectará en tiempo de ejecución con los datos necesarios para guardar quién ha hecho las modificaciones de la entidad.
* `InventoryDbContext.cs` especifica un DbContext de EF y sobreescribe el método SaveChangesAsync para guardar la auditoría antes de guardar los cambios en BBDD.
* `NullEventBus.cs` es una implementación del vacía para bus de eventos.
* `ProductRepository.cs` expone los métodos del repositorio de acceso a los productos. Se ha intentado seguir en su mayor parte la [Guía para la arquitectura de Microsoft.](https://docs.microsoft.com/es-es/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core)
* `UnitOfWork.cs` provee de una abstracción al DbContext (wrapper) para no exponer una dependencia a la capa de aplicación. Esta clase también se encarga de propagar los eventos a través del bus definido en la capa de Dominio.
* La carpeta _Configuration_ contiene la configuración de las entidades. Para la entidad base se ha utilzado el patrón _template method_ o _método plantilla_ para forzar la declaración de una configuración que se hará antes de la configuración de la entidad base.
---
## API
### Dependencias
* _FluentValidation_ para la validación de las peticiones entrantes.
* _Hangfire_ para la programación de tareas. En este caso, la comprobación de expiración de productos.
* _Entity Framework_ para la inyección de dependencias y el acceso a datos
* _Authentication.JwtBearer_ para la configuración de autenticación por token JWT.
* _Scrutor_ para ayudar al registro de dependencias en el contenedor de AspNetCore.
* _Swashbuckle_ para la configuración de swagger y exponer la información de la API.

De estas dependencias, _Hangfire_ puede ser prescindible si se decidiera utilizar un método externo para la comprobación de los productos expirados, por ejemplo utilizando un [worker service](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio) o con un batch creado específicamente para el proceso. También se podría eliminar la necesidad de _Scrutor_, ya que es posible analizar el _assembly_ a mano para busar los tipos, pero la facilidad que da para el registro de classes de cara a su adición al contenedor de dependencias ha favorecido su inclusión.

### Contenido del proyecto
Como es normal, en `Startup.cs` está definida la configuración del contenedor para la inyección de dependencias, así como la configuración del token JWT, Swagger, Hangfire, etc. También aquí está creado el usuario de auditoría. Por cada scope (request http) se revisará el HttpContext y si el usuario está logado, se utilizará ese nombre para guardarlo en la auditoría.

Las carpetas _Authentication_ y _Products_ son similares: ambas contienen el controller en cuestión para la funcionalidad correspondiente y una carpeta por cada caso de uso con los modelos asociados.

Para el caso de _Authentication_, la autenticación se hace sobre unas credenciales _hardcoded_ (usuario: admin, password: 123456). Esto se ha hecho sólo como prueba de concepto. Lo correcto sería aquí utilizar el UserManager, o contactar con el servicio necesario de cara a la verificación de la identidad del usuario.

Para los productos, todas las llamadas se realizan utilizando los casos de uso disponibles desde la capa de aplicación. Tras ello, devuelven el código correspondiente.

La carpeta _Infrastructure_ contiene los elementos más trasversales al proyecto:
* `ActionResults/InternalServerErrorObjectResult.cs` es la clase que contiene el object result que se devolverá cuando se produzca un error 500.
* `BackgroundJobs/NotifyExpiredProductsJob.cs` contiene el trabajo recurrente de notifiación de expiración de productos que se lanza con _Hangfire_.
* `Filters/GlobalExceptionFilter.cs` es un filtro global para la gestión de excepciones comunes. Por ejemplo, para el caso de un producto que haya que sacar pero que no exista en la BBDD, se lanzará una excepción `ProductNotFoundException`, que será capturada por este filtro para devolver adecuadamente un error 404.

Como se puede comprobar, el caso de uso de sacar un producto lanza una excepción si el producto no existe, mientras que cuando se intenta leer un producto por nombre, simplemente se devuelve el objeto. Esto está hecho así a propósito, ya que intentar tomar un producto puede devolver null, pero realizar una acción sobre un producto que no existe no es posible.

### Nota adicional
Debido a la arquitectura de la aplicación, se podría haber usado para esta API el paquete MediatR como mediador de la gestión de casos de uso. Aunque en principio pueda parecer razonable, se ha prescindido conscientemente de este patrón porque, al igual que el patrón _Service Locator_, esconde parte de la información.

Visualmente, esta definición
```
public ProductController(
            ICreateProductHandler createProductUseCase,
            IGetProductByNameHandler getProductByNameHandler,
            IRemoveProductByNameHandler removeProductByNameHandler,
            IGetAllProductsHandler getAllProductsHandler)
        {
            _createProductHandler = createProductUseCase;
            _getProductByNameHandler = getProductByNameHandler;
            _removeProductByNameHandler = removeProductByNameHandler;
            _getAllProductsHandler = getAllProductsHandler;
        }
```
contiene mucha más información que esta otra
```
public ProductController(IMediator mediator)
{
    _mediator = mediator;
}
```

y por ese motivo se ha preferido no incluirlo.

Por otra parte, el sistema de generación del token es básico, y por el momento no permite refrescar el token. De cara a una aplicación para producción, este sistema debería realizarse para evitar que el usuario tenga que volver a iniciar sesión.

---
## Web
### Dependencias
La única dependencia añadida ha sido la necesaria para la gestión de los token JWT. El resto de dependencias son las estándar de un proyecto MVC. De las existentes, varias de ellas podrían incluso ser eliminadas (Bootstrap, Entity Framework...)

### Contenido del proyecto
* La carpeta _Api_ contiene toda la información necesaria para la conexión con la Api. En concreto:
    * Contiene una `ApiAuthenticationException` definida para cuando no estemos identificados en la API. Al igual que en el proyecto de la API, esta excepción será capturada en un controlador base creado para el proyecto (`BaseController.cs`) que gestionará la excepción redireccionando al usuario a la pantalla de login.
    * `ApiHelpers` contiene helpers de cara a la simplificación de la creación de las llamadas a la API.
    * `ApiService` expone las llamadas de la API.
* Para la autenticación, en el método de Login se capturan los parámetros y se envían a la API, que será la encargada de validar nuestra identidad y devolvernos el token si todo ha ido bien. Este token será guardado posteriormente en una variable de sesión para este usuario.
* Para la parte del frontend, se ha utilizado MaterializeCSS de cara a dar un aspecto visual más atractivo.

### Nota adicional
El proyecto web sólo se añade como ejemplo de conexión a la API y de creación de un frontend. De cara a producción, el proyecto debe ser revisado para eliminar el código no usado (EF, etc.), securizado correctamente, establecido un contenedor de inyección de dependencias, etc.

---
# Tests
### Dependencias
Para el proyecto de tests unitarios, se utiliza _Moq_ como framework para el mocking. En el poryecto con test de integración, se utiliza además el propio framework de testeo de Microsoft para Mvc (`Microsoft.AspNetCore.Mvc.Testing`).

### Contenido del proyecto
De ambos, destacar el helper existente en el proyecto de integración, diseñado para obviar la autenticación de cara a testear los casos de uso que requieren de autenticación.

### Nota adicional
De cara a su uso en un entorno 100% real, sería necesario implementar un sistema correcto de autenticación, ya que se podrían tener roles, usuarios con diferentes permisos... Estas casuísticas deben ser testeadas y no es posible por el momento al no existir métodos disponibles.

---
## Calidad
La solución se ha analizado con SonarQube, apareciendo sólo problemas menores. Un análisis completo arroja errores graves en la parte de JS, provenientes de los ficheros de jQuery. Estos problemas, que pueden estar corregidos en versiones más recientes, quedan fuera del objetivo de este proyecto.