using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class ReviewUpdates
    {
        [Key]
        public int Id { get; set; }

        public int ReviewerId { get; set; }
        [ForeignKey("ReviewerId")]
        [JsonIgnore]
        public virtual Reviewer Reviewer { get; set; }

        public int AdvisorId { get; set; }
        [ForeignKey("AdvisorId")]
        [JsonIgnore]
        public virtual Advisor Advisor { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual StudentDetails StudentDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime PostponedDate { get; set; }

        public PostponeStatus PostponeStatus { get; set; }
        public int WeekNo { get; set; }
        public ReviewStatus ReviewStatus { get; set; }


    }

    public enum PostponeStatus
    {
        PENDING, POSTPONED, REJECTED
    }

    public enum ReviewStatus
    {
        PENDING, UPCOMING, WEEKBACK, WEEKREPEAT, PASSED
        // based on summary status poor-WEEKBACK, average-WEEKREPEAT, good-PASSED
    }
}
