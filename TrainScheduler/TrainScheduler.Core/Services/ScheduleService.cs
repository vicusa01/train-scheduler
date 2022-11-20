using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainScheduler.Core.Database;
using TrainScheduler.Model.Entities;
using TrainScheduler.Model.Exceptions;
using TrainScheduler.Model.Interfaces;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.Core.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly TrainSchedulerContext _dbContext;
        public ScheduleService(TrainSchedulerContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task CreateAsync(CreateScheduleModel model)
        {
            _dbContext.Schedules.Add(new Schedule() 
            {
                DestinationId = model.DestinationId ,
                DepartureTime = model.DepartureTime,
                ArrivalTime = model.ArrivalTime
            });

            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var schedule = await _dbContext.Schedules.FindAsync(id);
            if (schedule == null)
            {
                throw new ObjectNotFoundException(nameof(Schedule));
            }

            _dbContext.Schedules.Remove(schedule);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateScheduleModel model)
        {
            var schedule = await _dbContext.Schedules.FindAsync(model.Id);
            if (schedule == null)
            {
                throw new ObjectNotFoundException(nameof(Schedule));
            }

            schedule.DestinationId = model.DestinationId;
            schedule.DepartureTime = model.DepartureTime;
            schedule.ArrivalTime = model.ArrivalTime;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Schedule> GetByIdAsync(int id)
        {
            return await _dbContext.Schedules.FindAsync(id);
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            return await _dbContext.Schedules
                                   .Include(x => x.Destination)
                                   .AsNoTracking()
                                   .ToListAsync();
        }
    }
}
