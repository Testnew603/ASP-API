using ASP_API.Model.Staff;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class TrainingAndCommunication
    {
        [Key]
        public int Id { get; set; }

        public int TrainerId { get; set; }
        [ForeignKey("TrainerId")]
        [JsonIgnore]
        public virtual Trainer Trainer { get; set; }

        public int BatchId { get; set; }
        [ForeignKey("BatchId")]
        [JsonIgnore]
        public virtual Batch Batch { get; set; }

        public int BatchStrength { get; set; }
        public int AttendedStrength { get; set; }
        public string Activity { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public TrainingStatus Status { get; set; } = TrainingStatus.PENDING;
    }

    public enum TrainingStatus
    {
        PENDING, EXECUTED, POSTPONED, CANCELLED
    }
}
