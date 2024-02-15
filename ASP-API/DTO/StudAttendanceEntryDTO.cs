using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ASP_API.Model.Public;

namespace ASP_API.DTO
{
    public class StudAttendanceEntryDTO
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual StudentDetails Student { get; set; }      

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss t}")]
        [DataType(DataType.DateTime)]
        public DateTime EntryTime { get; set; }
        
        public string LateReason { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        public AttendanceStatus Status { get; set; }
    }
}

namespace ASP_API.DTO
{
    public class StudAttendanceExitDTO
    {
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual StudentDetails Student { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss t}")]
        [DataType(DataType.DateTime)]
        public DateTime ExitTime { get; set; }
        public string LeavingReason { get; set; } = string.Empty;                     
    }    
}
