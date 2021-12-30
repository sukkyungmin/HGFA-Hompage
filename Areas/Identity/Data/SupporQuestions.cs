using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangilFA.Areas.Identity.Data
{
    [Table(name: "AspNetSupporQuestions")]
    public class SupporQuestions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        public string Content { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string CreateUserId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string CreateUser { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string CreateFullname { get; set; }

        public DateTime CreateTime { get; set; }

        public int AnswerCount { get; set; }

    }
}
