using System;
using System.ComponentModel.DataAnnotations;

namespace HangilFA.Model
{
    public class NoticesViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string RichContent { get; set; }

        public string CreateUser { get; set; }

        public string ModifyUser { get; set; }

        [Display(Name = "Create User")]
        public string CreateFullname { get; set; }

        [Display(Name = "Modify User")]
        [DisplayFormat(NullDisplayText = "Not filled")]
        public string ModifyFullname { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime ModifyTime { get; set; }

        public int ModifyCount { get; set; }

    }
}
