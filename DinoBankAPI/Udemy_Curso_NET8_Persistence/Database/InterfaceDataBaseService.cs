using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_Curso_NET8_Domain.User;

namespace Udemy_Curso_NET8_Persistence.Database
{
    public interface InterfaceDataBaseService
    {
        List<UserEntity> GetAll();
        bool Create(UserEntity user);
        bool Update(UserEntity user);
        bool Delete( int id);
    }
}
