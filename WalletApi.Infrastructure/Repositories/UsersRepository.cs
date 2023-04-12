using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Domain;
using WalletApi.Framework;

namespace WalletApi.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        #region 
        private readonly UsersContext _context;
        #endregion
        #region Constructor
        public UsersRepository(UsersContext context)
        { 
           _context = context;
        }
        #endregion
        #region Methods
        public ICollection<Users> GetAll(int? userId)
        {
            var query = _context.Users.AsQueryable();
            if (userId.HasValue)
            {
                query = query.Where(item => item.Id == userId.Value) ;
            }
            return query.ToList();
        }
        public Users Register(Users user)
        {
            return _context.Users.Add(user).Entity;
        }
        public Users GetUserByEmail(string email)
        { 
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }
        #endregion
        #region Properties
        public IUnitOfWork UnitOfWork => _context;
        #endregion

    }
}
