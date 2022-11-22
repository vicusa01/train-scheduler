using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainScheduler.Core.Database;
using TrainScheduler.Model.Entities;
using TrainScheduler.Model.Exceptions;
using TrainScheduler.Model.Interfaces;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.Core.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly TrainSchedulerContext _dbContext;
        public DestinationService(TrainSchedulerContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task CreateAsync(CreateDestinationModel model)
        {
            _dbContext.Destinations.Add(new Destination()
            {
                Name = model.Name,
                Price = model.Price,
                DepartureId = model.DepartureId,
                ArrivalId = model.ArrivalId,
                TrainId = model.TrainId
            });

            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var destination = await _dbContext.Destinations.FindAsync(id);
            if (destination == null)
            {
                throw new ObjectNotFoundException(nameof(Train));
            }

            _dbContext.Destinations.Remove(destination);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateDestinationModel model)
        {
            var destination = await _dbContext.Destinations.FindAsync(model.Id);
            if (destination == null)
            {
                throw new ObjectNotFoundException(nameof(Stop));
            }

            destination.Name = model.Name;
            destination.Price = model.Price;
            destination.DepartureId = model.DepartureId;
            destination.ArrivalId = model.ArrivalId;
            destination.TrainId = model.TrainId;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Destination> GetByIdAsync(int id)
        {
            return await _dbContext.Destinations.FindAsync(id);
        }

        public Task<List<Destination>> GetAllAsync()
        {
            return _dbContext.Destinations
                             .Include(x => x.Departure)
                             .Include(x => x.Arrival)
                             .Include(x => x.Train)
                             .AsNoTracking()
                             .ToListAsync();
        }
    }
}
