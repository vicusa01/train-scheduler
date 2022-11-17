using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainScheduler.Model.Entities;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.Model.Interfaces
{
    public interface ITrainService
    {
        Task CreateAsync(CreateTrainModel model);

        Task DeleteAsync(int id);

        Task UpdateAsync(UpdateTrainModel model);

        Task<Train> GetByIdAsync(int id);

        Task<IEnumerable<Train>> GetAllAsync();
    }
}
