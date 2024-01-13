using ASP_API.Model.Staff;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class AllocBatchToAdvisor
    {
        [Key]
        public int Id { get; set; }

        public int AdvisorId { get; set; }
        [ForeignKey("AdvisorId")]
        [JsonIgnore]
        public virtual Advisor Advisor { get; set; }

        public int BatchId { get; set; }
        [ForeignKey("BatchId")]
        [JsonIgnore]
        public virtual Batch Batch { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public BatchToAdvisorStatus Status { get; set; } = BatchToAdvisorStatus.ACTIVE;
    }
        public enum BatchToAdvisorStatus
        {   
            PENDING, ACTIVE, BLOCK, MODIFIED
        }
}
