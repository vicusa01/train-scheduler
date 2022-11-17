using System.Threading.Tasks;
using TrainScheduler.Model.Entities;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.Model.Interfaces
{
    public interface IStopService : IEntityServiceBase<Stop>
    {
        Task CreateAsync(CreateStopModel model);

        Task UpdateAsync(UpdateStopModel model);
    }
}
