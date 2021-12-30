using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangilFA.Areas.Identity.Data
{
    [Table(name: "AspNetSupporQuestionsAnswer")]
    public class SupporQuestionsAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public Guid SupporQuestionsId { get; set; }

        public string Content { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string AnswerUserId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string AnswerUser { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string AnswerUserFullname { get; set; }

        public DateTime CreateTime { get; set; }

        public SupporQuestions SupporQuestions { get; set; }

    }
}
