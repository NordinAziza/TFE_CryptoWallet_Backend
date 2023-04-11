using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Framework;

namespace WalletApi.Domain
{
    public  interface IUsersRepository : IRepository
    {
        ICollection<Users> GetAll(int? userId);
        Users Register(Users user);
    }
}
