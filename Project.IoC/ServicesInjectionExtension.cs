using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Project.Core.Entities;
using Project.Domain.Users;
using Project.Infra.Database.Context;
using Project.Infra.Domain;
using Project.Infra.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Application.Users.CreateUserUseCase;

namespace Project.IoC
{
    public static class ServicesInjectionExtension 
    {
        public static void Register(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Application - handlers
            services.AddMediatR(typeof(CreateUserHandler).Assembly);

            // Infra - data
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
