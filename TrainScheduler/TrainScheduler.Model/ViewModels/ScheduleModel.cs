using System;
using System.Collections.Generic;
using System.Text;
using TrainScheduler.Model.Dto;

namespace TrainScheduler.Model.ViewModels
{
    public class ScheduleModel
    {
        public DateTime Date { get; set; }

        public List<ScheduleDto> Schedules { get; set; }
    }
}
