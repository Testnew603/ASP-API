using ASP_API.Model.Student;
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

        public string StartedAt { get; set; } = string.Empty;
        public string EndedAt { get; set; } = string.Empty;
        public string Documents { get; set; } = string.Empty;
        public AgreementStatus Status { get; set; } = AgreementStatus.NOTVERIFIED;
    }

    public enum AgreementStatus
    {
        NOTVERIFIED, VERIFIED, INCOMPLETE, COMPLETE
    }
}
