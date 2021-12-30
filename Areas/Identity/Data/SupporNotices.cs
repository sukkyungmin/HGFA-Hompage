using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangilFA.Areas.Identity.Data
{
    [Table(name: "AspNetSupporNotices")]
    public class SupporNotices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        public string Content { get; set; }

        public string RichContent { get; set; }

        [Column(TypeName = "image")]
        public byte Images { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string CreateUser { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ModifyUser { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string CreateFullname { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ModifyFullname { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ModifyTime { get; set; }

        public int ModifyCount { get; set; }
    }
}
