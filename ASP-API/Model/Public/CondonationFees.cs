﻿using ASP_API.Model.Student;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP_API.Model.Public
{
    public class CondonationFees
    {
        public int Id { get; set; }

        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        [JsonIgnore]
        public virtual StudentDetails Student {  get; set; }

        public FineType FineType { get; set; } = FineType.OTHER;

        public int FineAmount { get; set; }

        [MaxLength(100)]
        public string Reason { get; set; } = string.Empty;

        public FineStatus Status { get; set; } = FineStatus.NOTPAID;

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
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
