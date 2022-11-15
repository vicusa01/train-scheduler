﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainScheduler.Model.Entities
{
    [Table("Destinations")]
    public class Destination
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Departure))]
        public int DepartureId { get; set; }

        [ForeignKey(nameof(Arrival))]
        public int ArrivalId { get; set; }

        public virtual Stop Departure { get; set; }

        public virtual Stop Arrival { get; set; }
    }
}
