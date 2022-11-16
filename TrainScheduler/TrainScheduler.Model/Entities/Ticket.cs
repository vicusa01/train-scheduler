using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainScheduler.Model.Entities
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Schedule))]
        public int ScheduleId { get; set; }

        public double Price { get; set; }

        public DateTime PurchaseTime { get; set; }

        public virtual Schedule Schedule { get; set; }
    }
}
