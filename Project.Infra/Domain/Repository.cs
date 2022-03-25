using Project.Core.Entities;
using Project.Infra.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project.Infra.Domain
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ProjectContext _context;

        public Repository(ProjectContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(T obj)
        {
           _context.Set<T>().Add(obj);
        }

        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
        }

        public void Update(T obj)
        {
            _context.Set<T>().Update(obj);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
