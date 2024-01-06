using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_API.Model.Public
{
    public class CourseFee
    {
        public int Id { get; set; }

        public int DomainId { get; set; }
        [ForeignKey("DomainId")]
        public virtual Domain Domain { get; set; }

        public string FeeAmount { get; set; } = string.Empty;
        public double? Tax { get; set; }
        public string CreatedAt { get; set; } = string.Empty;

    }
}
