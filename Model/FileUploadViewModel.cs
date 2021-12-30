using System;
using System.ComponentModel.DataAnnotations;

namespace HangilFA.Model
{
    public class FileUploadViewModel
    {
        public Guid Id { get; set; }

        public int FileMasterId { get; set; }

        public string FileName { get; set; }

        public string DirectoryPath { get; set; }

        public string FileSize { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { get; set; }

    }
}
