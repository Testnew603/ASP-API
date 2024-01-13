using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.DTO
{
    public class AllocStudentToAdvisorDTO
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
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public StudentToAdvisorStatus Status { get; set; } = StudentToAdvisorStatus.PENDING;
    }
}
