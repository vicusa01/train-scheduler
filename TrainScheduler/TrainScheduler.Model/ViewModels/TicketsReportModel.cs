using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainScheduler.Model.Dto;

namespace TrainScheduler.Model.ViewModels
{
    public class TicketsReportModel
    {
        public TicketsReportModel()
        {
            Report = new List<TicketsReportDto>();
        }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public List<TicketsReportDto> Report { get; set; }

        public int TicketsSolt => Report.Sum(r => r.TicketsSolt);
    }
}
