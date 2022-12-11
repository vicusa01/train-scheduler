using System;
using System.Collections.Generic;
using TrainScheduler.Model.Dto;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.ViewModels
{
    public class AvailableSeatsModel
    {
        public AvailableSeatsModel()
        {
            Stops = new List<Stop>();
            AvailableSeats = new List<AvailableSeatsDto>();
        }

        public int DepartureStopId { get; set; }

        public int ArrivalStopId { get; set; }

        public DateTime Date { get; set; }

        public List<Stop> Stops { get; set; }

        public List<AvailableSeatsDto> AvailableSeats { get; set; }
    }
}
