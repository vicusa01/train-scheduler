using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrainScheduler.Model.Interfaces
{
    public interface IEntityServiceBase<TEntity> where TEntity : class
    {
        Task DeleteAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
