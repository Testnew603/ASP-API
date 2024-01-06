using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class AllocStudentToAdvisor
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual StudentDetails StudentDetails { get; set; }

        public int AdvisorId { get; set; }
        [ForeignKey("AdvisorId")]
        [JsonIgnore]
        public virtual Advisor Advisor { get; set; }

        public int BatchId { get; set; }
        [ForeignKey("BatchId")]
        [JsonIgnore]
        public virtual Batch Batch { get; set;}

        public int DomainId { get; set; }
        [ForeignKey("DomainId")]
        [JsonIgnore]
        public virtual Domain Domain { get; set; }

        public string CreatedAt { get; set; } = DateTime.Now.ToString("dd:MM:yyyy");
        public StudentToAdvisorStatus Status { get; set; } = StudentToAdvisorStatus.PENDING;
    }

    public enum StudentToAdvisorStatus
    {
        ACTIVE, PENDING, BLOCK
    }
}
