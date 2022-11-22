using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TrainScheduler.Model.Entities
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Schedule))]
        public int ScheduleId { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [Required]
        public string Fio { get; set; }

        public DateTime PurchaseTime { get; set; }

        public virtual Schedule Schedule { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
