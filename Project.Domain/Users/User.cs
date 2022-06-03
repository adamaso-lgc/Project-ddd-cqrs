using Project.Core.Entities;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Users
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }

        public User(string name, string email, string password, Roles role)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
