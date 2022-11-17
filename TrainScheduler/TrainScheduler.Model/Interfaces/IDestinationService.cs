using System.Threading.Tasks;
using TrainScheduler.Model.Entities;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.Model.Interfaces
{
    public interface IDestinationService : IEntityServiceBase<Destination>
    {
        Task CreateAsync(CreateDestinationModel model);

        Task UpdateAsync(UpdateDestinationModel model);
    }
}
