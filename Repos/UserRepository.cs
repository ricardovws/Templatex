using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class UserRepository<T> : IUserRepository<T>
    {
        public Task Save(T user)
        {
            // Lógica para gravar em base de dados 

            return Task.CompletedTask;
        }
    }
}
