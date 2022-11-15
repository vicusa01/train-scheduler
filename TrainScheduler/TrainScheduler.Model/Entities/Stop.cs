using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainScheduler.Model.Entities
{
    [Table("Stops")]
    public class Stop
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
