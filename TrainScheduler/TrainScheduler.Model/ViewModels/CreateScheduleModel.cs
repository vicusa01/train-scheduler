﻿using System;
using System.Collections.Generic;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.ViewModels
{
    public class CreateScheduleModel
    {
        public int DestinationId { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public IEnumerable<Destination> Destinations { get; set; }
    }
}
