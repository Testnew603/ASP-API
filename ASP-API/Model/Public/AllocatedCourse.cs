using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class AllocatedCourse
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual StudentDetails Student { get; set; }

        public int AdvisorId { get; set; }
        [ForeignKey("AdvisorId")]
        [JsonIgnore]
        public virtual Advisor Advisor { get; set; }

        public int DomainId { get; set; }
        [ForeignKey("DomainId")]
        [JsonIgnore]
        public virtual Domain Domain { get; set; }

        public int BatchId { get; set; }
        [ForeignKey("BatchId")]
        [JsonIgnore]
        public virtual Batch Batch { get; set; }

        public int Duration { get; set; }
        public Type FeeType { get; set; } = Type.NONE;
    }

    public enum Type
    {
        NONE, UP_FRONT, AFTER_PLACEMENT, OTHER
    }
}
