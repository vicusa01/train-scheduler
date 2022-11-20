using System;
using System.Collections.Generic;
using System.Text;

namespace TrainScheduler.Model.Dto
{
    public class ScheduleDto
    {
        public int Id { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }
    }
}
