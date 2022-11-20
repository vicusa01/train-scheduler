using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.ViewModels
{
    public class UpdateTicketModel
    {
        public int Id { get; set; }

        public int ScheduleId { get; set; }

        [Required]
        public string UserId { get; set; }

        public double Price { get; set; }

        [Required]
        public string Fio { get; set; }

        public List<SelectListItem> Schedules { set; get; }

        // public IEnumerable<Schedule> Schedules { get; set; }

        public IEnumerable<IdentityUser> Users { get; set; }
    }
}
