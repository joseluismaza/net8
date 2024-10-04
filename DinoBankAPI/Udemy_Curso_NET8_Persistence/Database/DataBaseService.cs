using Newtonsoft.Json;
using Udemy_Curso_NET8_Domain.User;

namespace Udemy_Curso_NET8_Persistence.Database
{
    public class DataBaseService : InterfaceDataBaseService
    {
        //private static string route = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), "Udemy_Curso_NET8_Persistence", "Files");
        private static string route = Path.Combine(Directory.GetCurrentDirectory(),"Files");

        //Obtener lista de usuarios
        public List<UserEntity> GetAll()
        {
            var file = Path.Combine(route, "User.JSON"); //buscar archivo json
            using (StreamReader r = new StreamReader(file)) //lee el archivo json 
            {
                var json = r.ReadToEnd();
                 return JsonConvert.DeserializeObject<List<UserEntity>>(json) ?? new List<UserEntity>();
            }

        }

        //Crear usuarios
        public bool Create(UserEntity user)
        {
            var file = Path.Combine(route, "User.JSON"); //buscar archivo json
            using (StreamReader r = new StreamReader(file)) //lee el archivo json
            {
                var json = r.ReadToEnd();
                r.Close();//cerrar el archivo para manipularlo
                var listUser = JsonConvert.DeserializeObject<List<UserEntity>>(json) ?? new List<UserEntity>(); //obtener la lista del archivo y añadirlo en caso contrario en una nueva

                //Encontrar el último ID más alto existente en la lista de usuarios
                int lastId = listUser.Count > 0 ? listUser.Max(u => u.Id) : 0;

                //asignar el nuevo ID sumando 1 al último
                user.Id = lastId + 1;

                listUser.Add(user);
                json = JsonConvert.SerializeObject(listUser, Formatting.Indented); //nueva lista con el nuevo usuario creado
                File.WriteAllText(file, json); //escribir el dato en el archivo
                return true;
            }

        }
        //Actualizar lista usuarios
        public bool Update(UserEntity user)
        {
            var file = Path.Combine(route, "User.JSON"); //buscar archivo json
            using (StreamReader r = new StreamReader(file)) //lee el archivo json
            {
                var json = r.ReadToEnd();
                r.Close();//cerrar el archivo para manipularlo
                var listUser = JsonConvert.DeserializeObject<List<UserEntity>>(json);

                if (listUser == null || listUser.Count == 0)//si la lista es nula o vacía devolver false
                    return false;
                //si tiene contenido en que posición esta el usuario a manipular
                var index = listUser.FindIndex(x => x.Id == user.Id);//en que posición se encuentra el usuario

                //validación, si tiene contenido se remplaza
                if (index != -1)
                {
                    listUser[index] = user;
                    json = JsonConvert.SerializeObject(listUser, Formatting.Indented); //nueva lista con el nuevo usuario actualizado
                    File.WriteAllText(file, json); //escribir el dato en el archivo actualizado
                    return true;
                }

                return false;
            }

        }
        //Eliminar lista usuarios
        public bool Delete(int id)
        {
            var file = Path.Combine(route, "User.JSON"); //buscar archivo json
            using (StreamReader r = new StreamReader(file)) //lee el archivo json
            {
                var json = r.ReadToEnd();
                r.Close();//cerrar el archivo para manipularlo

                var listUser = JsonConvert.DeserializeObject<List<UserEntity>>(json);//convertir a un modelo UserEntity para poder manipular el archivo

                if (listUser == null || listUser.Count == 0)//si la lista es nula o vacía devolver false
                    return false;

                bool remove = listUser.RemoveAll(x => x.Id == id) > 0;

                if (remove)
                {
                    json = JsonConvert.SerializeObject(listUser, Formatting.Indented); //nueva lista con el nuevo usuario actualizado
                    File.WriteAllText(file, json); //escribir el dato en el archivo actualizado
                }
                return remove;
            }

        }
    }
}
