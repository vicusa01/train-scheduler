using System.Threading.Tasks;
using TrainScheduler.Model.Entities;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.Model.Interfaces
{
    public interface ITrainService : IEntityServiceBase<Train>
    {
        Task CreateAsync(CreateTrainModel model);

        Task UpdateAsync(UpdateTrainModel model);
    }
}
