using System;
using System.Collections.Generic;
using System.Text;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.Dto
{
    public class BuyTicketDto
    {
        public int ScheduleId { get; set; }

        public string DestinationName { set; get; }

        public double DestinationPrice { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }
    }
}
