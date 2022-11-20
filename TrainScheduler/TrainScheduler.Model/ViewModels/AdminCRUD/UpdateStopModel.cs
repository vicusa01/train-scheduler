using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrainScheduler.Model.ViewModels
{
    public class UpdateStopModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
