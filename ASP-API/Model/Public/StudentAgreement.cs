using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class StudentAgreement
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual StudentDetails Student { get; set; }

        public int DomainId { get; set; }
        [ForeignKey("DomainId")]
        [JsonIgnore]
        public virtual Domain Domain { get; set; }

        public int CourseFeeID { get; set; }
        [ForeignKey("CourseFeeID")]
        [JsonIgnore]
        public virtual CourseFee CourseFee { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartedAt { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EndedAt { get; set; } = DateTime.Now;
        
        public string Documents { get; set; } = string.Empty;
        public AgreementStatus Status { get; set; } = AgreementStatus.NOTVERIFIED;
    }

    public enum AgreementStatus
    {
        NOTVERIFIED, VERIFIED, INCOMPLETE, COMPLETE
    }
}
