using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TrainScheduler.Model.Entities;

namespace TrainScheduler.Model.ViewModels
{
    public class AdminBookingModel
    {
        public AdminBookingModel()
        {
            Tickets = new List<Ticket>();
            Users = new List<IdentityUser>();
        }

        public string UserId { get; set; }

        public string FIO { get; set; }

        public List<IdentityUser> Users { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
