using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
    }
}
