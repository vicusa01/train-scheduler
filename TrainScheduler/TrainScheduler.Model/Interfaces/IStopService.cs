using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainScheduler.Model.Entities;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.Model.Interfaces
{
    public interface IStopService
    {
        Task CreateAsync(CreateStopModel model);

        Task DeleteAsync(int id);

        Task UpdateAsync(UpdateStopModel model);

        Task<Stop> GetByIdAsync(int id);

        Task<IEnumerable<Stop>> GetAllAsync();
    }
}
