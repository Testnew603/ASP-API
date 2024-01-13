using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class ReviewSummary
    {
        [Key]
        public int Id { get; set; }

        public int ReviewId { get; set; }
        [ForeignKey("ReviewId")]
        [JsonIgnore]
        public virtual ReviewUpdates ReviewUpdates { get; set; }

        public ReviewType ReviewType { get; set; }
        public string Summary { get; set; } = string.Empty;
        public double Marks { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        //normally this date match with the same date in review date
        //if suppose reviewer made any change after the entry then the
        // status would be MODIFIED
        public SummaryStatus Status { get; set; } 
        // this based on marks eg: marks:1-50-poor, 51-60-average, 61-70-good, 71-100-V.good
        // *status should be MODIFIED
            // - createdAt > ReviewDate (if summary exist)
    }       

    public enum ReviewType
    {
        WEEKREVIEW, PROJECTREVIEW
    }

    public enum SummaryStatus
    {
        BELOWAVERAGE, AVERAGE, GOOD, VERYGOOD
    }


}
