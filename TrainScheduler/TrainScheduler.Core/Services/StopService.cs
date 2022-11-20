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
    public class StopService : IStopService
    {
        private readonly TrainSchedulerContext _dbContext;
        public StopService(TrainSchedulerContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task CreateAsync(CreateStopModel model)
        {
            _dbContext.Stops.Add(new Stop() { Name = model.Name });
            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           // var stopHasDestinataions = await _dbContext.Destinations.AnyAsync(d => d.DepartureId == id || d.ArrivalId == id);
           // if (stopHasDestinataions)
           // {
           //     throw new InvalidOperationException("Stop has assigned destinations");
          //  }

            var stop = await _dbContext.Stops.FindAsync(id);
            if (stop == null)
            {
                throw new ObjectNotFoundException(nameof(Stop));
            }

            _dbContext.Stops.Remove(stop);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateStopModel model)
        {
            var stop = await _dbContext.Stops.FindAsync(model.Id);
            if (stop == null)
            {
                throw new ObjectNotFoundException(nameof(Stop));
            }

            stop.Name = model.Name;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Stop> GetByIdAsync(int id)
        {
            return await _dbContext.Stops.FindAsync(id);
        }

        public Task<List<Stop>> GetAllAsync()
        {
            return _dbContext.Stops.AsNoTracking().ToListAsync();
        }
    }
}
