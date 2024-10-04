NET 8 API Project

Este proyecto es una API RESTful construida con .NET 8 que gestiona usuarios con operaciones CRUD (Crear, Leer, Actualizar, Eliminar). Los usuarios se almacenan en un archivo JSON local y cada operación manipula esta base de datos basada en archivos. El proyecto implementa inyección de dependencias y tiene varios endpoints para interactuar con los datos de usuario.

Características

.NET 8: Implementación de los métodos GET, POST, PUT y DELETE.
Inyección de dependencias: Uso de un servicio de base de datos para la manipulación de los usuarios.
Archivo JSON como base de datos.
Manejo de IDs incrementales: Al eliminar un usuario, la nueva ID generada continúa desde el último valor sin reutilizar IDs eliminadas.
Endpoints
La API proporciona los siguientes endpoints para la gestión de usuarios:

GET /api/user/getall

Obtiene una lista de todos los usuarios almacenados.
{
  "Id": "1",
  "UserName": "user001",
  "Password": "pass001",
  "Type":"External"
}
  
GET /api/user/getbyId/{id}

Obtiene un usuario específico basado en su ID.

Parámetro: 

id: El ID del usuario.
{
  "Id": 1,
  "UserName": "user001",
  "Password": "pass001",
  "Type": "External"
}

GET /api/user/getUserName/{userName}

Obtiene un usuario basado en su nombre de usuario.

Parámetro:

userName: El nombre de usuario.

GET /api/user/getbyType/{type}

Obtiene una lista de usuarios que coinciden con un tipo específico (por ejemplo, Internal o External).

Parámetro:

type: El tipo de usuario (Internal, External).

POST /api/user/create

Crea un nuevo usuario. La API genera automáticamente el próximo ID disponible.

Parámetros:

{
  "UserName": "user004",
  "Password": "pass004",
  "Type": "External"
}

PUT /api/user/update

Actualiza un usuario existente basado en el ID proporcionado.

{
  "Id": 4,
  "UserName": "user005",
  "Password": "pass005",
  "Type": "Internal"
}

DELETE /api/user/delete/{id}

Elimina un usuario basado en su ID.

Parámetro:

id: El ID del usuario que se desea eliminar.

Archivos importantes

UserController.cs: Controlador de la API donde están definidos todos los endpoints.
DataBaseService.cs: Servicio que maneja la lógica de acceso a la "base de datos" JSON.
User.JSON: Archivo donde se almacenan los datos de los usuarios.
InterfaceDataBaseService.cs: Interfaz para el servicio de base de datos.
