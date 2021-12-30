using System;
using System.ComponentModel.DataAnnotations;

namespace HangilFA.Model
{
    public class QuestionsAnswerViewModel
    {
        public Int64 Id { get; set; }

        public Guid SupporQuestionsId { get; set; }
        public string Content { get; set; }

        public string AnswerUserId { get; set; }

        public string AnswerUser { get; set; }

        [Display(Name = "Create User")]
        [DisplayFormat(NullDisplayText = "Not filled")]
        public string AnswerUserFullname { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { get; set; }
    }
}
