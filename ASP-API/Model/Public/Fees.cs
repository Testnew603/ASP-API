﻿using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class Fees
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual StudentDetails StudentDetails { get; set; }

        public int AdvisorId { get; set; }
        [ForeignKey("AdvisorId")]
        [JsonIgnore]
        public virtual Advisor Advisor { get; set; }

        public string Month { get; set; } = string.Empty;
        public double SpaceRent { get; set; }
        public double PaidAmount { get; set; }
        public double Balance { get; set; }
        public string PaidThrough { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime LastDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public FeeStatus status { get; set; } = FeeStatus.NOTPAID;
    }

    public enum FeeStatus
    {
        PAID, NOTPAID, BALANCE
    }
}
