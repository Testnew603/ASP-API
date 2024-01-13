using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ASP_API.Model.Public;

namespace ASP_API.DTO
{
    public class CondonationFeeDTO
    {
        public int Id { get; set; }

        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        [JsonIgnore]
        public virtual StudentDetails Student { get; set; }

        public FineType FineType { get; set; } = FineType.OTHER;

        public int FineAmount { get; set; }

        [MaxLength(100)]
        public string Reason { get; set; } = string.Empty;

        public FineStatus Status { get; set; } = FineStatus.NOTPAID;

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
    }
}
