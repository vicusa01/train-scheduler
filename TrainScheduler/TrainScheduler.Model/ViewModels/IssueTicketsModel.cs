using System;
using System.Collections.Generic;
using System.Text;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.ViewModels
{
    public class IssueTicketsModel
    {
        public List<BuyTicketModel> Tickets { get; set; }
    }
}
