using System;
using System.ComponentModel.DataAnnotations;

namespace HangilFA.Model
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [Display(Name ="User Name")]
        [DisplayFormat(NullDisplayText = "Not filled")]
        public string FullName { get; set; }

        public string Email { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { get; set; }

        public RoleViewModel[] Roles { get; set; }
    }
}
