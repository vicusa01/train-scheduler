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
    public class TrainService : ITrainService
    {
        private readonly TrainSchedulerContext _dbContext;
        public TrainService(TrainSchedulerContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task CreateAsync(CreateTrainModel model)
        {
            _dbContext.Trains.Add(new Train() 
            { 
                Number = model.Number,
                TrainType = model.TrainType,
                Seats = model.Seats
            });

            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var train = await _dbContext.Trains.FindAsync(id);
            if (train == null)
            {
                throw new ObjectNotFoundException(nameof(Train));
            }

            _dbContext.Trains.Remove(train);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTrainModel model)
        {
            var train = await _dbContext.Trains.FindAsync(model.Id);
            if (train == null)
            {
                throw new ObjectNotFoundException(nameof(Stop));
            }

            train.Number = model.Number;
            train.TrainType = model.TrainType;
            train.Seats = model.Seats;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Train> GetByIdAsync(int id)
        {
            return await _dbContext.Trains.FindAsync(id);
        }

        public Task<List<Train>> GetAllAsync()
        {
            return _dbContext.Trains.AsNoTracking().ToListAsync();
        }
    }
}
