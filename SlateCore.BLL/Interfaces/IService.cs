using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlateCORE.Common.Interfaces;

namespace SlateCore.BLL.Interfaces
{
    public interface IService<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int? id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(int? id);
        void Dispose();
    }
}
