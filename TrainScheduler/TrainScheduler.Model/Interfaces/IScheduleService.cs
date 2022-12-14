using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainScheduler.Model.Dto;
using TrainScheduler.Model.Entities;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.Model.Interfaces
{
    public interface IScheduleService : IEntityServiceBase<Schedule>
    {
        Task CreateAsync(CreateScheduleModel model);

        Task UpdateAsync(UpdateScheduleModel model);

        Task<List<AvailableSeatsDto>> GetAvailableSeatsAsync(int departureStopId, int arrivalStopId, DateTime date);

        Task<List<ScheduleDto>> GetByDateAsync(DateTime date);

        Task<bool> HasAvaiableTickets(int scheduleId, int ticketsInCart);
    }
}
