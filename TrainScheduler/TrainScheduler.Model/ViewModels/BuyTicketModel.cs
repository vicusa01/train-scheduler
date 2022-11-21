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

        //USER ID

        public double Price { get; set; }

        [Required]
        public string Fio { get; set; }

        public Schedule Schedule { set; get; }
    }
}
