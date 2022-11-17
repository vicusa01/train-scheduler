using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.ViewModels
{
    public class UpdateDestinationModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int DepartureId { get; set; }

        public int ArrivalId { get; set; }

        public int TrainId { get; set; }

        public IEnumerable<Stop> Stops { get; set; }

        public IEnumerable<Train> Trains { get; set; }
    }
}
