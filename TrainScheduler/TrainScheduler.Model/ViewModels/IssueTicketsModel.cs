using System;
using System.Collections.Generic;
using System.Text;
using TrainScheduler.Model.Dto;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.ViewModels
{
    public class IssueTicketsModel
    {
        public IssueTicketsModel()
        {
            Tickets = new List<BuyTicketModel>();
        }

        public List<BuyTicketModel> Tickets { get; set; }
    }
}
