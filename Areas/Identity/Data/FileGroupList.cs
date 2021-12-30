using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangilFA.Areas.Identity.Data
{
    [Table(name: "AspNetFileGroupList")]
    public class FileGroupList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int FileMasterId { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string FileName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string DirectoryPath { get; set; }

        [Column(TypeName = "char(100)")]
        public string FileSize { get; set; }

        public DateTime CreateTime { get; set; }

        public FileMaster FileMaster { get; set; }
    }
}
