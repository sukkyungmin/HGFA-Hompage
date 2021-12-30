using System;
using System.ComponentModel.DataAnnotations;

namespace HangilFA.Model
{
    public class QuestionsViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CreateUserId { get; set; }

        public string CreateUser { get; set; }

        [Display(Name = "Create User")]
        [DisplayFormat(NullDisplayText = "Not filled")]
        public string CreateFullname { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { get; set; }

        public int AnswerCount { get; set; }

        public bool ContentTimeCheck { get; set; }

    }
}
