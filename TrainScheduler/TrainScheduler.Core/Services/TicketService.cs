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
using System.Linq;
using System.Linq.Expressions;
using TrainScheduler.Model.Dto;

namespace TrainScheduler.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly TrainSchedulerContext _dbContext;
        public TicketService(TrainSchedulerContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task CreateAsync(CreateTicketModel model)
        {
            _dbContext.Tickets.Add(new Ticket()
            {
                ScheduleId = model.ScheduleId,
                UserId = model.UserId,
                Price = model.Price,
                Fio = model.Fio,
                PurchaseTime = DateTime.Now
            });

            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await _dbContext.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new ObjectNotFoundException(nameof(Ticket));
            }

            _dbContext.Tickets.Remove(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTicketModel model)
        {
            var ticket = await _dbContext.Tickets.FindAsync(model.Id);
            if (ticket == null)
            {
                throw new ObjectNotFoundException(nameof(Ticket));
            }

            ticket.ScheduleId = model.ScheduleId;
            ticket.UserId = model.UserId;
            ticket.Price = model.Price;
            ticket.Fio = model.Fio;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _dbContext.Tickets.FindAsync(id);
        }

        public Task<List<Ticket>> GetAllAsync()
        {
            return _dbContext.Tickets
                .Include(x => x.Schedule)
                .ThenInclude(x => x.Destination)
                .Include(x => x.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<Ticket>> FindTicketsAsync(string userId, string fio)
        {
            var query = _dbContext.Tickets
               .Include(x => x.Schedule)
               .ThenInclude(x => x.Destination)
               .Include(x => x.User)
               .AsQueryable();
              
            if (!string.IsNullOrWhiteSpace(userId))
            {
                query = query.Where(x => x.UserId == userId);
            }

            if (!string.IsNullOrWhiteSpace(fio))
            {
                query = query.Where(x => x.Fio.Contains(fio));
            }

            return query.AsNoTracking().ToListAsync();
        }

        public Task<List<TicketsReportDto>> GetTicketsReportAsync(DateTime from, DateTime to)
        {
            return _dbContext.Tickets
                .AsNoTracking()
                .Where(t => t.PurchaseTime >= from && t.PurchaseTime <= to)
                .GroupBy(s => s.Schedule.Destination.Name)
                .Select(g => new TicketsReportDto()
                {
                    Destination = g.Key,
                    TicketsSolt = g.Count()
                })
                .ToListAsync();
        }
    }
}
