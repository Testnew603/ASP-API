using ASP_API.Model.Student;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_API.Model.Public
{
    public class CondonationFees
    {
        [Display(Name = "Student ID")]
        public int Id { get; set; }

        [ForeignKey("StudentId")]
        [Description("Details of the associated student.")]
        public int StudentId { get; set; }        
        public virtual StudentDetails Student {  get; set; }

        [Display(Name = "Fine Type")]
        public FineType FineType { get; set; } = FineType.OTHER;

        [Display(Name = "Fine Amount")]
        public int FineAmount { get; set; }

        [MaxLength(100)]
        [Description("Reason for the fine.")]
        public string Reason { get; set; } = string.Empty;

        [Display(Name = "Fine Status")]
        public FineStatus Status { get; set; } = FineStatus.NOTPAID;

        [Description("Date and time of creation.")]
        public string CreatedAt { get; set; } = string.Empty;
    }

    public enum FineStatus
    {
        PAID, NOTPAID, DISCOUNTED, BADDEBT
    }

    public enum FineType
    {
        LATEFEE, PENALTY, DUEFEE, OTHER
    }
}
