using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApotekConsoleApp.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
        Task<bool> Delete(string kode);

    }
}
