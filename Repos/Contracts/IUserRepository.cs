using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public interface IUserRepository<T>
    {
        Task Save(T user);
    }
}
