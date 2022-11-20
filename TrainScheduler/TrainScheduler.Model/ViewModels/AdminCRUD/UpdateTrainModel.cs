using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrainScheduler.Model.ViewModels
{
    public class UpdateTrainModel
    {
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string TrainType { get; set; }

        [Range(1, 100000)]
        public int Seats { get; set; }
    }
}
