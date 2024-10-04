using Microsoft.AspNetCore.Mvc;
using Udemy_Curso_NET8_Domain.User;
using Udemy_Curso_NET8_Persistence.Database;

namespace Udemy_Curso_NET8.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly InterfaceDataBaseService _interfaceDataBaseService;
        public UserController(InterfaceDataBaseService interfaceDataBaseService)
        {
            _interfaceDataBaseService = interfaceDataBaseService; //inyección de dependencias y así poder manipular
        }
        //[HttpGet("sample")]
        //public IActionResult Sample()
        //{
        //Crear usuarios
        //var user = new UserEntity
        //{
        //    UserName = "user004",
        //    Password = "pass004",
        //    Type = "External"
        //};
        //var data = _interfaceDataBaseService.Create(user);

        //Actualizar usuario
        //var user = new UserEntity
        //{
        //    Id = 4,
        //    UserName = "user005",
        //   Password = "pass005",
        //    Type = "Internal"
        //};
        //var data = _interfaceDataBaseService.Update(user);

        //Eliminar usuario
        //var user = new UserEntity
        //{
        //    Id = 4,
        //    UserName = "user005",
        //    Password = "pass005",
        //    Type = "Internal"
        //};
        //var data = _interfaceDataBaseService.Delete(user);
        //return StatusCode(StatusCodes.Status200OK, data);
        //}


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var data = _interfaceDataBaseService.GetAll();
                if (data != null && data.Count == 0)
                    return StatusCode(StatusCodes.Status404NotFound);

                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }


        [HttpGet("getbyId/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _interfaceDataBaseService.GetAll();
            if (data == null && data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound);

            var user = data.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpGet("getUserName/{userName}")]
        public IActionResult GetByUserName(string userName)
        {
            var data = _interfaceDataBaseService.GetAll();
            if (data == null && data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound);

            var user = data.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpGet("getbyType/{type}")]
        public IActionResult GetByType(string type)
        {
            var data = _interfaceDataBaseService.GetAll();
            if (data == null && data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound);

            var users = data.Where(x => x.Type == type).ToList();//traer a todos los que sean de un tipo
            if (users == null && users.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpPost("create")]
        public IActionResult Create(UserEntity user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Type))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Parámetros no válidos");
            }

            var data = _interfaceDataBaseService.Create(user);

            if (!data)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status200OK);

        }

        [HttpPut("update")]
        public IActionResult Update(UserEntity user) 
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Type) || user.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Parámetros no válidos");
            }

            var data = _interfaceDataBaseService.Update(user);
            if(!data)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return StatusCode(StatusCodes.Status400BadRequest, "Id no válido");

            var data = _interfaceDataBaseService.Delete(id);
            if (!data)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(StatusCodes.Status200OK);
        }


    }
}
