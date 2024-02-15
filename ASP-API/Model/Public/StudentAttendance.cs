using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class StudentAttendance
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual StudentDetails Student { get; set; }

        public int AdvisorId { get; set; }
        [ForeignKey("AdvisorId")]
        [JsonIgnore]
        public virtual Advisor Advisor { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss t}")]
        [DataType(DataType.DateTime)]
        public DateTime EntryTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss t}")]
        [DataType(DataType.DateTime)]
        public DateTime ExitTime { get; set; }

        public string LateReason { get; set; } = string.Empty;
        public string LeavingReason { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        public AttendanceStatus Status { get; set; }
    }
    public enum AttendanceStatus
    {
        PENDING, PRESENT, ABSENT
    }
}
