using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SlateCORE.Common.Interfaces;

namespace SlateCore.DAL.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByID(int id);
        Task<IEnumerable<T>> GetByPredicate(Expression<Func<T, Boolean>> predicate);
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
    }
}
