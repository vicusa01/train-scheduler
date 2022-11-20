using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainScheduler.Model.Dto;
using TrainScheduler.Model.Entities;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.Model.Interfaces
{
    public interface ITicketService : IEntityServiceBase<Ticket>
    {
        Task CreateAsync(CreateTicketModel model);

        Task UpdateAsync(UpdateTicketModel model);

        Task<List<Ticket>> FindTicketsAsync(string userId, string fio);

        Task<List<TicketsReportDto>> GetTicketsReportAsync(DateTime from, DateTime to);
    }
}
