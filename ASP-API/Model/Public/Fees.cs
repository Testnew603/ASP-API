using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class Fees
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

        public string Month { get; set; } = string.Empty;
        public string SpaceRent { get; set; } = string.Empty;
        public string PaidAmount { get; set; } = string.Empty;
        public string Balance { get; set; } = string.Empty;
        public string PaidThrough { get; set; } = string.Empty;
        public string LastDate { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = DateTime.Now.ToString("dd:MM:yyyy");
        public FeeStatus status { get; set; } = FeeStatus.NOTPAID;
    }

    public enum FeeStatus
    {
        PAID, NOTPAID, PENALTY, DUEPENALTY
    }
}
