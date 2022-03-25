using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Entities
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();
        void Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task Save();
    }
}
