using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.ViewModels
{
    public class BuyTicketModel
    {
        public int ScheduleId { get; set; }

        public string DestinationName { set; get; }

        public double DestinationPrice { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        [Required]
        [RegularExpression(@"([A-Z][a-z]+[\-\s]?){3}", ErrorMessage = "Input valid FIO")]
        public string Fio { get; set; }

        public string UserId { get; set; }
    }
}
