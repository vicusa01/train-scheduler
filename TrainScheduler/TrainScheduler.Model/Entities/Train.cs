using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainScheduler.Model.Entities
{
    [Table("Trains")]
    public class Train
    {
        [Key]
        public int Id { get; set; }

        public string Number { get; set; }

        public string TrainType { get; set; }

        public int Seats { get; set; }

        public virtual ICollection<Destination> Destinations { get; set; }
    }
}
