using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrainScheduler.Model.ViewModels
{
    public class CreateStopModel
    {
        [Required]
        public string Name { get; set; }
    }
}
