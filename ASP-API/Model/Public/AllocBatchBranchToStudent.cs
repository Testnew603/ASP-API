using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class AllocBatchBranchToStudent
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual StudentDetails StudentDetails { get; set; }

        public int BatchId { get; set; }
        [ForeignKey("BatchId")]
        [JsonIgnore]
        public virtual Batch Batch { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        [JsonIgnore]
        public virtual Branch Branch { get; set; }

        public string ModifiedAt { get; set; } = string.Empty;
        public BatchBranchToStudentStatus Status { get; set; } = BatchBranchToStudentStatus.PENDING;

    }

        public enum BatchBranchToStudentStatus
        {
            PENDING, ACTIVE, MODIFIED
        }
}
