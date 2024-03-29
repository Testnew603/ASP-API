﻿using ASP_API.Model.Student;
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

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public BatchBranchToStudentStatus Status { get; set; } = BatchBranchToStudentStatus.ACTIVE;

    }

        public enum BatchBranchToStudentStatus
        {
            PENDING, ACTIVE, MODIFIED
        }
}
