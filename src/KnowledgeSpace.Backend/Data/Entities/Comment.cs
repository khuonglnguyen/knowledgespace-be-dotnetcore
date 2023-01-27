﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using KnowledgeSpace.Backend.Data.Interfaces;

namespace KnowledgeSpace.Backend.Data.Entities
{
    [Table("Comments")]
    public class Comment : IDateTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(500)]
        [Required]
        public string Content { get; set; }

        [Required]
        [Range(1, Double.PositiveInfinity)]
        public int KnowledgeBaseId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string OwnwerUserId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
