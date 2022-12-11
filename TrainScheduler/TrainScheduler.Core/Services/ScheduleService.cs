using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainScheduler.Core.Database;
using TrainScheduler.Model.Dto;
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
                DestinationId = model.DestinationId,
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

        public Task<List<Schedule>> GetAllAsync()
        {
            return _dbContext.Schedules
                             .Include(x => x.Destination)
                             .AsNoTracking()
                             .ToListAsync();
        }

        public async Task<List<AvailableSeatsDto>> GetAvailableSeatsAsync(int departureStopId, int arrivalStopId, DateTime date)
        {
            var schedules = await _dbContext.Schedules
               .AsNoTracking()
               .Where(s => s.Destination.DepartureId == departureStopId &&
                           s.Destination.ArrivalId == arrivalStopId &&
                           s.DepartureTime.Date == date.Date)
               .Select(x => new
               {
                   Schedule = x,
                   Price = x.Destination.Price,
                   Tickets = x.Tickets,
                   TrainSeats = x.Destination.Train.Seats
               })
               .ToListAsync();

            var results = schedules.Select(s => new AvailableSeatsDto()
            {
                Schedule = s.Schedule,
                Price = s.Price,
                Seats = s.TrainSeats - s.Tickets.Count
            })
            .ToList();

            return results;
        }

        public Task<List<ScheduleDto>> GetByDateAsync(DateTime date)
        {
            return _dbContext.Schedules
                .AsNoTracking()
                .Where(s => s.DepartureTime.Date == date.Date)
                .Select(s => new ScheduleDto()
                {
                    Id = s.Id,
                    DepartureTime = s.DepartureTime,
                    ArrivalTime = s.ArrivalTime,
                    Departure = s.Destination.Departure.Name,
                    Arrival = s.Destination.Arrival.Name
                })
                .ToListAsync();
        }

        public async Task<bool> HasAvaiableTickets(int scheduleId, int ticketsInCart)
        {
            var model = await _dbContext.Schedules
                .AsNoTracking()
                .Where(s => s.Id == scheduleId)
                .Select(s => new
                {
                    TicketsCount = s.Tickets.Count,
                    TrainTickets = s.Destination.Train.Seats
                })
                .SingleOrDefaultAsync();

            return (model.TrainTickets - model.TicketsCount - ticketsInCart) >= 0;
        }
    }
}