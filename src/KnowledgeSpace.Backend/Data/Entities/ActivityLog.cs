﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using KnowledgeSpace.Backend.Data.Interfaces;

namespace KnowledgeSpace.Backend.Data.Entities
{
    [Table("ActivityLogs")]
    public class ActivityLog : IDateTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Action { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string EntityName { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string EntityId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string UserId { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }
    }
}
