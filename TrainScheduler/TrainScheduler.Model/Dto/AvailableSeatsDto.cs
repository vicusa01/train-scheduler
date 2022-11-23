using System;
using System.Collections.Generic;
using System.Text;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.Dto
{
    public class AvailableSeatsDto
    {
        public int Seats { get; set; }

        public double Price { get; set; }

        public Schedule Schedule { get; set; }
    }
}
