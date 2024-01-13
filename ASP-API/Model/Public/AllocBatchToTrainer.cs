using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class AllocBatchToTrainer
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual StudentDetails StudentDetails { get; set; }

        public int TrainerId { get; set; }
        [ForeignKey("TrainerId")]
        [JsonIgnore]
        public virtual Trainer Trainer{ get; set; }

        public string BatchRef { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public TrainingBatchStatus Status { get; set; } = TrainingBatchStatus.ACTIVE;
    }

    public enum TrainingBatchStatus
    {
        ACTIVE, BLOCK
    }
}
