using System;
using System.Collections.Generic;
using System.Text;
using TrainScheduler.Model.Dto;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.ViewModels
{
    public class AvailableSeatsModel
    {
        public AvailableSeatsModel()
        {
            Destinations = new List<Destination>();
            AvailableSeats = new List<AvailableSeatsDto>();
        }

        public int DestinationId { get; set; }

        public DateTime Date { get; set; }

        public List<Destination> Destinations { get; set; }

        public List<AvailableSeatsDto> AvailableSeats { get; set; }
    }
}
