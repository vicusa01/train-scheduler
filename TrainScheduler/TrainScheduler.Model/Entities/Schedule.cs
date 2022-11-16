using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainScheduler.Model.Entities
{
    [Table("Schedules")]
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Destination))]
        public int DestinationId { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public virtual Destination Destination { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
